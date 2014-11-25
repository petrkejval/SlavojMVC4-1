using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SlavojMVC4_1.Models;

namespace SlavojMVC4_1.Controllers
{
    public class MenuController : Controller
    {
        private SlavojDBContainer db;
        public MenuController()
        {
            db = new SlavojDBContainer();
        }

        // GET: Menu
        public ActionResult Index(int id)
        {
            var model = db.WebPages.Find(id);
            return View(model);
        }

        // GET: Menu
        public ActionResult Kuzelny()
        {
            var id = db.Kuzelny.Max(m => m.KuzelnaId);
            var model = db.Kuzelny.Find(id);

            return View(model);
        }
    }
}