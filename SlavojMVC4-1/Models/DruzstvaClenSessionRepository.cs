namespace SlavojMVC4_1.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using SlavojMVC4_1.Models;

    public class DruzstvaClenSessionRepository
    {
        public static IList<DruzstvaClenEditable> All(int druzstvoId, bool refreshDb = false)
        {
            IList<DruzstvaClenEditable> result = (IList<DruzstvaClenEditable>)HttpContext.Current.Session["DruzstvaClen"];
            if (refreshDb) result = null;
            if (result == null)
            {
                HttpContext.Current.Session["DruzstvaClen"] = result =
                    (from item in new SlavojDBContainer().Druzstva.Find(druzstvoId).DruzstvoCleni
                     select new DruzstvaClenEditable
                     {
                         DruzstvoClenId = item.DruzstvoClenId,
                         DruzstvoId = druzstvoId,
                         ClenId = item.ClenId

                     }
                     ).ToList();
            }
            return result;
        }
        public static DruzstvaClenEditable One(Func<DruzstvaClenEditable, bool> predicate, int druzstvoId, bool refreshDb = false)
        {
            return All(druzstvoId, refreshDb).Where(predicate).FirstOrDefault();
        }

        public static void Insert(DruzstvaClenEditable item, int druzstvoId, bool refreshDb = false)
        {

            DruzstvaClenEditable target = One(p => p.DruzstvoClenId == item.DruzstvoClenId, druzstvoId, refreshDb);
            if (target == null)
            {
                All(druzstvoId, refreshDb).Insert(0, item);
            }
            
        }

        public static void Update(DruzstvaClenEditable item, int druzstvoId, bool refreshDb = false)
        {

            DruzstvaClenEditable target = One(p => p.DruzstvoClenId == item.DruzstvoClenId, druzstvoId, refreshDb);
            if (target != null)
            {
                target.DruzstvoClenId = item.DruzstvoClenId;
                target.DruzstvoId = item.DruzstvoId;
                target.ClenId = item.ClenId;
            }

        }

        public static void Delete(DruzstvaClenEditable item, int druzstvoId, bool refreshDb = false)
        {
            DruzstvaClenEditable target = One(p => p.DruzstvoClenId == item.DruzstvoClenId, druzstvoId, refreshDb);
            if (target != null)
            {
                All(druzstvoId, refreshDb).Remove(target);
            }
        }

    }
}