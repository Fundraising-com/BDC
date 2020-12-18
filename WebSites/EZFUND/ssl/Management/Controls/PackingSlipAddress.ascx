<%@ Register TagPrefix="uc1" TagName="AddressLabel" Src="../../Controls/AddressLabel.ascx" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="PackingSlipAddress.ascx.vb" Inherits="StoreFront.StoreFront.PackingSlipAddress" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
	<tr>
		<td>&nbsp;</td>
		<td width="50%"><TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
				<TR>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="ContentTableHeader" noWrap align="left" colSpan="3">&nbsp;&nbsp;Billing 
						Information
					</TD>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<tr>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="ContentTableHeader" width="1" colSpan="3"><IMG height="5" src="images/clear.gif"></TD>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</tr>
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="Content" colSpan="3" nowrap align="middle" height="100%" valign="top">
						<uc1:AddressLabel id="AddressLabel1" runat="server"></uc1:AddressLabel>
					</TD>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" colSpan="5" height="1"><IMG height="1" src="images/clear.gif"></TD>
				</TR>
			</TABLE>
		</td>
		<td>&nbsp;</td>
		<td width="50%"><TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
				<TR>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="ContentTableHeader" noWrap align="left" colSpan="3">&nbsp;&nbsp;Ship To:
						<asp:Label ID="lblNickName" Runat="server" CssClass="ContentTableHeader"></asp:Label>
					</TD>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<tr>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="ContentTableHeader" width="1" colSpan="3"><IMG height="5" src="images/clear.gif"></TD>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</tr>
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="Content" colSpan="3" nowrap height="100%" align="middle" valign="top">
						<uc1:AddressLabel id="AddressLabel2" runat="server"></uc1:AddressLabel>
					</TD>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" colSpan="5" height="1"><IMG height="1" src="images/clear.gif"></TD>
				</TR>
			</TABLE>
		</td>
		<td>&nbsp;</td>
	</tr>
</TABLE>
