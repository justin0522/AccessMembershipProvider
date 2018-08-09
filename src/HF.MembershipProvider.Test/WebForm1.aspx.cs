using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HF.MembershipProvider.Test
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Membership.ValidateUser(this.TextBox1.Text.Trim(), this.TextBox2.Text.Trim()))
            {
                string str = "登陆成功";
            }

            if (!Roles.IsUserInRole(this.TextBox1.Text.Trim(), "admin"))
                Roles.AddUserToRole(this.TextBox1.Text.Trim(), "admin");
        }
    }
}