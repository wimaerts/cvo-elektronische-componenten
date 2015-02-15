using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using elektronische_componenten.DAL;
using elektronische_componenten.Models;

namespace elektronische_componenten.Controllers
{
    public class ComponentController : Controller
    {
        private ComponentContext db = new ComponentContext();

        // GET: Component
        public ActionResult Index(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CategorieSortParm = (sortOrder=="cat_asc" || String.IsNullOrEmpty(sortOrder)) ? "cat_desc" : "cat_asc";
            ViewBag.AantalSortParm = (sortOrder == "aantal_asc" || String.IsNullOrEmpty(sortOrder)) ? "aantal_desc" : "aantal_asc";
            ViewBag.PrijsSortParm = (sortOrder == "prijs_asc" || String.IsNullOrEmpty(sortOrder)) ? "prijs_desc" : "prijs_asc";

            var componenten = from c in db.Componenten
                              select c;

            switch(sortOrder)
            {
                case "name_desc":
                    componenten = componenten.OrderByDescending(c => c.Naam);
                    break;
                case "cat_asc":
                    componenten = componenten.OrderBy(c => c.Categorie.Naam);
                    break;
                case "cat_desc":
                    componenten = componenten.OrderByDescending(c => c.Categorie.Naam);
                    break;
                case "aantal_asc":
                    componenten = componenten.OrderBy(c => c.Aantal);
                    break;
                case "aantal_desc":
                    componenten = componenten.OrderByDescending(c => c.Aantal);
                    break;
                case "prijs_asc":
                    componenten = componenten.OrderBy(c => c.Aankoopprijs);
                    break;
                case "prijs_desc":
                    componenten = componenten.OrderByDescending(c => c.Aankoopprijs);
                    break;
                default:
                    componenten = componenten.OrderBy(c => c.Naam);
                    break;
            }

            return View(componenten.ToList());
        }

        // GET: Component/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Component component = db.Componenten.Find(id);
            if (component == null)
            {
                return HttpNotFound();
            }
            return View(component);
        }

        // GET: Component/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Component/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Naam,Datasheet,Aantal,Aankoopprijs")] Component component)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Componenten.Add(component);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(component);
        }

        // GET: Component/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Component component = db.Componenten.Find(id);
            if (component == null)
            {
                return HttpNotFound();
            }
            return View(component);
        }

        // POST: Component/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naam,Datasheet,Aantal,Aankoopprijs")] Component component)
        {
            if (ModelState.IsValid)
            {
                db.Entry(component).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(component);
        }

        // GET: Component/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError=false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Component component = db.Componenten.Find(id);
            if (component == null)
            {
                return HttpNotFound();
            }
            return View(component);
        }

        // POST: Component/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Component component = db.Componenten.Find(id);
                db.Componenten.Remove(component);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
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
