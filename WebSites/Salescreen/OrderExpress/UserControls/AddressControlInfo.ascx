<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.AddressControlInfo" Codebehind="AddressControlInfo.ascx.cs" %>

<table id="tblFormDetailTitle" cellSpacing="3" cellPadding="0" width="300" border="0" class="DescTableInfo">
	<tr align="left">
		<td>
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR id="htmlTblRowTitleAddress" runat="server">
					<td colspan="2" align="left" style="height: 38px">
						<table border="0" cellpadding="0" cellspacing="0" class="DescTableInfo">
							<tr >
								<TD align="left">
									<asp:Label id="lblTitleItemNo" runat="server" CssClass="AddressInfoStandardLabel"></asp:Label>
								</TD>
							</tr>
						</table>
					</td>
				</TR>
				<TR class="ItemStyle">
					<TD align="left">
						<asp:label id="lblLabelOrgName" CssClass="AddressInfoStandardLabel" runat="server">Org.&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
					<TD align="left">
						<asp:Label id="lblOrgName" runat="server" CssClass="AddressInfoDescLabel"></asp:Label>
					</TD>
				</TR>
				<TR id="htmlTblRowTypeAddress" runat="server" class="ItemStyle">
					<TD align="left">
						<asp:label id="Label1" CssClass="AddressInfoStandardLabel" runat="server">Type&nbsp;:&nbsp;</asp:label></TD>
					<TD align="left">
						<asp:Label id="lblType" runat="server" CssClass="AddressInfoDescLabel"></asp:Label>
					</TD>
				</TR>
				<TR class="AlternatingItemStyle">
					<TD align="left">
						<asp:label id="lblLabelFirstName" CssClass="AddressInfoStandardLabel" runat="server">First&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
					<TD align="left">
						<asp:Label id="lblFirstName" runat="server" CssClass="AddressInfoDescLabel"></asp:Label>
					</TD>
				</TR>
				<TR class="ItemStyle" align="left">
					<TD>
						<asp:label id="lblLabelLastName" CssClass="AddressInfoStandardLabel" runat="server">Last&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
					<TD>
						<asp:Label id="lblLastName" runat="server" CssClass="AddressInfoDescLabel"></asp:Label>
					</TD>
				</TR>
				<TR class="AlternatingItemStyle">
					<TD vAlign="top">
						<asp:label id="lblLabelAddressLine1" CssClass="AddressInfoStandardLabel" runat="server">Address&nbsp;Line&nbsp;1&nbsp;:&nbsp;</asp:label></TD>
					<TD>
						<asp:Label id="lblAddressLine1" runat="server" CssClass="AddressInfoDescLabel"></asp:Label>
					</TD>
				</TR>
				<TR class="ItemStyle">
					<TD vAlign="top">
						<asp:label id="lblLabelAddressLine2" CssClass="AddressInfoStandardLabel" runat="server">Address&nbsp;Line&nbsp;2&nbsp;:&nbsp;</asp:label></TD>
					<TD>
						<asp:Label id="lblAddressLine2" runat="server" CssClass="AddressInfoDescLabel"></asp:Label>
					</TD>
				</TR>
				<TR class="AlternatingItemStyle">
					<TD>
						<asp:label id="lblLabelCity" CssClass="AddressInfoStandardLabel" runat="server">City&nbsp;:&nbsp;</asp:label>
					</TD>
					<TD valign="top">
						<table border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td>
									<asp:Label id="lblCity" CssClass="AddressInfoDescLabel" runat="server" Width="100px"></asp:Label>
								</td>
								<TD>
									<asp:label id="lblLabelCounty" CssClass="AddressInfoStandardLabel" runat="server">&nbsp;County&nbsp;:&nbsp;</asp:label></TD>
								<TD>
									<asp:Label id="lblCounty" CssClass="AddressInfoDescLabel" runat="server"></asp:Label>
								</TD>
							</tr>
						</table>
					</TD>
				</TR>
				<TR class="ItemStyle">
					<TD>
						<asp:label id="lblLabelState" CssClass="AddressInfoStandardLabel" runat="server">State&nbsp;:&nbsp;</asp:label>
					</TD>
					<TD vAlign="top">
						<table border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td>
									<asp:Label id="lblState" CssClass="AddressInfoDescLabel" Width="100px" runat="server"></asp:Label>
								</td>
								<td>
									<asp:label id="lblLabelZip" CssClass="AddressInfoStandardLabel" runat="server">&nbsp;Zip&nbsp;Code&nbsp;:&nbsp;</asp:label></td>
								<TD>
									<asp:Label id="lblZip" CssClass="AddressInfoDescLabel" runat="server"></asp:Label>
								</TD>
							</tr>
						</table>
					</TD>
				</TR>
				<tr class="AlternatingItemStyle">
					<TD><asp:label id="lblLabelBillingPhoneNumber" CssClass="AddressInfoStandardLabel" runat="server">Phone&nbsp;Number&nbsp;:&nbsp;</asp:label></TD>
					<TD>
						<table id="Table6" cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td><asp:label id="lblPhoneNumber" CssClass="AddressInfoDescLabel" runat="server" Width="100px"></asp:label></td>
								<TD><asp:label id="lblLabelFaxNumber" CssClass="AddressInfoStandardLabel" runat="server">&nbsp;Fax&nbsp;Number&nbsp;:&nbsp;</asp:label></TD>
								<TD><asp:label id="lblFaxNumber" CssClass="AddressInfoDescLabel" runat="server" Width="90px"></asp:label></TD>
							</tr>
						</table>
					</TD>
				</tr>
				<TR class="ItemStyle">
					<TD><asp:label id="lblLabelEmailAddress" CssClass="AddressInfoStandardLabel" runat="server">Email&nbsp;Address&nbsp;:&nbsp;</asp:label></TD>
					<TD><asp:label id="lblEmailAddress" CssClass="AddressInfoDescLabel" runat="server"></asp:label></TD>
				</TR>
				<tr class="AlternatingItemStyle">
					<td>
						<asp:label id="Label2" CssClass="AddressInfoStandardLabel" runat="server">Residential&nbsp;Area&nbsp;:&nbsp;</asp:label>
					</td>
					<td>
						<asp:CheckBox id="chkBoxResidentialArea" BorderColor="black" Enabled="False" CssClass="AddressInfoStandardLabel"
							runat="server"></asp:CheckBox>
					</td>
				</tr>
			</TABLE>
		</td>
	</tr>
	<tr>
		<td align="center"></td>
	</tr>
</table>
