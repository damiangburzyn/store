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
using Store.Services.Middleware;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserController(IOptions<AppSettings> settings
            , ILocalPageData pageData
            , IMapper mapper
            , UserManager<ApplicationUser> userManager
            , SignInManager<ApplicationUser> signInManager,
            ILogger<UserController> logger)
             : base(settings, pageData, mapper, logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet("test")]

        public IActionResult Test()
        {

            return Ok("test ok");
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

        // [ValidateAntiForgeryToken]
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AuthenticationModel model)
        {


                if (string.IsNullOrWhiteSpace(model.UserName) || string.IsNullOrWhiteSpace(model.Password))
                    return (IActionResult)Unauthorized("Nieprawidłowe dane do logowania");

                var user = await _userManager.FindByEmailAsync(model.UserName);

                if (user == null || (user.RemovalDateUtc.HasValue && user.RemovalDateUtc < DateTime.UtcNow))
                    return Unauthorized("Nieprawidłowe dane do logowania");

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
                    //HttpContext.Session.SetString("userId", user.Id.ToString());
                    var acToken = new AccessToken()
                    {
                        TokenString = tokenString,
                        ValidFrom = token.ValidFrom,
                        ValidTo = token.ValidTo
                    };

                    Response.Cookies.Append(JWTInHeaderMiddleware.AuthenticationCookieName, JsonSerializer.Serialize(acToken), new CookieOptions() { HttpOnly = true }); ;

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
          
                    return Ok(vm);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return Ok(vm);
            };
            return await fun(); 
        }





    }
}