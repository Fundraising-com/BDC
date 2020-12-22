<%@ Control Language="vb" AutoEventWireup="false" Codebehind="employeecontrol.ascx.vb" Inherits="StoreFront.StoreFront.employeecontrol" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
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
<table class="content" cellSpacing="0" cellPadding="0" width="100%">
	<TR>
		<td><asp:datagrid id="DataGrid1" runat="server" ShowHeader="True" AutoGenerateColumns="False" AllowPaging="True"
				Width="100%" ItemStyle-Width="100%" BorderWidth="0px" CellPadding="0" GridLines="Horizontal">
				<ItemStyle Width="100%"></ItemStyle>
				<Columns>
					<asp:TemplateColumn>
						<HeaderTemplate>
							<table width="100%" cellpadding="0" cellspacing="0" border="0">
								<tr class="ContentTableHeader" align="left">
									<td width="93%">&nbsp;User Name</td>
									<td width="7%">Action</td>
								</tr>
							</table>
						</HeaderTemplate>
						<ItemTemplate>
							<table width="100%" cellpadding="0" cellspacing="0" border="0">
								<input type="hidden" id="txtParentLevelHidden" runat="server" NAME="txtParentLevelHidden">
								<input type="hidden" id="txtParentIDHidden" runat="server" NAME="txtParentIDHidden">
								<tr>
									<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
									<TD class="Content" width="1">&nbsp;</TD>
									<td class="content" noWrap align="left">
										<asp:LinkButton Font-Underline=False ID="lnkExpander" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"uid") %>' Runat="server">
										</asp:LinkButton>
										<asp:Label id="lblUserName" runat="server" Width="136px">
											&nbsp;<%# Container.DataItem("UserName") %>
										</asp:Label>
									<td class="content" vAlign="bottom" noWrap align="right">
										<asp:LinkButton ID="cmdEdit" Runat="server" OnClick="cmdEdit_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"uid") %>' CommandName='0'>
											<asp:Image BorderWidth="0" ID="imgEdit" runat="server" ImageUrl="../images/icon_edit.gif" AlternateText="Edit"></asp:Image>
										</asp:LinkButton>
										&nbsp;
										<asp:LinkButton ID="cmdDelete" Runat="server" OnClick="cmdDelete_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"uid") %>' CommandName='0'>
											<asp:Image BorderWidth="0" ID="imgDelete" runat="server" ImageUrl="../images/icon_delete.gif"
												AlternateText="Delete"></asp:Image>
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
				<PagerStyle HorizontalAlign="Right" CssClass="ContentTableHeader" Mode="NumericPages"></PagerStyle>
			</asp:datagrid></td>
	</TR>
</table>
