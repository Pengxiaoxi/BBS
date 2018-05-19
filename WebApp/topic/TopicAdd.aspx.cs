using bbs.BLL;
using bbs.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.topic
{
    public partial class TopicAdd : System.Web.UI.Page
    {
        public List<Section> sectionList { get; set; }

        public int sectionId { get; set; }

        public string ErrorMsg { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userInfo"] == null)
            {
                string url = Request.Url.ToString();
                Response.Redirect("/Login.aspx?url="+url);  
            }
            else
            {
                SectionService sectionService = new SectionService();

                //板块信息列表
                sectionList = sectionService.GetModelList("");

                sectionId = Int32.Parse(Request["sectionId"]);

                TopicService topicService = new TopicService();

                Topic topicInfo = new Topic();

                if (IsPostBack)
                {
                    sectionId = Int32.Parse(Request["sectionId"]);

                    topicInfo.t_u_id = ((bbs.Model.User)Session["userInfo"]).id;
                    topicInfo.t_s_id = Int32.Parse(Request.Form["topic.section.id"]);
                    topicInfo.content = Request.Form["topic.content"];
                    topicInfo.title = Request.Form["topic.title"];
                    topicInfo.publishtime = DateTime.Now;
                    topicInfo.good = "0";
                    topicInfo.top = "0";

                    int i = topicService.Add(topicInfo); //topic表的id值

                    if (i > 0)
                    {
                        topicInfo.id = i;   //板块主键ID
                        Session["topicInfo"] = topicInfo;
                        Response.Redirect("/topic/TopicList.aspx?sectionId="+ topicInfo.t_s_id);
                    }
                    else
                    {
                        ErrorMsg = "发布失败，请重新发布！";
                    }
                }
            }
        }
    }
}