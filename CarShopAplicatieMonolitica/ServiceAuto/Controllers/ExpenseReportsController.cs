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
using SQLitePCL;

namespace ServiceAuto.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ExpenseReportsController : Controller
    {
      
        private readonly ExpenseReportService expenseReportService;
        private readonly ServiceService serviceService;

        public ExpenseReportsController(ExpenseReportService expReportService, ServiceService serService)
        {
            expenseReportService = expReportService;
            serviceService = serService;
        }

        // GET: ExpenseReports
        public ActionResult Index()
        {
            var expenseReports = expenseReportService.GetExpenseReports();    
            return View(expenseReports);
        }

        // GET: ExpenseReports/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseReport = expenseReportService.GetExpenseReport(id);
            if (expenseReport == null)
            {
                return NotFound();
            }

            return View(expenseReport);
        }

        // GET: ExpenseReports/Create
        public ActionResult Create()
        {
            ViewData["ServiceId"] = new SelectList(serviceService.GetServices(), "Id", "Id");
            return View();
        }

        // POST: ExpenseReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ExpenseReportExpense,ExpenseReportIncome,ExpenseReportProfit,ServiceId,Id")] ExpenseReport expenseReport)
        {
            if (ModelState.IsValid)
            {
                expenseReportService.AddExpenseReport(expenseReport);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServiceId"] = new SelectList(serviceService.GetServices(), "Id", "Id", expenseReport.ServiceId);
            return View(expenseReport);
        }

        // GET: ExpenseReports/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseReport = expenseReportService.GetExpenseReport(id);
            if (expenseReport == null)
            {
                return NotFound();
            }
            ViewData["ServiceId"] = new SelectList(serviceService.GetServices(), "Id", "Id", expenseReport.ServiceId);
            return View(expenseReport);
        }

        // POST: ExpenseReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("ExpenseReportExpense,ExpenseReportIncome,ExpenseReportProfit,ServiceId,Id")] ExpenseReport expenseReport)
        {
            if (id != expenseReport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    expenseReportService.UpdateExpenseReport(expenseReport);
                    return RedirectToAction(nameof(Index));
                }
                catch 
                {
                    return View(expenseReport);
                }            
            }
            ViewData["ServiceId"] = new SelectList(serviceService.GetServices(), "Id", "Id", expenseReport.ServiceId);
            return View(expenseReport);
        }

        // GET: ExpenseReports/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseReport = expenseReportService.GetExpenseReport(id);
            if (expenseReport == null)
            {
                return NotFound();
            }

            return View(expenseReport);
        }

        // POST: ExpenseReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (expenseReportService.GetExpenseReports() == null)
            {
                return Problem("Entity set 'ServiceingContext.ExpenseReports'  is null.");
            }
            var expenseReport = expenseReportService.GetExpenseReport(id);
            if (expenseReport != null)
            {
                expenseReportService.DeleteExpenseReport(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
