namespace SlavojMVC4_1.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using SlavojMVC4_1.Models;

    public class KlubySessionRepository
    {
        public static IList<KlubEditable> All(bool refreshDb = false)
        {
            IList<KlubEditable> result = (IList<KlubEditable>)HttpContext.Current.Session["Kluby"];
            if (refreshDb) result = null;
            if (result == null)
            {
                HttpContext.Current.Session["Kluby"] = result =
                    (from item in new SlavojDBContainer().Kluby
                     select new KlubEditable
                     {
                         KlubId = item.KlubId,
                         Nazev = item.Nazev,
                         IC = item.IC,
                         CisloUctu = item.CisloUctu,
                         Mail = item.Mail,
                         WWW = item.WWW,
                         KodKlubu = item.KodKlubu,
                         Image = item.Image,
                         WebPageId = item.WebPageId


                     }
                    ).ToList();

            }

            return result;
        }

        public static KlubEditable One(Func<KlubEditable, bool> predicate, bool refreshDb = false)
        {
            return All(refreshDb).Where(predicate).FirstOrDefault();
        }

        public static void Insert(KlubEditable item, bool refreshDb = false)
        {

            All(refreshDb).Insert(0, item);
        }

        public static void Update(KlubEditable item, bool refreshDb = false)
        {

            KlubEditable target = One(p => p.KlubId == item.KlubId, refreshDb);
            if (target != null)
            {
                target.KlubId = item.KlubId;
                target.Nazev = item.Nazev;
                target.IC = item.IC;
                target.CisloUctu = item.CisloUctu;
                target.Mail = item.Mail;
                target.WWW = item.WWW;
                target.KodKlubu = item.KodKlubu;
                target.Image = item.Image;
                target.WebPageId = item.WebPageId;
            }

        }

        public static void Delete(KlubEditable item, bool refreshDb = false)
        {
            KlubEditable target = One(p => p.KlubId == item.KlubId);
            if (target != null)
            {
                All(refreshDb).Remove(target);
            }
        }
    }
}