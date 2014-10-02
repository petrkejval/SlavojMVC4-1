using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SlavojMVC4_1.Extensions;
using SlavojMVC4_1.Models;

namespace SlavojMVC4_1.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Models;

    using Extensions;
    using System.Threading;

    public partial class ComboBoxController : Controller
    {
        public virtual ActionResult AjaxLoading()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult _AjaxLoading(string text)
        {
            Thread.Sleep(1000);

            using ( var db = new SlavojDBContainer() )
            {
                var clanky = db. Clanky.AsQueryable();

                if ( text.HasValue() )
                {
                     clanky = clanky.Where((p) => p.Predmet.StartsWith(text));
                }

                return new JsonResult { Data = new SelectList(clanky.ToList(), "ClanekId", "Predmet") };
            }
        }

        [HttpPost]
        public virtual ActionResult _AutoCompleteAjaxLoading(string text)
        {
            Thread.Sleep(1000);

            using (var db = new SlavojDBContainer())
            {
                var clanky = db.Clanky.AsQueryable();

                if (text.HasValue())
                {
                    clanky = clanky.Where((p) => p.Predmet.StartsWith(text));
                }

                return new JsonResult { Data = clanky.Select(p => p.Predmet).ToList() };
            }
        }
    }
}
