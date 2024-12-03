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
    public class ServicesController : Controller
    {
        private readonly ServiceService serviceService;

        public ServicesController(ServiceService serService)
        {
            serviceService = serService;
           
        }

        // GET: Services
        public ActionResult Index()
        {
             var services = serviceService.GetServices();
             return View(services);
        }

        // GET: Services/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = serviceService.GetService(id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Services/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ServiceName,ServiceDescription,ServiceAddress1,ServiceCity,ServiceState,Id")] Service service)
        {
            if (ModelState.IsValid)
            {
                serviceService.AddService(service);
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        // GET: Services/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = serviceService.GetService(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("ServiceName,ServiceDescription,ServiceAddress1,ServiceCity,ServiceState,Id")] Service service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    serviceService.UpdateService(service);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                   return View(service);
                }            
            }
            return View(service);
        }

        // GET: Services/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = serviceService.GetService(id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (serviceService.GetServices() == null)
            {
                return Problem("Entity set 'ServiceingContext.Services'  is null.");
            }
            var service = serviceService.GetService(id);
            if (service != null)
            {
                serviceService.DeleteService(id);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
