
/*  This was adopted from an example project written by
 *  author: Marc Macniel (https://github.com/mmacneil)
 *  cited in a blog post
 *  url: https://fullstackmark.com/post/13/jwt-authentication-with-aspnet-core-2-web-api-angular-5-net-core-identity-and-facebook-login
 *  demonstrating how to implement a framework for authenticating users with JWT
 *  in an ASP.NET Core 2/Angular 5 web application.
 *  
 *  The original class this class is based upon can be found on the project's GitHub repository
 *  url: https://github.com/mmacneil/AngularASPNETCore2WebApiAuth
 *  file url: https://github.com/mmacneil/AngularASPNETCore2WebApiAuth/blob/master/src/Controllers/AccountsController.cs
 */

using System.Linq;
using System;
using System.Threading.Tasks;
using IMOMaritimeSingleWindow.Data;
using IMOMaritimeSingleWindow.Helpers;
using IMOMaritimeSingleWindow.Identity;
using IMOMaritimeSingleWindow.Identity.Models;
using IMOMaritimeSingleWindow.ViewModels;
using IMOMaritimeSingleWindow.Auth;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Policies = IMOMaritimeSingleWindow.Helpers.Constants.Strings.Policies;
using Claims = IMOMaritimeSingleWindow.Helpers.Constants.Strings.Claims;
using Microsoft.AspNetCore.Http.Extensions;
using System.Collections.Generic;

namespace IMOMaritimeSingleWindow.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        // [HasClaim(Claims.Types.USER, Claims.Values.REGISTER)]
        // POST api/account/user
        [HttpPost("user/test")]
        public async Task<IActionResult> RegisterTest([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicationUser = _mapper.Map<ApplicationUser>(model);

            // Validate user and try to create new user in the backing store
            var result = await _userManager.CreateAsync(applicationUser);

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));
            
            // Functionality for sending email to user with new account, so that they can set their own password
            var callbackUrl = GetEmailLink(applicationUser.Email);

            return new OkObjectResult(callbackUrl);
        }

        [HasClaim(Claims.Types.USER, Claims.Values.REGISTER)]
        // POST api/account/user
        [HttpPost("user/")]
        public async Task<IActionResult> Register([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicationUser = _mapper.Map<ApplicationUser>(model);

            // Validate user and try to create new user in the backing store
            var result = await _userManager.CreateAsync(applicationUser);

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            // Functionality for sending email to user with new account, so that they can set their own password
            var callbackUrl = GetEmailLink(applicationUser.Email);

            // Send confirmation link to user's registered email address

            return new OkObjectResult($"Account created. Confirmation link sent to {model.Email}");
        }


        [HasClaim(Claims.Types.USER, Claims.Values.REGISTER)]
        // POST api/account/user
        [HttpPost("user")]
        public async Task<IActionResult> RegisterWithPassword([FromBody]RegistrationWithPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Tries to map the model to an object of type ApplicationUser
            var userIdentity = _mapper.Map<ApplicationUser>(model);

            // Verify the role the user is attempted added to exists
            var role = await _roleManager.FindByNameAsync(model.RoleName);
            if (role == null)
                return BadRequest($"The role \"{model.RoleName}\" does not exist! User not created.");

            // Validate user and try to create new user with given password in the backing store
            var result = await _userManager.CreateAsync(userIdentity, model.Password);
            if (!result.Succeeded)
                return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            // Add the user to the specified role
            result = await _userManager.AddToRoleAsync(userIdentity, model.RoleName);
            if (!result.Succeeded)
                return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));
            
            return new OkObjectResult("Account created");
        }
        
        private async Task<Uri> GetEmailLink(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            QueryBuilder queryBuilder = new QueryBuilder(
                new Dictionary<string, string>()
                {
                    { "userId", user.Id.ToString() },
                    { "emailConfirmationToken", emailConfirmationToken }
                }
            );

            UriBuilder uriBuilder = new UriBuilder(GetCallBackUri("ConfirmEmail"))
            {
                Query = queryBuilder.ToString()
            };

            return uriBuilder.Uri;
        }

        [AllowAnonymous]
        [HttpGet("user/email/confirm")]
        public async Task<IActionResult> ConfirmEmail(string userId, string emailConfirmationToken)
        {
            if (userId == null || emailConfirmationToken == null)
                return BadRequest();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest();

            var emailVerificationResult = await _userManager.ConfirmEmailAsync(user, emailConfirmationToken);
            if (!emailVerificationResult.Succeeded)
                return BadRequest();

            var passwordChangeToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            return Json(passwordChangeToken);
        }


        [AllowAnonymous]
        [HttpPut("user/password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (model.UserId == null || model.NewPassword == null || model.PasswordResetToken == null)
                return BadRequest();

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return BadRequest();

            var result = await _userManager.ResetPasswordAsync(user, model.PasswordResetToken, model.NewPassword);
            if (result.Succeeded)
                return Ok("Password changed");

            return BadRequest();
        }

        /// <summary>
        /// Gets the roles assignable to users.
        /// </summary>
        /// <returns>A list of rolenames</returns>
        [Authorize(Roles = Constants.Strings.UserRoles.Admin + ", " + Constants.Strings.UserRoles.SuperAdmin)]
        [HttpGet("roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roleMan = _roleManager as ApplicationRoleManager;
            var roleNames = await roleMan.GetAllRoles();
            roleNames.Remove("super_admin");
            return Ok(roleNames);
        }

        /// <summary>
        /// Gets the username of the logged in user
        /// </summary>
        /// <returns>Username as a string</returns>
        [Authorize]
        [HttpGet("user/name")]
        public async Task<IActionResult> GetUserName()
        {
            var idClaim = User.Claims.FirstOrDefault(cl => cl.Type == Constants.Strings.JwtClaimIdentifiers.Id);
            var userId = idClaim.Value;
            var user = await _userManager.FindByIdAsync(userId);
            return Json(user.UserName);
        }

        /// <summary>
        /// Gets the user by email address
        /// </summary>
        /// <param name="email">The email address of the user</param>
        /// <returns></returns>
        [Authorize(Roles = Constants.Strings.UserRoles.Admin + ", " + Constants.Strings.UserRoles.SuperAdmin)]
        [HttpGet("user/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return Json(user);
        }

        /// <summary>
        /// Checks whether a user with the given email address has been created already.
        /// </summary>
        /// <param name="email">The email address to search by</param>
        /// <returns>A boolean</returns>
        [Authorize(Roles = Constants.Strings.UserRoles.Admin + ", " + Constants.Strings.UserRoles.SuperAdmin)]
        [HttpGet("emailTaken/{email}")] 
        public async Task<IActionResult> EmailTaken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return Ok(user != null);
        }

        /// <summary>
        /// Gets the claims of the logged in user
        /// </summary>
        /// <returns>A list of claims</returns>
        [Authorize]
        [HttpGet("user/claims")]
        public async Task<IActionResult> GetUserClaims()
        {
            // Finds the id of the logged in user, if present in JWT
            var userIdClaim = User.FindFirst(claim => claim.Type == Constants.Strings.JwtClaimIdentifiers.Id);
            if (userIdClaim == null)
                return new BadRequestObjectResult("id not present on jwt");
            var userId = userIdClaim.Value;

            var user = await _userManager.FindByIdAsync(userId);
            var claims = await _userManager.GetClaimsAsync(user);
            return Json(claims);
        }


        #region Helper methods

        private Uri GetCallBackUri()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            int PORT = _env.IsDevelopment() ? 4200 : 80;

            UriBuilder uriBuilder = new UriBuilder
            {
                Scheme = request.Scheme,
                Host = request.Host.Host,
                Port = PORT
            };
            return uriBuilder.Uri;
        }
        // Returns an URI pointing to the route in the web application
        private Uri GetCallBackUri(string route)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            int PORT = _env.IsDevelopment() ? 4200 : 80;

            UriBuilder uriBuilder = new UriBuilder
            {
                Scheme = request.Scheme,
                Host = request.Host.Host,
                Path = route,
                Port = PORT
            };
            return uriBuilder.Uri;
        }


        private Uri GetRequestUri()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            UriBuilder uriBuilder = new UriBuilder
            {
                Scheme = request.Scheme,
                Host = request.Host.Host,
                Path = Url.Action(action: this.GetActionName(), controller: this.GetControllerName()),
                Port = HttpContext.Connection.LocalPort
            };
            return uriBuilder.Uri;

        }

        private Uri GetAbsoluteUri()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            UriBuilder uriBuilder = new UriBuilder
            {
                Scheme = request.Scheme,
                Host = request.Host.Host,
                Path = request.Path.ToString(),
                Query = request.QueryString.ToString(),
                Port = HttpContext.Connection.LocalPort
            };
            return uriBuilder.Uri;
        }

        #endregion

    }
}
