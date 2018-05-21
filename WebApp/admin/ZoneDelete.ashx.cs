using bbs.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.admin
{
    /// <summary>
    /// ZoneDelete 的摘要说明
    /// </summary>
    public class ZoneDelete : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            int zoneid = Int32.Parse(context.Request["zoneId"]);

            ZoneService zoneService = new ZoneService();

            bool b = zoneService.MyDelete(zoneid);
            
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