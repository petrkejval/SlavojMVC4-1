namespace SlavojMVC4_1.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using SlavojMVC4_1.Models;

    public class VysledekVysledkyKolaSessionRepository
    {
        public static IList<VysledekVysledkyKolaEditable> All(int VysledekId, bool refreshDb = false)
        {
            IList<VysledekVysledkyKolaEditable> result = (IList<VysledekVysledkyKolaEditable>)HttpContext.Current.Session["VysledekVysledkyKola"];
            if (refreshDb) result = null;
            if (result == null)
            {

                HttpContext.Current.Session["VysledekVysledkyKola"] = result =
                    //                    (from item in new SlavojDBContainer().UserCleni.Find(VysledekId).UserProfile.webpages_UsersInRoles
                    (from item in new SlavojDBContainer().VysledkyKol
                     select new VysledekVysledkyKolaEditable
                     {
                         VysledkyKoloId = item.VysledkyKoloId,
                         VysledekId = item.VysledekId,
                         PorCisloKola = item.PorCisloKola,
                         Zpravodaj = item.Zpravodaj

                     }
                     )
                     .Where(w => w.VysledekId == VysledekId)
                     .ToList();
            }
            return result;
        }
        public static VysledekVysledkyKolaEditable One(Func<VysledekVysledkyKolaEditable, bool> predicate, int VysledekId, bool refreshDb = false)
        {
            return All(VysledekId, refreshDb).Where(predicate).FirstOrDefault();
        }

        public static void Insert(VysledekVysledkyKolaEditable item, int VysledekId, bool refreshDb = false)
        {

            VysledekVysledkyKolaEditable target = One(p => p.VysledkyKoloId == item.VysledkyKoloId, VysledekId, refreshDb);
            if (target == null)
            {
                All(VysledekId, refreshDb).Insert(0, item);
            }

        }

        public static void Update(VysledekVysledkyKolaEditable item, int VysledekId, bool refreshDb = false)
        {

            VysledekVysledkyKolaEditable target = One(p => p.VysledkyKoloId == item.VysledkyKoloId, VysledekId, refreshDb);
            if (target != null)
            {
                target.VysledkyKoloId = item.VysledkyKoloId;
                target.VysledekId = item.VysledekId;
                target.PorCisloKola = item.PorCisloKola;
                target.Zpravodaj = item.Zpravodaj;

            }

        }

        public static void Delete(VysledekVysledkyKolaEditable item, int VysledekId, bool refreshDb = false)
        {
            VysledekVysledkyKolaEditable target = One(p => p.VysledkyKoloId == item.VysledkyKoloId, VysledekId, refreshDb);
            if (target != null)
            {
                All(VysledekId, refreshDb).Remove(target);
            }
        }

        public static int MaxKolo(int VysledekId, bool refreshDb = false)
        {

            return All(VysledekId, refreshDb).Where(w => w.VysledekId == VysledekId).Max(m => m.PorCisloKola); 
        }

    }
}