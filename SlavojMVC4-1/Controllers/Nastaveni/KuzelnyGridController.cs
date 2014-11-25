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
    public class KuzelnyGridController : Controller
    {
        // GET: KuzelnyGrid
        [SourceCodeFile("KuzelnaEditable (model)", "~/Models/KuzelnaEditable.cs")]
        [SourceCodeFile("KuzelnySessionRepository", "~/Models/KuzelnySessionRepository.cs")]
        public ActionResult KuzelnyGrid()
        {
            return View();
        }

        [GridAction]
        public ActionResult KuzelnySelect()
        {

            return View(new GridModel(KuzelnySessionRepository.All(true)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        [HandleError]
        public ActionResult KuzelnyUpdate(int id)
        {
            KuzelnaEditable item = KuzelnySessionRepository.One(p => p.KuzelnaId == id);

            TryUpdateModel(item);
            //.........................................................................................................................................................
            if (ModelState.IsValid)
            {
                using (var db = new SlavojDBContainer())
                {

                    var entity = db.Kuzelny.Find(item.KuzelnaId);
                    if (entity == null)
                    {
                        entity = new Kuzelna();
                        entity.KuzelnaId = item.KuzelnaId;
                    }


                    entity.Ulice = item.Ulice;
                    entity.CisloPopisne = item.CisloPopisne;
                    entity.Obec = item.Obec;
                    entity.Psc = item.Psc;
                    entity.KolaudacePlatnaDo = item.KolaudacePlatnaDo;
                    entity.Mapa = item.Mapa;
                    entity.StreeView = item.StreeView;
                    entity.GPS = item.GPS;
                    entity.WebPageId = item.WebPageId;
                    entity.Image = item.Image;
                    entity.LinkKuzelkyCz = item.LinkKuzelkyCz;

                    //Nastaví všechna modifikovaná pole jako modifikovaná
                    db.SetModifyFields(entity);
                    //.................................................................................................................................
                    this.ModelState.Clear();
                    EfStatus status = db.SaveChangesWithValidation();
                    if (status.IsValid)
                    {
                        KuzelnySessionRepository.Update(item);
                        return View(new GridModel(KuzelnySessionRepository.All(true)));
                    }
                    else
                    {
                        AddModelStateError(status);
                    }

                }

            }
            return View(new GridModel(KuzelnySessionRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult KuzelnyInsert()
        {
            //Create a new instance of the EditableProduct class.
            KuzelnaEditable item = new KuzelnaEditable();

            //Perform model binding (fill the product properties and validate it).
            if (TryUpdateModel(item))
            {
                if (ModelState.IsValid)
                {

                    using (var db = new SlavojDBContainer())
                    {
                        var entity = new SlavojMVC4_1.Models.Kuzelna();

                        entity.Ulice = item.Ulice;
                        entity.CisloPopisne = item.CisloPopisne;
                        entity.Obec = item.Obec;
                        entity.Psc = item.Psc;
                        entity.KolaudacePlatnaDo = item.KolaudacePlatnaDo;
                        entity.Mapa = item.Mapa;
                        entity.StreeView = item.StreeView;
                        entity.GPS = item.GPS;
                        entity.WebPageId = item.WebPageId;
                        entity.Image = item.Image;
                        entity.LinkKuzelkyCz = item.LinkKuzelkyCz;


                        // Add the entity
                        db.Kuzelny.Add(entity);
                        //.................................................................................................
                        this.ModelState.Clear();
                        EfStatus status = db.SaveChangesWithValidation();
                        if (status.IsValid)
                        {
                            item.KuzelnaId = entity.KuzelnaId;
                            KuzelnySessionRepository.Insert(item);
                            return View(new GridModel(KuzelnySessionRepository.All(true)));
                        }
                        else
                        {
                            AddModelStateError(status);
                        }

                    }

                }
            }

            //Rebind the grid
            return View(new GridModel(KuzelnySessionRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult KuzelnyDelete(int id)
        {
            //Find a customer with ProductID equal to the id action parameter
            KuzelnaEditable item = KuzelnySessionRepository.One(p => p.KuzelnaId == id);

            if (item != null)
            {
                if (TryValidateModel(item))
                {
                    using (var db = new SlavojDBContainer())
                    {

                        //Smažu v db
                        var entity = db.Kuzelny.Find(item.KuzelnaId);
                        if (entity != null)
                        {
                            db.Kuzelny.Remove(entity);
                            this.ModelState.Clear();
                            EfStatus status = db.SaveChangesWithValidation();
                            if (!status.IsValid)
                            {
                                AddModelStateError(status);
                            }
                            else
                            {
                                KuzelnySessionRepository.Delete(item);
                            }
                        }
                    }
                }
            }

            //Rebind the grid
            return View(new GridModel(KuzelnySessionRepository.All()));
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