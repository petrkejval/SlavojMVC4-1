
namespace SlavojMVC4_1.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using SlavojMVC4_1.Models;

    public class WebPagesSessionRepository
    {
        public static IList<WebPageEditable> All(bool refreshDb = false)
        {
            IList<WebPageEditable> result = (IList<WebPageEditable>)HttpContext.Current.Session["WebPages"];
            if (refreshDb) result = null;
            if (result == null)
            {
                HttpContext.Current.Session["WebPages"] = result =
                    (from item in new SlavojDBContainer().WebPages
                     select new WebPageEditable
                        {
                            WebPageId = item.WebPageId,
                            Nazev = item.Nazev,
                            HtmlText = item.HtmlText
                        }
                    )
                    .OrderBy(o => o.Nazev)
                    .ToList();

            }

            return result;
        }

        public static WebPageEditable One(Func<WebPageEditable, bool> predicate, bool refreshDb = false)
        {
            return All(refreshDb).Where(predicate).FirstOrDefault();
        }

        public static void Insert(WebPageEditable soutez, bool refreshDb = false)
        {

            All(refreshDb).Insert(0, soutez);
            MainMenuSessionRepository.DruzstvaMenuRead(true);
        }

        public static void Update(WebPageEditable item, bool refreshDb = false)
        {

            WebPageEditable target = One(p => p.WebPageId == item.WebPageId, refreshDb);
            if (target != null)
            {
                target.WebPageId = item.WebPageId;
                target.Nazev = item.Nazev;
                target.HtmlText = item.HtmlText;
            }
            MainMenuSessionRepository.DruzstvaMenuRead(true);
            
        }

        public static void Delete(WebPageEditable item, bool refreshDb = false)
        {
            WebPageEditable target = One(p => p.WebPageId == item.WebPageId);
            if (target != null)
            {
                All(refreshDb).Remove(target);
            }
            MainMenuSessionRepository.DruzstvaMenuRead(true);
        }
    }
}