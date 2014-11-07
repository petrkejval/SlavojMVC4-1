namespace SlavojMVC4_1.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using SlavojMVC4_1.Models;

    public class UserCleniSessionRepository
    {
        public static IList<UserClenEditable> All(bool refreshDb = false)
        {
            IList<UserClenEditable> result = (IList<UserClenEditable>)HttpContext.Current.Session["UserCleni"];
            if (refreshDb) result = null;
            if (result == null)
            {
                HttpContext.Current.Session["UserCleni"] = result =
                    (from item in new SlavojDBContainer().UserCleni
                     select new UserClenEditable
                     {
                         UserClenId = item.UserClenId,
                         UserId = item.UserId,
                         ClenId = item.ClenId
                     }
                    ).ToList();

            }

            return result;
        }

        public static UserClenEditable One(Func<UserClenEditable, bool> predicate, bool refreshDb = false)
        {
            return All(refreshDb).Where(predicate).FirstOrDefault();
        }

        public static void Insert(UserClenEditable soutez, bool refreshDb = false)
        {

            All(refreshDb).Insert(0, soutez);
        }

        public static void Update(UserClenEditable item, bool refreshDb = false)
        {

            UserClenEditable target = One(p => p.UserClenId == item.UserClenId, refreshDb);
            if (target != null)
            {
                target.UserClenId = item.UserClenId;
                target.UserId = item.UserId;
                target.ClenId = item.ClenId;
            }

        }

        public static void Delete(UserClenEditable item, bool refreshDb = false)
        {
            UserClenEditable target = One(p => p.UserClenId == item.UserClenId);
            if (target != null)
            {
                All(refreshDb).Remove(target);
            }
        }
    }
}