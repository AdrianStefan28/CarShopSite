using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceAuto.Models;
using ServiceAuto.Services;

namespace ServiceAuto.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class ProgramareServicesController : Controller
    {
        private readonly ILogger<ProgramareServicesController> _logger;
        private readonly ProgramareServiceService programareServiceService;
        private readonly ServiceService serviceService;
        private readonly ExpenseReportService expenseReportService;

        public ProgramareServicesController(ProgramareServiceService progServiceService, ILogger<ProgramareServicesController> logger, ServiceService serService, ExpenseReportService expReportService)
        {
            _logger = logger;
            programareServiceService = progServiceService;
            serviceService = serService;
            expenseReportService = expReportService;
        }

        // GET: ProgramareServices
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult List()
        {
            var programari = programareServiceService.GetProgramareServices();
            return View(programari);
        }

        // GET: ProgramareServices/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programareService = programareServiceService.GetProgramareService(id);
            if (programareService == null)
            {
                return NotFound();
            }

            return View(programareService);
        }

        // GET: ProgramareServices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProgramareServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Oras,Marca,Model,Defectiune,DataProgramare")] ProgramareService programareService)
        {
            if (ModelState.IsValid)
            {
                programareServiceService.AddProgramareService(programareService);
                var service = serviceService.GetServiceByCity(programareService.Oras);
                var expenseReport = expenseReportService.GetExpenseReportByServiceId(service.Id);
                if (service != null && expenseReport != null && programareService.Oras != null)
                {
                    expenseReport.ExpenseReportIncome += 250;
                    expenseReportService.UpdateExpenseReport(expenseReport);
                    expenseReportService.UpdateExpenseReportProfit(expenseReport);
                }
                return RedirectToAction(nameof(Confirm));
            }
            return View(programareService);
        }

        public ActionResult Confirm()
        {
            return View();
        }

        // GET: ProgramareServices/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programareService = programareServiceService.GetProgramareService(id);
            if (programareService == null)
            {
                return NotFound();
            }
            return View(programareService);
        }

        // POST: ProgramareServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Oras,Marca,Model,Defectiune,DataProgramare,Id")] ProgramareService programareService)
        {
            if (id != programareService.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    programareServiceService.UpdateProgramareService(programareService);
                    return RedirectToAction(nameof(List));
                }
                catch 
                {
                    return View(programareService);
                }
            }
            return View(programareService);
        }

        // GET: ProgramareServices/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programareService = programareServiceService.GetProgramareService(id);
            if (programareService == null)
            {
                return NotFound();
            }

            return View(programareService);
        }

        // POST: ProgramareServices/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (programareServiceService.GetProgramareServices() == null)
            {
                return Problem("Entity set 'ServiceingContext.ProgramareServices'  is null.");
            }
            var programareService = programareServiceService.GetProgramareService(id);
            if (programareService != null)
            {
                programareServiceService.DeleteProgramareService(id);
                var service = serviceService.GetServiceByCity(programareService.Oras);
                var expenseReport = expenseReportService.GetExpenseReportByServiceId(service.Id);
                if (service != null && expenseReport != null && programareService.Oras != null)
                {
                    expenseReport.ExpenseReportIncome -= 250;
                    expenseReportService.UpdateExpenseReport(expenseReport);
                    expenseReportService.UpdateExpenseReportProfit(expenseReport);
                }
            }
            return RedirectToAction(nameof(List));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
