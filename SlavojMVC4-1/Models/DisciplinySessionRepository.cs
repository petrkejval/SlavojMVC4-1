namespace SlavojMVC4_1.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using SlavojMVC4_1.Models;

    public class DisciplinySessionRepository
    {
        public static IList<DisciplinaEditable> All(bool refreshDb = false)
        {
            IList<DisciplinaEditable> result = (IList<DisciplinaEditable>)HttpContext.Current.Session["Discipliny"];
            if (refreshDb) result = null;
            if (result == null)
            {
                HttpContext.Current.Session["Discipliny"] = result =
                    (from item in new SlavojDBContainer().Discipliny
                     select new DisciplinaEditable
                     {
                         DisciplinaId = item.DisciplinaId,
                         PocetHodu = item.PocetHodu,
                         DisciplinyKategorieId = item.DisciplinyKategorieId,
                         Popis = item.Popis
                     }
                    ).ToList();

            }

            return result;
        }

        public static DisciplinaEditable One(Func<DisciplinaEditable, bool> predicate, bool refreshDb = false)
        {
            return All(refreshDb).Where(predicate).FirstOrDefault();
        }

        public static void Insert(DisciplinaEditable item, bool refreshDb = false)
        {

            All(refreshDb).Insert(0, item);
        }

        public static void Update(DisciplinaEditable item, bool refreshDb = false)
        {

            DisciplinaEditable target = One(p => p.DisciplinaId == item.DisciplinaId, refreshDb);
            if (target != null)
            {
                target.DisciplinaId = item.DisciplinaId;
                target.PocetHodu = item.PocetHodu;
                target.DisciplinyKategorieId = item.DisciplinyKategorieId;
                target.Popis = item.Popis;
            }

        }

        public static void Delete(DisciplinaEditable item, bool refreshDb = false)
        {
            DisciplinaEditable target = One(p => p.DisciplinaId == item.DisciplinaId);
            if (target != null)
            {
                All(refreshDb).Remove(target);
            }
        }
    }
}