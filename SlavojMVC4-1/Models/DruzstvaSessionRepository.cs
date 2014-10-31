namespace SlavojMVC4_1.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using SlavojMVC4_1.Models;

    public class DruzstvaSessionRepository
    {
        public static IList<DruzstvoEditable> All(bool refreshDb = false)
        {
            IList<DruzstvoEditable> result = (IList<DruzstvoEditable>)HttpContext.Current.Session["Druzstva"];
            if (refreshDb) result = null;
            if (result == null)
            {
                HttpContext.Current.Session["Druzstva"] = result =
                    (from item in new SlavojDBContainer().Druzstva
                     select new DruzstvoEditable
                        {
                            DruzstvoId = item.DruzstvoId,
                            Nazev = item.Nazev,
                            Pismeno = item.Pismeno,
                            SoutezId = item.SoutezId,
                            Existuje = item.Existuje,
                            VedouciId = item.VedouciId,
                            TrenerId = item.TrenerId,
                            Image = item.Image,
                            WebPageId = item.WebPageId
                        }
                    ).ToList();

            }

            return result;
        }

        public static DruzstvoEditable One(Func<DruzstvoEditable, bool> predicate, bool refreshDb = false)
        {
            return All(refreshDb).Where(predicate).FirstOrDefault();
        }

        public static void Insert(DruzstvoEditable soutez, bool refreshDb = false)
        {

            All(refreshDb).Insert(0, soutez);
            MainMenuSessionRepository.DruzstvaMenuRead(true);
        }

        public static void Update(DruzstvoEditable item, bool refreshDb = false)
        {

            DruzstvoEditable target = One(p => p.DruzstvoId == item.DruzstvoId, refreshDb);
            if (target != null)
            {
                target.DruzstvoId = item.DruzstvoId;
                target.Nazev = item.Nazev;
                target.Pismeno = item.Pismeno;
                target.SoutezId = item.SoutezId;
                target.Existuje = item.Existuje;
                target.VedouciId = item.VedouciId;
                target.TrenerId = item.TrenerId;
                target.Image = item.Image;
                target.WebPageId = item.WebPageId;
            }
            MainMenuSessionRepository.DruzstvaMenuRead(true);
            
        }

        public static void Delete(DruzstvoEditable item, bool refreshDb = false)
        {
            DruzstvoEditable target = One(p => p.DruzstvoId == item.DruzstvoId);
            if (target != null)
            {
                All(refreshDb).Remove(target);
            }
            MainMenuSessionRepository.DruzstvaMenuRead(true);
        }
    }
}