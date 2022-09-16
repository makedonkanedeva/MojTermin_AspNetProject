using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MojTermin.Domain.DomainModels;
using MojTermin.Domain.Identity;
using MojTermin.Service.Interface;

namespace MojTermin.Web.Controllers
{
    public class PatientsController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;
        private readonly UserManager<MojTerminUser> _userManager;
        private readonly IRoleService _roleService;
        private readonly IVisitService _visitService;
        private readonly IReferralService _referralService;

        public PatientsController(IPatientService patientService, IDoctorService doctorService, UserManager<MojTerminUser> userManager, IRoleService roleService, IVisitService visitService, IReferralService referralService)
        {
            _patientService = patientService;
            _doctorService = doctorService;
            _userManager = userManager;
            _roleService = roleService;
            _visitService = visitService;
            _referralService = referralService;
        }

        // GET: Patients
        public IActionResult Index()
        {
            List<Doctor> doctors = _doctorService.GetAllDoctors().ToList();
            ViewBag.Message = doctors;
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
            return View(this._patientService.GetAllPatients());
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            List<Doctor> doctors = _doctorService.GetAllDoctors().ToList();
            ViewBag.Message = doctors;
            return View();
        }

        // GET: Patients/Details/5
        public IActionResult Details(Guid? id)
        {
            List<Doctor> doctors = _doctorService.GetAllDoctors().ToList();
            ViewBag.Message = doctors;
            if (id == null)
            {
                return NotFound();
            }

            var patient = this._patientService.Get((Guid)id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Ssn,Name,Surname,Uhid,Doctor")] Patient patient, IFormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                patient.Id = Guid.NewGuid();
                Guid doctorId = Guid.Parse(formCollection["Doctor"]);
                Doctor doctor = _doctorService.Get(doctorId);
                patient.Doctor = doctor;
                this._patientService.CreateNewPatient(patient);
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }


        // GET: Patients/Edit/5
        public IActionResult Edit(Guid? id)
        {
            List<Doctor> doctors = _doctorService.GetAllDoctors().ToList();
            ViewBag.Message = doctors;
            if (id == null)
            {
                return NotFound();
            }

            var patient = this._patientService.Get((Guid)id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,Ssn,Name,Surname,Uhid,Doctor")] Patient patient, IFormCollection formCollection)
        {
            List<Doctor> doctors = _doctorService.GetAllDoctors().ToList();
            ViewBag.Message = doctors;
            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Guid doctorId = Guid.Parse(formCollection["Doctor"]);
                    Doctor doctor = _doctorService.Get(doctorId);
                    patient.Doctor = doctor;
                    this._patientService.UpdatePatient(patient);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.Id))
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
            return View(patient);
        }

        // GET: Patients/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = this._patientService.Get((Guid)id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            this._patientService.DeletePatient(id);
            return RedirectToAction(nameof(Index));
        }


        private bool PatientExists(Guid id)
        {
            return this._patientService.Get(id) != null;
        }
    }
}
