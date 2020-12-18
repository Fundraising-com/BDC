<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.CampaignInfo" Codebehind="CampaignInfo.ascx.cs" %>

<table id="Table3" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td><br>
		</td>
	</tr>
	<tr>
		<td>
			<TABLE cellSpacing="0" cellPadding="2" border="0">
				<TR>
					<TD noWrap width="160">
						<asp:label id="lblLabelCampID" runat="server" CssClass="StandardLabel">Campaign ID :</asp:label></TD>
					<TD noWrap width="440">
						<asp:Label id="lblCampID" runat="server" CssClass="DescLabel" Width="100px" Enabled="False"></asp:Label></TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="lblLabelTypeID" runat="server" CssClass="StandardLabel">Type :</asp:label></TD>
					<TD>
						<TABLE cellSpacing="0" cellPadding="0" width="400" border="0">
							<TR>
								<TD>
									<asp:Label id="lblType" runat="server" CssClass="DescLabel" Width="200px"></asp:Label>
								</TD>
								<TD width="100%">&nbsp;
								</TD>
								<TD>
									<asp:label id="Label28" runat="server" CssClass="StandardLabel">Fiscal&nbsp;Year&nbsp;:&nbsp;</asp:label></TD>
								<TD>
									<asp:Label id="lblFiscalYear" runat="server" CssClass="DescLabel" Width="100px"></asp:Label>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="Label16" runat="server" CssClass="StandardLabel">Name :</asp:label></TD>
					<TD>
						<table border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td>
									<asp:Label id="lblName" runat="server" CssClass="DescLabel" Width="400px"></asp:Label>
								</td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="Label27" runat="server" CssClass="StandardLabel">Start Date :</asp:label></TD>
					<TD>
						<TABLE cellSpacing="0" cellPadding="0" width="400" border="0">
							<TR>
								<TD>
									<asp:Label id="lblStartDate" runat="server" CssClass="DescLabel" Width="100px"></asp:Label>
								</TD>
								<TD width="100%">&nbsp;
								</TD>
								<TD>
									<asp:label id="Label29" runat="server" CssClass="StandardLabel">End&nbsp;Date&nbsp;:&nbsp;</asp:label></TD>
								<TD>
									<asp:Label id="lblEndDate" runat="server" CssClass="DescLabel" Width="100px"></asp:Label>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="Label7" runat="server" CssClass="StandardLabel">Account :</asp:label></TD>
					<TD>
						<TABLE cellSpacing="0" cellPadding="0" width="400" border="0">
							<TR>
								<TD>
									<asp:Label id="lblAccountID" runat="server" CssClass="DescLabel" Width="50px"></asp:Label>
								<TD>
								<TD>&nbsp;
									<asp:Label id="lblFULFAccountID" CssClass="DescLabel" runat="server" Width="230px"></asp:Label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="lblLabelFM" runat="server" CssClass="StandardLabel">Field Sales Manager :</asp:label></TD>
					<TD>
						<TABLE cellSpacing="0" cellPadding="0" width="400" border="0">
							<TR>
								<TD>
									<asp:Label id="lblFMID" runat="server" CssClass="DescLabel" Width="50px"></asp:Label>
								<TD>&nbsp;
									<asp:Label id="lblFMName" runat="server" CssClass="DescLabel" Width="230px"></asp:Label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="lblLabelTaxExemptionNumber" runat="server" CssClass="StandardLabel">Tax Exemption Number :</asp:label></TD>
					<TD>
						<TABLE cellSpacing="0" cellPadding="0" width="400" border="0">
							<TR>
								<TD>
									<asp:Label id="lblTaxExemptionNumber" CssClass="DescLabel" runat="server" MaxLength="25" Width="200px"></asp:Label></TD>
								<TD width="100%">&nbsp;
								</TD>
								<TD>
									<asp:label id="Label23" runat="server" CssClass="StandardLabel">Expire&nbsp;:&nbsp;</asp:label></TD>
								<TD>
									<asp:Label id="lblTaxExemptionExpirationDate" CssClass="DescLabel" runat="server" MaxLength="10"
										Width="100px"></asp:Label>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label30" runat="server" CssClass="StandardLabel">Warehouse :</asp:Label></TD>
					<TD>
						<asp:Label id="lblWarehouse" runat="server" CssClass="DescLabel" Width="100px"></asp:Label>
					</TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="lblLabelEnrollment" runat="server" CssClass="StandardLabel">Enrollment :</asp:Label></TD>
					<TD>
						<asp:Label id="lblEnrollment" runat="server" CssClass="DescLabel" Width="100px"></asp:Label>
					</TD>
				</TR>
				<TR>
					<TD valign="top">
						<asp:label id="lblLabelComments" runat="server" CssClass="StandardLabel">Comments :</asp:label></TD>
					<TD>
						<asp:Label id="lblComments" runat="server" CssClass="DescLabel" Width="400px"></asp:Label></TD>
				</TR>
			</TABLE>
		</td>
	</tr>
</table>
