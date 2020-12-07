<%@ Page Language="C#" AutoEventWireup="true" ClientIDMode="Static" CodeBehind="MaintenanceRegister.aspx.cs" Inherits="ITWorkShopHomePage.MaintenanceRegister" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Common/main.css" rel="stylesheet" />
    <link href="~/Common/common.css" rel="stylesheet" />
    <link href="~/Common/layui/css/layui.css" rel="stylesheet" />
    <script src="Common/jquery.min.js"></script>
    <style type="text/css">
        .auto-style1 {
            height: 27px;
        }
        .auto-style2 {
            height: 26px;
        }
    </style>
</head>
<body class="body" runat="server">
    <form id="form1" method="post" runat="server">
        <div class="center">
            <h2 class="center">
                <asp:Label ID="Form_Title" Text="" runat="server"></asp:Label></h2>
            <img src="Images/Code128.jpeg" style="-webkit-user-select: none;margin: auto;" height="15" width="250" aria-readonly="true" class="auto-style2" />
            <p></p>
        </div>               
        <div class="smartflow_MainRegister_content">
            <table width="100%" border="0" align="center" cellpadding="1" cellspacing="1" id="TABLE1" runat="server">
                <%--<tr>
                    <td width="16%" align="center" bgcolor="#FFFFFF" class="F">设置名称</td>
                    <td width="42%" height="25" align="center" bgcolor="#FFFFFF" class="F">基本参数设置</td>
                    <td width="21%" align="center" bgcolor="#FFFFFF" class="F">设置说明</td>
                    <td width="21%" align="center" bgcolor="#FFFFFF" class="F">单号:</td>
                </tr>--%>
                <tr>
                    <td align="center" bgcolor="#F6F6F6">单号：</td>
                    <td align="left" bgcolor="#F6F6F6" style="height: 27px">
                        <%-- <asp:TextBox ID="TextBox1" runat="server" Width="298px" Text="<%# ITWorkShopWorkFlow.BLL.UserSession.order%>" ReadOnly="true"></asp:TextBox>--%>
                        <label id="labticketnum" aria-readonly="true" runat="server"></label>
                    </td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#F6F6F6">用户ID(NTID)：</td>
                    <td align="left" bgcolor="#F6F6F6" style="height: 27px">
                        <asp:TextBox ID="txtID" runat="server" Width="298px" Text=""></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtID"
                            ErrorMessage="用户ID不能为空" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#F6F6F6" class="auto-style1">电话：</td>
                    <td align="left" bgcolor="#F6F6F6" class="auto-style1">
                        <asp:TextBox ID="txtPhone" runat="server" Width="298px" Text=""></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#F6F6F6">设备类型：</td>
                    <td height="25" align="left" bgcolor="#F6F6F6">
                        <asp:DropDownList ID="ddlDeviceType" runat="server" Width="180px" DataTextField="DeviceType" DataValueField="DeviceType" >
                        </asp:DropDownList>
                        
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDeviceType"
                            ErrorMessage="设备类型不能为空" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td bgcolor="#F6F6F6"></td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#F6F6F6">设备名称：</td>
                    <td align="left" bgcolor="#F6F6F6" style="height: 27px">
                        <asp:TextBox ID="txtDeviceName" runat="server" Width="298px" Text=""></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDeviceName"
                            ErrorMessage="设备名称不能为空" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#F6F6F6">序列号：</td>
                    <td align="left" bgcolor="#F6F6F6" style="height: 27px">
                        <asp:TextBox ID="txtSN" runat="server" Width="298px" Text=""></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSN"
                            ErrorMessage="序列号不能为空" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#F6F6F6">MAC地址：</td>
                    <td align="left" bgcolor="#F6F6F6" style="height: 27px">
                        <asp:TextBox ID="txtMAC" runat="server" Width="298px" Text=""></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#f6f6f6" style="height: 27px">图片：</td>
                    <td align="left" bgcolor="#f6f6f6" style="height: 27px">
                        <asp:TextBox ID="tbPicPath" runat="server"></asp:TextBox>
                        <asp:FileUpload ID="FileUpload1" runat="server" Height="22px" Width="185px" />
                        <asp:Button ID="btnUpLoad" runat="server" Text="上传图片" OnClick="btnUpLoad_Click" />
                        <span style="color: #808080">/上传图片(可选)</span></td>
                    <td bgcolor="#f6f6f6" style="height: 27px"></td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#F6F6F6">登记时间：</td>
                    <td align="left" bgcolor="#F6F6F6" style="height: 27px">
                        <asp:TextBox ID="txtRegisterTime" runat="server" Width="180px" Text="" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#F6F6F6">接收人ID(NTID)：</td>
                    <td align="left" bgcolor="#F6F6F6" style="height: 27px">
                        <asp:TextBox ID="txtReceiver" runat="server" Width="180px" Text="" ReadOnly="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtReceiver"
                            ErrorMessage="接收人不能为空" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#F6F6F6" style="height: 27px">进度：</td>
                    <td align="left" bgcolor="#F6F6F6" style="height: 27px">
                         <asp:DropDownList ID="ddlSteps" runat="server" Width="180px" DataValueField="Step" DataTextField="Step">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlSteps"
                            ErrorMessage="进度不能为空" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td bgcolor="#F6F6F6" style="height: 27px"></td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#F6F6F6" class="auto-style1">备注：</td>
                    <td align="left" bgcolor="#F6F6F6" class="auto-style1">
                        <asp:TextBox ID="txtRemarks" runat="server" Width="298px" Text=""></asp:TextBox>
                    </td>
                    <td bgcolor="#F6F6F6" class="auto-style1"></td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#f6f6f6" style="height: 27px">说明信息：</td>
                    <td align="left" bgcolor="#f6f6f6" style="height: 27px">请填写必须项，用于项目跟踪。该项目流程依据流程节点自动进行提醒操作，直到流程结束！</td>
                    <td bgcolor="#f6f6f6" style="height: 27px"></td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#f6f6f6" style="height: 27px"></td>
                    <td align="left" bgcolor="#f6f6f6" style="height: 27px"></td>
                    <td bgcolor="#f6f6f6" style="height: 27px"></td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#f6f6f6" style="height: 27px"></td>
                    <td align="left" bgcolor="#f6f6f6" style="height: 27px">
                        <asp:Button ID="Button1" runat="server" Text="提 交" Width="104px" OnClick="Button1_Click" /></td>
                    <td bgcolor="#f6f6f6" style="height: 27px"></td>
                </tr>

            </table>
        </div>
    </form>
</body>
</html>

