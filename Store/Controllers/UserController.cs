using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Store.Contracts;
using Store.Contracts.Authentication;
using Store.Contracts.ViewModel;
using Store.Data.Entities.Identity;
using Store.Services;
using Store.Services.Middleware;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;
using System.Web;

namespace Store.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMailingService _mailSenderManager;
        private readonly LinkGenerator _linkGenerator;

        public UserController(IOptions<AppSettings> settings
            , ILocalPageData pageData
            , IMapper mapper
            , UserManager<ApplicationUser> userManager
            , SignInManager<ApplicationUser> signInManager,
            ILogger<UserController> logger,
            IMailingService mailSenderManager,
            LinkGenerator linkGenerator)
             : base(settings, pageData, mapper, logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mailSenderManager = mailSenderManager;
            _linkGenerator = linkGenerator;
        }

        [HttpGet("getProfile")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProfile()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = (await _userManager.GetUserAsync(HttpContext.User)).Id;
                //string userIdStr = HttpContext.Session.GetString("userId");
                if (userId != 0)
                {
                    var user = await _userManager.FindByIdAsync(userId.ToString());
                    if (user != null)
                    {
                        var userRoles = await _userManager.GetRolesAsync(user);
                        var profile = new ProfileViewModel()
                        {
                            Roles = userRoles,
                            UserName = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Id = user.Id
                        };
                        return Ok(profile);
                    }
                }
            }
            return Ok();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AuthenticationModel model)
        {
            if (string.IsNullOrWhiteSpace(model.UserName) || string.IsNullOrWhiteSpace(model.Password))
            {
                return (IActionResult)Unauthorized("Nieprawidłowe dane do logowania");
            }
            var user = await _userManager.FindByEmailAsync(model.UserName);

            if (user == null || (user.RemovalDateUtc.HasValue && user.RemovalDateUtc < DateTime.UtcNow))
            {
                return Unauthorized("Nieprawidłowe dane do logowania");
            }

            if (user.RemovalDateUtc.HasValue && user.RemovalDateUtc < DateTime.UtcNow)
            {
                return Unauthorized("Nieprawidłowe dane do logowania");
            }

            if (user.Status == EUserStatus.Blocked)
            {
                return Unauthorized("Nieprawidłowe dane do logowania");
            }

            var result = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!result)
            {
                return Unauthorized("Nieprawidłowe dane do logowania");
            }
            else
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var roleClaims = new List<Claim>();
                foreach (var role in userRoles)
                {
                    roleClaims.Add(new Claim(ClaimTypes.Role, role));
                }
                var claims = new[]
                {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                claims = claims.Concat(roleClaims).ToArray();
                var tokenKey = Encoding.ASCII.GetBytes(base._appsettings.Secret);
                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                var acToken = new AccessToken()
                {
                    TokenString = tokenString,
                    ValidFrom = token.ValidFrom,
                    ValidTo = token.ValidTo
                };
                Response.Cookies.Append(JWTInHeaderMiddleware.AuthenticationCookieName, JsonSerializer.Serialize(acToken), new CookieOptions() { HttpOnly = true });
                var profile = new ProfileViewModel()
                {
                    Roles = userRoles,
                    UserName = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = user.Id
                };
                var identity = new System.Security.Principal.GenericIdentity(user.UserName);
                var principal = new GenericPrincipal(identity, new string[0]);
                Request.HttpContext.User = principal;
                Thread.CurrentPrincipal = principal;
                return Ok(profile);
            }
        }

        [HttpGet("logOff")]
        public async Task<IActionResult> LogOff()
        {
            var userId = (await _userManager.GetUserAsync(HttpContext.User)).Id;
            Response.Cookies.Delete(JWTInHeaderMiddleware.AuthenticationCookieName);
            await _signInManager.SignOutAsync();
            return Ok();
        }

        //[HttpGet("VerifyAccountConfirmation/{userId}/{code}")] -- this route configuration couses double escape sequnece error
        //second solution: enable double escape sequnece in web config and   //code = Uri.UnescapeDataString(code) here
        [HttpGet("VerifyAccountConfirmation")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyAccountConfirmation(long? userId, string code)
        {
            ViewBag.RobotsIndex = false;

            if (!userId.HasValue || code == null)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return BadRequest();
            }


            //code = Uri.UnescapeDataString(code);
            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return Redirect("https://www.google.com");
        }


        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUserViewModel vm)
        {
            Func<Task<IActionResult>> fun = async () =>
            {

                if (!string.Equals(vm.Password, vm.RepeatPassword))
                {
                    ModelState.AddModelError(nameof(RegisterUserViewModel.RepeatPassword), "Hasła się nie zgadzają");
                }
                if (await _userManager.FindByNameAsync(vm.Email) != null)
                {
                    ModelState.AddModelError(nameof(RegisterUserViewModel.Email), "Użytkownik o podanym Loginie już istnieje");
                }
                if (await _userManager.FindByEmailAsync(vm.Email) != null)
                {
                    ModelState.AddModelError(nameof(RegisterUserViewModel.Email), "Użytkownik o podanym email już istnieje");
                }

                var user = Mapper.Map<RegisterUserViewModel, ApplicationUser>(vm);

                var userRoles = new List<string>();
                userRoles.Add(ERole.User.ToString());

                var result = await _userManager.CreateAsync(user, vm.Password);
                if (result.Succeeded)
                {
                    if (userRoles.Count > 0)
                    {
                        await _userManager.AddToRolesAsync(user, userRoles);
                    }
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //code = HttpUtility.UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = _linkGenerator.GetUriByAction("VerifyAccountConfirmation", "User", new { userId = user.Id, code = code }, scheme: HttpContext.Request.Scheme, HttpContext.Request.Host);
                    //var callbackUrl = Url.Action("VerifyAccountConfirmation", "User", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    try
                    {
                        await _mailSenderManager.SendVerificationCodeMail(vm.Email, callbackUrl);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Wystąpił błąd wysyłania wiadomości z linkiem aktywacyjnym", null);
                        return BadRequest();
                    }

                    return Ok();
                }

                var errors = new List<string>();

                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }

                if (errors.Count > 0)
                {

                    var det = string.Join("<br />", errors);
                    return Problem(detail: det, statusCode: 400);
                }

                return BadRequest();
            };
            return await fun();
        }
    }
}