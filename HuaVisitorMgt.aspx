<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HuaVisitorMgt.aspx.cs" Inherits="ITWorkShopHomePage.HuaVisitorMgt" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <%--<meta http-equiv="refresh" content="120">--%>
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0" />
    <title>HUA Vistor Password Management System</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Expires" content="-1" /> 
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=2.0, minimum-scale=1.0">
    <link rel="stylesheet" href="Common/layui/css/layui.css" media="all" />
    <link href="Common/nav/font-awesome.min.css" rel="stylesheet" />
    <script type="text/javascript" src="Common/layui/css/layui.js"></script>
    <script type="text/javascript" src="Common/jquery.min.js"></script>

    <style type="text/css">
        /*谷哥滚动条样式*/

        ::-webkit-scrollbar {
            width: 5px;
            height: 5px;
            position: absolute;
        }

        ::-webkit-scrollbar-thumb {
            background-color: #5bc0de;
        }

        ::-webkit-scrollbar-track {
            background-color: #ddd;
        }

        /***/
        body {
            /*margin-top: 2%;*/
            /*margin-left: 10%;*/
            /*margin-right: 20%;*/
            min-height: 200px;
            max-height: 900px;
            text-align: center;
            /*background-color: #dcd9d9;*/
            /*background: 0 0;*/
        }

        .bg {
            background-image: url('Images/bg-login-15.png');
            background-size: cover;
        }

        .demo-carousel {
            height: 200px;
            line-height: 200px;
            text-align: center;
        }

        .top {
            margin-top: 3%;
            margin-bottom: 1%;
        }

        .iframe {
            /*width:40%;*/
            /*height:500px;*/
            margin-top: 10px;
            /*margin-left:10%;*/
        }

        #weather {
            float: left;
            width: 30%;
            margin-top: 3%;
            margin-left: 4%;
            /*height:5%;*/
            border-radius: 15px;
            opacity: 0.5;
        }

        #if1 {
            border-radius: 50px;
            /*background:rgba(0,0,0,5);*/
            background-image: linear-gradient(to right, #ccc, #fff, #ccc);
            opacity: 0.6;
        }

            #if1 div {
                position: relative;
            }

        #wifi {
            float: right;
            margin-top: 3%;
            margin-left: 3%;
            margin-right: 5%;
            /*height:5%;*/
            width: 48%;
            text-align: left;
        }

        .header {
            font-size: 36px;
            color: #1E9FFF;
            font-style: initial;
            text-align: center;
        }

        .textheader {
            font-size: 22px;
            text-align: left;
            color: #ffffff;
            border: none;
        }

        .textbuttom {
            font-size: 14px;
            text-align: left;
            color: #d1d1d1;
        }

        .text {
            font-size: 22px;
            text-align: center;
            color: #ff0000;
        }

        .logo {
            height: 40px;
            width: 200px;
        }
        /*渐变*/
        hr {
            width: 100%;
            margin: 0 auto;
            border: 0;
            height: 2px;
            background: #333;
            background-image: linear-gradient(to right, #ccc, #333, #ccc);
        }

        #tips {
            width: 90%;
            margin: 0 auto;
            border: 0;
            height: 2px;
            background: #333;
            background-image: linear-gradient(to right, #ccc, #333, #ccc);
        }
    </style>

    <script type="text/javascript" src="Common/layui/css/layui.js"></script>
    <script src="http://pv.sohu.com/cityjson?ie=utf-8"></script>    
    <!--获取接口数据，注意charset -->
    <script type="text/javascript">
        //$(window).load(function(){})在高版本中已经废弃,请用：$(window).on('load',function(){});替代
        $(window).on('load', function () {
            //document.getElementById('if1').style.backgroundColor = '#ffffff';
            var clientinf = returnCitySN["cip"] + "(" + returnCitySN["cname"] + ")";            //输出接口数据中的IP地址
            //alert(appName + ";" + navigator.product + "/" + navigator.appVersion + ";" + window.screen.height + "*" + window.screen.width );
            document.getElementById("notif3").innerHTML = "Environment:" + navigator.product + "[" + navigator.userAgent + "] ;ClientLanguage: " + navigator.language + "; Display: " + window.screen.height + "*" + window.screen.width + "; ClientAddress: " + clientinf;
        });
    </script>
</head>
<body class="layui-card-body bg" runat="server"  oncontextmenu="return(false)">
    <form id="form1" runat="server" class="layui-form">
         <img src="Common/Images/logo.png" class="top" />
                    
        <div class="layui-container">
            <hr />
            <div>
                <asp:Timer ID="Timer2" runat="server" Interval="300000">
                    <!--间隔刷新时间半小时-->
                </asp:Timer>
                <div id="weather">
                    <iframe runat="server" id="if1" scrolling="no" style="color: #ff0000" src="https://tianqiapi.com/api.php?style=ya&skin=gif&city=%E9%BB%84%E5%9F%94" frameborder="0" allowtransparency="true"></iframe>
                </div>
                <script>
                    document.getElementById('if1').style.fontStyle.fontcolor = "#ffffff";
                </script>
                <div id="wifi">
                    <asp:ScriptManager ID="smScriptManager" runat="server">
                        <%--定时更新password区域--%>
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="upLinkmanList" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:Timer ID="Timer1" runat="server" Interval="3000">
                                <!--间隔刷新时间 秒-->
                            </asp:Timer>
                            <div>
                                <i class="fa fa-wifi" style="font-size: 36px; color: #1E9FFF;"></i><label id="title" class="header"><strong>Hua Visitor WIFI</strong></label>
                                <br />
                                <i class="fa fa-clock-o" style="font-size:14px;color:#d1d1d1"></i><label id="updatelab" class="textbuttom" runat="server">LastUpdate:</label>
                                <asp:Label ID="updatetime" runat="server" ReadOnly="true" BorderStyle="None" Style="color: #eaeae8; font-size: 14px"></asp:Label>
                                <i class="fa fa-clock-o" style="font-size:14px;color:#d1d1d1"></i><label id="updatelab2" class="textbuttom" runat="server">NextUpdate:</label>
                                <asp:Label ID="nextupdatetime" runat="server" ReadOnly="true" BorderStyle="None" Style="color: #eaeae8; font-size: 14px"></asp:Label>
                                <%--<br />
                            <label id="updatelab2" class="textbuttom" runat="server">Update By:</label>
                            <asp:Label ID="updateby" runat="server" Text="" ReadOnly="true" BorderStyle="None" Style="color: #eaeae8; font-size: 14px"></asp:Label>--%>
                                <br />
                                <br />
                                <label id="name" class="textheader">UserName:</label>
                                <label id="username" class="textheader" style="color: #259FE5">huavisitor2</label>
                                <br />
                                <br />
                                <label id="pwd" class="textheader">PassWord:</label>
                                <asp:Label CssClass="text" runat="server" ID="password" AutoPostBack="false" Text=""></asp:Label>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="layui-container"> 
            <hr />
                <asp:Label runat="server" ID="notify0" CssClass="textheader" Text="" BorderStyle="None"  Width="100%">Tips:</asp:Label>
                <asp:Label runat="server" ID="notify" CssClass="textbuttom" Text="" BorderStyle="None"  Width="100%">1.Connect your WIFI to:<a href="javascript:void(0);" style="font-size:16px;color:#1E9FFF">huavisitor2</a></asp:Label>
                <asp:Label runat="server" ID="notify2" CssClass="textbuttom" Text="" BorderStyle="None" Width="100%">2.Open <a href="javascript:void(0);" style="font-size:16px;color:#1E9FFF">IE browser</a> and navigate to URL:<a href="javascript:void(0);" style="font-size:16px;color:#1E9FFF">https://2.2.2.2</a> (Only <a href="javascript:void(0);" style="font-size:16px;color:#1E9FFF">IE browser</a> is supported for WIFI authentication!)</asp:Label>
                <asp:Label runat="server" ID="notif3" CssClass="textbuttom" Text="" BorderStyle="None" Width="100%"></asp:Label>
                <asp:Label runat="server" ID="notify4" CssClass="textbuttom" Text="" ReadOnly="True" Width="100%"></asp:Label>
                <asp:Label runat="server" CssClass="textbuttom" Text="Huangpu IT 7*24(Hours) Support Hotline(MainPlant:+86 18688890902 / ENE:+86 18688890912)"></asp:Label>
        </div> 
    </form>

</body>

</html>
