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
    public class UserCleniGridController : Controller
    {
        // GET: UserCleniGrid
        [SourceCodeFile("UserClenEditable (model)", "~/Models/UserClenEditable.cs")]
        [SourceCodeFile("UserCleniSessionRepository", "~/Models/UserCleniSessionRepository.cs")]
        public ActionResult UserCleniGrid()
        {
            return View();
        }

        [GridAction]
        public ActionResult UserCleniSelect()
        {

            return View(new GridModel(UserCleniSessionRepository.All(true)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        [HandleError]
        public ActionResult UserCleniUpdate(int id)
        {
            UserClenEditable item = UserCleniSessionRepository.One(p => p.UserClenId == id);

            TryUpdateModel(item);
            //.........................................................................................................................................................
            if (ModelState.IsValid)
            {
                using (var db = new SlavojDBContainer())
                {

                    var entity = db.UserCleni.Find(item.UserClenId);
                    if (entity == null)
                    {
                        entity = new UserClen();
                        entity.UserClenId = item.UserClenId;
                    }


                    entity.UserId = item.UserId;
                    entity.ClenId = item.ClenId;


                    //Nastaví všechna modifikovaná pole jako modifikovaná
                    db.SetModifyFields(entity);
                    //.................................................................................................................................
                    this.ModelState.Clear();
                    EfStatus status = db.SaveChangesWithValidation();
                    if (status.IsValid)
                    {
                        UserCleniSessionRepository.Update(item);
                        return View(new GridModel(UserCleniSessionRepository.All(true)));
                    }
                    else
                    {
                        AddModelStateError(status);
                    }

                }

            }
            return View(new GridModel(UserCleniSessionRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult UserCleniInsert()
        {
            //Create a new instance of the EditableProduct class.
            UserClenEditable item = new UserClenEditable();

            //Perform model binding (fill the product properties and validate it).
            if (TryUpdateModel(item))
            {
                if (ModelState.IsValid)
                {

                    using (var db = new SlavojDBContainer())
                    {
                        var entity = new SlavojMVC4_1.Models.UserClen();

                        entity.UserId = item.UserId;
                        entity.ClenId = item.ClenId;


                        //Toto zajistí trigger tiudUserClen
                        //var webpagesRole = db.webpages_Roles.Where(w => w.RoleName == "clen").FirstOrDefault();
                        //if (webpagesRole != null)
                        //{
                        //    var roleClen = new SlavojMVC4_1.Models.webpages_UsersInRoles();
                        //    roleClen.UserId = item.UserId;
                        //    roleClen.RoleId = webpagesRole.RoleId;
                        //    db.webpages_UsersInRoles.Add(roleClen);

                        //}
                        // Add the entity
                        db.UserCleni.Add(entity);

                        //.................................................................................................
                        this.ModelState.Clear();
                        EfStatus status = db.SaveChangesWithValidation();
                        if (status.IsValid)
                        {
                            item.UserClenId = entity.UserClenId;
                            UserCleniSessionRepository.Insert(item);
                            return View(new GridModel(UserCleniSessionRepository.All(true)));
                        }
                        else
                        {
                            AddModelStateError(status);
                        }

                    }

                }
            }

            //Rebind the grid
            return View(new GridModel(UserCleniSessionRepository.All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult UserCleniDelete(int id)
        {
            //Find a customer with ProductID equal to the id action parameter
            UserClenEditable item = UserCleniSessionRepository.One(p => p.UserClenId == id);

            if (item != null)
            {
                if (TryValidateModel(item))
                {
                    using (var db = new SlavojDBContainer())
                    {

                        //Smažu v db
                        var entity = db.UserCleni.Find(item.UserClenId);
                        if (entity != null)
                        {
                            //Toto zajistí trigger tiudUserClen
                            ////smazu uzivatelske role
                            //while (entity.UserProfile.webpages_UsersInRoles.Count > 0)
                            //{
                            //    var role = entity.UserProfile.webpages_UsersInRoles.First();
                            //    db.webpages_UsersInRoles.Remove(role);

                            //}
                            db.UserCleni.Remove(entity);
                           
                            this.ModelState.Clear();
                            EfStatus status = db.SaveChangesWithValidation();
                            if (!status.IsValid)
                            {
                                AddModelStateError(status);
                            }
                            else
                            {
                                UserCleniSessionRepository.Delete(item);
                            }
                        }
                    }
                }
            }

            //Rebind the grid
            return View(new GridModel(UserCleniSessionRepository.All()));
        }
        //.......................................................................................................................................................................
        [GridAction]
        public ActionResult RoleSelect(int userId)
        {
            return View(new GridModel(UserClenUserInRoleSessionRepository.All(userId, true)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        [HandleError]
        public ActionResult RoleUpdate(int id, int userId)
        {
            UserClenUserInRoleEditable item = UserClenUserInRoleSessionRepository.One(p => p.UserInRoleId == id, userId);
            var values = ModelState.Values;
            TryUpdateModel(item);
            //.........................................................................................................................................................
            if (ModelState.IsValid)
            {
                using (var db = new SlavojDBContainer())
                {
                    this.ModelState.Clear();
                    var entity = db.webpages_UsersInRoles.Find(item.UserInRoleId);
                    if (entity != null)
                    {
                        entity.RoleId = item.RoleId;
                        //.................................................................................................
                        EfStatus status = db.SaveChangesWithValidation();
                        if (status.IsValid)
                        {
                            UserClenUserInRoleSessionRepository.Insert(item, userId);
                        }
                        else
                        {
                            AddModelStateError(status);
                        }
                    }

                }

            }

            return View(new GridModel(UserClenUserInRoleSessionRepository.All(userId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        [HandleError]
        public ActionResult RoleInsert(int userId)
        {
            //Create a new instance of the EditableProduct class.
            using (var db = new SlavojDBContainer())
            {
                this.ModelState.Clear();
                UserClenUserInRoleEditable item = new UserClenUserInRoleEditable();
                //Perform model binding (fill the product properties and validate it).
                if (TryUpdateModel(item))
                {
                    if (ModelState.IsValid)
                    {

                        var entity = new SlavojMVC4_1.Models.webpages_UsersInRoles();
                        item.UserId = userId;
                        entity.UserId = item.UserId;
                        entity.RoleId = item.RoleId;

                        // Add the entity
                        db.webpages_UsersInRoles.Add(entity);
                        //.................................................................................................
                        EfStatus status = db.SaveChangesWithValidation();
                        if (status.IsValid)
                        {
                            item.UserInRoleId = entity.UserInRoleId;
                            UserClenUserInRoleSessionRepository.Insert(item, userId);
                        }
                        else
                        {
                            AddModelStateError(status);
                        }

                    }
                }

                //Rebind the grid
                return View(new GridModel(UserClenUserInRoleSessionRepository.All(userId)));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult RoleDelete(int id, int userId)
        {
            UserClenUserInRoleEditable item = UserClenUserInRoleSessionRepository.One(p => p.UserInRoleId == id, userId);

            if (item != null)
            {
                if (TryValidateModel(item))
                {
                    using (var db = new SlavojDBContainer())
                    {

                        this.ModelState.Clear();
                        var entity = db.webpages_UsersInRoles.Find(item.UserInRoleId);
                        if (entity != null)
                        {
                            db.webpages_UsersInRoles.Remove(entity);
                            EfStatus status = db.SaveChangesWithValidation();
                            if (status.IsValid)
                            {
                                UserClenUserInRoleSessionRepository.Delete(item, userId);
                            }
                            else
                            {
                                AddModelStateError(status);
                            }
                        }
                    }
                }
            }

            return View(new GridModel(UserClenUserInRoleSessionRepository.All(userId)));
        }
        //......................................................................................................................................................................
        private void AddModelStateError(EfStatus status)
        {
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