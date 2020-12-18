<%@ Control Language="c#" AutoEventWireup="false" Codebehind="DidYouKnow.ascx.cs" Inherits="GA.BDC.WEB.ScratchcardWeb.Components.User.Controls.Common.DidYouKnow" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="contentpanel" Namespace="GA.BDC.Core.Web.UI.UIControls" Assembly="GA.BDC.Core.Web.UI.UIControls" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR height="35">
		<TD colSpan="3"><contentpanel:contentpanelcontrol id="ContentPanelControl1" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Components\User\Controls\Common\DidYouKnow.ascx"
				runat="server"></contentpanel:contentpanelcontrol></TD>
	</TR>
	<TR>
		<td width="12">&nbsp;&nbsp;</td>
		<TD class="small_black_bold">
			<asp:label id="lblStory" runat="server"></asp:label></TD>
		<td width="12">&nbsp;&nbsp;</td>
	</TR>
</TABLE>
<CONTENTPANEL:PAGEPANELCONTROL id="PagePanelControl1" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Components\User\Controls\Common\DidYouKnow.ascx"
	runat="server"></CONTENTPANEL:PAGEPANELCONTROL>
