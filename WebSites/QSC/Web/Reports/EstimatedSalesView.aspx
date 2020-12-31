<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="UC" TagName="OrderStatus" Src="../OrderMgt/UC/OrderStatus.ascx" %>
<%@ Register TagPrefix="UC" TagName="Date" Src="../Common/DateEntry.ascx" %>
<%@ Register TagPrefix="UC" TagName="Programs" Src="../Common/ProgramsDDL.ascx" %>
<%@ Register TagPrefix="UC" TagName="FMddl" Src="../Common/FieldManagerDDL.ascx" %>
<%@ Page language="c#" Codebehind="EstimatedSalesView.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.Reports.EstimatedSalesView" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Estimated Sales View</title>
		<link href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</head>
	<body leftmargin="0" topmargin="0" onload="return window_onunload()">
		<form id="Form1" method="post" runat="server">
			<!--#include file="../Includes/Menu.inc"-->
			<!--#include file="../CustomerService/fctjavascriptall.js"--><br>
			<div align="center">
				<table id="Table1" cellspacing="0" cellpadding="0" width="90%" border="0">
					<tr>
						<td>
							<table width="100%" border="0">
								<tr>
									<td align="left"><asp:label id="Label5" runat="server" cssclass="CSPageTitle">Estimated Order and Sales</asp:label></td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td>
							<br>
							<table class="CSTable" id="Table2" cellspacing="0" cellpadding="2" width="80%" align="left">
								<tr>
									<td class="CSTableHeader" colspan="5" align="left">
										<span class="CSTitle">Search</span>
									</td>
								</tr>
								<tr>
									<td align="left"><asp:label id="Label2" runat="server" cssclass="Font8BoldVBlue">Date Received From</asp:label></td>
									<td align="left"><uc:date id="f_from_date" runat="server" required="True" /></td>
									<td align="left"><asp:label id="Label19" runat="server" cssclass="Font8BoldVBlue">To</asp:label></td>
									<td align="left"><uc:date id="f_to_date" runat="server" required="True" /></td>
									<td valign="middle" align="left" rowspan="4">
										<asp:button id="pbSearch" runat="server" text="Search" cssclass="fields2" />
									</td>
								</tr>
								<tr>
									<td align="left"><asp:label id="Label7" runat="server" cssclass="Font8BoldVBlue">Program</asp:label></td>
									<td align="left">
										<uc:programs id="ucPrograms" mode="2" allprogramsoption="true" runat="server" cssclass="boxlookW" />
									</td>
									<td align="left"><asp:label id="Label6" runat="server" cssclass="Font8BoldVBlue">Status</asp:label></td>
									<td align="left">
										<uc:orderstatus id="ucOrderStatus" allstatusesoption="true" runat="server" cssclass="boxlookW" /> <!-- id="f_status" -->
									</td>
								</tr>
								<tr id="trFMSearchRow1" runat="server">
									<td align="left" nowrap><asp:label id="Label4" runat="server" cssclass="Font8BoldVBlue">FM</asp:label></td>
									<td align="left">
										<uc:fmddl id="ucFMddl" runat="server" cssclass="boxlookW" /> <!-- id="f_fm" tabIndex="5"-->
									</td>
									<td colspan="2">&nbsp;</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td>
							<table>
								<tbody>
									<tr>
										<td>
											<br>
											<asp:label id="lblRecordInfo" runat="server" cssclass="csPlainText" font-bold="True" />
										</td>
										<td align="right">
											<asp:label id="lblShow" runat="server" cssclass="Font8BoldVBlue" visible="False" width="32px"
												height="16px" text="Show" />
											<asp:dropdownlist id="f_rows_per_page" tabindex="5" runat="server" cssclass="boxlookW" visible="False">
												<asp:listitem value="10">10</asp:listitem>
												<asp:listitem value="50">50</asp:listitem>
												<asp:listitem value="100">100</asp:listitem>
												<asp:listitem value="ALL">ALL</asp:listitem>
											</asp:dropdownlist>
										</td>
									</tr>
									<tr>
										<td colspan="2">
											<asp:datagrid id="dgEstimatedSalesView" style="Z-INDEX: 100" runat="server" width="744px" height="96px"
												horizontalalign="Left" font-size="Smaller" allowsorting="True" autogeneratecolumns="False"
												allowpaging="True" cellpadding="3" backcolor="White" borderwidth="1px" borderstyle="None"
												bordercolor="#CCCCCC" font-names="Verdana" showfooter="True">
												<selecteditemstyle font-bold="True" forecolor="White" backcolor="#669999"></selecteditemstyle>
												<itemstyle forecolor="#000066"></itemstyle>
												<headerstyle font-bold="True" forecolor="White" verticalalign="Top" backcolor="#006699"></headerstyle>
												<footerstyle horizontalalign="Right" forecolor="#000066" backcolor="White"></footerstyle>
												<columns>
													<asp:boundcolumn datafield="DateReceived" sortexpression="DateReceived" headertext="Date Received">
														<headerstyle width="2in"></headerstyle>
														<itemstyle wrap="False"></itemstyle>
													</asp:boundcolumn>
													<asp:boundcolumn datafield="AccountId" sortexpression="AccountId" headertext="Group Id"></asp:boundcolumn>
													<asp:templatecolumn sortexpression="Name" headertext="Group Name">
														<itemtemplate>
															<asp:Label CssClass=Font7BoldV id=dg_group_name runat="server" Width="152px" Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>' />
														</itemtemplate>
													</asp:templatecolumn>
													<asp:boundcolumn datafield="CampaignId" sortexpression="CampaignId" headertext="Campaign Id"></asp:boundcolumn>
													<asp:templatecolumn headertext="Programs">
														<headerstyle font-size="X-Small" font-names="Arial" font-bold="True"></headerstyle>
														<itemtemplate>
															<asp:label id="Label1" runat="server" width="128px" height="8px" text='Programs out of commission right now' />
														</itemtemplate>
													</asp:templatecolumn>
													<asp:boundcolumn datafield="FMname" sortexpression="FMname" headertext="Field Manager"></asp:boundcolumn>
													<asp:templatecolumn sortexpression="OrderID" headertext="Order ID">
														<itemtemplate>
															<asp:Label id=Label11 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OrderID") %>' />
														</itemtemplate>
														<footertemplate>
															<p>
																<asp:label id="Label10" runat="server" width="72px">Page Total:</asp:label></p>
															<p>
																<asp:label id="Label12" runat="server" width="80px">Report Total:</asp:label></p>
														</footertemplate>
													</asp:templatecolumn>
													<asp:templatecolumn sortexpression="EnterredAmount" headertext="Estimated Amount">
														<itemstyle horizontalalign="Right"></itemstyle>
														<itemtemplate>
															<asp:Label id=Label8 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EnterredAmount") %>'>
															</asp:label>
														</itemtemplate>
														<footertemplate>
															<p>
																<asp:Label id=est_page_total runat="server" Width="56px" Text='<%# string.Format("{0:c}",f_EstimatedAmountTotal) %>' />
															</p>
															<p>
																<asp:Label id=est_rep_total runat="server" Width="48px" Text='<%# string.Format("{0:c}",f_em_total) %>' />
															</p>
														</footertemplate>
													</asp:templatecolumn>
													<asp:templatecolumn sortexpression="CalculatedAmount" headertext="Actual Amount">
														<itemstyle horizontalalign="Right"></itemstyle>
														<itemtemplate>
															<asp:Label id=Label9 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CalculatedAmount") %>' />
														</itemtemplate>
														<footertemplate>
															<p>
																<asp:Label id=Label13 runat="server" Text='<%# string.Format("{0:c}",f_ActualAmountTotal) %>' />
															</p>
															<p>
																<asp:Label id=Label14 runat="server" Text='<%# string.Format("{0:c}",f_ac_total) %>' />
															</p>
														</footertemplate>
													</asp:templatecolumn>
													<asp:templatecolumn sortexpression="Variance" headertext="Variance">
														<itemstyle horizontalalign="Right"></itemstyle>
														<itemtemplate>
															<asp:Label id="Label15" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Variance") %>' />
														</itemtemplate>
														<footertemplate>
															<p>
																<asp:Label id="Label16" runat="server" Text='<%# string.Format("{0:c}",f_VarianceAmountTotal) %>' />
															</p>
															<p>
																<asp:Label id="Label18" runat="server" Text='<%# string.Format("{0:c}",f_var_total) %>' />
															</p>
														</footertemplate>
													</asp:templatecolumn>
													<asp:boundcolumn datafield="Status" sortexpression="Status" headertext="Status">
														<itemstyle horizontalalign="Center"></itemstyle>
													</asp:boundcolumn>
													<asp:boundcolumn datafield="OrderDetailCount" sortexpression="OrderDetailCount" headertext="Units">
														<itemstyle horizontalalign="Center"></itemstyle>
													</asp:boundcolumn>
													<asp:boundcolumn datafield="ReportedEnvelopes" sortexpression="ReportedEnvelopes" headertext="Envelopes">
														<itemstyle horizontalalign="Center"></itemstyle>
													</asp:boundcolumn>
												</columns>
												<pagerstyle horizontalalign="Left" forecolor="#000066" backcolor="White" mode="NumericPages"></pagerstyle>
											</asp:datagrid>
										</td>
									</tr>
								</tbody>
							</table>
						</td>
					</tr>
				</table>
				<br>
				<!--#include file="../CustomerService/errorwindow.js"-->
				<asp:validationsummary id="ValidationSummary1" runat="server" font-bold="True" showmessagebox="True" showsummary="False" />
			</div>
		</form>
	</body>
</html>
