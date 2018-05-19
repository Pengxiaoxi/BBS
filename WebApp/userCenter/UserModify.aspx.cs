using bbs.BLL;
using bbs.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Util;

namespace WebApp.userCenter
{
    public partial class UserModify : System.Web.UI.Page
    {
        public string sex { get; set; }
        public string msg { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //调用业务层的方法进行用户信息添加
            UserService userService = new UserService();
            User userInfo = new User();

            sex = ((User)Session["userInfo"]).sex;  //下拉菜单默认显示

            if (IsPostBack)
            {
                HttpPostedFile file = Request.Files["face"];  //获取上传的图片

                if (file != null && !file.FileName.Equals(""))
                {    //判断文件是否为空

                    string fileName = file.FileName;   //得到上传图片的文件名字

                    string ext = Path.GetExtension(fileName);   //得到上传图片的文件扩展名

                    if (ext == ".jpg" || ext == ".gif" || ext == ".png" || ext == ".jpeg" || ext == ".JPG") //设定文件的类型
                    {

                        string newFileNames = Guid.NewGuid().ToString() + ext;

                        //Directory.CreateDirectory(Path.GetDirectoryName(Request.MapPath("/face/" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "/")));

                        string fileSavePath = Request.MapPath("/face/" + newFileNames);

                        userInfo.face = "/face/" + newFileNames;

                        file.SaveAs(fileSavePath);   //保存图片到服务器指定的目录中去
                    }
                    else
                    {
                        userInfo.face = ((bbs.Model.User)Session["userInfo"]).face;
                    }
                }
                else
                {
                    userInfo.face = ((bbs.Model.User)Session["userInfo"]).face;
                }

                userInfo.nickname = Request.Form["user.nickName"];
                userInfo.truename = Request.Form["user.trueName"];
                userInfo.sex = Request.Form["user.sex"];
                //face = "/face/" + newFileNames;
                userInfo.password = MyMd5.GetMd5String(MyMd5.GetMd5String(Request.Form["user.password"]));
                userInfo.regtime = DateTime.Parse(Request.Form["user.regTime"]);
                userInfo.mobile = Request.Form["user.mobile"];
                userInfo.email = Request.Form["user.email"];
                userInfo.type = Request.Form["user.type"];
                userInfo.id = Int32.Parse(Request.Form["user.id"]);

                var i = userService.Update(userInfo);   //true or false

                if (i == true)
                {
                    Session["userInfo"] = userInfo;
                    Response.Redirect("/userCenter/UserInfo.aspx");
                }
                else
                {
                    msg = "个人信息修改失败，请重新修改！";
                }
            }
        }
    }
}