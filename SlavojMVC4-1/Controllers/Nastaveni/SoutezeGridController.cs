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

namespace SlavojMVC4_1.Controllers
{
    public class SoutezeGridController : Controller
    {

        [SourceCodeFile("EditableSoutez (model)", "~/Models/EditableSoutez.cs")]
        [SourceCodeFile("SessionSoutezeRepository", "~/Models/SessionSoutezeRepository.cs")]
        public ActionResult SoutezeGrid()//AjaxEditing()
        {
            return View();
        }

        [GridAction]
        public ActionResult _SelectAjaxEditing()
        {

            return View(new GridModel(SessionSoutezeRepository.All(true)));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        [HandleError]
        public ActionResult _SaveAjaxEditing(int id, int KategorieSoutezeId)
        {
            EditableSoutez soutez = SessionSoutezeRepository.One(p => p.SoutezId == id);

            TryUpdateModel(soutez);
            //.........................................................................................................................................................
            if (ModelState.IsValid)
            {
                using (var db = new SlavojDBContainer())
                {

                    var entity = db.Souteze.Find(soutez.SoutezId);
                    if (entity == null)
                    {
                        entity = new Soutez();
                        entity.SoutezId = soutez.SoutezId;
                    }
                    entity.Nazev = soutez.Nazev;
                    entity.KategorieSoutezeId = soutez.KategorieSoutezeId;
                    entity.MinPocetHracu = soutez.MinPocetHracu;
                    entity.PocetNutnychDrah = soutez.PocetNutnychDrah;

                    //Nastaví všechna modifikovaná pole jako modifikovaná
                    db.SetModifyFields(entity);
                    //.................................................................................................................................
                    this.ModelState.Clear();
                    EfStatus status = db.SaveChangesWithValidation();
                    if (status.IsValid)
                    {
                        SessionSoutezeRepository.Update(soutez);
                    }
                    else
                    {
                        AddModelStateError(status);
                    }

                }

            }
            return View(new GridModel(SessionSoutezeRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertAjaxEditing()
        {
            //Create a new instance of the EditableProduct class.
            EditableSoutez soutez = new EditableSoutez();

            //Perform model binding (fill the product properties and validate it).
            if (TryUpdateModel(soutez))
            {
                if (ModelState.IsValid)
                {

                    using (var db = new SlavojDBContainer())
                    {
                        var entity = new SlavojMVC4_1.Models.Soutez();

                        entity.Nazev = soutez.Nazev;
                        entity.KategorieSoutezeId = soutez.KategorieSoutezeId;
                        entity.MinPocetHracu = soutez.MinPocetHracu;
                        entity.PocetNutnychDrah = soutez.PocetNutnychDrah;

                        // Add the entity
                        db.Souteze.Add(entity);
                        //.................................................................................................
                        this.ModelState.Clear();
                        EfStatus status = db.SaveChangesWithValidation();
                        if (status.IsValid)
                        {
                            soutez.SoutezId = entity.SoutezId;
                            SessionSoutezeRepository.Insert(soutez);
                        }
                        else
                        {
                            AddModelStateError(status);
                        }

                    }

                }
            }

            //Rebind the grid
            return View(new GridModel(SessionSoutezeRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteAjaxEditing(int id)
        {
            //Find a customer with ProductID equal to the id action parameter
            EditableSoutez soutez = SessionSoutezeRepository.One(p => p.SoutezId == id);

            if (soutez != null)
            {
                if (TryValidateModel(soutez))
                {
                    using (var db = new SlavojDBContainer())
                    {

                        //Smažu v db
                        var entity = db.Souteze.Find(soutez.SoutezId);
                        if (entity != null)
                        {
                            db.Souteze.Remove(entity);
                            this.ModelState.Clear();
                            EfStatus status = db.SaveChangesWithValidation();
                            if (!status.IsValid)
                            {
                                AddModelStateError(status);
                            }
                            else
                            {
                                SessionSoutezeRepository.Delete(soutez);
                            }
                        }
                    }
                }
            }

            //Rebind the grid
            return View(new GridModel(SessionSoutezeRepository.All()));
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