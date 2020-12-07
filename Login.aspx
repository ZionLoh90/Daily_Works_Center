<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ITWorkShopHomePage.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>IT WorkShop HomePage Login</title>
    <link href="Common/Login.css" rel="stylesheet" />
    <link href="Common/Images/jabil.ico" rel="shortcut icon" type="image/x-icon" />
    <script src="Common/common.js"></script>
    <script src="Common/jquery.min.js"></script>

    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('#currentpassWord').on('keypress', function (e) {
                if (event.keyCode == 13) {
                    event.cancelBubble = true;
                    event.returnValue = false;
                    $(this).parents("div").find("#login").click();
                }
            });
        });
    </script>
</head>
<body runat="server">
    <div class="login">
        <div class="message">IT WorkShop HomePage Login</div>
        <div id="darkbannerwrap"></div>
        <form runat="server" method="post" id="frmLogin">
            <input name="username" placeholder="用户名" type="text" value="" id="userName" runat="server" />
            <hr class="hr15" />
            <input name="password" placeholder="密码" type="password" id="currentpassWord" runat="server" />
            <hr class="hr15" />
            <input value="登录" type="button" id="login" onserverclick="btnLogin_Click" runat="server" />
            <div id="remarks">请使用您的Jabil NT账号/密码进行登录(建议使用Google浏览器)<p style="color:red;font-style:italic">注意：如果连续输错五次密码，您的账号将会被锁定!</p></div>
        </form>
        <%--<div id="footer">
            <footer>
                <p>&copy; <%: DateTime.Now.Year %> HUA IT Specialist Support Team@JABIL</p>
            </footer>
        </div>--%>
    </div>

</body>
    <script type="text/javascript">
        // 禁止登录页内嵌
        if (window != top) {
            parent.location.href = location.href;
        }
    </script>
</html>

