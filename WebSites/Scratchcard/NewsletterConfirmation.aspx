<%@ Register TagPrefix="contentpanel" Namespace="GA.BDC.Core.Web.UI.UIControls" Assembly="GA.BDC.Core.Web.UI.UIControls" %>
<%@ Page language="c#" Codebehind="NewsletterConfirmation.aspx.cs" AutoEventWireup="false" Inherits="GA.BDC.WEB.ScratchcardWeb.NewsletterConfirmation" %>
<%@ Register TagPrefix="efundraising" Namespace="GA.BDC.Core.Web.UI.MasterPages" Assembly="GA.BDC.Core.Web.UI.MasterPages" %>
<efundraising:MASTERPAGE id="MasterPage1" runat="server" master="~/MasterPage/SiteTemplate1.ascx">
	<efundraising:Content id="Content1" runat="server" ContentPlaceHolderID="cph_PageContent">
		<DIV class="normal_text" align="center" style="font-weight: bold;">
			<contentpanel:ContentPanelControl id="ContentPanelControl1" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\NewsletterConfirmation.aspx"></contentpanel:ContentPanelControl></DIV>
	</efundraising:Content>
</efundraising:MASTERPAGE>
<contentpanel:PagePanelControl id="PagePanelControl1" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\NewsletterConfirmation.aspx"></contentpanel:PagePanelControl>
