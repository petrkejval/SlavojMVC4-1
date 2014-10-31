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
    public class TurnajeGridController : Controller
    {
        // GET: TurnajeGrid
        [SourceCodeFile("TurnajEditable (model)", "~/Models/TurnajEditable.cs")]
        [SourceCodeFile("TurnajeSessionRepository", "~/Models/TurnajeSessionRepository.cs")]
        public ActionResult TurnajeGrid()
        {
            return View();
        }

        [GridAction]
        public ActionResult TurnajeSelect()
        {

            return View(new GridModel(TurnajeSessionRepository.All(true)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        [HandleError]
        public ActionResult TurnajeUpdate(int id)
        {
            TurnajEditable item = TurnajeSessionRepository.One(p => p.TurnajId == id);

            TryUpdateModel(item);
            //.........................................................................................................................................................
            if (ModelState.IsValid)
            {
                using (var db = new SlavojDBContainer())
                {

                    var entity = db.Turnaje.Find(item.TurnajId);
                    if (entity == null)
                    {
                        entity = new Turnaj();
                        entity.TurnajId = item.TurnajId;
                    }


                    entity.Nazev = item.Nazev;
                    entity.ReditelTurnajeId = item.ReditelTurnajeId;
                    entity.DatumOd = item.DatumOd;
                    entity.DatumDo = item.DatumDo;
                    entity.WebPageId = item.WebPageId;
                    entity.Obrazek = item.Obrazek;
                    entity.Existuje = item.Existuje;


                    //Nastaví všechna modifikovaná pole jako modifikovaná
                    db.SetModifyFields(entity);
                    //.................................................................................................................................
                    this.ModelState.Clear();
                    EfStatus status = db.SaveChangesWithValidation();
                    if (status.IsValid)
                    {
                        TurnajeSessionRepository.Update(item);
                        return View(new GridModel(TurnajeSessionRepository.All(true)));
                        //return RedirectToAction("TurnajeSelect", this.GridRouteValues());
                    }
                    else
                    {
                        AddModelStateError(status);
                    }

                }

            }
            return View(new GridModel(TurnajeSessionRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult TurnajeInsert()
        {
            //Create a new instance of the EditableProduct class.
            TurnajEditable item = new TurnajEditable();

            //Perform model binding (fill the product properties and validate it).
            if (TryUpdateModel(item))
            {
                if (ModelState.IsValid)
                {

                    using (var db = new SlavojDBContainer())
                    {
                        var entity = new SlavojMVC4_1.Models.Turnaj();

                        entity.Nazev = item.Nazev;
                        entity.ReditelTurnajeId = item.ReditelTurnajeId;
                        entity.DatumOd = item.DatumOd;
                        entity.DatumDo = item.DatumDo;
                        entity.WebPageId = item.WebPageId;
                        entity.Obrazek = item.Obrazek;
                        entity.Existuje = item.Existuje;


                        // Add the entity
                        db.Turnaje.Add(entity);
                        //.................................................................................................
                        this.ModelState.Clear();
                        EfStatus status = db.SaveChangesWithValidation();
                        if (status.IsValid)
                        {
                            item.TurnajId = entity.TurnajId;
                            TurnajeSessionRepository.Insert(item);
                            return View(new GridModel(TurnajeSessionRepository.All(true)));
                        }
                        else
                        {
                            AddModelStateError(status);
                        }

                    }

                }
            }

            //Rebind the grid
            return View(new GridModel(TurnajeSessionRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult TurnajeDelete(int id)
        {
            //Find a customer with ProductID equal to the id action parameter
            TurnajEditable item = TurnajeSessionRepository.One(p => p.TurnajId == id);

            if (item != null)
            {
                if (TryValidateModel(item))
                {
                    using (var db = new SlavojDBContainer())
                    {

                        //Smažu v db
                        var entity = db.Turnaje.Find(item.TurnajId);
                        if (entity != null)
                        {
                            db.Turnaje.Remove(entity);
                            this.ModelState.Clear();
                            EfStatus status = db.SaveChangesWithValidation();
                            if (!status.IsValid)
                            {
                                AddModelStateError(status);
                            }
                            else
                            {
                                TurnajeSessionRepository.Delete(item);
                            }
                        }
                    }
                }
            }

            //Rebind the grid
            return View(new GridModel(TurnajeSessionRepository.All()));
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