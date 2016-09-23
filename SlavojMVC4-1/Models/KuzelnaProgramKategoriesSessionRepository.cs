namespace SlavojMVC4_1.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using SlavojMVC4_1.Models;
    public class KuzelnaProgramKategoriesSessionRepository
    {
        public static IList<KuzelnaProgramKategorieEditable> All(bool refreshDb = false)
        {
            IList<KuzelnaProgramKategorieEditable> result = (IList<KuzelnaProgramKategorieEditable>)HttpContext.Current.Session["KuzelnProgramKategories"];
            if (refreshDb) result = null;
            if (result == null)
            {
                HttpContext.Current.Session["KuzelnProgramKategories"] = result =
                    (from item in new SlavojDBContainer().KuzelnaProgramKategories
                     select new KuzelnaProgramKategorieEditable
                     {
                         KuzelnaProgramKategorieId = item.KuzelnaProgramKategorieId,
                         Nazev = item.Nazev,
                         Barva = item.Barva
                     }
                    ).ToList();

            }

            return result;
        }

        public static KuzelnaProgramKategorieEditable One(Func<KuzelnaProgramKategorieEditable, bool> predicate, bool refreshDb = false)
        {
            return All(refreshDb).Where(predicate).FirstOrDefault();
        }

        public static void Insert(KuzelnaProgramKategorieEditable item, bool refreshDb = false)
        {

            All(refreshDb).Insert(0, item);
        }

        public static void Update(KuzelnaProgramKategorieEditable item, bool refreshDb = false)
        {

            KuzelnaProgramKategorieEditable target = One(p => p.KuzelnaProgramKategorieId == item.KuzelnaProgramKategorieId, refreshDb);
            if (target != null)
            {
                target.KuzelnaProgramKategorieId = item.KuzelnaProgramKategorieId;
                target.Nazev = item.Nazev;
                target.Barva = item.Barva;
            }

        }

        public static void Delete(KuzelnaProgramKategorieEditable item, bool refreshDb = false)
        {
            KuzelnaProgramKategorieEditable target = One(p => p.KuzelnaProgramKategorieId == item.KuzelnaProgramKategorieId);
            if (target != null)
            {
                All(refreshDb).Remove(target);
            }
        }
    }
}