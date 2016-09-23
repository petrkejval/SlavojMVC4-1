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
    public class KuzelnaProgramyGridController : Controller
    {
        DateTime defDatum = new DateTime(1899, 12, 31);

        // GET: KuzelnaProgramyGrid
        [SourceCodeFile("KuzelnaProgramEditable (model)", "~/Models/KuzelnaProgramEditable.cs")]
        [SourceCodeFile("KuzelnaProgramySessionRepository", "~/Models/KuzelnaProgramySessionRepository.cs")]
        public ActionResult KuzelnaProgramyGrid()
        {
            return View();
        }

        [GridAction]
        public ActionResult KuzelnaProgramySelect()
        {
            return View(new GridModel(KuzelnaProgramySessionRepository.All(true)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        [HandleError]
        public ActionResult KuzelnaProgramyUpdate(int id)
        {
            KuzelnaProgramEditable item = KuzelnaProgramySessionRepository.One(p => p.KuzelnaProgramId == id);

            TryUpdateModel(item);
            //.........................................................................................................................................................
            if (ModelState.IsValid)
            {
                using (var db = new SlavojDBContainer())
                {
                    var defDatum = new DateTime(1899, 12, 31);
                    var entity = db.KuzelnaProgramy.Find(item.KuzelnaProgramId);
                    if (entity == null)
                    {
                        entity = new KuzelnaProgram();
                        entity.KuzelnaProgramId = item.KuzelnaProgramId;
                    }

                    entity.ProgramNazev = item.ProgramNazev;
                    entity.ProgramKategorieId = item.ProgramKategorieId;
                    entity.ProgramDatumCasOd = item.ProgramDatumCasOd;
                    entity.ProgramDatumCasDo = item.ProgramDatumCasDo;
                    entity.Poznamka = item.Poznamka;
                    entity.OpakovatKazdyTyden = item.OpakovatKazdyTyden;
                    entity.OpakovatDatumDo = item.OpakovatDatumDo == defDatum ? null : item.OpakovatDatumDo;
                    entity.JeAktivni = item.JeAktivni;

                    //Nastaví všechna modifikovaná pole jako modifikovaná
                    db.SetModifyFields(entity);
                    //.................................................................................................................................
                    this.ModelState.Clear();
                    EfStatus status = db.SaveChangesWithValidation();
                    if (status.IsValid)
                    {
                        KuzelnaProgramySessionRepository.Update(item);
                        return View(new GridModel(KuzelnaProgramySessionRepository.All(true)));
                        //return RedirectToAction("KuzelnaProgramySelect", this.GridRouteValues());
                    }
                    else
                    {
                        AddModelStateError(status);
                    }

                }

            }
            return View(new GridModel(KuzelnaProgramySessionRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult KuzelnaProgramyInsert()
        {
            //Create a new instance of the EditableProduct class.
            KuzelnaProgramEditable item = new KuzelnaProgramEditable();

            //Perform model binding (fill the product properties and validate it).
            if (TryUpdateModel(item))
            {
                if (ModelState.IsValid)
                {

                    using (var db = new SlavojDBContainer())
                    {
                        var entity = new SlavojMVC4_1.Models.KuzelnaProgram();

                        entity.ProgramNazev = item.ProgramNazev;
                        entity.ProgramKategorieId = item.ProgramKategorieId;
                        entity.ProgramDatumCasOd = item.ProgramDatumCasOd;
                        entity.ProgramDatumCasDo = item.ProgramDatumCasDo;
                        entity.Poznamka = item.Poznamka;
                        entity.OpakovatKazdyTyden = item.OpakovatKazdyTyden;
                        entity.OpakovatDatumDo = item.OpakovatDatumDo;
                        entity.JeAktivni = item.JeAktivni;


                        // Add the entity
                        db.KuzelnaProgramy.Add(entity);
                        //.................................................................................................
                        this.ModelState.Clear();
                        EfStatus status = db.SaveChangesWithValidation();
                        if (status.IsValid)
                        {
                            item.KuzelnaProgramId = entity.KuzelnaProgramId;
                            KuzelnaProgramySessionRepository.Insert(item);
                            return View(new GridModel(KuzelnaProgramySessionRepository.All(true)));
                        }
                        else
                        {
                            AddModelStateError(status);
                        }

                    }

                }
            }

            //Rebind the grid
            return View(new GridModel(KuzelnaProgramySessionRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult KuzelnaProgramyDelete(int id)
        {
            //Find a customer with ProductID equal to the id action parameter
            KuzelnaProgramEditable item = KuzelnaProgramySessionRepository.One(p => p.KuzelnaProgramId == id);

            if (item != null)
            {
                if (TryValidateModel(item))
                {
                    using (var db = new SlavojDBContainer())
                    {

                        //Smažu v db
                        var entity = db.KuzelnaProgramy.Find(item.KuzelnaProgramId);
                        if (entity != null)
                        {
                            db.KuzelnaProgramy.Remove(entity);
                            this.ModelState.Clear();
                            EfStatus status = db.SaveChangesWithValidation();
                            if (!status.IsValid)
                            {
                                AddModelStateError(status);
                            }
                            else
                            {
                                KuzelnaProgramySessionRepository.Delete(item);
                            }
                        }
                    }
                }
            }

            //Rebind the grid
            return View(new GridModel(KuzelnaProgramySessionRepository.All()));
        }
        //.......................................................................................................................................................................
        [GridAction]
        public ActionResult SluzbaSelect(int KuzelnaProgramId)
        {
            var model = new GridModel(KuzelnaProgramSluzbySessionRepository.All(KuzelnaProgramId, true));
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        [HandleError]
        public ActionResult SluzbaUpdate(int id, int KuzelnaProgramId)
        {
            KuzelnaProgramSluzbaEditable item = KuzelnaProgramSluzbySessionRepository.One(p => p.KuzelnaProgramSluzbaId == id, KuzelnaProgramId);
            var values = ModelState.Values;
            TryUpdateModel(item);
            //.........................................................................................................................................................
            if (ModelState.IsValid)
            {
                using (var db = new SlavojDBContainer())
                {
                    this.ModelState.Clear();
                    var entity = db.KuzelnaProgramSluzby.Find(item.KuzelnaProgramSluzbaId);
                    if (entity != null)
                    {
                        entity.ClenId = item.ClenId;
                        item.SluzbaDatumCasOd = item.SluzbaDatumCasOd == defDatum ? null : item.SluzbaDatumCasOd;
                        item.SluzbaDatumCasDo = item.SluzbaDatumCasDo == defDatum ? null : item.SluzbaDatumCasDo;

                        entity.SluzbaDatumCasOd = item.SluzbaDatumCasOd;
                        entity.SluzbaDatumCasDo = item.SluzbaDatumCasDo;
                        //.................................................................................................
                        EfStatus status = db.SaveChangesWithValidation();
                        if (status.IsValid)
                        {
                            KuzelnaProgramSluzbySessionRepository.Update(item, KuzelnaProgramId);
                        }
                        else
                        {
                            AddModelStateError(status);
                        }
                    }

                }

            }

            return View(new GridModel(KuzelnaProgramSluzbySessionRepository.All(KuzelnaProgramId)));
        }



        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        [HandleError]
        public ActionResult SluzbaInsert(int KuzelnaProgramId)
        {
            //Create a new instance of the EditableProduct class.
            using (var db = new SlavojDBContainer())
            {
                this.ModelState.Clear();
                KuzelnaProgramSluzbaEditable item = new KuzelnaProgramSluzbaEditable();
                //Perform model binding (fill the product properties and validate it).
                if (TryUpdateModel(item))
                {
                    if (ModelState.IsValid)
                    {

                        var entity = new SlavojMVC4_1.Models.KuzelnaProgramSluzba();
                        item.KuzelnaProgramId = KuzelnaProgramId;
                        entity.KuzelnaProgramId = item.KuzelnaProgramId;

                        entity.ClenId = item.ClenId;
                        entity.SluzbaDatumCasOd = item.SluzbaDatumCasOd;
                        entity.SluzbaDatumCasDo = item.SluzbaDatumCasDo;

                        // Add the entity
                        db.KuzelnaProgramSluzby.Add(entity);
                        //.................................................................................................
                        EfStatus status = db.SaveChangesWithValidation();
                        if (status.IsValid)
                        {
                            item.KuzelnaProgramSluzbaId = entity.KuzelnaProgramSluzbaId;
                            KuzelnaProgramSluzbySessionRepository.Insert(item, KuzelnaProgramId);
                        }
                        else
                        {
                            AddModelStateError(status);
                        }

                    }
                }

                //Rebind the grid
                return View(new GridModel(KuzelnaProgramSluzbySessionRepository.All(KuzelnaProgramId)));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult KoloDelete(int id, int KuzelnaProgramId)
        {
            KuzelnaProgramSluzbaEditable item = KuzelnaProgramSluzbySessionRepository.One(p => p.KuzelnaProgramSluzbaId == id, KuzelnaProgramId);

            if (item != null)
            {
                if (TryValidateModel(item))
                {
                    using (var db = new SlavojDBContainer())
                    {

                        this.ModelState.Clear();
                        var entity = db.KuzelnaProgramSluzby.Find(item.KuzelnaProgramSluzbaId);
                        if (entity != null)
                        {
                            db.KuzelnaProgramSluzby.Remove(entity);
                            EfStatus status = db.SaveChangesWithValidation();
                            if (status.IsValid)
                            {
                                KuzelnaProgramSluzbySessionRepository.Delete(item, KuzelnaProgramId);
                            }
                            else
                            {
                                AddModelStateError(status);
                            }
                        }
                    }
                }
            }

            return View(new GridModel(KuzelnaProgramSluzbySessionRepository.All(KuzelnaProgramId)));
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