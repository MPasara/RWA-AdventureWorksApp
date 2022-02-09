<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="AdventureWorksApp.Pages.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Css/Login.css" rel="stylesheet" />
    <title>Login</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-12 ">
                    <div class="form-control custom-group">
                        <asp:Label ID="lbUsername" runat="server" Text="Username:"></asp:Label>
                        <asp:TextBox ID="tbUsername" runat="server"  CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-control custom-group">
                        <asp:Label ID="lbPassword" runat="server" Text="Password:"></asp:Label>
                        <asp:TextBox ID="tbPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-12">
                        <asp:Button ID="btnLogin" OnClick="btnLogin_Click" runat="server" Text="Login" CssClass="btn custom-btn mx-auto d-block"/>
                        <asp:Button ID="btnRegister" runat="server" OnClick="btnRegister_Click" Text="Register" CssClass="btn custom-btn mx-auto d-block"/>
                    </div>
                    <asp:Label ID="lbInfo" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
