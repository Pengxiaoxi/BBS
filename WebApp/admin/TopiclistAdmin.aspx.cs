using bbs.BLL;
using bbs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Util;

namespace WebApp.admin
{
    public partial class TopiclistAdmin : System.Web.UI.Page
    {
        public List<Topic> topicList { get; set; }

        public string pageCode { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            int pageNumber = 1;

            if (!Int32.TryParse(Request["page"], out pageNumber))
            {
                pageNumber = 1;
            }

            TopicService topicService = new TopicService();

            //根据pageNumber查询出topicList
            topicList = topicService.FindAllTopic(pageNumber);

            //生成分页链接
            pageCode = PageUtil.genPagination("/admin/TopiclistAdmin.aspx", topicService.GetRecordCount(""), pageNumber, topicService.pageSize, "");
        }
    }
}