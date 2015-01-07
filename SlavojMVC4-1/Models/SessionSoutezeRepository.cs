namespace SlavojMVC4_1.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using SlavojMVC4_1.Models;

    public class SessionSoutezeRepository
    {
        public static IList<EditableSoutez> All(bool refreshDb = false)
        {
            IList<EditableSoutez> result = (IList<EditableSoutez>)HttpContext.Current.Session["Souteze"];
            if (refreshDb) result = null;
            if (result == null)
            {
                HttpContext.Current.Session["Souteze"] = result =
                    (from soutez in new SlavojDBContainer().Souteze
                     select new EditableSoutez
                        {
                            SoutezId = soutez.SoutezId,
                            Nazev = soutez.Nazev,
                            KategorieSoutezeId = soutez.KategorieSoutezeId,
                            DisciplinaId = soutez.DisciplinaId,
                            MinPocetHracu = soutez.MinPocetHracu,
                            PocetNutnychDrah = soutez.PocetNutnychDrah
                        }
                    ).ToList();

            }

            return result;
        }

        public static EditableSoutez One(Func<EditableSoutez, bool> predicate, bool refreshDb = false)
        {
            return All(refreshDb).Where(predicate).FirstOrDefault();
        }

        public static void Insert(EditableSoutez soutez, bool refreshDb = false)
        {

            All(refreshDb).Insert(0, soutez);
            MainMenuSessionRepository.DruzstvaMenuRead(true);
        }

        public static void Update(EditableSoutez soutez, bool refreshDb = false)
        {

            EditableSoutez target = One(p => p.SoutezId == soutez.SoutezId, refreshDb);
            if (target != null)
            {
                target.SoutezId = soutez.SoutezId;
                target.Nazev = soutez.Nazev;
                target.KategorieSoutezeId = soutez.KategorieSoutezeId;
                target.DisciplinaId = soutez.DisciplinaId;
                target.MinPocetHracu = soutez.MinPocetHracu;
                target.PocetNutnychDrah = soutez.PocetNutnychDrah;
            }
            MainMenuSessionRepository.DruzstvaMenuRead(true);
            
        }

        public static void Delete(EditableSoutez soutez, bool refreshDb = false)
        {
            EditableSoutez target = One(p => p.SoutezId == soutez.SoutezId);
            if (target != null)
            {
                All(refreshDb).Remove(target);
            }
            MainMenuSessionRepository.DruzstvaMenuRead(true);
        }
    }
}