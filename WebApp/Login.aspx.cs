using bbs.BLL;
using bbs.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Util;

namespace WebApp
{
    public partial class Login : System.Web.UI.Page
    {
        public string ErrorMsg { get; set; }

        public string nickName { get; set; }

        public string passWord { get; set; }

        //public List<User> userInfo { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //服务端也要校验.(与客户端校验自己完成)
            //1.判断验证码是否正确
            if (IsPostBack)
            {
                String nickName = Request.Form["nickName"];
                String passWord = Request.Form["passWord"];

                if (CheckValidateCode())
                {
                    CheckUserLogin();  //登录操作
                }
                else
                {
                    ErrorMsg = "验证码错误！";
                }

            }
            else  //表示get请求
            {
                CheckCookieInfo(); //检查有没有cookie可用
            }
        }


        //对Cookie中存储的信息进行校验
        protected void CheckCookieInfo()
        {
            if (Request.Cookies["cp1"] != null && Request.Cookies["cp2"] != null)
            {
                string userName = Request.Cookies["cp1"].Value;
                string userPwd = Request.Cookies["cp2"].Value;

                //判断Cookie中存储的用户名是否正确
                //UserInfoDao dao = new UserInfoDao();
                //UserInfo userInfo = dao.findByName(userName);

                UserService userService = new UserService();

                //userPwd = MyMd5.GetMd5String(MyMd5.GetMd5String(userPwd));

                List<User> userInfo = userService.GetModelList("nickName='" + userName + "'" + "and passWord='" + userPwd + "'");

                int i = userInfo.Count();

                if (i != 0)
                {
                    User user = userInfo[0];
                    //给Session赋值
                    Session["userInfo"] = user;
                    ////直接跳转到目的地址
                    //if (Request["url"] != null)
                    //{
                    //    Response.Redirect(Request["url"].ToString());
                    //}
                    Response.Redirect("/Index.aspx");
                }
                else
                {
                    //用户名或密码错误删除Cookie
                    Response.Cookies["cp1"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["cp2"].Expires = DateTime.Now.AddDays(-1);
                }
            }
        }

        //判断用户名密码是否正确，用户登录
        protected void CheckUserLogin()
        {
            nickName = Request.Form["nickName"];
            passWord = Request.Form["passWord"];

            passWord = MyMd5.GetMd5String(MyMd5.GetMd5String(passWord));  //两次MD5加密

            //UserInfo userInfo = null;
            //UserInfoDao dao = new UserInfoDao();

            //List<User> userInfo = null;

            UserService userService = new UserService();

            List<User> userInfo = userService.GetModelList("nickName='" + nickName + "'" + "and passWord='" + passWord + "'");

            int i = userInfo.Count();

            if (i != 0)
            {
                //给session赋值
                User user = userInfo[0];     
                Session["userInfo"] = user;  //用户名密码判断登录成功后一定要将userInfo对象保存到session中

                //判断checkbox是否勾选
                if (!string.IsNullOrEmpty(Request.Form["CheckMe"]))  //表示用户选择了复选框，只会将选中的复选框的值提交到服务器端
                {
                    //Response.Cookies["cp1"].Value = userName
                    HttpCookie cookie1 = new HttpCookie("cp1", nickName);
                    HttpCookie cookie2 = new HttpCookie("cp2", passWord);

                    cookie1.Expires = DateTime.Now.AddDays(3);  //设置过期三天
                    cookie2.Expires = DateTime.Now.AddDays(3);

                    //将cookie数据写入电脑硬盘
                    Response.Cookies.Add(cookie1);
                    Response.Cookies.Add(cookie2);


                }

                //直接跳转到目的地址 url
                if (Request.QueryString["url"] != null)
                {
                    Response.Redirect(Request.QueryString["url"].ToString());
                }
                else
                {
                    Response.Redirect("/Index.aspx");
                }

                Response.Redirect("/Index.aspx");

            }
            else
            {
                ErrorMsg = "用户名或密码错误！";

            }
        }

        //对验证码进行校验
        protected bool CheckValidateCode()
        {
            bool isSuccess = false;
            if (Session["code"] != null)
            {
                string sysCode = Session["code"].ToString();
                string txtCode = Request.Form["imageCode"];

                if (sysCode.Equals(txtCode, StringComparison.InvariantCultureIgnoreCase))
                {
                    Session["code"] = null;

                    isSuccess = true;
                }
            }
            return isSuccess;
        }

    }
}