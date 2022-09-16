using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MojTermin.Domain.DomainModels;
using MojTermin.Domain.Identity;
using MojTermin.Service.Interface;
using MojTermin.Web.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MojTermin.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<MojTerminUser> _userManager;
        private readonly IRoleService _roleService;

        public HomeController(ILogger<HomeController> logger, UserManager<MojTerminUser> userManager, IRoleService roleService)
        {
            _logger = logger;
            _userManager = userManager;
            _roleService = roleService;
        }

        public IActionResult Index()
        {
            List<Role> roles = _roleService.GetAllRoles().ToList();
            var userId = _userManager.GetUserId(HttpContext.User);
            
            MojTerminUser user =  _userManager.FindByIdAsync(userId).Result;
            if (user != null)
            {
                string role = user.Role.Name;
                ViewBag.Message1 = role;
            }
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
