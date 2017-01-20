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
    public class ClientsController : Controller
    {
        private EFUnitOfWork unit = new EFUnitOfWork("name = SSModel");

        // GET: Clients
        public ActionResult Index(string last, string first)
        {
            IEnumerable<Client> client = unit.Clients.GetAll();
            if (!String.IsNullOrEmpty(last))
            {
                client = unit.Clients.Find(x => x.last_name == last);
            }

            if (!String.IsNullOrEmpty(first))
            {
                client = unit.Clients.Find(x => x.first_name == first);
            }

            return View(client.OrderBy(x => x.last_name));
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = unit.Clients.Get((int)id);
            if (client == null)
            {
                return HttpNotFound();
            }
            IEnumerable<Car> cars = unit.Cars.Find(x => x.client_id == client.id);
            ViewBag.Cars = cars;
            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,first_name,last_name,birthday,address,phone,email")] Client client)
        {
            if (unit.Clients.GetAll().FirstOrDefault(x => x.last_name == client.last_name && x.first_name == client.first_name) != null)
            {
                Response.Write("<br/><b><pre>            Client has already registed!</pre></b>");
                return View("Error");
            }
            if (ModelState.IsValid)
            {
                unit.Clients.Create(client);
                unit.Save();
                return RedirectToAction("Index");
            }

            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = unit.Clients.Get((int)id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,first_name,last_name,birthday,address,phone,email")] Client client)
        {
            if (ModelState.IsValid)
            {
                unit.Clients.Update(client);
                unit.Save();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        protected override void Dispose(bool disposing)
        {
            unit.Dispose();
        }
    }
}
