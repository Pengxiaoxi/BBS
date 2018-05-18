using bbs.BLL;
using bbs.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Util;

namespace WebApp
{
    public partial class Register : System.Web.UI.Page
    {
        public string nickname { get; set; }

        public string truename { get; set; }
        public string sex { get; set; }
        public string face { get; set; }
        public DateTime regtime { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string type { get; set; }

        public string msg { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

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

                            face = "/face/" + newFileNames;

                            file.SaveAs(fileSavePath);   //保存图片到服务器指定的目录中去
                        }
                        else
                        {
                            face = "/images/timg.jpg";
                        }
                    }
                    else
                    {
                        face = "/images/timg.jpg";
                    }

                    nickname = Request.Form["user.nickName"];
                    truename = Request.Form["user.trueName"];
                    sex= Request.Form["user.sex"];
                    //face = "/face/" + newFileNames;
                    string password = MyMd5.GetMd5String(MyMd5.GetMd5String(Request.Form["user.password"]));
                    regtime = DateTime.Now;
                    mobile = Request.Form["user.mobile"];
                    email = Request.Form["user.email"];
                    type = Request.Form["type"];

                    //创建业务层，且调用业务层的方法进行用户信息添加
                    UserService userService = new UserService();
                    User userInfo = new User();

                    userInfo.nickname = nickname;
                    userInfo.truename = truename;
                    userInfo.sex = sex;
                    userInfo.face = face;
                    userInfo.password = password;
                    userInfo.regtime = regtime;
                    userInfo.mobile = mobile;
                    userInfo.email = email;
                    userInfo.type = type;

                    int i = userService.Add(userInfo);   //user表的id值

                    if (i > 0)
                    {
                        userInfo.id = i;   //用户主键ID
                        Session["userInfo"] = userInfo;
                        Response.Redirect("Index.aspx");
                    }
                    else
                    {
                        msg = "注册失败，请重新注册！"; 
                    }
                }
        }
    }
}