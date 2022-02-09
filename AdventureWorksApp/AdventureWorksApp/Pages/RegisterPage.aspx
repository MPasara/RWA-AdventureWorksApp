<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterPage.aspx.cs" Inherits="AdventureWorksApp.Pages.RegisterPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Css/Register.css" rel="stylesheet" />
    <title>Register</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-12 ">
                    <div class="form-control custom-group">
                        <asp:Label ID="lbFirstname" runat="server" Text="Firstname:"></asp:Label>
                        <asp:RequiredFieldValidator ID="rfvFirstname" runat="server" ControlToValidate="tbFirstname" ForeColor="Red" ErrorMessage="Firstname is a must" Text="*"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="tbFirstname" runat="server"  CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-control custom-group">
                        <asp:Label ID="lbSurname" runat="server" Text="Surname:"></asp:Label>
                        <asp:RequiredFieldValidator ID="rfvSurname" runat="server" ControlToValidate="tbSurname" ForeColor="Red" ErrorMessage="Surname is a must" Text="*"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="tbSurname" runat="server"  CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-control custom-group">
                        <asp:Label ID="lbUsername" runat="server" Text="Username:"></asp:Label>
                        <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="tbUsername" ForeColor="Red" ErrorMessage="Username is a must" Text="*"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="tbUsername" runat="server"  CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-control custom-group">
                        <asp:Label ID="lbPassword" runat="server" Text="Password:"></asp:Label>
                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="tbPassword" ForeColor="Red" ErrorMessage="Password is a must" Text="*"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="tbPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-control custom-group">
                        <asp:Label ID="Label1" runat="server" Text="Repeat password:"></asp:Label>
                        <asp:RequiredFieldValidator ID="rfvRepeatPassword" runat="server" ControlToValidate="tbRepeatPassword" ForeColor="Red" ErrorMessage="Password must match" Text="*"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="tbRepeatPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-12">
                        <asp:Button ID="btnRegister" runat="server" OnClick="btnRegister_Click" Text="Register" CssClass="btn custom-btn mx-auto d-block"/>
                    </div>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red"/>
                    <asp:Label ID="lbInfo" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
