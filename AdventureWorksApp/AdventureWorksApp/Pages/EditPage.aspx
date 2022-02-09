<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="EditPage.aspx.cs" Inherits="AdventureWorksApp.Pages.EditPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Css/EditPage.css" rel="stylesheet" />
    <div class="container-fluid">
            <div class="row">
                <div>
                    <div>
                        <div >
                            <asp:GridView ID="gvCustomers" runat="server" OnPageIndexChanging="gvCustomers_PageIndexChanging" CssClass="table table-hover" BackColor="#DCDCDC" AutoGenerateColumns="False" AllowPaging="True"  BorderWidth="1px" CellPadding="3"  BorderColor="Black">
                        <Columns>
                            <asp:BoundField DataField ="IDKupac" HeaderText="ID"/>
                            <asp:BoundField DataField ="Ime" HeaderText="Firstname"/>
                            <asp:BoundField DataField ="Prezime" HeaderText="Surname"/>
                            <asp:BoundField DataField ="Email" HeaderText="Email"/>
                            <asp:BoundField DataField ="Telefon" HeaderText="Phone"/>
                            <asp:BoundField DataField ="Grad" HeaderText="City"/>
                            <asp:TemplateField HeaderStyle-CssClass="select-col">
                                <ItemTemplate>
                                    <asp:Button ID="btnEditCustomer" runat="server" Width="50px" Text="Edit" OnClick="btnEditCustomer_Click" ForeColor="White" CssClass="btn" BackColor="#006699"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                            
                            <span class="buttons"> 
                        <asp:Button ID="btn10" runat="server" Width="50px" Text="10" CommandArgument="10" OnClick="btn10_Click" CssClass="btn btn-info"/>
                        <asp:Button ID="btn20" runat="server" Width="50px" Text="20" CommandArgument="20" OnClick="btn20_Click" CssClass="btn btn-info"/>
                        <asp:Button ID="btn30" runat="server" Width="50px" Text="30" CommandArgument="30" OnClick="btn30_Click" CssClass="btn btn-info"/>
                        <asp:Button ID="btn40" runat="server" Width="50px" Text="40" CommandArgument="40" OnClick="btn40_Click" CssClass="btn btn-info"/>
                        <asp:Button ID="btn50" runat="server" Width="50px" Text="50" CommandArgument="50" OnClick="btn50_Click" CssClass="btn btn-info"/>
                            </span>
                            
                        </div>
                    </div>
                </div>
                <div class="container">
                    <div class="row">
                        <div class="col-12">
                            <div class="form-control custom group">
                                <label style="margin-right:11px">Firstname: </label>
                                <asp:TextBox ID="tbFirstname" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-control custom group">
                                <label style="margin-right:17px">Surname: </label>
                                <asp:TextBox ID="tbSurname" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-control custom group">
                                <label style="margin-right:42px">Email: </label>
                                <asp:TextBox ID="tbEmail" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-control custom group">
                                 <label style="margin-right:12px">Phone no: </label>
                                 <asp:TextBox ID="tbPhoneNumber" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-control custom group">
                                <label style="margin-right:52px">City: </label>
                                <asp:DropDownList runat="server" AppendDataBoundItems="true" CssClass="btn btn-warning" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" ID="ddlCity"  Width="209px"/>
                            </div>
                            <div class="form-control custom group">
                                <label style="margin-right:22px">Country: </label>
                                <asp:DropDownList runat="server" AppendDataBoundItems="true" ID="ddlCountry" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" CssClass="btn btn-warning" Width="209px"/>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="buttons">
                        <asp:Button Text="Update" OnClick="btnUpdate_Click" ID="btnUpdate" runat="server" Width="90" CssClass="btn btn-info" />
                        <asp:Button Text="Delete"  ID="btnDelete" OnClick="btnDelete_Click" runat="server" Width="90" CssClass="btn btn-danger" />
                        <asp:Button Text="Clear selection" ID="btnClearForm" OnClick="btnClearForm_Click" runat="server" Width="120" CssClass="btn btn-warning" />
                    </div>
                    <div>
                        <asp:Label ID="lbInfo" runat="server" Text=""></asp:Label>
                    </div>
            </div>
        </div>
</asp:Content>
