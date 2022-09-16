using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MojTermin.Domain.DomainModels;
using MojTermin.Domain.Identity;
using MojTermin.Service.Interface;
using MojTermin.Web.Data;

namespace MojTermin.Web.Controllers
{
    public class SpecialtiesController : Controller
    {
        private readonly ISpecialtyService _specialtyService;
        private readonly UserManager<MojTerminUser> _userManager;
        private readonly IRoleService _roleService;
        private readonly IDoctorService _doctorService;

        public SpecialtiesController(ISpecialtyService specialtyService, UserManager<MojTerminUser> userManager, IRoleService roleService, IDoctorService doctorService)
        {
            _specialtyService = specialtyService;
            _userManager = userManager;
            _roleService = roleService;
            _doctorService = doctorService;
        }

        // GET: Specialties
        public IActionResult Index()
        {
            List<Role> roles = _roleService.GetAllRoles().ToList();
            var userId = _userManager.GetUserId(HttpContext.User);

            List<Doctor> doctors = _doctorService.GetAllDoctors().ToList();
            ViewBag.Message = doctors;

            MojTerminUser user = _userManager.FindByIdAsync(userId).Result;
            if (user != null)
            {
                string role = user.Role.Name;
                ViewBag.Message1 = role;
            }
            return View(this._specialtyService.GetAll());
        }

        // GET: Specialties/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialty = this._specialtyService.Get((Guid)id);
            if (specialty == null)
            {
                return NotFound();
            }

            return View(specialty);
        }

        // GET: Specialties/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Specialties/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Id")] Specialty specialty)
        {
            if (ModelState.IsValid)
            {
                specialty.Id = Guid.NewGuid();
                this._specialtyService.CreateNewSpecialty(specialty);
                return RedirectToAction(nameof(Index));
            }
            return View(specialty);
        }

        // GET: Specialties/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialty = this._specialtyService.Get((Guid)id);
            if (specialty == null)
            {
                return NotFound();
            }
            return View(specialty);
        }

        // POST: Specialties/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Name,Id")] Specialty specialty)
        {
            if (id != specialty.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this._specialtyService.UpdateSpecialty(specialty);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecialtyExists(specialty.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(specialty);
        }

        // GET: Specialties/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialty = this._specialtyService.Get((Guid)id);
            if (specialty == null)
            {
                return NotFound();
            }

            return View(specialty);
        }

        // POST: Specialties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            this._specialtyService.DeleteSpecialty(id);
            return RedirectToAction(nameof(Index));
        }

        private bool SpecialtyExists(Guid? id)
        {
            return this._specialtyService.Get(id) != null;
        }
    }
}
