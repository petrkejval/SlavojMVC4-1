using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SlavojMVC4_1.Models;

namespace SlavojMVC4_1.Controllers
{
    public partial class TurnajeController : Controller
    {
        private SlavojDBContainer db;

        public TurnajeController()
        {
            db = new SlavojDBContainer();
        }
        //
        // GET: /Turnaje/
        public virtual ActionResult Index(int id)
        {
            var model = db.Turnaje.Find(id);
            return View(model);
        }

    
    }
}
