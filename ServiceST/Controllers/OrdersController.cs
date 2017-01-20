using System;
using System.Globalization;
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
    public class OrdersController : Controller
    {
        private EFUnitOfWork unit = new EFUnitOfWork("name = SSModel");

        // GET: Orders
        public ActionResult Index()
        {
            var order = unit.Orders.GetAll();
            return View(order.OrderBy(x => x.date).ThenBy(x => x.Car.make));
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = unit.Orders.Get((int)id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            int selectedIndex = 1;
            SelectList l_name = new SelectList(unit.Clients.GetAll().Select(x => x.last_name).Distinct(), null);
            ViewBag.client_id_l_name = l_name;
            SelectList f_name = new SelectList(unit.Clients.Find(c => c.id == selectedIndex), "id", "first_name");
            ViewBag.client_id_f_name = f_name;

            SelectList status = new SelectList(unit.OrderStatuses.GetAll(), "id", "name");
            ViewBag.status = status;
            return View();
        }

        public ActionResult GetFName(string id)
        {
            return PartialView(unit.Clients.Find(c => c.last_name == id));
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,date,amount")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.client_id = int.Parse(Request.Params["fname"]);
                order.orderstatus_id = int.Parse(Request.Params["status"]);
                return RedirectToAction("Next", order);
            }
            ViewBag.car_id = new SelectList(unit.Cars.GetAll(), "id", "make", order.car_id);
            ViewBag.client_id = new SelectList(unit.Clients.GetAll(), "id", "first_name", order.client_id);
            return View(order);
        }

        // GET: Orders/Next
        public ActionResult Next(Order order)
        {
            var cars = unit.Cars.Find(c => c.client_id == order.client_id).ToList();
            if (cars.Count == 0)
            {
                Response.Write("<br/><b><pre>              Sorry!Client has no cars!</pre></b>");
                return View("Error");
            }
            ViewBag.order = order;
            return View(cars);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Next()
        {
            try
            {
                CultureInfo culture = new CultureInfo("en-US");   
                DateTime _date = Convert.ToDateTime(Request.Params["date"], culture);
                int    _status = int.Parse(Request.Params["orderstatus_id"]);
                int    _client_id = int.Parse(Request.Params["client_id"]);
                int    _car_id = int.Parse(Request.Params["car"]);
                int    _amount = int.Parse(Request.Params["amount"]);
                Order order = new Order() { date = _date, orderstatus_id = _status, client_id = _client_id, car_id = _car_id, amount = _amount };
                unit.Orders.Create(order);
                unit.Save();
            }
            catch (Exception e)
            {}
            return RedirectToAction("Index");
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = unit.Orders.Get((int)id);
            if (order == null)
            {
                return HttpNotFound();
            }
            SelectList st = new SelectList(unit.OrderStatuses.GetAll(), "id", "name", order.orderstatus_id);
            ViewBag.st = st;
            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,car_id,client_id,date,amount")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.orderstatus_id = int.Parse(Request.Params["Status"]);
                unit.Orders.Update(order);
                unit.Save();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = unit.Orders.Get((int)id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            unit.Orders.Delete(id);
            unit.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            unit.Dispose();
        }
    }
}