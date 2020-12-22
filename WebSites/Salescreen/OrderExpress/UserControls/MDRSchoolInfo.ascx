<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.MDRSchoolInfo" Codebehind="MDRSchoolInfo.ascx.cs" %>

<table id="Table3" cellSpacing="0" cellPadding="0" border="0">	
	<tr>
		<td>
			<TABLE cellSpacing="0" cellPadding="2" border="0">
				<TR>
					<TD noWrap width="160">
						<asp:label id="lblLabelMdrPID" runat="server" CssClass="StandardLabel">MDR PID :</asp:label>
					</TD>
					<TD noWrap width="440">
						<asp:Label id="lblMdrPID" runat="server" CssClass="DescLabel" Width="100px" Enabled="False"></asp:Label>
					</TD>
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
									&nbsp;
								</TD>
								<TD>
									&nbsp;
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="lblLabelName" runat="server" CssClass="StandardLabel">Name :</asp:label></TD>
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
						<asp:Label id="lblLabelAddress" runat="server" CssClass="StandardLabel">Address :</asp:Label>
					</TD>
					<TD>
						<asp:Label id="lblAddress" runat="server" CssClass="DescLabel" Width="100px"></asp:Label>
					</TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="lblLabelCity" runat="server" CssClass="StandardLabel">City :</asp:label></TD>
					<TD>
						<TABLE cellSpacing="0" cellPadding="0" width="200" border="0">
							<TR>
								<TD>
									<asp:Label id="lblCity" runat="server" CssClass="DescLabel" Width="100px"></asp:Label>
								</TD>
								<TD width="100%">&nbsp;
								</TD>
								<TD>
									<asp:label id="lblLabelState" runat="server" CssClass="StandardLabel">State/Province&nbsp;:</asp:label></TD>
								<TD>
									<asp:Label id="lblState" runat="server" CssClass="DescLabel" Width="100px"></asp:Label>
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
						<asp:Label id="lblPostalCode" runat="server" CssClass="DescLabel" Width="100px"></asp:Label>
					</TD>
				</TR>
			</TABLE>
		</td>
	</tr>
</table>
