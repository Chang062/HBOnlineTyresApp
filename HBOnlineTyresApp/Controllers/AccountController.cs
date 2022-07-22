﻿using HBOnlineTyresApp.Data;
using HBOnlineTyresApp.Data.Static;
using HBOnlineTyresApp.Data.ViewModels;
using HBOnlineTyresApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HBOnlineTyresApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;
        
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;

        }
        public IActionResult Login() => View(new LoginVM());

        [HttpPost]
        public async Task <IActionResult>Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if(user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if(passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if(result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    TempData["Error"] = "Invalid Credentials. Please Try Again";
                    return View(loginVM);
                }
            }
            TempData["Error"] = "Invalid Credentials. Please Try Again";
            return View(loginVM);
        }

        public IActionResult Register() => View(new RegisterVM());

        [HttpPost]
        public async Task <IActionResult>Register(RegisterVM registerVM)
        {
            if(!ModelState.IsValid) return View(registerVM);
            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);

            if(user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerVM);
            }

            var newUser = new ApplicationUser()
            {
                FName = registerVM.FullName,
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress,
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if(newUserResponse.Succeeded)
            await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            
            return View("RegistrationCompleted");
            
        }

        [HttpPost]
        public async Task <IActionResult> Logout()
        {
           await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
