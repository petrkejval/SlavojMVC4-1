namespace SlavojMVC4_1.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using SlavojMVC4_1.Models;
    public class ZapasySessionRepository
    {
        public static IList<ZapasEditable> All(bool refreshDb = false)
        {
            IList<ZapasEditable> result = (IList<ZapasEditable>)HttpContext.Current.Session["Zapasy"];
            if (refreshDb) result = null;
            if (result == null)
            {
                HttpContext.Current.Session["Zapasy"] = result =
                    (from item in new SlavojDBContainer().Zapasy
                     select new ZapasEditable
                     {
                         ZapasId = item.ZapasId,
                         SoutezId = item.SoutezId,
                         KuzelnaNazev = item.KuzelnaNazev,
                         KuzelnaDomaci = item.KuzelnaDomaci,
                         ZapasDatumCasOd = item.ZapasDatumCasOd,
                         ZapasDatumCasDo = item.ZapasDatumCasDo,
                         Druzstvo1Nazev = item.Druzstvo1Nazev,
                         Druzstvo2Nazev = item.Druzstvo2Nazev,
                         SrazDatumCas = item.SrazDatumCas,
                         MistoSrazu = item.MistoSrazu,
                         Poznamka = item.Poznamka,
                         RozhodciId = item.RozhodciId
                     }
                    )
                    .OrderBy(o => o.ZapasDatumCasOd)
                    .ThenBy(o => o.SoutezId)
                    .ToList()
                    ;

            }

            return result;
        }

        public static ZapasEditable One(Func<ZapasEditable, bool> predicate, bool refreshDb = false)
        {
            return All(refreshDb).Where(predicate).FirstOrDefault();
        }

        public static void Insert(ZapasEditable item, bool refreshDb = false)
        {

            All(refreshDb).Insert(0, item);
        }

        public static void Update(ZapasEditable item, bool refreshDb = false)
        {

            ZapasEditable target = One(p => p.ZapasId == item.ZapasId, refreshDb);
            if (target != null)
            {
                target.ZapasId = item.ZapasId;
                target.SoutezId = item.SoutezId;
                target.KuzelnaNazev = item.KuzelnaNazev;
                target.KuzelnaDomaci = item.KuzelnaDomaci;
                target.ZapasDatumCasOd = item.ZapasDatumCasOd;
                target.ZapasDatumCasDo = item.ZapasDatumCasDo;
                target.Druzstvo1Nazev = item.Druzstvo1Nazev;
                target.Druzstvo2Nazev = item.Druzstvo2Nazev;
                target.SrazDatumCas = item.SrazDatumCas;
                target.MistoSrazu = item.MistoSrazu;
                target.Poznamka = item.Poznamka;
                target.RozhodciId = item.RozhodciId;
            }

        }

        public static void Delete(ZapasEditable item, bool refreshDb = false)
        {
            ZapasEditable target = One(p => p.ZapasId == item.ZapasId);
            if (target != null)
            {
                All(refreshDb).Remove(target);
            }
        }
    }
}