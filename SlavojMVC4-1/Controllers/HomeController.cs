using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using SlavojMVC4_1.Models;
using WebMatrix.WebData;
using PagedList;
using System.Threading;
using SlavojMVC4_1.Extensions;
using Telerik.Web.Mvc.UI;

namespace SlavojMVC4_1.Controllers
{
    public partial class HomeController : Controller
    {
        //SlavojDBContainer _db = new SlavojDBContainer();

        //public virtual ActionResult Index()
        //{
        //    ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
        //    var model = _db.Clanky.ToList();
        //    return View(model);
        //}

        private SlavojDBContainer db;

        //
        // GET: /Home/


        //public virtual ActionResult Index()
        //{
        //    ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
        //    return View(db.Clanky.ToList());
        //}
        public HomeController()
        {
            db = new SlavojDBContainer();
        }

        //public virtual ActionResult Autocomplete(string term)
        //{
        //    var model =
        //        db.Clanky
        //           .Where(r => r.Predmet.StartsWith(term))
        //           .Take(10)
        //           .Select(r => new
        //           {
        //               label = r.Predmet
        //           });

        //    return Json(model, JsonRequestBehavior.AllowGet);
        //}
        //..........................................................................................
        public virtual ActionResult AjaxLoading()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult _AjaxLoading(string text)
        {
            Thread.Sleep(1000);

            var kategories = db.Kategories.AsQueryable();

            if (text.HasValue())
            {
                kategories = kategories.Where((p) => p.Nazev.StartsWith(text));
            }

            return new JsonResult { Data = new SelectList(kategories.ToList(), "KategorieId", "Nazev") };
        }

        public virtual ActionResult LoadKategorie()
        {
           
            var data = new SelectList(db.Kategories.ToList(), "KategorieId", "Nazev");
            return View(data); 

        }

        [HttpPost]
        public virtual ActionResult _AutoCompleteAjaxLoading(string text)
        {
           Thread.Sleep(1000);

            var clanky = db.Clanky.AsQueryable();

            if (text.HasValue())
            {
                clanky = clanky.Where(r => r.Predmet.Contains(text));
            }

            return new JsonResult { Data = clanky.Select(r => r.Predmet).ToList()};
        }
        //..........................................................................................

        public virtual ActionResult Index(
            string searchTerm = null
              ,int page = 1
              ,int userId = 0
              ,int kategorieId = -1
              ,DateTime? datumvVytvoreni = null
            )
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            DateTime datumvVytvoreni1;

            if (datumvVytvoreni == null)
            {
                datumvVytvoreni1 = DateTime.MinValue;
            }
            else
            {
                datumvVytvoreni1 = (DateTime)datumvVytvoreni;
            }
            var model = db.Clanky
                    .OrderByDescending(r => r.DatumVytvoreni)
                    .Where
                    (
                        r =>    (searchTerm       == null              || r.Text.Contains(searchTerm) || r.Predmet.Contains(searchTerm))
                             && (userId           == 0                 || r.UserId      == userId)
                             && (kategorieId      == -1                || r.KategorieId == kategorieId)
                             && (datumvVytvoreni1 == DateTime.MinValue || (r.DatumVytvoreni.Year == datumvVytvoreni1.Year && r.DatumVytvoreni.Month == datumvVytvoreni1.Month && r.DatumVytvoreni.Day == datumvVytvoreni1.Day))
                    )
                    .ToPagedList(page, 10)
                    ;
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Clanky", model);
            }

            return View(model);
        }

        //
        // GET: /Home/Details/5

        public virtual ActionResult Details(int id = 0)
        {
            Clanek clanek = db.Clanky.Find(id);
            if (clanek == null)
            {
                return HttpNotFound();
            }
            return View(clanek);
        }

        //
        // GET: /Home/Create

        public virtual ActionResult Create()
        {
            ViewBag.KategoriesListItem = new SelectList(db.Kategories.OrderBy(r => r.Nazev).ToList(), "KategorieId", "Nazev");
            return View();
        }

        //
        // POST: /Home/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public virtual ActionResult Create(Clanek clanek)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    clanek.UserId = WebSecurity.CurrentUserId;
                    clanek.DatumVytvoreni = DateTime.Now;
                    clanek.DatumZmeny = DateTime.Now;

                    db.Clanky.Add(clanek);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(clanek);
            }
            catch (DbEntityValidationException ex)
            {
                var error = ex.EntityValidationErrors.First().ValidationErrors.First();
                this.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                return View(clanek);
            }
        }

        //
        // GET: /Home/Edit/5
        [Authorize]
        public virtual ActionResult Edit(int id = 0)
        {
            Clanek clanek = db.Clanky.Find(id);
            if (clanek == null)
            {
                return HttpNotFound();
            }
            if ((WebSecurity.CurrentUserId == clanek.UserId) || (User.IsInRole("admin")))
            {
                
                ViewBag.KategoriesListItem = new SelectList(db.Kategories.OrderBy(r => r.Nazev).ToList(), "KategorieId", "Nazev");
                return View(clanek);
            }
            return RedirectToAction("Index");
        }

        //
        // POST: /Home/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(Clanek clanek)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    clanek.DatumZmeny = DateTime.Now;
                 
                    db.Entry(clanek).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                return View(clanek);
            }
            catch (DbEntityValidationException ex)
            {
                var error = ex.EntityValidationErrors.First().ValidationErrors.First();
                this.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                ViewBag.KategoriesListItem = new SelectList(db.Kategories.OrderBy(r => r.Nazev).ToList(), "KategorieId", "Nazev");
                return View(clanek);
            }
        }

        //
        // GET: /Home/Delete/5
        [Authorize]
        public virtual ActionResult Delete(int id = 0)
        {
            Clanek clanek = db.Clanky.Find(id);
            if (clanek == null)
            {
                return HttpNotFound();
            }
            if ((WebSecurity.CurrentUserId == clanek.UserId) || (User.IsInRole("admin")))
            {
                return View(clanek);
            }
            return RedirectToAction("Index");
        }

        //
        // POST: /Home/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            Clanek clanek = db.Clanky.Find(id);
            try
            {
                db.Clanky.Remove(clanek);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException ex)
            {
                var error = ex.EntityValidationErrors.First().ValidationErrors.First();
                this.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                return View(clanek);
            }

        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }

}
