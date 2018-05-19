using bbs.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.topic
{
    /// <summary>
    /// TopicDelete 的摘要说明
    /// </summary>
    public class TopicDelete : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            
            //获取帖子ID
            int topicId = Int32.Parse(context.Request["topicId"]);

            //先删除回帖
            ReplyService replyService = new ReplyService();

            bool r = replyService.Delete(topicId);

            //删除主贴
            TopicService topicService = new TopicService();

            bool b = topicService.Delete(topicId);

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