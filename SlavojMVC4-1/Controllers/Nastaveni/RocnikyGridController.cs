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
    public class RocnikyGridController : Controller
    {
        // GET: RocnikyGrid
        [SourceCodeFile("RocnikEditable (model)", "~/Models/RocnikEditable.cs")]
        [SourceCodeFile("RocnikySessionRepository", "~/Models/RocnikySessionRepository.cs")]
        public ActionResult RocnikyGrid()
        {
            return View();
        }

        [GridAction]
        public ActionResult RocnikySelect()
        {

            return View(new GridModel(RocnikySessionRepository.All(true)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        [HandleError]
        public ActionResult RocnikyUpdate(int id)
        {
            RocnikEditable item = RocnikySessionRepository.One(p => p.RocnikId == id);

            TryUpdateModel(item);
            //.........................................................................................................................................................
            if (ModelState.IsValid)
            {
                using (var db = new SlavojDBContainer())
                {

                    var entity = db.Rocniky.Find(item.RocnikId);
                    if (entity == null)
                    {
                        entity = new Rocnik();
                        entity.RocnikId = item.RocnikId;
                    }


                    entity.Nazev = item.Nazev;
                    entity.JeVybrany = item.JeVybrany;

                    //Nastaví všechna modifikovaná pole jako modifikovaná
                    db.SetModifyFields(entity);
                    //.................................................................................................................................
                    this.ModelState.Clear();
                    EfStatus status = db.SaveChangesWithValidation();
                    if (status.IsValid)
                    {
                        RocnikySessionRepository.Update(item);
                        return View(new GridModel(RocnikySessionRepository.All(true)));
                        //return RedirectToAction("RocnikySelect", this.GridRouteValues());
                    }
                    else
                    {
                        AddModelStateError(status);
                    }

                }

            }
            return View(new GridModel(RocnikySessionRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult RocnikyInsert()
        {
            //Create a new instance of the EditableProduct class.
            RocnikEditable item = new RocnikEditable();

            //Perform model binding (fill the product properties and validate it).
            if (TryUpdateModel(item))
            {
                if (ModelState.IsValid)
                {

                    using (var db = new SlavojDBContainer())
                    {
                        var entity = new SlavojMVC4_1.Models.Rocnik();

                        entity.Nazev = item.Nazev;
                        entity.JeVybrany = item.JeVybrany;

                        // Add the entity
                        db.Rocniky.Add(entity);
                        //.................................................................................................
                        this.ModelState.Clear();
                        EfStatus status = db.SaveChangesWithValidation();
                        if (status.IsValid)
                        {
                            item.RocnikId = entity.RocnikId;
                            RocnikySessionRepository.Insert(item);
                            return View(new GridModel(RocnikySessionRepository.All(true)));
                        }
                        else
                        {
                            AddModelStateError(status);
                        }

                    }

                }
            }

            //Rebind the grid
            return View(new GridModel(RocnikySessionRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult RocnikyDelete(int id)
        {
            //Find a customer with ProductID equal to the id action parameter
            RocnikEditable item = RocnikySessionRepository.One(p => p.RocnikId == id);

            if (item != null)
            {
                if (TryValidateModel(item))
                {
                    using (var db = new SlavojDBContainer())
                    {

                        //Smažu v db
                        var entity = db.Rocniky.Find(item.RocnikId);
                        if (entity != null)
                        {
                            db.Rocniky.Remove(entity);
                            this.ModelState.Clear();
                            EfStatus status = db.SaveChangesWithValidation();
                            if (!status.IsValid)
                            {
                                AddModelStateError(status);
                            }
                            else
                            {
                                RocnikySessionRepository.Delete(item);
                            }
                        }
                    }
                }
            }

            //Rebind the grid
            return View(new GridModel(RocnikySessionRepository.All()));
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