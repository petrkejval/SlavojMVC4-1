using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SlavojMVC4_1.Models;

namespace SlavojMVC4_1.Infrastructure
{
    //public static class Role
    //{

    //    public static bool IsRole(int UserId, string RoleName)
    //    {
    //        SlavojDBContainer db = new SlavojDBContainer();
    //        var user = db.UserProfiles.Find(UserId);
    //        var userrole = user.webpages_Roles.Where(x => x.RoleName == RoleName);
    //        if (userrole.Count() > 0)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }

    //    }

    //}
    public static class Tools
    {
        public static string GetFullName(string titulPred, string jmeno, string prijmeni, string titulZa)
        {
            string result = titulPred;

            if (result != null) { result = result + " "; }
            result = result + jmeno;

            if (result != null) { result = result + " "; }
            result = result + prijmeni;

            if (result != null) { result = result + " "; }
            result = result + titulZa;

            return result;
        }
    }
}