<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Control ClassName="LeftColumnNav1" Language="vb" EnableViewState="False" AutoEventWireup="false" Codebehind="LeftColumnNav.ascx.vb" Inherits="StoreFront.StoreFront.LeftColumnNav1" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<%@ Register TagPrefix="uc1" TagName="SimpleSearch" Src="../../Controls/SimpleSearch.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CMenubar" Src="../Controls/CMenubar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CartList" Src="../../Controls/CartList.ascx" %>
<%@ Import Namespace = "StoreFront.SystemBase"%>
<asp:panel id="merchanttools" runat="server">
	<uc1:cmenubar id="CMenubar1" runat="server" CallPage="left"></uc1:cmenubar>
</asp:panel>
