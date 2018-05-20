<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SectionList.aspx.cs" Inherits="WebApp.admin.SectionList" %>

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
//$(function(){
//	var sectionPage="section.jsp";var topicPage="topic.jsp";var userPage="user.jsp";var zonePage="zone.jsp";
//	var curPage='${mainPage}';
//	if(sectionPage.indexOf(curPage)>=0&&curPage!=""){
//		$("#sectionLi").addClass("active");
//	} else if(topicPage.indexOf(curPage)>=0&&curPage!=""){
//		$("#topicLi").addClass("active");
//	} else if(userPage.indexOf(curPage)>=0&&curPage!=""){
//		$("#userLi").addClass("active");
//	} else if(zonePage.indexOf(curPage)>=0&&curPage!=""){
//		$("#zoneLi").addClass("active");
//	}
    //})

 $(function () {
        $("#logo").uploadPreview({ Img: "ImgPr", Width: 220, Height: 220 });
    });
    function openAddDlg() {
        $("#myModalLabel").html("增加小板块");
    }
    function saveSection() {
        if ($("#sectionName").val() == null || $("#sectionName").val() == '') {
            $("#error").html("请输入小板块名称！");
            return false;
        }
        if ($("#zone").val() == null || $("#zone").val() == '') {
            $("#error").html("请选择所属大板块！");
            return false;
        }
        if ($("#masterId").val() == null || $("#masterId").val() == '') {
            $("#error").html("请输入版主昵称！");
            return false;
        }
        /* $.post("Section_save.action", $("#fm").serialize()); */
        $("#fm").submit();
        alert("保存成功！");
        resetValue();
        location.reload(true);
    }
    function modifySection(id, name, zone, masterNickName, logo) {
        $("#myModalLabel").html("修改小板块");
        $("#id").val(id);
        $("#sectionName").val(name);
        $("#ImgPr").attr("src", "${pageContext.request.contextPath}/" + logo);
        $("#zone").val(zone);
        $("#masterNickName").val(masterNickName);
    }
    function sectionDelete(sectionId) {
        if (confirm("确定要删除这条数据吗?")) {
            $.post("Section_delete.action", { sectionId: sectionId },
                    function (result) {
                        var result = eval('(' + result + ')');
                        if (result.error) {
                            alert(result.error);
                        } else {
                            alert("删除成功！");
                            window.location.reload(true);
                        }
                    }
                );
        }
    }
    function deleteSections() {
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
        if (confirm("您确定要删除这" + selectedSpan.length + "条数据吗？")) {
            $.post("Section_delete1.action", { ids: ids }, function (result) {
                if (result.success) {
                    alert("数据已成功删除！");
                    location.reload(true);
                } else {
                    alert("数据删除失败！");
                }
            }, "json");
        } else {
            return;
        }
    }
    function resetValue() {
        $("#id").val("");
        $("#sectionName").val("");
    }
    function searchUserByNickName(userNickName) {
        $.post("Section_getUserByNickName.action", { nickName: userNickName }, function (result) {
            var result = eval('(' + result + ')');
            $("#info").html(result.info);
            $("#masterId").val(result.masterId);
        });
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
			<h1>小版块管理</h1>
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
			<i class="icon-home"></i> 首页</a><a href="" class="current">小版块</a>
		</div>

		<div class="container-fluid">
		<div id="tooBar" style="padding: 10px 0px 0px 10px;">
			<button class="btn btn-primary" type="button" data-backdrop="static" data-toggle="modal" data-target="#dlg" onclick="return openAddDlg()">添加小板块</button>&nbsp;&nbsp;&nbsp;&nbsp;
			<a href="#" role="button" class="btn btn-danger" onclick="javascrip:deleteSections()">批量删除</a>
			<form action="Section_list.action" method="post" class="form-search" style="display: inline;">
	          &nbsp;小板块名称：
			  <input name="s_section.name" value="${s_section.name }" type="text" class="input-medium search-query" placeholder="输入小板块名称..."/>
			  &nbsp;所属大板块：
			  <select name="s_section.zone.id"><option value="">请选择...</option>
				<c:forEach var="zone" items="${zoneList }">
					<option value="${zone.id }" ${s_section.zone.id==zone.id?'selected':'' }>${zone.name }</option>
				</c:forEach>
			  </select>
			  &nbsp;版主：
			  <select name="s_section.master.id"><option value="">请选择...</option>
				<c:forEach var="master" items="${masterList }">
					<option value="${master.id }" ${s_section.master.id==master.id?'selected':'' }>${master.nickName }</option>
				</c:forEach>
			  </select>
			  &nbsp;
			  <button type="submit" class="btn btn-primary" title="Search">查询&nbsp;<i class="icon  icon-search"></i></button>
			</form>
		</div>
		<div class="row-fluid">
			<div class="span12">
				<div class="widget-box">
					<div class="widget-title">
						<!-- <span class="icon"> <input type="checkbox"
							id="title-checkbox" name="title-checkbox" />
						</span> -->
						<h5>小板块列表</h5>
					</div>
					<div class="widget-content nopadding">
						<table class="table table-bordered table-striped with-check">
							<thead>
								<tr>
									<th><i class=""></i></th>
									<th>编号</th>
									<th>小板块名称</th>
									<th>小板块logo</th>
									<th class="th">所属大板块</th>
									<th>版主</th>
									<th>操作</th>
								</tr>
							</thead>
							<tbody>
								<c:forEach items="${sectionList }" var="section">
									<tr>
										<td><input type="checkbox" /></td>
										<td style="text-align: center;vertical-align: middle;">${section.id }</td>
										<td style="text-align: center;vertical-align: middle;">${section.name }</td>
										<td style="text-align: center;vertical-align: middle;width: 110px;vertical-align: middle;">
											<img style="width: 100px;" src='${pageContext.request.contextPath}/${section.logo }'></img>
										</td>
										<td style="text-align: center;vertical-align: middle;">${section.zone.name }</td>
										<td style="text-align: center;vertical-align: middle;">${section.master.nickName }</td>
										<td style="text-align: center;vertical-align: middle;">
											<button class="btn btn-info" type="button" data-backdrop="static" data-toggle="modal" data-target="#dlg" onclick="return modifySection(${section.id},'${section.name }',${section.zone.id },'${section.master.nickName }','${section.logo }')">修改
											</button>&nbsp;&nbsp;<button class="btn btn-danger" type="button" onclick="javascript:sectionDelete(${section.id})">删除</button>
										</td>
									</tr>
								</c:forEach>
							</tbody>
						</table>
					</div>
				</div>
				<div class="pagination alternate">
					<ul class="clearfix">${pageCode }
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
				<form id="fm" action="Section_save.action" method="post" enctype="multipart/form-data">
					<table>
						<tr>
							<td>
								<label class="control-label" for="sectionName">请输入小板块名称：</label>
							</td>
							<td>
								<input id="sectionName" type="text" name="section.name" placeholder="请输入…">
							</td>
						</tr>
						<tr>
							<td>
								<img id="ImgPr" class="pull-left" style="width: 120px; height: 120px;" src="${pageContext.request.contextPath}/${section.logo }" />
							</td>
							<td>
							</td>
						</tr>
						<tr>
							<td>
								<label class="control-label" for="logo">上传logo：</label>
							</td>
							<td>
								<input type="file" id="logo" name="logo">
							</td>
						</tr>
						<tr>
							<td>
								<label class="control-label" for="zone">请选择所属大板块：</label>
							</td>
							<td>
								<select id="zone" name="section.zone.id"><option value="">请选择...</option>
									<c:forEach var="zone" items="${zoneList }">
										<option value="${zone.id }">${zone.name }</option>
									</c:forEach>
								</select>
							</td>
						</tr>
						<tr>
							<td>
								<label class="control-label" for="masterNickName">版主：</label>
							</td>
							<td>
								<input id="masterNickName" type="text" name="section.master.nickName" onkeydown="javascript:searchUserByNickName(this.value)" placeholder="请输入昵称回车">
								<font id="info" style="color: red;"></font>
							</td>
						</tr>
					</table>
					<input id="id" type="hidden" name="section.id">
					<input id="masterId" type="hidden" name="section.master.id">
				</form>
			</div>
			<div class="modal-footer">
				<font id="error" style="color: red;"></font>
				<button class="btn" data-dismiss="modal" aria-hidden="true"
					onclick="return resetValue()">关闭</button>
				<button class="btn btn-primary" onclick="javascript:saveSection()">保存</button>
				<!-- <button class="btn btn-primary" type="submit">保存</button> -->
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