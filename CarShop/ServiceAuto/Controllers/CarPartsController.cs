using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceAuto.Models;
using ServiceAuto.Services;

namespace ServiceAuto.Controllers
{
    public class CarPartsController : Controller
    {
        private readonly CarPartService carPartService;
        private readonly ServiceService serviceService;
        private readonly ExpenseReportService expenseReportService;

        public CarPartsController(CarPartService cpService, ServiceService serService, ExpenseReportService expReportService)
        {
            carPartService = cpService;
            serviceService = serService;
            expenseReportService = expReportService;
        }

        // GET: CarParts
        public ActionResult Index()
        {
            var carParts = carPartService.GetCarParts();
            return View(carParts);
        }

        // GET: CarParts/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carPart = carPartService.GetCarPart(id);
            if (carPart == null)
            {
                return NotFound();
            }

            return View(carPart);
        }

        // GET: CarParts/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarParts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,CarPartName,CarPartDescription,CarPartPrice,CarPartPhoto")] CarPart carPart)
        {
            if (ModelState.IsValid)
            {
                carPartService.AddCarPart(carPart);
                return RedirectToAction(nameof(Index));
            }
            return View(carPart);
        }

        // GET: CarParts/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carPart = carPartService.GetCarPart(id);
            if (carPart == null)
            {
                return NotFound();
            }
            return View(carPart);
        }

        // POST: CarParts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id,CarPartName,CarPartDescription,CarPartPrice,CarPartPhoto")] CarPart carPart)
        {
            if (id != carPart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    carPartService.UpdateCarPart(carPart);
                    return RedirectToAction(nameof(Index));

                }
                catch
                {
                    return View(carPart);
                }
            }
            return View(carPart);
        }

        // GET: CarParts/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carPart = carPartService.GetCarPart(id);
            if (carPart == null)
            {
                return NotFound();
            }

            return View(carPart);
        }

        // POST: CarParts/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (carPartService.GetCarParts() == null)
            {
                return Problem("Entity set 'ServiceingContext.CarParts'  is null.");
            }
            var carPart = carPartService.GetCarPart(id);
            if (carPart != null)
            {
                carPartService.DeleteCarPart(id);
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin,User")]
        public ActionResult Buy(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carPart = carPartService.GetCarPart(id);
            if (carPart == null)
            {
                return NotFound();
            }

            return View(carPart);
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPost, ActionName("Buy")]
        [ValidateAntiForgeryToken]
        public ActionResult BuyConfirmed(int id)
        {
            if (carPartService.GetCarParts() == null)
            {
                return Problem("Entity set 'ServiceingContext.Cars'  is null.");
            }
            var carPart = carPartService.GetCarPart(id);
            if (carPart != null)
            {
                var service = serviceService.GetServiceByName("Central");
                if (service == null)
                {
                    return NotFound();
                }

                var expenseReport = expenseReportService.GetExpenseReportByServiceId(service.Id);
                if (expenseReport == null)
                {
                    return NotFound();
                }

                expenseReport.ExpenseReportIncome += carPart.CarPartPrice;
                expenseReportService.UpdateExpenseReport(expenseReport);
                expenseReportService.UpdateExpenseReportProfit(expenseReport);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
