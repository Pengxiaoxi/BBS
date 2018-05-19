<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApp.admin.js.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>后台登录</title>
<link rel="stylesheet" href="/admin/css/bootstrap.min.css" />
<link rel="stylesheet" href="/admin/css/bootstrap-responsive.min.css" />
<link rel="stylesheet" href="/admin/css/unicorn.login.css" />

<script src="/admin/js/jquery.min.js"></script>  
<script src="/admin/js/unicorn.login.js"></script> 
</head>
<body>
	<div id="logo">
        <img src="/admin/img/logo1.jpg" alt="" />
    </div>
    <div id="loginbox">
		<form id="loginform" class="form-vertical" runat="server">
			<p>输入用户昵称和密码进入后台.</p>
			<div class="control-group">
				<div class="controls">
					<div class="input-prepend">
						<span class="add-on"><i class="icon-user"></i></span><input
							type="text" name="user.nickName" value="<%=nickName %>" placeholder="昵称" />
					</div>
				</div>
			</div>
			<div class="control-group">
				<div class="controls">
					<div class="input-prepend">
						<span class="add-on"><i class="icon-lock"></i></span><input
							type="password" name="user.password" value="" placeholder="密码" />
					</div>
				</div>
			</div>
			<div class="form-actions">
				 <span class="pull-right">
				 	<font id="error" style="font-size: 20px;" color="red"><%=error %></font>
				 	<input type="submit" class="btn btn-inverse" value="进入后台" />
				 </span>
			</div>
		</form>
	</div>
</body>
</html>
