﻿using System;
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
    public class VysledkyGridController : Controller
    {
        // GET: VysledkyGrid
        [SourceCodeFile("VysledekEditable (model)", "~/Models/VysledekEditable.cs")]
        [SourceCodeFile("VysledkySessionRepository", "~/Models/VysledkySessionRepository.cs")]
        public ActionResult VysledkyGrid()
        {
            return View();
        }

        [GridAction]
        public ActionResult VysledkySelect()
        {

            return View(new GridModel(VysledkySessionRepository.All(true)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        [HandleError]
        public ActionResult VysledkyUpdate(int id)
        {
            VysledekEditable item = VysledkySessionRepository.One(p => p.VysledekId == id);

            TryUpdateModel(item);
            //.........................................................................................................................................................
            if (ModelState.IsValid)
            {
                using (var db = new SlavojDBContainer())
                {

                    var entity = db.Vysledky.Find(item.VysledekId);
                    if (entity == null)
                    {
                        entity = new Vysledek();
                        entity.VysledekId = item.VysledekId;
                    }

                    entity.RocnikId = item.RocnikId;
                    entity.SoutezId = item.SoutezId;
                    entity.Tabulka = item.Tabulka;
                    entity.WebPageIId = item.WebPageIId;


                    //Nastaví všechna modifikovaná pole jako modifikovaná
                    db.SetModifyFields(entity);
                    //.................................................................................................................................
                    this.ModelState.Clear();
                    EfStatus status = db.SaveChangesWithValidation();
                    if (status.IsValid)
                    {
                        VysledkySessionRepository.Update(item);
                        return View(new GridModel(VysledkySessionRepository.All(true)));
                        //return RedirectToAction("VysledkySelect", this.GridRouteValues());
                    }
                    else
                    {
                        AddModelStateError(status);
                    }

                }

            }
            return View(new GridModel(VysledkySessionRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult VysledkyInsert()
        {
            //Create a new instance of the EditableProduct class.
            VysledekEditable item = new VysledekEditable();

            //Perform model binding (fill the product properties and validate it).
            if (TryUpdateModel(item))
            {
                if (ModelState.IsValid)
                {

                    using (var db = new SlavojDBContainer())
                    {
                        var entity = new SlavojMVC4_1.Models.Vysledek();

                        entity.RocnikId = item.RocnikId;
                        entity.SoutezId = item.SoutezId;
                        entity.Tabulka = item.Tabulka;
                        entity.WebPageIId = item.WebPageIId;


                        // Add the entity
                        db.Vysledky.Add(entity);
                        //.................................................................................................
                        this.ModelState.Clear();
                        EfStatus status = db.SaveChangesWithValidation();
                        if (status.IsValid)
                        {
                            item.VysledekId = entity.VysledekId;
                            VysledkySessionRepository.Insert(item);
                            return View(new GridModel(VysledkySessionRepository.All(true)));
                        }
                        else
                        {
                            AddModelStateError(status);
                        }

                    }

                }
            }

            //Rebind the grid
            return View(new GridModel(VysledkySessionRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult VysledkyDelete(int id)
        {
            //Find a customer with ProductID equal to the id action parameter
            VysledekEditable item = VysledkySessionRepository.One(p => p.VysledekId == id);

            if (item != null)
            {
                if (TryValidateModel(item))
                {
                    using (var db = new SlavojDBContainer())
                    {

                        //Smažu v db
                        var entity = db.Vysledky.Find(item.VysledekId);
                        if (entity != null)
                        {
                            db.Vysledky.Remove(entity);
                            this.ModelState.Clear();
                            EfStatus status = db.SaveChangesWithValidation();
                            if (!status.IsValid)
                            {
                                AddModelStateError(status);
                            }
                            else
                            {
                                VysledkySessionRepository.Delete(item);
                            }
                        }
                    }
                }
            }

            //Rebind the grid
            return View(new GridModel(VysledkySessionRepository.All()));
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