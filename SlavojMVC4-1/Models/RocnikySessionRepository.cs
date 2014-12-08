namespace SlavojMVC4_1.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using SlavojMVC4_1.Models;
    public class RocnikySessionRepository
    {
        public static IList<RocnikEditable> All(bool refreshDb = false)
        {
            IList<RocnikEditable> result = (IList<RocnikEditable>)HttpContext.Current.Session["Rocniky"];
            if (refreshDb) result = null;
            if (result == null)
            {
                HttpContext.Current.Session["Rocniky"] = result =
                    (from item in new SlavojDBContainer().Rocniky
                     select new RocnikEditable
                     {
                         RocnikId = item.RocnikId,
                         Nazev = item.Nazev,
                         JeVybrany = item.JeVybrany
                     }
                    ).ToList();

            }

            return result;
        }

        public static RocnikEditable One(Func<RocnikEditable, bool> predicate, bool refreshDb = false)
        {
            return All(refreshDb).Where(predicate).FirstOrDefault();
        }

        public static void Insert(RocnikEditable item, bool refreshDb = false)
        {

            All(refreshDb).Insert(0, item);
        }

        public static void Update(RocnikEditable item, bool refreshDb = false)
        {

            RocnikEditable target = One(p => p.RocnikId == item.RocnikId, refreshDb);
            if (target != null)
            {
                target.RocnikId = item.RocnikId;
                target.Nazev = item.Nazev;
                target.JeVybrany = item.JeVybrany;
            }

        }

        public static void Delete(RocnikEditable item, bool refreshDb = false)
        {
            RocnikEditable target = One(p => p.RocnikId == item.RocnikId);
            if (target != null)
            {
                All(refreshDb).Remove(target);
            }
        }
    }
}