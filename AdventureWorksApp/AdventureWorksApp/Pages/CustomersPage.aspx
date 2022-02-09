<%@ Page Title="Customers" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="CustomersPage.aspx.cs" Inherits="AdventureWorksApp.Pages.CustomersPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
	<link href="../Content/bootstrap.css" rel="stylesheet" />
	<link href="../Css/CustomerPage.css" rel="stylesheet" />
	<div class="container-fluid">
			<div class="row">
				<div>
					<asp:GridView ID="gvCustomers" runat="server" AllowSorting="true" OnSorting="gvCustomers_Sorting" OnPageIndexChanging="gvCustomers_PageIndexChanging" CssClass="table table-hover" BackColor="#DCDCDC" AutoGenerateColumns="False" GridLines="Horizontal" AllowPaging="True"  BorderWidth="1px" CellPadding="3"  BorderColor="Black">
						<Columns>
							<asp:BoundField DataField ="IDKupac" HeaderText="ID"/>
							<asp:BoundField DataField ="Ime" HeaderText="Firstname" SortExpression="Ime"/>
							<asp:BoundField DataField ="Prezime" HeaderText="Surname" SortExpression="Prezime"/>
							<asp:BoundField DataField ="Email" HeaderText="Email"/>
							<asp:BoundField DataField ="Telefon" HeaderText="Phone"/>
							<asp:BoundField DataField ="Grad" HeaderText="City"/>
							<asp:TemplateField HeaderStyle-CssClass="select-col">
								<ItemTemplate>
                                    <asp:LinkButton ID="btnViewHistory" Text="View history" Width="110px" OnCommand="Unnamed_Command" BackColor="#006699" CssClass="btn" CommandArgument= '<%# Eval("IDKupac") %>' runat="server" />
								</ItemTemplate>
							</asp:TemplateField>
						</Columns>
					</asp:GridView>
				</div>
				<div class="container-fluid">
					<div class="row align-content-center">
						<asp:Label ID="lbInfo" runat="server" Text=""></asp:Label>
					</div>
				</div>
				<div class="container-fluid">
					<div class="row align-content-center">
						<p></p>
						<label class="filer_label"><b>Filter users by country and city</b></label>
						<asp:DropDownList ID="ddlCountry" style="margin-left:10px" CssClass="btn btn-outline-light" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" Width="209px"></asp:DropDownList>
						<asp:DropDownList ID="ddlCity" style="margin-left:10px" runat="server" CssClass="btn btn-outline-light" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" Width="209px"></asp:DropDownList>
						<label><b>How many customers per page?</b></label>
						<div style="margin:4px">
						<asp:Button ID="btn10" runat="server" Width="50px" Text="10" OnClick="btn10_Click"/>
						<asp:Button ID="btn20" runat="server" Width="50px" Text="20" OnClick="btn20_Click"/>
						<asp:Button ID="btn30" runat="server" Width="50px" Text="30" OnClick="btn30_Click"/>
						<asp:Button ID="btn40" runat="server" Width="50px" Text="40" OnClick="btn40_Click"/>
						<asp:Button ID="btn50" runat="server" Width="50px" Text="50" OnClick="btn50_Click"/>
                            <asp:Button ID="btnShowAllCustomers" runat="server" OnClick="btnShowAllCustomers_Click" Text="Show all customers" CssClass="btn btn-dark" />
						</div>
					</div>
				</div>
			</div>
		</div>
</asp:Content>
