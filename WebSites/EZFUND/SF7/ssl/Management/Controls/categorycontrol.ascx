<%@ Control Language="vb" AutoEventWireup="false" Codebehind="categorycontrol.ascx.vb" Inherits="StoreFront.StoreFront.categorycontrol" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<%
'@BEGINVERSIONINFO

'@APPVERSION: 7.0.0

'@STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'@ENDCOPYRIGHT

'@ENDVERSIONINFO
%>
<table cellSpacing="0" cellPadding="0" width="100%" class="content">
	<tr id="trEditRow">
		<td class="ContentTableHeader" align="left" colSpan="10">&nbsp;Categories<span id="CategoryControl1_lblCustomerHeader" class="ContentTableHeader"></span></td>
	</tr>
	<tr>
		<td>
			<asp:DataGrid id="DataGrid1" runat="server" ShowHeader="False" AutoGenerateColumns="False" AllowPaging="True" Width="100%" ItemStyle-Width="100%" BorderWidth="0px" CellPadding="0" GridLines="Horizontal">
				<ItemStyle Width="100%"></ItemStyle>
				<PagerStyle HorizontalAlign="Right" CssClass="ContentTableHeader" Mode="NumericPages"></PagerStyle>
				<Columns>
					<asp:TemplateColumn>
						<ItemTemplate>
							<table width="100%" cellpadding="0" cellspacing="0" border="0">
								<input type="hidden"  id="txtParentLevelHidden" runat="server" value='<%# Databinder.Eval(Container.DataItem, "Level")%>' NAME="txtParentLevelHidden">
								<input type="hidden" id="txtParentIDHidden" runat="server" value='<%# Databinder.Eval(Container.DataItem, "ParentID")%>' NAME="txtParentIDHidden">
								<tr>
									<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
									<TD class="Content" width="1">&nbsp;</TD>
									<td class="content" noWrap align="left">&nbsp;<b>
											<asp:LinkButton Font-Underline=False ID="lnkExpander" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID") %>' Runat="server">
											</asp:LinkButton>
											<asp:label id="lblAttName" Runat="server" CssClass="content">
												<%# DataBinder.Eval(Container.DataItem,"Name") %>
											</asp:label></b></td>
									<td class="content" noWrap align="left">&nbsp;</td>
									<td class="content" vAlign="bottom" noWrap align="right">
										<asp:LinkButton ID="cmdAdd" Runat="server" OnClick="cmdAdd_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID") %>' CommandName='0'>
											<asp:Image BorderWidth="0" ID="imgAdd" runat="server" ImageUrl="../images/icon_add.gif" AlternateText="Add"></asp:Image>
										</asp:LinkButton>
										&nbsp;
										<asp:LinkButton ID="cmdEdit" Runat="server" OnClick="cmdEdit_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID") %>' CommandName='0'>
											<asp:Image BorderWidth="0" ID="imgEdit" runat="server" ImageUrl="../images/icon_edit.gif" AlternateText="Edit"></asp:Image>
										</asp:LinkButton>
										&nbsp;
										<asp:LinkButton ID="cmdDelete" Runat="server" OnClick="cmdDelete_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID") %>' CommandName='0'>
											<asp:Image BorderWidth="0" ID="imgDelete" runat="server" ImageUrl="../images/icon_delete.gif" AlternateText="Delete"></asp:Image>
										</asp:LinkButton>
										&nbsp;</td>
									<TD class="Content" width="1">&nbsp;</TD>
									<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
								</tr>
								<tr>
									<TD class="ContentTable" width="1" colspan="7"><IMG src="images/clear.gif" width="1"></TD>
								</tr>
							</table>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>
		</td>
	</tr>
</table>
