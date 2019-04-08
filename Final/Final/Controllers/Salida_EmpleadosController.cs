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
    public class Salida_EmpleadosController : Controller
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: Salida_Empleados
        public ActionResult Index()
        {
            var salida_Empleados = db.Salida_Empleados.Include(s => s.Empleado).Include(s => s.Tipo_Salida);
            return View(salida_Empleados.ToList());
        }

        // GET: Salida_Empleados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salida_Empleados salida_Empleados = db.Salida_Empleados.Find(id);
            if (salida_Empleados == null)
            {
                return HttpNotFound();
            }
            return View(salida_Empleados);
        }

        // GET: Salida_Empleados/Create
        public ActionResult Create()
        {
            ViewBag.Empleado_ID = new SelectList(db.Empleados, "Id", "Nombre");
            ViewBag.Tipo_Salida_ID = new SelectList(db.Tipo_Salida, "Id", "Tipo_Salida1");
            return View();
        }

        // POST: Salida_Empleados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Empleado_ID,Tipo_Salida_ID,Motivo,Fecha_Salida")] Salida_Empleados salida_Empleados)
        {
            if (ModelState.IsValid)
            {
                db.Salida_Empleados.Add(salida_Empleados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Empleado_ID = new SelectList(db.Empleados, "Id", "Nombre", salida_Empleados.Empleado_ID);
            ViewBag.Tipo_Salida_ID = new SelectList(db.Tipo_Salida, "Id", "Tipo_Salida1", salida_Empleados.Tipo_Salida_ID);
            return View(salida_Empleados);
        }

        // GET: Salida_Empleados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salida_Empleados salida_Empleados = db.Salida_Empleados.Find(id);
            if (salida_Empleados == null)
            {
                return HttpNotFound();
            }
            ViewBag.Empleado_ID = new SelectList(db.Empleados, "Id", "Nombre", salida_Empleados.Empleado_ID);
            ViewBag.Tipo_Salida_ID = new SelectList(db.Tipo_Salida, "Id", "Tipo_Salida1", salida_Empleados.Tipo_Salida_ID);
            return View(salida_Empleados);
        }

        // POST: Salida_Empleados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Empleado_ID,Tipo_Salida_ID,Motivo,Fecha_Salida")] Salida_Empleados salida_Empleados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salida_Empleados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Empleado_ID = new SelectList(db.Empleados, "Id", "Nombre", salida_Empleados.Empleado_ID);
            ViewBag.Tipo_Salida_ID = new SelectList(db.Tipo_Salida, "Id", "Tipo_Salida1", salida_Empleados.Tipo_Salida_ID);
            return View(salida_Empleados);
        }

        // GET: Salida_Empleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salida_Empleados salida_Empleados = db.Salida_Empleados.Find(id);
            if (salida_Empleados == null)
            {
                return HttpNotFound();
            }
            return View(salida_Empleados);
        }

        // POST: Salida_Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Salida_Empleados salida_Empleados = db.Salida_Empleados.Find(id);
            db.Salida_Empleados.Remove(salida_Empleados);
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
