<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.PostalAddressInfo" Codebehind="PostalAddressInfo.ascx.cs" %>

<table id="tblFormDetailTitle" cellSpacing="0" cellPadding="0" width="300" border="0">
	<tr>
		<td><asp:datalist id=dtLstAddress runat="server" DataKeyField="postal_address_entity_id" width="300px" DataSource="<%# DVAddress %>">
				<SeparatorTemplate>
					<hr width="100%" size="1px" color="#003366">
				</SeparatorTemplate>
				<ItemTemplate>
					<TABLE cellSpacing="0" cellPadding="1" width="300px" border="0" class="DescTableInfo"  bgcolor="White">
						<TR id="htmlTblRowTitleAddress" runat="server">
							<td colspan="2">
								<table border="0" cellpadding="0" cellspacing="0" bgcolor="#993300" class="DescTableInfo">
									<tr>
										<TD valign=top>
											<asp:Label id="lblTitleItemNo"  runat="server" Font-Size="xx-small" ForeColor=White CssClass="StandardLabel" Text='<%# "Shipping Address&nbsp;#&nbsp;" + (Convert.ToInt32(DataBinder.Eval(Container, "ItemIndex")) + 1) + "&nbsp;" %>'>
											</asp:Label>
										</TD>
									</tr>
								</table>
							</td>
						</TR>
						<TR id="htmlTblRowTypeAddress" runat="server">
							<TD>
								<asp:label id="Label1" CssClass="StandardLabel" Font-Size="xx-small" runat="server">Type&nbsp;:&nbsp;</asp:label></TD>
							<TD>
								<asp:Label id="lblType" runat="server" CssClass="DescLabel" Font-Size="xx-small" Text='<%# DataBinder.Eval(Container, "DataItem.postal_address_type_name") %>'>
								</asp:Label>
							</TD>
						</TR>
						<TR>
							<TD>
								<asp:label id="lblLabelLastName" CssClass="StandardLabel" Font-Size="xx-small" runat="server">First&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
							<TD>
								<asp:Label id="lblLastName" runat="server" CssClass="DescLabel" Font-Size="xx-small" Text='<%# DataBinder.Eval(Container, "DataItem.First_Name") %>' >
								</asp:Label>
							</TD>
						</TR>
						<TR>
							<TD>
								<asp:label id="lblLabelFirstName" CssClass="StandardLabel" Font-Size="xx-small" runat="server">Last&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
							<TD>
								<asp:Label id="lblFirstName" runat="server" CssClass="DescLabel" Font-Size="xx-small" Text='<%# DataBinder.Eval(Container, "DataItem.Last_Name") %>' >
								</asp:Label>
							</TD>
						</TR>
						<TR>
							<TD vAlign="top">
								<asp:label id="lblLabelAddressLine1" CssClass="StandardLabel" Font-Size="xx-small" runat="server">Address&nbsp;Line&nbsp;1&nbsp;:&nbsp;</asp:label></TD>
							<TD>
								<asp:Label id="lblAddressLine1" runat="server" CssClass="DescLabel" Font-Size="xx-small" Text='<%# DataBinder.Eval(Container, "DataItem.Address1") %>' >
								</asp:Label>
							</TD>
						</TR>
						<TR>
							<TD vAlign="top">
								<asp:label id="lblLabelAddressLine2" CssClass="StandardLabel" Font-Size="xx-small" runat="server">Address&nbsp;Line&nbsp;2&nbsp;:&nbsp;</asp:label></TD>
							<TD>
								<asp:Label id="lblAddressLine2" runat="server" CssClass="DescLabel" Font-Size="xx-small" Text='<%# DataBinder.Eval(Container, "DataItem.Address2") %>' >
								</asp:Label>
							</TD>
						</TR>
						<TR>
							<TD>
								<asp:label id="lblLabelCity" CssClass="StandardLabel" Font-Size="xx-small" runat="server">City&nbsp;:&nbsp;</asp:label>
							</TD>
							<TD>
								<table border="0" cellpadding="0" cellspacing="0">
									<tr>
										<td>
											<asp:Label id="lblCity" CssClass="DescLabel" Font-Size="xx-small" Text='<%# DataBinder.Eval(Container, "DataItem.City") %>' runat="server" Width="100px">
											</asp:Label>
										</td>
										<TD>
											<asp:label id="lblLabelCounty" CssClass="StandardLabel" Font-Size="xx-small" runat="server">&nbsp;County&nbsp;:&nbsp;</asp:label></TD>
										<TD>
											<asp:Label id="lblCounty" CssClass="DescLabel" runat="server" Font-Size="xx-small" Text='<%# DataBinder.Eval(Container, "DataItem.county") %>' >
											</asp:Label>
										</TD>
									</tr>
								</table>
							</TD>
						</TR>
						<TR>
							<TD>
								<asp:label id="lblLabelState" CssClass="StandardLabel" Font-Size="xx-small" runat="server">State&nbsp;:&nbsp;</asp:label>
							</TD>
							<TD vAlign="top">
								<table border="0" cellpadding="0" cellspacing="0">
									<tr>
										<td>
											<asp:Label id="lblState" CssClass="DescLabel" Width="100px" runat="server" Font-Size="xx-small" Text='<%# DataBinder.Eval(Container, "DataItem.subdivision_name_1") %>' >
											</asp:Label>
										</td>
										<td>
											<asp:label id="lblLabelZip" CssClass="StandardLabel" Font-Size="xx-small" runat="server">&nbsp;Zip&nbsp;Code&nbsp;:&nbsp;</asp:label></td>
										<TD>
											<asp:Label id="lblZip" CssClass="DescLabel" Font-Size="xx-small" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Zip") %>' >
											</asp:Label>
										</TD>
									</tr>
								</table>
							</TD>
						</TR>
					</TABLE>
				</ItemTemplate>
			</asp:datalist>
		</td>
	</tr>
	<tr>
		<td align="center"></td>
	</tr>
</table>
