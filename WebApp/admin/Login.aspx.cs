using bbs.BLL;
using bbs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Util;

namespace WebApp.admin.js
{
    public partial class Login : System.Web.UI.Page
    {
        public string nickName { get; set; }
        public string error { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
            {
                nickName = Request["user.nickName"];
                string passWord = MyMd5.GetMd5String(MyMd5.GetMd5String(Request["user.password"]));

                UserService userService = new UserService();

                List<User> user = userService.GetModelList("type = '3' and nickName='" + nickName + "'and passWord='" + passWord + "'");

                int i = user.Count();

                if (i > 0)
                {
                    Session["adminuser"] = user[0];

                    Response.Redirect("/admin/Main.aspx");

                }
                else
                {
                    error = "登录失败！";
                }
            }
        }
    }
}