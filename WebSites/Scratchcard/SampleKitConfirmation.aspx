<%@ Page language="c#" Codebehind="SampleKitConfirmation.aspx.cs" AutoEventWireup="false" Inherits="GA.BDC.WEB.ScratchcardWeb.SampleKitConfirmation" %>
<%@ Register TagPrefix="contentpanel" Namespace="GA.BDC.Core.Web.UI.UIControls" Assembly="GA.BDC.Core.Web.UI.UIControls" %>
<%@ Register TagPrefix="efundraising" Namespace="GA.BDC.Core.Web.UI.MasterPages" Assembly="GA.BDC.Core.Web.UI.MasterPages" %>
<efundraising:MASTERPAGE id="MasterPage1" runat="server" master="~/MasterPage/SiteTemplate1.ascx">
	<efundraising:Content id="Content1" runat="server" ContentPlaceHolderID="cph_PageContent">
		<SPAN class="normal_text">
			<contentpanel:ContentPanelControl id="ContentPanelControl1" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\SampleKitConfirmation.aspx"></contentpanel:ContentPanelControl>
		</SPAN>
		<SCRIPT LANGUAGE="JavaScript">
		<!-- Overture Services Inc. 07/15/2003
		var cc_tagVersion = "1.0";
		var cc_accountID = "1861951591";
		var cc_marketID =  "0";
		var cc_protocol="http";
		var cc_subdomain = "convctr";
		if(location.protocol == "https:")
		{
			cc_protocol="https";
			cc_subdomain="convctrs";
		}
		var cc_queryStr = "?" + "ver=" + cc_tagVersion + "&aID=" + cc_accountID + "&mkt=" + cc_marketID +"&ref=" + escape(document.referrer);
		var cc_imageUrl = cc_protocol + "://" + cc_subdomain + ".overture.com/images/cc/cc.gif" + cc_queryStr;
		var cc_imageObject = new Image();
		cc_imageObject.src = cc_imageUrl;
		// -->
		</SCRIPT>
	</efundraising:Content>
</efundraising:MASTERPAGE>
<contentpanel:PagePanelControl id="PagePanelControl1" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\SampleKitConfirmation.aspx"></contentpanel:PagePanelControl>
