<%@ Import Namespace = "StoreFront.SystemBase"%>
<%@ Register TagPrefix="uc1" TagName="CMenubar" Src="../Controls/CMenubar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CartList" Src="../Controls/CartList.ascx" %>
<%@ Control Language="vb" EnableViewState = False AutoEventWireup="false" Codebehind="RightColumnNav.ascx.vb" Inherits="StoreFront.StoreFront.RightColumnNav" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<%@ Register TagPrefix="uc1" TagName="SimpleSearch" Src="../Controls/SimpleSearch.ascx" %>
<div class="rc-wrap">
	<div class="rc-nav">
		<uc1:CMenubar id="CMenubar1" runat="server"></uc1:CMenubar>
	</div>
</div>