using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SlavojMVC4_1.Models;

namespace SlavojMVC4_1.Controllers
{
    public partial class DruzstvaController : Controller
    {
        private SlavojDBContainer db;
        public DruzstvaController()
        {
            db = new SlavojDBContainer();
        }

        //
        // GET: /Druzstva/
        public virtual ActionResult Index(int id = 1)
        {
            var model = db.Druzstva.Find(id);
            return View(model);
        }

    }
}
