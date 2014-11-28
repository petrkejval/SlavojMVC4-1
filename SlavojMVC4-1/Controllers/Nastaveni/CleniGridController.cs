using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.Objects;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Telerik.Web.Mvc.UI;
using Telerik.Web.Mvc;
using System.Globalization;
using SlavojMVC4_1.Models;
using SlavojMVC4_1.Filters;
using System.Data.Entity.Infrastructure;

namespace SlavojMVC4_1.Controllers
{
    public class CleniGridController : Controller
    {


        [SourceCodeFile("EditableClen (model)", "~/Models/EditableClen.cs")]
        [SourceCodeFile("SessionClenRepository", "~/Models/SessionClenRepository.cs")]
        [SourceCodeFile("Date.cshtml (editor)", "~/Views/Shared/EditorTemplates/Date.cshtml")]
        [SourceCodeFile("EditableClen.cshtml", "~/Views/Shared/EditorTemplates/EditableClen.cshtml")]
        public ActionResult EditingAjax()
        {
            return View();
        }


        [GridAction]
        public ActionResult _SelectAjaxEditing()
        {
            return View(new GridModel(SessionCleniRepository.All(true)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        [HandleError]
        public ActionResult _SaveAjaxEditing(int id, FormCollection form)
        {
            EditableClen clen = SessionCleniRepository.One(p => p.ClenId == id);

            //Pokud je Datum null není klíč přidán do seznamu form.AllKey, což má za následek, že TryUpdateModel(clen) pole Datum neaktualizuje
            //To lze obejít tak, že do seznamu form.AllKey přídám položku Datum manuálně a pak použiju form jako parametr příkazu TryUpdateModel(clen, form) 
            if (form.AllKeys.FirstOrDefault(p => p.ToString() == "RegistraceDatumNarozeni") == null) form.Add("RegistraceDatumNarozeni", String.Empty);
            if (form.AllKeys.FirstOrDefault(p => p.ToString() == "RegistracePlatnaDo") == null) form.Add("RegistracePlatnaDo", String.Empty);
            if (form.AllKeys.FirstOrDefault(p => p.ToString() == "RozhodciPlatnaDo") == null) form.Add("RozhodciPlatnaDo", String.Empty);
            if (form.AllKeys.FirstOrDefault(p => p.ToString() == "TrenerPlatnaDo") == null) form.Add("TrenerPlatnaDo", String.Empty);
            TryUpdateModel(clen, form ); 
            //.........................................................................................................................................................
            if (ModelState.IsValid)
            {
                using (var db = new SlavojDBContainer())
                {

                    var entity = db.Cleni.Find(clen.ClenId);
                    if (entity == null)
                    {
                        entity = new Clen();
                        entity.ClenId = clen.ClenId;
                    }

                    entity.TitulPred = clen.TitulPred;
                    entity.Jmeno = clen.Jmeno;
                    entity.Prijmeni = clen.Prijmeni;
                    entity.TitulZa = clen.TitulZa;
                    entity.JeClen = clen.JeClen;
                    entity.RodneCislo = clen.RodneCislo;
                    entity.Fotka = clen.Fotka;


                    //Nastaví všechna modifikovaná pole jako modifikovaná
                    db.SetModifyFields(entity);

                    //Ukázka jak lze také nastavit, že je pole modifikované
                    //db.Entry(entity).Property(p => p.Prijmeni).IsModified = true;

                    //Nepoužiju Attach protože pracuji s objektem v databázi a Attach nastavu všem položkám v objektu entity State na Unchanged
                    //db.Cleni.Attach(entity);
                    //db.Entry(entity).State = EntityState.Modified;
                    //.............................................................................................................
                    if (clen.IsAdresa())
                        {
                        if (entity.Adresa == null)
                        {
                            entity.Adresa = new Adresa();
                            entity.Adresa.ClenId = entity.ClenId;
                            entity.Adresa.Ulice = clen.AdresaUlice;
                            if (clen.AdresaCisloPopisne != null)
                                entity.Adresa.CisloPopisne = clen.AdresaCisloPopisne ?? default(int);
                            entity.Adresa.Obec = clen.AdresaObec;
                            if (clen.AdresaPsc != null)
                                entity.Adresa.Psc = clen.AdresaPsc ?? default(int);

                            db.Adresy.Add(entity.Adresa);
                        }
                        else
                        {
                            entity.Adresa.Ulice = clen.AdresaUlice;
                            if (clen.AdresaCisloPopisne != null)
                                entity.Adresa.CisloPopisne = clen.AdresaCisloPopisne ?? default(int);
                            entity.Adresa.Obec = clen.AdresaObec;
                            if (clen.AdresaPsc != null)
                                entity.Adresa.Psc = clen.AdresaPsc ?? default(int);

                            //Nastaví všechna modifikovaná pole jako modifikovaná
                            db.SetModifyFields(entity.Adresa);

                            //Nepoužiju Attach protože pracuji s objektem v databázi a Attach nastavu všem položkám v objektu entity.Adresa State na Unchanged
                            //db.Adresy.Attach(entity.Adresa);
                            //db.Entry(entity.Adresa).State = EntityState.Modified;


                        }
                    }
                    else
                    {
                        Adresa adresa = db.Adresy.Find(entity.ClenId);
                        if (adresa != null)
                        {
                            db.Adresy.Remove(adresa);

                        }
                    }
                    //.............................................................................................................
                    if (clen.IsKontakt())
                    {
                        if (entity.Kontakt == null)
                        {
                            entity.Kontakt = new Kontakt();
                            entity.Kontakt.ClenId = entity.ClenId;
                            entity.Kontakt.Telefon = clen.KontaktTelefon;
                            entity.Kontakt.Mail = clen.KontaktMail;
                            entity.Kontakt.WWW = clen.KontaktWWW;

                            db.Kontakty.Add(entity.Kontakt);
                        }
                        else
                        {
                            entity.Kontakt.Telefon = clen.KontaktTelefon;
                            entity.Kontakt.Mail = clen.KontaktMail;
                            entity.Kontakt.WWW = clen.KontaktWWW;

                            //Nastaví všechna modifikovaná pole jako modifikovaná
                            db.SetModifyFields(entity.Kontakt);

                            //Nepoužiju Attach protože pracuji s objektem v databázi a Attach nastavu všem položkám v objektu entity.Kontakt State na Unchanged
                            //db.Kontakty.Attach(entity.Kontakt);
                            //db.Entry(entity.Kontakt).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        Kontakt kontakt = db.Kontakty.Find(entity.ClenId);
                        if (kontakt != null)
                        {
                            db.Kontakty.Remove(kontakt);
                        }
                    }
                    //.............................................................................................................
                    if (clen.IsRegistrace())
                    {
                        if (entity.Registrace == null)
                        {
                            entity.Registrace = new Registrace();
                            entity.Registrace.ClenId = entity.ClenId;
                            if (clen.RegistraceCisloRegistrace != null)
                                entity.Registrace.CisloRegistrace = clen.RegistraceCisloRegistrace ?? default(int);
                            if (clen.RegistracePlatnaDo != null) 
                                entity.Registrace.PlatnaDo = clen.RegistracePlatnaDo ?? default(DateTime);

                            db.Registraces.Add(entity.Registrace);
                        }
                        else
                        {
                            if (clen.RegistraceCisloRegistrace != null)
                                entity.Registrace.CisloRegistrace = clen.RegistraceCisloRegistrace ?? default(int);
                            if (clen.RegistracePlatnaDo != null)
                                entity.Registrace.PlatnaDo = clen.RegistracePlatnaDo ?? default(DateTime);

                            //Nastaví všechna modifikovaná pole jako modifikovaná
                            db.SetModifyFields(entity.Registrace);

                            //Nepoužiju Attach protože pracuji s objektem v databázi a Attach nastavu všem položkám v objektu entity.Registrace State na Unchanged
                            //db.Registraces.Attach(entity.Registrace);
                            //db.Entry(entity.Registrace).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        Registrace registrace = db.Registraces.Find(entity.ClenId);
                        if (registrace != null)
                        {
                            db.Registraces.Remove(registrace);
                        }
                    }
                    //.............................................................................................................
                    if (clen.IsRozhodci())
                    {
                        if (entity.Rozhodci == null)
                        {
                            entity.Rozhodci = new Rozhodci();
                            entity.Rozhodci.ClenId = entity.ClenId;
                            entity.Rozhodci.CisloRegistrace = clen.RozhodciCisloRegistrace;
                            entity.Rozhodci.Trida = clen.RozhodciTrida;
                            if (clen.RozhodciPlatnaDo != null)
                                entity.Rozhodci.PlatnaDo = clen.RozhodciPlatnaDo ?? default(DateTime);

                            db.Rozhodcis.Add(entity.Rozhodci);
                        }
                        else
                        {
                            entity.Rozhodci.CisloRegistrace = clen.RozhodciCisloRegistrace;
                            entity.Rozhodci.Trida = clen.RozhodciTrida;
                            if (clen.RozhodciPlatnaDo != null)
                                entity.Rozhodci.PlatnaDo = clen.RozhodciPlatnaDo ?? default(DateTime);

                            //Nastaví všechna modifikovaná pole jako modifikovaná
                            db.SetModifyFields(entity.Rozhodci);

                            //Nepoužiju Attach protože pracuji s objektem v databázi a Attach nastavu všem položkám v objektu entity.Rozhodci State na Unchanged
                            //db.Rozhodcis.Attach(entity.Rozhodci);
                            //db.Entry(entity.Rozhodci).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        Rozhodci rozhodci = db.Rozhodcis.Find(entity.ClenId);
                        if (rozhodci != null)
                        {
                            db.Rozhodcis.Remove(rozhodci);
                        }
                    }
                    //.............................................................................................................
                    if (clen.IsTrener())
                    {
                        if (entity.Trener == null)
                        {
                            entity.Trener = new Trener();
                            entity.Trener.ClenId = entity.ClenId;
                            entity.Trener.CisloRegistrace = clen.TrenerCisloRegistrace;
                            entity.Trener.Trida = clen.TrenerTrida;
                            if (clen.TrenerPlatnaDo != null)
                                entity.Trener.PlatnaDo = clen.TrenerPlatnaDo ?? default(DateTime);

                            db.Treneri.Add(entity.Trener);
                        }
                        else
                        {
                            entity.Trener.CisloRegistrace = clen.TrenerCisloRegistrace;
                            entity.Trener.Trida = clen.TrenerTrida;
                            if (clen.TrenerPlatnaDo != null)
                                entity.Trener.PlatnaDo = clen.TrenerPlatnaDo ?? default(DateTime);

                            //Nastaví všechna modifikovaná pole jako modifikovaná
                            db.SetModifyFields(entity.Trener);

                            //Nepoužiju Attach protože pracuji s objektem v databázi a Attach nastavu všem položkám v objektu entity.Trener State na Unchanged
                            //db.Treners.Attach(entity.Trener);
                            //db.Entry(entity.Trener).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        Trener trener = db.Treneri.Find(entity.ClenId);
                        if (trener != null)
                        {
                            db.Treneri.Remove(trener);
                        }
                    }
                    //.................................................................................................................................
                    this.ModelState.Clear();
                    EfStatus status = db.SaveChangesWithValidation();
                    if (status.IsValid)
                    {
                        clen.PohlaviId = entity.PohlaviId;
                        clen.PohlaviNazev = entity.PohlavyNazev;
                        clen.Vek = entity.Vek;
                        clen.DatumNarozeni = entity.DatumNarozeni;
                        SessionCleniRepository.Update(clen);
                    }
                    else
                    {
                        AddModelStateError(status);
                    }

                }

            }
            return View(new GridModel(SessionCleniRepository.All()));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        [Authorize(Roles = ("admin"))]
        public ActionResult _InsertAjaxEditing()
        {
            //Create a new instance of the EditableProduct class.
            EditableClen clen = new EditableClen();


            //Perform model binding (fill the product properties and validate it).
            if (TryUpdateModel(clen))
            {
                if (ModelState.IsValid)
                {

                    using (var db = new SlavojDBContainer())
                    {
                        var entity = new SlavojMVC4_1.Models.Clen();

                        entity.TitulPred = clen.TitulPred;
                        entity.Jmeno = clen.Jmeno;
                        entity.Prijmeni = clen.Prijmeni;
                        entity.TitulZa = clen.TitulZa;
                        entity.JeClen = clen.JeClen;
                        entity.RodneCislo = clen.RodneCislo;
                        entity.Fotka = clen.Fotka;

                        // Add the entity
                        db.Cleni.Add(entity);
                        //.................................................................................................
                        if (clen.IsAdresa())
                        {
                            if (entity.Adresa == null)
                            {
                                entity.Adresa = new Adresa();
                            }
                            entity.Adresa.Ulice = clen.AdresaUlice;
                            if (clen.AdresaCisloPopisne != null)
                                entity.Adresa.CisloPopisne = clen.AdresaCisloPopisne ?? default(int);
                            entity.Adresa.Obec = clen.AdresaObec;
                            if (clen.AdresaPsc != null)
                                entity.Adresa.Psc = clen.AdresaPsc ?? default(int);
                            db.Adresy.Add(entity.Adresa);

                        }
                        //.................................................................................................
                        if (clen.IsKontakt())
                        {
                            if (entity.Kontakt == null)
                            {
                                entity.Kontakt = new Kontakt();
                            }
                            entity.Kontakt.Mail = clen.KontaktMail;
                            entity.Kontakt.Telefon = clen.KontaktTelefon;
                            entity.Kontakt.WWW = clen.KontaktWWW;
                            db.Kontakty.Add(entity.Kontakt);

                        }
                        //.................................................................................................
                        if (clen.IsRegistrace())
                        {
                            if (entity.Registrace == null)
                            {
                                entity.Registrace = new Registrace();
                            }
                            if (clen.RegistraceCisloRegistrace != null)
                                entity.Registrace.CisloRegistrace = clen.RegistraceCisloRegistrace ?? default(int);
                            if (clen.RegistracePlatnaDo != null)
                                entity.Registrace.PlatnaDo = clen.RegistracePlatnaDo ?? default(DateTime);

                            db.Registraces.Add(entity.Registrace);
                        }
                        //.................................................................................................
                        if (clen.IsRozhodci())
                        {
                            if (entity.Rozhodci == null)
                            {
                                entity.Rozhodci = new Rozhodci();
                            }
                            entity.Rozhodci.CisloRegistrace = clen.RozhodciCisloRegistrace;
                            entity.Rozhodci.Trida = clen.RozhodciTrida;
                            if (clen.RozhodciPlatnaDo != null)
                                entity.Rozhodci.PlatnaDo = clen.RozhodciPlatnaDo ?? default(DateTime);
                            db.Rozhodcis.Add(entity.Rozhodci);
                        }
                        //.................................................................................................
                        if (clen.IsTrener())
                        {
                            if (entity.Trener == null)
                            {
                                entity.Trener = new Trener();
                            }
                            entity.Trener.CisloRegistrace = clen.TrenerCisloRegistrace;
                            entity.Trener.Trida = clen.TrenerTrida;
                            if (clen.TrenerPlatnaDo != null)
                                entity.Trener.PlatnaDo = clen.TrenerPlatnaDo ?? default(DateTime);
                            db.Treneri.Add(entity.Trener);
                        }
                        //.................................................................................................
                        this.ModelState.Clear();
                        EfStatus status = db.SaveChangesWithValidation();
                        if (status.IsValid)
                        {
                            clen.ClenId = entity.ClenId;
                            clen.PohlaviId = entity.PohlaviId;
                            clen.PohlaviNazev = entity.PohlavyNazev;
                            clen.Vek = entity.Vek;
                            clen.DatumNarozeni = entity.DatumNarozeni;
                            SessionCleniRepository.Insert(clen);
                        }
                        else
                        {
                            AddModelStateError(status);
                        }

                    }

                }
            }

            //Rebind the grid
            return View(new GridModel(SessionCleniRepository.All()));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        [Authorize(Roles = ("admin") )]
        public ActionResult _DeleteAjaxEditing(int id)
        {
            //Find a customer with ProductID equal to the id action parameter
            EditableClen clen = SessionCleniRepository.One(p => p.ClenId == id);

            if (clen != null)
            {
                if (TryValidateModel(clen))
                {
                    using (var db = new SlavojDBContainer())
                    {
                        
                        //Smažu v db
                        var entity = db.Cleni.Find(clen.ClenId);
                        if (entity != null)
                        {
                            db.Cleni.Remove(entity);
                            this.ModelState.Clear();
                            EfStatus status = db.SaveChangesWithValidation();
                            if (!status.IsValid)
                            {
                                AddModelStateError(status);
                            }
                            else
                            {
                                SessionCleniRepository.Delete(clen);
                            }
                        }
                    }
                }
            }

            //Rebind the grid
            return View(new GridModel(SessionCleniRepository.All()));
        }

        //private void SetModifyFields(SlavojDBContainer db, object entity)
        //{

        //    foreach (string n in db.Entry(entity).CurrentValues.PropertyNames)
        //    {
        //        if (!Equals(db.Entry(entity).OriginalValues[n], db.Entry(entity).CurrentValues[n]))
        //        {
        //            //Nastaví, že je položka modifikována
        //            ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(entity).SetModifiedProperty(n);
        //        }
        //    }
        //}

        private void AddModelStateError(EfStatus status)
        {
            //Naplnit chyb z db.Cleni do this.ModelState
            for (int i = 0; i < status.EfErrors.Count; i++)
            {
                var error = status.EfErrors[i];
                string propertyName = status.EfErrors[i].MemberNames.FirstOrDefault();
                if (
                    (status.EfErrors[i].MemberNames.Count() > 0)
                    && (status.EfErrors[i].MemberNames.Contains("CisloRegistrace"))
                    && (status.EfErrors[i].MemberNames.Contains("Registrace"))
                    )
                {
                    propertyName = "RegistraceCisloRegistrace";
                }
                else if (
                    (status.EfErrors[i].MemberNames.Count() > 0)
                    && (status.EfErrors[i].MemberNames.Contains("CisloRegistrace"))
                    && (status.EfErrors[i].MemberNames.Contains("Rozhodci"))
                    )
                {
                    propertyName = "RozhodciCisloRegistrace";
                }
                else if (
                    (status.EfErrors[i].MemberNames.Count() > 0)
                    && (status.EfErrors[i].MemberNames.Contains("CisloRegistrace"))
                    && (status.EfErrors[i].MemberNames.Contains("Trener"))
                    )
                {
                    propertyName = "TrenerCisloRegistrace";
                }

                else if (propertyName == "Ulice" || propertyName == "CisloPopisne" || propertyName == "Obec" || propertyName == "Psc") { propertyName = "Adresa" + propertyName; }
                else if (propertyName == "Telefon" || propertyName == "Mail" || propertyName == "WWW") { propertyName = "Kontakt" + propertyName; }
                else if (propertyName == "CisloRegistrace" || propertyName == "PlatnaDo") { propertyName = "Registrace" + propertyName; }
                else if (propertyName == "CisloRegistrace" || propertyName == "Trida" || propertyName == "PlatnaDo") { propertyName = "Rozhodci" + propertyName; }
                else if (propertyName == "CisloRegistrace" || propertyName == "Trida" || propertyName == "PlatnaDo") { propertyName = "Trener" + propertyName; }

                propertyName = propertyName != null ? propertyName : string.Empty;
                this.ModelState.AddModelError(propertyName, error.ErrorMessage);

            }

        }

        //.......................................................................................................................................................................
        [GridAction]
        public ActionResult RoleSelect(int clenId)
        {
            return View(new GridModel(ClenCleniInRoleSessionRepository.All(clenId, true)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        [HandleError]
        public ActionResult RoleUpdate(int id, int clenId)
        {
            ClenCleniInRoleEditable item = ClenCleniInRoleSessionRepository.One(p => p.ClenInRolesId == id, clenId);
            var values = ModelState.Values;
            TryUpdateModel(item);
            //.........................................................................................................................................................
            if (ModelState.IsValid)
            {
                using (var db = new SlavojDBContainer())
                {
                    this.ModelState.Clear();
                    var entity = db.CleniInRoles.Find(item.ClenInRolesId);
                    if (entity != null)
                    {
                        entity.ClenRoleId = item.ClenRoleId;
                        //.................................................................................................
                        EfStatus status = db.SaveChangesWithValidation();
                        if (status.IsValid)
                        {
                            ClenCleniInRoleSessionRepository.Insert(item, clenId);
                        }
                        else
                        {
                            AddModelStateError(status);
                        }
                    }

                }

            }

            return View(new GridModel(ClenCleniInRoleSessionRepository.All(clenId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        [HandleError]
        public ActionResult RoleInsert(int clenId)
        {
            //Create a new instance of the EditableProduct class.
            using (var db = new SlavojDBContainer())
            {
                this.ModelState.Clear();
                ClenCleniInRoleEditable item = new ClenCleniInRoleEditable();
                //Perform model binding (fill the product properties and validate it).
                if (TryUpdateModel(item))
                {
                    if (ModelState.IsValid)
                    {

                        var entity = new SlavojMVC4_1.Models.CleniInRole();
                        item.ClenId = clenId;
                        entity.ClenId = item.ClenId;
                        entity.ClenRoleId = item.ClenRoleId;

                        // Add the entity
                        db.CleniInRoles.Add(entity);
                        //.................................................................................................
                        EfStatus status = db.SaveChangesWithValidation();
                        if (status.IsValid)
                        {
                            item.ClenInRolesId = entity.ClenInRolesId;
                            ClenCleniInRoleSessionRepository.Insert(item, clenId);
                        }
                        else
                        {
                            AddModelStateError(status);
                        }

                    }
                }

                //Rebind the grid
                return View(new GridModel(ClenCleniInRoleSessionRepository.All(clenId)));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult RoleDelete(int id, int clenId)
        {
            ClenCleniInRoleEditable item = ClenCleniInRoleSessionRepository.One(p => p.ClenInRolesId == id, clenId);

            if (item != null)
            {
                if (TryValidateModel(item))
                {
                    using (var db = new SlavojDBContainer())
                    {

                        this.ModelState.Clear();
                        var entity = db.CleniInRoles.Find(item.ClenInRolesId);
                        if (entity != null)
                        {
                            db.CleniInRoles.Remove(entity);
                            EfStatus status = db.SaveChangesWithValidation();
                            if (status.IsValid)
                            {
                                ClenCleniInRoleSessionRepository.Delete(item, clenId);
                            }
                            else
                            {
                                AddModelStateError(status);
                            }
                        }
                    }
                }
            }

            return View(new GridModel(ClenCleniInRoleSessionRepository.All(clenId)));
        }
        //......................................................................................................................................................................
    }

}