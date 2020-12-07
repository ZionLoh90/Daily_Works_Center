<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeetingRoomTest.aspx.cs" Inherits="ITWorkShopHomePage.MeetingRoomTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Common/layui/css/layui.css" rel="stylesheet" />
    <link href="Common/nav/font-awesome.min.css" rel="stylesheet" />
    <link href="Common/layui/css/layui.all.js" rel="stylesheet" />
    <script src="Common/jquery.min.js"></script>
    <style>
        /*Chrome滚动条样式*/

  ::-webkit-scrollbar {width:5px;height:5px;position:absolute}
  ::-webkit-scrollbar-thumb {background-color:#5bc0de}
  ::-webkit-scrollbar-track {background-color:#ddd}

        /***/
        #pagetitle{
            text-align:center;
            font-size:24px;
        }
        .container{
            margin-top:20px;
            text-align:left;
        }
        .boxwidth{
            width:30%;
            text-align:left;
            padding-left:10px;
        }
        .bg1{
            background-color:#0094ff;
            border-radius:15px;
            height:300px;
            float:left;
            overflow:hidden;
        }
        .bg2{
            background-color:#b6ff00;
            border-radius:15px;
            height:300px;
            margin-left:3.5%;
            float:left;
            overflow:hidden;
        }
        .bg3{
            background-color:#808080;
            border-radius:15px;
            height:300px;
            float:right;
            overflow:hidden;
        }
        .bk1{
            background-color:#0094ff;
            /*border: 1px dashed #fff;*/
            background: linear-gradient(to bottom, #0094ff, #011a2f);
            /*background: repeating-linear-gradient(135deg, transparent, transparent 3px, #000 3px, #000 8px);*/
            background-origin: border-box;
            animation: shine 1s infinite linear;
            overflow: hidden;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="form">
        <div class="layui-container container">
            <div class="layui-row">
                <div id="pagetitle"><span>Projector Health Dashboard</span></div>
                <div class="layui-row layui-col-space10  container">
                    <div class="boxwidth bg1">
                        <!--layui-col-md4-->
                        <%--<div class="layui-box">--%>
                            <%--<ul>
                                <li>
                                    <label class="layui-form-label">ProjectorName:HongKong</label>
                                </li>
                                <li>
                                    <label class="layui-form-label">PowerStatus:</label>
                                </li>
                                <li>
                                    <label class="layui-form-label">NumOfLamps:</label>
                                </li>
                                <li>
                                    <label class="layui-form-label">Company:NEC Display Solutions, Ltd.</label>
                                </li>
                                <li>
                                    <label class="layui-form-label">186 Hr(s)</label>
                                </li>
                            </ul>--%>
                             <ul>
                                <asp:Panel ID="mainten_register" runat="server">
                                    <li>
                                        <span>ProjectorName:HongKong</span>
                                    </li>
                                </asp:Panel>
                                <asp:Panel ID="Info_Change" runat="server">
                                    <li>
                                        <span>PowerStatus:</span>
                                    </li>
                                    <li>
                                        <span>NumOfLamps:</span>
                                    </li>
                                </asp:Panel>
                                <asp:Panel ID="mainten_search" runat="server">
                                    <li>
                                        <span>LampHours:186(hrs)</span>
                                    </li>
                                </asp:Panel>
                            </ul>
                       <%-- </div>--%>
                    </div>
                    <div class="boxwidth bg2">
                        1/3
                    </div>
                    <div class="boxwidth bg3">
                        1/3
                    </div>
                </div>
            </div>
        </div>               
    </form>
</body>
</html>
