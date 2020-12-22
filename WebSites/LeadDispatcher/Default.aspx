<%@ Register TagPrefix="uc1" TagName="WebUserControl1" Src="UserControls/WebUserControl1.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeadDispatcher" Src="UserControls/Lead/LeadDispatcher.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Admin_Section" Src="UserControls/Admin_Section.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Admin_Menu" Src="UserControls/Admin_Menu.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopMenuAR" Src="UserControls/TopMenuAR.ascx" %>
<%@ Page language="c#" Codebehind="Default.aspx.cs" AutoEventWireup="True" Inherits="CRMWeb.Default" %>
<%@ Register TagPrefix="uc1" TagName="LeadInfo" Src="UserControls/Lead/LeadInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="DataHeaderLead" Src="UserControls/Lead/DataHeaderLead.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PromotionGroups" Src="UserControls/Lead/PromotionGroups.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopMenuLead" Src="UserControls/Lead/TopMenuLead.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PaymentInfo" Src="UserControls/PaymentInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AdjustmentInfo" Src="UserControls/AdjustmentInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="DataHeaderAR" Src="UserControls/DataHeaderAR.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Login" Src="UserControls/Login.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftMenuConsultant" Src="UserControls/LeftMenuConsultant.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CRM v.1.2</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body bgColor="#edede1" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 0px; WIDTH: 840px; POSITION: absolute; TOP: 0px; HEIGHT: 619px"
				cellSpacing="0" cellPadding="0" width="840" border="0">
				<TR>
					<TD style="WIDTH: 849px; HEIGHT: 53px" vAlign="top" background="images/backgrnd.gif">
						<TABLE id="Table5" style="WIDTH: 680px; HEIGHT: 56px" cellSpacing="0" cellPadding="0" width="680"
							border="0">
							<TR>
								<TD style="WIDTH: 175px; HEIGHT: 27px">&nbsp;
									<asp:imagebutton id="ImageButton1" runat="server" ImageUrl="images/efund_top2.gif"></asp:imagebutton></TD>
								<TD style="WIDTH: 27px; HEIGHT: 21px" vAlign="middle" align="center"></TD>
								<TD style="WIDTH: 65px; HEIGHT: 21px" vAlign="middle" align="center"></TD>
								<TD style="HEIGHT: 21px" vAlign="middle" align="left"><uc1:dataheaderar id="DataHeaderAR1" runat="server" Visible="False"></uc1:dataheaderar><uc1:dataheaderlead id="DataHeaderLead1" runat="server" Visible="False"></uc1:dataheaderlead></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 849px; HEIGHT: 519px" vAlign="top" align="left">
						<TABLE id="Table6" style="WIDTH: 808px; HEIGHT: 544px" cellSpacing="0" cellPadding="0"
							width="808" align="left" border="0">
							<TR>
								<TD style="WIDTH: 111px" vAlign="top">
									<TABLE id="Table3" style="WIDTH: 112px; HEIGHT: 453px" cellSpacing="0" cellPadding="0"
										width="112" border="0">
										<TR>
											<TD style="WIDTH: 111px; HEIGHT: 104px" background="images/backgrnd2_.gif">
												<TABLE id="Table2" style="WIDTH: 114px; HEIGHT: 80px" cellSpacing="0" cellPadding="0" width="114"
													border="0">
													<TR>
														<TD style="WIDTH: 112px; HEIGHT: 17px">
															<DIV title="e" style="DISPLAY: inline; FONT-WEIGHT: bold; FONT-SIZE: 10pt; WIDTH: 96px; COLOR: #fffec7; HEIGHT: 14px"
																ms_positioning="FlowLayout">Quick Search</DIV>
														</TD>
													</TR>
													<TR>
														<TD style="WIDTH: 112px" vAlign="bottom"><asp:dropdownlist id="cboQuickSearch" runat="server" Width="104px"></asp:dropdownlist></TD>
													</TR>
													<TR>
														<TD style="WIDTH: 112px; HEIGHT: 34px" vAlign="top">
															<TABLE id="Table4" style="WIDTH: 113px; HEIGHT: 8px" cellSpacing="0" cellPadding="0" width="113"
																border="0">
																<TR>
																	<TD style="WIDTH: 80px; HEIGHT: 19px"><asp:textbox id="txtQuickSearch" runat="server" Width="85px" Height="20px" Font-Names="Microsoft Sans Serif" ontextchanged="txtQuickSearch_TextChanged"></asp:textbox></TD>
																	<TD style="HEIGHT: 19px" bgColor="#587ea5"><asp:imagebutton id="cmdQuickSearch" runat="server" ImageUrl="images/Continue3.gif"></asp:imagebutton></TD>
																</TR>
															</TABLE>
														</TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
										<TR>
											<TD style="WIDTH: 111px; HEIGHT: 355px" vAlign="top"><uc1:leftmenuconsultant id="LeftMenuConsultant1" runat="server"></uc1:leftmenuconsultant>
											</TD>
										</TR>
									</TABLE>
								</TD>
								<TD style="WIDTH: 721px" vAlign="top" align="left">
									<TABLE id="Table7" style="WIDTH: 672px; HEIGHT: 544px" cellSpacing="0" cellPadding="0"
										width="672" align="left" border="0">
										<TR>
											<TD style="WIDTH: 663px; HEIGHT: 29px" vAlign="top"><uc1:topmenulead id="TopMenuLead1" runat="server" Visible="False"></uc1:topmenulead>
												<uc1:TopMenuAR id="TopMenuAR1" runat="server"></uc1:TopMenuAR></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 663px; HEIGHT: 189px"><uc1:login id="Login1" runat="server"></uc1:login><uc1:paymentinfo id="PaymentInfo1" runat="server" Visible="False"></uc1:paymentinfo><uc1:adjustmentinfo id="AdjustmentInfo1" runat="server" Visible="False"></uc1:adjustmentinfo><uc1:leaddispatcher id="LeadDispatcher1" runat="server" Visible="False"></uc1:leaddispatcher><uc1:leadinfo id="LeadInfo1" runat="server" Visible="False"></uc1:leadinfo>
												<uc1:Admin_Section id="Admin_Section1" runat="server"></uc1:Admin_Section></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 663px"></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
