namespace SlavojMVC4_1.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using SlavojMVC4_1.Models;

    public class KuzelnaProgramSluzbySessionRepository
    {
        public static IList<KuzelnaProgramSluzbaEditable> All(int KuzelnaProgramId, bool refreshDb = false)
        {
            IList<KuzelnaProgramSluzbaEditable> result = (IList<KuzelnaProgramSluzbaEditable>)HttpContext.Current.Session["KuzelnaProgramSluzby"];
            if (refreshDb) result = null;
            if (result == null)
            {

                HttpContext.Current.Session["KuzelnaProgramSluzby"] = result =
                    //                    (from item in new SlavojDBContainer().UserCleni.Find(VysledekId).UserProfile.webpages_UsersInRoles
                    (from item in new SlavojDBContainer().KuzelnaProgramSluzby
                     select new KuzelnaProgramSluzbaEditable
                     {
                         KuzelnaProgramSluzbaId = item.KuzelnaProgramSluzbaId,
                         KuzelnaProgramId = item.KuzelnaProgramId,
                         ClenId = item.ClenId,
                         SluzbaDatumCasOd = item.SluzbaDatumCasOd,
                         SluzbaDatumCasDo = item.SluzbaDatumCasDo
                         

                     }
                     )
                     .Where(w => w.KuzelnaProgramId == KuzelnaProgramId)
                     .ToList();
            }
            return result;
        }
        public static KuzelnaProgramSluzbaEditable One(Func<KuzelnaProgramSluzbaEditable, bool> predicate, int KuzelnaProgramId, bool refreshDb = false)
        {
            return All(KuzelnaProgramId, refreshDb).Where(predicate).FirstOrDefault();
        }

        public static void Insert(KuzelnaProgramSluzbaEditable item, int KuzelnaProgramId, bool refreshDb = false)
        {

            KuzelnaProgramSluzbaEditable target = One(p => p.KuzelnaProgramSluzbaId == item.KuzelnaProgramSluzbaId, KuzelnaProgramId, refreshDb);
            if (target == null)
            {
                All(KuzelnaProgramId, refreshDb).Insert(0, item);
            }

        }

        public static void Update(KuzelnaProgramSluzbaEditable item, int KuzelnaProgramId, bool refreshDb = false)
        {

            KuzelnaProgramSluzbaEditable target = One(p => p.KuzelnaProgramSluzbaId == item.KuzelnaProgramSluzbaId, KuzelnaProgramId, refreshDb);
            if (target != null)
            {
                target.KuzelnaProgramSluzbaId = item.KuzelnaProgramSluzbaId;
                target.KuzelnaProgramId = item.KuzelnaProgramId;
                target.ClenId = item.ClenId;
                target.SluzbaDatumCasOd = item.SluzbaDatumCasOd;
                target.SluzbaDatumCasDo = item.SluzbaDatumCasDo;

            }

        }

        public static void Delete(KuzelnaProgramSluzbaEditable item, int KuzelnaProgramId, bool refreshDb = false)
        {
            KuzelnaProgramSluzbaEditable target = One(p => p.KuzelnaProgramSluzbaId == item.KuzelnaProgramSluzbaId, KuzelnaProgramId, refreshDb);
            if (target != null)
            {
                All(KuzelnaProgramId, refreshDb).Remove(target);
            }
        }


    }
}