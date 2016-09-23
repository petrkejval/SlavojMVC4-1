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
    public class KuzelnaProgramKategoriesGridController : Controller
    {
        // GET: KuzelnaProgramKategoriesGrid
        [SourceCodeFile("KuzelnaProgramKategorieEditable (model)", "~/Models/KuzelnaProgramKategorieEditable.cs")]
        [SourceCodeFile("KuzelnaProgramKategoriesSessionRepository", "~/Models/KuzelnaProgramKategoriesSessionRepository.cs")]
        public ActionResult KuzelnaProgramKategoriesGrid()
        {
            return View();
        }

        [GridAction]
        public ActionResult KuzelnaProgramKategoriesSelect()
        {

            return View(new GridModel(KuzelnaProgramKategoriesSessionRepository.All(true)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        [HandleError]
        public ActionResult KuzelnaProgramKategoriesUpdate(int id)
        {
            KuzelnaProgramKategorieEditable item = KuzelnaProgramKategoriesSessionRepository.One(p => p.KuzelnaProgramKategorieId == id);

            TryUpdateModel(item);
            //.........................................................................................................................................................
            if (ModelState.IsValid)
            {
                using (var db = new SlavojDBContainer())
                {

                    var entity = db.KuzelnaProgramKategories.Find(item.KuzelnaProgramKategorieId);
                    if (entity == null)
                    {
                        entity = new KuzelnaProgramKategorie();
                        entity.KuzelnaProgramKategorieId = item.KuzelnaProgramKategorieId;
                    }
                    entity.Nazev = item.Nazev;
                    entity.Barva = item.Barva;

                    //Nastaví všechna modifikovaná pole jako modifikovaná
                    db.SetModifyFields(entity);
                    //.................................................................................................................................
                    this.ModelState.Clear();
                    EfStatus status = db.SaveChangesWithValidation();
                    if (status.IsValid)
                    {
                        KuzelnaProgramKategoriesSessionRepository.Update(item);
                        return View(new GridModel(KuzelnaProgramKategoriesSessionRepository.All(true)));
                    }
                    else
                    {
                        AddModelStateError(status);
                    }

                }

            }
            return View(new GridModel(KuzelnaProgramKategoriesSessionRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult KuzelnaProgramKategoriesInsert()
        {
            //Create a new instance of the EditableProduct class.
            KuzelnaProgramKategorieEditable item = new KuzelnaProgramKategorieEditable();

            //Perform model binding (fill the product properties and validate it).
            if (TryUpdateModel(item))
            {
                if (ModelState.IsValid)
                {

                    using (var db = new SlavojDBContainer())
                    {
                        var entity = new SlavojMVC4_1.Models.KuzelnaProgramKategorie();

                        entity.Nazev = item.Nazev;
                        entity.Barva = item.Barva;


                        // Add the entity
                        db.KuzelnaProgramKategories.Add(entity);
                        //.................................................................................................
                        this.ModelState.Clear();
                        EfStatus status = db.SaveChangesWithValidation();
                        if (status.IsValid)
                        {
                            item.KuzelnaProgramKategorieId = entity.KuzelnaProgramKategorieId;
                            KuzelnaProgramKategoriesSessionRepository.Insert(item);
                            return View(new GridModel(KuzelnaProgramKategoriesSessionRepository.All(true)));
                        }
                        else
                        {
                            AddModelStateError(status);
                        }

                    }

                }
            }

            //Rebind the grid
            return View(new GridModel(KuzelnaProgramKategoriesSessionRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult KuzelnaProgramKategoriesDelete(int id)
        {
            //Find a customer with ProductID equal to the id action parameter
            KuzelnaProgramKategorieEditable item = KuzelnaProgramKategoriesSessionRepository.One(p => p.KuzelnaProgramKategorieId == id);

            if (item != null)
            {
                if (TryValidateModel(item))
                {
                    using (var db = new SlavojDBContainer())
                    {

                        //Smažu v db
                        var entity = db.KuzelnaProgramKategories.Find(item.KuzelnaProgramKategorieId);
                        if (entity != null)
                        {
                            db.KuzelnaProgramKategories.Remove(entity);
                            this.ModelState.Clear();
                            EfStatus status = db.SaveChangesWithValidation();
                            if (!status.IsValid)
                            {
                                AddModelStateError(status);
                            }
                            else
                            {
                                KuzelnaProgramKategoriesSessionRepository.Delete(item);
                            }
                        }
                    }
                }
            }

            //Rebind the grid
            return View(new GridModel(KuzelnaProgramKategoriesSessionRepository.All()));
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