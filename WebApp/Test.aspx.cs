using bbs.BLL;
using bbs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{
    public partial class Test : System.Web.UI.Page
    {
        public List<Zone> zoneList { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                ZoneService zoneService = new ZoneService();
                //string name = "蛋炒饭";
                //string description = "蛋炒饭不加蛋";     name='" + name + "' and description = '" + description + "'
                zoneList = zoneService.GetModelList("");
            }

        }
    }
}