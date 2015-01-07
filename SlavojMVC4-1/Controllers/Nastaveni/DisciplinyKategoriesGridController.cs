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
    public class DisciplinyKategoriesGridController : Controller
    {
        // GET: DisciplinyKategoriesGrid
        [SourceCodeFile("DisciplinyKategorieEditable (model)", "~/Models/DisciplinyKategorieEditable.cs")]
        [SourceCodeFile("DisciplinyKategoriesSessionRepository", "~/Models/DisciplinyKategoriesSessionRepository.cs")]
        public ActionResult DisciplinyKategoriesGrid()
        {
            return View();
        }

        [GridAction]
        public ActionResult DisciplinyKategoriesSelect()
        {

            return View(new GridModel(DisciplinyKategoriesSessionRepository.All(true)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        [HandleError]
        public ActionResult DisciplinyKategoriesUpdate(int id)
        {
            DisciplinyKategorieEditable item = DisciplinyKategoriesSessionRepository.One(p => p.DisciplinyKategorieId == id);

            TryUpdateModel(item);
            //.........................................................................................................................................................
            if (ModelState.IsValid)
            {
                using (var db = new SlavojDBContainer())
                {

                    var entity = db.DisciplinyKategories.Find(item.DisciplinyKategorieId);
                    if (entity == null)
                    {
                        entity = new DisciplinyKategorie();
                        entity.DisciplinyKategorieId = item.DisciplinyKategorieId;
                    }

                    entity.Nazev = item.Nazev;

                    //Nastaví všechna modifikovaná pole jako modifikovaná
                    db.SetModifyFields(entity);
                    //.................................................................................................................................
                    this.ModelState.Clear();
                    EfStatus status = db.SaveChangesWithValidation();
                    if (status.IsValid)
                    {
                        DisciplinyKategoriesSessionRepository.Update(item);
                        return View(new GridModel(DisciplinyKategoriesSessionRepository.All(true)));
                        //return RedirectToAction("DisciplinyKategoriesSelect", this.GridRouteValues());
                    }
                    else
                    {
                        AddModelStateError(status);
                    }

                }

            }
            return View(new GridModel(DisciplinyKategoriesSessionRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult DisciplinyKategoriesInsert()
        {
            //Create a new instance of the EditableProduct class.
            DisciplinyKategorieEditable item = new DisciplinyKategorieEditable();

            //Perform model binding (fill the product properties and validate it).
            if (TryUpdateModel(item))
            {
                if (ModelState.IsValid)
                {

                    using (var db = new SlavojDBContainer())
                    {
                        var entity = new SlavojMVC4_1.Models.DisciplinyKategorie();

                        entity.Nazev = item.Nazev;

                        // Add the entity
                        db.DisciplinyKategories.Add(entity);
                        //.................................................................................................
                        this.ModelState.Clear();
                        EfStatus status = db.SaveChangesWithValidation();
                        if (status.IsValid)
                        {
                            item.DisciplinyKategorieId = entity.DisciplinyKategorieId;
                            DisciplinyKategoriesSessionRepository.Insert(item);
                            return View(new GridModel(DisciplinyKategoriesSessionRepository.All(true)));
                        }
                        else
                        {
                            AddModelStateError(status);
                        }

                    }

                }
            }

            //Rebind the grid
            return View(new GridModel(DisciplinyKategoriesSessionRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult DisciplinyKategoriesDelete(int id)
        {
            //Find a customer with ProductID equal to the id action parameter
            DisciplinyKategorieEditable item = DisciplinyKategoriesSessionRepository.One(p => p.DisciplinyKategorieId == id);

            if (item != null)
            {
                if (TryValidateModel(item))
                {
                    using (var db = new SlavojDBContainer())
                    {

                        //Smažu v db
                        var entity = db.DisciplinyKategories.Find(item.DisciplinyKategorieId);
                        if (entity != null)
                        {
                            db.DisciplinyKategories.Remove(entity);
                            this.ModelState.Clear();
                            EfStatus status = db.SaveChangesWithValidation();
                            if (!status.IsValid)
                            {
                                AddModelStateError(status);
                            }
                            else
                            {
                                DisciplinyKategoriesSessionRepository.Delete(item);
                            }
                        }
                    }
                }
            }

            //Rebind the grid
            return View(new GridModel(DisciplinyKategoriesSessionRepository.All()));
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