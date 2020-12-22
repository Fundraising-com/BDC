<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.FMInfo" Codebehind="FMInfo.ascx.cs" %>

<table id="Table3" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<TD noWrap width="160">
			<asp:label id="lblLabelFMID" runat="server" CssClass="StandardLabel">FSM ID :</asp:label>
		</TD>
		<TD noWrap width="440">
			<asp:Label id="lblFMID" runat="server" CssClass="DescLabel" Width="100px" Enabled="False" />
		</TD>
	</tr>
	<TR>
		<TD noWrap width="160">
			<asp:label id="lblLabelMGRFMID" runat="server" CssClass="StandardLabel">Manager FSM ID :</asp:label>
		</TD>
		<TD noWrap width="440">
			<asp:Label id="lblMGRFMID" runat="server" CssClass="DescLabel" Width="100px" Enabled="False" />
		</TD>
	</TR>
	<TR>
		<TD>
			<asp:label id="lblLabelFName" runat="server" CssClass="StandardLabel">First Name :</asp:label></TD>
		<TD>
			<table cellSpacing="0" cellPadding="0" width="400" border="0">
				<tr>
					<td>
						<asp:Label id="lblFName" runat="server" CssClass="DescLabel" Width="200px" />
					</td>
					<td width="100%">&nbsp;</td>
					<td>
						<asp:label id="lblLabelLName" runat="server" CssClass="StandardLabel">Last&nbsp;Name&nbsp;:</asp:label>
					</td>
					<td>
						<asp:Label id="lblLName" runat="server" CssClass="DescLabel" Width="100px" />
					</td>
				</tr>
			</table>
		</TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="lblLabelAddress" runat="server" CssClass="StandardLabel">Address :</asp:Label>
		</TD>
		<TD>
			<asp:Label id="lblAddress" runat="server" CssClass="DescLabel" Width="100px" />
		</TD>
	</TR>
	<TR>
		<TD>
			<asp:label id="lblLabelCity" runat="server" CssClass="StandardLabel">City :</asp:label></TD>
		<TD>
			<TABLE cellSpacing="0" cellPadding="0" width="400" border="0">
				<TR>
					<TD>
						<asp:Label id="lblCity" runat="server" CssClass="DescLabel" Width="100px" />
					</TD>
					<TD width="100%">&nbsp;</TD>
					<TD>
						<asp:label id="lblLabelState" runat="server" CssClass="StandardLabel">State&nbsp;/&nbsp;Province&nbsp;:&nbsp;</asp:label></TD>
					<TD>
						<asp:Label id="lblState" runat="server" CssClass="DescLabel" Width="100px" />
					</TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="lblLabelPostalCode" runat="server" CssClass="StandardLabel">Zip / Postal Code :</asp:Label>
		</TD>
		<TD>
			<asp:Label id="lblPostalCode" runat="server" CssClass="DescLabel" Width="100px" />
		</TD>
	</TR>
	<tr>
		<td colspan="2">
			<asp:Label id="lbPhoneInformation" runat="server" CssClass="StandardLabel">Phone Information :</asp:Label>
		</td>
	</tr>
	<tr>
		<td>
			<asp:Label ID="lbLabelVoiceMailExt" Runat="server" CssClass="StandardLabel">Meridian Voice Mail ext</asp:Label>
		</td>
		<td>
			<TABLE cellSpacing="0" cellPadding="0" width="400" border="0">
				<TR>
					<td>
						<asp:Label ID="lbVoiceMailExt" Runat="server" CssClass="DescLabel" />
					</td>
					<td>
						<asp:Label ID="lbLabelWorkPH" Runat="server" CssClass="StandardLabel">Work</asp:Label>
					</td>
					<td>
						<asp:Label ID="lbWorkPH" Runat="server" CssClass="DescLabel" />
					</td>
				</TR>
			</TABLE>
		</td>
	</tr>
	<tr>
		<td>
			<asp:Label ID="lbLabelMobilePH" Runat="server" CssClass="StandardLabel">Mobile Phone</asp:Label>
		</td>
		<td>
			<TABLE cellSpacing="0" cellPadding="0" width="400" border="0">
				<TR>
					<td>
						<asp:Label ID="lbMobilePH" Runat="server" CssClass="DescLabel" />
					</td>
					<td>
						<asp:Label ID="lbLabelFaxPH" Runat="server" CssClass="StandardLabel">Fax</asp:Label>
					</td>
					<td>
						<asp:Label ID="lbFaxPH" Runat="server" CssClass="DescLabel" />
					</td>
				</TR>
			</TABLE>
		</td>
	</tr>
	<tr>
		<td>
			<asp:Label ID="lbLabelPagerPH" Runat="server" CssClass="StandardLabel">Pager</asp:Label>
		</td>
		<td>
			<TABLE cellSpacing="0" cellPadding="0" width="400" border="0">
				<TR>
					<td>
						<asp:Label ID="lbPagerPH" Runat="server" CssClass="DescLabel"></asp:Label>
					</td>
					<td>
						<asp:Label ID="lbLabelTollFreePH" Runat="server" CssClass="StandardLabel">Toll Free #</asp:Label>
					</td>
					<td>
						<asp:Label ID="lbTollFreePH" Runat="server" CssClass="DescLabel"></asp:Label>
					</td>
				</TR>
			</TABLE>
		</td>
	</tr>
	<tr>
		<td>
			<asp:Label ID="lbLabelCorporateEM" Runat="server" CssClass="StandardLabel">Corporate EMail</asp:Label>
		</td>
		<td>
			<TABLE cellSpacing="0" cellPadding="0" width="400" border="0">
				<TR>
					<td>
						<asp:HyperLink ID="hlCorporateEM" Runat="server" CssClass="DescLabel" style='COLOR: blue; TEXT-DECORATION: none' />
						<asp:Label ID="lbCorporateEM_none" Runat="server" CssClass="DescLabel" Text="none" />
					</td>
					<td>
						<asp:Label ID="lbLabelPersonalEM" Runat="server" CssClass="StandardLabel">Personal Email</asp:Label>
					</td>
					<td>
						<asp:HyperLink ID="hlPersonalEM" Runat="server" CssClass="DescLabel" style='COLOR: blue; TEXT-DECORATION: none' />
						<asp:Label ID="lbPersonalEM_none" Runat="server" CssClass="DescLabel" Text="none" />
					</td>
				</TR>
			</TABLE>
		</td>
	</tr>
</table>
