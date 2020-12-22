<%@ Control Language="vb" AutoEventWireup="false" Codebehind="NavObjects.ascx.vb" Inherits="StoreFront.StoreFront.NavObjects" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<%@ Register TagPrefix="uc1" TagName="SFExpressUploadControl" Src="SFExpressUploadControl.ascx"%>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%">
	<TR>
		<TD class="ContentTable" colSpan="4" height="1"><IMG src="images/clear.gif"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD align="center" colSpan="2">
			<table width="100%" border="0" cellpadding="0" cellspacing="0" align="center" class="content">
				<tr valign="top">
					<td width="35%" class="ContentTableSubHeader">
						&nbsp;Links/Control
					</td>
					<td width="35%" class="ContentTableSubHeader" style="display:none;">
						Image
					</td>
					<td width="20%" class="ContentTableSubHeader">Order
					</td>
					<td width="10%" class="ContentTableSubHeader">
						Actions
					</td>
				</tr>
			</table>
		</TD>
		<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD align="center" colSpan="2"><asp:datalist id="dlNavObjects" HorizontalAlign="Center" Width="100%" runat="server" BackColor="White"
				CellSpacing="0" CellPadding="0" BorderWidth="0">
				<HeaderTemplate>
				</HeaderTemplate>
				<ItemStyle CssClass="Content"></ItemStyle>
				<AlternatingItemStyle CssClass="AlternatingContent" BackColor="#F0F0F0"></AlternatingItemStyle>
				<ItemTemplate>
					<table cellpadding="0" width="100%" cellspacing="0" align="center" border="0">
						<tr valign="middle">
							<td width="35%">&nbsp;&nbsp;
								<asp:DropDownList id="ddlLink" runat="server" Width="155" OnSelectedIndexChanged="ddlLinkChanged"
									AutoPostBack="True"></asp:DropDownList>
								<asp:TextBox id="txtMenuText" Visible="False" Columns="15" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MenuText") %>'>
								</asp:TextBox>
							</td>
							<td width="35%" style="display:none;">
								<asp:TextBox id="txtImage" runat="server" Columns="15" Text='<%# DataBinder.Eval(Container.DataItem, "MenuImage") %>'>
								</asp:TextBox>&nbsp;&nbsp;&nbsp;
								<asp:ImageButton ID="btnBrowse" OnClick="UploadImage" ImageUrl="../Images/icon_browse.gif" Runat="server"></asp:ImageButton>
							</td>
							<td width="20%">
								<asp:imagebutton id="cmdUp" runat="server" ImageUrl="../images/up.gif" CommandName="MoveUp"></asp:imagebutton>
								<br>
								<asp:imagebutton id="cmdDown" runat="server" ImageUrl="../images/down.gif" CommandName="MoveDown"></asp:imagebutton>
							</td>
							<td width="10%">
								<asp:imagebutton id="cmdDelete" runat="server" ImageUrl="../images/icon_delete.gif" CommandName="Delete"></asp:imagebutton>
							</td>
						</tr>
						<tr>
							<td colspan="4">
								<uc1:SFExpressUploadControl id="ucUploadImage" Visible="False" runat="server"></uc1:SFExpressUploadControl></td>
						</tr>
					</table>
				</ItemTemplate>
			</asp:datalist>&nbsp;
		</TD>
		<TD class="ContentTable" width="1"><IMG src="images/clear.gif"></TD>
	</TR>
	<TR>
		<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="ContentTableSubHeader" colSpan="2" height="1">&nbsp;Add New Item</TD>
		<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>	
	<TR>
		<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD align="center" colSpan="2">
			<table width="100%" border="0" cellpadding="0" cellspacing="0" align="center" class="content">
				<tr valign="middle" height="40px">
					<td width="35%">&nbsp;&nbsp;&nbsp;
						<asp:dropdownlist id="ddlLinkType" runat="server" Width="155"></asp:dropdownlist>
					</td>
					<td width="35%" style="display:none;">
						<asp:TextBox id="txtImageName" runat="server" Columns="15"></asp:TextBox>&nbsp;
					</td>
					<td width="20%">&nbsp;
					</td>
					<td width="10%">
						<asp:imagebutton id="cmdAdd" runat="server" ImageUrl="../images/icon_add.gif"></asp:imagebutton>
					</td>
				</tr>
			</table>
		</TD>
		<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTable" colSpan="4" height="1"><IMG height="1" src="images/clear.gif"></TD>
	</TR>
</TABLE>
