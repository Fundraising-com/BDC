<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClientHeader.ascx.cs" Inherits="EFundraisingCRMWeb.Components.User.ClientHeader" %>
<TABLE cellSpacing="0" cellPadding="0" width="744" class="GeneralBackGround" style="WIDTH: 744px; HEIGHT: 8px">
	<TR>
		<TD style="WIDTH: 60px; HEIGHT: 4px" vAlign="top"></TD>
		<td style="WIDTH: 304px; HEIGHT: 4px" vAlign="top" align="left">
			<TABLE cellSpacing="0" cellPadding="0">
				<tr>
					<TD class="NormalHeaderTextBold" width="67" style="WIDTH: 67px">Person:&nbsp;</TD>
					<TD>
						<asp:Label id="PersonLabel" runat="server" CssClass="NormalHeaderValueBold"></asp:Label></TD>
				</tr>
			</TABLE>
		</td>
		<td style="HEIGHT: 4px" vAlign="top" align="left">
			<TABLE cellSpacing="0" cellPadding="0">
				<tr>
					<TD class="NormalHeaderTextBold" width="65" style="WIDTH: 65px">E-Mail:&nbsp;</TD>
					<TD>
						<asp:Label id="EmailLabel" runat="server" CssClass="NormalHeaderValueBold"></asp:Label></TD>
				</tr>
			</TABLE>
		</td>
	</TR>
	<TR>
		<TD style="WIDTH: 60px" vAlign="top"></TD>
		<td style="WIDTH: 304px" vAlign="top" align="left">
			<TABLE cellSpacing="0" cellPadding="0">
				<tr>
					<TD class="NormalHeaderTextBold" width="68" style="WIDTH: 68px">Group:&nbsp;</TD>
					<TD>
						<asp:Label id="GroupLabel" runat="server" CssClass="NormalHeaderValueBold" Width="200px"></asp:Label></TD>
				</tr>
			</TABLE>
		</td>
		<td vAlign="top" align="left">
			<TABLE cellSpacing="0" cellPadding="0">
				<tr>
					<TD class="NormalHeaderTextBold" width="65" style="WIDTH: 65px">Phone:&nbsp;</TD>
					<TD>
						<asp:Label id="PhoneLabel" runat="server" CssClass="NormalHeaderValueBold"></asp:Label></TD>
				</tr>
			</TABLE>
		</td>
	</TR>
</TABLE>
