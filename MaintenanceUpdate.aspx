<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaintenanceUpdate.aspx.cs" Inherits="ITWorkShopHomePage.MaintenanceUpdate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Common/main.css" rel="stylesheet" />
    <link href="Common/common.css" rel="stylesheet" />
    <link href="Common/layui/css/layui.css" rel="stylesheet" />
    <script src="Common/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <h2 class="center"><asp:Label ID="Form_Title" Text="" runat="server"></asp:Label></h2>                   
        <div class="smartflow_content">
            <div id="searcharea" runat="server">
                <p class="text-left">
                    &nbsp;<span style="color: rgb(95, 98, 102); font-size: 13px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: 19px; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: auto; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(249, 251, 252); display: inline !important; float: none;">选择对应的查询条件，然后点击查询</span>
                </p>
                <p>
                    &nbsp;<asp:DropDownList ID="DropDownList1" runat="server" Font-Size="15pt" ToolTip="选择查询条件" Width="190px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" BackColor="White" Height="28pt">
                        <asp:ListItem Value="TicketNumber" Selected="True">单号</asp:ListItem>
                        <asp:ListItem Value="NTID">送修人</asp:ListItem>
                        <asp:ListItem Value="DeviceName">设备名称</asp:ListItem>
                        <asp:ListItem Value="MACAddress">物理地址</asp:ListItem>
                    </asp:DropDownList>

                    &nbsp;
                        
                &nbsp;&nbsp;
                        
                <asp:TextBox ID="txtSearch" runat="server" Height="28pt" Width="276px" Font-Size="15pt" ></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSearch" runat="server" Font-Size="15px" Height="28pt" Text="查询" Width="83px" OnClick="btnSearch_Click" />
                </p>
            </div>
            <fieldset class="layui-elem-field layui-field-title" style="margin-top: 20px;">
                <legend>信息维护表</legend>
            </fieldset>

            <asp:GridView ID="GridView1" CssClass="GridView_colume" AllowPaging="True" AllowSorting="True" runat="server" CellPadding="0" PageSize="15" ForeColor="#333333" GridLines="Vertical" OnPageIndexChanging="GridView1_PageIndexChanging" OnSorting="GridView1_Sorting" DataKeyNames="id" EmptyDataText="NULL" AutoGenerateColumns="False" Width="98%" HorizontalAlign="Center" AutoGenerateSelectButton="False" OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowCommand="GridView1_RowCommand" OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowUpdating="GridView1_RowUpdating" OnRowDeleting="GridView1_RowDeleting">
                    <AlternatingRowStyle BackColor="White" />
                    <EmptyDataRowStyle BackColor="Red" />
                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" Wrap="true" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" Wrap="true" />
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" HorizontalAlign="Center" Wrap="true" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    <SortedAscendingCellStyle BackColor="#FDF5AC" />
                    <SortedAscendingHeaderStyle BackColor="#4D0000" />
                    <SortedDescendingCellStyle BackColor="#FCF6C0" />
                    <SortedDescendingHeaderStyle BackColor="#820000" />
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="ID" ReadOnly="true" ItemStyle-Wrap="true" SortExpression="GridView1_Sorting">
                                                       <%-- <HeaderStyle Font-Size="14pt" />--%>
<ItemStyle Wrap="True"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="TicketNumber" HeaderText="单号" ReadOnly="True" ItemStyle-Wrap="true">

                        </asp:BoundField>
                        <asp:BoundField DataField="NTID" HeaderText="送修人">
                        </asp:BoundField>
                        <asp:BoundField DataField="TelphoneNumber" HeaderText="电话" ItemStyle-Wrap="true">

<ItemStyle Wrap="True"></ItemStyle>
                        </asp:BoundField>
                         <asp:BoundField DataField="DeviceType" HeaderText="设备类型" ItemStyle-Wrap="true">

<ItemStyle Wrap="True"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="DeviceName" HeaderText="设备名称" ItemStyle-Wrap="true">

<ItemStyle Wrap="True"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="SerialNumber" HeaderText="序列号" ItemStyle-Wrap="true">

                        </asp:BoundField>
                        <asp:BoundField DataField="MACAddress" HeaderText="物理地址" ItemStyle-Wrap="true">

                        </asp:BoundField>
                         <asp:BoundField DataField="Steps" HeaderText="进度" ReadOnly="true" ItemStyle-Wrap="true">
<ItemStyle Wrap="True"></ItemStyle>
                        </asp:BoundField>

                       <%-- <asp:CommandField ShowEditButton="true" HeaderText="更新" ButtonType="Button" />
                         <asp:CommandField ShowDeleteButton="true" HeaderText="删除" ButtonType="Button" />--%>
                         <%--<asp:TemplateField HeaderText="更新" ShowHeader="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnUpdate" runat="server" CausesValidation="False" CommandName="Update" OnClick="btnUpdate_Click" Text="更新" CommandArgument='<%# Bind("id")%>' >

                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:CommandField HeaderText="编辑" CancelText="取消" EditText="编辑" ShowEditButton="True" UpdateText="更新" />
                        <asp:TemplateField HeaderText="删除" ShowHeader="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="False" CommandName="Delete" Text="删除" OnClientClick="return confirm('您确认删除该记录吗?');" CommandArgument='<%# Bind("id")%>' >

                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                <PagerTemplate>
                    当前第:
                    <asp:Label ID="LabelCurrentPage" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageIndex + 1 %>"></asp:Label>
                    页/共
                    <asp:Label ID="LabelPageCount" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageCount %>"></asp:Label>
                    页，每页
                    <asp:Label ID="Pagenumber" runat="server" Text="<%#((GridView)Container.NamingContainer).PageSize %>"></asp:Label>
                    条
                     <asp:Button ID="ButtonFirstPage" runat="server" Text="首页" CommandArgument="First" CommandName="Page" Visible='<%#((GridView)Container.NamingContainer).PageIndex != 0 %>' />                
                    <asp:Button ID="ButtonPreviousPage" runat="server" Text="上一页" CommandArgument="Prev" CommandName="Page" Visible='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' />
                    <asp:Button ID="LinkButtonNextPage" runat="server" Text="下一页" CommandArgument="Next" CommandName="Page" Visible='<%# ((GridView)Container.NamingContainer).PageIndex != ((GridView)Container.NamingContainer).PageCount - 1 %>' />
                    <asp:Button ID="LinkButtonLastPage" runat="server" Text="尾页" CommandArgument="Last" CommandName="Page" Visible='<%# ((GridView)Container.NamingContainer).PageIndex != ((GridView)Container.NamingContainer).PageCount - 1 %>' />                   
                    转到第
                    <asp:TextBox ID="txtNewPageIndex" runat="server" Width="20px" Text='<%# ((GridView)Container.Parent.Parent).PageIndex + 1 %>'/>
                    页
                    <asp:Button ID="btnGo" runat="server" CausesValidation="False" CommandArgument="-2" CommandName="Page" Text="跳转"/>
                </PagerTemplate>
                </asp:GridView>



        </div>
    </form>
</body>

</html>
