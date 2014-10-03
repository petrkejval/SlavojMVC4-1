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


        [SourceCodeFile("EditableProduct (model)", "~/Models/EditableProduct.cs")]
        [SourceCodeFile("SessionProductRepository", "~/Models/SessionProductRepository.cs")]
        [SourceCodeFile("Date.ascx (editor)", "~/Views/Shared/EditorTemplates/Date.ascx")]
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
            foreach (var modelValue in ModelState.Values)
            {
                modelValue.Errors.Clear();
            }
            TryUpdateModel(clen, form ); 
            //.........................................................................................................................................................
            if (ModelState.IsValid)
            {
                using (var db = new SlavojDBContainer())
                {

                    //var entity = new SlavojMVC4_1.Models.Clen();
                    //int asresyCount = db.Adresy.Count(p => p.ClenId == clen.ClenId);
                    //int kontaktCount = db.Kontakty.Count(p => p.ClenId == clen.ClenId);
                    //int registraceCount = db.Registraces.Count(p => p.ClenId == clen.ClenId);

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


                    //Nastaví všechna modifikovaná pole jako modifikovaná
                    SetModifyFields(db, entity);

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
                            SetModifyFields(db, entity.Adresa);

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
                            SetModifyFields(db, entity.Kontakt);

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
                            SetModifyFields(db, entity.Registrace);

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
                            SetModifyFields(db, entity.Rozhodci);

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
                            SetModifyFields(db, entity.Trener);

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
                    try
                    {
                        db.SaveChanges();

                        //Nástin jak refreshovat záznam nebo důležitá pole hodnotami z databáze
                        //using (var dbnew = new SlavojDBContainer())
                        //{
                        //    var clenDb = dbnew.Cleni.Find(clen.ClenId);
                        //    if (clenDb != null)
                        //    {
                        //        clen.TitulZa = clenDb.TitulZa;
                        //    }
                        //}
                        SessionCleniRepository.Update(clen);
                    }
                    catch (DbEntityValidationException ex)
                    {
                        var error = ex.EntityValidationErrors.First().ValidationErrors.First();
                        string propertyName = error.PropertyName;
                        if (propertyName == "Ulice" || propertyName == "CisloPopisne" || propertyName == "Obec" || propertyName == "Psc") { propertyName = "Adresa" + error.PropertyName; }
                        else if (propertyName == "Telefon" || propertyName == "Mail" || propertyName == "WWW") { propertyName = "Kontakt" + error.PropertyName; }
                        else if (propertyName == "CisloRegistrace" || propertyName == "PlatnaDo") { propertyName = "Registrace" + error.PropertyName; }
                        else if (propertyName == "CisloRegistrace" || propertyName == "Trida" || propertyName == "PlatnaDo") { propertyName = "Rozhodci" + error.PropertyName; }
                        else if (propertyName == "CisloRegistrace" || propertyName == "Trida" || propertyName == "PlatnaDo") { propertyName = "Trener" + error.PropertyName; }

                        this.ModelState.AddModelError(propertyName, error.ErrorMessage);
                    }

                }

            }
            return View(new GridModel(SessionCleniRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
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

                        try
                        {
                            // Insert the entity in the database
                            db.SaveChanges();

                            // Get the ProductID generated by the database
                            clen.ClenId = entity.ClenId;

                            SessionCleniRepository.Insert(clen);
                        }
                        catch (DbEntityValidationException ex)
                        {
                            var error = ex.EntityValidationErrors.First().ValidationErrors.First();
                            this.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                        }
                    }

                }
            }

            //Rebind the grid
            return View(new GridModel(SessionCleniRepository.All()));
        }




        private void SetModifyFields(SlavojDBContainer db, object entity)
        {

            foreach (string n in db.Entry(entity).CurrentValues.PropertyNames)
            {
                if (!Equals(db.Entry(entity).OriginalValues[n], db.Entry(entity).CurrentValues[n]))
                {
                    //Nastaví, že je položka modifikována
                    ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(entity).SetModifiedProperty(n);
                }
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
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
                            db.SaveChanges();
                        }
                        //smažu v Listu
                        SessionCleniRepository.Delete(clen);
                    }
                }
            }

            //Rebind the grid
            return View(new GridModel(SessionCleniRepository.All()));
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //[CultureAwareAction]
        //[GridAction]
        //public ActionResult _RegistraceDelete(int id)
        //{
        //    EditableClen clen = SessionCleniRepository.One(p => p.ClenId == id);

        //    if (clen != null)
        //    {
        //        if (TryValidateModel(clen))
        //        {

        //            using (var db = new SlavojDBContainer())
        //            {

        //                try
        //                {
        //                    Registrace registrace = db.Registraces.Find(clen.ClenId);
        //                    if (registrace != null)
        //                    {
        //                        db.Registraces.Remove(registrace);
        //                        db.SaveChanges();
        //                        clen.RegistraceCisloRegistrace = MyNullableInt;
        //                        clen.RegistraceDatumNarozeni = MyNullableDate;
        //                        clen.RegistracePlatnaDo = MyNullableDate;

        //                    }
        //                    SessionCleniRepository.Update(clen);
        //                }
        //                catch (DbEntityValidationException ex)
        //                {
        //                    var error = ex.EntityValidationErrors.First().ValidationErrors.First();
        //                    this.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        //                }
        //            }
        //        }

        //    }
        //    //Rebind the grid
        //    return View(new GridModel(SessionCleniRepository.All()));
        //}
    }

//    public static class GridControllerExtensions
//    {
//        public static RouteValueDictionary GridRouteValues(this ControllerBase controller)
//        {
//            var routeValues = new RouteValueDictionary();

//            var values = GetParams(controller.ControllerContext.HttpContext.Request);

//            foreach (string key in values.Keys)
//            {
//                if (key.EndsWith(GridUrlParameters.CurrentPage, StringComparison.OrdinalIgnoreCase) ||
//                    key.EndsWith(GridUrlParameters.Filter, StringComparison.OrdinalIgnoreCase) ||
//                    key.EndsWith(GridUrlParameters.OrderBy, StringComparison.OrdinalIgnoreCase) ||
//                    key.EndsWith(GridUrlParameters.GroupBy, StringComparison.OrdinalIgnoreCase) ||
//                    key.EndsWith(GridUrlParameters.PageSize, StringComparison.OrdinalIgnoreCase))
//                {
//                    routeValues[key] = values[key];
//                }
//            }

//            return routeValues;
//        }

//#if MVC3
//        private static IDictionary<string, object> GetParams(HttpRequestBase request)
//        {
//            var result = new Dictionary<string, object>();
//            var unvalidated = request.Unvalidated();
//            unvalidated.Form.CopyTo(result);
//            unvalidated.QueryString.CopyTo(result);
//            return result;
//        }
//#else
//        private static IDictionary<string, object> GetParams(HttpRequestBase request)
//        {
//            var result = new Dictionary<string, object>();
//            request.Params.CopyTo(result);
//            return result;
//        }
//#endif
//        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
//        public static T ValueOf<T>(this ControllerBase controller, string key)
//        {
//            ValueProviderResult result;
//            bool found = true;
//#if MVC1
//            found = controller.ValueProvider.TryGetValue(key, out result);
////#endif
////#if MVC2 || MVC3 || MVC4 || MVC5
//#else
//            result = controller.ValueProvider.GetValue(key);
//            found = result != null;
//#endif
//            if (found)
//            {
//                return (T)result.ConvertTo(typeof(T), CultureInfo.CurrentCulture);
//            }

//            return default(T);
//        }
//    }
}