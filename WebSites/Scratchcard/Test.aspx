<%@ Register TagPrefix="efundraising" Namespace="GA.BDC.Core.Web.UI.MasterPages" Assembly="GA.BDC.Core.Web.UI.MasterPages" %>
<%@ Register TagPrefix="contentpanel" Namespace="GA.BDC.Core.Web.UI.UIControls" Assembly="GA.BDC.Core.Web.UI.UIControls" %>
<%@ Page language="c#" Codebehind="Test.cs" AutoEventWireup="true" Inherits="GA.BDC.WEB.ScratchcardWeb.Test" %>
<efundraising:masterpage id="MasterPage1" runat="server" master="~/MasterPage/SiteTemplate1.ascx">
	<EFUNDRAISING:CONTENT id="Content1" runat="server" ContentPlaceHolderID="cph_PageContent">
		<ASP:PLACEHOLDER id="MainPlaceHolder" runat="server"></ASP:PLACEHOLDER>YYYYYYYYYYYYYY
	</EFUNDRAISING:CONTENT>
</efundraising:masterpage>
