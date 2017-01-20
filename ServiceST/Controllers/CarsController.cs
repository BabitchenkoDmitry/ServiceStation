using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ServiceST.DAL.Entities;
using ServiceST.DAL.Repositories;

namespace ServiceST.Controllers
{
    public class CarsController : Controller
    {
        private EFUnitOfWork unit = new EFUnitOfWork("name = SSModel");

        // GET: Cars
        public ActionResult Index()
        {
            var car = unit.Cars.GetAll();
            return View(car.OrderBy(x => x.make).ThenBy(x => x.model));
        }

        // GET: Cars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = unit.Cars.Get((int)id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            int selectedIndex = 1;
            SelectList l_name = new SelectList(unit.Clients.GetAll().Select(x => x.last_name).Distinct(), null);
            ViewBag.client_id_l_name = l_name;
            SelectList f_name = new SelectList(unit.Clients.Find(c => c.id == selectedIndex), "id", "first_name");
            ViewBag.client_id_f_name = f_name;
            return View();
        }

        public ActionResult GetFName(string id)
        {
            return PartialView(unit.Clients.Find(c => c.last_name == id));
        }

        // POST: Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,make,model,year,vin")] Car car)
        {
            if (unit.Cars.GetAll().FirstOrDefault(x => x.vin == car.vin)!= null)
            {
                Response.Write("<br/><b><pre>            Car with this vin already exists!</pre></b>");
                return View("Error");
            }
            if (ModelState.IsValid)
            {
                car.client_id = int.Parse(Request.Params["fname"]);
                unit.Cars.Create(car);
                unit.Save();
                return RedirectToAction("Index");
            }

            ViewBag.client_id = new SelectList(unit.Clients.GetAll(), "id", "first_name", car.client_id);
            return View(car);
        }

        // GET: Cars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = unit.Cars.Get((int)id);
            if (car == null)
            {
                return HttpNotFound();
            }
            SelectList l_name = new SelectList(unit.Clients.GetAll().Select(x => x.last_name).Distinct(), car.Client.last_name);
            ViewBag.client_id_l_name = l_name;
            SelectList f_name = new SelectList(unit.Clients.Find(c => c.id == car.client_id), "id", "first_name");
            ViewBag.client_id_f_name = f_name;
            return View(car);
        }

        // POST: Cars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,make,model,year,vin")] Car car)
        {
            if (ModelState.IsValid)
            {
                car.client_id = int.Parse(Request.Params["fname"]);
                unit.Cars.Update(car);
                unit.Save();
                return RedirectToAction("Index");
            }
            ViewBag.client_id = new SelectList(unit.Clients.GetAll(), "id", "first_name", car.client_id);

            return View(car);
        }

        // GET: Cars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = unit.Cars.Get((int)id);
            if (car == null)
            {
                return HttpNotFound();
            }
            if (unit.Orders.GetAll().FirstOrDefault(p => p.car_id == car.id) != null)
            {
                Response.Write("<br/><b><pre>       There are some orders on this car! First you should delete orders!</pre></b>");
                return View("ErrorDelete");
            }
            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            unit.Cars.Delete(id);
            unit.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            unit.Dispose();
        }
    }
}