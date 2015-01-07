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
    public class DisciplinyGridController : Controller
    {
        // GET: DisciplinyGrid
        [SourceCodeFile("DisciplinaEditable (model)", "~/Models/DisciplinaEditable.cs")]
        [SourceCodeFile("DisciplinySessionRepository", "~/Models/DisciplinySessionRepository.cs")]
        public ActionResult DisciplinyGrid()
        {
            return View();
        }

        [GridAction]
        public ActionResult DisciplinySelect()
        {

            return View(new GridModel(DisciplinySessionRepository.All(true)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        [HandleError]
        public ActionResult DisciplinyUpdate(int id)
        {
            DisciplinaEditable item = DisciplinySessionRepository.One(p => p.DisciplinaId == id);

            TryUpdateModel(item);
            //.........................................................................................................................................................
            if (ModelState.IsValid)
            {
                using (var db = new SlavojDBContainer())
                {

                    var entity = db.Discipliny.Find(item.DisciplinaId);
                    if (entity == null)
                    {
                        entity = new Disciplina();
                        entity.DisciplinaId = item.DisciplinaId;
                    }

                    entity.PocetHodu = item.PocetHodu;
                    entity.DisciplinyKategorieId = item.DisciplinyKategorieId;
                    entity.Popis = item.Popis;

                    //Nastaví všechna modifikovaná pole jako modifikovaná
                    db.SetModifyFields(entity);
                    //.................................................................................................................................
                    this.ModelState.Clear();
                    EfStatus status = db.SaveChangesWithValidation();
                    if (status.IsValid)
                    {
                        DisciplinySessionRepository.Update(item);
                        return View(new GridModel(DisciplinySessionRepository.All(true)));
                        //return RedirectToAction("DisciplinySelect", this.GridRouteValues());
                    }
                    else
                    {
                        AddModelStateError(status);
                    }

                }

            }
            return View(new GridModel(DisciplinySessionRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult DisciplinyInsert()
        {
            //Create a new instance of the EditableProduct class.
            DisciplinaEditable item = new DisciplinaEditable();

            //Perform model binding (fill the product properties and validate it).
            if (TryUpdateModel(item))
            {
                if (ModelState.IsValid)
                {

                    using (var db = new SlavojDBContainer())
                    {
                        var entity = new SlavojMVC4_1.Models.Disciplina();

                        entity.PocetHodu = item.PocetHodu;
                        entity.DisciplinyKategorieId = item.DisciplinyKategorieId;
                        entity.Popis = item.Popis;

                        // Add the entity
                        db.Discipliny.Add(entity);
                        //.................................................................................................
                        this.ModelState.Clear();
                        EfStatus status = db.SaveChangesWithValidation();
                        if (status.IsValid)
                        {
                            item.DisciplinaId = entity.DisciplinaId;
                            DisciplinySessionRepository.Insert(item);
                            return View(new GridModel(DisciplinySessionRepository.All(true)));
                        }
                        else
                        {
                            AddModelStateError(status);
                        }

                    }

                }
            }

            //Rebind the grid
            return View(new GridModel(DisciplinySessionRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult DisciplinyDelete(int id)
        {
            //Find a customer with ProductID equal to the id action parameter
            DisciplinaEditable item = DisciplinySessionRepository.One(p => p.DisciplinaId == id);

            if (item != null)
            {
                if (TryValidateModel(item))
                {
                    using (var db = new SlavojDBContainer())
                    {

                        //Smažu v db
                        var entity = db.Discipliny.Find(item.DisciplinaId);
                        if (entity != null)
                        {
                            db.Discipliny.Remove(entity);
                            this.ModelState.Clear();
                            EfStatus status = db.SaveChangesWithValidation();
                            if (!status.IsValid)
                            {
                                AddModelStateError(status);
                            }
                            else
                            {
                                DisciplinySessionRepository.Delete(item);
                            }
                        }
                    }
                }
            }

            //Rebind the grid
            return View(new GridModel(DisciplinySessionRepository.All()));
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