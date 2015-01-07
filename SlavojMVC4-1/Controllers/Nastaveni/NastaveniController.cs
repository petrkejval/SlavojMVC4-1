using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SlavojMVC4_1.Models;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SqlClient;
using System.Data;


namespace SlavojMVC4_1.Controllers.Nastaveni
{
    //public class ClenList
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }

    //}
    [Authorize(Roles = ("clen"))]
    public class NastaveniController : Controller
    {
        // GET: Cleni

        [Authorize(Roles = ("clen"))]
        public ActionResult Cleni()
        {
            ViewBag.ClenRoleListItem = new SlavojDBContainer().CleniRoles
                                     .Select(e => new { Id = e.ClenRoleId, Name = e.ClenRoleName })
                                     .OrderBy(e => e.Name);
            return View();
        }

        [Authorize(Roles = ("superuser"))]
        public ActionResult Druzstva()
        {
            ViewBag.DruzstvaCleniListItem = new SlavojDBContainer().Cleni
                                                 .Where(w => (w.JeClen && w.Registrace != null) || (w.ClenId == 0))
                                                 .Select(e => new { Id = e.ClenId, Name = e.Prijmeni + " " + e.Jmeno })
                                                 .OrderBy(e => e.Name);

            ViewBag.SoutezeListItem = new SlavojDBContainer().Souteze
                .Select(e => new { Id = e.SoutezId, Name = e.SoutezId != 0 ? e.Nazev + " " + "(" + e.KategorieSouteze.Nazev + ", " + e.PocetNutnychDrah + ")" : e.Nazev })
                                     .OrderBy(e => e.Id);

            ViewBag.TreneriListItem = new SlavojDBContainer().Cleni
                                                 .Where(w => (w.JeClen && w.Trener != null) || (w.ClenId == 0))
                                                 .Select(e => new { Id = e.ClenId, Name = e.Prijmeni + " " + e.Jmeno })
                                                 .OrderBy(e => e.Name);

            ViewBag.WebPagesListItem = new SlavojDBContainer().WebPages
                                                 .Select(e => new { Id = e.WebPageId, Name = e.Nazev })
                                                 .OrderBy(e => e.Name);
            return View();
        }

        [Authorize(Roles = ("superuser"))]
        public ActionResult Souteze()
        {
            ViewBag.KategorieSouteziListItem = new SlavojDBContainer().KategorieSoutezi
                                                 .Select(e => new { Id = e.KategorieSoutezeId, Name = e.Nazev })
                                                 .OrderBy(e => e.Name);

            ViewBag.DisciplinyListItem = new SlavojDBContainer().Discipliny
                                                 .Select(e => new { Id = e.DisciplinaId, Name = e.PocetHodu + " " + e.DisciplinyKategorie.Nazev })
                                                 .OrderBy(e => e.Name);
            return View();
        }

        [Authorize(Roles = ("superuser"))]
        public ActionResult KategorieSoutezi()
        {
            return View();
        }

        [Authorize(Roles = ("superuser"))]
        public ActionResult Rekordy()
        {
            ViewBag.RekordyDisciplinyListItem = new SlavojDBContainer().Discipliny
                                                 .Select(e => new { Id = e.DisciplinaId, Name = e.PocetHodu + " " + e.DisciplinyKategorie.Nazev })
                                                 .OrderBy(e => e.Name);

            ViewBag.RekordyKategorieListItem = new SlavojDBContainer().RekordyKategories
                                                 .Select(e => new { Id = e.RekordyKategorieId, Name = e.Nazev})
                                                 .OrderBy(e => e.Name);

            
            return View();
        }

        [Authorize(Roles = ("superuser"))]
        public ActionResult RekordyKategories()
        {
            return View();
        }

        [Authorize(Roles = ("superuser"))]
        public ActionResult Discipliny()
        {
            ViewBag.DisciplinyKategorieListItem = new SlavojDBContainer().DisciplinyKategories
                                                 .Select(e => new { Id = e.DisciplinyKategorieId, Name = e.Nazev })
                                                 .OrderBy(e => e.Name);
            return View();
        }

        [Authorize(Roles = ("superuser"))]
        public ActionResult DisciplinyKategories()
        {
            return View();
        }

        [Authorize(Roles = ("superuser"))]
        public ActionResult Turnaje()
        {
            ViewBag.RediteleTurnajuListItem = new SlavojDBContainer().Cleni
                                                 .Where(w => (w.JeClen) || (w.ClenId == 0))
                                                 .Select(e => new { Id = e.ClenId, Name = e.Prijmeni + " " + e.Jmeno })
                                                 .OrderBy(e => e.Name);
            
            ViewBag.WebPagesListItem = new SlavojDBContainer().WebPages
                                                 .Select(e => new { Id = e.WebPageId, Name = e.Nazev })
                                                 .OrderBy(e => e.Name);

            return View();
        }

        [Authorize(Roles = ("superuser"))]
        public ActionResult Kuzelny()
        {
            ViewBag.WebPagesListItem = new SlavojDBContainer().WebPages
                                                 .Select(e => new { Id = e.WebPageId, Name = e.Nazev })
                                                 .OrderBy(e => e.Name);

            return View();
        }

        [Authorize(Roles = ("superuser"))]
        public ActionResult Kluby()
        {
            ViewBag.WebPagesListItem = new SlavojDBContainer().WebPages
                                                 .Select(e => new { Id = e.WebPageId, Name = e.Nazev })
                                                 .OrderBy(e => e.Name);

            return View();
        }

        [Authorize(Roles = ("admin"))]
        public ActionResult Rocniky()
        {
            return View();
        }

        [Authorize(Roles = ("superuser"))]
        public ActionResult Vysledky()
        {
            ViewBag.RocnikyListItem = new SlavojDBContainer().Rocniky
                                                 .Select(e => new { Id = e.RocnikId, Name = e.Nazev })
                                                 .OrderBy(e => e.Name);
            ViewBag.SoutezeListItem = new SlavojDBContainer().Souteze
                                                 .Select(e => new { Id = e.SoutezId, Name = e.Nazev })
                                                 .OrderBy(e => e.Name);

            ViewBag.WebPagesListItem = new SlavojDBContainer().WebPages
                                                 .Select(e => new { Id = e.WebPageId, Name = e.Nazev })
                                                 .OrderBy(e => e.Name);
            return View();
        }

        [Authorize(Roles = ("superuser"))]
        public ActionResult WebPages()
        {
            return View();
        }


        [Authorize(Roles = ("admin"))]
        public ActionResult UserClen()
        {
            ViewBag.UserCleniUserListItem = new SlavojDBContainer().UserProfiles
                                                 .Select(e => new { Id = e.UserId, Name = e.UserName })
                                                 .OrderBy(e => e.Name);

            ViewBag.UserCleniClenListItem = new SlavojDBContainer().Cleni
                                                 .Select(e => new { Id = e.ClenId, Name = e.Prijmeni + " " + e.Jmeno })
                                                 .OrderBy(e => e.Name);

            ViewBag.RoleListItem = new SlavojDBContainer().webpages_Roles
                                     .Select(e => new { Id = e.RoleId, Name = e.RoleName })
                                     .OrderBy(e => e.Name);

            return View();
        }

        [Authorize(Roles = ("admin"))]
        public ActionResult ExportDat()
        {
            List<string> exportDat = new List<string>();
            List<string> dropContraints = new List<string>();
            List<string> addContraints = new List<string>();

            using (var db = new SlavojDBContainer())
            {
                ObjectContext objContext = ((IObjectContextAdapter)db).ObjectContext;
                MetadataWorkspace workspace = objContext.MetadataWorkspace;
                IEnumerable<EntityType> tables = workspace.GetItems<EntityType>(DataSpace.SSpace);
                dropContraints.Add("--ALTER TABLE DROP CONSTRAINT ALL---------------------------------------------------------------------------------");
                foreach (var table in tables)
                {
                    var tableNameParam = new SqlParameter("TableName", SqlDbType.VarChar);
                    tableNameParam.Value = table.Name;
                    var d = db.Database.SqlQuery<string>("sp_generate_drop_add_contraint 'dbo', @TableName, 'DROP'", tableNameParam).ToList<string>();
                    foreach (string line in d)
                    {
                        if (!dropContraints.Contains(line))
                        {
                            dropContraints.Add(line);
                        }
                    }
                }
                addContraints.Add("--ALTER TABLE ADD CONSTRAINT ALL---------------------------------------------------------------------------------");
                foreach (var table in tables)
                {
                    var tableNameParam = new SqlParameter("TableName", SqlDbType.VarChar);
                    tableNameParam.Value = table.Name;
                    var d = db.Database.SqlQuery<string>("sp_generate_drop_add_contraint 'dbo', @TableName, 'ADD'", tableNameParam).ToList<string>();
                    foreach (string line in d)
                    {
                        if (!addContraints.Contains(line))
                        {
                            addContraints.Add(line);
                        }
                    }
                }


                //exportDat.Add("--ALTER TABLE NOCHECK CONSTRAINT ALL---------------------------------------------------------------------------------");
                //foreach (var table in tables)
                //{
                //    exportDat.Add(String.Format("ALTER TABLE [{0}] NOCHECK CONSTRAINT ALL", table.Name));

                //}

                exportDat = exportDat.Concat(dropContraints).ToList();
                foreach (var table in tables)
                {
                    var tableNameParam = new SqlParameter("TableName", SqlDbType.VarChar);
                    tableNameParam.Value = table.Name;
                    var d = db.Database.SqlQuery<string>("sp_generate_inserts @table_name = @TableName, @ommit_identity = 0, @ommit_computed_cols = 1", tableNameParam).ToList<string>();
                    exportDat.Add(String.Format("--[{0}]-------------------------------------------------------------------------------------------------------", table.Name));
                    exportDat.Add(String.Format("ALTER TABLE [{0}] DISABLE TRIGGER ALL", table.Name));
                    exportDat.Add(String.Format("TRUNCATE TABLE [{0}]", table.Name));
                    //exportDat.Add(String.Format("DELETE FROM [{0}]", table.Name));
                    bool isIdentity = table.Properties.Where(w => w.StoreGeneratedPattern == StoreGeneratedPattern.Identity).Count() > 0;
                    if (isIdentity)
                    {
                        exportDat.Add(String.Format("SET IDENTITY_INSERT [{0}] ON", table.Name));

                    }

                    var result = exportDat.Concat(d);
                    exportDat = result.ToList();

                    if (isIdentity)
                    {
                        exportDat.Add(String.Format("SET IDENTITY_INSERT [{0}] OFF", table.Name));

                    }
                    exportDat.Add(String.Format("ALTER TABLE [{0}] ENABLE TRIGGER ALL", table.Name));

                }
                exportDat = exportDat.Concat(addContraints).ToList();

                //exportDat.Add("--ALTER TABLE CHECK CONSTRAINT ALL---------------------------------------------------------------------------------");
                //foreach (var table in tables)
                //{
                //    exportDat.Add(String.Format("ALTER TABLE [{0}] CHECK CONSTRAINT ALL", table.Name));

                //}


                //var categoriesTable = workspace.GetItem<EntityType>("SlavojDB.Store.Adresy", DataSpace.SSpace);
                //ViewBag.RowsAffected = db.Database.ExecuteSqlCommand("[sp_generate_inserts] cleni, @ommit_identity = 0, @disable_constraints = 0, @ommit_computed_cols = 1, @debug_mode = 0");
                //var d = db.Database.SqlQuery<string>("sp_generate_inserts cleni, @ommit_identity = 0, @disable_constraints = 0, @ommit_computed_cols = 1").ToList();

            }

            return View(exportDat);
        }
    }
}