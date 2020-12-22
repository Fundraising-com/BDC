<%@ Page language="c#" Codebehind="SampleKit.aspx.cs" AutoEventWireup="false" Inherits="GA.BDC.WEB.ScratchcardWeb.SampleKit" %>
<%@ Register TagPrefix="contentpanel" Namespace="GA.BDC.Core.Web.UI.UIControls" Assembly="GA.BDC.Core.Web.UI.UIControls" %>
<%@ Register TagPrefix="efundraising" Namespace="GA.BDC.Core.Web.UI.MasterPages" Assembly="GA.BDC.Core.Web.UI.MasterPages" %>
<%@ Register TagPrefix="uc1" TagName="SampleKitShort" Src="Components/User/Controls/Sections/SampleKitShort.ascx" %>
<efundraising:MASTERPAGE id="MasterPage1" runat="server" master="~/MasterPage/SiteTemplate1.ascx">
	<efundraising:Content id="Content1" runat="server" ContentPlaceHolderID="cph_PageContent">
		<uc1:SampleKitShort id="SampleKitShort" runat="server"></uc1:SampleKitShort>
	</efundraising:Content>
</efundraising:MASTERPAGE>
