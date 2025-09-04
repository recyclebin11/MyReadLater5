using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ReadLater5.DTO;
using ReadLater5.Helpers;
using ReadLater5.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ReadLater5.Controllers
{
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }


        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("jwttoken")]
        public async Task<IActionResult> SignIn(LoginRequest input)
        {
            var user = await _userManager.FindByEmailAsync(input.Username);
            if (user == null)
            {
                throw new Exception("Username or password is incorrect");
            }

            var loginResult = await _signInManager.PasswordSignInAsync(user, input.Password, false, lockoutOnFailure: false);

            if(!loginResult.Succeeded)
            {
                throw new Exception("Username or password is incorrect");
            }

            var token = JwtHelper.GetToken(user, _configuration, out var expires);

            return Ok(new AuthResponse
            {
                Token = token,
                Expires = expires,
                Username = user.UserName,
                Email = user.Email,
                UserId = user.Id
            });

        }


        [AllowAnonymous]
        public async Task Auth0SignIn(string returnUrl = "/")
        {
            returnUrl = "https://localhost:44326/";
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
          // Indicate here where Auth0 should redirect the user after a login.
          // Note that the resulting absolute Uri must be added to the
          // **Allowed Callback URLs** settings for the app.
          .WithRedirectUri(returnUrl)
          .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);



        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var authScheme = User.Identity.AuthenticationType;

            if (authScheme != Auth0Constants.AuthenticationScheme)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");

            }
            else
            {

                var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
                    // Indicate here where Auth0 should redirect the user after a logout.
                    // Note that the resulting absolute Uri must be whitelisted in 
                    .Build();

                await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Index", "Home");
            }
        }





    }
}
