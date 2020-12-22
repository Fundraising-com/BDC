<%@ Control Language="vb" AutoEventWireup="false" Codebehind="addcoupon.ascx.vb" Inherits="StoreFront.StoreFront.addcoupon" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<asp:label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:label><asp:textbox id="txtApplyToID" runat="server" Visible="False">0</asp:textbox><br>
<br>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td class="Content">&nbsp;</td>
		<td width="100%" colSpan="3">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<td class="ContentTableHeader">&nbsp;</td>
					<td class="ContentTableHeader" align="left">General</td>
					<td class="ContentTableHeader">&nbsp;</td>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</tr>
				<tr>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<td class="Content" colSpan="3">&nbsp;</td>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</tr>
				<tr>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<td class="Content">&nbsp;</td>
					<td class="Content">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="Content" noWrap align="right">Activate Coupon: &nbsp;</td>
								<td class="Content" noWrap align="left"><asp:checkbox id="chkActive" Runat="server"></asp:checkbox></td>
								<td class="Content" noWrap width="100%">&nbsp;</td>
							</tr>
							<tr>
								<td class="Content" noWrap align="right">Coupon Description : &nbsp;</td>
								<td class="Content" noWrap align="left"><asp:textbox id="txtDiscription" Runat="server" Columns="20" MaxLength="100"></asp:textbox></td>
								<td class="Content" noWrap width="100%">&nbsp;</td>
							</tr>
							<TR>
								<TD class="Content" noWrap align="right">Coupon Code:&nbsp;</TD>
								<td class="Content" noWrap align="left"><asp:textbox id="txtCode" Runat="server" Columns="20" MaxLength="100"></asp:textbox></td>
								<td class="Content" noWrap width="100%">&nbsp;</td>
							</TR>
							<TR>
								<TD class="Content" noWrap align="right">For Orders Equal to or Above:&nbsp;</TD>
								<td class="Content" noWrap align="left"><asp:textbox id="minAmt" Runat="server" Columns="10"></asp:textbox></td>
								<td class="Content" noWrap width="100%">&nbsp;</td>
							</TR>
						</table>
					</td>
					<td class="Content">&nbsp;</td>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</tr>
				<tr>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<td class="Content" colSpan="3">&nbsp;</td>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</tr>
				<tr>
					<TD class="ContentTable" colSpan="5" height="1"><IMG height="1" src="images/clear.gif"></TD>
				</tr>
			</table>
		</td>
		<td class="Content">&nbsp;</td>
	</tr>
	<tr>
		<TD class="Content" colSpan="5">&nbsp;</TD>
	</tr>
	<tr>
		<td class="Content">&nbsp;</td>
		<td width="100%" colSpan="3">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<td class="ContentTableHeader">&nbsp;</td>
					<td class="ContentTableHeader" align="left">Discount</td>
					<td class="ContentTableHeader">&nbsp;</td>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</tr>
				<tr>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<td class="Content" colSpan="3">&nbsp;</td>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</tr>
				<tr>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<td class="Content">&nbsp;</td>
					<td class="Content">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="Content" noWrap align="right">Amount: &nbsp;</td>
								<td class="Content" noWrap align="left"><asp:textbox id="txtAmount" Runat="server" Columns="10"></asp:textbox>&nbsp;
									<asp:radiobutton id="rdoDollar" Runat="server" Text="Dollar" Checked="True" GroupName="AmountType"></asp:radiobutton><asp:radiobutton id="rdoPercent" Runat="server" Text="Percent" GroupName="AmountType"></asp:radiobutton>&nbsp;&nbsp;</td>
								<td class="Content" noWrap width="100%">&nbsp;</td>
							</tr>
							<tr>
								<TD class="Content" noWrap align="right">Apply Discount To:&nbsp;</TD>
								<TD class="Content" noWrap align="left"><asp:dropdownlist id="ddlApplyTo" runat="server" AutoPostBack="True">
										<asp:ListItem Value="0">All Products</asp:ListItem>
										<asp:ListItem Value="1">Select Product</asp:ListItem>
										<asp:ListItem Value="2">All Product In Category</asp:ListItem>
										<asp:ListItem Value="4">All Product Assigned to Manufacturer</asp:ListItem>
										<asp:ListItem Value="3">All Product Assigned to Vendor</asp:ListItem>
									</asp:dropdownlist>&nbsp;
									<asp:LinkButton ID="btnSelect" Runat="server" onclick="SelectClick" Visible="False">
										<asp:Image BorderWidth="0" ID="imgSelect" runat="server" ImageUrl="../images/select.jpg" AlternateText="Select" Visible="False"></asp:Image>
									</asp:LinkButton>
								</TD>
								<TD class="Content" noWrap width="100%"></TD>
							</tr>
						</table>
					</td>
					<td class="Content">&nbsp;</td>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</tr>
				<tr>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<td class="Content" colSpan="3">&nbsp;</td>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</tr>
				<tr>
					<TD class="ContentTable" colSpan="5" height="1"><IMG height="1" src="images/clear.gif"></TD>
				</tr>
			</table>
		</td>
		<td class="Content">&nbsp;</td>
	</tr>
	<tr>
		<TD class="Content" colSpan="5">&nbsp;</TD>
	</tr>
	<tr>
		<td class="Content">&nbsp;</td>
		<td width="100%" colSpan="3">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<td class="ContentTableHeader">&nbsp;</td>
					<td class="ContentTableHeader" align="left">Expiration</td>
					<td class="ContentTableHeader">&nbsp;</td>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</tr>
				<tr>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<td class="Content" colSpan="3">&nbsp;</td>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</tr>
				<tr>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<td class="Content">&nbsp;</td>
					<td class="Content">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="Content" noWrap align="right">Discount Expires:&nbsp;</TD>
								<TD class="Content" noWrap align="left"><asp:dropdownlist id="DropDownList1" runat="server">
										<asp:ListItem Value="Never">Never</asp:ListItem>
										<asp:ListItem Value="On Specified Date">On Specified Date</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD class="Content" noWrap width="100%"></TD>
							</TR>
							<TR>
								<TD class="Content" noWrap align="right"></TD>
								<TD class="Content" noWrap align="left">Expiration Date:&nbsp;<asp:textbox id="txtDate" runat="server" Columns="10"></asp:textbox></TD>
								<TD class="Content" noWrap width="100%"></TD>
							</TR>
							<TR>
								<TD class="Content" noWrap align="right">One&nbsp;Time&nbsp;Use:&nbsp;</TD>
								<TD class="Content" noWrap><asp:checkbox id="chkOneTime" Runat="server" Text=""></asp:checkbox></TD>
								<TD class="Content" noWrap align="right"></TD>
							</TR>
						</table>
					</td>
					<td class="Content">&nbsp;</td>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</tr>
				<tr>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<td class="Content" colSpan="3">&nbsp;</td>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</tr>
				<tr>
					<TD class="ContentTable" colSpan="5" height="1"><IMG height="1" src="images/clear.gif"></TD>
				</tr>
			</table>
		</td>
		<td class="Content">&nbsp;</td>
	</tr>
	<tr>
		<td class="Content" colSpan="5">&nbsp;</td>
	</tr>
	<tr>
		<td class="Content">&nbsp;</td>
		<td width="100%" colSpan="3"></td>
		<td class="Content">&nbsp;</td>
	</tr>
	<tr>
		<td class="Content" colSpan="5">&nbsp;</td>
	</tr>
	<tr>
		<TD class="Content" align="right" colSpan="5" height="1">
			<asp:LinkButton ID="btnSave" Runat="server" onclick="SaveClick">
				<asp:Image BorderWidth="0" ID="imgSave" runat="server" ImageUrl="../images/save.jpg" AlternateText="Save"></asp:Image>
			</asp:LinkButton>
		</TD>
	</tr>
</table>
