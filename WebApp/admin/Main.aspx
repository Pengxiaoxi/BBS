﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="WebApp.admin.Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>后台管理</title>
<link rel="stylesheet" href="/admin/css/bootstrap.min.css" />
<link rel="stylesheet" href="/admin/css/bootstrap-responsive.min.css" />
<link rel="stylesheet" href="/admin/css/uniform.css" />
<link rel="stylesheet" href="/admin/css/unicorn.main.css" />
<link rel="stylesheet" href="/admin/css/unicorn.grey.css" class="skin-color" />
<script type="text/javascript" src="/admin/js/jquery-1.11.1.js"></script>
<script src="/admin/js/My97DatePicker/WdatePicker.js"></script>
<script type="text/javascript">
$(function(){
	var sectionPage="section.jsp";var topicPage="topic.jsp";var userPage="user.jsp";var zonePage="zone.jsp";
	var curPage='${mainPage}';
	if(sectionPage.indexOf(curPage)>=0&&curPage!=""){
		$("#sectionLi").addClass("active");
	} else if(topicPage.indexOf(curPage)>=0&&curPage!=""){
		$("#topicLi").addClass("active");
	} else if(userPage.indexOf(curPage)>=0&&curPage!=""){
		$("#userLi").addClass("active");
	} else if(zonePage.indexOf(curPage)>=0&&curPage!=""){
		$("#zoneLi").addClass("active");
	}
})
</script>
</head>
<%
    if (Session["adminuser"] == null)
    {
        Response.Redirect("/admin/Login.aspx");
        return;
    }
%>
<body>
	<div id="header">
		<h1 style="margin-left: 0px;padding-left: 0px;"><a href="www.hbpu.edu.cn">湖北理工学院</a></h1>	
		<!-- <h2 style="padding: 0px; margin-top: 10px; margin-bottom: 0px;">
			<a href="#"><font color="#cccccc">Java1234论坛</font></a>
		</h2>
		<h3 style="margin: 0px 0px 0px 40px;">
			<a href="#"><font color="#cccccc">后台管理</font></a>
		</h3> -->
	</div>

	<div id="sidebar">
		<ul>
			<li id="zoneLi"><a href="/admin/ZoneList.aspx"><i class="icon icon-home"></i> <span>大板块管理</span></a></li>
			<li id="sectionLi"><a href="/admin/SectionList.aspx"><i class="icon icon-home"></i> <span>小板块管理</span></a></li>
			<li id="topicLi"><a href="/admin/TopiclistAdmin.aspx"><i class="icon icon-home"></i> <span>帖子管理</span></a></li>
			<!-- <li><a href="#"><i class="icon icon-home"></i> <span>回复管理</span></a></li> -->
			<li id="userLi"><a href="/admin/UserList.aspx"><i class="icon icon-home"></i> <span>用户管理</span></a></li>
			<li class="submenu"><a href="#"><i class="icon icon-th-list"></i>
					<span>系统管理</span> <span class="label">3</span></a>
				<ul>
					<li><a href="#">修改密码</a></li>
					<li><a href="/admin/AdminLoginOut.ashx">安全退出</a></li>
					<li><a href="#">刷新系统缓存</a></li>
				</ul></li>
		</ul>

	</div>

	<div id="style-switcher">
		<i class="icon-arrow-left icon-white"></i> <span>颜色:</span> 
		<a href="#grey" style="background-color: #555555; border-color: #aaaaaa;"></a> 
		<a href="#blue" style="background-color: #2D2F57;"></a> 
		<a href="#red" style="background-color: #673232;"></a>
	</div>

	<div id="content">
		<div id="content-header">
			<h1>后台管理</h1>
			<!-- <div class="btn-group">
				<a class="btn btn-large tip-bottom" title="Manage Files"><i
					class="icon-file"></i></a> <a class="btn btn-large tip-bottom"
					title="Manage Users"><i class="icon-user"></i></a> <a
					class="btn btn-large tip-bottom" title="Manage Comments"><i
					class="icon-comment"></i><span class="label label-important">5</span></a>
				<a class="btn btn-large tip-bottom" title="Manage Orders"><i
					class="icon-shopping-cart"></i></a>
			</div> -->
		</div>
		<div id="breadcrumb">
			<a href="#" title="首页" class="tip-bottom">
			<i class="icon-home"></i> 首页</a> <a href="" class="current"></a>
		</div>

		<%--<jsp:include page="${mainPage }"></jsp:include>--%>


		<div class="row-fluid">
			<div id="footer" class="span12">
				2015 &copy; 湖理 作者：计算机学院&nbsp;&nbsp;&nbsp;&nbsp; <a href="http://www.hbpu.edu.cn/">湖北理工学院</a>
			</div>
		</div>
	</div>

<script src="/admin/js/jquery.min.js"></script>
<script src="/admin/js/jquery.ui.custom.js"></script>
<script src="/admin/js/bootstrap.min.js"></script>
<script src="/admin/js/jquery.uniform.js"></script>
<!-- <script src="js/select2.min.js"></script> -->
<script src="/admin/js/jquery.dataTables.min.js"></script>
<script src="/admin/js/unicorn.js"></script>
<script src="/admin/js/unicorn.tables.js"></script>
<script type="text/javascript" src="/admin/js/uploadPreview.min.js"></script>
</body>
</html>
