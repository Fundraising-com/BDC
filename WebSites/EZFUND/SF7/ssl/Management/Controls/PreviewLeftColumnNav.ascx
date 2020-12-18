<%@ Register TagPrefix="uc1" TagName="PreviewMenuBar" Src="PreviewMenuBar.ascx" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="PreviewLeftColumnNav.ascx.vb" Inherits="StoreFront.StoreFront.PreviewLeftColumnNav" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<table border="0" id="Table1" cellpadding="0" cellspacing="0" width="100%" runat="Server" visible="True">
	<tr>
		<td class="LeftColumnText">
			<uc1:PreviewMenuBar id="CMenubar1" CallPage="left" runat="server"></uc1:PreviewMenuBar>
		</td>
	</tr>
</table>
