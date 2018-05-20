using bbs.BLL;
using bbs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Util;

namespace WebApp.admin
{
    /// <summary>
    /// UserSave 的摘要说明
    /// </summary>
    public class UserSave : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            int userId = Int32.Parse(context.Request["user.id"]);

            UserService userService = new UserService();

            User user = userService.GetModel(userId);

            //判断是否输入新的密码，输入则更新密码，否则使用原密码
            string passWord = context.Request["user.password"];

            if (passWord != "")  
            {
                user.password = MyMd5.GetMd5String(MyMd5.GetMd5String(passWord));
            }

            user.nickname = context.Request["user.nickName"];
            user.truename = context.Request["user.trueName"];
            user.sex = context.Request["user.sex"];
            user.email = context.Request["user.email"];
            user.mobile = context.Request["user.mobile"];

            bool b = userService.Update(user);

            context.Response.Write(b);
            context.Response.End();
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