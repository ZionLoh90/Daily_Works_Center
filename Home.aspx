<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="ITWorkShopHomePage.Home" %>

<!DOCTYPE html>

<html lang="zh-cn" runat="server">
<head>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta http-equiv="Pragma" content="no-cache,no-store">
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Expires" content="-1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=2.0, minimum-scale=1.0">
    <%-- <meta NAME="ROBOTS" CONTENT="NONE" /> 不允许爬虫访问--%>
    <%--<webopt:bundlereference runat="server" path="~/Content/css" />--%>
    <link href="~/Common/Images/jabil.ico" rel="shortcut icon" type="image/x-icon" />

    <title>IT WorkShop HomePage</title>

    <link href="Common/main.css" rel="stylesheet" />
    <link href="Common/Login.css" rel="stylesheet" />
    <link href="Common/common.css" rel="stylesheet" />
    <link href="Common/layui/css/layui.css" rel="stylesheet" />
    <link href="Common/nav/font-awesome.min.css" rel="stylesheet" />
    <link href="Common/layui/css/layui.all.js" rel="stylesheet" />
    <script src="Common/jquery.min.js"></script>

    <style>
        /*@charset "utf-8";*/
        /* 以下实际使用若已初始化可删除 .nav height父级需逐级设置为100%*/

        #li {
            list-style: none;
        }

        #a {
            text-decoration: none;
        }

        /* 以上实际使用若已初始化可删除 */

        #navn > div {
            display: inline-block;
            vertical-align: top;
            overflow: hidden;
        }
        /* nav */

        .nav-top {
            border-bottom: 1px solid rgba(255, 255, 255, .1);
        }

            .nav-top .nav-toptxt {
                border-bottom: 1px solid rgba(255, 255, 255, .1);
                color: #fff;
                font-size: 34px;
                width: 60px;
                text-align: center;
                cursor: pointer;
            }

        .nav-top-title {
            text-align: center;
            font-size: 16px;
            color: #e8e5e5;
        }

        .nav {
            width: 200px;
            height: 100%;
            background: #004e8e; /*#144B85;*/
            transition: all .3s;
            overflow: auto;
            font-size: 18px;
        }

            .nav a {
                display: block;
                overflow: hidden;
                padding-left: 20px;
                line-height: 40px;
                max-height: 46px;
                color: #fff;
                transition: all .3s;
            }

            .nav #a.on {
                color: #666;
                text-decoration: none;
            }

            .nav a span {
                margin-left: 10px;
                text-decoration: none;
            }

        .nav-item {
            position: relative;
        }

            .nav-item.nav-show {
                border-bottom: none;
            }

            .nav-item ul {
                display: none;
                /*background: rgba(0, 0, 0, .1);*/
                background: #004e8e;
            }

            .nav-item li ul {
                display: none;
                /*background: rgba(0, 0, 0, .1);*/
                background: #004e8e;
            }

            .nav-item.nav-show ul {
                display: block;
            }

            /*鼠标经过菜单左侧竖线*/
            .nav-item > a:before {
                content: "";
                position: absolute;
                left: 0px;
                width: 3px;
                height: 35px;
                background: #fff; /*#34A0CE;*/
                /*background: #004e8e;*/
                opacity: 0;
                transition: all .3s;
            }

        .nav .nav-icon {
            font-size: 18px;
            position: absolute;
            margin-left: -1px;
            top: 14px;
        }
        /* 此处修改导航图标 可自定义iconfont 替换*/

        .icon_1::after {
            content: "";
            display: block;
            width: 20px;
            height: 20px;
            /*background: url(men1.png)no-repeat;*/
        }

        .nav-item:nth-child(1) .icon_1::after {
            /*background: url(men1.png)no-repeat;*/
            background-size: 100% 100%;
        }

        .nav-item:nth-child(2) .icon_1::after {
            /*background: url(men2.png)no-repeat;*/
            background-size: 100% 100%;
        }

        .nav-item:nth-child(3) .icon_1::after {
            /*background: url(men3.png)no-repeat;*/
            background-size: 100% 100%;
        }
        /*.nav-item:nth-child(2) .icon_1::after{background: url(men1.png)no-repeat;}*/
        /*.icon_2::after{content: "\e669";}*/
        /*.icon_3::after{content: "\e61d";}*/
        /*---------------------*/

        .nav-more {
            float: right;
            margin-top: 10px;
            margin-right: 30px;
            font-size: 14px;
            transition: transform .3s;
        }
            /* 此处为导航右侧箭头 如果自定义iconfont 也需要替换*/

            .nav-more::after {
                content: ">";
                color: #fff;
                font-style: normal;
                font-family: SimSun;
            }
        /*---------------------*/

        .nav-show .nav-more {
            transform: rotate(90deg);
        }

        .nav-show,
        .nav-item > a:hover {
            /*color: #fff;*/ /*#2AF8CB;*/
            background-color: #0459ae;
            text-decoration: none;
            color: #eee;
        }

            .nav-show > a:before,
            .nav-item > a:hover:before {
                opacity: 1;
            }

        .nav-item li:hover a {
            /*color: #FFF;*/
            background: #0459ae; /*#3399FF;*/
            text-decoration: none;
        }
        /* nav-mini */

        .nav-mini.nav {
            width: 60px;
        }

            .nav-mini.nav .nav-icon {
                /*margin-left:-2px;*/
            }

            .nav-mini.nav .nav-item > a span {
                display: none;
            }

            .nav-mini.nav .nav-more {
                margin-right: -20px;
            }

            .nav-mini.nav .nav-item ul {
                position: absolute;
                top: 0px;
                left: 60px;
                width: 180px;
                z-index: 99;
                background: #004e8e; /*#144B76;*/ /*#144B76;*/
                overflow: auto;
            }

            .nav-mini.nav .nav-item:hover {
                background: rgba(255, 255, 255, .1);
            }

                .nav-mini.nav .nav-item:hover .nav-item a {
                    color: #FFF;
                }

                .nav-mini.nav .nav-item:hover a:before {
                    opacity: 1;
                }

                .nav-mini.nav .nav-item:hover ul {
                    display: block;
                }

        li.act {
            background: #3399FF; /*#0459ae;*/ /*#3399FF;*/
        }
    </style>


    <script type="text/javascript">
        function changeClock() {
            var d = new Date();
            //document.getElementById("clock").innerHTML = d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate() + " " + d.getHours() + ":" + d.getMinutes() + ":" + d.getSeconds();
            document.getElementById("clock").innerHTML = d.getHours() + ":" + d.getMinutes() + ":" + d.getSeconds();
        }
        function hellouser() {
            // 获取当前时间
            let timeNow = new Date();
            // 获取当前小时
            let hours = timeNow.getHours();
            // 设置默认文字
            let text = "";
            // 判断当前时间段
            if (hours > 5 && hours < 10) {
                text = "早上好:";
            } else if (hours >= 10 && hours <= 14) {
                text = "中午好:";
            } else if (hours > 14 && hours <= 18) {
                text = "下午好:";
            } else if (hours > 18 && hours < 24) {
                text = "晚上好:";
            } else if (hours >= 0 && hours <= 5) {
                text = "凌晨好:";
            }
            document.getElementById("labhello").innerHTML = text;
            <%-- var txt = "<%= ITWorkShopWorkFlow.BLL.HoursVerify.hourto()%>";
            document.getElementById("labhello").innerHTML = txt;--%>
        }

<%--        function tick() {
            var lb = document.getElementById("counter");
            lb.innerHTML = "在线人数:" + <%= Application["count"].ToString() %>;
            //window.setTimeout("tick();", 1000);//设置一个定时器 定时改变label的值
        }--%>
        //window.setInterval(tick, 1000);
        window.setInterval(changeClock, 1000);
        window.setInterval(hellouser, 1500);
    </script>

    <script type="text/javascript">
        //可折叠导航栏
        $(function () {
            //				 nav收缩展开
            $('#aat').on('click', '.nav-item>a', function () {
                if (!$('.nav').hasClass('nav-mini')) {
                    if ($(this).next().css('display') == "none") {
                        //展开未展开
                        $('.nav-item').children('ul').slideUp(300);
                        $(this).next('ul').slideDown(300);
                        $(this).parent('li').addClass('nav-show').siblings('li').removeClass('nav-show');
                    } else {
                        //收缩已展开
                        $(this).next('ul').slideUp(300);
                        $('.nav-item.nav-show').removeClass('nav-show');
                    }
                }
            });
            //				 标志点击样式
            $('#aat').on('click', '.nav-item>ul>li', function () {
                $(this).addClass("act").siblings().removeClass("act");
                $(this).parents(".nav-item").siblings().find(">ul>li").removeClass("act");
                //console.log($(this).text())
            })
            //				nav-mini切换

            $('#mini').on('click', function () {
                if (!$('.nav').hasClass('nav-mini')) {
                    $('.nav-item.nav-show').removeClass('nav-show');
                    $('.nav-item').children('ul').removeAttr('style');
                    $('.nav').addClass('nav-mini');
                    $('.nav').css('overflow', 'visible');
                } else {
                    $('.nav').removeClass('nav-mini');
                    $('.nav').css('overflow', 'auto');
                }
            });
        });
    </script>

</head>
<body class="smartflow_main" runat="server">
    <form runat="server">
        <div class="smartflow_head">
            <span class="smartflow_title">IT HomePage
                                <a class="navbar-brand" href="Home.aspx" target="_self" style="background-image: url('Common/Images/logo.png');" title="" data-sys-properties="glide.product.image.light" ng-click="setNavigatingState(true)" data-original-title="IT WorkShop HomePage">
                                    <img id="mainBannerImage16" src="Common/Images/logo.png" style="height: 20px; visibility: hidden;" aria-hidden="true"></a>
            </span>
            <div class="smartflow_menu fr" oncontextmenu="return(false)">
                <ul class="smartflow_menu_items clearfix" id="smartflow_menu_items">
                    <%-- <li class="smartflow_menu_item fr">
                        <asp:Button runat="server" ID="notic" data-method="notice" class="layui-btn" Text="公告" />
                    </li>
                        <li class="smartflow_menu_item fr">
                        <i class="layui-icon layui-icon-logout" style="cursor: pointer;"><a class="alink" href="javascript:window.opener=null;window.open('','_self');window.close();">退出</a></i>
                    </li>--%>
                    <li class="smartflow_menu_item fr">
                        <a class="alink" runat="server" id="logout">注销 | </a>
                        <span id="clock"></span>
                    </li>
                    <li class="smartflow_menu_item fr">
                        <i class="fa fa-delicious" style="cursor: pointer;"></i>
                        <asp:Label runat="server" ID="labLogin" CssClass="smartflow_menu_item fr" Text="" />

                        <asp:Label runat="server" ID="labRole" CssClass="smartflow_menu_item fr" Text="" />
                        <a href="PersonalInfo.html" class="alink" target="Container">
                            <asp:Label runat="server" ID="labUID" CssClass="smartflow_menu_item fr" Text="" /></a>
                        <asp:Label runat="server" ID="labhello" CssClass="smartflow_menu_item fr" Text="" />
                    </li>
                    <%-- <asp:Label ID="counter" runat="server" Text=""></asp:Label>--%>
                </ul>
            </div>
        </div>
        <div class="smartflow_content">
            <%--oncontextmenu="return(false)"禁用右键--%>
            <div class="smartflow_left nav" oncontextmenu="return(false)">
                <div class="nav-top">
                    <div id="mini" class="nav-toptxt">≡</div>
                </div>
                <ul id="aat">
                    <asp:Panel ID="nav_item1" runat="server">
                        <li class="nav-item ">
                            <a href="javascript:;">
                                <i class="fa fa-desktop"></i><span>设备维修</span><i class="nav-more"></i>
                            </a>
                            <ul>
                                <asp:Panel ID="mainten_register" runat="server">
                                    <li>
                                        <a href="MaintenanceRegister.aspx" target="Container"><span>维修登记</span></a>
                                    </li>
                                </asp:Panel>
                                <asp:Panel ID="Info_Change" runat="server">
                                    <li>
                                        <a href="StepTemplate.aspx?Form=MAINTENANCESTEP&User=<%= ITWorkShopHomePage.BLL.UserSession.userid %>" target="Container"><span>进度更新</span></a>
                                    </li>
                                    <li>
                                        <a href="MaintenanceUpdate.aspx" target="Container"><span>信息维护</span></a>
                                    </li>
                                </asp:Panel>
                                <asp:Panel ID="mainten_search" runat="server">
                                    <li>
                                        <a href="MaintenanceSearch.aspx" target="Container"><span>报表查询</span></a>
                                    </li>
                                </asp:Panel>
                            </ul>
                    </asp:Panel>

                    <asp:Panel ID="nav_item2" runat="server">
                        <li class="nav-item">
                            <a href="javascript:;">
                                <i class="fa fa-random"></i><span>供应商入厂</span><i class="nav-more"></i>
                            </a>
                            <ul>
                                <asp:Panel ID="vendor_register" runat="server">
                                    <li>
                                        <a href="javascript:;"><span>入厂登记</span></a>
                                    </li>
                                </asp:Panel>
                                <asp:Panel ID="vendor_update" runat="server">
                                    <li>
                                        <a href="StepTemplate.aspx?Form=VENDORSTEP&User=<%= ITWorkShopHomePage.BLL.UserSession.userid %>" target="Container"><span>信息维护</span></a>
                                    </li>
                                </asp:Panel>
                                <asp:Panel ID="vendor_search" runat="server">
                                    <li>
                                        <a href="javascript:;"><span>记录查询</span></a>
                                    </li>
                                </asp:Panel>
                            </ul>
                        </li>
                    </asp:Panel>

                    <asp:Panel ID="nav_item3" runat="server">
                        <li class="nav-item">
                            <a href="javascript:;">
                                <i class="fa fa-battery-3"></i><span>报废流程</span><i class="nav-more"></i>
                            </a>
                            <ul>
                                <asp:Panel ID="scrap_register" runat="server">
                                    <li>
                                        <a href="javascript:;"><span>报废登记</span></a>
                                    </li>
                                </asp:Panel>
                                <asp:Panel ID="scrap_update" runat="server">
                                    <li>
                                        <a href="javascript:;"><span>信息维护</span></a>
                                    </li>
                                </asp:Panel>
                                <asp:Panel ID="scrap_search" runat="server">
                                    <li>
                                        <a href="javascript:;"><span>记录查询</span></a>
                                    </li>
                                </asp:Panel>
                            </ul>
                        </li>
                    </asp:Panel>

                    <asp:Panel ID="nav_item4" runat="server">
                        <li class="nav-item">
                            <a href="javascript:;">
                                <i class="fa fa-renren"></i><span>IT物品租借</span><i class="nav-more"></i>
                            </a>
                            <ul>
                                <asp:Panel ID="rent_register" runat="server">
                                    <li>
                                        <a href="javascript:;"><span>租借登记</span></a>
                                    </li>
                                </asp:Panel>
                                <asp:Panel ID="rent_update" runat="server">
                                    <li>
                                        <a href="javascript:;"><span>记录修改</span></a>
                                    </li>
                                </asp:Panel>
                                <asp:Panel ID="rent_search" runat="server">
                                    <li>
                                        <a href="javascript:;"><span>报表查询</span></a>
                                    </li>
                                </asp:Panel>
                            </ul>
                        </li>
                    </asp:Panel>

                    <asp:Panel ID="Zion_Tool" runat="server">
                        <li class="nav-item">
                            <a href="javascript:;">
                                <i class="fa fa-android"></i><span>ZionApp</span><i class="nav-more"></i>
                            </a>
                            <ul>
                                <asp:Panel ID="Tool_HR" runat="server">
                                    <li>
                                        <a href="HRInfo.aspx" target="Container"><span>HR信息查询</span></a>
                                    </li>
                                </asp:Panel>
                                <asp:Panel ID="Zion_Tool_IT" runat="server">
                                    <li>
                                        <a href="SelfServicePortal.html" target="Container"><span>ADTool</span></a>
                                    </li>
                                    <li>
                                        <a href="http://cnhuait60/autotask/RFTestResult" target="Container"><span>RF GUN Test Report</span></a>
                                    </li>
                                    <li>
                                        <a href="http://cnhuait60/autotask/printerserver" target="Container"><span>打印服务器</span></a>
                                    </li>
                                    <li>
                                        <a href="http://cnhuam0stg81/ITHomePage/HuaVisitorMgt.aspx" target="Container"><span>HUA Visitor(VMC)</span></a>
                                    </li>
                                    <li>
                                        <a href="http://cnhuait60/Duty/" target="Container"><span>AutoTicket--Duty</span></a>
                                    </li>
                                    <li>
                                        <a href="HuangpuPermissionMgt.html" target="Container"><span>黄埔权限管理中心</span></a>
                                    </li>

                                </asp:Panel>
                            </ul>
                        </li>
                    </asp:Panel>

                    <asp:Panel ID="Zion_Dashboard" runat="server">
                        <li class="nav-item">
                            <a href="javascript:;">
                                <i class="fa fa-dashboard"></i><span>ReportCenter</span><i class="nav-more"></i>
                            </a>
                            <ul>
                                <li>
                                    <a href="AutoTaskDashboard.html" target="Container"><span>AutoTask</span></a>
                                </li>
                            </ul>
                        </li>
                    </asp:Panel>

                    <li class="nav-item">
                        <a href="javascript:;">
                            <i class="fa fa-search"></i><span>常用网站</span><i class="nav-more"></i>
                        </a>
                        <ul>
                            <asp:Panel ID="Tool_IT" runat="server">
                                <li>
                                    <a href="http://huaweb11/ITTools/" target="Container"><span>ITTools</span></a>
                                </li>
                                <li>
                                    <a href="http://huaweb01/SearchTesterInfo/" target="Container"><span>TesterInfo</span></a>
                                </li>
                                <li>
                                    <a href="http://cnhuam0appprd01/MDLC/Sys/Main#" target="Container"><span>资产管理系统</span></a>
                                </li>
                                <li>
                                    <a href="http://cnhuam0appprd01/eGate/" target="Container"><span>E-Gate</span></a>
                                </li>
                            </asp:Panel>
                            <asp:Panel ID="Tool_User" runat="server">
                                <li>
                                    <a href="javascript:;" target="Container"><span>用户常用信息查询</span></a>
                                </li>
                                <li>
                                    <a href="http://cnhuait60/unlock" target="Container"><span>自助解锁NTID</span></a>
                                </li>
                                <li>
                                    <a href="http://hutisd0devsql01/scom/default.asp" target="Container"><span>查询用户被锁设备</span></a>
                                </li>
                                <li>
                                    <a href="https://jabilit.service-now.com/task.do?sysparm_query=number=SCTASK0441412" accesskey="<%= ITWorkShopHomePage.BLL.UserSession.userid %>" target="_blank"><span>Case查询</span></a>
                                </li>
                                <li>
                                    <a href="http://cnhuam0sfmmts81:8900/IT/Index" target="Container"><span>HUA IT Portal</span></a>
                                </li>
                            </asp:Panel>
                        </ul>
                    </li>

                </ul>

            </div>
            <div class="smartflow_right">
                <iframe style="width: 100%; height: 100%;" id="ifrmContent" name="Container" title="ContentFrame" runat="server" border="0" frameborder="0" allowtransparency="true" scrolling="auto" allowfullscreen="true" webkitallowfullscreen="true" mozallowfullscreen="true"></iframe>
            </div>

            }
            <%--<asp:ContentPlaceHolder ID="Right" runat="server">scrolling="yes"
        </asp:ContentPlaceHolder>  --%>
        </div>
    </form>
</body>
<script type="text/javascript">
    if (window != top) {
        parent.location.href = location.href;
    }

    var logout = document.getElementById("logout");
    logout.onclick = function () {
        top.location = "Login.aspx";
    }
</script>
<%--<script type="text/javascript">
    window.onbeforeunload = onbeforeunload_handler;
    window.onunload = onunload_handler;
    function onbeforeunload_handler(){
        var warning="确认退出?";
        //设定时间一秒后触发
        setTimeout(function() {           
            //window.parent.location.reload();//刷新父页面
            var index = parent.layer.getFrameIndex(window.name);
            parent.layer.close(index); //关闭当前页 
        }, 1000);
        return warning;
    }

    function onunload_handler(){
        var warning="欢迎光临!";
        alert(warning);
    }

</script>--%>
</html>

