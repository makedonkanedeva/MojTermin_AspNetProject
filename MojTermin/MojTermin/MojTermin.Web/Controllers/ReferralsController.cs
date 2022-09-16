using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class ReferralsController : Controller
    {
        private readonly IReferralService _referralService;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly UserManager<MojTerminUser> _userManager;
        private readonly IRoleService _roleService;

        public ReferralsController(IReferralService referralService, IDoctorService doctorService, IPatientService patientService, UserManager<MojTerminUser> userManager, IRoleService roleService)
        {
            _referralService = referralService;
            _doctorService = doctorService;
            _patientService = patientService;
            _userManager = userManager;
            _roleService = roleService;
        }

        // GET: Referrals
        public IActionResult Index()
        {
            List<Doctor> doctors = _doctorService.GetAllDoctors().ToList();
            ViewBag.Doctors = doctors;
            List<Patient> patients = _patientService.GetAllPatients().ToList();
            ViewBag.Patients = patients;
            List<Role> roles = _roleService.GetAllRoles().ToList();
            var userId = _userManager.GetUserId(HttpContext.User);

            MojTerminUser user = _userManager.FindByIdAsync(userId).Result;
            if (user != null)
            {
                string role = user.Role.Name;
                ViewBag.Message1 = role;
            }
            return View(this._referralService.GetAllReferrals());
        }

        // GET: Referrals/Details/5
        public IActionResult Details(Guid? id)
        {
            List<Doctor> doctors = _doctorService.GetAllDoctors().ToList();
            ViewBag.Doctors = doctors;
            List<Patient> patients = _patientService.GetAllPatients().ToList();
            ViewBag.Patients = patients;
            if (id == null)
            {
                return NotFound();
            }

            var referral = this._referralService.Get((Guid)id);
            if (referral == null)
            {
                return NotFound();
            }

            return View(referral);
        }

        // GET: Referrals/Create
        public IActionResult Create()
        {
            List<Doctor> doctors = _doctorService.GetAllDoctors().ToList();
            ViewBag.Doctors = doctors;
            List<Patient> patients = _patientService.GetAllPatients().ToList();
            ViewBag.Patients = patients;
            return View();
        }

        // POST: Referrals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Term,Id,Patient,ForwardTo")] Referral referral, IFormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                referral.Id = Guid.NewGuid();
                
                Guid patientId = Guid.Parse(formCollection["Patient"]);
                Patient patient = _patientService.Get(patientId);
                referral.Patient = patient;

                Guid doctorId = Guid.Parse(formCollection["ForwardTo"]);
                Doctor doctor = _doctorService.Get(doctorId);
                referral.ForwardTo = doctor;

                this._referralService.CreateNewReferral(referral);
                return RedirectToAction(nameof(Index));
            }
            return View(referral);
        }

        // GET: Referrals/Edit/5
        public IActionResult Edit(Guid? id)
        {
            List<Doctor> doctors = _doctorService.GetAllDoctors().ToList();
            ViewBag.Doctors = doctors;
            List<Patient> patients = _patientService.GetAllPatients().ToList();
            ViewBag.Patients = patients;
            if (id == null)
            {
                return NotFound();
            }

            var referral = this._referralService.Get((Guid)id);
            if (referral == null)
            {
                return NotFound();
            }
            return View(referral);
        }

        // POST: Referrals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Term,Id,Patient,ForwardTo")] Referral referral, IFormCollection formCollection)
        {
            List<Doctor> doctors = _doctorService.GetAllDoctors().ToList();
            ViewBag.Doctors = doctors;
            List<Patient> patients = _patientService.GetAllPatients().ToList();
            ViewBag.Patients = patients;
            if (id != referral.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Guid patientId = Guid.Parse(formCollection["Patient"]);
                    Patient patient = _patientService.Get(patientId);
                    referral.Patient = patient;

                    Guid doctorId = Guid.Parse(formCollection["ForwardTo"]);
                    Doctor doctor = _doctorService.Get(doctorId);
                    referral.ForwardTo = doctor;
                    this._referralService.UpdateReferral(referral);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReferralExists(referral.Id))
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
            return View(referral);
        }

        // GET: Referrals/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var referral = this._referralService.Get((Guid)id);
            if (referral == null)
            {
                return NotFound();
            }
            return View(referral);
        }

        // POST: Referrals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            this._referralService.DeleteReferral(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ReferralExists(Guid id)
        {
            return this._referralService.Get(id) != null;
        }
    }
}
