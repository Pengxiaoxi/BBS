using bbs.BLL;
using bbs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.topic
{
    /// <summary>
    /// TopicModify 的摘要说明
    /// </summary>
    public class TopicModify : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            //topicId: topicId,topicTop: topicTop,topicGood: topicGood
            int topicId = Int32.Parse(context.Request["topicId"]);
            string topicTop = context.Request["topicTop"];
            string topicGood = context.Request["topicGood"];

            TopicService topicService = new TopicService();

            Topic topic = new Topic();

            topic = topicService.GetModel(topicId);   //查询出帖子信息

            topic.top = topicTop;
            topic.good = topicGood;

            bool success = topicService.Update(topic);

            context.Response.Write(success);

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