<!-- begin: JDB - 4/2/2007 - UrlRewriter Add-On -->
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="MappedURLsControl.ascx.vb" Inherits="StoreFront.StoreFront.MappedURLsControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<table border="0" cellpadding="4" cellspacing="0" width="100%">
	<tr>
		<td>
			<asp:datagrid id="dgMappedURLs" runat="server" AutoGenerateColumns="False" Width="100%" BorderWidth="0px"
				CellPadding="5" DataKeyField="uid">
				<PagerStyle HorizontalAlign="Right" Position="TopAndBottom" CssClass="Content" Wrap="False" Mode="NumericPages"></PagerStyle>
				<Columns>
					<asp:TemplateColumn HeaderStyle-CssClass="ContentTableHeader" ItemStyle-CssClass="Content" ItemStyle-Wrap="True"
						HeaderText="Mapped URLs" HeaderStyle-Width="90%" ItemStyle-Width="90%" ItemStyle-VerticalAlign="Top" ItemStyle-BorderColor="#ACB8B8"
						ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1">
						<ItemTemplate>
							<asp:label id="lblBaseURL" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BaseURL") %>'>
							</asp:label>
							<br>< maps to ><br>
							<asp:label id="lblRewrittenURL" runat="server" Text='<%# GetMapsToForDisplay(DataBinder.Eval(Container.DataItem, "RewrittenURL")) %>'>
							</asp:label>
						</ItemTemplate>
						<EditItemTemplate>
							<table cellpadding="0" cellspacing="0">
								<tr>
									<td class="Content"><%=VirtualDirectory%></td>
									<td class="Content"><asp:TextBox cssclass="Content" ID="txtBaseURL" Runat="server" Text='<%# GetURLForEdit(DataBinder.Eval(Container.DataItem, "BaseURL")) %>' Width="300px"></asp:TextBox></td>
									<td class="Content"><%=FileExt%></td>
								</tr>
							</table>
							<asp:RequiredFieldValidator ControlToValidate="txtBaseURL" ErrorMessage="Enter a Base URL." ID="rfvBaseURL" Runat="server"></asp:RequiredFieldValidator>
							<br>< maps to ><br>
							<table cellpadding="0" cellspacing="0" width="100%">
								<tr>
									<td class="Content" width="16%"><asp:RadioButton cssclass="Content" ID="rbProduct" Runat="server" GroupName='<%# DataBinder.Eval(Container.DataItem, "uid") %>'></asp:RadioButton>&nbsp;Product Detail&nbsp;</td>
									<td class="Content"><asp:DropDownList cssclass="Content" ID="ddlProduct" Runat="server"></asp:DropDownList></td>
								</tr>
								<tr>
									<td class="Content"><asp:RadioButton cssclass="Content" ID="rbCategory" Runat="server" GroupName='<%# DataBinder.Eval(Container.DataItem, "uid") %>'></asp:RadioButton>&nbsp;Category Search&nbsp;</td>
									<td class="Content"><asp:DropDownList cssclass="Content" ID="ddlCategory" Runat="server"></asp:DropDownList></td>
								</tr>
								<tr>
									<td class="Content"><asp:RadioButton cssclass="Content" ID="rbCustom" Runat="server" GroupName='<%# DataBinder.Eval(Container.DataItem, "uid") %>'></asp:RadioButton>&nbsp;Custom Search&nbsp;</td>
									<td class="Content"><asp:TextBox cssclass="Content" ID="txtCustom" Runat="server" Width="250px"></asp:TextBox></td>
								</tr>
							</table>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderStyle-CssClass="ContentTableHeader" ItemStyle-CssClass="Content" ItemStyle-Wrap="True"
						HeaderText="Actions" HeaderStyle-Width="10%" ItemStyle-Width="10%" ItemStyle-VerticalAlign="Top"
						ItemStyle-HorizontalAlign="Center" ItemStyle-BorderColor="#ACB8B8" ItemStyle-BorderStyle="Solid"
						ItemStyle-BorderWidth="1">
						<ItemTemplate>
							<asp:LinkButton CausesValidation="False" ID="btnEdit" Runat="server" CommandName='Edit'>
								<asp:Image BorderWidth="0" ID="imgEdit" runat="server" ImageUrl="../images/edit.jpg" AlternateText="Edit"></asp:Image>
							</asp:LinkButton>
							<br>
							<asp:LinkButton CausesValidation="False" ID="btnDelete" Runat="server" CommandName='Delete'>
								<asp:Image BorderWidth="0" ID="imgDelete" runat="server" ImageUrl="../images/delete.jpg" AlternateText="Delete"></asp:Image>
							</asp:LinkButton>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:LinkButton ID="btnSave" Runat="server" CommandName='Save'>
								<asp:Image BorderWidth="0" ID="imgSave" runat="server" ImageUrl="../images/save.jpg" AlternateText="Save"></asp:Image>
							</asp:LinkButton>
							<br>
							<asp:LinkButton CausesValidation="False" ID="btnCancel" Runat="server" CommandName='Cancel'>
								<asp:Image BorderWidth="0" ID="imgCancel" runat="server" ImageUrl="../images/cancel.jpg" AlternateText="Cancel Edit"></asp:Image>
							</asp:LinkButton>
						</EditItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:datagrid>
		</td>
	</tr>
	<tr>
		<td align="right">
			<asp:linkbutton CausesValidation="False" id="btnAdd" Runat="server">
				<asp:Image BorderWidth="0" ID="imgAdd" runat="server" ImageUrl="../images/add.jpg" AlternateText="Add"></asp:Image>
			</asp:linkbutton>
		</td>
	</tr>
</table>
<!-- end: JDB - 4/2/2007 - UrlRewriter Add-On -->
