<%@ Control Language="vb" AutoEventWireup="false" Codebehind="AdminLists.ascx.vb" Inherits="StoreFront.StoreFront.AdminLists" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<asp:datagrid id="DataGrid1" Runat="server" PageSize="10" GridLines="None" CellSpacing="0" CellPadding="0"
	DataKeyField="UID" Showheader="True" AutoGenerateColumns="False" AllowPaging="True" Width=100%>
	<PagerStyle HorizontalAlign="Right" CssClass="ContentTableHeader" Mode="NumericPages"></PagerStyle>
	<Columns>
		<asp:TemplateColumn>
			<HeaderTemplate>
				<table border="0" cellpadding="0" cellspacing="0" width="100%">
					<tr>
						<td class="contenttableheader" width="1"><img src="../images/clear.gif"></td>
						<td class="Contenttableheader" width="25%">User Name</td>
						<td class="Contenttableheader" width="25%">First Name</td>
						<td class="Contenttableheader" width="25%">Last Name</td>
						<td class="Contenttableheader" width="10%">Locked</td>
						<td class="Contenttableheader" width="15%">Action</td>
						<td class="contenttableheader" width="1"><img src="../images/clear.gif"></td>
					</tr>
					<tr>
						<td class="contenttableheader" width="1" colspan="6" height="1"><img src="../images/clear.gif" width="1"></td>
					</tr>
				</table>
			</HeaderTemplate>
			<ItemTemplate>
				<table border="0" cellpadding="0" cellspacing="0" width="100%">
					<tr>
						<td class="contenttableheader" width="1"><img src="../images/clear.gif" width="1"></td>
						<td class="Content" width="25%"><%# Databinder.Eval(container.dataitem,"UserName")%></td>
						<td class="Content" width="25%"><%# Databinder.Eval(container.dataitem,"FirstName")%></td>
						<td class="Content" width="25%"><%# Databinder.Eval(container.dataitem,"LastName")%></td>
						<td class="Content" width="10%"><asp:Label Runat=server ForeColor="red" text="X" Visible=<%# Databinder.Eval(container.dataitem,"IsLocked")%>></asp:Label></td>
						<td class="Content" width="15%">
							<asp:LinkButton ID=edit Runat=server commandname='<%# container.itemindex%>' OnClick=EditAdmin >
								<asp:image ID="imag1" Runat="server" AlternateText="Edit" ImageUrl="../images/icon_edit.gif"></asp:image>
							</asp:LinkButton>&nbsp;&nbsp;
							<asp:LinkButton id="delete" Runat =server commandname='<%# container.itemindex%>' OnClick=DeleteAdmin Visible='<%# iif(Databinder.Eval(container.dataitem,"IsSuperUser"), false, true)%>'>
								<asp:image ID="Image1" Runat="server" AlternateText="Delete" ImageUrl="../images/icon_delete.gif"></asp:image>
							</asp:LinkButton>
						</td>
						<td class="contenttableheader" width="1"><img src="../images/clear.gif" width="1"></td>
					</tr>
					<tr>
						<td class="contenttableheader" width="1" colspan="6" height="1"><img src="../images/clear.gif" width="1"></td>
					</tr>
				</table>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
</asp:datagrid>
<p ><asp:linkbutton id="cmdAddNew" runat="server">
	<asp:image ID="img1" Runat="server" ImageUrl="../images/Add_New.jpg"></asp:image>
</asp:linkbutton>
</p>