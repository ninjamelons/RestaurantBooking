using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UserWebClient.Controllers
{
    public class AdminController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool admin = true;

            if (Session["roleId"] == null)
                admin = false;
            if(admin)
                if (Session["roleId"].ToString() != "1")
                    admin = false;

            if (!admin)
            {
                filterContext.Result = RedirectToAction("Index", "Home"); ; ; ; ; ; ; ; ; ; ; ; ; ; ; ;
            }
            else
                base.OnActionExecuting(filterContext);
        }
    }
}