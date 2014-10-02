using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SlavojMVC4_1.Controllers
{
    public partial class TurnajeController : Controller
    {
        //
        // GET: /Turnaje/

        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult OPerletovouPlaketu()
        {
            return View();
        }

        public virtual ActionResult LigaNeregistrovanych()
        {
            return View();
        }

        public virtual ActionResult VelikonocniKouleni()
        {
            return View();
        }
    
    }
}
