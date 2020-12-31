<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="UC" TagName="BatchType" Src="../OrderMgt/UC/BatchType.ascx" %>
<%@ Register TagPrefix="UC" TagName="OrderQualifier" Src="../OrderMgt/UC/OrderQualifier.ascx" %>
<%@ Page language="c#" Codebehind="OrderReturns.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.OrderMgt.OrderReturns" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Order Returns</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</head>
	<body leftMargin="0" topMargin="0">
		<form id="Form1" method="post" runat="server">
			<!--#include file="../Includes/Menu.inc"-->
			<!--#include file="../CustomerService/fctjavascriptall.js"--><br>
			<table border="1">
				<tr>
					<td colspan="4">
						<asp:Label runat="server" id="Label3" Font-Bold="True" ForeColor="#2F4F88" CssClass="font9boldv" Font-Size="X-Small" Font-Names="verdana">GIFT ORDER RETURNS</asp:Label>
					</td>
				</tr>
				<tr>
					<td><asp:Label runat="server" id="Label1" Font-Size="X-Small" Font-Names="Verdana">Order Type</asp:Label></td>
					<td>
						<UC:BatchType id="ucBatchType" AllTypesOption="true" runat="server" CssClass="boxlookW" />
					</td>
					<td><asp:Label runat="server" id="Label2" Font-Size="X-Small" Font-Names="verdana">Order Qualifier</asp:Label></td>
					<td>
						<UC:OrderQualifier id="ucOrderQualifier" AllQualifiersOption="true" runat="server" CssClass="boxlookW" />
					</td>
					<td><asp:Label runat="server" id="lbltxtlblOrderId" Font-Size="X-Small" Font-Names="verdana">Order Id</asp:Label></td>
					<td><asp:Label runat="server" id="lblOrderId" ForeColor="#404040" BackColor="#E0E0E0" Width="113px"></asp:Label></td>
				</tr>
				<tr>
					<td><asp:Label runat="server" id="Label21" Font-Size="X-Small" Font-Names="Verdana">Ship To Account Id</asp:Label></td>
					<td><asp:TextBox runat="server" id="ShiptoAccountId" CssClass="TextBox2" ontextchanged="ShiptoAccountId_TextChanged" /></td>
					<td><asp:Label runat="server" id="Label20" Font-Size="X-Small" Font-Names="verdana">Campaign Id</asp:Label></td>
					<td><asp:TextBox runat="server" id="CampaignId" tabIndex="3"  Font-Size="X-Small" Font-Names="Verdana" AutoPostBack="True" ontextchanged="CampaignId_TextChanged">0</asp:TextBox></td>
					<td><asp:Label runat="server" id="lbltxtlblOrderStatus" Font-Size="X-Small" Font-Names="verdana">Order Status</asp:Label></td>
					<td><asp:Label runat="server" id="lblOrderStatus" BackColor="#E0E0E0" Width="113px">&nbsp;&nbsp;&nbsp;</asp:Label></td>
				</tr>
				<tr>
					<td><asp:Label runat="server" id="Label22" Font-Size="X-Small" Font-Names="verdana">Ship To FM Id</asp:Label></td>
					<td><asp:TextBox runat="server" id="ShiptoFMId" ontextchanged="ShiptoFMId_TextChanged" /></td>
					<td><asp:Label runat="server" id="Label5" Font-Size="X-Small" Font-Names="Verdana">Bill To Account Id</asp:Label></td>
					<td><asp:TextBox runat="server" id="BilltoAccountId" tabIndex="8" /></td>
					<td><asp:Label runat="server" id="Label19" Font-Size="X-Small" Font-Names="verdana">Invoice Id</asp:Label></td>
					<td><asp:TextBox runat="server" id="InvoiceId" tabIndex="4" Font-Size="X-Small" Font-Names="Verdana" /></td>
				</tr>
				<tr>
					<td><asp:Label runat="server" id="Label23" Font-Size="X-Small" Font-Names="verdana">Employee Id</asp:Label></td>
					<td><asp:TextBox runat="server" id="ShiptoEmpId" ontextchanged="ShiptoEmpId_TextChanged" /></td>
					<td><asp:Label runat="server" id="Label4" Font-Size="X-Small" Font-Names="verdana">Bill To FM Id</asp:Label></td>
					<td><asp:TextBox runat="server" id="BilltoFMId" /></td>
					<td><asp:Label runat="server" id="Label24" Font-Size="X-Small" Font-Names="verdana">Total Actual Order Amount</asp:Label></td>
					<td><asp:Label runat="server" id="lblTotalActualAmount" BackColor="#E0E0E0" Width="113px">&nbsp;&nbsp;&nbsp;</asp:Label></td>
				</tr>
				<tr>
					<td colspan="8">&nbsp;&nbsp;</td>
				</tr>
				<tr>
					<td colspan="3">
						<asp:Label runat="server" id="Label26" 
							Font-Bold="True" ForeColor="#2F4F88" 
							CssClass="font9boldv" Font-Size="X-Small" 
							Font-Names="verdana">SHIPPING / BILLING</asp:Label>
						<br>
						<!--Inner Table-->
						<table>
							<tr>
								<td><asp:Label runat="server" id="lbltxtShiptoName" Font-Size="X-Small" Font-Names="verdana">Ship To Name</asp:Label></td>
								<td><asp:Label runat="server" id="lblShiptoName" Font-Size="X-Small" Font-Names="Verdana" BackColor="#E0E0E0"
										Width="113px">&nbsp;&nbsp;&nbsp;</asp:Label></td>
								<td><asp:TextBox runat="server" id="ShiptoContactName2" tabIndex="10" Font-Size="X-Small" Font-Names="Verdana" /></td>
							</tr>
							<tr>
								<td><asp:Label runat="server" id="lbltxtShiptoAddress" Font-Size="X-Small" Font-Names="verdana">Address</asp:Label></td>
								<td><asp:Label runat="server" id="lblShiptoAddress" Font-Size="X-Small" Font-Names="Verdana" BackColor="#E0E0E0"
										Width="113px">&nbsp;&nbsp;&nbsp;</asp:Label></td>
								<td><asp:Label runat="server" id="lblShiptoCity" Font-Size="X-Small" Font-Names="Verdana" BackColor="#E0E0E0"
										Width="113px">&nbsp;&nbsp;&nbsp;</asp:Label></td>
							</tr>
							<tr>
								<td><asp:Label runat="server" id="lbltxtBilltoName" Font-Size="X-Small" Font-Names="verdana">Bill To Name</asp:Label></td>
								<td><asp:Label runat="server" id="lblBilltoName" Font-Size="X-Small" Font-Names="Verdana" BackColor="#E0E0E0"
										Width="113px">&nbsp;&nbsp;&nbsp;</asp:Label></td>
							</tr>
							<tr>
								<td><asp:Label runat="server" id="lbltxtBilltoAddress" Font-Size="X-Small" Font-Names="verdana">Address</asp:Label></td>
								<td><asp:Label runat="server" id="lblBilltoAddress" Font-Size="X-Small" Font-Names="Verdana" BackColor="#E0E0E0"
										Width="113px">&nbsp;&nbsp;&nbsp;</asp:Label></td>
								<td><asp:Label runat="server" id="lblBilltoCity" Font-Size="X-Small" Font-Names="Verdana" BackColor="#E0E0E0"
										Width="113px">&nbsp;&nbsp;&nbsp;</asp:Label></td>
							</tr>
						</table>
					</td>
					<td colspan="3">
						<asp:Label runat="server" id="Label38" 
							Font-Bold="True" ForeColor="#2F4F88" 
							CssClass="Font9boldv" Font-Size="X-Small" 
							Font-Names="verdana">SHIP TO CONTACT</asp:Label>
						<br>
						<!--Inner Table-->
						<table>
							<tr>
								<td><asp:Label runat="server" id="lbltxtShiptoContactName1" Font-Size="X-Small" Font-Names="verdana">Name</asp:Label></td>
								<td><asp:TextBox runat="server" id="ShiptoContactName1" tabIndex="9" Font-Size="X-Small" Font-Names="Verdana" /></td>
							</tr>
							<tr>
								<td><asp:Label runat="server" id="lbltxtShiptoContactEmail" Font-Size="X-Small" Font-Names="verdana">Email</asp:Label></td>
								<td><asp:TextBox runat="server" id="ShiptoContactEmail" tabIndex="11" /></td>
							</tr>
							<tr>
								<td><asp:Label runat="server" id="lbltxtShiptoContactPhone" Font-Size="X-Small" Font-Names="Verdana">Phone</asp:Label></td>
								<td><asp:TextBox runat="server" id="ShiptoContactPhone" tabIndex="12" /></td>
								<td><asp:Label runat="server" id="lbltxtShiptoContactFax" Font-Size="X-Small" Font-Names="verdana">Fax</asp:Label></td>
								<td><asp:TextBox runat="server" id="ShiptoContactFax" tabIndex="13" /></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td><asp:Label runat="server" id="Acc" Height="16px" Visible="False">0</asp:Label></td>
				</tr>
			</table>
			<br>
			<asp:Label runat="server" id="Label6" Font-Bold="True" ForeColor="#2F4F88" CssClass="Font9boldv"
				Font-Size="X-Small" Font-Names="verdana" Text="GIFT ITEMS" />
			<br>
			<asp:DataGrid runat="server" id="DGGiftOrderDetail" Font-Size="X-Small" Font-Names="Verdana" HorizontalAlign="Left"
				ShowFooter="True" AutoGenerateColumns="False">
				<Columns>
					<asp:TemplateColumn HeaderText="Validate Product">
						<FooterTemplate>
							<asp:LinkButton runat="server" id="ValidateButton" CommandName="ValidatePcode">GetDesc&Price</asp:LinkButton>
						</FooterTemplate>
						<EditItemTemplate>
							<asp:LinkButton runat="server" id="ValidateButtonE" Width="88px">GetDesc&Price</asp:LinkButton>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Product<br>Code">
						<ItemStyle Font-Names="Verdana" />
						<ItemTemplate>
							<asp:Label runat="server" id=Label10 Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode") %>' />
						</ItemTemplate>
						<FooterTemplate>
							<asp:TextBox runat="server" id="ProductCode" AutoPostBack="True" OnTextChanged="tb_TextChanged" Columns=10>0</asp:TextBox>
						</FooterTemplate>
						<EditItemTemplate>
							<asp:TextBox runat="server" id="TextBox2" />
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Product Description">
						<ItemTemplate>
							<asp:Label runat="server" id=Label9 Text='<%# DataBinder.Eval(Container.DataItem, "ProductName") %>' />
						</ItemTemplate>
						<FooterTemplate>
							<asp:TextBox runat="server" id="ProductDesc" Font-Names="Verdana" Font-Size="X-Small" Enabled="False" />
						</FooterTemplate>
						<EditItemTemplate>
							<asp:TextBox runat="server" id="ProductDescE" />
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="CatalogType">
						<ItemStyle Font-Names="Verdana" />
						<ItemTemplate>
							<asp:Label runat="server" id="Label12" />
						</ItemTemplate>
						<FooterTemplate>
							<asp:DropDownList runat="server" id="ddlCatalogType" DataValueField="Instance" DataTextField="Description" AutoPostBack="True" />
						</FooterTemplate>
						<EditItemTemplate>
							<asp:DropDownList runat="server" id="ddlCatalogType1" runat="server" />
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Catalog Name">
						<ItemStyle Font-Names="Verdana" />
						<ItemTemplate>
							<asp:Label runat="server" id="Label13" runat="server" />
						</ItemTemplate>
						<FooterTemplate>
							<asp:TextBox runat="server" id="CatalogName" runat="server" Enabled="False" />
						</FooterTemplate>
						<EditItemTemplate>
							<asp:TextBox runat="server" id="TextBox7" runat="server" />
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Qty">
						<ItemStyle Font-Names="Verdana" />
						<ItemTemplate>
							<asp:Label runat="server" id=Label14 Text='<%# DataBinder.Eval(Container.DataItem, "Quantity") %>' />
						</ItemTemplate>
						<FooterTemplate>
							<asp:TextBox runat="server" id="Quantity" AutoPostBack="True" Columns="10">0</asp:TextBox>
						</FooterTemplate>
						<EditItemTemplate>
							<asp:TextBox runat="server" id="QuantityE" />
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Unit Price">
						<ItemStyle Font-Names="Verdana" />
						<ItemTemplate>
							<asp:Label runat="server" id=Label15 Text='<%# DataBinder.Eval(Container.DataItem, "Price") %>' />
						</ItemTemplate>
						<FooterTemplate>
							<asp:TextBox runat="server" id="UnitPrice" AutoPostBack="True" Columns="10">0</asp:TextBox>
						</FooterTemplate>
						<EditItemTemplate>
							<asp:TextBox runat="server" id="UnitPriceE" />
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Catalog<br>Price">
						<ItemStyle Font-Names="Verdana" />
						<ItemTemplate>
							<asp:Label runat="server" id=Label25 Text='<%# DataBinder.Eval(Container.DataItem, "CatalogPrice") %>' />
						</ItemTemplate>
						<FooterTemplate>
							<asp:TextBox runat="server" id="CatalogPrice" AutoPostBack="True" Enabled="False" Columns="10" />
						</FooterTemplate>
						<EditItemTemplate>
							<asp:TextBox runat="server" id="CatalogPriceE" />
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Price<br>Override">
						<ItemStyle Font-Names="Verdana" />
						<ItemTemplate>
							<asp:Label runat="server" id=Label27 Text='<%# DataBinder.Eval(Container.DataItem, "priceOverride") %>' />
						</ItemTemplate>
						<FooterTemplate>
							<asp:DropDownList runat="server" id="ddlPriceOverride" DataValueField="instance" DataTextField="Description" AutoPostBack="True" />
						</FooterTemplate>
						<EditItemTemplate>
							<asp:DropDownList runat="server" id="DropDownList4" runat="server" Width="104px" />
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Action">
						<ItemTemplate>
							<asp:LinkButton runat="server" id="LinkButton1" CommandName="" Text="" CausesValidation="false">Edit</asp:LinkButton>
						</ItemTemplate>
						<FooterTemplate>
							<asp:LinkButton runat="server" id="LinkButton2" CommandName="InsertGiftItem">Insert</asp:LinkButton>
						</FooterTemplate>
						<EditItemTemplate>
							<asp:LinkButton runat="server" id="LinkButton3">Update</asp:LinkButton>
						</EditItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>
			<br><br><br><br>
			<div></div>
			<asp:Label runat="server" id="lblMessage" Width="344px" ForeColor="Red" />
			<br><asp:Label runat="server" id="coh" Width="67px" Enabled="False">Coh</asp:Label>
			<!--#include file="../CustomerService/errorwindow.js"-->
			<asp:validationsummary id="ValidationSummary1" runat="server" Font-Bold="True" ShowMessageBox="True" ShowSummary="False" />
		</form>
	</body>
</html>
