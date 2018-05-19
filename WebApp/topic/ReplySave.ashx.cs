using bbs.BLL;
using bbs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.topic
{
    /// <summary>
    /// ReplySave 的摘要说明
    /// </summary>
    public class ReplySave : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            int userId = Int32.Parse(context.Request["reply.user.id"]);
            int TopicId = Int32.Parse(context.Request["reply.topic.id"]);
            string content = context.Request["reply.content"];

            Reply reply = new Reply();
            reply.t_u_id = userId;
            reply.t_t_id = TopicId;
            reply.content = content;
            reply.publishtime = DateTime.Now;

            ReplyService replyService = new ReplyService();

            bool b;
            if (replyService.Add(reply) >0 )
            {
                b = true;
            }
            else
            {
                b = false;
            }

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