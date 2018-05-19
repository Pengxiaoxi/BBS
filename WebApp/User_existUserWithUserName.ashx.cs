using bbs.BLL;
using bbs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp
{
    /// <summary>
    /// User_existUserWithUserName 的摘要说明
    /// </summary>
    public class User_existUserWithUserName : IHttpHandler
    {
        public string ErrorMsg { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            string nickName = context.Request["nickName"];

            UserService userService = new UserService();

            List<User> user = userService.GetModelList("nickName='"+nickName+"'");

            if (user.Count() > 0)
            {
                context.Response.Write(false);  //用户名已存在
            }
            else
            {
                context.Response.Write(true);  //用户名可以使用
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