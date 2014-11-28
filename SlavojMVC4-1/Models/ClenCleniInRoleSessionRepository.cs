
namespace SlavojMVC4_1.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using SlavojMVC4_1.Models;

    public class ClenCleniInRoleSessionRepository
    {
        public static IList<ClenCleniInRoleEditable> All(int clenId, bool refreshDb = false)
        {
            IList<ClenCleniInRoleEditable> result = (IList<ClenCleniInRoleEditable>)HttpContext.Current.Session["ClenCleniInRole"];
            if (refreshDb) result = null;
            if (result == null)
            {

                HttpContext.Current.Session["ClenCleniInRole"] = result =
                    //                    (from item in new SlavojDBContainer().UserCleni.Find(userId).UserProfile.webpages_UsersInRoles
                    (from item in new SlavojDBContainer().CleniInRoles
                     select new ClenCleniInRoleEditable
                     {
                         ClenInRolesId = item.ClenInRolesId,
                         ClenId = item.ClenId,
                         ClenRoleId = item.ClenRoleId

                     }
                     )
                     .Where(w => w.ClenId == clenId)
                     .ToList();
            }
            return result;
        }
        public static ClenCleniInRoleEditable One(Func<ClenCleniInRoleEditable, bool> predicate, int clenId, bool refreshDb = false)
        {
            return All(clenId, refreshDb).Where(predicate).FirstOrDefault();
        }

        public static void Insert(ClenCleniInRoleEditable item, int clenId, bool refreshDb = false)
        {

            ClenCleniInRoleEditable target = One(p => p.ClenInRolesId == item.ClenInRolesId, clenId, refreshDb);
            if (target == null)
            {
                All(clenId, refreshDb).Insert(0, item);
            }

        }

        public static void Update(ClenCleniInRoleEditable item, int clenId, bool refreshDb = false)
        {

            ClenCleniInRoleEditable target = One(p => p.ClenInRolesId == item.ClenInRolesId, clenId, refreshDb);
            if (target != null)
            {
                target.ClenInRolesId = item.ClenInRolesId;
                target.ClenId = item.ClenId;
                target.ClenRoleId = item.ClenRoleId;
            }

        }

        public static void Delete(ClenCleniInRoleEditable item, int clenId, bool refreshDb = false)
        {
            ClenCleniInRoleEditable target = One(p => p.ClenInRolesId == item.ClenInRolesId, clenId, refreshDb);
            if (target != null)
            {
                All(clenId, refreshDb).Remove(target);
            }
        }
    }
}