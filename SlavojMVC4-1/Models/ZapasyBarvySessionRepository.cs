namespace SlavojMVC4_1.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using SlavojMVC4_1.Models;
    public class ZapasyBarvySessionRepository
    {
        public static IList<ZapasBarvaEditable> All(bool refreshDb = false)
        {
            IList<ZapasBarvaEditable> result = (IList<ZapasBarvaEditable>)HttpContext.Current.Session["ZapasyBarvy"];
            if (refreshDb) result = null;
            if (result == null)
            {
                HttpContext.Current.Session["ZapasyBarvy"] = result =
                    (from item in new SlavojDBContainer().ZapasyBarvy
                     select new ZapasBarvaEditable
                     {
                         ZapasBarvaId = item.ZapasBarvaId,
                         DruzstvoNazev = item.DruzstvoNazev,
                         Barva = item.Barva
                     }
                    ).ToList();

            }

            return result;
        }

        public static ZapasBarvaEditable One(Func<ZapasBarvaEditable, bool> predicate, bool refreshDb = false)
        {
            return All(refreshDb).Where(predicate).FirstOrDefault();
        }

        public static void Insert(ZapasBarvaEditable item, bool refreshDb = false)
        {

            All(refreshDb).Insert(0, item);
        }

        public static void Update(ZapasBarvaEditable item, bool refreshDb = false)
        {

            ZapasBarvaEditable target = One(p => p.ZapasBarvaId == item.ZapasBarvaId, refreshDb);
            if (target != null)
            {
                target.ZapasBarvaId = item.ZapasBarvaId;
                target.DruzstvoNazev = item.DruzstvoNazev;
                target.Barva = item.Barva;
            }

        }

        public static void Delete(ZapasBarvaEditable item, bool refreshDb = false)
        {
            ZapasBarvaEditable target = One(p => p.ZapasBarvaId == item.ZapasBarvaId);
            if (target != null)
            {
                All(refreshDb).Remove(target);
            }
        }
    }
}