using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.admin
{
    /// <summary>
    /// AdminLoginOut 的摘要说明
    /// </summary>
    public class AdminLoginOut : IHttpHandler, System.Web.SessionState.IRequiresSessionState  //处理程序中使用session需要实现此接口
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            if (context.Session["adminuser"] != null)
            {
                context.Session["adminuser"] = null;   //清除session中的adminuser对象 

                context.Response.Redirect("/Index.aspx");
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