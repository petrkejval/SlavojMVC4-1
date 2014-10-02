using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SlavojMVC4_1.Models;

namespace SlavojMVC4_1.Controllers
{
    public class CleniController : Controller
    {
        private SlavojDBContainer db = new SlavojDBContainer();

        //
        // GET: /Cleni/

        public ActionResult Index()
        {
            return View(db.Cleni.ToList());
        }

        //
        // GET: /Cleni/Details/5

        public ActionResult Details(int id = 0)
        {
            Clen clen = db.Cleni.Find(id);
            if (clen == null)
            {
                return HttpNotFound();
            }
            return View(clen);
        }

        //
        // GET: /Cleni/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Cleni/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Clen clen)
        {
            if (ModelState.IsValid)
            {
                db.Cleni.Add(clen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(clen);
        }

        //
        // GET: /Cleni/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Clen clen = db.Cleni.Find(id);
            if (clen == null)
            {
                return HttpNotFound();
            }
            return View(clen);
        }

        //
        // POST: /Cleni/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Clen clen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clen);
        }

        //
        // GET: /Cleni/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Clen clen = db.Cleni.Find(id);
            if (clen == null)
            {
                return HttpNotFound();
            }
            return View(clen);
        }

        //
        // POST: /Cleni/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clen clen = db.Cleni.Find(id);
            db.Cleni.Remove(clen);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}