namespace SlavojMVC4_1.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using SlavojMVC4_1.Models;

    public class SessionKategorieSoutezeRepository
    {
        public static IList<EditableKategorieSouteze> All(bool refreshDb = false)
        {
            IList<EditableKategorieSouteze> result = (IList<EditableKategorieSouteze>)HttpContext.Current.Session["KategorieSoutezi"];
            if (refreshDb) result = null;
            if (result == null)
            {
                HttpContext.Current.Session["KategorieSoutezi"] = result =
                    (from kategorieSouteze in new SlavojDBContainer().KategorieSoutezi
                     select new EditableKategorieSouteze
                        {
                            KategorieSoutezeId = kategorieSouteze.KategorieSoutezeId,
                            Nazev = kategorieSouteze.Nazev
                        }
                    ).ToList();

            }

            return result;
        }

        public static EditableKategorieSouteze One(Func<EditableKategorieSouteze, bool> predicate, bool refreshDb = false)
        {
            return All(refreshDb).Where(predicate).FirstOrDefault();
        }

        public static void Insert(EditableKategorieSouteze kategorieSouteze, bool refreshDb = false)
        {

            All(refreshDb).Insert(0, kategorieSouteze);
        }

        public static void Update(EditableKategorieSouteze kategorieSouteze, bool refreshDb = false)
        {

            EditableKategorieSouteze target = One(p => p.KategorieSoutezeId == kategorieSouteze.KategorieSoutezeId, refreshDb);
            if (target != null)
            {
                target.KategorieSoutezeId = kategorieSouteze.KategorieSoutezeId;
                target.Nazev = kategorieSouteze.Nazev;
            }
            
        }

        public static void Delete(EditableKategorieSouteze kategorieSouteze, bool refreshDb = false)
        {
            EditableKategorieSouteze target = One(p => p.KategorieSoutezeId == kategorieSouteze.KategorieSoutezeId);
            if (target != null)
            {
                All(refreshDb).Remove(target);
            }
        }
    }
}