using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp
{
    /// <summary>
    /// LoginOut 的摘要说明
    /// </summary>
    public class LoginOut : IHttpHandler, System.Web.SessionState.IRequiresSessionState //处理程序中使用session需要实现此接口
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            if (context.Session["userInfo"] != null)
            {
                context.Session["userInfo"] = null;   //清除session中的userinfo对象 

                context.Response.Cookies["cp1"].Expires = DateTime.Now.AddDays(-1);   //删除本地的cookie
                context.Response.Cookies["cp2"].Expires = DateTime.Now.AddDays(-1);

                //退出到登录页
                context.Response.Redirect("/Login.aspx");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}