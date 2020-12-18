<%@ Register TagPrefix="uc1" TagName="CAddressBook" Src="Controls/CAddressBook.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="CommonControls/RightColumnNav.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="custaddressbook.aspx.vb" Inherits="StoreFront.StoreFront.CustAddressBook"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Customer Address Book</title>
		<%
'@BEGINVERSIONINFO

'@APPVERSION: 7.0.0

'@STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'@ENDCOPYRIGHT

'@ENDVERSIONINFO
%>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="<%=ThemesPath()%>Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="general.js"></script>
		<% Me.PageHeader %>
		<script language="javascript">
		<!--
			function SetValidation()
			{
				ResetForm(window.document.Form2);
				window.document.Form2.elements["NickName"].required=true;
				window.document.Form2.elements["NickName"].title="Save As";
				window.document.Form2.elements["FirstName"].required=true;
				window.document.Form2.elements["FirstName"].title="First Name";
				window.document.Form2.elements["LastName"].required=true;
				window.document.Form2.elements["LastName"].title="Last Name";
				window.document.Form2.elements["Address1"].required=true;
				window.document.Form2.elements["Address1"].title="Address";
				window.document.Form2.elements["City"].required=true;
				window.document.Form2.elements["City"].title="City";
				window.document.Form2.elements["Phone"].required=true;
				window.document.Form2.elements["Phone"].phonenumber=true;
				window.document.Form2.elements["Phone"].title="Phone";
				window.document.Form2.elements["Fax"].phonenumber=true;
				window.document.Form2.elements["Fax"].title="Fax";
				
				if (window.document.Form2.elements["Country"].value == "US" ||
					window.document.Form2.elements["Country"].value == "CA")
				{
					window.document.Form2.elements["Zip"].required=true;
					window.document.Form2.elements["Zip"].title="Postal Code";
				}
				else
				{
					window.document.Form2.elements["Zip"].required=false;
				}

				return ValidateForm(window.document.Form2)
			}
			//-->
		</script>
	</HEAD>
	<body id="BodyTag" runat="server" class="GeneralPage">
		<form id="Form2" method="post" runat="server">
			<table id="PageTable" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
				<tr>
					<td id="PageCell">
						<table id="PageSubTable" cellSpacing="0" cellPadding="0" width="100%" align="center" border="1" runat="server">
							<tr>
								<td class="TopBanner" id="TopBannerCell" width="100%" colSpan="3">
									<!-- Top Banner Start -->
									<uc1:topbanner id="TopBanner1" runat="server"></uc1:topbanner>
									<!-- Top Banner End -->
								</td>
							</tr>
							<tr>
								<td class="TopSubBanner" id="TopSubBannerCell" width="100%" colSpan="3">
									<!-- Top Sub Banner Start -->
									<uc1:topsubbanner id="TopSubBanner1" runat="server"></uc1:topsubbanner>
									<!-- Top Sub Banner End -->
								</td>
							</tr>
							<tr>
								<td class="LeftColumn" id="LeftColumnCell">
									<!-- Left Column Start -->
									<uc1:leftcolumnnav id="LeftColumnNav1" runat="server"></uc1:leftcolumnnav>
									<!-- Left Column End -->
								</td>
								<td class="Content" vAlign="top" id="ContentCell">
									<!-- Content Start -->
									<uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
									<p class="PageNavigation">
										<a href="CustEdit.aspx">Edit My Profile</a> <span>|</span>
										<a href="OrderHistory.aspx">View Order Status and History</a> <span>|</span>
										<a href="SavedCart.aspx">Access My Wish List</a> <span>|</span>
										<a href="CustAddressBook.aspx">Manage Address Book</a>
									</p>
									<h1 class="Headings">My Address Book</h1>
									<P id="ErrorAlignment" runat="server" align="center"><asp:Label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:Label></P>
									<P id="P1" runat="server" align="center"><asp:label id="Message" runat="server" Visible="False" CssClass="Messages"></asp:label></P>
									<asp:TextBox id="ReturnPage" runat="server" Visible="False"></asp:TextBox>
									<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0" class="Content col2">
										<TR>
											<TD class="c1">
												<h2 class="subHeadings"><asp:Label id="NewEditLabel" runat="server">Add An Address</asp:Label></h2>
												<TABLE id="Table2" cellSpacing="0" cellPadding="0" border="0" class="Content formtbl">
													<tr>
														<td class="name">Save As:</td>
														<td class="input"><asp:textbox id="NickName" runat="server" MaxLength="50"></asp:textbox><asp:label id="Label5" CssClass="ErrorMessages" Runat="server">*</asp:label></td>
													</tr>
													<TR>
														<TD class="name">First Name:</TD>
														<TD class="input"><asp:textbox id="FirstName" runat="server" MaxLength="100"></asp:textbox><asp:label id="Label2" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
													</TR>
													<TR>
														<TD class="name">Middle Initial:</TD>
														<TD class="input"><asp:textbox id="MI" runat="server" Width="33px" MaxLength="2"></asp:textbox></TD>
													</TR>
													<TR>
														<TD class="name">Last Name:</TD>
														<TD class="input"><asp:textbox id="LastName" runat="server" MaxLength="100"></asp:textbox><asp:label id="Label3" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
													</TR>
													<TR>
														<TD class="name">Company:</TD>
														<TD class="input"><asp:TextBox id="Company" runat="server" MaxLength="75"></asp:TextBox></TD>
													</TR>
													<TR>
														<TD class="name">Address 1:</TD>
														<TD class="input"><asp:textbox id="Address1" runat="server" MaxLength="255"></asp:textbox><asp:label id="Label6" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
													</TR>
													<TR>
														<TD class="name">Address 2:</TD>
														<TD class="input"><asp:textbox id="Address2" runat="server" MaxLength="255"></asp:textbox></TD>
													</TR>
													<TR>
														<TD class="name">City:</TD>
														<TD class="input"><asp:textbox id="City" runat="server" MaxLength="50"></asp:textbox><asp:label id="Label7" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
													</TR>
													<TR>
														<TD class="name">State/Province:</TD>
														<TD class="input"><cc1:SelectValControl id="State" runat="server" Width="206px"></cc1:SelectValControl><asp:label id="Label8" CssClass="ErrorMessages" Runat="server">*</asp:label></TD></TR>
													<TR>
														<TD class="name">Postal Code:</TD>
														<TD class="input"><asp:textbox id="Zip" runat="server" MaxLength="50"></asp:textbox><span class="ErrorMessages">*</span></TD>
													</TR>
													<TR>
														<TD class="name">Country:</TD>
														<TD class="input"><cc1:SelectValControl id="Country" runat="server" Width="203px" DisplaySelect="Country"></cc1:SelectValControl></TD>
													</TR>
													<TR>
														<TD class="name">Phone:</TD>
														<TD class="input"><asp:textbox id="Phone" runat="server"></asp:textbox><asp:label id="Label9" CssClass="ErrorMessages" Runat="server">*</asp:label></TD>
													</TR>
													<TR>
														<TD class="name">Fax:</TD>
														<TD class="input"><asp:textbox id="Fax" runat="server"></asp:textbox></TD>
													</TR>
													<TR>
														<TD colSpan="2" class="button">
															<asp:TextBox id="AddressID" runat="server" Visible="False"></asp:TextBox>
															<asp:LinkButton ID="btnAdd" Runat="server">
																<asp:Image BorderWidth="0" ID="imgAdd" Runat="server" AlternateText="Add"></asp:Image>
															</asp:LinkButton>
															&nbsp;
															<asp:LinkButton ID="btnClear" Runat="server">
																<asp:Image BorderWidth="0" ID="imgClear" Runat="server" AlternateText="Clear"></asp:Image>
															</asp:LinkButton>
														</TD>
													</TR>
													<TR>
														<TD colSpan="2" class="button">
															<asp:LinkButton ID="btnSave" Runat="server">
																<asp:Image BorderWidth="0" ID="imgSave" Runat="server" AlternateText="Save"></asp:Image>
															</asp:LinkButton>
															&nbsp;
															<asp:LinkButton ID="btnCancel" Runat="server">
																<asp:Image BorderWidth="0" ID="imgCancel" Runat="server" AlternateText="Cancel"></asp:Image>
															</asp:LinkButton>
														</TD>
													</TR>
													<TR>
														<TD colSpan="2" class="button">
															<asp:LinkButton ID="btnContinue" Runat="server">
																<asp:Image BorderWidth="0" ID="imgContinue" Runat="server" AlternateText="Continue"></asp:Image>
															</asp:LinkButton>																		
														</TD>
													</TR>
												</TABLE>
											</TD>
											<TD class="c2" valign="top">
												<h2 class="subHeadings">Saved Addresses</h2>
												<uc1:caddressbook id="CAddressBook1" runat="server"></uc1:caddressbook>
											</TD>
										</TR>
									</TABLE>
									<!-- Content End -->
								</td>
								<td class="RightColumn" id="RightColumnCell">
									<!-- Right Column Start -->
									<uc1:rightcolumnnav id="RightColumnNav1" runat="server"></uc1:rightcolumnnav>
									<!-- Right Column End -->
								</td>
							</tr>
							<tr>
								<td colSpan="3" class="Footer" id="FooterCell">
									<!-- Footer Start -->
									<uc1:footer id="Footer1" runat="server"></uc1:footer>
									<!-- Footer End -->
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
