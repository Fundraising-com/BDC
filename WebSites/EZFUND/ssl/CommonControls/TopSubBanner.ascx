<%@ Register TagPrefix="uc1" TagName="CMenubar" Src="../Controls/CMenubar.ascx" %>
<%@ Control Language="vb" EnableViewState = False AutoEventWireup="false" Codebehind="TopSubBanner.ascx.vb" Inherits="StoreFront.StoreFront.TopSubBanner" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<div align="center" class="TopSubBanner">
	<asp:Label id="Label" runat="server" CssClass="TopSubBanner" Visible="false">PageName</asp:Label>
	<uc1:CMenubar id="CMenubar1" runat="server" Callpage="top"></uc1:CMenubar>
	<asp:Image id="PageImage" runat="server" Visible="False"></asp:Image>
</div>
