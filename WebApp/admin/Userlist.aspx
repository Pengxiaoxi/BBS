﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="WebApp.admin.UserList" %>

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

    function modifyUser(id, nickName, trueName, password, sex, face, email, mobile) {
        $("#myModalLabel").html("修改用户");
        $("#id").val(id);
        $("#nickName").val(nickName);
        $("#trueName").val(trueName);
        $("#password").val(password);
        $("#sex").val(sex);
        //$("#face").attr("src", "${pageContext.request.contextPath}/" + face);
        $("#face").attr("src", "/pageContext/" + face);
        $("#email").val(email);
        $("#mobile").val(mobile);
    }

    function saveUser() {
        $.post("/admin/UserSave.ashx", $("#fm").serialize(), function (data) {
            //alert($("#fm").serialize());
            //alert(data);
            if(data)
            {
                alert("用户信息修改成功！");
                resetValue();
                location.reload(true);
            }
            else
            {
                alert("用户信息修改失败！");
            }
        }, "text");
    }

    function userDelete(userId) {
        if (confirm("用户所发的帖子也将被删除，确定要删除这条数据吗?")) {
            $.post("/admin/UserDelete.ashx", { userId: userId },
                    function (result) {
                        //var result = eval('(' + result + ')');
                        //alert(result);
                        if (result) {
                            alert("删除成功！");
                            window.location.reload(true);
                        }
                        else
                        {
                            alert("删除失败");
                        }
                    },"text"
                );
        }
    }


    function deleteUsers() {
        var selectedSpan = $(".checked").parent().parent().next("td");
        if (selectedSpan.length == 0) {
            alert("请选择要删除的数据！");
            return;
        }
        var strIds = [];
        for (var i = 0; i < selectedSpan.length; i++) {
            strIds.push(selectedSpan[i].innerHTML);
        }
        var ids = strIds.join(",");
        //alert(ids);
        
        if (confirm("用户所发的帖子也将被删除，您确定要删除这" + selectedSpan.length + "条数据吗？")) {
            $.post("/admin/UserDelete.ashx  ", { ids: ids }, function (result) {
                
                //var result = eval('(' + result + ')');
                if (result) {
                    //alert(result);
                    alert("删除成功！");
                    location.reload(true);
                }
                else
                {
                    alert("删除失败");
                }
            },"text");
        } else {
            return;
        }
    }
    function resetValue() {
        $("#id").val("");
        $("#userName").val("");
    }


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
			<li id="userLi"><a href="/admin/Userlist.aspx"><i class="icon icon-home"></i> <span>用户管理</span></a></li>
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
			<h1>用户管理</h1>
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
			<i class="icon-home"></i> 首页</a> <a href="" class="current">用户</a>
		</div>

		<div class="container-fluid">
		<div id="tooBar" style="padding: 10px 0px 0px 10px;">
			<!-- <button class="btn btn-primary" type="button" data-backdrop="static" data-toggle="modal" data-target="#dlg" onclick="return openAddDlg()">添加小板块</button>&nbsp;&nbsp;&nbsp;&nbsp; -->

			<a href="#" role="button" class="btn btn-danger" onclick="javascrip:deleteUsers()">批量删除</a>

			<%-- <form action="User_list.action" method="post" class="form-search" style="display: inline;">
	          &nbsp;小板块名称：
			  <input name="s_user.name" value="${s_user.name }" type="text" class="input-medium search-query" placeholder="输入小板块名称..."/>
			  &nbsp;所属大板块：
			  <select name="s_user.zone.id"><option value="">请选择...</option>
				<c:forEach var="zone" items="${zoneList }">
					<option value="${zone.id }" ${s_user.zone.id==zone.id?'selected':'' }>${zone.name }</option>
				</c:forEach>
			  </select>
			  &nbsp;版主：
			  <select name="s_user.master.id"><option value="">请选择...</option>
				<c:forEach var="master" items="${masterList }">
					<option value="${master.id }" ${s_user.master.id==master.id?'selected':'' }>${master.nickName }</option>
				</c:forEach>
			  </select>
			  &nbsp;
			  <button type="submit" class="btn btn-primary" title="Search">查询&nbsp;<i class="icon  icon-search"></i></button>
			</form> --%>
		</div>
		<div class="row-fluid">
			<div class="span12">
				<div class="widget-box">
					<div class="widget-title">
						<!-- <span class="icon"> <input type="checkbox"
							id="title-checkbox" name="title-checkbox" />
						</span> -->
						<h5>用户列表</h5>
					</div>
					<div class="widget-content nopadding">
						<table class="table table-bordered table-striped with-check">
							<thead>
								<tr>
									<th><i class=""></i></th>
									<th>编号</th>
									<th>昵称</th>
									<th>真实姓名</th>
									<%--<th>登录密码</th>--%>
									<th>性别</th>
									<th>头像</th>
									<th>注册时间</th>
									<th>邮箱</th>
									<th>联系电话</th>
									<th>用户类型</th>
									<th>操作</th>
								</tr>
							</thead>
							<tbody>
                                <% 
                                    foreach (bbs.Model.User user in userList) {%>

									<tr>
										<td><input type="checkbox" /></td>
										<td style="text-align: center;vertical-align: middle;"><%=user.id%></td>
										<td style="text-align: center;vertical-align: middle;"><%=user.nickname %></td>
										<td style="text-align: center;vertical-align: middle;"><%=user.truename %></td>
										<%--<td style="text-align: center;vertical-align: middle;"><%=user.password %></td>--%>
										<td style="text-align: center;vertical-align: middle;"><%=user.sex %></td>
										<td style="text-align: center;vertical-align: middle;">
											
                                            <img alt="" src="<%=user.face %>" style="width: 80px; height: 80px;">

										</td>
										<td style="text-align: center;vertical-align: middle;"><%=user.regtime %></td>
										<td style="text-align: center;vertical-align: middle;"><%=user.email %></td>
										<td style="text-align: center;vertical-align: middle;"><%=user.mobile %></td>
										<td style="text-align: center;vertical-align: middle;width: 150px;">
											
                                            <%
                                                if (user.type == "1")
                                                {%>
                                                    <font style = "color: black;" >普通用户</ font >
                                                <%}
                                                else if (user.type == "2")
                                                {%>
                                                    <font style="color: blue;">版主</font>
                                                <%}
                                                else
                                                {%>
                                                    <font style="color: red;">管理员</font>
                                                <%}
                                            %>

										</td>
										<td style="text-align: center;vertical-align: middle;">
											<button class="btn btn-info" type="button" data-backdrop="static" data-toggle="modal" data-target="#dlg" 

											onclick="return modifyUser(<%=user.id %>,'<%=user.nickname %>','<%=user.truename %>','','<%=user.sex %>','<%=user.face %>','<%=user.email %>','<%=user.mobile %>')">修改</button>&nbsp;&nbsp;
											<button class="btn btn-danger" type="button" onclick="javascript:userDelete(<%=user.id %>)">删除</button>
										</td>
									</tr>
								<%}
                                %>
							</tbody>
						</table>
					</div>
				</div>
				<div class="pagination alternate">
					<ul class="clearfix">
                        <%=pageCode %>
					</ul>
				</div>


			</div>
		</div>
		<div id="dlg" class="modal hide fade"  tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
			<div class="modal-header">
				<button type="button" class="close" data-dismiss="modal"
					aria-hidden="true" onclick="return resetValue()">×</button>
				<h3 id="myModalLabel">增加小板块</h3>
			</div>
			<div class="modal-body">
				<form id="fm" action="User_save.action">
					<table>
						<tr>
							<td>
								<label class="control-label" for="userName">用户昵称：</label>
							</td>
							<td>
								<input id="nickName" type="text" name="user.nickName" placeholder="导入数据失败！">
							</td>
						</tr>
						<tr>
							<td>
								<label class="control-label" for="trueName">真实姓名：</label>
							</td>
							<td>
								<input id="trueName" type="text" name="user.trueName" placeholder="导入数据失败！">
							</td>
						</tr>
						<tr>
							<td>
								<label class="control-label" for="password">登录密码：</label>
							</td>
							<td>
								<input id="password" type="password" name="user.password">
							</td>
						</tr>
						<tr>
							<td>
								<label class="control-label" for="sex">性别：</label>
							</td>
							<td>
								<input id="sex" type="text" name="user.sex" placeholder="导入数据失败！">
							</td>
						</tr>
						<tr>
							<td>
								<label class="control-label" for="face">头像：</label>
							</td>
							<td>
								<img id="face" style="width: 100px;"></img>
							</td>
						</tr>
						<tr>
							<td>
								<label class="control-label" for="email">邮箱：</label>
							</td>
							<td>
								<input id="email" type="text" name="user.email" placeholder="导入数据失败！">
							</td>
						</tr>
						<tr>
							<td>
								<label class="control-label" for="mobile">联系电话：</label>
							</td>
							<td>
								<input id="mobile" type="text" name="user.mobile" placeholder="导入数据失败！">
							</td>
						</tr>
						<%-- <tr>
							<td>
								<label class="control-label" for="section">设置版主：</label>
							</td>
							<td>
								<select multiple="multiple">
									<c:forEach var="section" items="${sectionList }">
										<option id="section${section.id }" value="section.id">${section.name }</option>
									</c:forEach>
								</select> 
							</td>
						</tr> --%>
					</table>
					<input id="id" type="hidden" name="user.id">
				</form>
			</div>
			<div class="modal-footer">
				<font id="error" style="color: red;"></font>
				<button class="btn" data-dismiss="modal" aria-hidden="true"
					onclick="return resetValue()">关闭</button>
				<button class="btn btn-primary" onclick="javascript:saveUser()">保存</button>
			</div>
		</div>
	</div>


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
