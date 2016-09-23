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
    public class ZapasyBarvyGridController : Controller
    {
        // GET: ZapasyBarvyGrid
        [SourceCodeFile("ZapasBarvaEditable (model)", "~/Models/ZapasBarvaEditable.cs")]
        [SourceCodeFile("ZapasyBarvySessionRepository", "~/Models/ZapasyBarvySessionRepository.cs")]
        public ActionResult ZapasyBarvyGrid()
        {
            return View();
        }

        [GridAction]
        public ActionResult ZapasyBarvySelect()
        {

            return View(new GridModel(ZapasyBarvySessionRepository.All(true)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        [HandleError]
        public ActionResult ZapasyBarvyUpdate(int id)
        {
            ZapasBarvaEditable item = ZapasyBarvySessionRepository.One(p => p.ZapasBarvaId == id);

            TryUpdateModel(item);
            //.........................................................................................................................................................
            if (ModelState.IsValid)
            {
                using (var db = new SlavojDBContainer())
                {

                    var entity = db.ZapasyBarvy.Find(item.ZapasBarvaId);
                    if (entity == null)
                    {
                        entity = new ZapasBarva();
                        entity.ZapasBarvaId = item.ZapasBarvaId;
                    }
                    entity.DruzstvoNazev = item.DruzstvoNazev;
                    entity.Barva = item.Barva;

                    //Nastaví všechna modifikovaná pole jako modifikovaná
                    db.SetModifyFields(entity);
                    //.................................................................................................................................
                    this.ModelState.Clear();
                    EfStatus status = db.SaveChangesWithValidation();
                    if (status.IsValid)
                    {
                        ZapasyBarvySessionRepository.Update(item);
                        return View(new GridModel(ZapasyBarvySessionRepository.All(true)));
                    }
                    else
                    {
                        AddModelStateError(status);
                    }

                }

            }
            return View(new GridModel(ZapasyBarvySessionRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult ZapasyBarvyInsert()
        {
            //Create a new instance of the EditableProduct class.
            ZapasBarvaEditable item = new ZapasBarvaEditable();

            //Perform model binding (fill the product properties and validate it).
            if (TryUpdateModel(item))
            {
                if (ModelState.IsValid)
                {

                    using (var db = new SlavojDBContainer())
                    {
                        var entity = new SlavojMVC4_1.Models.ZapasBarva();

                        entity.DruzstvoNazev = item.DruzstvoNazev;
                        entity.Barva = item.Barva;


                        // Add the entity
                        db.ZapasyBarvy.Add(entity);
                        //.................................................................................................
                        this.ModelState.Clear();
                        EfStatus status = db.SaveChangesWithValidation();
                        if (status.IsValid)
                        {
                            item.ZapasBarvaId = entity.ZapasBarvaId;
                            ZapasyBarvySessionRepository.Insert(item);
                            return View(new GridModel(ZapasyBarvySessionRepository.All(true)));
                        }
                        else
                        {
                            AddModelStateError(status);
                        }

                    }

                }
            }

            //Rebind the grid
            return View(new GridModel(ZapasyBarvySessionRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult ZapasyBarvyDelete(int id)
        {
            //Find a customer with ProductID equal to the id action parameter
            ZapasBarvaEditable item = ZapasyBarvySessionRepository.One(p => p.ZapasBarvaId == id);

            if (item != null)
            {
                if (TryValidateModel(item))
                {
                    using (var db = new SlavojDBContainer())
                    {

                        //Smažu v db
                        var entity = db.ZapasyBarvy.Find(item.ZapasBarvaId);
                        if (entity != null)
                        {
                            db.ZapasyBarvy.Remove(entity);
                            this.ModelState.Clear();
                            EfStatus status = db.SaveChangesWithValidation();
                            if (!status.IsValid)
                            {
                                AddModelStateError(status);
                            }
                            else
                            {
                                ZapasyBarvySessionRepository.Delete(item);
                            }
                        }
                    }
                }
            }

            //Rebind the grid
            return View(new GridModel(ZapasyBarvySessionRepository.All()));
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