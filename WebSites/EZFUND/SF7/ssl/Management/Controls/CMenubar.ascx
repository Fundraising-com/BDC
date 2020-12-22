<%@ Register TagPrefix="uc1" TagName="LivePerson" Src="../../Controls/LivePerson.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Navigator" Src="../../Controls/TextNavigator1.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CartList" Src="../../Controls/CartList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SimpleSearch" Src="../../Controls/SimpleSearch.ascx" %>
<%@ Control ClassName="CMenuBar1" Language="vb" EnableViewState="False" AutoEventWireup="false" Codebehind="CMenubar.ascx.vb" Inherits="StoreFront.StoreFront.CMenubar1" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:DataList id="dlMenu" runat="server" RepeatDirection="Horizontal" ItemStyle-Wrap="False" CellPadding="0" BorderWidth="0px" CssClass="menu">
	<ItemStyle Wrap="False"></ItemStyle>
	<ItemTemplate>
		<table border="0" cellpadding="0" cellspacing="0" runat="server" id="TableMenu" width="100%">
			<tr id="TopBar" visible="false">
				<td id="TopBarCell" height="0" colspan="5"></td>
			</tr>
			<tr id="TopBarSpacer" visible="false">
				<td id="Column11" class="MenuBorder" width="0"></td>
				<td id="TopBarTD" colspan="3" height="0"></td>
				<td id="Column12" class="MenuBorder" width="0"></td>
			</tr>
			<tr>
				<td id="Column21" class="MenuBorder" width="0" visible="false"></td>
				<td id="LeftSpacerTD" width="0" visible="false"></td>
				<td id="ContentTD" nowrap>
					<%-- BEGIN: GJV - 8/22/2007 - OSP merge --%>
					<uc1:SimpleSearch id="SimpleSearch1" runat="server"></uc1:SimpleSearch>
					<uc1:CartList id="CartList1" runat="server"></uc1:CartList>
					<uc1:Navigator id="Navigator1" runat="server"></uc1:Navigator>
					<%-- END: GJV - 8/22/2007 - OSP merge --%>
					<uc1:LivePerson id="LivePerson1" runat="server" visible="false"></uc1:LivePerson>
					<asp:HyperLink id="PageLink" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "Link") %>'><%# DataBinder.Eval(Container.DataItem, "DisplayName") %></asp:HyperLink>
					<asp:HyperLink id="PageLink2" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "Link") %>'>
						<%-- BEGIN: GJV - 8/22/2007 - OSP merge --%>
						<!--SFEXpress-->
						<img border="0" src= '<%# imgpath%>' alt='<%# DataBinder.Eval(Container.DataItem, "DisplayName") %>'>
						<%-- END: GJV - 8/22/2007 - OSP merge --%>
					</asp:HyperLink>
				</td>
				<td id="RightSpacerTD" width="0" visible="false"></td>
				<td id="Column22" class="MenuBorder" width="0" visible="false"></td>
			</tr>
			<tr id="BottomBarSpacer" visible="false">
				<td id="Column13" class="MenuBorder" width="0"></td>
				<td id="BottomBarTD" colspan="3" height="0"></td>
				<td id="Column23" class="MenuBorder" width="0"></td>
			</tr>
			<tr id="BottomBar" visible="false">
				<td id="BottomBarCell" class="MenuBorder" height="0" colspan="5"></td>
			</tr>
		</table>
	</ItemTemplate>
</asp:DataList>