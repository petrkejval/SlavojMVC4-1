using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SlavojMVC4_1.Models;


namespace SlavojMVC4_1.Controllers.Nastaveni
{
    public class ClenList
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class NastaveniController : Controller
    {
        // GET: Cleni
        public ActionResult Cleni()
        {
            return View();
        }

        public ActionResult Druzstva()
        {
            ViewBag.DruzstvaCleniListItem = new SlavojDBContainer().Cleni
                                                 .Where(w => (w.JeClen && w.Registrace != null) || (w.ClenId == 0))
                                                 .Select(e => new { Id = e.ClenId, Name = e.Prijmeni + " " + e.Jmeno })
                                                 .OrderBy(e => e.Name);

            ViewBag.SoutezeListItem = new SlavojDBContainer().Souteze
                .Select(e => new { Id = e.SoutezId, Name = e.SoutezId != 0 ? e.Nazev + " " + "(" + e.KategorieSouteze.Nazev + ", " + e.PocetNutnychDrah + ")" : e.Nazev })
                                     .OrderBy(e => e.Id);

            ViewBag.TreneriListItem = new SlavojDBContainer().Cleni
                                                 .Where(w => (w.JeClen && w.Trener != null) || (w.ClenId == 0))
                                                 .Select(e => new { Id = e.ClenId, Name = e.Prijmeni + " " + e.Jmeno })
                                                 .OrderBy(e => e.Name);

            ViewBag.WebPagesListItem = new SlavojDBContainer().WebPages
                                                 .Select(e => new { Id = e.WebPageId, Name = e.Nazev })
                                                 .OrderBy(e => e.Name);
            return View();
        }

        public ActionResult Souteze()
        {
            ViewBag.KategorieSouteziListItem = new SlavojDBContainer().KategorieSoutezi
                                                 .Select(e => new { Id = e.KategorieSoutezeId, Name = e.Nazev })
                                                 .OrderBy(e => e.Name);
            return View();
        }

        public ActionResult KategorieSoutezi()
        {
            return View();
        }

        public ActionResult Turnaje()
        {
            ViewBag.RediteleTurnajuListItem = new SlavojDBContainer().Cleni
                                                 .Where(w => (w.JeClen) || (w.ClenId == 0))
                                                 .Select(e => new { Id = e.ClenId, Name = e.Prijmeni + " " + e.Jmeno })
                                                 .OrderBy(e => e.Name);
            
            ViewBag.WebPagesListItem = new SlavojDBContainer().WebPages
                                                 .Select(e => new { Id = e.WebPageId, Name = e.Nazev })
                                                 .OrderBy(e => e.Name);

            return View();
        }

        public ActionResult WebPages()
        {
            return View();
        }
    }
}