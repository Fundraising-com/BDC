<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="TrackingForOrderAddress.ascx.vb" Inherits="StoreFront.StoreFront.TrackingForOrderAddress" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<asp:datalist id="DataList1" Width="100%" runat="server">
	<ItemTemplate>
<br><br>
		<TABLE cellSpacing="0" cellPadding="0" width="100%">
			<TR>
				<TD class="Content" width="15%">&nbsp</TD>
				<td>
					<TABLE cellSpacing="0" cellPadding="0" width="75%">
						<TR>
							<TD class="Content" width="100%">
								<cc1:DynamicCartDisplay id=DynamicCartDisplay1 runat="server" datasource='<%# DataBinder.Eval(Container.DataItem,"OrderItems") %>' QuantityLabel="" ProductLabel="Product(s) In Shipment:" BorderClass="ContentTable" DesignCount="2" StatusColumnDisplay="False" TotalColumnDisplay="False" HorizontalClass="ContentTable" PriceColumnDisplay="False" OptionsColumnDisplay="False" HeadingClass="ContentTableHeader">
								</cc1:DynamicCartDisplay></TD>
						</TR>
					</table>
					<br>
					<TABLE cellSpacing="0" cellPadding="0" width="75%">
						<TR>
							<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
							<TD class="ContentTableHeader" colspan=1 width="100%">Tracking Details:
							</TD>
							<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						</TR>
						<TR>
							<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
							<TD class="Content" colSpan="1">&nbsp;</TD>
							<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						</TR>
						<TR>
							<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
							<TD class="Content" colSpan="1"><%# DataBinder.Eval(Container.DataItem,"TrackingMessageToDisplay") %></TD>
							<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						</TR>
						<TR>
							<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
							<TD class="Content" colSpan="1">&nbsp;</TD>
							<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						</TR>
						<TR>
							<TD class="ContentTable" colSpan="3" height="1"><IMG height="1" src="images/clear.gif"></TD>
						</TR>
					</TABLE>
				</td>
			</tr>
			<TR>
				<TD class="Content" colSpan="2">&nbsp;</TD>
			</TR>
			<TR>
				<TD class="ContentTable" colSpan="2" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
		</table>
	</ItemTemplate>
</asp:datalist>
