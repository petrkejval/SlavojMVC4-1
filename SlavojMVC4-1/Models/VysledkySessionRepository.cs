namespace SlavojMVC4_1.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using SlavojMVC4_1.Models;

    public class VysledkySessionRepository
    {
        public static IList<VysledekEditable> All(bool refreshDb = false)
        {
            IList<VysledekEditable> result = (IList<VysledekEditable>)HttpContext.Current.Session["Vysledky"];
            if (refreshDb) result = null;
            if (result == null)
            {
                HttpContext.Current.Session["Vysledky"] = result =
                    (from item in new SlavojDBContainer().Vysledky
                     select new VysledekEditable
                     {
                         VysledekId = item.VysledekId,
                         RocnikId = item.RocnikId,
                         SoutezId = item.SoutezId,
                         Tabulka = item.Tabulka,
                         WebPageIId = item.WebPageIId,
                         Rozpis = item.Rozpis,
                         Rozlosovani = item.Rozlosovani,
                         SoupiskaPodzim = item.SoupiskaPodzim,
                         SoupiskaJaro = item.SoupiskaJaro
                     }
                    ).ToList();

            }

            return result;
        }

        public static VysledekEditable One(Func<VysledekEditable, bool> predicate, bool refreshDb = false)
        {
            return All(refreshDb).Where(predicate).FirstOrDefault();
        }

        public static void Insert(VysledekEditable item, bool refreshDb = false)
        {

            All(refreshDb).Insert(0, item);
        }

        public static void Update(VysledekEditable item, bool refreshDb = false)
        {

            VysledekEditable target = One(p => p.VysledekId == item.VysledekId, refreshDb);
            if (target != null)
            {
                target.VysledekId = item.VysledekId;
                target.RocnikId = item.RocnikId;
                target.SoutezId = item.SoutezId;
                target.Tabulka = item.Tabulka;
                target.WebPageIId = item.WebPageIId;
                target.Rozpis = item.Rozpis;
                target.Rozlosovani = item.Rozlosovani;
                target.SoupiskaPodzim = item.SoupiskaPodzim;
                target.SoupiskaJaro = item.SoupiskaJaro;
            }

        }

        public static void Delete(VysledekEditable item, bool refreshDb = false)
        {
            VysledekEditable target = One(p => p.VysledekId == item.VysledekId);
            if (target != null)
            {
                All(refreshDb).Remove(target);
            }
        }
    }
}