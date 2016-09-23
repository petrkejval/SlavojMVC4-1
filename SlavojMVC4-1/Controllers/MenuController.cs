using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SlavojMVC4_1.Models;
using PagedList;
using SlavojMVC4_1.Infrastructure;

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
        public ActionResult Vysledky()
        {
            var model = db.Vysledky.Where(w => w.Rocnik.JeVybrany).OrderBy(o => o.SoutezId);

            return View(model);
        }

        // GET: Menu
        public ActionResult Kuzelna()
        {
            var id = db.Kuzelny.Max(m => m.KuzelnaId);
            var model = db.Kuzelny.Find(id);

            ViewBag.RecordyGroup =
            (from item in new SlavojDBContainer().Rekordy
             select new RekordGroup
             {
                 JeRegistrovan = item.JeRegistrovan,
                 RegistrovanNazev = item.JeRegistrovan ? "Registrovaní" : "Neregistrovaní",
                 RekordyKategorieId = item.RekordyKategorieId,
                 RekordyKategorieNazev = item.RekordyKategorie.Nazev,
                 DisciplinaId = item.DisciplinaId,
                 DisciplinaPocetHodu = item.Disciplina.PocetHodu,
                 DisciplinaNazev = item.PocetHracu == 1 ? "Jednotlivci" : item.PocetHracu.ToString() + " členná družstva",
                 DisciplinaKategorieNazev = item.Disciplina.DisciplinyKategorie.Nazev,
                 PocetHracu = item.PocetHracu,
             }
            )
            .Distinct().OrderByDescending(o => o.JeRegistrovan).ThenBy(o => o.RekordyKategorieId).ThenBy(o => o.DisciplinaId).ThenByDescending(o => o.PocetHracu)
            .ToList();

            ViewBag.Recordy = db.Rekordy as IEnumerable<SlavojMVC4_1.Models.Rekord>;

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


        // GET: AkceNaKuzelne
        public ActionResult ProgramNaKuzelne()
        {
            int pocetNejblizsichDnu = 60;
            var model = SlavojMVC4_1.Infrastructure.Tools.ProgramNaKuzelne(pocetNejblizsichDnu);
            return View(model);
        }

        public virtual ActionResult Zapasy(string podmDruzstva = ItemDruzstva.Vse, string podmKde = ItemKde.DomaciIVenkovniKuzelna, int page = 1)
        {
            List<ZapasBarva> zapasyBarvy = new SlavojDBContainer().ZapasyBarvy.ToList();
            bool? pKde = null;
            if (podmKde == ItemKde.DomaciKuzelna)
            {
                pKde = true;
            }
            else if (podmKde == ItemKde.VenkovniKuzelna)
            {
                pKde = false;
            }
            var model =
                (from item in new SlavojDBContainer().Zapasy

                 select new ZapasNejblizsi
                 {
                     ZapasId = item.ZapasId,
                     SoutezNazev = item.Soutez.Nazev,
                     KuzelnaNazev = item.KuzelnaNazev,
                     KuzelnaDomaci = item.KuzelnaDomaci,
                     ZapasDatumCasOd = item.ZapasDatumCasOd,
                     ZapasDatumCasDo = item.ZapasDatumCasDo,
                     Druzstvo1Nazev = item.Druzstvo1Nazev,
                     Druzstvo2Nazev = item.Druzstvo2Nazev,
                     SrazDatumCas = item.SrazDatumCas,
                     MistoSrazu = item.MistoSrazu,
                     Poznamka = item.Poznamka,
                     Rozhodci = item.Rozhodci

                 }
                )
                .ToList()
                .Where
                  (w =>
                        (w.ZapasDatumCasDo >= DateTime.Now)
                     && (podmDruzstva == ItemDruzstva.Vse || w.Druzstvo1Nazev == podmDruzstva || w.Druzstvo2Nazev == podmDruzstva)
                     && (podmKde == ItemKde.DomaciIVenkovniKuzelna || w.KuzelnaDomaci == pKde)
                  )
                .OrderBy(o => o.ZapasDatumCasOd)
                .ThenBy(o => o.SoutezNazev)
                .ToPagedList(page, 22)
                ;

            foreach (var item in model)
            {
                item.Druzstvo1Barva = SlavojMVC4_1.Infrastructure.Tools.GetBarva(item.Druzstvo1Nazev, zapasyBarvy);
                item.Druzstvo2Barva = SlavojMVC4_1.Infrastructure.Tools.GetBarva(item.Druzstvo2Nazev, zapasyBarvy);

            };

            var list = db.Zapasy.Where(w => w.KuzelnaDomaci == true).Select(s => new Telerik.Web.Mvc.UI.DropDownItem { Text = s.Druzstvo1Nazev, Value = s.Druzstvo1Nazev }).Distinct().ToList();
            list.Add(new Telerik.Web.Mvc.UI.DropDownItem { Text = ItemDruzstva.Vse, Value = ItemDruzstva.Vse });

            ViewBag.DruzstvaListItem = list.ToList().OrderBy(o => o.Text);

            List<Telerik.Web.Mvc.UI.DropDownItem> kdeList = new List<Telerik.Web.Mvc.UI.DropDownItem>();
            kdeList.Add(new Telerik.Web.Mvc.UI.DropDownItem { Text = ItemKde.DomaciIVenkovniKuzelna, Value = ItemKde.DomaciIVenkovniKuzelna });
            kdeList.Add(new Telerik.Web.Mvc.UI.DropDownItem { Text = ItemKde.DomaciKuzelna, Value = ItemKde.DomaciKuzelna });
            kdeList.Add(new Telerik.Web.Mvc.UI.DropDownItem { Text = ItemKde.VenkovniKuzelna, Value = ItemKde.VenkovniKuzelna });
            ViewBag.KdeListItem = kdeList.ToList();

            ViewBag.PodmDruzstva = podmDruzstva;
            ViewBag.PodmKde = podmKde;
            ViewBag.Page = page;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Zapasy", model);
            }

            return View(model);
        }

        [Authorize(Roles = ("superuser"))]
        public ActionResult ZapasEdit(int zapasId = 0, string podmDruzstva = ItemDruzstva.Vse, string podmKde = ItemKde.DomaciIVenkovniKuzelna, int page = 0)
        {
            //ZapasId = 0;
            return RedirectToAction("Zapasy", "Nastaveni", new {zapasId = zapasId, podmDruzstva = podmDruzstva, podmKde = podmKde, page = page });
        }
    }
}