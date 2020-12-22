<%@ Control Language="vb" AutoEventWireup="false" Codebehind="CSRPayment.ascx.vb" Inherits="StoreFront.StoreFront.CSRPayment" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="ContentTableHeader" width="100%">&nbsp;Payment Type</TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="content"><asp:radiobuttonlist CssClass="content" AutoPostBack="True" id="PaymentTypes" Runat="server" RepeatDirection="Horizontal"
				RepeatColumns="4" Width="100%"></asp:radiobuttonlist></TD>
		<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTable" height="1" colspan="3"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="Content" colSpan="3">&nbsp;</TD>
	</TR>
</TABLE>
<TABLE id="tblCreditCard" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="ContentTableHeader" height="12">&nbsp;Credit Card</TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD>
			<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="Content" noWrap align="right">&nbsp;</TD>
					<TD class="Content" noWrap align="right" colSpan="5">&nbsp;&nbsp;</TD>
					<TD class="Content" align="left">&nbsp;</TD>
				</TR>
				<TR>
					<TD class="Content" noWrap align="right">&nbsp;</TD>
					<TD class="Content" noWrap align="left">Credit Card Number:</TD>
					<TD class="Content" vAlign="top" align="left">&nbsp;</TD>
					<TD class="Content" noWrap align="left" width="97"><asp:label Runat="server" ID="lblCVV">CVV/CVC</asp:label></TD>
					<TD class="Content" vAlign="middle" align="left"></TD>
					<TD class="Content" vAlign="middle" align="left">Expiration Date:</TD>
					<TD class="Content" align="left">&nbsp;</TD>
				</TR>
				<TR>
					<TD class="Content" noWrap align="right">&nbsp;</TD>
					<TD class="Content" noWrap align="left"><INPUT id="txtCardNumber" type="text" maxLength="16" name="txtCardNumber" runat="server"></TD>
					<TD class="Content" vAlign="top" align="left">&nbsp;</TD>
					<TD class="Content" noWrap align="left" width="97"><asp:textbox id="txtSecureCode" runat="server" MaxLength="4" Columns="5"></asp:textbox></TD>
					<TD class="Content" vAlign="middle" align="left"></TD>
					<TD class="Content" vAlign="middle" align="left">
						<cc1:selectvalcontrol id="txtExpMonth" runat="server" DisplaySelect="ExpireMonth"></cc1:selectvalcontrol>
						<cc1:selectvalcontrol id="txtExpYear" runat="server" DisplaySelect="ExpireYear"></cc1:selectvalcontrol>
					</TD>
					<TD class="Content" align="left">&nbsp;</TD>
				</TR>
				<TR>
					<TD class="Content" noWrap align="right">&nbsp;</TD>
					<TD class="Content" noWrap align="right" colSpan="5">&nbsp;</TD>
					<TD class="Content" align="left">&nbsp;</TD>
				</TR>
				<asp:panel id="pnlswitchSoloElements" Runat="server" Visible="False">
					<TR>
						<TD class="Content" noWrap align="left" width="494" colSpan="7"><FONT size="2">These 
								Fields are required for Switch/Solo Transactions Only!.</FONT></TD>
					</TR>
					<TR>
						<TD class="Content" noWrap align="right">&nbsp;</TD>
						<TD class="Content" noWrap align="left">Start Date&nbsp;</TD>
						<TD class="Content" vAlign="top" align="left">&nbsp;</TD>
						<TD class="Content" noWrap align="left" width="97">Issue Number</TD>
						<TD class="Content" vAlign="middle" align="left"></TD>
						<TD class="Content" vAlign="middle" align="left"></TD>
						<TD class="Content" align="left">&nbsp;</TD>
					</TR>
					<TR>
						<TD class="Content" noWrap align="right">&nbsp;</TD>
						<TD class="Content" noWrap align="right" colSpan="5">&nbsp;&nbsp;</TD>
						<TD class="Content" align="left">&nbsp;</TD>
					</TR>
					<TR>
						<TD class="Content" noWrap align="right">&nbsp;</TD>
						<TD class="Content" noWrap align="left">Month:&nbsp;
							<CC1:SELECTVALCONTROL id="txtStartMonth" runat="server" DisplaySelect="StartMonth"></CC1:SELECTVALCONTROL>
							Year:&nbsp;
							<CC1:SELECTVALCONTROL id="txtStartYear" runat="server" DisplaySelect="StartYear"></CC1:SELECTVALCONTROL></TD>
						<TD class="Content" vAlign="top" align="left">&nbsp;</TD>
						<TD class="Content" noWrap align="left" width="97">
							<asp:textbox id="txtIssueNum" runat="server" Columns="5" MaxLength="4"></asp:textbox></TD>
						<TD class="Content" vAlign="middle" align="left"></TD>
						<TD class="Content" vAlign="middle" align="left"></TD>
						<TD class="Content" align="left">&nbsp;</TD>
					</TR>
					<TR>
						<TD class="Content" noWrap align="right">&nbsp;</TD>
						<TD class="Content" noWrap align="right" colSpan="5">&nbsp;&nbsp;</TD>
						<TD class="Content" noWrap align="left" width="1">&nbsp;</TD>
					</TR>
				</asp:panel>
				<TR>
					<TD class="Content" noWrap align="right">&nbsp;</TD>
					<TD class="Content" noWrap align="right" colSpan="7">&nbsp;&nbsp;</TD>
					<TD class="Content" noWrap align="left" width="1">&nbsp;</TD>
				</TR>
			</TABLE>
		</TD>
		<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTable" height="1" colspan="3"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="Content" colSpan="4">&nbsp;</TD>
	</TR>
</TABLE>
<TABLE id="tblPurchaseOrder" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="ContentTableHeader" width="100%">&nbsp;Purchase Order</TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD>
			<TABLE id="Table7" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="Content" noWrap align="right" colSpan="3">&nbsp;</TD>
				</TR>
				<TR>
					<TD class="Content" noWrap align="right">&nbsp;</TD>
					<TD class="Content" vAlign="middle" noWrap align="center" width="100%">Purchase 
						Order Number:&nbsp;
						<asp:textbox id="txtPONumber" runat="server" MaxLength="100"></asp:textbox></TD>
					<TD class="Content" noWrap align="left">&nbsp;</TD>
				</TR>
				<TR>
					<TD class="Content" noWrap align="right" colSpan="3">&nbsp;</TD>
				</TR>
			</TABLE>
		</TD>
		<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="Content" colSpan="4">&nbsp;</TD>
	</TR>
</TABLE>
<TABLE id="tblECheck" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="ContentTableHeader" height="12">&nbsp;E-Check</TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD>
			<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="Content" noWrap align="right">&nbsp;</TD>
					<TD class="Content" noWrap align="right" colSpan="5">&nbsp;&nbsp;</TD>
					<TD class="Content" align="left">&nbsp;</TD>
				</TR>
				<TR>
					<TD class="Content" noWrap align="right">&nbsp;</TD>
					<TD class="Content" vAlign="middle" noWrap align="right">Check Number:&nbsp;</TD>
					<TD class="Content" align="left"><asp:textbox id="txtCheckNumber" runat="server" MaxLength="8" Columns="5"></asp:textbox></TD>
					<TD class="Content" align="left">&nbsp;</TD>
					<TD class="Content" noWrap align="right">Bank Name:&nbsp;</TD>
					<TD class="Content" align="left"><asp:textbox id="txtBankName" runat="server" MaxLength="255"></asp:textbox></TD>
					<TD class="Content" align="left">&nbsp;</TD>
				</TR>
				<TR>
					<TD class="Content" noWrap align="right">&nbsp;</TD>
					<TD class="Content" noWrap align="right" colSpan="5">&nbsp;</TD>
					<TD class="Content" align="left">&nbsp;</TD>
				</TR>
				<TR>
					<TD class="Content" noWrap align="right">&nbsp;</TD>
					<TD class="Content" noWrap align="right">Bank&nbsp;<BR>
						Routing Number:&nbsp;</TD>
					<TD class="Content" align="left"><asp:textbox id="txtRoutingNumber" runat="server" MaxLength="9"></asp:textbox></TD>
					<TD class="Content" align="left">&nbsp;</TD>
					<TD class="Content" noWrap align="right">Checking&nbsp;<BR>
						Account Number:&nbsp;</TD>
					<TD class="Content" noWrap align="left"><asp:textbox id="txtAccountNumber" runat="server" MaxLength="18"></asp:textbox></TD>
					<TD class="Content" align="left">&nbsp;</TD>
				</TR>
				<TR>
					<TD class="Content" noWrap align="right">&nbsp;</TD>
					<TD class="Content" noWrap align="right" colSpan="5">&nbsp;</TD>
					<TD class="Content" align="left">&nbsp;</TD>
				</TR>
				<TR>
					<TD class="Content" noWrap align="right">&nbsp;</TD>
					<TD class="Content" vAlign="baseline" noWrap align="right">SSN:&nbsp;</TD>
					<TD class="Content" noWrap align="left">
						<P><asp:textbox id="txtSSN" Runat="server" MaxLength="11"></asp:textbox></P>
						<P>&nbsp;</P>
					</TD>
				</TR>
				<TR>
					<TD class="Content" noWrap align="right">&nbsp;</TD>
					<TD class="Content" noWrap align="right" colSpan="5">&nbsp;&nbsp;</TD>
					<TD class="Content" noWrap align="left" width="1">&nbsp;</TD>
				</TR>
			</TABLE>
		</TD>
		<td class="contenttableheader" width="1"><img src="images/clear.gif"></td>
	</TR>
	<TR>
		<td class="contenttableheader" colspan="3" height="1"><img src="images/clear.gif"></td>
	</TR>
</TABLE>
