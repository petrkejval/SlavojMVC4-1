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
using System.Globalization;
using SlavojMVC4_1.Models;
using SlavojMVC4_1.Filters;
using System.Data.Entity.Infrastructure;
using System.Collections;

namespace SlavojMVC4_1.Controllers
{
    public class DruzstvaGridController : Controller
    {
        // GET: DruzstvaGrid
        [SourceCodeFile("DruzsvaEditable (model)", "~/Models/DruzstvaEditable.cs")]
        [SourceCodeFile("DruzstvaSessionRepository", "~/Models/DruzstvaSessionRepository.cs")]
        public ActionResult DruzstvaGrid()
        {
            return View();
        }

        [GridAction]
        public ActionResult DruzstvaSelect()
        {

            return View(new GridModel(DruzstvaSessionRepository.All(true)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        [HandleError]
        public ActionResult DruzstvaUpdate(int id)
        {
            DruzstvoEditable item = DruzstvaSessionRepository.One(p => p.DruzstvoId == id);

            TryUpdateModel(item);
            //.........................................................................................................................................................
            if (ModelState.IsValid)
            {
                using (var db = new SlavojDBContainer())
                {

                    var entity = db.Druzstva.Find(item.DruzstvoId);
                    if (entity == null)
                    {
                        entity = new Druzstvo();
                        entity.DruzstvoId = item.DruzstvoId;
                    }

                    entity.Nazev = item.Nazev;
                    entity.Pismeno = item.Pismeno;
                    entity.SoutezId = item.SoutezId;
                    entity.Existuje = item.Existuje;
                    entity.VedouciId = item.VedouciId;
                    entity.TrenerId = item.TrenerId;
                    entity.Popis = item.Popis;

                    //Nastaví všechna modifikovaná pole jako modifikovaná
                    db.SetModifyFields(entity);
                    //.................................................................................................................................
                    this.ModelState.Clear();
                    EfStatus status = db.SaveChangesWithValidation();
                    if (status.IsValid)
                    {
                        DruzstvaSessionRepository.Update(item);
                    }
                    else
                    {
                        AddModelStateError(status);
                    }

                }

            }
            return View(new GridModel(DruzstvaSessionRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult DruzstvaInsert()
        {
            //Create a new instance of the EditableProduct class.
            DruzstvoEditable item = new DruzstvoEditable();

            //Perform model binding (fill the product properties and validate it).
            if (TryUpdateModel(item))
            {
                if (ModelState.IsValid)
                {

                    using (var db = new SlavojDBContainer())
                    {
                        var entity = new SlavojMVC4_1.Models.Druzstvo();

                        entity.Nazev = item.Nazev;
                        entity.Pismeno = item.Pismeno;
                        entity.SoutezId = item.SoutezId;
                        entity.Existuje = item.Existuje;
                        entity.VedouciId = item.VedouciId;
                        entity.TrenerId = item.TrenerId;
                        entity.Popis = item.Popis;

                        // Add the entity
                        db.Druzstva.Add(entity);
                        //.................................................................................................
                        this.ModelState.Clear();
                        EfStatus status = db.SaveChangesWithValidation();
                        if (status.IsValid)
                        {
                            item.DruzstvoId = entity.DruzstvoId;
                            DruzstvaSessionRepository.Insert(item);
                        }
                        else
                        {
                            AddModelStateError(status);
                        }

                    }

                }
            }

            //Rebind the grid
            return View(new GridModel(DruzstvaSessionRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult DruzstvaDelete(int id)
        {
            //Find a customer with ProductID equal to the id action parameter
            DruzstvoEditable item = DruzstvaSessionRepository.One(p => p.DruzstvoId == id);

            if (item != null)
            {
                if (TryValidateModel(item))
                {
                    using (var db = new SlavojDBContainer())
                    {

                        //Smažu v db
                        var entity = db.Druzstva.Find(item.DruzstvoId);
                        if (entity != null)
                        {
                            db.Druzstva.Remove(entity);
                            this.ModelState.Clear();
                            EfStatus status = db.SaveChangesWithValidation();
                            if (!status.IsValid)
                            {
                                AddModelStateError(status);
                            }
                            else
                            {
                                DruzstvaSessionRepository.Delete(item);
                            }
                        }
                    }
                }
            }

            //Rebind the grid
            return View(new GridModel(DruzstvaSessionRepository.All()));
        }
        //.......................................................................................................................................................................
        [GridAction]
        public ActionResult CleniSelect(int druzstvoId)
        {
            return View(new GridModel(DruzstvaClenSessionRepository.All(druzstvoId, true)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        [HandleError]
        public ActionResult CleniUpdate(int id, int druzstvoId)
        {
            DruzstvaClenEditable item = DruzstvaClenSessionRepository.One(p => p.DruzstvoClenId == id, druzstvoId);
            var values = ModelState.Values;
            TryUpdateModel(item);
            //.........................................................................................................................................................
            if (ModelState.IsValid)
            {
                using (var db = new SlavojDBContainer())
                {
                    this.ModelState.Clear();
                    var entity = db.DruzstvoClen.Find(item.DruzstvoClenId);
                    if (entity != null)
                    {
                        entity.ClenId = item.ClenId;
                        //.................................................................................................
                        EfStatus status = db.SaveChangesWithValidation();
                        if (status.IsValid)
                        {
                            DruzstvaClenSessionRepository.Insert(item, druzstvoId);
                        }
                        else
                        {
                            AddModelStateError(status);
                        }
                    }

                }

            }

            return View(new GridModel(DruzstvaClenSessionRepository.All(druzstvoId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        [HandleError]
        public ActionResult CleniInsert(int druzstvoId)
        {
            //Create a new instance of the EditableProduct class.
            using (var db = new SlavojDBContainer())
            {
                this.ModelState.Clear();
                DruzstvaClenEditable item = new DruzstvaClenEditable();
                //Perform model binding (fill the product properties and validate it).
                if (TryUpdateModel(item))
                {
                    if (ModelState.IsValid)
                    {

                        var entity = new SlavojMVC4_1.Models.DruzstvaCleni();
                        item.DruzstvoId = druzstvoId;
                        entity.DruzstvoId = item.DruzstvoId;
                        entity.ClenId = item.ClenId;

                        // Add the entity
                        db.DruzstvoClen.Add(entity);
                        //.................................................................................................
                        EfStatus status = db.SaveChangesWithValidation();
                        if (status.IsValid)
                        {
                            item.DruzstvoClenId = entity.DruzstvoClenId;
                            DruzstvaClenSessionRepository.Insert(item, druzstvoId);
                        }
                        else
                        {
                            AddModelStateError(status);
                        }

                    }
                }

                //Rebind the grid
                return View(new GridModel(DruzstvaClenSessionRepository.All(druzstvoId)));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult CleniDelete(int id, int druzstvoId)
        {
            DruzstvaClenEditable item = DruzstvaClenSessionRepository.One(p => p.DruzstvoClenId == id, druzstvoId);

            if (item != null)
            {
                if (TryValidateModel(item))
                {
                    using (var db = new SlavojDBContainer())
                    {

                        this.ModelState.Clear();
                        var entity = db.DruzstvoClen.Find(item.DruzstvoClenId);
                        if (entity != null)
                        {
                            db.DruzstvoClen.Remove(entity);
                            EfStatus status = db.SaveChangesWithValidation();
                            if (status.IsValid)
                            {
                                DruzstvaClenSessionRepository.Delete(item, druzstvoId);
                            }
                            else
                            {
                                AddModelStateError(status);
                            }
                        }
                    }
                }
            }

            return View(new GridModel(DruzstvaClenSessionRepository.All(druzstvoId)));
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