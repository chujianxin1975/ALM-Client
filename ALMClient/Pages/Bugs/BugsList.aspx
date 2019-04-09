<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="BugsList.aspx.cs" Inherits="ALMClient.BugsList" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" />
        <f:Grid ID="Grid1" Title="表格" EnableCollapse="false" AllowSorting="true" SortField="Name" SortDirection="ASC"
            PageSize="20" ShowBorder="true" ShowHeader="true" AllowPaging="true"
            runat="server" EnableCheckBoxSelect="True" DataKeyNames="ID,Name"
            OnPageIndexChange="Grid1_PageIndexChange" OnSort="Grid1_Sort" OnRowCommand="Grid1_RowCommand">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>

                        <f:Button ID="btnNew" runat="server" Icon="Add" EnablePostBack="false" Text="Add New Bug">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Columns> 
                <f:BoundField Width="40px" SortField="ID" DataField="ID" HeaderText="ID" />
                <f:BoundField Width="320px" SortField="Name" DataField="Name" HeaderText="Name" />
                <f:BoundField Width="180px" DataField="CreationTime" ID="CreationTime" SortField="CreationTime" HeaderText="Creation Time" />
                <f:BoundField Width="120px" DataField="Priority" ID="Priority" SortField="Priority" HeaderText="Priority" />
                <f:BoundField Width="120px" DataField="Status" ID="Status" SortField="Status" HeaderText="Status" />
                <f:BoundField Width="120px" DataField="Description" ID="Description" SortField="Description" HeaderText="Description" />
                <f:WindowField TextAlign="Center" Width="80px" WindowID="Window1" Icon="Pencil"
                    ToolTip="编辑" DataIFrameUrlFields="Id,Name" DataIFrameUrlFormatString="~/Pages/Bugs/BugsEdit.aspx?id={0}"
                    Title="编辑" IFrameUrl="~/alert.aspx" />
                <f:LinkButtonField ColumnID="deleteField" TextAlign="Center" Icon="Delete" ToolTip="删除"
                    ConfirmText="确定删除此记录？" ConfirmTarget="Top" CommandName="Delete" Width="50px" />
            </Columns>
        </f:Grid>

        <f:Label ID="labResult" EncodeText="false" runat="server">
        </f:Label>
        <br />
        <br />
        <f:Window ID="Window1" Title="弹出窗体" Hidden="true" EnableIFrame="true"
            EnableMaximize="true" Target="Top" EnableResize="true" runat="server" OnClose="Window1_Close"
            IsModal="true" Width="850px" Height="550px">
        </f:Window>
    </form>
</body>
</html>
