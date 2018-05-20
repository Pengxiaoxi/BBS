<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TopiclistAdmin.aspx.cs" Inherits="WebApp.admin.TopiclistAdmin" %>

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

    function deleteTopic(topicId) {
        if (confirm("确定要删除这条数据吗?")) {
            $.post("Topic_delete.action", { topicId: topicId },
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
    function deleteTopics() {
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
            $.post("Topic_delete1.action", { ids: ids }, function (result) {
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


</script>
</head>
<%--<%
    if (Session["adminuser"] == null)
    {
        Response.Redirect("/admin/Login.aspx");
        return;
    }
%>--%>
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
			<h1>帖子管理</h1>
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
			<i class="icon-home"></i> 首页</a> <a href="" class="current">帖子</a>
		</div>

        
		<div id="tooBar" style="padding: 10px 0px 0px 10px;">
			<a href="#" role="button" class="btn btn-danger" onclick="javascrip:deleteTopics()">批量删除</a>
			<form action="Topic_listAdmin.action" method="post" class="form-search">
			<table cellpadding="5px;">
				<tr>
					<td>帖子标题:</td>
					<td><input name="s_topic.title" value="${s_topic.title }" type="text" class="input-medium search-query" placeholder="输入帖子标题..." style="width: 165px;"/></td>
					<td>发帖人:</td>
					<td><input name="s_topic.user.nickName" value="${s_topic.user.nickName }" type="text" class="input-medium search-query" placeholder="输入发帖人..." style="width: 165px;"/></td>
					<td>所属小板块:</td>
					<td>
						<select id="section" name="s_topic.section.id" style="width: 165px;"><option value="0">请选择板块...</option>
							<c:forEach var="section" items="${sectionList }">
								<option value="${section.id }" ${s_topic.section.id==section.id?'selected':'' }>${section.name }</option>
							</c:forEach>
						</select>
					</td>
				</tr>
				<tr>
					<%-- <td>发帖时间:</td>
					<td><input type="text" id="publishTime" class="input-medium search-query Wdate" onClick="WdatePicker()" name="s_topic.publishTime" value="<fmt:formatDate value="${s_topic.publishTime }" type="date" pattern="yyyy-MM-dd"/>" style="width: 165px;"/></td>
					<td>最后修改时间:</td>
					<td><input type="text" id="modifyTime" class="input-medium search-query Wdate" onClick="WdatePicker()" name="s_topic.modifyTime" value="<fmt:formatDate value="${s_topic.modifyTime }" type="date" pattern="yyyy-MM-dd"/>" style="width: 165px;"/></td> --%>
					<td>是否置顶:</td>
					<td>
						<select name="s_topic.top" style="width: 195px;"><option value="2">全部</option>
							<option value="1" ${s_topic.top==1?'selected':'' }>置顶</option>
							<option value="0" ${s_topic.top==0?'selected':'' }>非置顶</option>
						</select>
					</td>
					<td>是否精华:</td>
					<td>
						<select name="s_topic.good" style="width: 195px;"><option value="2">全部</option>
							<option value="1" ${s_topic.good==1?'selected':'' }>精华</option>
							<option value="0" ${s_topic.good==0?'selected':'' }>非精华</option>
						</select>
					</td>
					<td></td>
					<td>
						<button type="submit" class="btn btn-primary" title="Search">查询&nbsp;<i class="icon  icon-search"></i></button>
					</td>
				</tr>
			</table>
			</form>
		</div>
		<div class="row-fluid">
			<div class="span12">
				<div class="widget-box">
					<div class="widget-title">
						<!-- <span class="icon"> <input type="checkbox"
							id="title-checkbox" name="title-checkbox" />
						</span> -->
						<h5>主题列表</h5>
					</div>
					<div class="widget-content nopadding">
						<table class="table table-bordered table-striped with-check">
							<thead>
								<tr>
									<th><i class=""></i></th>
									<th>编号</th>
									<th>帖子标题</th>
									<th>发帖人</th>
									<th class="th">所属小板块</th>
									<th>发帖时间</th>
									<th>最后修改时间</th>
									<th>是否置顶</th>
									<th>是否精华</th>
									<th>操作</th>
								</tr>
							</thead>
							<tbody>
								<c:forEach items="${TopicList }" var="topic">
									<tr>
										<td><input type="checkbox" /></td>
										<td style="text-align: center;vertical-align: middle;">${topic.id }</td>
										<td style="text-align: center;vertical-align: middle;">${topic.title }</td>
										<td style="text-align: center;vertical-align: middle;width: 110px;vertical-align: middle;">
											${topic.user.nickName }
										</td>
										<td style="text-align: center;vertical-align: middle;">${topic.section.name }</td>
										<td style="text-align: center;vertical-align: middle;">${topic.publishTime }</td>
										<td style="text-align: center;vertical-align: middle;">${topic.modifyTime }</td>
										<td style="text-align: center;vertical-align: middle;">
											<c:choose>
												<c:when test="${topic.top==1 }"><font style="color: red;">置顶</font></c:when>
												<c:otherwise>非置顶</c:otherwise>
											</c:choose>
										</td>
										<td style="text-align: center;vertical-align: middle;">
											<c:choose>
												<c:when test="${topic.good==1 }"><font style="color: red;">精华</font></c:when>
												<c:otherwise>非精华</c:otherwise>
											</c:choose>
										</td>
										<td style="text-align: center;vertical-align: middle;">
											<button class="btn btn-info" type="button" data-backdrop="static" data-toggle="modal" data-target="#dlg" onclick="return modifyTopic()">修改
											</button>&nbsp;&nbsp;<button class="btn btn-danger" type="button" onclick="javascript:deleteTopic(${topic.id})">删除</button>
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
