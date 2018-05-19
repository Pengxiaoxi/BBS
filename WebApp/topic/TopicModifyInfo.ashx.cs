using bbs.BLL;
using bbs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.topic
{
    /// <summary>
    /// TopicModifyInfo 的摘要说明
    /// </summary>
    public class TopicModifyInfo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            int topicId = Int32.Parse(context.Request["topicId"]);
            string title = context.Request["title"];
            string content = context.Request["content"];

            //string content1 = "<p>"+ content + "</p>";

            TopicService topicService = new TopicService();

            //先根据topicId查出所有信息 先装载在进行更新（只更新部分信息）
            Topic topicInfo = topicService.GetModel(topicId);

            //将更新后的title和conten赋值给topicInfo对象
            topicInfo.title = title;
            topicInfo.content = content;
            topicInfo.modifytime = DateTime.Now;   //修改时间

            bool b = topicService.Update(topicInfo);

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