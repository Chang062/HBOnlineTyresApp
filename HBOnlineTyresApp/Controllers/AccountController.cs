using HBOnlineTyresApp.Data;
using HBOnlineTyresApp.Data.Static;
using HBOnlineTyresApp.Data.ViewModels;
using HBOnlineTyresApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;

namespace HBOnlineTyresApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _emailSender = emailSender;

        }

        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }
        public IActionResult Login() => View(new LoginVM());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    bool emailStatus = await _userManager.IsEmailConfirmedAsync(user);
                    if (emailStatus == false)
                    {
                       
                        ModelState.AddModelError(nameof(loginVM.EmailAddress), "Email is unconfirmed, please confirm it first");
                        return View(loginVM);
                    }
                }
            }
            TempData["Error"] = "Invalid Credentials. Please Try Again";
            return View(loginVM);
        }

        [HttpGet]
        public IActionResult ForgotPassword() => View(new ForgotPasswordVM());

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult>ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            if (ModelState.IsValid)
            {
                var appUser = await _userManager.FindByEmailAsync(forgotPasswordVM.EmailAddress);
                if(appUser != null && await _userManager.IsEmailConfirmedAsync(appUser))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(appUser);
                    var callbackUrl = Url.Action("ResetPassword", "Account", new {email = forgotPasswordVM.EmailAddress, token = token }, Request.Scheme);

                    await _emailSender.SendEmailAsync(forgotPasswordVM.EmailAddress, "Password Reset",
                      $"Please reset your account password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    return View("ForgotPasswordConfirmation");
                }
                return View("ForgotPasswordConfirmation");
            }
            return View(forgotPasswordVM);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword( string token, string email)
        { 
            if(token == null || email == null)
            {
                ModelState.AddModelError("", "Invalid password reset token");
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPasswordVM)
        {
            if (ModelState.IsValid)
            {
                var appUser = await _userManager.FindByEmailAsync(resetPasswordVM.Email);
                if(appUser != null)
                {
                    var result = await _userManager.ResetPasswordAsync(appUser, resetPasswordVM.Token, resetPasswordVM.Password);
                    if(result.Succeeded)
                    {
                        return View("ResetPasswordConfirmation");
                    }
                    foreach ( var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(resetPasswordVM);
                }
                return View("ResetPasswordConfirmation");
            }
            return View(resetPasswordVM);
        }

        public IActionResult Register() => View(new RegisterVM());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            string returnUrl = null;
            returnUrl ??= Url.Content("~/");

            if (!ModelState.IsValid) return View(registerVM);
          var newUser = new ApplicationUser()
            {
                FName = registerVM.FullName,
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress,
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

                var userId = await _userManager.GetUserIdAsync(newUser);
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
               // token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { token, email = registerVM.EmailAddress }, Request.Scheme);

                await _emailSender.SendEmailAsync(registerVM.EmailAddress, "Confirm your email",
                      $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                return View("RegistrationCompleted");
            }
            foreach (var error in newUserResponse.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }


            return View(registerVM);
        }
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return View("Error");

            var result = await _userManager.ConfirmEmailAsync(user, token);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}