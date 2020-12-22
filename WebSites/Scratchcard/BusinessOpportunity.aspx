<%@ Register TagPrefix="uc1" TagName="BusinessOpportunity" Src="Components/User/Controls/Sections/BusinessOpportunity.ascx" %>
<%@ Page language="c#" Codebehind="BusinessOpportunity.aspx.cs" AutoEventWireup="false" Inherits="GA.BDC.WEB.ScratchcardWeb.BusinessOpportunity" %>
<%@ Register TagPrefix="contentpanel" Namespace="GA.BDC.Core.Web.UI.UIControls" Assembly="GA.BDC.Core.Web.UI.UIControls" %>
<%@ Register TagPrefix="efundraising" Namespace="GA.BDC.Core.Web.UI.MasterPages" Assembly="GA.BDC.Core.Web.UI.MasterPages" %>
<efundraising:MASTERPAGE id="MasterPage1" runat="server" master="~/MasterPage/SiteTemplate1.ascx">
	<efundraising:Content id="Content1" runat="server" ContentPlaceHolderID="cph_PageContent">
		<uc1:BusinessOpportunity id="BusinessOpportunity1" runat="server"></uc1:BusinessOpportunity>
	</efundraising:Content>
</efundraising:MASTERPAGE>
