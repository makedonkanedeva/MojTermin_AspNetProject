using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    public class VisitsController : Controller
    {
        private readonly IVisitService _visitService;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly UserManager<MojTerminUser> _userManager;
        private readonly IRoleService _roleService;
        private readonly IDiagnosisService _diagnosisService;

        public VisitsController(IVisitService visitService, IDoctorService doctorService, IPatientService patientService, UserManager<MojTerminUser> userManager, IRoleService roleService, IDiagnosisService diagnosisService)
        {
            _visitService = visitService;
            _doctorService = doctorService;
            _patientService = patientService;
            _userManager = userManager;
            _roleService = roleService;
            _diagnosisService = diagnosisService;
        }

        // GET: Visits
        public IActionResult Index()
        {
            List<Doctor> doctors = _doctorService.GetAllDoctors().ToList();
            ViewBag.Doctors = doctors;
            List<Patient> patients = _patientService.GetAllPatients().ToList();
            ViewBag.Patients = patients;
            List<Diagnosis> diagnoses = _diagnosisService.GetAllDiagnoses().ToList();
            ViewBag.Diagnoses = diagnoses;

            List<Role> roles = _roleService.GetAllRoles().ToList();
            var userId = _userManager.GetUserId(HttpContext.User);

            MojTerminUser user = _userManager.FindByIdAsync(userId).Result;
            if (user != null)
            {
                string role = user.Role.Name;
                ViewBag.Message1 = role;
            }
            return View(this._visitService.GetAllVisits());
        }

        // GET: Visits/Details/5
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

            var visit = this._visitService.Get((Guid)id);
            if (visit == null)
            {
                return NotFound();
            }

            return View(visit);
        }

        // GET: Visits/Create
        public IActionResult Create()
        {
            List<Doctor> doctors = _doctorService.GetAllDoctors().ToList();
            ViewBag.Doctors = doctors;
            List<Patient> patients = _patientService.GetAllPatients().ToList();
            ViewBag.Patients = patients;
            return View();
        }

        // POST: Visits/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Term,Id,Patient,Doctor")] Visit visit, IFormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                visit.Id = Guid.NewGuid();

                Guid patientId = Guid.Parse(formCollection["Patient"]);
                Patient patient = _patientService.Get(patientId);
                visit.Patient = patient;

                Guid doctorId = Guid.Parse(formCollection["Doctor"]);
                Doctor doctor = _doctorService.Get(doctorId);
                visit.Doctor = doctor;

                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                this._visitService.CreateNewVisit(visit, userId);
                return RedirectToAction(nameof(Index));
            }
            return View(visit);
        }

        // GET: Visits/Edit/5
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

            var visit = this._visitService.Get((Guid)id);
            if (visit == null)
            {
                return NotFound();
            }
            return View(visit);
        }

        // POST: Visits/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Term,Id,Patient,Doctor")] Visit visit, IFormCollection formCollection)
        {
            List<Doctor> doctors = _doctorService.GetAllDoctors().ToList();
            ViewBag.Doctors = doctors;
            List<Patient> patients = _patientService.GetAllPatients().ToList();
            ViewBag.Patients = patients;
            if (id != visit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Guid patientId = Guid.Parse(formCollection["Patient"]);
                    Patient patient = _patientService.Get(patientId);
                    visit.Patient = patient;

                    Guid doctorId = Guid.Parse(formCollection["Doctor"]);
                    Doctor doctor = _doctorService.Get(doctorId);
                    visit.Doctor = doctor;

                    this._visitService.UpdateVisit(visit);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitExists(visit.Id))
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
            return View(visit);
        }

        // GET: Visits/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visit = this._visitService.Get((Guid)id);
            if (visit == null)
            {
                return NotFound();
            }

            return View(visit);
        }

        // POST: Visits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            this._visitService.DeleteVisit(id);
            return RedirectToAction(nameof(Index));
        }

        private bool VisitExists(Guid id)
        {
            return this._visitService.Get(id) != null; 
        }
    }
}
