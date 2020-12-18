<%@ Control Language="vb" AutoEventWireup="false" Codebehind="attTemplates.ascx.vb" Inherits="StoreFront.StoreFront.attTemplates" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<%@ Register TagPrefix="uc1" TagName="attMainctrl" Src="attMainctrl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="attdetailctrl" Src="attdetailctrl.ascx" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" runat="server">
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="ContentTableHeader" width="20%">&nbsp;Attribute</TD>
		<TD class="ContentTableHeader" width="20%">&nbsp;&nbsp;Price</TD>
		<TD class="ContentTableHeader" width="20%">&nbsp;&nbsp;Weight</TD>
		<TD class="ContentTableHeader" id="Cell1" width="20%">&nbsp;Download</TD>
		<TD class="ContentTableHeader" width="20%">&nbsp;</TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="content" align="middle" colSpan="5"><asp:datalist id="DLAttributes" ShowFooter="False" ShowHeader="False" Runat="server" Width="100%">
				<ItemTemplate>
					<table cellSpacing="0" cellPadding="0" width="100%">
						<tr>
							<td class="content" colSpan="5">&nbsp;</td>
						</tr>
						<tr>
							<td class="content" noWrap align="left" width="20%">&nbsp;<b>
									<asp:LinkButton ID="cmdDrill" Runat="server" CommandArgument ='<%# DataBinder.Eval(Container.DataItem,"Name") %>' OnClick="HideDetails">
										<asp:Image BorderWidth="0" ID="imgDrill" runat="server" ImageUrl="../images/icon_add.gif" AlternateText="Drill"></asp:Image>
									</asp:LinkButton>
									<asp:label id="lblAttName" Runat="server" CssClass="content">
										<%# DataBinder.Eval(Container.DataItem,"Name") %>
									</asp:label></b>
								<asp:TextBox ID=atttype Runat =server CssClass =content Visible =False Text='<%# DataBinder.Eval(Container.DataItem,"AttributeType") %>'>
								</asp:TextBox></td>
							<td class="content" noWrap width="20%">&nbsp;</td>
							<td class="content" noWrap width="20%">&nbsp;</td>
							<td class="content" noWrap align="right" width="100%">
								<asp:LinkButton ID="cmdAddDetail" Runat="server" OnClick="AddDetail" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"uid") %>'>
									<asp:Image BorderWidth="0" ID="imgAddDetail" runat="server" ImageUrl="../images/add_new.jpg" AlternateText="AddDetail"></asp:Image>
								</asp:LinkButton>
								&nbsp;&nbsp;</td>
							<td class="content" vAlign="bottom" noWrap align="right" width="20%">
								<asp:LinkButton ID="cmdEdit" Runat="server" OnClick =EditAttribute CommandArgument='<%# DataBinder.Eval(Container.DataItem,"uid") %>' CommandName='0'>
									<asp:Image BorderWidth="0" ID="imgEdit" runat="server" ImageUrl="../images/icon_edit.gif" AlternateText="Edit"></asp:Image>
								</asp:LinkButton>
								&nbsp;
								<asp:LinkButton ID="cmdDelete" Runat="server" OnClick =DeleteAttribute CommandArgument='<%# DataBinder.Eval(Container.DataItem,"uid") %>' CommandName='0'>
									<asp:Image BorderWidth="0" ID="imgDelete" runat="server" ImageUrl="../images/icon_delete.gif" AlternateText="Delete"></asp:Image>
								</asp:LinkButton>
								&nbsp;</td>
						</tr>
						<asp:Panel ID="pnlDetails" Runat="server" Visible="False" Width="100%">
							<tr>
								<td class="content" colSpan="5">
									<asp:TextBox ID='AttName' Runat =server Visible =false Text='<%# DataBinder.Eval(Container.DataItem,"Name") %>'>
									</asp:TextBox>
									<asp:DataList ID="dlAttributeDetail" Runat="server" Width="100%">
										<ItemTemplate>
											<table cellSpacing="0" cellPadding="0" width="100%">
												<tr>
													<td class="content" noWrap align="left" width="20%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;...
														<asp:label id="lblAttDetail" Runat="server" CssClass="content">
															<%# DataBinder.Eval(Container.DataItem,"Name") %>
														</asp:label></td>
													<td class="content" noWrap align="left" width="20%">&nbsp;&nbsp;
														<asp:label id="lblPrice" Runat="server" CssClass="content">
															<%# DataBinder.Eval(Container.DataItem,"CartPrice") %>
														</asp:label></td>
													<td class="content" noWrap align="left" width="20%">&nbsp;&nbsp;
														<asp:label id="lblWeight" Runat="server" CssClass="content">
															<%# DataBinder.Eval(Container.DataItem,"CartWeight") %>
														</asp:label></td>
													<td class="content" noWrap align="left" width="20%">&nbsp;&nbsp;
														<asp:label id="lblFile" Runat="server" CssClass="content"></asp:label><%# DataBinder.Eval(Container.DataItem,"FilePath") %></td>
													<td class="content" vAlign="bottom" noWrap align="right" width="20%">
														<asp:LinkButton ID="cmdEditDetail" Runat="server" OnClick =EditAttribute CommandArgument='<%# DataBinder.Eval(Container.DataItem,"uid") %>' CommandName='1'>
															<asp:Image BorderWidth="0" ID="imgEditDetail" runat="server" ImageUrl="../images/icon_edit.gif" AlternateText="Edit Detail"></asp:Image>
														</asp:LinkButton>
														&nbsp;
														<asp:LinkButton ID="cmdDeleteDetail" Runat="server" OnClick =DeleteAttribute CommandArgument='<%# DataBinder.Eval(Container.DataItem,"uid") %>' CommandName='1'>
															<asp:Image BorderWidth="0" ID="imgDeleteDetail" runat="server" ImageUrl="../images/icon_delete.gif" AlternateText="Delete Detail"></asp:Image>
														</asp:LinkButton>
														&nbsp;</td>
												</tr>
											</table>
										</ItemTemplate>
									</asp:DataList>
								</td>
							</tr>
						</asp:Panel>
						<TR>
						</TR>
					</table>
				</ItemTemplate>
			</asp:datalist></TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<tr>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<td class="content" colSpan="5">&nbsp;</td>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</tr>
	<TR id="NewAtt" runat="server">
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<td class="content" align="middle" colspan="5">
			<asp:label id="Label1" CssClass="content" Runat="server">Attribute Name:</asp:label>&nbsp;
			<asp:textbox id="txtAttName" CssClass="content" Runat="server" MaxLength="50"></asp:textbox>&nbsp;
			<asp:dropdownlist id="ddType" CssClass="content" Runat="server">
				<asp:ListItem Value="0">Merchant Defined</asp:ListItem>
				<asp:ListItem Value="1">Customer Defined</asp:ListItem>
			</asp:dropdownlist>&nbsp;
			<asp:LinkButton ID="btnAdd" Runat="server">
				<asp:Image BorderWidth="0" ID="imgAdd" runat="server" ImageUrl="../images/add_new.jpg" AlternateText="Add"></asp:Image>
			</asp:LinkButton>
		</td>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<tr>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<td class="content" colSpan="5">&nbsp;</td>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</tr>
	<TR>
		<TD class="ContentTable" colSpan="7" height="1"><IMG height="1" src="images/clear.gif"></TD>
	</TR>
</TABLE>
<uc1:attdetailctrl id="Attdetailctrl1" runat="server"></uc1:attdetailctrl>
<uc1:attMainctrl id="AttMainctrl1" runat="server"></uc1:attMainctrl>
