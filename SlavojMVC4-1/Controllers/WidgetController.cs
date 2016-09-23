using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SlavojMVC4_1.Models;

namespace SlavojMVC4_1.Controllers
{
    [ChildActionOnly]
    public class WidgetController : Controller
    {
        private int pocetNejblizsichDnu = 30;

        // GET: AkceNaKuzelne
        public ActionResult NejblizsiProgram()
        {
            var model = SlavojMVC4_1.Infrastructure.Tools.ProgramNaKuzelne(pocetNejblizsichDnu);

            return PartialView(model);
        }
        // GET: NejblizsiZapas
        public ActionResult NejblizsiZapasy()
        {
            //var model = nejblizsiZapasy.ToList().OrderBy(o => o.DatunACas).ThenBy(o => o.Soutez);

            List<ZapasBarva> zapasyBarvy = new SlavojDBContainer().ZapasyBarvy.ToList();

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
                .Where (w => (w.ZapasDatumCasDo >= DateTime.Now) && (w.ZapasDatumCasDo <= DateTime.Now.AddDays(pocetNejblizsichDnu)))
                .OrderBy(o => o.ZapasDatumCasOd)
                .ThenBy(o => o.SoutezNazev)
                ;

            foreach(var item in model)
            {
                item.Druzstvo1Barva = SlavojMVC4_1.Infrastructure.Tools.GetBarva(item.Druzstvo1Nazev, zapasyBarvy);
                item.Druzstvo2Barva = SlavojMVC4_1.Infrastructure.Tools.GetBarva(item.Druzstvo2Nazev, zapasyBarvy);

            };

            return PartialView(model);
        }


    }
}