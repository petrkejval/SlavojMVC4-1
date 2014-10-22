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
    public class KategorieSouteziGridController : Controller
    {
        [SourceCodeFile("EditableKategorieSouteze (model)", "~/Models/EditableKategorieSouteze.cs")]
        [SourceCodeFile("SessionKategorieSoutezeRepository", "~/Models/SessionKategorieSoutezeRepository.cs")]
        public ActionResult EditingAjax()
        {
            return View();
        }

        [GridAction]
        public ActionResult _SelectAjaxEditing()
        {

            return View(new GridModel(SessionKategorieSoutezeRepository.All(true)));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        [HandleError]
        public ActionResult _SaveAjaxEditing(int id)
        {
            EditableKategorieSouteze kategorieSouteze = SessionKategorieSoutezeRepository.One(p => p.KategorieSoutezeId == id);

            TryUpdateModel(kategorieSouteze);
            //.........................................................................................................................................................
            if (ModelState.IsValid)
            {
                using (var db = new SlavojDBContainer())
                {

                    var entity = db.KategorieSoutezi.Find(kategorieSouteze.KategorieSoutezeId);
                    if (entity == null)
                    {
                        entity = new KategorieSouteze();
                        entity.KategorieSoutezeId = kategorieSouteze.KategorieSoutezeId;
                    }

                    entity.Nazev = kategorieSouteze.Nazev;

                    //Nastaví všechna modifikovaná pole jako modifikovaná
                    db.SetModifyFields(entity);
                    //.................................................................................................................................
                    this.ModelState.Clear();
                    EfStatus status = db.SaveChangesWithValidation();
                    if (status.IsValid)
                    {
                        SessionKategorieSoutezeRepository.Update(kategorieSouteze);
                    }
                    else
                    {
                        AddModelStateError(status);
                    }

                }

            }
            return View(new GridModel(SessionKategorieSoutezeRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertAjaxEditing()
        {
            //Create a new instance of the EditableProduct class.
            EditableKategorieSouteze kategorieSouteze = new EditableKategorieSouteze();
            
            //Perform model binding (fill the product properties and validate it).
            if (TryUpdateModel(kategorieSouteze))
            {
                if (ModelState.IsValid)
                {

                    using (var db = new SlavojDBContainer())
                    {
                        var entity = new SlavojMVC4_1.Models.KategorieSouteze();

                        entity.Nazev = kategorieSouteze.Nazev;

                        // Add the entity
                        db.KategorieSoutezi.Add(entity);
                        //.................................................................................................
                        this.ModelState.Clear();
                        EfStatus status = db.SaveChangesWithValidation();
                        if (status.IsValid)
                        {
                            kategorieSouteze.KategorieSoutezeId = entity.KategorieSoutezeId;
                            SessionKategorieSoutezeRepository.Insert(kategorieSouteze);
                        }
                        else
                        {
                            AddModelStateError(status);
                        }

                    }

                }
            }

            //Rebind the grid
            return View(new GridModel(SessionKategorieSoutezeRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteAjaxEditing(int id)
        {
            //Find a customer with ProductID equal to the id action parameter
            EditableKategorieSouteze kategorieSouteze = SessionKategorieSoutezeRepository.One(p => p.KategorieSoutezeId == id);

            if (kategorieSouteze != null)
            {
                if (TryValidateModel(kategorieSouteze))
                {
                    using (var db = new SlavojDBContainer())
                    {

                        //Smažu v db
                        var entity = db.KategorieSoutezi.Find(kategorieSouteze.KategorieSoutezeId);
                        if (entity != null)
                        {
                            db.KategorieSoutezi.Remove(entity);
                            this.ModelState.Clear();
                            EfStatus status = db.SaveChangesWithValidation();
                            if (!status.IsValid)
                            {
                                AddModelStateError(status);
                            }
                            else
                            {
                                SessionKategorieSoutezeRepository.Delete(kategorieSouteze);
                            }
                        }
                    }
                }
            }

            //Rebind the grid
            return View(new GridModel(SessionKategorieSoutezeRepository.All()));
        }


        //......................................................................................................................................................................
        private void AddModelStateError(EfStatus status)
        {
            //Naplnit chyb z db.KategorieSoutezi do this.ModelState
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