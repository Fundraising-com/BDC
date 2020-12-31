<%@ Page language="c#" Codebehind="CampaignsForLinks.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.AcctMgt.CampaignsForLinks" %>
<%@ Register TagPrefix="UC" TagName="DynamicList" Src="../Common/DynamicList.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
	<title>QSP Canada - Campaigns For Links</title>
	<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
	<meta name="CODE_LANGUAGE" Content="C#">
	<meta name="vs_defaultClientScript" content="JavaScript">
	<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	<style type="text/css">
		A { COLOR: blue; TEXT-DECORATION: none }
		UL { LIST-STYLE-POSITION: inside; MARGIN-BOTTOM: 0px; MARGIN-LEFT: 0px; LIST-STYLE-TYPE: circle }
	</style>
	<link rel="stylesheet" href="../Includes/QSPFulfillment.css" type="text/css">
	<link rel="stylesheet" href="../AcctMgt/AcctMgt.css" type="text/css">
</head>
<body leftMargin="0" topMargin="0" marginwidth="0" marginheight="0">
	<form id="Form1" method="post" runat="server">
		<!--#include file="../Includes/Menu.inc"-->
		<!--#include file="../CustomerService/fctjavascriptall.js"-->

		<h3>QSP Canada Campaign Lookup</h3>
		<div class="CSDirections">
			Enter a Group/Account ID number
			<br>and click submit to see a list of campaigns.
			<br>
			<br>
		</div>
		<table border="0" cellspacing="2" cellpadding="2">
			<tr>
				<td align="right">
					Group ID:
				</td>
				<td align="left">
					<asp:TextBox runat="server" ID="tbAccountID" Columns="10" MaxLength="10" TextMode="SingleLine" />
				</td>
				<td align="left">
					<asp:Button runat="server" ID="btSubmit" Text="Submit" CssClass="fields2" />
				</td>
			</tr>
			<tr>
				<td colspan="3">
					<asp:RequiredFieldValidator Runat="server" ID="rqAccountID" ErrorMessage="Please enter a Group/Account ID" ControlToValidate="tbAccountID" />
				</td>
			</tr>
			<tr>
				<td colspan="3">
					<asp:RegularExpressionValidator Runat="server" ID="regAccountID" ErrorMessage="Please enter numbers only for the Group/Account ID"
						ControlToValidate="tbAccountID" ValidationExpression="\d*" />
				</td>
			</tr>
		</table>
		<hr>
		<asp:DataGrid ID="DataGridCampaigns" Runat="server" AllowPaging="False" AllowSorting="False" 
		AutoGenerateColumns="False">
			<ItemStyle ForeColor="#000066" cssClass="CSPlainText"></ItemStyle>
			<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#006699" cssClass="CSPlainText" VerticalAlign="Middle"
				HorizontalAlign="Center"></HeaderStyle>
			<Columns>
				<asp:TemplateColumn HeaderText="CampaignID">
					<ItemTemplate>
						<asp:Label runat="server" id="lbCampaignID" EnableViewState="False" Text='<%# DataBinder.Eval(Container.DataItem,"CampaignID")%>' />
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="ValidYear">
					<ItemTemplate>
						<asp:Label runat="server" id="lbValidYear" EnableViewState="False" Text='<%# DataBinder.Eval(Container.DataItem,"ValidYear")%>' />
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Dates" ItemStyle-Wrap="False">
					<ItemTemplate>
						<asp:Placeholder runat="server">
							<table border="0" cellspacing="0" cellpading="0">
								<tr>
									<td align="right">
										<asp:Label ForeColor="#000066" CssClass="CSPlainText" runat="server" ID="Label2" NAME="lbStartDtTitle">Start:</asp:Label>
									</td>
									<td align="left">
										<asp:Label ForeColor="#000066" CssClass="CSPlainText" runat="server" EnableViewState="False" Text='<%# DataBinder.Eval(Container.DataItem,"StartDate")%>' ID="lbStartDt" />
									</td>
								</tr>
								<tr>
									<td align="right" nowrap="nowrap">
										<asp:Label ForeColor="#000066" CssClass="CSPlainText" runat="server" ID="Label3" NAME="lbEndDtTitle">End:</asp:Label>
									</td>
									<td align="left">
										<asp:Label ForeColor="#000066" CssClass="CSPlainText" runat="server" EnableViewState="False" Text='<%# DataBinder.Eval(Container.DataItem,"EndDate")%>' ID="lbEndDt" />
									</td>
								</tr>
							</table>
						</asp:Placeholder>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Groups" ItemStyle-Wrap="False">
					<ItemTemplate>
						<asp:Placeholder runat="server">
							<table border="0" cellspacing="0" cellpading="0">
								<tr>
									<td align="right">
										<asp:Label ForeColor="#000066" CssClass="CSPlainText" runat="server" ID="lbShipToTitle">Ship To:</asp:Label>
									</td>
									<td align="left">
										<asp:Label ForeColor="#000066" CssClass="CSPlainText" runat="server" id="lbShipTo" EnableViewState="False" Text='<%# DataBinder.Eval(Container.DataItem,"ShipAID")%>' />	
									</td>
								</tr>
								<tr>
									<td align="right">
										<asp:Label ForeColor="#000066" CssClass="CSPlainText" runat="server" ID="lbBillToTitle">Bill To:</asp:Label>
									</td>
									<td align="left">
										<asp:Label ForeColor="#000066" CssClass="CSPlainText" runat="server" id="lbBillTo" EnableViewState="False" Text='<%# DataBinder.Eval(Container.DataItem,"BillAID")%>' />
									</td>
								</tr>
							</table>
						</asp:Placeholder>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="FMID">
					<ItemTemplate>
						<asp:Label runat="server" id="lbFMID" EnableViewState="False" Text='<%# DataBinder.Eval(Container.DataItem,"FMID")%>' />
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Status">
					<ItemTemplate>
						<asp:Label runat="server" id="lbStatus" EnableViewState="False" Text='<%# DataBinder.Eval(Container.DataItem,"Status")%>' />
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Lang">
					<ItemTemplate>
						<asp:Label runat="server" id="lbLang" EnableViewState="False" Text='<%# DataBinder.Eval(Container.DataItem,"Lang")%>' />
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Programs" ItemStyle-Wrap="False">
					<ItemTemplate>
						<UC:DynamicList ID="ucDynListPrograms" Runat="server" DataString='<%# DataBinder.Eval(Container.DataItem, "Programs") %>' />
					</ItemTemplate>
				</asp:TemplateColumn>
			</Columns>
		</asp:DataGrid>
	</form>
</body>
</html>
