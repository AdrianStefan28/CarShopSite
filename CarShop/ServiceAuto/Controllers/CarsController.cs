using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceAuto.Models;
using ServiceAuto.Services;
using ServiceAuto.Services.Interfaces;

namespace ServiceAuto.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarService carService;
        private readonly ServiceService serviceService;
        private readonly ExpenseReportService expenseReportService;

        public CarsController(CarService cService, ServiceService serService, ExpenseReportService expReportService)
        {
            carService = cService;
            serviceService = serService;
            expenseReportService = expReportService;
        }

        // GET: Cars
        public ActionResult Index()
        {
            var cars = carService.GetCars();
            return View(cars);
        }

        // GET: Cars/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = carService.GetCar(id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,CarBrand,CarModel,CarProductYear,CarPrice,CarPhoto")] Car car)
        {
            if (ModelState.IsValid)
            {
                carService.AddCar(car);
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Cars/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = carService.GetCar(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id,CarBrand,CarModel,CarProductYear,CarPrice,CarPhoto")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    carService.UpdateCar(car);
                    return RedirectToAction(nameof(Index));
                }
                catch 
                {
                    return View(car);
                }
            }
            return View(car);
        }

        // GET: Cars/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var car = carService.GetCar(id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (carService.GetCars() == null)
            {
                return Problem("Entity set 'ServiceingContext.Cars'  is null.");
            }
            var car = carService.GetCar(id);
            if (car != null)
            {
                carService.DeleteCar(id);
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

            var car = carService.GetCar(id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPost, ActionName("Buy")]
        [ValidateAntiForgeryToken]
        public ActionResult BuyConfirmed(int id)
        {
            if (carService.GetCars() == null)
            {
                return Problem("Entity set 'ServiceingContext.Cars'  is null.");
            }
            var car = carService.GetCar(id);
            if (car != null)
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

                expenseReport.ExpenseReportIncome += car.CarPrice;
                expenseReportService.UpdateExpenseReport(expenseReport);
                expenseReportService.UpdateExpenseReportProfit(expenseReport);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
