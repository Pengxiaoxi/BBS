<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TopicList.aspx.cs" Inherits="WebApp.topic.TopicList"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>帖子列表</title>
<%-- <link href="bootstrap/css/bootstrap.css" rel="stylesheet" />
<link href="bootstrap/css/bootstrap-responsive.css" rel="stylesheet" />
<link href="css/style.css" rel="stylesheet" />

<script type="text/javascript" src="/js/jquery-1.11.1.js"></script>
<script src="bootstrap/js/jquery.js"></script>
<script src="bootstrap/js/bootstrap.min.js"></script>
<script src="bootstrap/js/bootstrap.js"></script> --%>
<link rel="stylesheet" href="/admin/css/bootstrap.css" />
<link rel="stylesheet" href="/admin/css/bootstrap-responsive.min.css" />
<link rel="stylesheet" href="/admin/css/uniform.css" />
<%-- <link rel="stylesheet" href="/admin/css/unicorn.main.css" /> --%>
<%-- <link rel="stylesheet" href="/admin/css/unicorn.grey.css" class="skin-color" /> --%>

<script type="text/javascript" src="/js/jquery-1.11.1.js"></script>
<script src="/admin/js/jquery.min.js"></script>
<script src="/admin/js/jquery.ui.custom.js"></script>
<script src="/admin/js/bootstrap.min.js"></script>
<%-- <script src="/admin/js/jquery.uniform.js"></script> --%>
<script src="/admin/js/jquery.dataTables.min.js"></script>
<%-- <script src="/admin/js/unicorn.js"></script> --%>
<script src="/admin/js/unicorn.tables.js"></script>

<script src="/js/ckeditor/ckeditor.js"></script>
<script type="text/javascript">

function deleteTopic(topicId){
	if(confirm("您确定要删除这条数据吗？")){
		$.post("/topic/TopicDelete.ashx",{topicId:topicId},function(result){
			if(result){
				/* var result=eval('('+result+')'); */
				alert("数据已成功删除！");
				location.reload(true);
			}else{
				alert("数据删除失败！");
			}
		},"text");
	}else{
		return;
	}
}

function modifyTopic(topicId,topicTop,topicGood){
	$("#topicId").val(topicId);
	$("#topicTop").val(topicTop);
	$("#topicGood").val(topicGood);
}

function saveTopic(){
	var topicId=$("#topicId").val();
	var topicTop=$("#topicTop").val();
	var topicGood = $("#topicGood").val();
	//alert(topicId);
	//alert(topicTop);
	//alert(topicGood);

	$.post("/topic/TopicModify.ashx",{topicId:topicId,topicTop:topicTop,topicGood:topicGood},function(result){
		if (result) {
			alert("数据已成功修改！");
			location.reload(true); //网页重新加载
		} else {
			alert("数据修改失败！");
		}
	},"text");
}


function updateTopic(topicId, title, content) {
    //alert(topicId);
    //alert(title);
    //alert(content);
    $("#topicIds").val(topicId);
    $("#title").val(title);
    //$("#content").val(content);
    CKEDITOR.instances.content.setData(content);  //给conteng赋值
}

function updateTopicInfo() {
    var topicId = $("#topicIds").val();
    var title = $("#title").val();
    //var content = $("#content").val();
    var content = CKEDITOR.instances.content.getData();  //conteng取值

    $.post("/topic/TopicModifyInfo.ashx", { topicId: topicId, title: title, content: content }, function (result) {
        if (result) {
            alert("帖子修改成功！");
            location.reload(true); //网页重新加载
        } else {
            alert("帖子修改失败！");
        }
    }, "text");
}

</script>
</head>
<body>
<div id="header" class="wrap" style="width: 1200px; margin: 0 auto;">
	<%--<jsp:include page="../common/top.jsp"/>--%>
    <% Server.Execute("/common/Top.aspx"); %>  <%--Execute是从当前页面转移到指定页面，并将执行返回到当前页面--%>
</div>
<div style="width: 1200px; margin: 0 auto;">
	<h1 align="center">欢迎进入<%=section.name %>版面！</h1>
	<h4>版主：<%=section.user.nickname %></h4>
	<h4><%=section.zone.description %></h4>
</div>
<div style="width: 1200px; margin: 0 auto;">
	<div style="margin-bottom: 10px;">
		<a class="" href="/topic/TopicAdd.aspx?sectionId=<%=section.id %>" ><img alt="发帖" src="/images/post.jpg"></a>
		<div class="pagination alternate pull-right" align="center" style="margin: 0px;">
			<ul class="clearfix"><%=pageCode %>
			</ul>
		</div>
	</div>
	<table border="0" width="100%" cellspacing="0" cellpadding="0" style="margin-top: 8;">
		<!-- 置顶帖子 -->
		<!-- <tr height="30">
			<td style="text-indent:5;" background="images/index/classT.jpg"><b><font color="white">■ 置顶帖子</font></b></td>
		</tr> -->
		<tr>
			<td>
				<table class="table table-bordered" width="100%" cellspacing="0" cellpadding="0" style="margin-top: 8;">
					<tr>
						<th style="text-align: center;vertical-align: middle; width: 150px;">
							状态
						</th>
						<th style="text-align: center;vertical-align: middle;">
							帖子标题
						</th>
						<th style="text-align: center;vertical-align: middle; width: 100px;">
							回复数 	
						</th>
						<th style="text-align: center;vertical-align: middle; width: 100px;">
							发表者
						</th>
						<th style="text-align: center;vertical-align: middle; width: 200px;">
							最后回复
						</th>
						<th style="text-align: center;vertical-align: middle; width: 150px;">
							操作
						</th>
					</tr>
					<%
                        foreach (bbs.Model.Topic topic in zdptTopicList){
                    %>
						<tr>
							<td style="text-align: center;vertical-align:middle;">

                                <span style="color:darkblue ">【置顶】</span>
                                <%
                                    if (topic.good == "0")
                                    {
                                %>
                                        <span style = 'color: blue; ' >【普通】</ span >
                                    <%}
                                    else{
                                    %>
                                        <span style = 'color: red; ' >【精华】</ span >
                                    <%
                                        }
                                %>
                                
							</td>
							<td style="text-align: center;vertical-align:middle;">
								<a href="/topic/TopicDetails.aspx?topicId=<%=topic.id %>" ><%=topic.title %></a>

							</td>
							<td style="text-align: center;vertical-align:middle;">
                                <%= zdtopicReplyCount.ContainsKey(topic.id) ? zdtopicReplyCount[topic.id] : 0%>
							</td>
							<td style="text-align: center;vertical-align:middle;">
                                <%=topic.topicuser.nickname%>
							</td>
							<td style="text-align: center;vertical-align:middle; width: 200px;">
								<strong>
                                    <%=zdtopicLastReply.ContainsKey(topic.id) ? zdtopicLastReply[topic.id].replyuser.nickname : ""  %>
								</strong><br />
                                    <%=zdtopicLastReply.ContainsKey(topic.id) ? zdtopicLastReply[topic.id].publishtime : null %>
							</td>
							<td style="text-align: center;vertical-align:middle;">
								
                                <%
                                    bbs.Model.User user = (bbs.Model.User)Session["userInfo"];  //当前登录的用户对象
                                    if (user != null)
                                    {
                                        if (user.id == topic.topicuser.id && user.id != section.user.id &&user.type != "3")  //判断此主贴是不是自己发的
                                        {
                                        %>
                                            <button class="btn btn-info" data-backdrop="static" data-toggle="modal" data-target="#dlgUpdate" onclick="return updateTopic(<%=topic.id %>, '<%=topic.title %>', '<%=(topic.content).Substring(0,(topic.content).Length-2)%>' )">修改帖子</button>
                                            <button class="btn btn-danger" onclick="javascript:deleteTopic(<%=topic.id %>)">删除</button>
                                        <%
                                        }
                                        else if(user.id == section.user.id) //判断是不是版主
                                        {  
                                        %>
                                            <button class="btn btn-info" data-backdrop="static" data-toggle="modal" data-target="#dlg" onclick="return modifyTopic(<%=topic.id %>,<%=topic.top %>,<%=topic.good %>)">修改</button>
										    <button class="btn btn-danger" onclick="javascript:deleteTopic(<%=topic.id %>)">删除</button>
                                        <%
                                        }
                                        else if(user.type == "3")
                                        { 
                                        %>
                                            <button class="btn btn-info" data-backdrop="static" data-toggle="modal" data-target="#dlg" onclick="return modifyTopic(<%=topic.id %>,<%=topic.top %>,<%=topic.good %>)">修改</button>
										    <button class="btn btn-danger" onclick="javascript:deleteTopic(<%=topic.id %>)">删除</button>
                                        <%
                                        }
                                        else
                                        { %>
                                            您无权对本帖进行操作！
                                          <%
                                         }
                                     }
                                         else { Response.Write("您无权对本帖进行操作！"); }
                                %>
							</td>
						</tr>
					 <% 
                         }
                     %>
				</table>
			</td>
		</tr>
		
		<!-- 其他帖子 -->
		<!-- <tr height="30">
			<td style="text-indent:5;" background="images/index/classT.jpg"><b><font color="white">■ 其它帖子</font></b></td>
		</tr> -->
		
		<tr>
			<td>
				<table class="table table-bordered" width="100%" cellspacing="0" cellpadding="0" style="margin-top: 8;">
					<!-- <tr>
						<th style="text-align: center;vertical-align: middle; width: 150px;">
							状态
						</th>
						<th style="text-align: center;vertical-align: middle;">
							帖子标题
						</th>
						<th style="text-align: center;vertical-align: middle; width: 100px;">
							回复数 	
						</th>
						<th style="text-align: center;vertical-align: middle; width: 100px;">
							发表者
						</th>
						<th style="text-align: center;vertical-align: middle; width: 200px;">
							最后回复
						</th>
						<th style="text-align: center;vertical-align: middle; width: 150px;">
							操作
						</th>
					</tr> -->
					

                    <%
                        foreach (bbs.Model.Topic topic in ptTopicList){
                    %>

						<tr>
							<td style="text-align: center;vertical-align:middle;width: 150px;">

                                <%
                                    if (topic.good == "0")
                                    {
                                %>
                                        <span style = 'color: blue; ' >【普通】</ span >
                                    <%}
                                    else{
                                    %>
                                        <span style = 'color: red; ' >【精华】</ span >
                                    <%
                                        }
                                %>

							</td>
							<td style="text-align: center;vertical-align:middle;">
								<a href="/topic/TopicDetails.aspx?topicId=<%=topic.id %>"><%=topic.title%></a>
							</td>
							<td style="text-align: center;vertical-align:middle;width: 100px;">

                                <%= topicReplyCount.ContainsKey(topic.id) ? topicReplyCount[topic.id] : 0%>
							</td>
							<td style="text-align: center;vertical-align:middle;width: 100px;">
								
                                <%=topic.topicuser.nickname%>
							</td>
							<td style="text-align: center;vertical-align:middle;width: 200px;">
								<strong>
                                    <%=topicLastReply.ContainsKey(topic.id) ? topicLastReply[topic.id].replyuser.nickname : ""  %>
								</strong><br />
                                    <%=topicLastReply.ContainsKey(topic.id) ? topicLastReply[topic.id].publishtime : null %>

							</td>

							<td style="text-align: center;vertical-align:middle;width: 150px;">

                                <%
                                    bbs.Model.User user = (bbs.Model.User)Session["userInfo"];  //当前登录的用户对象
                                    if (user != null)
                                    {
                                        if (user.id == topic.topicuser.id && user.id != section.user.id && user.type != "3")  //判断此主贴是不是自己发的
                                        {
                                        %>
                                            <%--<%=(topic.content).Substring(0,(topic.content).Length-2)%>截取字符串--%>
                                            <button class="btn btn-info" data-backdrop="static" data-toggle="modal" data-target="#dlgUpdate" onclick="return updateTopic(<%=topic.id %>, '<%=topic.title %>', '<%=(topic.content).Substring(0,(topic.content).Length-2)%>' )">修改帖子</button>
                                            <button class="btn btn-danger" onclick="javascript:deleteTopic(<%=topic.id %>)">删除</button>
                                        <%
                                        }
                                        else if(user.id == section.user.id) //判断是不是版主
                                        {  
                                        %>
                                            <button class="btn btn-info" data-backdrop="static" data-toggle="modal" data-target="#dlg" onclick="return modifyTopic(<%=topic.id %>,<%=topic.top %>,<%=topic.good %>)">修改</button>
										    <button class="btn btn-danger" onclick="javascript:deleteTopic(<%=topic.id %>)">删除</button>
                                        <%
                                        }
                                        else if(user.type == "3")
                                        { 
                                        %>
                                            <button class="btn btn-info" data-backdrop="static" data-toggle="modal" data-target="#dlg" onclick="return modifyTopic(<%=topic.id %>,<%=topic.top %>,<%=topic.good %>)">修改</button>
										    <button class="btn btn-danger" onclick="javascript:deleteTopic(<%=topic.id %>})">删除</button>
                                        <%
                                        }
                                        else
                                        { %>
                                            您无权对本帖进行操作！
                                          <%
                                         }
                                     }
                                         else { Response.Write("您无权对本帖进行操作！"); }
                                %>
							</td>
						</tr>

                    <%
                        }
                    %>

				</table>
			</td>
		</tr>
	</table>
</div>

<%--修改帖子属性--%>
<div id="dlg" class="modal hide fade"  tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
			<div class="modal-header">
				<button type="button" class="close" data-dismiss="modal"
					aria-hidden="true" onclick="return resetValue()">×</button>
				<h3 id="myModalLabel">修改主题</h3>
			</div>
			<div class="modal-body">
				<form id="fm" action="#" method="post" enctype="multipart/form-data">
					<table>
						<tr>
							<td>
								<label class="control-label" for="top">置顶：</label>
							</td>
							<td>
								<select id="topicTop">
									<option value="0">非置顶</option>
									<option value="1">置顶</option>
								</select>
							</td>
						</tr>
						<tr>
							<td>
								<label class="control-label" for="good">精华：</label>
							</td>
							<td>
								<select id="topicGood">
									<option value="0">非精华</option>
									<option value="1">精华</option>
								</select>
							</td>
						</tr>
					</table>
					<input id="topicId" type="hidden">
				</form>
			</div>
			<div class="modal-footer">
				<font id="error" style="color: red;"></font>
				<button class="btn" data-dismiss="modal" aria-hidden="true"
					onclick="return resetValue()">关闭</button>
				<button class="btn btn-primary" onclick="javascript:saveTopic()">保存</button>
			</div>
</div>
<%--修改帖子属性--%>


<%--修改帖子内容--%>

<div id="dlgUpdate" class="modal hide fade"  tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
			<div class="modal-header">
				<button type="button" class="close" data-dismiss="modal"
					aria-hidden="true" onclick="return resetValue()">×</button>
				<h3 id="myModalLabelUpdate">修改帖子</h3>
			</div>
			<div class="modal-body">
				<form id="fm1" action="#" method="post" enctype="multipart/form-data">
					<table>
						<tr>
							<td>
								<label class="control-label" for="top">标题：</label>
							</td>
							<td>
                                <input type="text" id="title" />
								<%--<select id="topicTitle">
									<option value="0">非置顶</option>
									<option value="1">置顶</option>
								</select>--%>
							</td>
						</tr>
						<tr>
							<td>
								<label class="control-label" for="good">内容：</label>
							</td>
							<td>
                                <textarea name="topic.content" id="content" class="ckeditor" style="height: 180px; width: 300px;" ></textarea>


								<%--<select id="topicGood">
									<option value="0">非精华</option>
									<option value="1">精华</option>
								</select>--%>
							</td>
						</tr>
					</table>
                    <input id="topicIds" type="hidden">
				</form>
			</div>
			<div class="modal-footer">
				<font id="error1" style="color: red;"></font>
				<button class="btn" data-dismiss="modal" aria-hidden="true"
					onclick="return resetValue()">关闭</button>
				<button class="btn btn-primary" onclick="javascript:updateTopicInfo()">保存</button>
			</div>
</div>

<%--修改帖子内容--%>

<div id="footer" style="width: 1200px; margin: 0 auto;">
    <% Server.Execute("/common/Footer.aspx"); %>
</div>
</body>
</html>
