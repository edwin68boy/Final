using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Final.Models;

namespace Final.Controllers
{
    public class VacacionesController : Controller
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: Vacaciones
        public ActionResult Index()
        {
            var vacaciones = db.Vacaciones.Include(v => v.Empleado);
            return View(vacaciones.ToList());
        }

        // GET: Vacaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vacacione vacacione = db.Vacaciones.Find(id);
            if (vacacione == null)
            {
                return HttpNotFound();
            }
            return View(vacacione);
        }

        // GET: Vacaciones/Create
        public ActionResult Create()
        {
            ViewBag.Empleado_ID = new SelectList(db.Empleados, "Id", "Nombre");
            return View();
        }

        // POST: Vacaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Empleado_ID,Fecha_Inicio,Fecha_Fin,Comentario")] Vacacione vacacione)
        {
            if (ModelState.IsValid)
            {
                db.Vacaciones.Add(vacacione);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Empleado_ID = new SelectList(db.Empleados, "Id", "Nombre", vacacione.Empleado_ID);
            return View(vacacione);
        }

        // GET: Vacaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vacacione vacacione = db.Vacaciones.Find(id);
            if (vacacione == null)
            {
                return HttpNotFound();
            }
            ViewBag.Empleado_ID = new SelectList(db.Empleados, "Id", "Nombre", vacacione.Empleado_ID);
            return View(vacacione);
        }

        // POST: Vacaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Empleado_ID,Fecha_Inicio,Fecha_Fin,Comentario")] Vacacione vacacione)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vacacione).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Empleado_ID = new SelectList(db.Empleados, "Id", "Nombre", vacacione.Empleado_ID);
            return View(vacacione);
        }

        // GET: Vacaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vacacione vacacione = db.Vacaciones.Find(id);
            if (vacacione == null)
            {
                return HttpNotFound();
            }
            return View(vacacione);
        }

        // POST: Vacaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vacacione vacacione = db.Vacaciones.Find(id);
            db.Vacaciones.Remove(vacacione);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
