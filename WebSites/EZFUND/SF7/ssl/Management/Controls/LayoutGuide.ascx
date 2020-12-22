<%@ Control Language="vb" AutoEventWireup="false" Codebehind="LayoutGuide.ascx.vb" Inherits="StoreFront.StoreFront.LayoutGuide" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<table align="center" border="1" cellpadding="10" cellspacing="0" bordercolor="#ff3806">
	<tr align="center">
		<td align="center" id="tdGeneral" runat="server">
			<BR>
			<table border="1" cellpadding="0" cellspacing="0" bordercolor="#acb8b8" width="100%">
				<!-- MSTableType="layout" -->
				<tr>
					<td valign="middle" colspan="3" height="10%" id="tdTopBanner" runat="server">
						<!-- MSCellType="ContentHead" -->
						<p align="center">
							<asp:LinkButton id="lnkTopBanner" class="content" runat="server">Top Banner</asp:LinkButton></p>
					</td>
				</tr>
				<tr>
					<td valign="middle" colspan="3" height="10%" id="tdTopSubBanner" runat="server">
						<!-- MSCellType="ContentHead" -->
						<p align="center">
							<asp:LinkButton class="content" id="lnkTopSubBanner" runat="server">Top Sub Banner</asp:LinkButton></p>
					</td>
				</tr>
				<tr>
					<td valign="middle" width="172" id="tdLeftColumn" runat="server">
						<!-- MSCellType="NavBody" -->
						<p align="center">
							<asp:LinkButton id="lnkLeftColumn" class="content" runat="server">Left Column</asp:LinkButton></p>
					</td>
					<td valign="middle" width="250" id="tdContent" runat="server" height="300">
						<p align="center" class="content">Content</p>
					</td>
					<td valign="middle" width="176" id="tdRightColumn" runat="server">
						<!-- MSCellType="NavBody2" -->
						<p align="center">
							<asp:LinkButton class="content" id="lnkRightColumn" runat="server">Right Column</asp:LinkButton></p>
					</td>
				</tr>
				<tr>
					<td valign="top" colspan="3" height="10%" id="tdBottomBar" runat="server">
						<!-- MSCellType="ContentFoot2" -->
						<p align="center">
							<asp:LinkButton id="lnkBottomBar" class="content" runat="server">Bottom Bar</asp:LinkButton></p>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
