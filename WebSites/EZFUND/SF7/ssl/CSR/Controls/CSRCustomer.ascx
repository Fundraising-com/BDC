<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="CSRCustomer.ascx.vb" Inherits="StoreFront.StoreFront.CSRCustomer" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<TABLE class="contentTable" id="tbl1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD class="content" noWrap width="100%" colSpan="6">&nbsp;
		</TD>
	</TR>
	<TR>
		<td class="ContentTableHeader" noWrap></td>
		<TD class="ContentTableHeader" align="left" colSpan="5">Search For An Existing 
			Customer</TD>
		<td class="ContentTableHeader" noWrap></td>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="Content" style="padding-left:3px;">First Name:&nbsp;<br>
			<asp:textbox id="FirstName" Runat="server" columns="22"></asp:textbox>&nbsp;&nbsp;
		</TD>
		<TD class="Content">Last Name:&nbsp;<br>
			<asp:textbox id="LastName" Runat="server" columns="22"></asp:textbox>&nbsp;&nbsp;
		</TD>
		<TD class="Content">
			<table cellpadding="0" cellspacing="0" border="0">
				<tr>
					<td class="content">E-Mail Address:&nbsp;<br>
						<asp:textbox id="Email" Runat="server" columns="22"></asp:textbox>&nbsp;&nbsp;
					</td>
					<TD class="Content" align="left">
						&nbsp;<br>
						<asp:linkbutton id="Search" Runat="server">
							<asp:Image ID="Image1" Runat="server" ImageUrl="../images/icon_go.gif"></asp:Image>
						</asp:linkbutton>
					</TD>
				</tr>
			</table>
		</TD>
		<TD class="Content" valign="bottom" width="270" noWrap>Select a Customer:<br>
			<asp:dropdownlist id="Customers" Runat="server" AutoPostBack="True"></asp:dropdownlist>
		</TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" colspan="6"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
</TABLE>
<br>
<TABLE class="contentTable" id="tbl1" cellSpacing="0" cellPadding="1" width="100%" border="0">
	<tr>
		<td class="Content">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<td class="ContentTableHeader">&nbsp;</td>
					<TD class="ContentTableHeader" colSpan="3"><asp:label id="Label7" CssClass="ContentTableHeader" runat="server">Billing Address</asp:label>&nbsp;&nbsp;<asp:dropdownlist id="BillAddresslist" runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="Content" align="right" colSpan="4">&nbsp;</TD>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<td class="Content">&nbsp;</td>
					<TD class="Content" vAlign="top" noWrap align="right">Nick Name:&nbsp;
					</TD>
					<TD class="Content" vAlign="top" noWrap><asp:textbox id="BillNickName" runat="server" MaxLength="50" cssclass="Content"></asp:textbox><asp:label id="Label8" Runat="server" CssClass="ErrorMessages">*</asp:label></TD>
					<td class="Content">&nbsp;</td>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<td class="Content">&nbsp;</td>
					<TD class="Content" vAlign="top" noWrap align="right">First Name:&nbsp;</TD>
					<TD class="Content" vAlign="top" noWrap><asp:textbox id="BillFirstName" runat="server" MaxLength="100" cssclass="Content"></asp:textbox><asp:label id="Label9" Runat="server" CssClass="ErrorMessages">*</asp:label></TD>
					<td class="Content">&nbsp;</td>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<td class="Content">&nbsp;</td>
					<TD class="Content" vAlign="top" noWrap align="right">Middle Initial:&nbsp;
					</TD>
					<TD class="Content" vAlign="top"><asp:textbox id="BillMI" runat="server" MaxLength="2" cssclass="Content" Columns="3"></asp:textbox></TD>
					<td class="Content">&nbsp;</td>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<td class="Content">&nbsp;</td>
					<TD class="Content" vAlign="top" noWrap align="right">Last Name:&nbsp;</TD>
					<TD class="Content" vAlign="top" noWrap><asp:textbox id="BillLastName" runat="server" MaxLength="100" cssclass="Content"></asp:textbox><asp:label id="Label11" Runat="server" CssClass="ErrorMessages">*</asp:label></TD>
					<td class="Content">&nbsp;</td>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<td class="Content">&nbsp;</td>
					<TD class="Content" vAlign="top" noWrap align="right">Company:&nbsp;</TD>
					<TD class="Content" vAlign="top"><asp:textbox id="BillCompany" runat="server" MaxLength="75" cssclass="Content"></asp:textbox></TD>
					<td class="Content">&nbsp;</td>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<td class="Content">&nbsp;</td>
					<TD class="Content" vAlign="top" noWrap align="right">Address:&nbsp;</TD>
					<TD class="Content" vAlign="top" noWrap><asp:textbox id="BillAddress1" runat="server" MaxLength="255" cssclass="Content"></asp:textbox><asp:label id="Label12" Runat="server" CssClass="ErrorMessages">*</asp:label></TD>
					<td class="Content">&nbsp;</td>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<td class="Content">&nbsp;</td>
					<TD class="Content" vAlign="top" noWrap align="right">Address2:&nbsp;</TD>
					<TD class="Content" vAlign="top"><asp:textbox id="BillAddress2" runat="server" MaxLength="255" cssclass="Content"></asp:textbox></TD>
					<td class="Content">&nbsp;</td>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<td class="Content">&nbsp;</td>
					<TD class="Content" vAlign="top" noWrap align="right">City:&nbsp;</TD>
					<TD class="Content" vAlign="top" noWrap><asp:textbox id="BillCity" runat="server" MaxLength="50" cssclass="Content"></asp:textbox><asp:label id="Label13" Runat="server" CssClass="ErrorMessages">*</asp:label></TD>
					<td class="Content">&nbsp;</td>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<td class="Content">&nbsp;</td>
					<TD class="Content" vAlign="top" noWrap align="right">State/Province:&nbsp;</TD>
					<TD class="Content" vAlign="top" noWrap><cc1:selectvalcontrol id="BillState" Width="204px" runat="server" DisplaySelect="States"></cc1:selectvalcontrol><FONT color="#ff0000"></FONT></TD>
					<td class="Content">&nbsp;</td>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<td class="Content">&nbsp;</td>
					<TD class="Content" vAlign="top" noWrap align="right">Postal Code:&nbsp;</TD>
					<TD class="Content" vAlign="top" noWrap><asp:textbox id="BillZip" runat="server" MaxLength="50" cssclass="Content"></asp:textbox></TD>
					<td class="Content">&nbsp;</td>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<td class="Content">&nbsp;</td>
					<TD class="Content" vAlign="top" noWrap align="right">Country:&nbsp;</TD>
					<TD class="Content" vAlign="top" noWrap><cc1:selectvalcontrol id="BillCountry" Width="205px" runat="server" DisplaySelect="Country"></cc1:selectvalcontrol><FONT color="#ff0000"></FONT></TD>
					<td class="Content">&nbsp;</td>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<td class="Content">&nbsp;</td>
					<TD class="Content" vAlign="top" noWrap align="right">Phone:&nbsp;</TD>
					<TD class="Content" vAlign="top" noWrap><asp:textbox id="BillPhone" runat="server" cssclass="Content"></asp:textbox><asp:label id="Label14" Runat="server" CssClass="ErrorMessages">*</asp:label></TD>
					<td class="Content">&nbsp;</td>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<td class="Content">&nbsp;</td>
					<TD class="Content" vAlign="top" noWrap align="right">Fax:&nbsp;</TD>
					<TD class="Content" vAlign="top"><asp:textbox id="BillFax" runat="server" cssclass="Content"></asp:textbox></TD>
					<td class="Content">&nbsp;</td>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR id="trBillEmail" runat="server">
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<td class="Content">&nbsp;</td>
					<TD class="Content" vAlign="top" noWrap align="right">E-Mail:&nbsp;</TD>
					<TD class="Content" vAlign="top"><asp:textbox id="BillEmail" runat="server" cssclass="Content"></asp:textbox>
						<asp:label id="Label15" Runat="server" CssClass="ErrorMessages">*</asp:label></TD>
					<td class="Content">&nbsp;</td>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" width="1" height="19"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="Content" height="19"></TD>
					<TD class="Content" vAlign="middle" align="right" height="19">
						&nbsp;Send E-Mail Confirmation:&nbsp;
					</TD>
					<TD class="Content" noWrap align="left" height="19"><asp:checkbox id="SendEmail" runat="server" ToolTip="Check to send customer E-Mail Confirmation"
							Checked="True"></asp:checkbox></TD>
					<TD class="Content" height="19"></TD>
					<TD class="ContentTable" width="1" height="19"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" height="1"><IMG height="1" src="images/clear.gif"></TD>
					<TD class="ContentTable" colSpan="4" height="1"><IMG height="1" src="images/clear.gif"></TD>
					<TD class="ContentTable" height="1"><IMG height="1" src="images/clear.gif"></TD>
				</TR>
				<TR>
					<TD class="Content" colSpan="6">&nbsp;</TD>
				</TR>
			</TABLE>
		</td>
		<td vAlign="top" class="Content"><asp:Linkbutton ID="MoveRight" Runat="server" EnableViewState="True"><IMG src="images/right.gif" border="0"></asp:Linkbutton></td>
		<td vAlign="top" class="Content" align="right" width="50%">
			<TABLE id="ShippingHeaderTable" runat="server" cellSpacing="0" cellPadding="0" width="100%"
				border="0">
				<TR>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<td class="ContentTableHeader">&nbsp;</td>
					<TD class="ContentTableHeader" noWrap colSpan="3"><asp:label id="Label1" CssClass="ContentTableHeader" runat="server">Shipping Address</asp:label>&nbsp;&nbsp;<asp:dropdownlist id="ShipAddressList" runat="server" AutoPostBack="True"></asp:dropdownlist>
						<br>
						<asp:CheckBox id="chkMultiShip" Runat="server" AutoPostBack="True"></asp:CheckBox><asp:LinkButton id="MapShipments" Runat="server">
											Map Shipments
					</asp:LinkButton>
					</TD>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
			</TABLE>
			<TABLE id="ShippingTable" runat="server" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="Content" align="right" colSpan="4">&nbsp;</TD>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="Content">&nbsp;</TD>
					<TD class="Content" vAlign="top" noWrap align="right">Nick Name:&nbsp;
					</TD>
					<TD class="Content" vAlign="top" noWrap>
						<asp:textbox id="ShipNickName" runat="server" cssclass="Content" MaxLength="50"></asp:textbox>
						<asp:label id="Label2" Runat="server" CssClass="ErrorMessages">*</asp:label></TD>
					<TD class="Content">&nbsp;</TD>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="Content">&nbsp;</TD>
					<TD class="Content" vAlign="top" noWrap align="right">First Name:&nbsp;</TD>
					<TD class="Content" vAlign="top" noWrap>
						<asp:textbox id="ShipFirstName" runat="server" cssclass="Content" MaxLength="100"></asp:textbox>
						<asp:label id="Label3" Runat="server" CssClass="ErrorMessages">*</asp:label></TD>
					<TD class="Content">&nbsp;</TD>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="Content">&nbsp;</TD>
					<TD class="Content" vAlign="top" noWrap align="right">Middle Initial:&nbsp;
					</TD>
					<TD class="Content" vAlign="top">
						<asp:textbox id="ShipMI" runat="server" cssclass="Content" MaxLength="2" Columns="3"></asp:textbox></TD>
					<TD class="Content">&nbsp;</TD>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="Content">&nbsp;</TD>
					<TD class="Content" vAlign="top" noWrap align="right">Last Name:&nbsp;</TD>
					<TD class="Content" vAlign="top" noWrap>
						<asp:textbox id="ShipLastName" runat="server" cssclass="Content" MaxLength="100"></asp:textbox>
						<asp:label id="Label4" Runat="server" CssClass="ErrorMessages">*</asp:label></TD>
					<TD class="Content">&nbsp;</TD>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="Content">&nbsp;</TD>
					<TD class="Content" vAlign="top" noWrap align="right">Company:&nbsp;</TD>
					<TD class="Content" vAlign="top">
						<asp:textbox id="ShipCompany" runat="server" cssclass="Content" MaxLength="75"></asp:textbox></TD>
					<TD class="Content">&nbsp;</TD>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="Content">&nbsp;</TD>
					<TD class="Content" vAlign="top" noWrap align="right">Address:&nbsp;</TD>
					<TD class="Content" vAlign="top" noWrap>
						<asp:textbox id="ShipAddress1" runat="server" cssclass="Content" MaxLength="255"></asp:textbox>
						<asp:label id="Label5" Runat="server" CssClass="ErrorMessages">*</asp:label></TD>
					<TD class="Content">&nbsp;</TD>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="Content">&nbsp;</TD>
					<TD class="Content" vAlign="top" noWrap align="right">Address2:&nbsp;</TD>
					<TD class="Content" vAlign="top">
						<asp:textbox id="ShipAddress2" runat="server" cssclass="Content" MaxLength="255"></asp:textbox></TD>
					<TD class="Content">&nbsp;</TD>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="Content">&nbsp;</TD>
					<TD class="Content" vAlign="top" noWrap align="right">City:&nbsp;</TD>
					<TD class="Content" vAlign="top" noWrap>
						<asp:textbox id="ShipCity" runat="server" cssclass="Content" MaxLength="50"></asp:textbox>
						<asp:label id="Label6" Runat="server" CssClass="ErrorMessages">*</asp:label></TD>
					<TD class="Content">&nbsp;</TD>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="Content">&nbsp;</TD>
					<TD class="Content" vAlign="top" noWrap align="right">State/Province:&nbsp;</TD>
					<TD class="Content" vAlign="top" noWrap>
						<CC1:SELECTVALCONTROL id="ShipState" Width="204px" runat="server" DisplaySelect="States"></CC1:SELECTVALCONTROL><FONT color="#ff0000"></FONT></TD>
					<TD class="Content">&nbsp;</TD>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="Content">&nbsp;</TD>
					<TD class="Content" vAlign="top" noWrap align="right">Postal Code:&nbsp;</TD>
					<TD class="Content" vAlign="top" noWrap>
						<asp:textbox id="ShipZip" runat="server" cssclass="Content" MaxLength="50"></asp:textbox></TD>
					<TD class="Content">&nbsp;</TD>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="Content">&nbsp;</TD>
					<TD class="Content" vAlign="top" noWrap align="right">Country:&nbsp;</TD>
					<TD class="Content" vAlign="top" noWrap>
						<CC1:SELECTVALCONTROL id="ShipCountry" Width="205px" runat="server" DisplaySelect="Country"></CC1:SELECTVALCONTROL><FONT color="#ff0000"></FONT></TD>
					<TD class="Content">&nbsp;</TD>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="Content">&nbsp;</TD>
					<TD class="Content" vAlign="top" noWrap align="right">Phone:&nbsp;</TD>
					<TD class="Content" vAlign="top" noWrap>
						<asp:textbox id="ShipPhone" runat="server" cssclass="Content"></asp:textbox>
						<asp:label id="Label10" Runat="server" CssClass="ErrorMessages">*</asp:label></TD>
					<TD class="Content">&nbsp;</TD>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="Content">&nbsp;</TD>
					<TD class="Content" vAlign="top" noWrap align="right">Fax:&nbsp;</TD>
					<TD class="Content" vAlign="top">
						<asp:textbox id="ShipFax" runat="server" cssclass="Content"></asp:textbox></TD>
					<TD class="Content">&nbsp;</TD>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" height="1"><IMG height="1" src="images/clear.gif"></TD>
					<TD class="ContentTable" colSpan="4" height="1"><IMG height="1" src="images/clear.gif"></TD>
					<TD class="ContentTable" height="1"><IMG height="1" src="images/clear.gif"></TD>
				</TR>
				<TR>
					<TD class="Content" colSpan="6">&nbsp;</TD>
				</TR>
			</TABLE>
		</td>
	</tr>
</TABLE>
