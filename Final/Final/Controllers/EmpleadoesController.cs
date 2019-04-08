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
    public class EmpleadoesController : Controller
    {
        private DatabaseEntities db = new DatabaseEntities();

        public ActionResult ActiveClients(string option, string search)
        {
            //return View(db.Empleados.Where(x => x.Nombre.Contains(searching) || searching == null).ToList());
            //return View(db.Empleados.Where(x => x.Nombre.Contains(search)|| search == null ).ToList());
            var empleados = db.Empleados.Where(e => e.Estatus == true);
            if (option == "Nombre")
            {
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                return View(db.Empleados.Where(x => x.Nombre.Contains(search) && x.Estatus == true || search == null && x.Estatus == true).ToList());
            }
           // else if (option == "Departamento")
           // {
                //var query = db.Empleados.Join(db.Departamentos, emp => emp.Departamento,
                //dep => dep.Id,(emp,dep)=>new {db }    )
                //return View(db.Departamentos.Select(c => c.Empleados) && db.Empleados.Where(c.Departamentos == search || search == null).ToList());
                //return View(db.Empleados.Where(x => x.Departamentos.Contains(search) && x.Estatus == true || search == null && x.Estatus == true).ToList())
           // }
            else
            {
                return View(empleados.ToList());
            }
            //else
            //{
            //    //return View(db.Students.Where(x = > x.Name.StartsWith(search) || search == null).ToList());
            //}
        }

        //public ActionResult InMonth(DateTime month)
        //{
        //    var empleados = db.Empleados.Where(e => e.Fecha_Ingreso == );


        //    return View(empleados.ToList());
        //}

        public ActionResult InactiveClient()
        {
            var empleados = db.Empleados.Where(e => e.Estatus == false);


            return View(empleados.ToList());
        }

        // GET: Empleadoes
        public ActionResult Index()
        {
            var empleados = db.Empleados.Include(e => e.Cargo1).Include(e => e.Departamento1);
            return View(empleados.ToList());
        }

        // GET: Empleadoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // GET: Empleadoes/Create
        public ActionResult Create()
        {
            ViewBag.Cargo = new SelectList(db.Cargoes, "Id", "Nombre_Cargo");
            ViewBag.Departamento = new SelectList(db.Departamentos, "Id", "Nombre_Departamento");
            return View();
        }

        // POST: Empleadoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Apellido,Telefono,Departamento,Cargo,Fecha_Ingreso,Salario,Estatus")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Empleados.Add(empleado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cargo = new SelectList(db.Cargoes, "Id", "Nombre_Cargo", empleado.Cargo);
            ViewBag.Departamento = new SelectList(db.Departamentos, "Id", "Nombre_Departamento", empleado.Departamento);
            return View(empleado);
        }

        // GET: Empleadoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cargo = new SelectList(db.Cargoes, "Id", "Nombre_Cargo", empleado.Cargo);
            ViewBag.Departamento = new SelectList(db.Departamentos, "Id", "Nombre_Departamento", empleado.Departamento);
            return View(empleado);
        }

        // POST: Empleadoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Apellido,Telefono,Departamento,Cargo,Fecha_Ingreso,Salario,Estatus")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cargo = new SelectList(db.Cargoes, "Id", "Nombre_Cargo", empleado.Cargo);
            ViewBag.Departamento = new SelectList(db.Departamentos, "Id", "Nombre_Departamento", empleado.Departamento);
            return View(empleado);
        }

        // GET: Empleadoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: Empleadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleado empleado = db.Empleados.Find(id);
            db.Empleados.Remove(empleado);
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
