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
    [Authorize(Roles = "Admin")]
    public class EmployeeAddressesController : Controller
    {
        private readonly EmployeeAddressService employeeAddressService;
        private readonly EmployeeService employeeService;

        public EmployeeAddressesController(EmployeeAddressService empAService, EmployeeService empService)
        {
            employeeAddressService = empAService;
            employeeService = empService;
        }

        // GET: EmployeeAddresses
        public ActionResult Index()
        {
            var employeeAddresses = employeeAddressService.GetEmployeeAddresses();
            return View(employeeAddresses);
        }

        // GET: EmployeeAddresses/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeAddress = employeeAddressService.GetEmployeeAddress(id);
            if (employeeAddress == null)
            {
                return NotFound();
            }

            return View(employeeAddress);
        }

        // GET: EmployeeAddresses/Create
        public ActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(employeeService.GetEmployees(), "Id", "Id");
            return View();
        }

        // POST: EmployeeAddresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("EmployeeAddress1,EmployeeCity,EmployeeState,EmployeeId,Id")] EmployeeAddress employeeAddress)
        {
            if (ModelState.IsValid)
            {
                employeeAddressService.AddEmployeeAddress(employeeAddress);
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(employeeService.GetEmployees(), "Id", "Id", employeeAddress.EmployeeId);
            return View(employeeAddress);
        }

        // GET: EmployeeAddresses/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeAddress = employeeAddressService.GetEmployeeAddress(id);
            if (employeeAddress == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(employeeService.GetEmployees(), "Id", "Id", employeeAddress.EmployeeId);
            return View(employeeAddress);
        }

        // POST: EmployeeAddresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("EmployeeAddress1,EmployeeCity,EmployeeState,EmployeeId,Id")] EmployeeAddress employeeAddress)
        {
            if (id != employeeAddress.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    employeeAddressService.UpdateEmployeeAddress(employeeAddress);
                    return RedirectToAction(nameof(Index));
                }
                catch 
                {
                    return View(employeeAddress);
                }             
            }
            ViewData["EmployeeId"] = new SelectList(employeeService.GetEmployees(), "Id", "Id", employeeAddress.EmployeeId);
            return View(employeeAddress);
        }

        // GET: EmployeeAddresses/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeAddress = employeeAddressService.GetEmployeeAddress(id);
            if (employeeAddress == null)
            {
                return NotFound();
            }

            return View(employeeAddress);
        }

        // POST: EmployeeAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (employeeAddressService.GetEmployeeAddresses() == null)
            {
                return Problem("Entity set 'ServiceingContext.EmployeeAddresses'  is null.");
            }
            var employeeAddress = employeeAddressService.GetEmployeeAddress(id);
            if (employeeAddress != null)
            {
                employeeAddressService.DeleteEmployeeAddress(id);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
