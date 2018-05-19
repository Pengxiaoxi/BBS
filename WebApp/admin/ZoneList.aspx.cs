using bbs.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bbs.Model;
using Util;

namespace WebApp.admin
{
    public partial class ZoneList : System.Web.UI.Page
    {
        public List<Zone> zoneList { get; set; }

        public string pageCode { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int pageNumber = 1;
                if (!Int32.TryParse(Request["page"], out pageNumber))
                {
                    pageNumber = 1;
                }

                ZoneService zoneService = new ZoneService();

                zoneList = zoneService.FindAllZone(pageNumber);

                pageCode = PageUtil.genPagination("/admin/ZoneList.aspx",zoneService.GetRecordCount(""),pageNumber,zoneService.pageCount,"");
            }

        }
    }
}