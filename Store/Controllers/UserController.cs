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
            , SignInManager<ApplicationUser> signInManager)
             : base(settings, pageData, mapper)
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
            //string token = HttpContext.Session.GetString("token");
            string userIdStr = HttpContext.Session.GetString("userId");
            var user = await _userManager.FindByIdAsync(userIdStr);
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
            return Ok();
        }

       // [ValidateAntiForgeryToken]
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AuthenticationModel model)
        {
            return await this.WrapExceptionAsync(async () =>
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
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
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
                    HttpContext.Session.SetString("userId", user.Id.ToString());
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
            });
        }


        public async Task<IActionResult> LogOff()
        {
            var userId = (await _userManager.GetUserAsync(HttpContext.User)).Id;
            HttpContext.Session.SetString("userId", "");

            Response.Cookies.Delete(JWTInHeaderMiddleware.AuthenticationCookieName);

            await _signInManager.SignOutAsync();
            return Ok();
        }

    }
}