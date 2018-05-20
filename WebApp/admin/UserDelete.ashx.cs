using bbs.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.admin
{
    /// <summary>
    /// UserDelete 的摘要说明
    /// </summary>
    public class UserDelete : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            int userId = Int32.Parse(context.Request["userId"]);

            //通过用户Id删除用户的所有回帖
            ReplyService replyService = new ReplyService();
            bool b = replyService.DeleteByUid(userId);

            //通过用户Id删除用户的所有发帖
            TopicService topicService = new TopicService();
            bool b1 = topicService.DeleteByUid(userId);

            //通过用户Id删除用户信息
            UserService userService = new UserService();
            bool b2 = userService.Delete(userId);

            if (b == true && b1 == true && b2 == true)
            {
                b2 = true;
            }
            else
            {
                b2 = false;
            }

            context.Response.Write(b2);
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