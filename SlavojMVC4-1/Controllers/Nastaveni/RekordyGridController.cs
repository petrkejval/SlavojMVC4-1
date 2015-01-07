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
    public class RekordyGridController : Controller
    {
        // GET: RekordyGrid
        [SourceCodeFile("RekordEditable (model)", "~/Models/RekordEditable.cs")]
        [SourceCodeFile("RekordySessionRepository", "~/Models/RekordySessionRepository.cs")]
        public ActionResult RekordyGrid()
        {
            return View();
        }

        [GridAction]
        public ActionResult RekordySelect()
        {

            return View(new GridModel(RekordySessionRepository.All(true)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        [HandleError]
        public ActionResult RekordyUpdate(int id)
        {
            RekordEditable item = RekordySessionRepository.One(p => p.RekordId == id);

            TryUpdateModel(item);
            //.........................................................................................................................................................
            if (ModelState.IsValid)
            {
                using (var db = new SlavojDBContainer())
                {

                    var entity = db.Rekordy.Find(item.RekordId);
                    if (entity == null)
                    {
                        entity = new Rekord();
                        entity.RekordId = item.RekordId;
                    }


                    entity.Nazev = item.Nazev;
                    entity.DisciplinaId = item.DisciplinaId;
                    entity.PocetHracu = item.PocetHracu;
                    entity.RekordyKategorieId = item.RekordyKategorieId;
                    entity.JeRegistrovan = item.JeRegistrovan;
                    entity.Nahoz = item.Nahoz;
                    entity.DatumNahozu = item.DatumNahozu;
                    entity.Popis = item.Popis;


                    //Nastaví všechna modifikovaná pole jako modifikovaná
                    db.SetModifyFields(entity);
                    //.................................................................................................................................
                    this.ModelState.Clear();
                    EfStatus status = db.SaveChangesWithValidation();
                    if (status.IsValid)
                    {
                        RekordySessionRepository.Update(item);
                        return View(new GridModel(RekordySessionRepository.All(true)));
                        //return RedirectToAction("RekordySelect", this.GridRouteValues());
                    }
                    else
                    {
                        AddModelStateError(status);
                    }

                }

            }
            return View(new GridModel(RekordySessionRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult RekordyInsert()
        {
            //Create a new instance of the EditableProduct class.
            RekordEditable item = new RekordEditable();

            //Perform model binding (fill the product properties and validate it).
            if (TryUpdateModel(item))
            {
                if (ModelState.IsValid)
                {

                    using (var db = new SlavojDBContainer())
                    {
                        var entity = new SlavojMVC4_1.Models.Rekord();

                        entity.Nazev = item.Nazev;
                        entity.DisciplinaId = item.DisciplinaId;
                        entity.PocetHracu = item.PocetHracu;
                        entity.RekordyKategorieId = item.RekordyKategorieId;
                        entity.JeRegistrovan = item.JeRegistrovan;
                        entity.Nahoz = item.Nahoz;
                        entity.DatumNahozu = item.DatumNahozu;
                        entity.Popis = item.Popis;


                        // Add the entity
                        db.Rekordy.Add(entity);
                        //.................................................................................................
                        this.ModelState.Clear();
                        EfStatus status = db.SaveChangesWithValidation();
                        if (status.IsValid)
                        {
                            item.RekordId = entity.RekordId;
                            RekordySessionRepository.Insert(item);
                            return View(new GridModel(RekordySessionRepository.All(true)));
                        }
                        else
                        {
                            AddModelStateError(status);
                        }

                    }

                }
            }

            //Rebind the grid
            return View(new GridModel(RekordySessionRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult RekordyDelete(int id)
        {
            //Find a customer with ProductID equal to the id action parameter
            RekordEditable item = RekordySessionRepository.One(p => p.RekordId == id);

            if (item != null)
            {
                if (TryValidateModel(item))
                {
                    using (var db = new SlavojDBContainer())
                    {

                        //Smažu v db
                        var entity = db.Rekordy.Find(item.RekordId);
                        if (entity != null)
                        {
                            db.Rekordy.Remove(entity);
                            this.ModelState.Clear();
                            EfStatus status = db.SaveChangesWithValidation();
                            if (!status.IsValid)
                            {
                                AddModelStateError(status);
                            }
                            else
                            {
                                RekordySessionRepository.Delete(item);
                            }
                        }
                    }
                }
            }

            //Rebind the grid
            return View(new GridModel(RekordySessionRepository.All()));
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