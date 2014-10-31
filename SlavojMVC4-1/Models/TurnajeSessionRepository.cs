namespace SlavojMVC4_1.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using SlavojMVC4_1.Models;

    public class TurnajeSessionRepository
    {
        public static IList<TurnajEditable> All(bool refreshDb = false)
        {
            IList<TurnajEditable> result = (IList<TurnajEditable>)HttpContext.Current.Session["Turnaje"];
            if (refreshDb) result = null;
            if (result == null)
            {
                HttpContext.Current.Session["Turnaje"] = result =
                    (from item in new SlavojDBContainer().Turnaje
                     select new TurnajEditable
                     {
                         TurnajId = item.TurnajId,
                         Nazev = item.Nazev,
                         ReditelTurnajeId = item.ReditelTurnajeId,
                         DatumOd = item.DatumOd,
                         DatumDo = item.DatumDo,
                         WebPageId = item.WebPageId,
                         Obrazek = item.Obrazek,
                         Existuje = item.Existuje
                     }
                    ).ToList();

            }

            return result;
        }

        public static TurnajEditable One(Func<TurnajEditable, bool> predicate, bool refreshDb = false)
        {
            return All(refreshDb).Where(predicate).FirstOrDefault();
        }

        public static void Insert(TurnajEditable soutez, bool refreshDb = false)
        {

            All(refreshDb).Insert(0, soutez);
            MainMenuSessionRepository.TurnajeMenuRead(true);
        }

        public static void Update(TurnajEditable item, bool refreshDb = false)
        {

            TurnajEditable target = One(p => p.TurnajId == item.TurnajId, refreshDb);
            if (target != null)
            {
                target.TurnajId = item.TurnajId;
                target.Nazev = item.Nazev;
                target.ReditelTurnajeId = item.ReditelTurnajeId;
                target.DatumOd = item.DatumOd;
                target.DatumDo = item.DatumDo;
                target.HtmlClanek = item.HtmlClanek;
                target.Obrazek = item.Obrazek;
                target.Existuje = item.Existuje;
            }
            MainMenuSessionRepository.TurnajeMenuRead(true);

        }

        public static void Delete(TurnajEditable item, bool refreshDb = false)
        {
            TurnajEditable target = One(p => p.TurnajId == item.TurnajId);
            if (target != null)
            {
                All(refreshDb).Remove(target);
            }
            MainMenuSessionRepository.TurnajeMenuRead(true);
        }
    }
}