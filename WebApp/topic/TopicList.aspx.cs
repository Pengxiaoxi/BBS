using bbs.BLL;
using bbs.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.topic
{
    public partial class TopicList : System.Web.UI.Page
    {
        public Dictionary<int, Reply> topicLastReply { set; get; }
        public Dictionary<int, int> topicReplyCount { set; get; }
        public List<Topic> ptTopicList { set; get; }

        public Dictionary<int, Reply> zdtopicLastReply { set; get; }
        public Dictionary<int, int> zdtopicReplyCount { set; get; }
        public List<Topic> zdptTopicList { set; get; }

        public Section section { get; set; }
        public string pageCode { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //page
                int page = 1;
                if (!Int32.TryParse(Request["page"],out page))
                {
                    page = 1;
                }

                int sectionId = Int32.Parse(Request["sectionId"]);  //获取板块主键id

                TopicService topicService = new TopicService();

                //普通帖子取值方法,得到置顶帖子的信息
                ArrayList mylist = topicService.findTopic(sectionId, page);

                topicReplyCount = (Dictionary<int, int>)mylist[0];
                topicLastReply = (Dictionary<int, Reply>)mylist[1];
                ptTopicList = (List<Topic>)mylist[2];
                pageCode = mylist[3].ToString();
                section = (Section)mylist[4];


                //置顶帖子取值方法
                ArrayList myzdlist = topicService.findZdTopic(sectionId);
                zdtopicReplyCount = (Dictionary<int, int>)myzdlist[0];
                zdtopicLastReply = (Dictionary<int, Reply>)myzdlist[1];
                zdptTopicList = (List<Topic>)myzdlist[2];



                //foreach (Topic topic in ptTopicList)
                //{
                //    Response.Write("贴子标题:" + topic.title + "--");
                //    Response.Write("贴子作者:" + topic.topicuser.nickname + "--");
                //    if (topicReplyCount.ContainsKey(topic.id))
                //    {
                //        Response.Write("此贴回复数:" + topicReplyCount[topic.id] + "--");
                //    }
                //    if (topicLastReply.ContainsKey(topic.id))
                //    {
                //        Response.Write("最后回复时间:" + topicLastReply[topic.id].publishtime + "--");
                //        Response.Write("最后回复人:" + topicLastReply[topic.id].replyuser.nickname + "--");
                //    }
                //    Response.Write("<br>");
                //}

            }

        }
    }
}

