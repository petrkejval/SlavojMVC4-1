namespace SlavojMVC4_1.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using SlavojMVC4_1.Models;
    using WebMatrix.WebData;


    public class SessionCleniRepository
    {

        public static IList<EditableClen> All(bool refreshDb = false)
        {
            IList<EditableClen> result = (IList<EditableClen>)HttpContext.Current.Session["Cleni"];
            if (refreshDb) result = null;
            DateTime? MyNullableDate = null;
            int? MyNullableInt = null;
            if (result == null)
            {

                using (var db = new SlavojDBContainer())
                {
                   int userId = WebSecurity.CurrentUserId;
                   var userclen = db.UserCleni.Where(w => w.UserId == userId).FirstOrDefault() ?? null;
                   //var userclen = db.UserCleni.Find(userId) ?? null;
                   int clenId = -1; 
                   if (userclen != null)
                   {
                      clenId = userclen.ClenId;
                   }
                   var userprofiles = db.UserProfiles.Find(userId).webpages_UsersInRoles.FirstOrDefault(a => a.webpages_Roles.RoleName == "admin" || a.webpages_Roles.RoleName == "superuser");
                   if (userprofiles == null)
                   {
                       HttpContext.Current.Session["Cleni"] = result =
                           (from clen in db.Cleni
                            select new EditableClen
                            {

                                ClenId = clen.ClenId,
                                TitulPred = clen.TitulPred,
                                Jmeno = clen.Jmeno,
                                Prijmeni = clen.Prijmeni,
                                TitulZa = clen.TitulZa,
                                JeClen = clen.JeClen,
                                RodneCislo = clen.RodneCislo,
                                DatumNarozeni = (clen.DatumNarozeni != null) ? clen.DatumNarozeni : MyNullableDate,
                                PohlaviId = (clen.PohlaviId != null) ? clen.PohlaviId : MyNullableInt,
                                PohlaviNazev = (clen.PohlavyNazev != null) ? clen.PohlavyNazev : null,
                                Vek = (clen.Vek != null) ? clen.Vek : MyNullableInt,
                                Fotka = clen.Fotka,


                                AdresaUlice = (clen.Adresa != null) ? clen.Adresa.Ulice : null,
                                AdresaCisloPopisne = (clen.Adresa != null) ? clen.Adresa.CisloPopisne : MyNullableInt,
                                AdresaObec = (clen.Adresa != null) ? clen.Adresa.Obec : null,
                                AdresaPsc = (clen.Adresa != null) ? clen.Adresa.Psc : MyNullableInt,

                                KontaktTelefon = (clen.Kontakt != null) ? clen.Kontakt.Telefon : null,
                                KontaktMail = (clen.Kontakt != null) ? clen.Kontakt.Mail : null,
                                KontaktWWW = (clen.Kontakt != null) ? clen.Kontakt.WWW : null,

                                RegistraceCisloRegistrace = (clen.Registrace != null) ? clen.Registrace.CisloRegistrace : MyNullableInt,
                                RegistracePlatnaDo = (clen.Registrace != null) ? clen.Registrace.PlatnaDo : MyNullableDate,

                                RozhodciCisloRegistrace = (clen.Rozhodci != null) ? clen.Rozhodci.CisloRegistrace : null,
                                RozhodciTrida = (clen.Rozhodci != null) ? clen.Rozhodci.Trida : null,
                                RozhodciPlatnaDo = (clen.Rozhodci != null) ? clen.Rozhodci.PlatnaDo : MyNullableDate,

                                TrenerCisloRegistrace = (clen.Trener != null) ? clen.Trener.CisloRegistrace : null,
                                TrenerTrida = (clen.Trener != null) ? clen.Trener.Trida : null,
                                TrenerPlatnaDo = (clen.Trener != null) ? clen.Trener.PlatnaDo : MyNullableDate

                            }
                           )
                           .Where(w => w.ClenId != 0 && w.ClenId == clenId)
                           .OrderBy(o => o.Prijmeni)
                           .ThenBy(o => o.Jmeno)
                           .ToList();

                   }
                   else
                   {
                       HttpContext.Current.Session["Cleni"] = result =
                           (from clen in new SlavojDBContainer().Cleni
                            select new EditableClen
                            {

                                ClenId = clen.ClenId,
                                TitulPred = clen.TitulPred,
                                Jmeno = clen.Jmeno,
                                Prijmeni = clen.Prijmeni,
                                TitulZa = clen.TitulZa,
                                JeClen = clen.JeClen,
                                RodneCislo = clen.RodneCislo,
                                DatumNarozeni = (clen.DatumNarozeni != null) ? clen.DatumNarozeni : MyNullableDate,
                                PohlaviId = (clen.PohlaviId != null) ? clen.PohlaviId : MyNullableInt,
                                PohlaviNazev = (clen.PohlavyNazev != null) ? clen.PohlavyNazev : null,
                                Vek = (clen.Vek != null) ? clen.Vek : MyNullableInt,
                                Fotka = clen.Fotka,


                                AdresaUlice = (clen.Adresa != null) ? clen.Adresa.Ulice : null,
                                AdresaCisloPopisne = (clen.Adresa != null) ? clen.Adresa.CisloPopisne : MyNullableInt,
                                AdresaObec = (clen.Adresa != null) ? clen.Adresa.Obec : null,
                                AdresaPsc = (clen.Adresa != null) ? clen.Adresa.Psc : MyNullableInt,

                                KontaktTelefon = (clen.Kontakt != null) ? clen.Kontakt.Telefon : null,
                                KontaktMail = (clen.Kontakt != null) ? clen.Kontakt.Mail : null,
                                KontaktWWW = (clen.Kontakt != null) ? clen.Kontakt.WWW : null,

                                RegistraceCisloRegistrace = (clen.Registrace != null) ? clen.Registrace.CisloRegistrace : MyNullableInt,
                                RegistracePlatnaDo = (clen.Registrace != null) ? clen.Registrace.PlatnaDo : MyNullableDate,

                                RozhodciCisloRegistrace = (clen.Rozhodci != null) ? clen.Rozhodci.CisloRegistrace : null,
                                RozhodciTrida = (clen.Rozhodci != null) ? clen.Rozhodci.Trida : null,
                                RozhodciPlatnaDo = (clen.Rozhodci != null) ? clen.Rozhodci.PlatnaDo : MyNullableDate,

                                TrenerCisloRegistrace = (clen.Trener != null) ? clen.Trener.CisloRegistrace : null,
                                TrenerTrida = (clen.Trener != null) ? clen.Trener.Trida : null,
                                TrenerPlatnaDo = (clen.Trener != null) ? clen.Trener.PlatnaDo : MyNullableDate

                            }
                           )
                           .Where(w => w.ClenId != 0)
                           .OrderBy(o => o.Prijmeni)
                           .ThenBy(o => o.Jmeno)
                           .ToList();
                       
                   }
                }

            }

            return result;
        }

        public static EditableClen One(Func<EditableClen, bool> predicate, bool refreshDb = false)
        {
            return All(refreshDb).Where(predicate).FirstOrDefault();
        }

        public static void Insert(EditableClen clen, bool refreshDb = false)
        {

            All(refreshDb).Insert(0, clen);
        }

        public static void Update(EditableClen clen, bool refreshDb = false)
        {

            EditableClen target = One(p => p.ClenId == clen.ClenId, refreshDb);
            if (target != null)
            {
                target.ClenId = clen.ClenId;
                target.TitulPred = clen.TitulPred;
                target.Jmeno = clen.Jmeno;
                target.Prijmeni = clen.Prijmeni;
                target.TitulZa = clen.TitulZa;
                target.JeClen = clen.JeClen;
                target.RodneCislo = clen.RodneCislo;
                target.DatumNarozeni = clen.DatumNarozeni;
                target.PohlaviId = clen.PohlaviId;
                target.PohlaviNazev = clen.PohlaviNazev;
                target.Vek = clen.Vek;
                target.Fotka = clen.Fotka;

                target.AdresaUlice = clen.AdresaUlice;
                target.AdresaCisloPopisne = clen.AdresaCisloPopisne;
                target.AdresaObec = clen.AdresaObec;
                target.AdresaPsc = clen.AdresaPsc;

                target.KontaktTelefon = clen.KontaktTelefon;
                target.KontaktMail = clen.KontaktMail;
                target.KontaktWWW = clen.KontaktWWW;

                target.RegistraceCisloRegistrace = clen.RegistraceCisloRegistrace;
                target.RegistracePlatnaDo = clen.RegistracePlatnaDo;

                target.RozhodciCisloRegistrace = clen.RozhodciCisloRegistrace;
                target.RozhodciTrida = clen.RozhodciTrida;
                target.RozhodciPlatnaDo = clen.RozhodciPlatnaDo;

                target.TrenerCisloRegistrace = clen.TrenerCisloRegistrace;
                target.TrenerTrida = clen.TrenerTrida;
                target.TrenerPlatnaDo = clen.TrenerPlatnaDo;
            }
            
        }

        public static void Delete(EditableClen clen, bool refreshDb = false)
        {
            EditableClen target = One(p => p.ClenId == clen.ClenId);
            if (target != null)
            {
                All(refreshDb).Remove(target);
            }
        }

    }
}