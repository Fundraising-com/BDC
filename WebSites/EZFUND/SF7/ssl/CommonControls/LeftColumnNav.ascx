<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Control ClassName="LeftColumnNav" Language="vb" EnableViewState = False AutoEventWireup="false" Codebehind="LeftColumnNav.ascx.vb" Inherits="StoreFront.StoreFront.LeftColumnNav" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<%@ Register TagPrefix="uc1" TagName="SimpleSearch" Src="../Controls/SimpleSearch.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CMenubar" Src="../Controls/CMenubar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CartList" Src="../Controls/CartList.ascx" %>
<%@ Import Namespace = "StoreFront.SystemBase"%>
<asp:panel id="custommenu" runat="server">
	<DIV class="lc-head">
		<H2>Browse by Category</H2>
	</DIV>
	<DIV class="lc-nav">
		<uc1:cmenubar id="CMenubar2" runat="server" CallPage="left"></uc1:cmenubar></DIV>
</asp:panel>
