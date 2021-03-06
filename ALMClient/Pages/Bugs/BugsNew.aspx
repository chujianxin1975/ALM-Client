﻿<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="BugsNew.aspx.cs" Inherits="ALMClient.BugsNew" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false"
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
                         
                        <f:TextBox ID="txtName" Label="Name" runat="server" />
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                           <f:DropDownList ID="ddSeverity" Label="Severity" runat="server" />
                        <f:DropDownList ID="ddStatus" Label="Status" runat="server" />
                        <f:DropDownList ID="ddPriority" Label="Priority" runat="server" />
                        <f:DatePicker ID="dCreationTime" Required="True" ShowRedStar="true" runat="server" Label="CreationTime">
                        </f:DatePicker>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                <%--        <f:HtmlEditor ID="hDescription" Label="详细描述"  runat="server" >
                        </f:HtmlEditor>--%>
                     <%--    <f:HtmlEditor runat="server" Label="文本编辑器" ID="hDescription" Editor="UMEditor" BasePath="~/res/third-party/umeditor/" Height="250px">
                </f:HtmlEditor>--%>

         <%--           <f:HtmlEditor runat="server" Label="详细描述" ID="hDescription" Readonly="true"
                    Editor="CKEditor"  ToolbarSet="Full" Height="350px">
                </f:HtmlEditor>--%>
                         <f:HtmlEditor ID="hDescription" Editor="UMEditor" BasePath="~/res/third-party/umeditor/"
                            Label="详细描述" Height="300px" runat="server">
                        </f:HtmlEditor>

                    </Items>
                 </f:FormRow>
            </Rows> 
        </f:Form>
    </form>
</body>
</html>
