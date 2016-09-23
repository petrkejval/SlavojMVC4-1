using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Telerik.Web.Mvc.UI;
using Telerik.Web.Mvc;
using Telerik.Web.Mvc.Extensions;
using System.Globalization;
using SlavojMVC4_1.Models;
using SlavojMVC4_1.Filters;
using System.Data.Entity.Infrastructure;
using System.Collections;

namespace SlavojMVC4_1.Controllers
{
    public class ZapasyGridController : Controller
    {
        // GET: ZapasyGrid
        [SourceCodeFile("ZapasEditable (model)", "~/Models/ZapasEditable.cs")]
        [SourceCodeFile("ZapasySessionRepository", "~/Models/ZapasySessionRepository.cs")]
        public ActionResult ZapasyGrid()
        {
            return View();
        }

        [GridAction]
        public ActionResult ZapasySelect()
        {

            return View(new GridModel(ZapasySessionRepository.All(true)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        [HandleError]
        public ActionResult ZapasyUpdate(int id)
        {
            ZapasEditable item = ZapasySessionRepository.One(p => p.ZapasId == id);

            TryUpdateModel(item);
            //.........................................................................................................................................................
            if (ModelState.IsValid)
            {
                using (var db = new SlavojDBContainer())
                {
                    var defDatum = new DateTime(1899, 12, 31);
                    var entity = db.Zapasy.Find(item.ZapasId);
                    if (entity == null)
                    {
                        entity = new Zapas();
                        entity.ZapasId = item.ZapasId;
                    }

                    entity.SoutezId = item.SoutezId;
                    entity.KuzelnaNazev = item.KuzelnaNazev;
                    entity.KuzelnaDomaci = item.KuzelnaDomaci;
                    entity.ZapasDatumCasOd = item.ZapasDatumCasOd;
                    entity.ZapasDatumCasDo = item.ZapasDatumCasDo;
                    entity.Druzstvo1Nazev = item.Druzstvo1Nazev;
                    entity.Druzstvo2Nazev = item.Druzstvo2Nazev;
                    entity.SrazDatumCas = item.SrazDatumCas == defDatum ? null : item.SrazDatumCas;
                    entity.MistoSrazu = item.MistoSrazu;
                    entity.Poznamka = item.Poznamka;
                    entity.RozhodciId = item.RozhodciId;

                    //Nastaví všechna modifikovaná pole jako modifikovaná
                    db.SetModifyFields(entity);
                    //.................................................................................................................................
                    this.ModelState.Clear();
                    EfStatus status = db.SaveChangesWithValidation();
                    if (status.IsValid)
                    {
                        ZapasySessionRepository.Update(item);
                        return View(new GridModel(ZapasySessionRepository.All(true)));
                    }
                    else
                    {
                        AddModelStateError(status);
                    }

                }

            }
            return View(new GridModel(ZapasySessionRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult ZapasyInsert()
        {
            //Create a new instance of the EditableProduct class.
            ZapasEditable item = new ZapasEditable();

            //Perform model binding (fill the product properties and validate it).
            if (TryUpdateModel(item))
            {
                if (ModelState.IsValid)
                {

                    using (var db = new SlavojDBContainer())
                    {
                        var entity = new SlavojMVC4_1.Models.Zapas();

                        entity.SoutezId = item.SoutezId;
                        entity.KuzelnaNazev = item.KuzelnaNazev;
                        entity.KuzelnaDomaci = item.KuzelnaDomaci;
                        entity.ZapasDatumCasOd = item.ZapasDatumCasOd;
                        entity.ZapasDatumCasDo = item.ZapasDatumCasDo;
                        entity.Druzstvo1Nazev = item.Druzstvo1Nazev;
                        entity.Druzstvo2Nazev = item.Druzstvo2Nazev;
                        entity.SrazDatumCas = item.SrazDatumCas;
                        entity.MistoSrazu = item.MistoSrazu;
                        entity.Poznamka = item.Poznamka;
                        entity.RozhodciId = item.RozhodciId;


                        // Add the entity
                        db.Zapasy.Add(entity);
                        //.................................................................................................
                        this.ModelState.Clear();
                        EfStatus status = db.SaveChangesWithValidation();
                        if (status.IsValid)
                        {
                            item.ZapasId = entity.ZapasId;
                            ZapasySessionRepository.Insert(item);
                            return View(new GridModel(ZapasySessionRepository.All(true)));
                        }
                        else
                        {
                            AddModelStateError(status);
                        }

                    }

                }
            }

            //Rebind the grid
            return View(new GridModel(ZapasySessionRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult ZapasyDelete(int id)
        {
            //Find a customer with ProductID equal to the id action parameter
            ZapasEditable item = ZapasySessionRepository.One(p => p.ZapasId == id);

            if (item != null)
            {
                if (TryValidateModel(item))
                {
                    using (var db = new SlavojDBContainer())
                    {

                        //Smažu v db
                        var entity = db.Zapasy.Find(item.ZapasId);
                        if (entity != null)
                        {
                            db.Zapasy.Remove(entity);
                            this.ModelState.Clear();
                            EfStatus status = db.SaveChangesWithValidation();
                            if (!status.IsValid)
                            {
                                AddModelStateError(status);
                            }
                            else
                            {
                                ZapasySessionRepository.Delete(item);
                            }
                        }
                    }
                }
            }

            //Rebind the grid
            return View(new GridModel(ZapasySessionRepository.All()));
        }
        //......................................................................................................................................................................
        private void AddModelStateError(EfStatus status)
        {
            //Naplnit chyb z db.Souteze do this.ModelState
            for (int i = 0; i < status.EfErrors.Count; i++)
            {
                var error = status.EfErrors[i];
                string propertyName = status.EfErrors[i].MemberNames.FirstOrDefault();

                propertyName = propertyName != null ? propertyName : string.Empty;
                this.ModelState.AddModelError(propertyName, error.ErrorMessage);

            }

        }
    }
}