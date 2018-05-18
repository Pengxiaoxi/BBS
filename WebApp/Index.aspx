<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebApp.Index" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>论坛首页</title>
    <link href="/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="/bootstrap/css/bootstrap-responsive.css" rel="stylesheet" />

    <script src="/bootstrap/js/jquery.js"></script>
    <script src="/bootstrap/js/bootstrap.min.js"></script>
    <script src="/bootstrap/js/bootstrap.js"></script>
</head>

<body>
    <div id="header" class="wrap" style="width: 1200px; margin: 0 auto;">
	    <%--<jsp:include page="common/top.jsp"/>--%>
        <% Server.Execute("/common/Top.aspx"); %>  <%--Execute是从当前页面转移到指定页面，并将执行返回到当前页面--%>

    </div>
    <div id="content" style="width: 1200px; margin: 0 auto;">
	    <%-- <jsp:include page="common/default.jsp"></jsp:include> --%>
	    <%--<jsp:include page="common/default.jsp"></jsp:include>--%>

        <% Server.Execute("/common/Default.aspx"); %>
    </div>
    <div id="footer" style="width: 1200px; margin: 0 auto;">
	    <%--<jsp:include page="common/footer.jsp"/>--%>

        <% Server.Execute("/common/Footer.aspx"); %>
    </div>
</body>
</html>
