<%@ Register TagPrefix="contentpanel" Namespace="GA.BDC.Core.Web.UI.UIControls" Assembly="GA.BDC.Core.Web.UI.UIControls" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SuccessStory.ascx.cs" Inherits="GA.BDC.WEB.ScratchcardWeb.Components.User.Controls.Common.SuccessStory" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR height="35">
		<TD colspan="3"><contentpanel:contentpanelcontrol id="ContentPanelControl1" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Components\User\Controls\Common\SuccessStory.ascx"></contentpanel:contentpanelcontrol></TD>
	</TR>
	<TR>
		<td width="12">&nbsp;&nbsp;</td>
		<TD class="success_story">"<asp:label id="lblStory" runat="server"></asp:label>..."</TD>
		<td width="12">&nbsp;</td>
	</TR>
	<TR height="20">
		<td width="12">&nbsp;&nbsp;</td>
		<TD>
			<contentpanel:ContentPanelControl id="ContentPanelControl2" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Components\User\Controls\Common\SuccessStory.ascx"></contentpanel:ContentPanelControl></TD>
		<td width="12">&nbsp;&nbsp;</td>
	</TR>
</TABLE>
<ContentPanel:PagePanelControl id="PagePanelControl1" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Components\User\Controls\Common\SuccessStory.ascx"></ContentPanel:PagePanelControl>
