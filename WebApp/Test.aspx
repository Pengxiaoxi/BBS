<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="WebApp.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <table border="1">
        <%
            foreach (bbs.Model.Zone zone in zoneList)
            {
        %>
            <tr>
                <td><%=zone.id %></td>
                <td><%=zone.name %></td>
                <td><%=zone.description %></td>

            </tr>
           
        <%
             }
        %>
    </table>

        <br />
        <img src="/Login_ValidateCode.ashx" onclick="this.src='/Login_ValidateCode.ashx?d='+Math.random()" />

        <br />
        <%=Server.UrlEncode("我想吃蛋炒饭") %>      <%--将字符串以URL编码--%>

</body>
</html>
