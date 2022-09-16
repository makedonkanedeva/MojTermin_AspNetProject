using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
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
    public class DoctorsController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly ISpecialtyService _specialtyService;
        private readonly UserManager<MojTerminUser> _userManager;
        private readonly IRoleService _roleService;
        private readonly IPatientService _patientService;
        private readonly IVisitService _visitService;
        private readonly IReferralService _referralService;

        public DoctorsController(IDoctorService doctorService, ISpecialtyService specialtyService, UserManager<MojTerminUser> userManager, IRoleService roleService, IPatientService patientService, IVisitService visitService, IReferralService referralService)
        {
            _doctorService = doctorService;
            _specialtyService = specialtyService;
            _userManager = userManager;
            _roleService = roleService;
            _patientService = patientService;
            _visitService = visitService;
            _referralService = referralService;
        }

        // GET: Doctors
        public IActionResult Index()
        {
            List<Specialty> specialties = _specialtyService.GetAll().ToList();
            ViewBag.Message = specialties;
            List<Patient> patients = _patientService.GetAllPatients().ToList();
            ViewBag.Patients = patients;
            List<Visit> visits = _visitService.GetAllVisits().ToList();
            ViewBag.Visits = visits;
            List<Referral> referrals = _referralService.GetAllReferrals().ToList();
            ViewBag.Referrals = referrals;

            List<Role> roles = _roleService.GetAllRoles().ToList();
            var userId = _userManager.GetUserId(HttpContext.User);

            MojTerminUser user = _userManager.FindByIdAsync(userId).Result;
            if (user != null)
            {
                string role = user.Role.Name;
                ViewBag.Message1 = role;
            }
            return View(this._doctorService.GetAllDoctors());
        }

        // GET: Doctors/Create
        public IActionResult Create()
        {
            List<Specialty> specialties = _specialtyService.GetAll().ToList();
            ViewBag.Message = specialties;
            return View();
        }

       
        // GET: Doctors/Details/5
        public IActionResult Details(Guid? id)
        {
            List<Specialty> specialties = _specialtyService.GetAll().ToList();
            ViewBag.Message = specialties;
            if (id == null)
            {
                return NotFound();
            }

            var doctor = this._doctorService.Get((Guid) id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // POST: Doctors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Ssn,Name,Surname,LicenceNumber,Specialty")] Doctor doctor, IFormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                doctor.Id = Guid.NewGuid();
                Guid specialtyId = Guid.Parse(formCollection["Specialty"]);
                Specialty specialty = _specialtyService.Get(specialtyId);
                doctor.Specialty = specialty;
                this._doctorService.CreateNewDoctor(doctor);
                return RedirectToAction(nameof(Index));
            }
            return View(doctor);
        }

        // GET: Doctors/Edit/5
        public IActionResult Edit(Guid? id)
        {
            List<Specialty> specialties = _specialtyService.GetAll().ToList();
            ViewBag.Message = specialties;
            if (id == null)
            {
                return NotFound();
            }

            var doctor = this._doctorService.Get((Guid)id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid? id, [Bind("Id,Ssn,Name,Surname,LicenceNumber,Specialty")] Doctor doctor, IFormCollection formCollection)
        {
            List<Specialty> specialties = _specialtyService.GetAll().ToList();
            ViewBag.Message = specialties;
            if (id != doctor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Guid specialtyId = Guid.Parse(formCollection["Specialty"]);
                    Specialty specialty = _specialtyService.Get(specialtyId);
                    doctor.Specialty = specialty;
                    this._doctorService.UpdateDoctor(doctor);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(doctor.Id))
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
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = this._doctorService.Get((Guid)id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            this._doctorService.DeleteDoctor(id);
            return RedirectToAction(nameof(Index));
        }


        private bool DoctorExists(Guid id)
        {
            return this._doctorService.Get(id) != null;
        }


    }
}
