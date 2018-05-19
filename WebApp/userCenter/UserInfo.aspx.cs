using bbs.Model;
using System;

namespace WebApp.userCenter
{
    public partial class UserInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((User)Session["userInfo"] == null)
            {
                string url = Request.Url.ToString();
                Response.Redirect("/Login.aspx?url="+url);
            }
        }
    }
}