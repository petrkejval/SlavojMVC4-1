namespace SlavojMVC4_1.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using SlavojMVC4_1.Models;

    public class DisciplinyKategoriesSessionRepository
    {
        public static IList<DisciplinyKategorieEditable> All(bool refreshDb = false)
        {
            IList<DisciplinyKategorieEditable> result = (IList<DisciplinyKategorieEditable>)HttpContext.Current.Session["DisciplinyKategories"];
            if (refreshDb) result = null;
            if (result == null)
            {
                HttpContext.Current.Session["DisciplinyKategories"] = result =
                    (from item in new SlavojDBContainer().DisciplinyKategories
                     select new DisciplinyKategorieEditable
                     {
                         DisciplinyKategorieId = item.DisciplinyKategorieId,
                         Nazev = item.Nazev
                     }
                    ).ToList();

            }

            return result;
        }

        public static DisciplinyKategorieEditable One(Func<DisciplinyKategorieEditable, bool> predicate, bool refreshDb = false)
        {
            return All(refreshDb).Where(predicate).FirstOrDefault();
        }

        public static void Insert(DisciplinyKategorieEditable item, bool refreshDb = false)
        {

            All(refreshDb).Insert(0, item);
        }

        public static void Update(DisciplinyKategorieEditable item, bool refreshDb = false)
        {

            DisciplinyKategorieEditable target = One(p => p.DisciplinyKategorieId == item.DisciplinyKategorieId, refreshDb);
            if (target != null)
            {
                target.DisciplinyKategorieId = item.DisciplinyKategorieId;
                target.Nazev = item.Nazev;
            }

        }

        public static void Delete(DisciplinyKategorieEditable item, bool refreshDb = false)
        {
            DisciplinyKategorieEditable target = One(p => p.DisciplinyKategorieId == item.DisciplinyKategorieId);
            if (target != null)
            {
                All(refreshDb).Remove(target);
            }
        }
    }
}