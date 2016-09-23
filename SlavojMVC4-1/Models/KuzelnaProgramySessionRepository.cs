namespace SlavojMVC4_1.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using SlavojMVC4_1.Models;

    public class KuzelnaProgramySessionRepository
    {
        public static IList<KuzelnaProgramEditable> All(bool refreshDb = false)
        {
            IList<KuzelnaProgramEditable> result = (IList<KuzelnaProgramEditable>)HttpContext.Current.Session["KuzelnaProgramy"];
            if (refreshDb) result = null;
            if (result == null)
            {
                HttpContext.Current.Session["KuzelnaProgramy"] = result =
                    (from item in new SlavojDBContainer().KuzelnaProgramy
                     select new KuzelnaProgramEditable
                     {
                         KuzelnaProgramId = item.KuzelnaProgramId,
                         ProgramNazev = item.ProgramNazev,
                         ProgramKategorieId = item.ProgramKategorieId,
                         ProgramDatumCasOd = item.ProgramDatumCasOd,
                         ProgramDatumCasDo = item.ProgramDatumCasDo,
                         Poznamka = item.Poznamka,
                         OpakovatKazdyTyden = item.OpakovatKazdyTyden,
                         OpakovatDatumDo = item.OpakovatDatumDo,
                         JeAktivni = item.JeAktivni
                     }
                    )
                    .OrderBy(o => o.ProgramDatumCasOd)
                    .ThenBy(o => o.ProgramNazev)

                    .ToList()
                    ;

            }

            return result;
        }

        public static KuzelnaProgramEditable One(Func<KuzelnaProgramEditable, bool> predicate, bool refreshDb = false)
        {
            return All(refreshDb).Where(predicate).FirstOrDefault();
        }

        public static void Insert(KuzelnaProgramEditable item, bool refreshDb = false)
        {
            All(refreshDb).Insert(0, item);
        }

        public static void Update(KuzelnaProgramEditable item, bool refreshDb = false)
        {

            KuzelnaProgramEditable target = One(p => p.KuzelnaProgramId == item.KuzelnaProgramId, refreshDb);
            if (target != null)
            {
                target.KuzelnaProgramId = item.KuzelnaProgramId;
                target.ProgramNazev = item.ProgramNazev;
                target.ProgramKategorieId = item.ProgramKategorieId;
                target.ProgramDatumCasOd = item.ProgramDatumCasOd;
                target.ProgramDatumCasDo = item.ProgramDatumCasDo;
                target.Poznamka = item.Poznamka;
                target.OpakovatKazdyTyden = item.OpakovatKazdyTyden;
                target.OpakovatDatumDo = item.OpakovatDatumDo;
                target.JeAktivni = item.JeAktivni;

            }

        }

        public static void Delete(KuzelnaProgramEditable item, bool refreshDb = false)
        {
            KuzelnaProgramEditable target = One(p => p.KuzelnaProgramId == item.KuzelnaProgramId);
            if (target != null)
            {
                All(refreshDb).Remove(target);
            }
        }
    }
}