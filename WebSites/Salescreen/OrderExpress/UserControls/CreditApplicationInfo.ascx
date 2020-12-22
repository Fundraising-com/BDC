<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.CreditApplicationInfo" Codebehind="CreditApplicationInfo.ascx.cs" %>

<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td bgColor="#336699"><asp:label id="lblTitleAccountInfo" ForeColor="White" runat="server" CssClass="StandardLabel">
				Organization information
			</asp:label>
		</td>
	</tr>
	<tr>
		<td>
			<hr width="100%" SIZE="2">
		</td>
	</tr>
	<tr id="trValSumAccountInfo" runat="server" visible="false">
		<td></td>
	</tr>
	<tr>
		<td>
			<TABLE id="tblAccountInfo" runat="server" cellSpacing="0" cellPadding="0" width="100%"
				border="0">
				<TR>
					<td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
					<td>
						<TABLE cellSpacing="0" cellPadding="1" width="500" border="0">
							<TR>
								<TD><asp:label id="Label15" runat="server" CssClass="StandardLabel">Account&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
								<TD colspan="3"><asp:label id="lblAccountName" runat="server" CssClass="DescInfoLabel"></asp:label></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label29" runat="server" CssClass="StandardLabel">Officer&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
								<TD width="100%" colspan="3">
									<asp:label id="lblOfficerName" runat="server" CssClass="DescInfoLabel"></asp:label>
								</TD>
							<TR>
								<TD><asp:label id="Label1" runat="server" CssClass="StandardLabel">Address&nbsp;Line&nbsp;1&nbsp;:&nbsp;</asp:label></TD>
								<TD colspan="3"><asp:label id="lblAddressLine1" runat="server" CssClass="DescInfoLabel"></asp:label></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label2" runat="server" CssClass="StandardLabel">Address&nbsp;Line&nbsp;2&nbsp;:&nbsp;</asp:label></TD>
								<TD colspan="3"><asp:label id="lblAddressline2" runat="server" CssClass="DescInfoLabel"></asp:label></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label4" runat="server" CssClass="StandardLabel">City&nbsp;:&nbsp;</asp:label></TD>
								<td><asp:label id="lblCity" runat="server" CssClass="DescInfoLabel"></asp:label></td>
								<TD><asp:label id="Label6" runat="server" CssClass="StandardLabel">&nbsp;County&nbsp;:&nbsp;</asp:label></TD>
								<TD><asp:label id="lblCounty" runat="server" CssClass="DescInfoLabel"></asp:label></TD>
							</TR>
							<TR>
								<TD><asp:label id="Labesl5" runat="server" CssClass="StandardLabel">State&nbsp;:&nbsp;</asp:label></TD>
								<TD><asp:label id="lblState" runat="server" CssClass="DescInfoLabel" Width="100px"></asp:label></TD>
								<TD><asp:label id="Label7" runat="server" CssClass="StandardLabel">Zip&nbsp;Code&nbsp;:&nbsp;</asp:label></TD>
								<TD><asp:label id="lblZip" runat="server" CssClass="DescInfoLabel" Width="100px"></asp:label></TD>
							</TR>
							<TR>
								<TD vAlign="top"><asp:label id="Label28" runat="server" CssClass="StandardLabel">Credit&nbsp;App.&nbsp;Type&nbsp;:&nbsp;</asp:label></TD>
								<TD colspan="3">
									<asp:label id="lblTypeName" CssClass="DescInfoLabel" runat="server"></asp:label></TD>
							</TR>
						</TABLE>
						<br>
						<br>
					</td>
				</TR>
			</TABLE>
		</td>
	</tr>
	<tr id="trCreditApp" runat="server">
		<td>
			<table border="0" cellpadding="o" cellspacing="0" width="100%">
				<tr>
					<td bgColor="#336699" colspan="2"><asp:label id="lblTitleCreditAppInfo" ForeColor="White" runat="server" CssClass="StandardLabel">
							Individual Responsible for Payment  -- (Must be 18 years or older)
						</asp:label></td>
				</tr>
				<tr>
					<td colspan="2">
						<hr width="100%" SIZE="2">
					</td>
				</tr>
				<tr id="trValSumCreditAppInfo" runat="server" visible="false">
					<td colspan="2">
					</td>
				</tr>
				<TR>
					<td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
					<td>
						<TABLE cellSpacing="0" cellPadding="1" width="500" border="0">
							<TR>
								<TD><asp:label id="Label3" runat="server" CssClass="StandardLabel">First&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
								<TD width="100%" colspan="3"><asp:label id="lblAppFirstName" runat="server" CssClass="DescInfoLabel"></asp:label></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label9" runat="server" CssClass="StandardLabel">Last&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
								<TD width="100%" colspan="3">
									<asp:label id="lblAppLastName" runat="server" CssClass="DescInfoLabel"></asp:label>
								</TD>
							<TR>
								<TD><asp:label id="Label11" runat="server" CssClass="StandardLabel">Address&nbsp;Line&nbsp;1&nbsp;:&nbsp;</asp:label></TD>
								<TD colspan="3"><asp:label id="lblAppAddressLine1" runat="server" CssClass="DescInfoLabel"></asp:label></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label13" runat="server" CssClass="StandardLabel">Address&nbsp;Line&nbsp;2&nbsp;:&nbsp;</asp:label></TD>
								<TD colspan="3"><asp:label id="lblAppAddressLine2" runat="server" CssClass="DescInfoLabel"></asp:label></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label24" runat="server" CssClass="StandardLabel">City&nbsp;:&nbsp;</asp:label></TD>
								<td><asp:label id="lblAppCity" runat="server" CssClass="DescInfoLabel"></asp:label></td>
								<TD><asp:label id="Label36" runat="server" CssClass="StandardLabel">&nbsp;County&nbsp;:&nbsp;</asp:label></TD>
								<TD><asp:label id="lblAppCounty" runat="server" CssClass="DescInfoLabel"></asp:label></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label40" runat="server" CssClass="StandardLabel">State&nbsp;:&nbsp;</asp:label></TD>
								<TD><asp:label id="lblAppState" runat="server" CssClass="DescInfoLabel" Width="100px"></asp:label></TD>
								<TD><asp:label id="Label44" runat="server" CssClass="StandardLabel">&nbsp;Zip&nbsp;Code&nbsp;:&nbsp;</asp:label></TD>
								<TD><asp:label id="lblAppZip" runat="server" CssClass="DescInfoLabel" Width="100px"></asp:label></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label46" runat="server" CssClass="StandardLabel">Phone&nbsp;Number&nbsp;:&nbsp;</asp:label></TD>
								<TD><asp:label id="lblAppPhone" runat="server" CssClass="DescInfoLabel" Width="150px"></asp:label></TD>
								<TD><asp:label id="Label48" runat="server" CssClass="StandardLabel">&nbsp;Home&nbsp;Phone&nbsp;:&nbsp;</asp:label></TD>
								<TD><asp:label id="lblAppHomePhone" runat="server" CssClass="DescInfoLabel" Width="150px"></asp:label></TD>
							</TR>
							<TR>
								<TD><asp:label id="Label47" runat="server" CssClass="StandardLabel">SSN&nbsp;:&nbsp;</asp:label></TD>
								<TD><asp:label id="lblSSN" runat="server" CssClass="DescInfoLabel" Width="100px"></asp:label></TD>
								<TD><asp:label id="Label50" runat="server" CssClass="StandardLabel">&nbsp;Credit&nbsp;Limit&nbsp;:&nbsp;</asp:label></TD>
								<TD><asp:label id="lblCreditLimit" runat="server" CssClass="DescInfoLabel" Width="100px"></asp:label></TD>
							</TR>
						</TABLE>
						<br>
						<br>
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<td><asp:label id="Label23" runat="server" CssClass="StandardLabel" Font-Size="xx-small">
										By completing this information, you authorize QSP to obtain a consumer report 
										from a consumer reporting agency in connection with the credit transaction.
										Upon request, we will provide the name and address at theconsumer report agency.
									</asp:label><br>
									<br>
								</td>
							</TR>
						</TABLE>
					</td>
				</TR>
			</table>
			<br>
			<br>
		</td>
	</tr>
	<tr id="trCreditCard" runat="server">
		<td>
			<table border="0" cellpadding="o" cellspacing="0" width="100%">
				<tr>
					<td bgColor="#336699"><asp:label id="lblTitleCreditCardInfo" ForeColor="White" runat="server" CssClass="StandardLabel">
							Credit Card Information -- Credit Card Payments Only
						</asp:label></td>
				</tr>
				<tr>
					<td>
						<hr width="100%" SIZE="2">
					</td>
				</tr>
				<tr id="trValSumCreditCardInfo" runat="server" visible="false">
					<td>
					</td>
				</tr>
				<tr>
					<td>
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td>
									<TABLE cellSpacing="0" cellPadding="1" width="500" border="0">
										<TR>
											<TD><asp:label id="Label16" runat="server" CssClass="StandardLabel">First&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
											<TD colspan="3"><asp:label id="lblCCFirstName" runat="server" CssClass="DescInfoLabel"></asp:label></TD>
										</TR>
										<TR>
											<TD><asp:label id="Label51" runat="server" CssClass="StandardLabel">Last&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
											<TD width="100%" colspan="3">
												<asp:label id="lblCCLastName" runat="server" CssClass="DescInfoLabel"></asp:label>
											</TD>
										<TR>
											<TD><asp:label id="Label53" runat="server" CssClass="StandardLabel">Address&nbsp;Line&nbsp;1&nbsp;:&nbsp;</asp:label></TD>
											<TD colspan="3"><asp:label id="lblCCAddressLine1" runat="server" CssClass="DescInfoLabel"></asp:label></TD>
										</TR>
										<TR>
											<TD><asp:label id="Label55" runat="server" CssClass="StandardLabel">Address&nbsp;Line&nbsp;2&nbsp;:&nbsp;</asp:label></TD>
											<TD colspan="3"><asp:label id="lblCCAddressLine2" runat="server" CssClass="DescInfoLabel"></asp:label></TD>
										</TR>
										<TR>
											<TD><asp:label id="Label57" runat="server" CssClass="StandardLabel">City&nbsp;:&nbsp;</asp:label></TD>
											<td><asp:label id="lblCCCity" runat="server" CssClass="DescInfoLabel"></asp:label></td>
											<TD><asp:label id="Label59" runat="server" CssClass="StandardLabel">&nbsp;County&nbsp;:&nbsp;</asp:label></TD>
											<TD><asp:label id="lblCCCounty" runat="server" CssClass="DescInfoLabel"></asp:label></TD>
										</TR>
										<TR>
											<TD><asp:label id="Label61" runat="server" CssClass="StandardLabel">State&nbsp;:&nbsp;</asp:label></TD>
											<TD><asp:label id="lblCCState" runat="server" CssClass="DescInfoLabel" Width="100px"></asp:label></TD>
											<TD><asp:label id="Label63" runat="server" CssClass="StandardLabel">&nbsp;Zip&nbsp;Code&nbsp;:&nbsp;</asp:label></TD>
											<TD><asp:label id="lblCCZip" runat="server" CssClass="DescInfoLabel" Width="100px"></asp:label></TD>
										</TR>
										<TR>
											<TD><asp:label id="Label65" runat="server" CssClass="StandardLabel">Credit&nbsp;Card&nbsp;Type&nbsp;:&nbsp;</asp:label></TD>
											<TD colspan="3">
												<asp:label id="lblCCTypeName" runat="server" CssClass="DescInfoLabel"></asp:label>
											</TD>
										</TR>
										<TR>
											<TD><asp:label id="Label69" runat="server" CssClass="StandardLabel">Credit&nbsp;Card&nbsp;Number&nbsp;:&nbsp;</asp:label></TD>
											<TD><asp:label id="lblCCNumber" runat="server" CssClass="DescInfoLabel" Width="100px"></asp:label></TD>
											<TD><asp:label id="Label71" runat="server" CssClass="StandardLabel">&nbsp;Expire&nbsp;:&nbsp;</asp:label></TD>
											<TD><asp:label id="lblCCExpire" runat="server" CssClass="DescInfoLabel" Width="100px"></asp:label></TD>
										</TR>
									</TABLE>
								</td>
							</TR>
						</TABLE>
						<br>
						<br>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr id="trQSPSalesRep" runat="server">
		<td>
			<table border="0" cellpadding="o" cellspacing="0" width="100%">
				<tr>
					<td bgColor="#336699"><asp:label id="lblTitleQSPSalesRepInfo" ForeColor="White" runat="server" CssClass="StandardLabel">
							QSP Sales Representative Authorization
						</asp:label></td>
				</tr>
				<tr>
					<td>
						<hr width="100%" SIZE="2">
					</td>
				</tr>
				<tr>
					<td>
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td><br>
									<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TR>
											<td><asp:label id="Label27" runat="server" CssClass="StandardLabel">
													On behalf of the above selling organization. I accept full responsibility for payment of this account
												</asp:label>
												<br>
												<br>
											</td>
										</TR>
									</TABLE>
									<TABLE cellSpacing="0" cellPadding="1" width="600" border="0">
										<TR>
											<TD><asp:label id="Label26" runat="server" CssClass="StandardLabel">FSM&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
											<TD><asp:label id="lblFMName" runat="server" CssClass="DescInfoLabel" Width="300px"></asp:label></TD>
											<TD><asp:label id="Label33" runat="server" CssClass="StandardLabel">&nbsp;FSM&nbsp;ID&nbsp;:&nbsp;</asp:label></TD>
											<TD><asp:label id="lblFMID" runat="server" CssClass="DescInfoLabel" Width="100px"></asp:label></TD>
										</TR>
									</TABLE>
								</td>
							</TR>
						</TABLE>
						<br>
						<br>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr id="trDocumentSection" runat="server">
		<td>
			<table cellSpacing="0" cellPadding="o" width="100%" border="0">
				<tr>
					<td bgColor="#336699"><asp:label id="lblTitleDocumentSection" ForeColor="White" runat="server" CssClass="StandardLabel">
							Documentation Section
						</asp:label></td>
				</tr>
				<tr>
					<td>
						<hr width="100%" SIZE="2">
					</td>
				</tr>
				<tr id="trValSumDocumentSection" runat="server" visible="false">
					<td><asp:validationsummary id="ValSumDocumentSection" runat="server" CssClass="LabelError" HeaderText="Correct the following error to proceed."></asp:validationsummary></td>
				</tr>
				<tr>
					<td>
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<td>&nbsp;&nbsp;&nbsp;&nbsp;
								</td>
								<td>
									<TABLE cellSpacing="0" cellPadding="1" width="600" border="0">
										<TR>
											<TD colSpan="2"><asp:label id="Label5" runat="server" CssClass="StandardLabel">
													The document is received:&nbsp;
												</asp:label><asp:checkbox id="chkBoxDocumentApproved" runat="server" Enabled="False"></asp:checkbox></TD>
										</TR>
										<TR>
											<TD><asp:label id="Label8" runat="server" CssClass="StandardLabel">Received&nbsp;By&nbsp;:&nbsp;</asp:label></TD>
											<TD>
												<table cellSpacing="0" cellPadding="0" border="0">
													<tr>
														<TD><asp:label id="lblDocumentApprovedBy" runat="server" CssClass="DescInfoLabel" Width="250px"></asp:label></TD>
														<td>&nbsp;
														</td>
														<TD><asp:label id="Label10" runat="server" CssClass="StandardLabel">Date&nbsp;:&nbsp;</asp:label></TD>
														<TD><asp:label id="lblDocumentApprovedDate" runat="server" CssClass="DescInfoLabel" Width="200px"></asp:label></TD>
													</tr>
												</table>
											</TD>
										</TR>
									</TABLE>
								</td>
							</TR>
						</TABLE>
						<br>
						<br>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td bgColor="#336699"><asp:label id="Label34" CssClass="StandardLabel" runat="server" ForeColor="White">
				Accounting Department Approval Section
			</asp:label></td>
	</tr>
	<tr>
		<td>
			<hr width="100%" SIZE="2">
		</td>
	</tr>
	<tr>
		<td>
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<td>&nbsp;&nbsp;&nbsp;&nbsp;
					</td>
					<td>
						<TABLE cellSpacing="0" cellPadding="1" width="600" border="0">
							<TR>
								<TD>
									<asp:label id="Label43" CssClass="StandardLabel" runat="server">Approved&nbsp;:&nbsp;</asp:label>
								</TD>
								<TD >
									<asp:CheckBox id="chkBoxApproved" runat="server" Enabled="False"></asp:CheckBox>
								</TD>
								<TD>
									<asp:label id="Label12" CssClass="StandardLabel" runat="server">Approved&nbsp;Code&nbsp;:&nbsp;</asp:label>
								</TD>
								<TD>
									<asp:label id="lblApproveCode" runat="server" Width="300px" CssClass="DescInfoLabel"></asp:label>
								</TD>
							</TR>
							<TR>
								<TD>
									<asp:label id="Label39" CssClass="StandardLabel" runat="server">Approved&nbsp;By&nbsp;:&nbsp;</asp:label>
								</TD>
								<TD>
									<asp:label id="lblApprovedBy" runat="server" Width="300px" CssClass="DescInfoLabel"></asp:label>
								</TD>
								<TD>
									<asp:label id="Label41" CssClass="StandardLabel" runat="server">Date&nbsp;:&nbsp;</asp:label>
								</TD>
								<TD>
									<asp:label id="lblApprovalDate" runat="server" Width="200px" CssClass="DescInfoLabel"></asp:label>
								</TD>
							</TR>
						</TABLE>
					</td>
				</TR>
			</TABLE>
			<br>
			<br>
		</td>
	</tr>
</table>
