using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SlavojMVC4_1.Models;

namespace SlavojMVC4_1.Models
{
    public class MainMenuSessionRepository : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);

            MainMenuRead(false);
            DruzstvaMenuRead(false);
            TurnajeMenuRead(false);

        }

        public static void MainMenuRead(bool refresh)
        {
            IList<MainMenu> sessionMainMenu = (IList<MainMenu>)System.Web.HttpContext.Current.Session["MainMenu"];
            if (sessionMainMenu == null || refresh)
            {
                System.Web.HttpContext.Current.Session["MainMenu"] = new SlavojDBContainer().MainMenus.ToList();
            }

        }

        public static void DruzstvaMenuRead(bool refresh)
        {
            IList<Druzstvo> sessionDruzstvaMenu = (IList<Druzstvo>)System.Web.HttpContext.Current.Session["DruzstvaMenu"];
            if (sessionDruzstvaMenu == null || refresh)
            {
                System.Web.HttpContext.Current.Session["DruzstvaMenu"] = new SlavojDBContainer().Druzstva.Where(w => (w.DruzstvoId > 0) && (w.Existuje)).ToList();
            }

        }

        public static void TurnajeMenuRead(bool refresh)
        {
            IList<Turnaj> sessionTurnajeMenu = (IList<Turnaj>)System.Web.HttpContext.Current.Session["TurnajeMenu"];
            if (sessionTurnajeMenu == null || refresh)
            {
                System.Web.HttpContext.Current.Session["TurnajeMenu"] = new SlavojDBContainer().Turnaje.Where(w => (w.TurnajId > 0) && (w.Existuje)).ToList();
            }

        }
    }
}