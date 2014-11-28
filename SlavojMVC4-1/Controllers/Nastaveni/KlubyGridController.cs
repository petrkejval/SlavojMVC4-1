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
    public class KlubyGridController : Controller
    {
        // GET: KlubyGrid
        [SourceCodeFile("KlubEditable (model)", "~/Models/KlubEditable.cs")]
        [SourceCodeFile("KlubySessionRepository", "~/Models/KlubySessionRepository.cs")]
        public ActionResult KlubyGrid()
        {
            return View();
        }

        [GridAction]
        public ActionResult KlubySelect()
        {

            return View(new GridModel(KlubySessionRepository.All(true)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        [HandleError]
        public ActionResult KlubyUpdate(int id)
        {
            KlubEditable item = KlubySessionRepository.One(p => p.KlubId == id);

            TryUpdateModel(item);
            //.........................................................................................................................................................
            if (ModelState.IsValid)
            {
                using (var db = new SlavojDBContainer())
                {

                    var entity = db.Kluby.Find(item.KlubId);
                    if (entity == null)
                    {
                        entity = new Klub();
                        entity.KlubId = item.KlubId;
                    }

                    entity.Nazev = item.Nazev;
                    entity.IC = item.IC;
                    entity.CisloUctu = item.CisloUctu;
                    entity.Mail = item.Mail;
                    entity.WWW = item.WWW;
                    entity.KodKlubu = item.KodKlubu;
                    entity.Image = item.Image;
                    entity.WebPageId = item.WebPageId;

                    //Nastaví všechna modifikovaná pole jako modifikovaná
                    db.SetModifyFields(entity);
                    //.................................................................................................................................
                    this.ModelState.Clear();
                    EfStatus status = db.SaveChangesWithValidation();
                    if (status.IsValid)
                    {
                        KlubySessionRepository.Update(item);
                        return View(new GridModel(KlubySessionRepository.All(true)));
                    }
                    else
                    {
                        AddModelStateError(status);
                    }

                }

            }
            return View(new GridModel(KlubySessionRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult KlubyInsert()
        {
            //Create a new instance of the EditableProduct class.
            KlubEditable item = new KlubEditable();

            //Perform model binding (fill the product properties and validate it).
            if (TryUpdateModel(item))
            {
                if (ModelState.IsValid)
                {

                    using (var db = new SlavojDBContainer())
                    {
                        var entity = new SlavojMVC4_1.Models.Klub();

                        entity.Nazev = item.Nazev;
                        entity.IC = item.IC;
                        entity.CisloUctu = item.CisloUctu;
                        entity.Mail = item.Mail;
                        entity.WWW = item.WWW;
                        entity.KodKlubu = item.KodKlubu;
                        entity.Image = item.Image;
                        entity.WebPageId = item.WebPageId;


                        // Add the entity
                        db.Kluby.Add(entity);
                        //.................................................................................................
                        this.ModelState.Clear();
                        EfStatus status = db.SaveChangesWithValidation();
                        if (status.IsValid)
                        {
                            item.KlubId = entity.KlubId;
                            KlubySessionRepository.Insert(item);
                            return View(new GridModel(KlubySessionRepository.All(true)));
                        }
                        else
                        {
                            AddModelStateError(status);
                        }

                    }

                }
            }

            //Rebind the grid
            return View(new GridModel(KlubySessionRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult KlubyDelete(int id)
        {
            //Find a customer with ProductID equal to the id action parameter
            KlubEditable item = KlubySessionRepository.One(p => p.KlubId == id);

            if (item != null)
            {
                if (TryValidateModel(item))
                {
                    using (var db = new SlavojDBContainer())
                    {

                        //Smažu v db
                        var entity = db.Kluby.Find(item.KlubId);
                        if (entity != null)
                        {
                            db.Kluby.Remove(entity);
                            this.ModelState.Clear();
                            EfStatus status = db.SaveChangesWithValidation();
                            if (!status.IsValid)
                            {
                                AddModelStateError(status);
                            }
                            else
                            {
                                KlubySessionRepository.Delete(item);
                            }
                        }
                    }
                }
            }

            //Rebind the grid
            return View(new GridModel(KlubySessionRepository.All()));
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