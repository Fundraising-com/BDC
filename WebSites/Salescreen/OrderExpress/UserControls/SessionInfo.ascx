<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.SessionInfo" Codebehind="SessionInfo.ascx.cs" %>
<TABLE BORDER="0" CELLPADDING="0" CELLSPACING="0">
	<TR>
		<TD>
			<TABLE WIDTH="100%" BORDER="0" CELLPADDING="0" CELLSPACING="0" background="images/InfoTab_TopMiddle.gif">
				<TR>
					<TD>
						<asp:image Runat="server" ImageUrl="~/images/InfoTab_TopLeft.gif" WIDTH="10" HEIGHT="20" id="Image1"></asp:image></TD>
					<TD align="left" WIDTH="100%">
						<IMG SRC="images/SessionInfo_Title.gif" HEIGHT="20"></TD>
					<TD>
						<asp:image Runat="server" ImageUrl="~/images/InfoTab_TopRight.gif" WIDTH="10" HEIGHT="20" id="Image2"></asp:image></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD bgcolor="#fdf6ee">
			<table cellpadding="2">
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="0" border="0" id="Table1">
							<%--
							<tr>
								<td>
									<asp:label id="lblCamp" Font-Names="Verdana" Font-Bold="True" runat="server" ForeColor="#053868" font-size="11px">Campaign Name:</asp:label>
								</td>
							</tr>
							<tr>
								<td>
									<asp:label id="lblCampName" Font-Names="Verdana" runat="server" ForeColor="Black" font-size="11px">blah</asp:label>
								</td>
							</tr>
							<tr>
								<td>
									<table border="0" cellpadding="0" cellspacing="0" width="100%">
										<tr>
											<td width="50%">
												<asp:label id="lblLabelStartDate" Font-Names="Verdana" Font-Bold="True" runat="server" ForeColor="#053868" font-size="11px">Start Date:</asp:label>
											</td>
											<td align="right">
												<asp:label id="lblLabelEndDate" Font-Names="Verdana" Font-Bold="True" runat="server" ForeColor="#053868" font-size="11px">End Date:</asp:label>
											</td>
										</tr>
										<tr width="50%">
											<td>
												<asp:label id="lblStartDate" Font-Names="Verdana" runat="server" ForeColor="Black" font-size="11px">1/1/2004</asp:label>
											</td>
											<td align="right">
												<asp:label id="lblEndDate" Font-Names="Verdana" runat="server" ForeColor="Black" font-size="11px">1/1/2004</asp:label>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td>
									<br>
									<table border="0" cellpadding="0" cellspacing="0" width="100%">
										<tr>
											<td>
												<asp:label id="lblLabelAccountNumber" Font-Bold="True" Font-Names="Verdana" runat="server"
													ForeColor="#053868" font-size="11px">Account #</asp:label>
											</td>
											<td align="right">
												<asp:label id="lblLabelFY" Font-Bold="True" Font-Names="Verdana" runat="server" 
													ForeColor="#053868"	font-size="11px">FY</asp:label>
											</td>
										</tr>
										<tr>
											<td>
												<asp:label id="lblAccountNumber" Font-Names="Verdana" runat="server" ForeColor="Black" font-size="11px">422200000</asp:label>
											</td>
											<td align="right">
												<asp:label id="lblFY" Font-Names="Verdana" runat="server" ForeColor="Black" font-size="11px">2004</asp:label>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							--%>
							<tr>
								<td>
									<table border="0" cellpadding="0" cellspacing="0" width="100%">
										<tr>
											<td>
												<asp:label id="lblLabelFMName" Font-Bold="True" Font-Names="Verdana" runat="server" ForeColor="#053868"
													font-size="11px">FM Name</asp:label>
											</td>
											<td align="right">
												<asp:label id="lblLabelFMNo" Font-Bold="True" Font-Names="Verdana" runat="server" ForeColor="#053868"
													font-size="11px">FM ID</asp:label>
											</td>
										</tr>
										<tr>
											<td>
												<asp:label id="lblFMName" Font-Names="Verdana" runat="server" ForeColor="Black" font-size="11px">John Smith</asp:label>
											</td>
											<td align="right" valign="top">
												<asp:label id="lblFMNo" Font-Names="Verdana" runat="server" ForeColor="Black" font-size="11px">1111</asp:label>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<%--
							<tr>
								<td>
									<table border="0" cellpadding="0" cellspacing="0" width="100%">
										<tr>
											<td>
												<asp:label id="lblLabelProgramType" Font-Bold="True" Font-Names="Verdana" runat="server" ForeColor="#053868"
													font-size="11px">Program</asp:label>
											</td>
											<td>
												<asp:label id="lblLabelProductType" Font-Bold="True" Font-Names="Verdana" runat="server" ForeColor="#053868"
													font-size="11px">Product</asp:label>
											</td>
										</tr>
										<tr>
											<td>
												<asp:label id="lblProgramType" Font-Names="Verdana" runat="server" ForeColor="Black" font-size="11px">Program</asp:label>
											</td>
											<td valign="top">
												<asp:label id="lblProductType" Font-Names="Verdana" runat="server" ForeColor="Black" font-size="11px">Product</asp:label>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td>
									<asp:HyperLink id="lnkChange" runat="server" Font-Names="Verdana" Font-Size="11px" Font-Bold="True"
										NavigateUrl="CampaignSelection.aspx"><br>Select another campaign<br></asp:HyperLink>
								</td>
							</tr>
							--%>
							<tr>
								<td>
									<asp:label id="lblUser" Font-Names="Verdana" Font-Bold="True" runat="server" ForeColor="#053868"
										font-size="11px">Current User Name:</asp:label>
								</td>
							</tr>
							<tr>
								<td>
									<asp:label id="lblUserName" Font-Names="Verdana" runat="server" ForeColor="Black" font-size="11px">User</asp:label>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</TD>
	</TR>
	<TR>
		<TD>
			<TABLE WIDTH="100%" BORDER="0" CELLPADDING="0" CELLSPACING="0" background="images/InfoTab_BottomMiddle.gif">
				<TR>
					<TD>
						<asp:image Runat="server" ImageUrl="~/images/InfoTab_BottomLeft.gif" WIDTH="10" HEIGHT="20" id="Image3" />
					</TD>
					<TD align="left" WIDTH="100%">
						<asp:image Runat="server" WIDTH="150px" ImageUrl="~/images/InfoTab_BottomMiddle.gif" HEIGHT="20"
							id="Image5" />
					</TD>
					<TD>
						<asp:image Runat="server" ImageUrl="~/images/InfoTab_BottomRight.gif" WIDTH="10" HEIGHT="20"
							id="Image4" />
					</TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>

