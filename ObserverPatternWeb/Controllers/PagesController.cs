using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using ObserverPattern.Base.Organization;
using ObserverPatternWeb.Resources.OPStrings;

namespace ObserverPatternWeb.Controllers
{
    public class PagesController : BaseController
    {
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        private readonly IEmailSender _emailSender;

        private readonly UserManager<CusUser> _userManager;
        private readonly SignInManager<CusUser> _signInManager;
        private readonly ILogger<HomeController> _logger;
        private IStringLocalizer<OPStringsResources> _localizer;
        
        public PagesController(
            SignInManager<CusUser> signInManager, 
            ILogger<HomeController> logger, 
            IStringLocalizer<OPStringsResources> localizer, 
            UserManager<CusUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _localizer = localizer;
        }

        [ViewData]
        public CurrentUser User => OPUser;

        [Authorize]
        public IActionResult SearchResults()
        {
            return View();
        }

        public IActionResult LockScreen()
        {
            return View();
        }

        public IActionResult Invoice()
        {
            return View();
        }

        public IActionResult InvoicePrint()
        {
            return View();
        }

        public async Task<IActionResult> Login(LoginModel input)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("zh")),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            if (!ModelState.IsValid)
            {
                await _signInManager.SignOutAsync();
                return View(input);
            }

            var returnUrl = Url.Action("Dashboard_1", "Dashboards");
            new List<AuthenticationScheme>();
            (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();


            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(input.UserName, input.Password, input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View();
                }
            }

            // If we got this far, something failed, redisplay form
            return View(); ;
        }

        public IActionResult Login_2()
        {
            return View();
        }

        public async Task<IActionResult> Register(RegisterModel Input)
        {
            var returnUrl = Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new CusUser {
                    UserName = Input.UserName,
                    DisplayName = Input.DisplayName,
                    PhoneNumber = Input.PhoneNumber,
                    Email = Input.Email };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(Input);
        }

        public IActionResult NotFoundError()
        {
            return View();
        }

        public IActionResult InternalServerError()
        {
            return View();
        }

        public IActionResult EmptyPage()
        {
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
    }

    public class LoginModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Full name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public LoginModel Input { get; }
    }
    public class RegisterModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Full name")]
        public string UserName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Full name")]
        public string DisplayName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }
        

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public RegisterModel Input { get; }
    }
}