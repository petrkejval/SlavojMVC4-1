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
    public static class ItemKde
    {
        public const string DomaciIVenkovniKuzelna = "Vše";
        public const string DomaciKuzelna = "Domácí kuželna";
        public const string VenkovniKuzelna = "Venkovní kuželna";

    }
    public static class ItemDruzstva
    {
        public const string Vse = " Vše";

    }

    public static class Tools
    {
        public const string itemKdeDomaciIVenkovniKuzelna = "Vše";
        public const string itemKdeDomaciKuzelna = "Domácí kuželna";
        public const string itemKdeVenkovniKuzelna = "Venkovní kuželna";
        public const string itemDruzstvaVse = " Vše";

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
        public static string GetHtmlPlatnoDo(DateTime platnoDo, int upozorneniPocDnu, string classNameUpozorneni, string classNameChyba)
        {
            string s = platnoDo.ToString("d");
            var dnes = DateTime.Now;
            var zaUpozorneniPocetDnu = dnes.AddDays(upozorneniPocDnu);
            if (dnes.CompareTo(platnoDo) == 0 && dnes.CompareTo(platnoDo) > 0)
            {
                s = String.Format("<div class=\"{0}\">{1}</div>", classNameChyba, platnoDo.ToString("d"));
            }
            else if (zaUpozorneniPocetDnu.CompareTo(platnoDo) > 0)
            {
                s = String.Format("<div class=\"{0}\">{1}</div>", classNameUpozorneni, platnoDo.ToString("d"));
            }
            return s;
        }
        public static DateTime GetFirstDate(DateTime datum)
        {
            DayOfWeek den = datum.DayOfWeek;
            DateTime d = DateTime.Now;
            DateTime datum1 = new DateTime(d.Year, d.Month, d.Day, datum.Hour, datum.Minute, datum.Second);
            DateTime datum2;

            for (int i = 0; i <= 6; i++)
            {
                datum2 = datum1.AddDays(i);
                if (den == datum2.DayOfWeek)
                {
                    return datum2;
                }
            }
            return default(DateTime);

        }
        public static IOrderedEnumerable<KuzelnaProgramZapas> ProgramNaKuzelne(int pocetNejblizsichDnu)
        {
            //var model = akceNaKuzelne.ToList().OrderBy(o => o.Id);
            var dnes = DateTime.Now;
            var dnesHranice = dnes.AddDays(pocetNejblizsichDnu);
            int KuzelnaProgramId = 0;

            //var programy =
            //      new SlavojDBContainer().KuzelnaProgramy.Where(w =>
            //      (
            //        (w.JeAktivni)
            //        &&
            //        (
            //            (w.ProgramDatumCasDo >= dnes)
            //        && (w.ProgramDatumCasDo <= dnesHranice)
            //        )
            //        &&
            //        (!w.OpakovatKazdyTyden)
            //      )
            //      ).ToList();
            var programy =
                  (from item in new SlavojDBContainer().KuzelnaProgramy.Where(w =>
                      (
                        (w.JeAktivni)
                        &&
                        (
                            (w.ProgramDatumCasDo >= dnes)
                        && (w.ProgramDatumCasDo <= dnesHranice)
                        )
                        &&
                        (!w.OpakovatKazdyTyden)
                      ))
                   select new KuzelnaProgramZapas
                   {
                       KuzelnaProgramId = item.KuzelnaProgramId,
                       ProgramNazev = item.ProgramNazev,
                       ProgramDatumCasOd = item.ProgramDatumCasOd,
                       ProgramDatumCasDo = item.ProgramDatumCasDo,
                       Poznamka = item.Poznamka,
                       OpakovatKazdyTyden = item.OpakovatKazdyTyden,
                       JeAktivni = item.JeAktivni,
                       JeProgram = true,
                       KuzelnaProgramSluzby = item.KuzelnaProgramSluzby
                   }
                ).ToList();

            var programyOpakovat =
                  new SlavojDBContainer().KuzelnaProgramy.Where(w =>
                  (
                    (w.JeAktivni)
                    &&
                    (w.ProgramDatumCasOd <= dnesHranice)
                    &&
                    (w.OpakovatKazdyTyden)
                    &&
                    (
                        (
                                (w.OpakovatDatumDo >= dnes)
                        )
                        ||
                        (w.OpakovatDatumDo == null)
                    )
                  )
                  );


            foreach (var item in programyOpakovat)
            {
                DateTime datumOd = GetFirstDate(item.ProgramDatumCasOd);
                DateTime datumDo = GetFirstDate(item.ProgramDatumCasDo);
                DateTime datumOpakovat;
                if (item.OpakovatDatumDo == null)
                {
                    datumOpakovat = dnesHranice;
                }
                else
                {
                    datumOpakovat = (DateTime)item.OpakovatDatumDo;
                }
                datumOpakovat = new DateTime(datumOpakovat.Year, datumOpakovat.Month, datumOpakovat.Day, 23, 59, 59);
                while ((datumOd <= DateTime.Now.AddDays(pocetNejblizsichDnu)) && (datumOd <= datumOpakovat))
                {
                    KuzelnaProgramId--;
                    var entity = new SlavojMVC4_1.Models.KuzelnaProgramZapas();
                    entity.KuzelnaProgramId = KuzelnaProgramId;
                    entity.ProgramNazev = item.ProgramNazev;
                    entity.ProgramDatumCasOd = datumOd;
                    entity.ProgramDatumCasDo = datumDo;
                    entity.Poznamka = item.Poznamka;
                    entity.OpakovatKazdyTyden = item.OpakovatKazdyTyden;
                    entity.OpakovatDatumDo = item.OpakovatDatumDo;
                    entity.JeAktivni = item.JeAktivni;
                    entity.JeProgram = true;

                    //.....................................................................................
                    if (item.KuzelnaProgramSluzby != null && item.KuzelnaProgramSluzby.Count > 0)
                    {
                        foreach (var sluzba in item.KuzelnaProgramSluzby)
                        {
                            entity.KuzelnaProgramSluzby.Add(sluzba);
                        }
                    }
                    //.....................................................................................
                    programy.Add(entity);

                    datumOd = datumOd.AddDays(7);
                    datumDo = datumDo.AddDays(7);
                }
            }

            var zapasy = new SlavojDBContainer().Zapasy.Where(w =>
                  (
                    (w.KuzelnaDomaci) && (w.ZapasDatumCasDo >= dnes) && (w.ZapasDatumCasDo <= dnesHranice)
                  )
                  );
            List<ZapasBarva> zapasyBarvy = new SlavojDBContainer().ZapasyBarvy.ToList();

            foreach (var item in zapasy)
            {
                KuzelnaProgramId--;
                var entity = new SlavojMVC4_1.Models.KuzelnaProgramZapas();
                entity.KuzelnaProgramId = KuzelnaProgramId;
                entity.ProgramNazev = item.Druzstvo1Nazev + " - " + item.Druzstvo2Nazev;
                entity.Druzstvo1Nazev = item.Druzstvo1Nazev;
                entity.Druzstvo2Nazev = item.Druzstvo2Nazev;
                entity.Druzstvo1Barva = GetBarva(item.Druzstvo1Nazev, zapasyBarvy);
                entity.Druzstvo2Barva = GetBarva(item.Druzstvo2Nazev, zapasyBarvy);
                entity.ProgramDatumCasOd = item.ZapasDatumCasOd;
                entity.ProgramDatumCasDo = item.ZapasDatumCasDo;
                entity.Poznamka = item.Poznamka;
                entity.JeAktivni = true;
                entity.JeProgram = false;
                //........................................................................................
                if (item.RozhodciId > 0)
                {
                    var sluzba = new SlavojMVC4_1.Models.KuzelnaProgramSluzba();
                    var clen = new SlavojDBContainer().Cleni.Where(w => w.ClenId == item.RozhodciId).FirstOrDefault();
                    sluzba.Clen = clen;
                    entity.KuzelnaProgramSluzby.Add(sluzba);
                }
                //........................................................................................

                programy.Add(entity);
            }


            var model = programy
                        .OrderBy(o => o.ProgramDatumCasOd).ThenBy(o => o.ProgramNazev);

            return model;
        }

        public static string GetBarva(string s, List<ZapasBarva> zapasyBarvy)
        {
            foreach (ZapasBarva item in zapasyBarvy)
            {
                if (s.IndexOf(item.DruzstvoNazev) > -1)
                {
                    return item.Barva;
                }
            }
            return null;
        }

    }
}