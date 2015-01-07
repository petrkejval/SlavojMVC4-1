namespace SlavojMVC4_1.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using SlavojMVC4_1.Models;

    public class RekordyKategoriesSessionRepository
    {
        public static IList<RekordyKategorieEditable> All(bool refreshDb = false)
        {
            IList<RekordyKategorieEditable> result = (IList<RekordyKategorieEditable>)HttpContext.Current.Session["RekordyKategories"];
            if (refreshDb) result = null;
            if (result == null)
            {
                HttpContext.Current.Session["RekordyKategories"] = result =
                    (from item in new SlavojDBContainer().RekordyKategories
                     select new RekordyKategorieEditable
                     {
                         RekordyKategorieId = item.RekordyKategorieId,
                         Nazev = item.Nazev
                     }
                    ).ToList();

            }

            return result;
        }

        public static RekordyKategorieEditable One(Func<RekordyKategorieEditable, bool> predicate, bool refreshDb = false)
        {
            return All(refreshDb).Where(predicate).FirstOrDefault();
        }

        public static void Insert(RekordyKategorieEditable item, bool refreshDb = false)
        {

            All(refreshDb).Insert(0, item);
        }

        public static void Update(RekordyKategorieEditable item, bool refreshDb = false)
        {

            RekordyKategorieEditable target = One(p => p.RekordyKategorieId == item.RekordyKategorieId, refreshDb);
            if (target != null)
            {
                target.RekordyKategorieId = item.RekordyKategorieId;
                target.Nazev = item.Nazev;
            }

        }

        public static void Delete(RekordyKategorieEditable item, bool refreshDb = false)
        {
            RekordyKategorieEditable target = One(p => p.RekordyKategorieId == item.RekordyKategorieId);
            if (target != null)
            {
                All(refreshDb).Remove(target);
            }
        }
    }
}