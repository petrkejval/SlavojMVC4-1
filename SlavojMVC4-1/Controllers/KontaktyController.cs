using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SlavojMVC4_1.Controllers
{
    public partial class KontaktyController : Controller
    {
        //
        // GET: /Kontakty/

        public virtual ActionResult Index()
        {
            return View();
        }

    }
}
