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
    public partial class SectionList : System.Web.UI.Page
    {
        public List<Section> sectionList { get; set; }

        public string pageCode { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            int pageNumber = 1;

            if (!Int32.TryParse(Request["page"],out pageNumber))
            {
                pageNumber = 1;
            }

            SectionService sectionService = new SectionService();

            sectionList = sectionService.FindAllSection(pageNumber);

            pageCode = PageUtil.genPagination("/admin/SectionList.aspx", sectionService.GetRecordCount(""), pageNumber, sectionService.pageCount, "");

        }
    }
}