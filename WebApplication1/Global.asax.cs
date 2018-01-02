using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace WebApplication1
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            if (!Roles.RoleExists("admin"))
                Roles.CreateRole("admin");

            if (!Roles.RoleExists("member"))
                Roles.CreateRole("member");
        }
    }
}