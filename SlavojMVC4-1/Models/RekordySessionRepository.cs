namespace SlavojMVC4_1.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using SlavojMVC4_1.Models;

    public class RekordySessionRepository
    {
        public static IList<RekordEditable> All(bool refreshDb = false)
        {
            IList<RekordEditable> result = (IList<RekordEditable>)HttpContext.Current.Session["Rekordy"];
            if (refreshDb) result = null;
            if (result == null)
            {
                HttpContext.Current.Session["Rekordy"] = result =
                    (from item in new SlavojDBContainer().Rekordy
                     select new RekordEditable
                     {
                         RekordId = item.RekordId,
                         Nazev = item.Nazev,
                         DisciplinaId = item.DisciplinaId,
                         PocetHracu = item.PocetHracu,
                         RekordyKategorieId = item.RekordyKategorieId,
                         JeRegistrovan = item.JeRegistrovan,
                         Nahoz = item.Nahoz,
                         DatumNahozu = item.DatumNahozu,
                         Popis = item.Popis
                     }
                    ).ToList();

            }

            return result;
        }

        public static RekordEditable One(Func<RekordEditable, bool> predicate, bool refreshDb = false)
        {
            return All(refreshDb).Where(predicate).FirstOrDefault();
        }

        public static void Insert(RekordEditable soutez, bool refreshDb = false)
        {

            All(refreshDb).Insert(0, soutez);
        }

        public static void Update(RekordEditable item, bool refreshDb = false)
        {

            RekordEditable target = One(p => p.RekordId == item.RekordId, refreshDb);
            if (target != null)
            {
                target.RekordId = item.RekordId;
                target.Nazev = item.Nazev;
                target.DisciplinaId = item.DisciplinaId;
                target.PocetHracu = item.PocetHracu;
                target.RekordyKategorieId = item.RekordyKategorieId;
                target.JeRegistrovan = item.JeRegistrovan;
                target.Nahoz = item.Nahoz;
                target.DatumNahozu = item.DatumNahozu;
                target.Popis = item.Popis;
            }

        }

        public static void Delete(RekordEditable item, bool refreshDb = false)
        {
            RekordEditable target = One(p => p.RekordId == item.RekordId);
            if (target != null)
            {
                All(refreshDb).Remove(target);
            }
        }
    }
}