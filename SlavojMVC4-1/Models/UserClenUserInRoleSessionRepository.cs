namespace SlavojMVC4_1.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using SlavojMVC4_1.Models;

    public class UserClenUserInRoleSessionRepository
    {
        public static IList<UserClenUserInRoleEditable> All(int userId, bool refreshDb = false)
        {
            IList<UserClenUserInRoleEditable> result = (IList<UserClenUserInRoleEditable>)HttpContext.Current.Session["UserClenUserInRole"];
            if (refreshDb) result = null;
            if (result == null)
            {
                
                HttpContext.Current.Session["UserClenUserInRole"] = result =
//                    (from item in new SlavojDBContainer().UserCleni.Find(userId).UserProfile.webpages_UsersInRoles
                    (from item in new SlavojDBContainer().webpages_UsersInRoles
                     select new UserClenUserInRoleEditable
                     {
                         UserInRoleId = item.UserInRoleId,
                         UserId = item.UserId,
                         RoleId = item.RoleId

                     }
                     )
                     .Where(w => w.UserId == userId)
                     .ToList();
            }
            return result;
        }
        public static UserClenUserInRoleEditable One(Func<UserClenUserInRoleEditable, bool> predicate, int userId, bool refreshDb = false)
        {
            return All(userId, refreshDb).Where(predicate).FirstOrDefault();
        }

        public static void Insert(UserClenUserInRoleEditable item, int userId, bool refreshDb = false)
        {

            UserClenUserInRoleEditable target = One(p => p.UserInRoleId == item.UserInRoleId, userId, refreshDb);
            if (target == null)
            {
                All(userId, refreshDb).Insert(0, item);
            }

        }

        public static void Update(UserClenUserInRoleEditable item, int userId, bool refreshDb = false)
        {

            UserClenUserInRoleEditable target = One(p => p.UserInRoleId == item.UserInRoleId, userId, refreshDb);
            if (target != null)
            {
                target.UserInRoleId = item.UserInRoleId;
                target.UserId = item.UserId;
                target.RoleId = item.RoleId;
            }

        }

        public static void Delete(UserClenUserInRoleEditable item, int userId, bool refreshDb = false)
        {
            UserClenUserInRoleEditable target = One(p => p.UserInRoleId == item.UserInRoleId, userId, refreshDb);
            if (target != null)
            {
                All(userId, refreshDb).Remove(target);
            }
        }
    }
}