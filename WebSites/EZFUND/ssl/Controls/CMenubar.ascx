<%@ Register TagPrefix="uc1" TagName="LivePerson" Src="LivePerson.ascx" %>
<%@ Control Language="vb" EnableViewState=False AutoEventWireup="false" Codebehind="CMenubar.ascx.vb" Inherits="StoreFront.StoreFront.CMenubar" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:DataList id="dlMenu" runat="server" RepeatDirection="Horizontal" ItemStyle-Wrap="False" CellPadding="0" BorderWidth="0px">
	<ItemStyle Wrap="False"></ItemStyle>
	<ItemTemplate>
		<table border="0" cellpadding="0" cellspacing="0" runat="server" id="TableMenu" width="100%">
			<tr id="TopBar">
				<td id="TopBarCell" height="1" colspan="5"><img src="images/clear.gif" height="1" width="1"></td>
			</tr>
			<tr id="TopBarSpacer">
				<td id="Column11" class="MenuBorder" width="1"><img src="images/clear.gif" height="1" width="1"></td>
				<td id="Stuff4" colspan="3" height="5"><img src="images/clear.gif" height="5" width="1"></td>
				<td id="Column21" class="MenuBorder" width="1"><img src="images/clear.gif" height="1" width="1"></td>
			</tr>
			<tr>
				<td id="LeftBar" class="MenuBorder" width="1"><img src="images/clear.gif" height="1" width="1"></td>
				<td id="Stuff" width="5" style="width:5px;"><img src="images/clear.gif" height="1" width="5"></td>
				<td id="Stuff2" nowrap style="Width=100%">
					<uc1:LivePerson id="LivePerson1" runat="server" visible="false"></uc1:LivePerson>
					<asp:HyperLink id="PageLink" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "Link") %>'>
						<%# DataBinder.Eval(Container.DataItem, "DisplayName") %>
					</asp:HyperLink>
					<asp:HyperLink id="PageLink2" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "Link") %>'>
					<img border="0" src= '<%# imgpath%>'>
					</asp:HyperLink></td>
				<td id="Stuff3" width="5" style="width:5px;"><img src="images/clear.gif" height="1" width="5"></td>
				<td id="Column22" class="MenuBorder" width="1"><img src="images/clear.gif" height="1" width="1"></td>
			</tr>
			<tr id="BottomBarSpacer">
				<td id="Column13" class="MenuBorder" width="1"><img src="images/clear.gif" height="1" width="1"></td>
				<td id="Stuff5" colspan="3" height="5"><img src="images/clear.gif" height="5" width="1"></td>
				<td id="Column23" class="MenuBorder" width="1"><img src="images/clear.gif" height="1" width="1"></td>
			</tr>
			<tr id="Row5">
				<td id="BottomBarCell" class="MenuBorder" height="1" colspan="5"><img src="images/clear.gif" height="1" width="1"></td>
			</tr>
		</table>
	</ItemTemplate>
</asp:DataList>
