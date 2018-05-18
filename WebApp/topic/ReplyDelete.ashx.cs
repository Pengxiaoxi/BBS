using bbs.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.topic
{
    /// <summary>
    /// ReplyDelete 的摘要说明
    /// </summary>
    public class ReplyDelete : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            ReplyService replyService = new ReplyService();

            int replyId = Int32.Parse(context.Request["replyId"]);

            int topicId = Int32.Parse(context.Request["topicId"]);
            int pageNumber = Int32.Parse(context.Request["pageNumber"]);

            bool b = replyService.Delete(replyId);

            int i = replyService.CheckPage(topicId, pageNumber);

            string str = b + "," + i;

            context.Response.Write(str);

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