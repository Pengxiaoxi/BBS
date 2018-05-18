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
    public partial class TopicDetails : System.Web.UI.Page
    {
        public Topic topic { get; set; }

        public List<Reply> replyList { get; set; }

        public string pageCode { get; set; }

        public Section section { get; set; }

        public int page = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //防止请求的page非法
                
                if (!Int32.TryParse(Request["page"], out page))
                {
                    page = 1;
                }

                int topicid = Int32.Parse(Request["topicId"]);

                ReplyService replyService = new ReplyService();

                ArrayList arrayList = replyService.finfTopicById(topicid, page);

                topic = (Topic)arrayList[0];
                replyList = (List<Reply>)arrayList[1];
                pageCode = (string)arrayList[2];
                section = (Section)arrayList[3];


            }
            else
            {

            }
        }
    }
}