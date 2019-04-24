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
    public class PuestoController : Controller
    {
        private DatabaseEntities2 db = new DatabaseEntities2();

        // GET: Puesto
        public ActionResult Index()
        {
            var puestos = db.Puestos.Include(p => p.Departamento);
            return View(puestos.ToList());
        }


        //aqui inicia el codigo
        [HttpGet]
        public PartialViewResult Create()
        {
            
            ViewBag.CodDepartamento = new SelectList(db.Departamentos, "Codigo", "Descripcion");
            return PartialView("Create", new Models.Puesto());
        }
        [HttpPost]
        public JsonResult Create(Puesto emp)
        {
            DatabaseEntities2 sd = new DatabaseEntities2();
            sd.Puestos.Add(emp);
            sd.SaveChanges();
            return Json(emp, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public PartialViewResult Edit(Int32 Codigo)
        {
            DatabaseEntities2 sd = new DatabaseEntities2();
            Puesto emp = sd.Puestos.Where(x => x.Codigo == Codigo).FirstOrDefault();
            Puesto empclass = new Puesto();
            empclass.Codigo = emp.Codigo;
            empclass.CodDepartamento = emp.CodDepartamento;
            empclass.Descripcion = emp.Descripcion;
          
            ViewBag.CodDepartamento = new SelectList(db.Departamentos, "Codigo", "Descripcion",empclass.CodDepartamento);
           
            return PartialView(empclass);

        }
        [HttpPost]
        public JsonResult Edit(Puesto emp)
        {
            DatabaseEntities2 sd = new DatabaseEntities2();
            Puesto emptb = sd.Puestos.Where(x => x.Codigo == emp.Codigo).FirstOrDefault();
            Puesto empclass = new Puesto();
            emptb.Codigo = emp.Codigo;
            emptb.CodDepartamento = emp.CodDepartamento;
            emptb.Descripcion = emp.Descripcion;
            sd.SaveChanges();
            return Json(emptb, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Borrar(Int32 Codigo)
        {
            DatabaseEntities2 sd = new DatabaseEntities2();
            var del = (from Puesto in sd.Puestos where Puesto.Codigo == Codigo select Puesto).FirstOrDefault();
            sd.Puestos.Remove(del);
            sd.SaveChanges();
            return RedirectToAction("Index");


        }

    }
}
