<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ProductGeneralControl.ascx.vb" Inherits="StoreFront.StoreFront.ProductGeneralControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<input id="ProdUID" type="hidden" name="ProdUID" runat="server"><input id="hidMinPrice" runat="server" type="hidden">
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
	<TBODY>
		<tr>
			<TD class="Content" width="1" colSpan="6">&nbsp;</TD>
		</tr>
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="ContentTableHeader" noWrap align="left" colSpan="4">&nbsp;&nbsp;<%# GetHeader() %>&nbsp;
			</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right">Activate Product:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left" colSpan="3"><asp:checkbox id="ProductIsActive" runat="server"></asp:checkbox></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<!-- Verisign Recurring Billing -->
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right">Subscription Product:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left" colSpan="3"><asp:checkbox id="SubscriptionProduct" AutoPostBack="True" Runat="server"></asp:checkbox></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<!-- Verisign Recurring Billing -->
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right">Product Code:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left" colSpan="3"><asp:textbox id="ProdCode" runat="server" MaxLength="50"></asp:textbox></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right">Product Name:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left"><asp:textbox id="ProdName" runat="server" MaxLength="255"></asp:textbox></td>
			<td class="content" align="right">Plural Name:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left"><asp:textbox id="ProdNamePlural" runat="server" MaxLength="255"></asp:textbox></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR id="trNormalPrice" runat="server" visible="false">
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right">Price:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left"><asp:textbox id="ProdPrice" runat="server"></asp:textbox></td>
			<td class="content" align="right">Cost:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left"><asp:textbox id="ProdCost" runat="server"></asp:textbox></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<TR id="trMerchantPrice" runat="server" visible="false">
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right">Bundle Component Total:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left"><asp:label id="lblMerchantPrice" Runat="server"></asp:label></td>
			<td class="content" align="right">Bundle Cost:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left"><asp:label id="lblMerchantCost" Runat="server"></asp:label></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<TR id="trCustomPrice" runat="server" visible="false">
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right">Bundle Component Price Range:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left"><asp:label id="lblCustomPrice" Runat="server"></asp:label></td>
			<td class="content" align="right">Bundle Component Cost Range:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left"><asp:label id="lblCustomCost" Runat="server"></asp:label></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right">Manufacturer:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left" colSpan="3"><asp:dropdownlist id="Manufacturers" runat="server"></asp:dropdownlist></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right">Vendor:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left" colSpan="3"><asp:dropdownlist id="Vendors" runat="server"></asp:dropdownlist></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<!-- Tee 7/31/2007 product configurator -->
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" height="25" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<tr id="trPriceSetting" runat="server" visible="false">
			<td class="Content" width="100%" colSpan="6">
				<TABLE id="tblPriceSetting" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<td class="content" align="left" width="30%"><asp:RadioButton ID="rbPrice" Text="Set price for bundle at:&nbsp;&nbsp;&nbsp;&nbsp;" Checked="True"
								GroupName="rbgPrice" Runat="server"></asp:RadioButton></td>
						<td class="content" align="left" colspan="3"><asp:TextBox ID="txtPrice" Runat="server" Width="75"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;or...&nbsp;&nbsp;&nbsp;&nbsp;</td>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</TR>
					<tr>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</tr>
					<TR>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<td class="Content" align="left"><asp:RadioButton ID="rbAutoPrice" Text="Price is a sum of the components:&nbsp;&nbsp;&nbsp;&nbsp;"
								groupname="rbgPrice" Runat="server"></asp:RadioButton></td>
						<td class="content" align="left" colspan="3">
							<asp:RadioButton ID="rbPlus" Text="Plus" Checked="true" groupname="rbgPlusMinus" Runat="server"></asp:RadioButton>
							&nbsp;&nbsp;or&nbsp;&nbsp;
							<asp:RadioButton ID="rbMinus" Text="Minus" GroupName="rbgPlusMinus" Runat="server"></asp:RadioButton>
							&nbsp;&nbsp;Amount&nbsp;&nbsp;
							<asp:TextBox ID="txtAmount" Runat="server" Width="75"></asp:TextBox>
							&nbsp;&nbsp;
							<asp:RadioButton ID="rbDollar" Text="Dollar" Checked="True" GroupName="rbgDollarPercent" Runat="server"></asp:RadioButton>
							&nbsp;&nbsp;
							<asp:RadioButton ID="rbPercent" Text="Percent" GroupName="rbgDollarPercent" Runat="server"></asp:RadioButton></td>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</TR>
					<tr>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</tr>
					<TR>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<td class="content" align="right" colspan="3">
						</td>
						<td class="Content" align="left">&nbsp;</td>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</TR>
					<tr>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</tr>
				</TABLE>
			</td>
		</tr>
		<!-- end Tee -->
		<TR>
			<TD class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<tr>
			<TD class="Content" width="1" colSpan="6">&nbsp;</TD>
		</tr>
	</TBODY></TABLE>
<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" align="left" border="0"
	runat="server">
	<tr>
		<td class="Content" vAlign="top" width="35%">
			<table id="taxTable" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
				<TBODY>
					<tr>
						<TD class="Content" width="1" colSpan="6">&nbsp;</TD>
					</tr>
					<TR>
						<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="ContentTableHeader" noWrap align="left" colSpan="4">&nbsp;&nbsp;Tax 
							Options&nbsp;
						</TD>
						<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</TR>
					<tr>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</tr>
					<TR>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<td class="content" noWrap align="right" width="180">Apply Local 
							Tax:&nbsp;&nbsp;&nbsp;&nbsp;</td>
						<td class="content" align="left" width="150" colSpan="3"><asp:checkbox id="LocalTax" runat="server"></asp:checkbox></td>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</TR>
					<tr>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</tr>
					<TR>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<td class="content" noWrap align="right" width="180">Apply State 
							Tax:&nbsp;&nbsp;&nbsp;&nbsp;</td>
						<td class="content" align="left" width="150" colSpan="3"><asp:checkbox id="StateTax" runat="server"></asp:checkbox></td>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</TR>
					<tr>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</tr>
					<TR>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<td class="content" noWrap align="right" width="180">Apply Country 
							Tax:&nbsp;&nbsp;&nbsp;&nbsp;</td>
						<td class="content" align="left" width="150" colSpan="3"><asp:checkbox id="CountryTax" runat="server"></asp:checkbox></td>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</TR>
					<tr>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</tr>
					<TR>
						<TD class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></TD>
					</TR>
					<tr>
						<TD class="Content" width="1" colSpan="6">&nbsp;</TD>
					</tr>
				</TBODY></table>
		</td>
		<td class="Content" width="2%">&nbsp;</td>
		<td class="Content" vAlign="top" width="63%">
			<table id="subscriptionTable" cellSpacing="0" cellPadding="0" width="100%" runat="server">
				<TBODY>
					<tr>
						<TD class="Content" width="1" colSpan="6">&nbsp;</TD>
					</tr>
					<TR>
						<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="ContentTableHeader" noWrap align="left" colSpan="3">&nbsp;&nbsp;Recurring 
							Payment Options&nbsp;
						</TD>
						<TD class="ContentTableHeader" noWrap align="right"><A class="ContentTableHeader" href="javascript: doHelp('http://support.storefront.net/mtdocs70/products_recurring.asp')">What's 
								This?</A></TD>
						<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</TR>
					<tr>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</tr>
					<tr>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</tr>
					<TR>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<td class="Content" align="right">Subscription Price:&nbsp;&nbsp;</td>
						<td class="Content" align="left"><asp:textbox id="txtSubscriptionPrice" Runat="server" MaxLength="255" Columns="6"></asp:textbox></td>
						<td class="Content" align="right">Subscription Term:&nbsp;&nbsp;</td>
						<td class="Content" align="left"><asp:textbox id="txtSubscriptionTerm" Runat="server" MaxLength="255" Columns="6"></asp:textbox></td>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</TR>
					<tr>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</tr>
					<tr>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</tr>
					<tr>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<td class="Content" align="right">Payment Period:&nbsp;&nbsp;</td>
						<td class="Content" align="left"><asp:dropdownlist id="PaymentPeriod" Runat="server">
								<asp:ListItem Value="-1">--Select One--</asp:ListItem>
								<asp:ListItem Value="0">Weekly</asp:ListItem>
								<asp:ListItem Value="1">BiWeekly</asp:ListItem>
								<asp:ListItem Value="2">Twice a Month</asp:ListItem>
								<asp:ListItem Value="3">Every 4 weeks</asp:ListItem>
								<asp:ListItem Value="4">Monthly</asp:ListItem>
								<asp:ListItem Value="5">Quarterly</asp:ListItem>
								<asp:ListItem Value="6">Twice a Year</asp:ListItem>
								<asp:ListItem Value="7">Yearly</asp:ListItem>
							</asp:dropdownlist></td>
						<td class="Content" align="right">Billing Delay(periods):&nbsp;&nbsp;</td>
						<td class="Content" align="left"><asp:textbox id="txtBillingDelay" Runat="server" MaxLength="255" Columns="6"></asp:textbox></td>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</tr>
					<tr>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</tr>
					<tr>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</tr>
					<TR>
						<TD class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></TD>
					</TR>
					<tr>
						<TD class="Content" width="1" colspan="6">&nbsp;</TD>
					</tr>
				</TBODY></table>
		</td>
	</tr>
	<TR>
		<td class="content" align="right" width="75%" colSpan="6">
			<asp:LinkButton ID="cmdSave" Runat="server">
				<asp:Image BorderWidth="0" ID="imgSave" runat="server" ImageUrl="../images/save.jpg" AlternateText="Save"></asp:Image>
			</asp:LinkButton>
		</td>
	</TR>
</TABLE>
