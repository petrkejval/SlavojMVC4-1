namespace SlavojMVC4_1.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using SlavojMVC4_1.Models;

    public class KuzelnySessionRepository
    {
        public static IList<KuzelnaEditable> All(bool refreshDb = false)
        {
            IList<KuzelnaEditable> result = (IList<KuzelnaEditable>)HttpContext.Current.Session["Kuzelny"];
            if (refreshDb) result = null;
            if (result == null)
            {
                HttpContext.Current.Session["Kuzelny"] = result =
                    (from item in new SlavojDBContainer().Kuzelny
                     select new KuzelnaEditable
                     {
                         KuzelnaId = item.KuzelnaId,
                         Ulice = item.Ulice,
                         CisloPopisne = item.CisloPopisne,
                         Obec = item.Obec,
                         Psc = item.Psc,
                         KolaudacePlatnaDo = item.KolaudacePlatnaDo,
                         Mapa = item.Mapa,
                         StreeView = item.StreeView,
                         GPS = item.GPS,
                         WebPageId = item.WebPageId,
                         Image = item.Image,
                         LinkKuzelkyCz = item.LinkKuzelkyCz


                     }
                    ).ToList();

            }

            return result;
        }

        public static KuzelnaEditable One(Func<KuzelnaEditable, bool> predicate, bool refreshDb = false)
        {
            return All(refreshDb).Where(predicate).FirstOrDefault();
        }

        public static void Insert(KuzelnaEditable soutez, bool refreshDb = false)
        {

            All(refreshDb).Insert(0, soutez);
        }

        public static void Update(KuzelnaEditable item, bool refreshDb = false)
        {

            KuzelnaEditable target = One(p => p.KuzelnaId == item.KuzelnaId, refreshDb);
            if (target != null)
            {
                target.KuzelnaId = item.KuzelnaId;
                target.Ulice = item.Ulice;
                target.CisloPopisne = item.CisloPopisne;
                target.Obec = item.Obec;
                target.Psc = item.Psc;
                target.KolaudacePlatnaDo = item.KolaudacePlatnaDo;
                target.Mapa = item.Mapa;
                target.StreeView = item.StreeView;
                target.GPS = item.GPS;
                target.WebPageId = item.WebPageId;
                target.Image = item.Image;
                target.LinkKuzelkyCz = item.LinkKuzelkyCz;

            }

        }

        public static void Delete(KuzelnaEditable item, bool refreshDb = false)
        {
            KuzelnaEditable target = One(p => p.KuzelnaId == item.KuzelnaId);
            if (target != null)
            {
                All(refreshDb).Remove(target);
            }
        }
    }
}