<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrganizationHeaderInfo" Codebehind="OrganizationHeaderInfo.ascx.cs" %>

<table id="Table3" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td>
			<TABLE cellSpacing="0" cellPadding="2" border="0">
				<TR align="left">
					<TD noWrap width="160"><asp:label id="lblLabelOrgID" CssClass="StandardLabel" runat="server">QSP&nbsp;Organization&nbsp;ID&nbsp;#&nbsp;:</asp:label></TD>
					<TD noWrap width="440"><asp:label id="lblOrgID" CssClass="DescInfoLabel" runat="server" Width="100px"></asp:label></TD>
				</TR>
				<TR align="left">
					<TD><asp:label id="Label16" CssClass="StandardLabel" runat="server">Name :</asp:label></TD>
					<TD><asp:label id="lblName" CssClass="DescInfoLabel" runat="server" Width="400px"></asp:label></TD>
				</TR>
				<TR align="left">
					<TD><asp:label id="lblLabelOrgTypeID" CssClass="StandardLabel" runat="server">Type :</asp:label></TD>
					<TD>
						<table border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td>
									<asp:label id="lblType" CssClass="DescInfoLabel" runat="server"></asp:label>
								</td>
								<td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<TD><asp:label id="lblLabelOrgLevelID" CssClass="StandardLabel" runat="server">Level :</asp:label></TD>
								<TD><asp:label id="lblLevel" CssClass="DescInfoLabel" runat="server"></asp:label></TD>
							</tr>
						</table>
					</TD>
				</TR>
				<TR align="left">
					<TD><asp:label id="lblLabelTaxExemptionNumber" CssClass="StandardLabel" runat="server">Tax&nbsp;Exemption&nbsp;#&nbsp;:&nbsp;</asp:label></TD>
					<TD>
						<TABLE cellSpacing="0" cellPadding="0" width="400" border="0">
							<TR>
								<TD><asp:label id="lblTaxExemptionNumber" CssClass="DescInfoLabel" runat="server" Width="200px"></asp:label></TD>
								<TD width="100%">&nbsp;
								</TD>
								<TD><asp:label id="Label23" CssClass="StandardLabel" runat="server">Expire&nbsp;:&nbsp;</asp:label></TD>
								<TD><asp:label id="lblTaxExemptionExpirationDate" CssClass="DescInfoLabel" runat="server" Width="100px"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR align="left">
					<TD><asp:label id="lblLabelMDRPID" CssClass="StandardLabel" runat="server">MDR PID :</asp:label></TD>
					<TD><asp:label id="lblMDRPID" CssClass="DescInfoLabel" runat="server" Width="100px"></asp:label></TD>
				</TR>
				<TR align="left">
					<TD vAlign="top"><asp:label id="lblLabelComments" CssClass="StandardLabel" runat="server">Comments :</asp:label></TD>
					<TD><asp:label id="lblComments" CssClass="DescInfoLabel" runat="server" Width="400px"></asp:label></TD>
				</TR>
			</TABLE>
		</td>
	</tr>
</table>