<%@ Register TagPrefix="uc1" TagName="LivePerson" Src="LivePerson.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Navigator" Src="../Controls/TextNavigator1.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CartList" Src="CartList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SimpleSearch" Src="SimpleSearch.ascx" %>
<%@ Control Language="vb" EnableViewState="False" AutoEventWireup="false" Codebehind="CMenubar.ascx.vb" Inherits="StoreFront.StoreFront.CMenubar" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%--
<asp:DataList id="dlMenu" runat="server" RepeatDirection="Horizontal" ItemStyle-Wrap="False" CellPadding="0" BorderWidth="0px" CssClass="menu">
--%>
<ul id="MenuUL" runat="server">
<asp:Repeater ID="dlMenu" Runat="server">
	<ItemTemplate>
		<li>
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
		</li>
	</ItemTemplate>
</asp:Repeater>
</ul>
<%--
</asp:DataList>
--%>