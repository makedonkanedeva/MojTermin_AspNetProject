using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MojTermin.Domain.DomainModels;
using MojTermin.Domain.Identity;
using MojTermin.Domain.Relations;
using MojTermin.Service.Interface;

namespace MojTermin.Web.Controllers
{
    public class DiagnosesController : Controller
    {
        private readonly IDiagnosisService _diagnosisService;
        private readonly IVisitService _visitService;
        private readonly UserManager<MojTerminUser> _userManager;
        private readonly IRoleService _roleService;

        public DiagnosesController(IDiagnosisService diagnosisService, IVisitService visitService, UserManager<MojTerminUser> userManager, IRoleService roleService)
        {
            _diagnosisService = diagnosisService;
            _visitService = visitService;
            _userManager = userManager;
            _roleService = roleService;
        }


        // GET: Diagnoses
        public IActionResult Index()
        {
            List<Visit> visits = _visitService.GetAllVisits().ToList();
            ViewBag.Message = visits;
            List<Role> roles = _roleService.GetAllRoles().ToList();
            var userId = _userManager.GetUserId(HttpContext.User);

            MojTerminUser user = _userManager.FindByIdAsync(userId).Result;
            if (user != null)
            {
                string role = user.Role.Name;
                ViewBag.Message1 = role;
            }
            return View(this._diagnosisService.GetAllDiagnoses());
        }

        // GET: Diagnoses/Details/5
        public IActionResult Details(Guid? id)
        {
            List<Visit> visits = _visitService.GetAllVisits().ToList();
            ViewBag.Message = visits;
            if (id == null)
            {
                return NotFound();
            }

            var diagnosis = this._diagnosisService.Get((Guid)id);
            if (diagnosis == null)
            {
                return NotFound();
            }

            return View(diagnosis);
        }

        // GET: Diagnoses/Create
        public IActionResult Create()
        {
            List<Visit> visits = _visitService.GetAllVisits().ToList();
            ViewBag.Message = visits;
            return View();
        }

        // POST: Diagnoses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Description,Id,Visits")] Diagnosis diagnosis, IFormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                List<Visit> visits = _visitService.GetAllVisits().ToList();
                ViewBag.Message = visits;

                diagnosis.Id = Guid.NewGuid();

                Guid visitId = Guid.Parse(formCollection["Visits"]);
                Visit visit = _visitService.Get(visitId);
                DiagnosisInVisit diagnosisInVisit = new DiagnosisInVisit(diagnosis.Id, visitId);
                diagnosisInVisit.Id = Guid.NewGuid();
                diagnosis.Visits.Add(diagnosisInVisit);

                this._diagnosisService.CreateNewDiagnosis(diagnosis);
                return RedirectToAction(nameof(Index));
            }
            return View(diagnosis);
        }

        // GET: Diagnoses/Edit/5
        public IActionResult Edit(Guid? id)
        {
            List<Visit> visits = _visitService.GetAllVisits().ToList();
            ViewBag.Message = visits;
            if (id == null)
            {
                return NotFound();
            }

            var diagnosis = this._diagnosisService.Get((Guid)id);
            if (diagnosis == null)
            {
                return NotFound();
            }
            return View(diagnosis);
        }

        // POST: Diagnoses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Name,Description,Id,Visits")] Diagnosis diagnosis, IFormCollection formCollection)
        {
            List<Visit> visits = _visitService.GetAllVisits().ToList();
            ViewBag.Message = visits;
            if (id != diagnosis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _diagnosisService.DeleteDiagnosis(diagnosis.Id);

                    diagnosis.Id = Guid.NewGuid();

                    Guid visitId = Guid.Parse(formCollection["Visits"]);
                    Visit visit = _visitService.Get(visitId);
                    DiagnosisInVisit diagnosisInVisit = new DiagnosisInVisit(diagnosis.Id, visitId);
                    diagnosisInVisit.Id = Guid.NewGuid();
                    diagnosis.Visits.Add(diagnosisInVisit);

                    this._diagnosisService.CreateNewDiagnosis(diagnosis);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiagnosisExists(diagnosis.Id))
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
            return View(diagnosis);
        }

        // GET: Diagnoses/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnosis = this._diagnosisService.Get((Guid)id);
            if (diagnosis == null)
            {
                return NotFound();
            }

            return View(diagnosis);
        }

        // POST: Diagnoses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            this._diagnosisService.DeleteDiagnosis(id);
            return RedirectToAction(nameof(Index));
        }

        private bool DiagnosisExists(Guid id)
        {
            return this._diagnosisService.Get(id) != null;
        }
    }
}
