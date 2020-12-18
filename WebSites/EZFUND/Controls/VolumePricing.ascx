<%@ Control Language="vb" AutoEventWireup="false" Codebehind="VolumePricing.ascx.vb" Inherits="StoreFront.StoreFront.VolumePricing" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<TABLE cellSpacing="0" cellPadding="0" border="0" align="left" runat="server">
	<TR>
		<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
		<TD class="ContentTableHeader">&nbsp;</TD>
		<TD class="ContentTableHeader" noWrap><asp:Label ID="lblVolumePricing" Runat="server"></asp:Label></TD>
		<TD class="ContentTableHeader">&nbsp;</TD>
		<td class="ContentTableHeader" width="1"><img src="images/clear.gif" width="1"></td>
	</TR>
	<TR vAlign="top" class="Content">
		<TD colSpan="5" class="Content"><asp:datagrid id="DataGrid1" GridLines="None" runat="server" BorderWidth="1px" Width="100%" CellPadding="0"
				ShowHeader="False" AutoGenerateColumns="False" PageSize="15">
				<Columns>
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:Label Runat="server" ID="Label1" NAME="Label1" CssClass="Content">&nbsp;Buy </asp:Label>
							<asp:Label Text='<%# container.dataItem("BreakLevel") %>' Runat="server" ID="Label2" CssClass="Content">
							</asp:Label>
							<asp:Label Runat="server" ID="Label3" NAME="Label3" CssClass="Content"> or more and pay <%#container.dataItem("VPrice") %> each (save <%# container.dataItem("Amount") %>)&nbsp;</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:datagrid></TD>
	</TR>
</TABLE>
