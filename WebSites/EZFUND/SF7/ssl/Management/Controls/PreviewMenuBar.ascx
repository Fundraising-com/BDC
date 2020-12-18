<%@ Register TagPrefix="uc1" TagName="LivePerson" Src="../../Controls/LivePerson.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CartList" Src="../../Controls/CartList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SimpleSearch" Src="../../Controls/SimpleSearch.ascx" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="PreviewMenuBar.ascx.vb" Inherits="StoreFront.StoreFront.PreviewMenuBar" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<asp:DataList id="dlMenu" runat="server" RepeatDirection="Horizontal" ItemStyle-Wrap="False" CellPadding="0"
	BorderWidth="0px">
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
				<td id="Stuff2" style="Width=100%" runat="server">
					<uc1:SimpleSearch id="SimpleSearch1" runat="server"></uc1:SimpleSearch>
					<uc1:CartList id="CartList1" runat="server"></uc1:CartList>
					<uc1:LivePerson id="LivePerson1" runat="server" visible="false"></uc1:LivePerson>
					<asp:Label id="PageLink" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DisplayName") %>'>
					</asp:Label>
					<asp:Label id="PageLink2" runat="server">
						<!--SFEXpress-->
						<img border="0" src= '<%# imgpath%>' alt='<%# DataBinder.Eval(Container.DataItem, "DisplayName") %>'>
					</asp:Label></td>
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
