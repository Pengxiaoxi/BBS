using bbs.BLL;
using bbs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.common
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Zone> zoneList { get; set; }

        public List<Topic> topicList { get; set; }

        public List<Section> sectionList { get; set; }


        ZoneService zoneService = new ZoneService();

        protected void Page_Load(object sender, EventArgs e)
        {
            //发帖板块选择
            zoneList = zoneService.GetZoneSectionList();


            //topicList = topicService.GetModelList();

            //SectionService sectionService = new SectionService();

            //sectionList = sectionService.GetModelList("");
            //foreach (Zone zone in zoneList)
            //{
            //    Response.Write("<h1>"+zone.name+ "</h1>");
            //        foreach (Section section in sectionList) {
            //            if (zone.id == section.t_z_id) { 
            //                Response.Write(section.name+"<br>");
            //        }
            //    }
            //}

        }
    }
}