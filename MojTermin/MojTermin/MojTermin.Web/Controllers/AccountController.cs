using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MojTermin.Domain.DomainModels;
using MojTermin.Domain.DTO;
using MojTermin.Domain.Identity;
using MojTermin.Service.Interface;

namespace MojTermin.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<MojTerminUser> _signInManager;
        private readonly UserManager<MojTerminUser> _userManager;
        private readonly IRoleService _roleService;
        public AccountController(SignInManager<MojTerminUser> signInManager, UserManager<MojTerminUser> userManager, IRoleService roleService)
        {

            _userManager = userManager;
            _signInManager = signInManager;
            _roleService = roleService;
        }

        public IActionResult Register()
        {
            List<Role> roles = _roleService.GetAllRoles().ToList();
            ViewBag.Message = roles;
            return View();
        }


        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(UserRegistrationDto request)
        {
            List<Role> roles = _roleService.GetAllRoles().ToList();
            ViewBag.Message = roles;
            if (ModelState.IsValid)
            {
                var userCheck = await _userManager.FindByEmailAsync(request.Email);
                if (userCheck == null)
                {
                    var role = _roleService.Get(request.Role);
                    var user = new MojTerminUser
                    {
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        UserName = request.Email,
                        NormalizedUserName = request.Email,
                        Email = request.Email,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        Address = request.Address,
                        Role = role
                    };
                    var result = await _userManager.CreateAsync(user, request.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        if (result.Errors.Count() > 0)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("message", error.Description);
                            }
                        }
                        return View(request);
                    }
                }
                else
                {
                    ModelState.AddModelError("message", "Email already exists.");
                    return View(request);
                }
            }
            return View(request);
        }

        public IActionResult Login()
        {
            List<Role> roles = _roleService.GetAllRoles().ToList();
            ViewBag.Message = roles;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null && !user.EmailConfirmed)
                {
                    ModelState.AddModelError("message", "Email not confirmed yet");
                    return View(model);

                }
                if (await _userManager.CheckPasswordAsync(user, model.Password) == false)
                {
                    ModelState.AddModelError("message", "Invalid credentials");
                    return View(model);

                }

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);

                if (result.Succeeded)
                {
                    await _userManager.AddClaimAsync(user, new Claim("UserRole", "Admin"));
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("message", "Invalid login attempt");
                    return View(model);
                }
            }
            return View(model);
        }
    }
}
