using System;
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
    public class DepartamentoController : Controller
    {
        private DatabaseEntities2 db = new DatabaseEntities2();

        // GET: Departamento
        public ActionResult Index()
        {
            var departamentos = db.Departamentos.Include(d => d.Nivel1);
            return View(departamentos.ToList());
        }

  

        [HttpGet]
        public PartialViewResult Create()
        {
            ViewBag.Nivel = new SelectList(db.Nivels, "Id", "Id");
            return PartialView("Create", new Models.Departamento());
        }
        [HttpPost]
        public JsonResult Create(Departamento emp)
        {
            DatabaseEntities2 sd = new DatabaseEntities2();
            sd.Departamentos.Add(emp);
            sd.SaveChanges();
            return Json(emp, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public PartialViewResult Edit(Int32 Codigo)
        {
            DatabaseEntities2 sd = new DatabaseEntities2();
            Departamento emp = sd.Departamentos.Where(x => x.Codigo == Codigo).FirstOrDefault();
            Departamento empclass = new Departamento();
            empclass.Codigo = emp.Codigo;
            empclass.Nivel =emp.Nivel;
            empclass.Descripcion = emp.Descripcion;
            ViewBag.Nivel = new SelectList(sd.Nivels, "Id", "Id", empclass.Nivel);
            return PartialView(empclass);

        }
        [HttpPost]
        public JsonResult Edit(Departamento emp)
        {
            DatabaseEntities2 sd = new DatabaseEntities2();
            Departamento emptb = sd.Departamentos.Where(x => x.Codigo == emp.Codigo).FirstOrDefault();
            Departamento empclass = new Departamento();
            emptb.Codigo = emp.Codigo;
            emptb.Nivel = emp.Nivel;
            emptb.Descripcion = emp.Descripcion;
            sd.SaveChanges();
            return Json(emptb, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Borrar(Int32 Codigo) {
            DatabaseEntities2 sd = new DatabaseEntities2();
            var del = (from Departamento in sd.Departamentos where Departamento.Codigo == Codigo select Departamento).FirstOrDefault();
            sd.Departamentos.Remove(del);
            sd.SaveChanges();
            return RedirectToAction("Index");


        }

    }
}
