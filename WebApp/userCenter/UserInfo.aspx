<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="WebApp.userCenter.UserInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>个人信息管理</title>
<link rel="stylesheet" href="/admin/css/bootstrap.min.css" />
<link rel="stylesheet" href="/admin/css/bootstrap-responsive.min.css" />
<script type="text/javascript" src="/js/jquery-1.11.1.js"></script>
<style type="text/css">
</style>
<script type="text/javascript">
</script>
</head>
<body>
<div id="header" class="wrap" style="width: 1200px; margin: 0 auto;">
	<% Server.Execute("/common/Top.aspx"); %>  <%--Execute是从当前页面转移到指定页面，并将执行返回到当前页面--%>
</div>
<div style="width: 1200px; margin: 0 auto;">
	<h2>个人信息管理</h2>
	<table class="table table-bordered table-striped with-check">
		<tr>
			<th style="text-align: center;vertical-align: middle;">ID</th> 
			<th style="text-align: center;vertical-align: middle;">昵称</th>
			<th style="text-align: center;vertical-align: middle;">真实姓名</th>
			<th style="text-align: center;vertical-align: middle;">密码</th>
			<th style="text-align: center;vertical-align: middle;">性别</th>
			<th style="text-align: center;vertical-align: middle;">头像</th>
			<th style="text-align: center;vertical-align: middle;">邮箱</th>
			<th style="text-align: center;vertical-align: middle;">联系电话</th>
			<th style="text-align: center;vertical-align: middle;">注册时间</th>
			<th style="text-align: center;vertical-align: middle;">用户类型</th>
			<th style="text-align: center;vertical-align: middle;">操作</th>
		</tr>
		<tr>
			<td style="text-align: center;vertical-align: middle;"><%=((bbs.Model.User)Session["userInfo"]).id %></td>
			<td style="text-align: center;vertical-align: middle;"><%=((bbs.Model.User)Session["userInfo"]).nickname %></td>
			<td style="text-align: center;vertical-align: middle;"><%=((bbs.Model.User)Session["userInfo"]).truename %></td>
			<td style="text-align: center;vertical-align: middle;"><%=((bbs.Model.User)Session["userInfo"]).password %></td>
			<td style="text-align: center;vertical-align: middle;"><%=((bbs.Model.User)Session["userInfo"]).sex %></td>
			<td style="text-align: center;vertical-align: middle;">
				<%--<c:choose>
						<c:when test="${(currentUser.face==null||currentUser.face=='')&&currentUser.sex=='男'}">
							<img alt=""
								src="${pageContext.request.contextPath}/images/user/user0.gif"
								style="width: 100px; height: 100px;">
						</c:when>
						<c:when test="${(currentUser.face==null||currentUser.face=='')&&currentUser.sex=='女'}">
							<img alt=""
								src="${pageContext.request.contextPath}/images/user/female.gif"
								style="width: 100px; height: 100px;">
						</c:when>
						<c:otherwise>
							<img alt="" src="${pageContext.request.contextPath}/${currentUser.face}"
								style="width: 100px; height: 100px;">
						</c:otherwise>
					</c:choose>--%>

                <%
                    if (((bbs.Model.User)Session["userInfo"]).face == null || ((bbs.Model.User)Session["userInfo"]).face == "" && ((bbs.Model.User)Session["userInfo"]).sex == "男")
                    {%>
                         <img alt="" src="/images/user/user4.gif" style="width: 100px; height: 100px;">

                    <%}
                    else if (((bbs.Model.User)Session["userInfo"]).face == null || ((bbs.Model.User)Session["userInfo"]).face == "" && ((bbs.Model.User)Session["userInfo"]).sex == "女")
                    {%>
                         <img alt="" src="/images/user/female.gif" style="width: 100px; height: 100px;">  
                    <%}
                    else
                    {%>
                         <img alt="" src="<%=((bbs.Model.User)Session["userInfo"]).face %>" style="width: 100px; height: 100px;">
                     <%}
                %>

				</td>
			<td style="text-align: center;vertical-align: middle;"><%=((bbs.Model.User)Session["userInfo"]).email %></td>
			<td style="text-align: center;vertical-align: middle;"><%=((bbs.Model.User)Session["userInfo"]).mobile %></td>
			<td style="text-align: center;vertical-align: middle;"><%=((bbs.Model.User)Session["userInfo"]).regtime.ToString() %></td>
			<td style="text-align: center;vertical-align: middle;">

                <%
                    if (((bbs.Model.User)Session["userInfo"]).type == "1")
                 {
                     Response.Write("<font style='color: black;'>普通用户</font>");
                 }
                 else if(((bbs.Model.User)Session["userInfo"]).type == "2")
                 {
                     Response.Write("<font style='color: blue;'>版主</font>");
                 }
                 else
                 {
                     Response.Write("<font style='color: red;'>管理员</font>");
                 }
                %>


				</td>
				<td style="text-align:center; vertical-align: middle;">
					<a class="btn btn-info" type="button" href="/userCenter/UserModify.aspx">修改</a>&nbsp;&nbsp;
				</td>
		</tr>
	</table>
</div>
<div id="footer" style="width: 1200px; margin: 0 auto;">
	<% Server.Execute("/common/Footer.aspx"); %>  <%--Execute是从当前页面转移到指定页面，并将执行返回到当前页面--%>
</div>
</body>
</html>
