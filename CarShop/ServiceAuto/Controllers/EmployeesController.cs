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

namespace ServiceAuto.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmployeesController : Controller
    {
        private readonly EmployeeService employeeService;
        private readonly ServiceService serviceService;
        private readonly ExpenseReportService expenseReportService;

        public EmployeesController(EmployeeService empService, ServiceService serService, ExpenseReportService expReportService)
        {
            employeeService = empService;
            serviceService = serService;
            expenseReportService = expReportService;
        }

        // GET: Employees
        public ActionResult Index()
        {
            var employees = employeeService.GetEmployees();
            return View(employees);
        }

        // GET: Employees/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = employeeService.GetEmployee(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewData["ServiceName"] = new SelectList(serviceService.GetServices(), "ServiceName", "ServiceName");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,EmployeeName,EmployeeAge,EmployeeSalary")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employeeService.AddEmployee(employee);
                var service = serviceService.GetServiceByName("Central");
                var expenseReport = expenseReportService.GetExpenseReportByServiceId(service.Id);
                if(service != null && expenseReport != null && employee.EmployeeSalary != null)
                {
                    expenseReport.ExpenseReportExpense += employee.EmployeeSalary;
                    expenseReportService.UpdateExpenseReport(expenseReport);
                    expenseReportService.UpdateExpenseReportProfit(expenseReport);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServiceName"] = new SelectList(serviceService.GetServices(), "ServiceName", "ServiceName", employee.EmployeeServiceName);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = employeeService.GetEmployee(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["ServiceName"] = new SelectList(serviceService.GetServices(), "ServiceName", "ServiceName", employee.EmployeeServiceName);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id,EmployeeName,EmployeeAge,EmployeeSalary")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    employeeService.UpdateEmployee(employee);
                    var service = serviceService.GetServiceByName("Central");
                    var expenseReport = expenseReportService.GetExpenseReportByServiceId(service.Id);
                    if (service != null && expenseReport != null && employee.EmployeeSalary != null)
                    {
                        expenseReport.ExpenseReportExpense += employee.EmployeeSalary;
                        expenseReportService.UpdateExpenseReport(expenseReport);
                        expenseReportService.UpdateExpenseReportProfit(expenseReport);
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch 
                {
                    return View(employee);
                }             
            }
            ViewData["ServiceName"] = new SelectList(serviceService.GetServices(), "ServiceName", "ServiceName", employee.EmployeeServiceName);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = employeeService.GetEmployee(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (employeeService.GetEmployees() == null)
            {
                return Problem("Entity set 'ServiceingContext.Employees'  is null.");
            }
            var employee = employeeService.GetEmployee(id);
            if (employee != null)
            {
                employeeService.DeleteEmployee(id);
                var service = serviceService.GetServiceByName("Central");
                var expenseReport = expenseReportService.GetExpenseReportByServiceId(service.Id);
                if (service != null && expenseReport != null && employee.EmployeeSalary != null)
                {
                    expenseReport.ExpenseReportExpense -= employee.EmployeeSalary;
                    expenseReportService.UpdateExpenseReport(expenseReport);
                    expenseReportService.UpdateExpenseReportProfit(expenseReport);
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
