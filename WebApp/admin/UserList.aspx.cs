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
    public partial class UserList : System.Web.UI.Page
    {
        public List<User> userList { get; set; }
        public string pageCode { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            int pageNumber = 1;

            if (!Int32.TryParse(Request["page"], out pageNumber))
            {
                pageNumber = 1;
            }

            UserService userService = new UserService();

            userList = userService.FindAllUser(pageNumber);

            pageCode = PageUtil.genPagination("/admin/UserList.aspx", userService.GetRecordCount(""), pageNumber, userService.pageCount, "");
        }
    }
}