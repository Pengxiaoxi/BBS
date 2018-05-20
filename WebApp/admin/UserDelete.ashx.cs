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

            ReplyService replyService = new ReplyService();
            TopicService topicService = new TopicService();
            UserService userService = new UserService();

            if (context.Request["userId"] != null)   //删除单个用户信息
            {
                int userId = Int32.Parse(context.Request["userId"]);

                //通过用户Id删除用户的所有回帖
                bool b = replyService.DeleteByUid(userId);

                //通过用户Id删除用户的所有发帖 
                bool b1 = topicService.DeleteByUid(userId);

                //通过用户Id删除用户信息
                bool b2 = userService.Delete(userId);

                context.Response.Write(b2);
                context.Response.End();

            }
            else //批量删除用户信息
            {
                string idList = context.Request["ids"];

                //通过外键用户Id批量删除回帖信息
                bool b = replyService.DeleteListByUid(idList);

                // 通过用户Id批量删除用户的所有发帖
                bool b1 = topicService.DeleteListByUid(idList);

                //通过用户Id批量删除用户信息
                bool b2 = userService.DeleteList(idList);

                context.Response.Write(b2);
                context.Response.End();

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