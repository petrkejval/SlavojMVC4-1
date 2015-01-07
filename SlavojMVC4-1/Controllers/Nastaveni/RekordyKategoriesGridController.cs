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

namespace SlavojMVC4_1.Controllers.Nastaveni
{
    public class RekordyKategoriesGridController : Controller
    {
        // GET: RekordyKategoriesGrid
        [SourceCodeFile("RekordyKategorieEditable (model)", "~/Models/RekordyKategorieEditable.cs")]
        [SourceCodeFile("RekordyKategoriesSessionRepository", "~/Models/RekordyKategoriesSessionRepository.cs")]
        public ActionResult RekordyKategoriesGrid()
        {
            return View();
        }

        [GridAction]
        public ActionResult RekordyKategoriesSelect()
        {

            return View(new GridModel(RekordyKategoriesSessionRepository.All(true)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        [HandleError]
        public ActionResult RekordyKategoriesUpdate(int id)
        {
            RekordyKategorieEditable item = RekordyKategoriesSessionRepository.One(p => p.RekordyKategorieId == id);

            TryUpdateModel(item);
            //.........................................................................................................................................................
            if (ModelState.IsValid)
            {
                using (var db = new SlavojDBContainer())
                {

                    var entity = db.RekordyKategories.Find(item.RekordyKategorieId);
                    if (entity == null)
                    {
                        entity = new RekordyKategorie();
                        entity.RekordyKategorieId = item.RekordyKategorieId;
                    }

                    entity.Nazev = item.Nazev;

                    //Nastaví všechna modifikovaná pole jako modifikovaná
                    db.SetModifyFields(entity);
                    //.................................................................................................................................
                    this.ModelState.Clear();
                    EfStatus status = db.SaveChangesWithValidation();
                    if (status.IsValid)
                    {
                        RekordyKategoriesSessionRepository.Update(item);
                        return View(new GridModel(RekordyKategoriesSessionRepository.All(true)));
                        //return RedirectToAction("RekordyKategoriesSelect", this.GridRouteValues());
                    }
                    else
                    {
                        AddModelStateError(status);
                    }

                }

            }
            return View(new GridModel(RekordyKategoriesSessionRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult RekordyKategoriesInsert()
        {
            //Create a new instance of the EditableProduct class.
            RekordyKategorieEditable item = new RekordyKategorieEditable();

            //Perform model binding (fill the product properties and validate it).
            if (TryUpdateModel(item))
            {
                if (ModelState.IsValid)
                {

                    using (var db = new SlavojDBContainer())
                    {
                        var entity = new SlavojMVC4_1.Models.RekordyKategorie();

                        entity.Nazev = item.Nazev;

                        // Add the entity
                        db.RekordyKategories.Add(entity);
                        //.................................................................................................
                        this.ModelState.Clear();
                        EfStatus status = db.SaveChangesWithValidation();
                        if (status.IsValid)
                        {
                            item.RekordyKategorieId = entity.RekordyKategorieId;
                            RekordyKategoriesSessionRepository.Insert(item);
                            return View(new GridModel(RekordyKategoriesSessionRepository.All(true)));
                        }
                        else
                        {
                            AddModelStateError(status);
                        }

                    }

                }
            }

            //Rebind the grid
            return View(new GridModel(RekordyKategoriesSessionRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult RekordyKategoriesDelete(int id)
        {
            //Find a customer with ProductID equal to the id action parameter
            RekordyKategorieEditable item = RekordyKategoriesSessionRepository.One(p => p.RekordyKategorieId == id);

            if (item != null)
            {
                if (TryValidateModel(item))
                {
                    using (var db = new SlavojDBContainer())
                    {

                        //Smažu v db
                        var entity = db.RekordyKategories.Find(item.RekordyKategorieId);
                        if (entity != null)
                        {
                            db.RekordyKategories.Remove(entity);
                            this.ModelState.Clear();
                            EfStatus status = db.SaveChangesWithValidation();
                            if (!status.IsValid)
                            {
                                AddModelStateError(status);
                            }
                            else
                            {
                                RekordyKategoriesSessionRepository.Delete(item);
                            }
                        }
                    }
                }
            }

            //Rebind the grid
            return View(new GridModel(RekordyKategoriesSessionRepository.All()));
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