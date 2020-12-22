<%@ Control Language="vb" AutoEventWireup="false" Codebehind="Roles.ascx.vb" Inherits="StoreFront.StoreFront.Roles" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<asp:DataGrid id="dlRoles" DataKeyField="UID" ShowHeader="true" AllowPaging="True" GridLines="None"
	AutoGenerateColumns="False" runat="server" BorderWidth="0px" CellPadding="0" Width="50%" PageSize="10">
	<PagerStyle HorizontalAlign="Right" CssClass="ContentTableHeader" Mode="NumericPages"></PagerStyle>
	<Columns>
		<asp:TemplateColumn>
			<HeaderTemplate>
				<table border="0" cellpadding="0" cellspacing="0" width="100%">
					<tr>
						<td class="contenttableheader" width="1"><img src="../images/clear.gif" width="1"></td>
						<td class="Contenttableheader" width="70%" nowrap>&nbsp;Role Name</td>
						<td class="Contenttableheader" width="30%" nowrap>&nbsp;Action</td>
						<td class="contenttableheader" width="1"><img src="../images/clear.gif" width="1"></td>
					</tr>
				</table>
			</HeaderTemplate>
			<ItemTemplate>
				<table border="0" cellpadding="0" cellspacing="0" width="100%">
					<tr>
						<td class="contenttable" width="1"><img src="../images/clear.gif" width="1"></td>
						<td class="Content" width="70%">&nbsp;<%# Databinder.Eval(container.dataitem,"RoleName")%></td>
						<td class="Content" width="30%">
							<asp:LinkButton ID=edit Runat=server commandname='<%# container.itemindex%>' OnClick=EditRole Visible='<%# iif(Databinder.Eval(container.dataitem,"IsSuper"), false, true)%>' >
								<asp:image ID="imag1" Runat="server" AlternateText="Edit" ImageUrl="../images/icon_edit.gif"></asp:image>
							</asp:LinkButton>&nbsp;&nbsp;
							<asp:LinkButton id="delete" Runat =server commandname='<%# container.itemindex%>' OnClick=DeleteRole Visible='<%# iif(Databinder.Eval(container.dataitem,"IsSuper"), false, true)%>'>
								<asp:image ID="Image1" Runat="server" AlternateText="Delete" ImageUrl="../images/icon_delete.gif"></asp:image>
							</asp:LinkButton>
						</td>
						<td class="contenttable" width="1"><img src="../images/clear.gif" width="1"></td>
					</tr>
					<tr>
						<td class="contenttableheader" width="1" height="5"><img src="../images/clear.gif" width="1"></td>
						<td class="Content" colspan="2" nowrap></td>
						<td class="contenttableheader" width="1"><img src="../images/clear.gif" width="1"></td>
					</tr>
					<tr>
						<td class="contenttable" colspan="4"><img src="../images/clear.gif" width="1"></td>
					</tr>
				</table>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
</asp:DataGrid><BR>
<asp:LinkButton id="cmdAddNew" runat="server">
	<asp:image ID="img1" Runat="server" ImageUrl="../images/Add_New.jpg"></asp:image>
</asp:LinkButton>
