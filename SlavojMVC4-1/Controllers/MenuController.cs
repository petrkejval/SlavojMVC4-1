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
        public ActionResult Kuzelna()
        {
            var id = db.Kuzelny.Max(m => m.KuzelnaId);
            var model = db.Kuzelny.Find(id);

            return View(model);
        }
        // GET: Menu
        public ActionResult Kontakty()
        {
            var id = db.Kluby.Max(m => m.KlubId);
            var model = db.Kluby.Find(id);

            ViewBag.Prezident =
            db.Cleni.Where(w =>
                                (w.CleniInRoles.Where(b => b.ClenRoleId == 1).Count() > 0)  //musí být prezident
                          )
                          .FirstOrDefault()
                          as SlavojMVC4_1.Models.Clen;

            ViewBag.VicePrezident =
            db.Cleni.Where(w =>
                                (w.CleniInRoles.Where(b => b.ClenRoleId == 2).Count() > 0)  //musí být viceprezident
                          )
                          .FirstOrDefault()
                          as SlavojMVC4_1.Models.Clen;

            ViewBag.Pokladnik =
            db.Cleni.Where(w =>
                                (w.CleniInRoles.Where(b => b.ClenRoleId == 3).Count() > 0)  //musí být pokladník
                          )
                          .FirstOrDefault()
                          as SlavojMVC4_1.Models.Clen;

            ViewBag.OrganizacniPracovnikKlubu =
                db.Cleni.Where(w =>
                                    (w.CleniInRoles.Where(b => b.ClenRoleId == 5).Count() > 0)  //musí být organizační pracovník
                              )
                              .FirstOrDefault()
                              as SlavojMVC4_1.Models.Clen;


            ViewBag.CleniVyboruPouze =
                db.Cleni.Where(w =>
                                    (w.CleniInRoles.Where(b => b.ClenRoleId == 4).Count() > 0)  //musí být člem výboru
                                 && (w.CleniInRoles.Where(c => c.ClenRoleId == 1).Count() == 0) //nesmí být prezident
                                 && (w.CleniInRoles.Where(c => c.ClenRoleId == 2).Count() == 0) //nesmí být viceprezident
                                 && (w.CleniInRoles.Where(c => c.ClenRoleId == 3).Count() == 0) //nesmí být pokladník
                              )
                              .OrderBy(o => o.Prijmeni).ThenBy(o => o.Jmeno)
                              as IEnumerable<SlavojMVC4_1.Models.Clen>;
            return View(model);
        }
    }
}