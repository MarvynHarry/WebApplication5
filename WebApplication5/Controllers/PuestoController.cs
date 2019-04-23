﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class PuestoController : Controller
    {
        private DatabaseEntities2 db = new DatabaseEntities2();

        // GET: Puesto
        public ActionResult Index()
        {
            var puestos = db.Puestos.Include(p => p.Departamento);
            return View(puestos.ToList());
        }

        // GET: Puesto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Puesto puesto = db.Puestos.Find(id);
            if (puesto == null)
            {
                return HttpNotFound();
            }
            return View(puesto);
        }

        // GET: Puesto/Create
        public ActionResult Create()
        {
            ViewBag.CodDepartamento = new SelectList(db.Departamentos, "Codigo", "Descripcion");
            return View();
        }

        // POST: Puesto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,CodDepartamento,Descripcion")] Puesto puesto)
        {
            if (ModelState.IsValid)
            {
                db.Puestos.Add(puesto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodDepartamento = new SelectList(db.Departamentos, "Codigo", "Descripcion", puesto.CodDepartamento);
            return View(puesto);
        }

        // GET: Puesto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Puesto puesto = db.Puestos.Find(id);
            if (puesto == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodDepartamento = new SelectList(db.Departamentos, "Codigo", "Descripcion", puesto.CodDepartamento);
            return View(puesto);
        }

        // POST: Puesto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,CodDepartamento,Descripcion")] Puesto puesto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(puesto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodDepartamento = new SelectList(db.Departamentos, "Codigo", "Descripcion", puesto.CodDepartamento);
            return View(puesto);
        }

        // GET: Puesto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Puesto puesto = db.Puestos.Find(id);
            if (puesto == null)
            {
                return HttpNotFound();
            }
            return View(puesto);
        }

        // POST: Puesto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Puesto puesto = db.Puestos.Find(id);
            db.Puestos.Remove(puesto);
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
