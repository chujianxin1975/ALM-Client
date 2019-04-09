<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs"
    Inherits="ALMClient.login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       <f:PageManager ID="PageManager1" runat="server" />
       
        <br />
        <f:Window ID="Window1" runat="server" Title="登录表单" IsModal="false" EnableClose="false"
            WindowPosition="GoldenSection" Width="350px">
            <Items>
                <f:SimpleForm ID="frmUser" runat="server" ShowBorder="false" BodyPadding="10px"
                    LabelWidth="60px" ShowHeader="false">
                    <Items>
                         <f:TextBox ID="txtUrl" Label="URL" Required="true" runat="server" Text="http://192.168.11.152:8080">
                        </f:TextBox>
                        <f:TextBox ID="txtUser" Label="用户名" Required="true" runat="server" Text="tester">
                        </f:TextBox>
                        <f:TextBox ID="txtPass" Label="密码" TextMode="Password" runat="server">
                        </f:TextBox>
                        <f:Button ID="btnAuth" Text="验证身份" Type="Submit" ValidateForms="frmUser" ValidateTarget="Top" Icon="Key" ColumnWidth="1"
                            runat="server" OnClick="btnAuth_Click">
                        </f:Button>                     
                    </Items>
                </f:SimpleForm>
                 <f:SimpleForm ID="frmDomain" runat="server" ShowBorder="false" BodyPadding="10px"
                    LabelWidth="60px" ShowHeader="false" Hidden="true">
                    <Items>
                        <f:DropDownList ID="ddDomain" Label="Domain" Required="true" runat="server" OnSelectedIndexChanged="ddDomain_SelectedIndexChanged" AutoPostBack="true" AutoSelectFirstItem="true">
                        </f:DropDownList>
                        <f:DropDownList ID="ddProject" Label="Project" Required="true" runat="server">
                        </f:DropDownList>  
                         <f:Button ID="btnLogin" Text="登录" Type="Submit" ValidateForms="frmDomain" ValidateTarget="Top" Icon="Key"
                            runat="server" OnClick="btnLogin_Click">
                        </f:Button>              
                    </Items>
                </f:SimpleForm>
            </Items>
            <%--<Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server" Position="Bottom" ToolbarAlign="Right">
                    <Items>
                        <f:Button ID="btnLogin" Text="登录" Type="Submit" ValidateForms="SimpleForm1" ValidateTarget="Top"
                            runat="server" OnClick="btnLogin_Click">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>--%>
        </f:Window>
        
    </form>
</body>
</html>
