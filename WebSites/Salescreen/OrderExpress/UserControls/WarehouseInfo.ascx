<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.WarehouseInfo" Codebehind="WarehouseInfo.ascx.cs" %>

<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td>
			<table border="0" cellpadding="o" cellspacing="0" width="100%">
				<TR>
					<td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
					<td><br>
						<TABLE cellSpacing="0" cellPadding="1" width="500" border="0">
							<TR>
								<TD><asp:label id="Label2" runat="server" CssClass="StandardLabel">Warehouse&nbsp;ID&nbsp;:&nbsp;</asp:label></TD>
								<TD width="100%" colspan="3"><asp:label id="lblWarehouseID" runat="server" CssClass="DescInfoLabel"></asp:label></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label5" runat="server" CssClass="StandardLabel">EDS&nbsp;Warehouse&nbsp;#&nbsp;:&nbsp;</asp:label></TD>
								<TD width="100%" colspan="3"><asp:label id="lblFulfWarehouseID" runat="server" CssClass="DescInfoLabel"></asp:label></TD>
							</TR>
							<tr>
								<td>
									<asp:label id="Label17" runat="server" CssClass="StandardLabel">
										Warehouse&nbsp;Status:&nbsp;
									</asp:label>
								</td>
								<td width="100%" colspan="3">
									<asp:label id="lblWarehouseStatusColor" runat="server" Width="5px" BorderColor="Black" BorderStyle="Solid"
										BorderWidth="1px" Height="3px" BackColor="White">
									&nbsp;&nbsp;
									</asp:label>&nbsp;
									<asp:label id="lblWarehouseStatus" runat="server" CssClass="DescInfoLabel">New Warehouse</asp:label>
								</td>
							</tr>
							<TR>
								<TD><asp:label id="Label1" runat="server" CssClass="StandardLabel">Warehouse&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
								<TD width="100%" colspan="3"><asp:label id="lblWarehouseName" runat="server" CssClass="DescInfoLabel"></asp:label></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label8" runat="server" CssClass="StandardLabel">Vendor&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
								<TD width="100%" colspan="3"><asp:label id="lblVendorName" runat="server" CssClass="DescInfoLabel"></asp:label></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label4" runat="server" CssClass="StandardLabel">Company&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
								<TD width="100%" colspan="3"><asp:label id="lblCompanyName" runat="server" CssClass="DescInfoLabel"></asp:label></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label3" runat="server" CssClass="StandardLabel">First&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
								<TD><asp:label id="lblFirstName" runat="server" CssClass="DescInfoLabel"></asp:label></TD>
								<TD><asp:label id="Label9" runat="server" CssClass="StandardLabel">&nbsp;Last&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
								<TD>
									<asp:label id="lblLastName" runat="server" CssClass="DescInfoLabel"></asp:label>
								</TD>
							<TR>
								<TD><asp:label id="Label11" runat="server" CssClass="StandardLabel">Address&nbsp;Line&nbsp;1&nbsp;:&nbsp;</asp:label></TD>
								<TD colspan="3"><asp:label id="lblAddressLine1" runat="server" CssClass="DescInfoLabel"></asp:label></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label13" runat="server" CssClass="StandardLabel">Address&nbsp;Line&nbsp;2&nbsp;:&nbsp;</asp:label></TD>
								<TD colspan="3"><asp:label id="lblAddressLine2" runat="server" CssClass="DescInfoLabel"></asp:label></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label24" runat="server" CssClass="StandardLabel">City&nbsp;:&nbsp;</asp:label></TD>
								<td><asp:label id="lblCity" runat="server" CssClass="DescInfoLabel"></asp:label></td>
								<TD><asp:label id="Label36" runat="server" CssClass="StandardLabel">&nbsp;County&nbsp;:&nbsp;</asp:label></TD>
								<TD><asp:label id="lblCounty" runat="server" CssClass="DescInfoLabel"></asp:label></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label40" runat="server" CssClass="StandardLabel">State&nbsp;:&nbsp;</asp:label></TD>
								<TD><asp:label id="lblState" runat="server" CssClass="DescInfoLabel" Width="100px"></asp:label></TD>
								<TD><asp:label id="Label44" runat="server" CssClass="StandardLabel">&nbsp;Zip&nbsp;Code&nbsp;:&nbsp;</asp:label></TD>
								<TD><asp:label id="lblZip" runat="server" CssClass="DescInfoLabel" Width="100px"></asp:label></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label46" runat="server" CssClass="StandardLabel">Phone&nbsp;Number&nbsp;:&nbsp;</asp:label></TD>
								<TD><asp:label id="lblPhoneNumber" runat="server" CssClass="DescInfoLabel" Width="150px"></asp:label></TD>
								<TD><asp:label id="Label48" runat="server" CssClass="StandardLabel">&nbsp;Fax&nbsp;Number&nbsp;:&nbsp;</asp:label></TD>
								<TD><asp:label id="lblFaxNumber" runat="server" CssClass="DescInfoLabel" Width="150px"></asp:label></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label6" runat="server" CssClass="StandardLabel">Receiving&nbsp;Phone&nbsp;:&nbsp;</asp:label></TD>
								<TD colspan="3"><asp:label id="lblReceivingPhoneNumber" runat="server" CssClass="DescInfoLabel"></asp:label></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label7" runat="server" CssClass="StandardLabel">Email&nbsp;Address&nbsp;:&nbsp;</asp:label></TD>
								<TD colspan="3"><asp:label id="lblEmailAddress" runat="server" CssClass="DescInfoLabel"></asp:label></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label10" runat="server" CssClass="StandardLabel">Pick&nbsp;Up:&nbsp;</asp:label></TD>
								<TD colspan="3"><asp:checkbox id="chkPickUp" runat="server" CssClass="DescInfoLabel" Enabled="False"></asp:checkbox></TD>
							</TR>
						</TABLE>
					</td>
				</TR>
			</table>
			<br>
			<br>
		</td>
	</tr>
</table>
