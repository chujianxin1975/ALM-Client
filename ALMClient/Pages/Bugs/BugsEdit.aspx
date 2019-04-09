<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="BugsEdit.aspx.cs" Inherits="ALMClient.BugsEdit" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm" runat="server" />
        <f:Form ID="SimpleForm" ShowBorder="false" ShowHeader="false"
            AutoScroll="true" BodyPadding="10px" runat="server">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:Button ID="btnClose" EnablePostBack="false" Text="关闭" runat="server" Icon="SystemClose">
                        </f:Button>
                        <f:Button ID="btnSaveRefresh" Text="保存并刷新列表" runat="server" Icon="SystemSave"
                            OnClick="btnSaveRefresh_Click">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:Label ID="lblID" Label="ID" CssClass="highlight" runat="server" />
                        <f:TextBox ID="lblName" Label="Name" runat="server" />
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:DropDownList ID="ddStatus" Label="Status" runat="server" />
                        <f:DropDownList ID="ddPriority" Label="Priority" runat="server" />
                        <f:DatePicker ID="dCreationTime" Required="True" ShowRedStar="true" runat="server" Label="CreationTime">
                        </f:DatePicker>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                       <%-- <f:HtmlEditor ID="hDescription" Label="详细描述" Height="150px" runat="server">
                        </f:HtmlEditor>--%>
                        <f:TextArea ID="hDescription" Label="详细描述" Height="150px" runat="server"></f:TextArea>
                         
                    </Items>
                </f:FormRow>
            </Rows>
        </f:Form>
    </form>
</body>
</html>
