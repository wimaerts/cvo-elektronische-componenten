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
using PagedList;
using System.Data.Entity.Infrastructure;

namespace elektronische_componenten.Controllers
{
    public class ComponentController : Controller
    {
        private ComponentContext db = new ComponentContext();

        // GET: Component
        public ActionResult Index(string sortOrder, string naamFilter, string currentFilter, string categorieId, string searchString, int? page)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CategorieSortParm = (sortOrder == "cat_asc" || String.IsNullOrEmpty(sortOrder)) ? "cat_desc" : "cat_asc";
            ViewBag.AantalSortParm = (sortOrder == "aantal_asc" || String.IsNullOrEmpty(sortOrder)) ? "aantal_desc" : "aantal_asc";
            ViewBag.PrijsSortParm = (sortOrder == "prijs_asc" || String.IsNullOrEmpty(sortOrder)) ? "prijs_desc" : "prijs_asc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = naamFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var componenten = from c in db.Componenten
                              select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                componenten = componenten.Where(c => c.Naam.ToUpper().Contains(searchString.ToUpper()));
            }

            if (!String.IsNullOrEmpty(categorieId))
            {
                int i = Convert.ToInt32(categorieId);
                componenten = componenten.Where(c => c.Categorie.Id.Equals(i));
            }

            switch (sortOrder)
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

            int pageSize = 5; 
            int pageNumber = (page ?? 1);
            PopulateCategoriënDropDownList();
            return View(componenten.ToPagedList(pageNumber, pageSize));
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
            PopulateCategoriënDropDownList();
            return View();
        }

        private void PopulateCategoriënDropDownList(object selectedCategorie = null)
        {
            var categoriënQuery = from c in db.Categoriën
                                  orderby c.Naam
                                  select c;

            ViewBag.CategorieList = new SelectList(categoriënQuery, "Id", "Naam", selectedCategorie);
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
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            PopulateCategoriënDropDownList(component.Categorie.Id);
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

            PopulateCategoriënDropDownList(component.Categorie.Id);
            return View(component);
        }

        // POST: Component/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naam,Datasheet,Aantal,Aankoopprijs,CategorieId")] Component component)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(component).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {                
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            PopulateCategoriënDropDownList(component.Categorie.Id);
            return View(component);
        }

        // GET: Component/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
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
