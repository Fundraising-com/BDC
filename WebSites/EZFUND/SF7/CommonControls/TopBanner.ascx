<%@ Import Namespace = "StoreFront.SystemBase"%>
<%@ Register TagPrefix="uc1" TagName="CartList" Src="../Controls/CartList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CMenubar" Src="../Controls/CMenubar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SimpleSearch" Src="../Controls/SimpleSearch.ascx" %>
<%@ Control ClassName="TopBanner" Language="vb" EnableViewState="False" AutoEventWireup="false" Codebehind="TopBanner.ascx.vb" Inherits="StoreFront.StoreFront.TopBanner" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>

<div class="logo">
	<a href="<%=StoreFrontConfiguration.SiteURL%>">
		<asp:Label id="StoreName" runat="server" CssClass="TopBanner" visible="false">StoreName</asp:Label>
		<asp:Image id="StoreImage" runat="server" Visible="False"></asp:Image>
	</a>
</div>

