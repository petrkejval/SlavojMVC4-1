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
    public class WebPagesGridController : Controller
    {
        // GET: WebPagesGrid
        [SourceCodeFile("WebPageEditable (model)", "~/Models/WebPageEditable.cs")]
        [SourceCodeFile("WebPagesSessionRepository", "~/Models/WebPagesSessionRepository.cs")]
        public ActionResult WebPagesGrid()
        {
            return View();
        }

        [GridAction]
        public ActionResult WebPagesSelect()
        {

            return View(new GridModel(WebPagesSessionRepository.All(true)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        [HandleError]
        public ActionResult WebPagesUpdate(int id)
        {
            WebPageEditable item = WebPagesSessionRepository.One(p => p.WebPageId == id);

            TryUpdateModel(item);
            //.........................................................................................................................................................
            if (ModelState.IsValid)
            {
                using (var db = new SlavojDBContainer())
                {

                    var entity = db.WebPages.Find(item.WebPageId);
                    if (entity == null)
                    {
                        entity = new WebPage();
                        entity.WebPageId = item.WebPageId;
                    }


                    entity.Nazev = item.Nazev;
                    entity.HtmlText = HttpUtility.HtmlDecode(item.HtmlText);


                    //Nastaví všechna modifikovaná pole jako modifikovaná
                    db.SetModifyFields(entity);
                    //.................................................................................................................................
                    this.ModelState.Clear();
                    EfStatus status = db.SaveChangesWithValidation();
                    if (status.IsValid)
                    {
                        WebPagesSessionRepository.Update(item);
                        return View(new GridModel(WebPagesSessionRepository.All(true)));
                        //return RedirectToAction("WebPagesSelect", this.GridRouteValues());
                    }
                    else
                    {
                        AddModelStateError(status);
                    }

                }

            }
            return View(new GridModel(WebPagesSessionRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult WebPagesInsert()
        {
            //Create a new instance of the EditableProduct class.
            WebPageEditable item = new WebPageEditable();

            //Perform model binding (fill the product properties and validate it).
            if (TryUpdateModel(item))
            {
                if (ModelState.IsValid)
                {

                    using (var db = new SlavojDBContainer())
                    {
                        var entity = new SlavojMVC4_1.Models.WebPage();

                        entity.Nazev = item.Nazev;
                        entity.HtmlText = HttpUtility.HtmlDecode(item.HtmlText);


                        // Add the entity
                        db.WebPages.Add(entity);
                        //.................................................................................................
                        this.ModelState.Clear();
                        EfStatus status = db.SaveChangesWithValidation();
                        if (status.IsValid)
                        {
                            item.WebPageId = entity.WebPageId;
                            WebPagesSessionRepository.Insert(item);
                            return View(new GridModel(WebPagesSessionRepository.All(true)));
                        }
                        else
                        {
                            AddModelStateError(status);
                        }

                    }

                }
            }

            //Rebind the grid
            return View(new GridModel(WebPagesSessionRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult WebPagesDelete(int id)
        {
            //Find a customer with ProductID equal to the id action parameter
            WebPageEditable item = WebPagesSessionRepository.One(p => p.WebPageId == id);

            if (item != null)
            {
                if (TryValidateModel(item))
                {
                    using (var db = new SlavojDBContainer())
                    {

                        //Smažu v db
                        var entity = db.WebPages.Find(item.WebPageId);
                        if (entity != null)
                        {
                            db.WebPages.Remove(entity);
                            this.ModelState.Clear();
                            EfStatus status = db.SaveChangesWithValidation();
                            if (!status.IsValid)
                            {
                                AddModelStateError(status);
                            }
                            else
                            {
                                WebPagesSessionRepository.Delete(item);
                            }
                        }
                    }
                }
            }

            //Rebind the grid
            return View(new GridModel(WebPagesSessionRepository.All()));
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