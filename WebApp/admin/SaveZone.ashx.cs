using bbs.BLL;
using bbs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.admin
{
    /// <summary>
    /// SaveZone 的摘要说明
    /// </summary>
    public class SaveZone : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            int id = 0;
            string zoneName = context.Request["zone.name"];
            string description = context.Request["zone.description"];
            
            //int id = Int32.Parse(context.Request["zone.id"]);

            ZoneService zoneService = new ZoneService();
            Zone zone = new Zone();
            zone.name = zoneName;
            zone.description = description;

            bool b;

            if (Int32.TryParse(context.Request["zone.id"], out id))
            {
                zone.id = id;
                b = zoneService.Update(zone);

            }
            else
            {
                if (zoneService.Add(zone) > 0)
                {
                    b = true;
                }
                else
                {
                    b = false;
                }
            }

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