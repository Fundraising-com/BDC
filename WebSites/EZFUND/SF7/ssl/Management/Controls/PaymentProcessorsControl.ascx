<%@ Register TagPrefix="uc1" TagName="UploadControl" Src="UploadControl.ascx" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="PaymentProcessorsControl.ascx.vb" Inherits="StoreFront.StoreFront.PaymentProcessorsControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TBODY>
		<tr>
			<TD class="Content" width="1" colSpan="7">&nbsp;</TD>
		</tr>
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="ContentTableHeader" noWrap align="left" colSpan="5">&nbsp;&nbsp;Payment 
				Gateway&nbsp;
			</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" noWrap align="right" colSpan="1">Gateway Provider:&nbsp;&nbsp;</td>
			<td class="content" align="left" colSpan="4" valign="middle"><asp:dropdownlist id="Gateway" AutoPostBack="True" runat="server"></asp:dropdownlist>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				<asp:label id="lblActivateRecurring" Runat="server" Visible="False">Activate Recurring Billing: </asp:label><asp:checkbox id="chkActivateRecurring" Runat="server" Visible="False"></asp:checkbox></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" colSpan="7" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<asp:panel id="CyberSource" runat="server">
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="center" width="159" colSpan="1"><IMG src="images/cybersource.jpg"></TD>
				<TD class="content" align="left" colSpan="4">CyberSource enables secure, reliable, 
					real-time card and e-check processing in multiple currencies. Supports major 
					cards and card types across multiple processors, worldwide. <A href="http://www.cybersource.com/" target="_blank">
						Click Here</A> for more information.</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" colSpan="7" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159" colSpan="1">Merchant ID:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="CSMerchantID" runat="server" MaxLength="100"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159" colSpan="1">Config File:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<uc1:UploadControl id="ucCyberSourceFile" runat="server"></uc1:UploadControl></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159" colSpan="1">&nbsp;&nbsp;</TD>
				<TD class="content" align="left" width="463" colSpan="2">
					<asp:RadioButton id="CSAuthOnly" Runat="server" GroupName="CSAuth" Text="Authorize Only"></asp:RadioButton></TD>
				<TD class="content" align="left" colSpan="2">
					<asp:RadioButton id="CSAuthCapture" Runat="server" GroupName="CSAuth" Text="Authorize And Capture"></asp:RadioButton></TD>
				<TD class="ContentTable" width="7"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
		</asp:panel>
		<asp:panel id="FirePay" runat="server">
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="center" width="159" colSpan="1"><IMG src="images/terra_payments_email_logo.jpg"></TD>
				<TD class="content" align="left" colSpan="4">Terra Payments' Internet Payment 
					Processing solution includes everything you need to get started quickly and 
					process online payments securely and in complete confidence.<A href=" http://www.terrapayments.com" target="_blank">
						Click Here</A> for more information.</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" colSpan="7" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159" colSpan="1">Merchant ID:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="FPMerchantID" runat="server" MaxLength="100"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159" colSpan="1">User ID:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="FPUserID" runat="server" MaxLength="100"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159" colSpan="1">Config File 
					Name:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<uc1:UploadControl id="ucFirePayFile" runat="server"></uc1:UploadControl></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
		</asp:panel>
		<asp:panel id="LinkPoint" runat="server">
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="center" width="159" colSpan="1"><IMG src="images/cardservice.gif"></TD>
				<TD class="content" align="left" colSpan="4">Cardservice International, the leader 
					in Internet merchant accounts,&nbsp;provides a complete solution for your 
					merchant accounts and payment gateway needs. <A href="http://www.storefront.net/partners/cardserviceint.asp" target="_blank">
						Click Here</A> for more information.</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" colSpan="7" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">&nbsp;&nbsp;</TD>
				<TD class="content" align="left" width="463" colSpan="2">
					<asp:RadioButton id="LPTestMode" Runat="server" GroupName="LTM" Text="Test Mode"></asp:RadioButton></TD>
				<TD class="content" align="left" colSpan="2">
					<asp:RadioButton id="LPLiveMode" Runat="server" GroupName="LTM" Text="Live Mode"></asp:RadioButton></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159" colSpan="1">Store Number:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="LPMerchantID" runat="server" MaxLength="100"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159" colSpan="1">Config File:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<uc1:UploadControl id="ucLinkPointFile" runat="server"></uc1:UploadControl></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">&nbsp;</TD>
				<TD class="content" align="left" width="463" colSpan="2">
					<asp:RadioButton id="LPAuthOnly" Runat="server" GroupName="LPAuth" Text="Authorize Only"></asp:RadioButton></TD>
				<TD class="content" align="left" colSpan="2">
					<asp:RadioButton id="LPAuthCapture" Runat="server" GroupName="LPAuth" Text="Authorize And Capture"></asp:RadioButton></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
		</asp:panel>
		<asp:panel id="PsiGate" runat="server">
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="center" width="159" colSpan="1"><IMG src="images/psigate.jpg"></TD>
				<TD class="content" align="left" colSpan="4">PSiGate specializes in e-commerce 
					payment services including both US and Canadian merchant accounts and gateway 
					services. <A href="http://www.psigate.com/merchantaccount.asp" target="_blank">Click 
						Here</A> for more information.</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" colSpan="7" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159" colSpan="1">Merchant ID:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="PGMerchantID" runat="server" MaxLength="100"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159" colSpan="1">Config File:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<uc1:UploadControl id="ucPsiGateFile" runat="server"></uc1:UploadControl>
				<TD class="ContentTable" width="1" colSpan="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159" colSpan="1">&nbsp;&nbsp;</TD>
				<TD class="content" align="left" width="462" colSpan="2">
					<asp:RadioButton id="PGAuthOnly" Runat="server" GroupName="PGAuth" Text="Authorize Only"></asp:RadioButton></TD>
				<TD class="content" align="left" colSpan="2">
					<asp:RadioButton id="PGAuthCapture" Runat="server" GroupName="PGAuth" Text="Authorize And Capture"></asp:RadioButton></TD>
				<TD class="ContentTable" width="1" colSpan="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
		</asp:panel>
		<asp:panel id="SecurePay" runat="server">
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="center" width="159" colSpan="1"><IMG src="images/securepay.jpg"></TD>
				<TD class="content" align="left" colSpan="4">SecurePay provides merchants with a 
					one-stop source for e-commerce. From establishing a merchant account to hosting 
					your StoreFront web site. <A href="http://www.securepay.com" target="_blank">Click 
						Here</A> for more information.</TD>
				<TD class="ContentTable" width="1" colSpan="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1" colSpan="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1" colSpan="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159" colSpan="1">Merchant ID:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="SPMerchantID" runat="server" MaxLength="100"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
		</asp:panel>
		<asp:panel id="VeriSign" runat="server">
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="center" width="159" colSpan="1"><IMG src="images/verisign.gif"></TD>
				<TD class="content" align="left" colSpan="4">VeriSign, with over&nbsp;75,000 
					merchants, processes approximately 25% of all US online transactions using 
					Payflow Payment Services. A&nbsp;robust, real-time, fully customizable payment 
					solution.&nbsp;<A href="http://www.verisign.com/products/payflow/pro/index.html" target="_blank">Click 
						Here</A> for more information.</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" colSpan="7" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">&nbsp;&nbsp;</TD>
				<TD class="content" align="left" width="463" colSpan="2">
					<asp:RadioButton id="VTestMode" Runat="server" GroupName="VTM" Text="Test Mode"></asp:RadioButton></TD>
				<TD class="content" align="left" colSpan="2">
					<asp:RadioButton id="VLiveMode" Runat="server" GroupName="VTM" Text="Live Mode"></asp:RadioButton></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">Partner&nbsp;ID:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="VSMerchantID" runat="server" MaxLength="100"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">User Name:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="VSUserName" runat="server" MaxLength="100"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">Vendor ID:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="VSVendorName" runat="server" MaxLength="100"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">Password:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="VSPassword" runat="server" MaxLength="100"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">&nbsp;&nbsp;</TD>
				<TD class="content" align="left" width="462" colSpan="2">
					<asp:RadioButton id="VSAuthOnly" Runat="server" GroupName="VSAuth" Text="Authorize Only"></asp:RadioButton></TD>
				<TD class="content" align="left" colSpan="2">
					<asp:RadioButton id="VSAuthCapture" Runat="server" GroupName="VSAuth" Text="Authorize And Capture"></asp:RadioButton></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
		</asp:panel>
		<asp:panel id="BankOfAmerica" runat="server">
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="center" width="159" colSpan="1"><IMG src="images/bankofamerica.jpg"></TD>
				<TD class="content" align="left" colSpan="4">Add flexible real-time credit card 
					processing to your online store with Bank of America Merchant Services. Call 
					800.228.5882 or <A href="http://www.bankofamerica.com/merchantservices/index.cfm?template=merch_ic_estores_settle.cfm"
						target="_blank">Click Here</A> for more information.</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" colSpan="7" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">Merchant ID:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="BAMerchantID" runat="server" MaxLength="100"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">User ID:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="BAUserName" runat="server" MaxLength="100"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">Password:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="BAPassword" runat="server" MaxLength="100"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1" height="20"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159" height="20">&nbsp;</TD>
				<TD class="content" align="left" width="462" colSpan="1" height="20">
					<asp:RadioButton id="BAAuthOnly" Runat="server" GroupName="BAAuth" Text="Authorize Only"></asp:RadioButton></TD>
				<TD class="content" align="left" colSpan="3" height="20">
					<asp:RadioButton id="BAAuthCapture" Runat="server" GroupName="BAAuth" Text="Authorize And Capture"></asp:RadioButton></TD>
				<TD class="ContentTable" width="1" height="20"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
		</asp:panel>
		<asp:panel id="IONGate" runat="server">
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="center" width="159" colSpan="1"><IMG src="images/iongate.jpg"></TD>
				<TD class="content" align="left" colSpan="4">IO•NGATE &nbsp;provides unique, 
					innovative and cost effective products that enable you to effectively process 
					credit cards online. <A href="http://www.iongate.com/merchant_app.htm" target="_blank">
						Click Here</A> for more information.</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" colSpan="7" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">Login:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="IGLogin" runat="server" MaxLength="100"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
		</asp:panel>
		<asp:panel id="PayPal" runat="server">
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="center" width="159" colSpan="1"><IMG src="images/paypal.gif"></TD>
				<TD class="content" align="left" colSpan="4"><BR>
					<FONT style="FONT-WEIGHT: bold">
						After selecting PayPal as your gateway provider, click on the PayPal tab or <A href="PayPal.aspx">Click Here</A> to configure
						your store to begin processing credit card payments using PayPal.</FONT></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
		</asp:panel>
		<asp:panel id="VeriSignPayFlowLink" Runat="server">
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="center" width="159" colSpan="1"><IMG src="images/verisign.gif"></TD>
				<TD class="content" align="left" colSpan="4">VeriSign, with over&nbsp;75,000 
					merchants, processes approximately 25% of all US online transactions using 
					Payflow Payment Services. A&nbsp;robust, real-time, fully customizable payment 
					solution.&nbsp;<A href="http://www.verisign.com/products/payflow/pro/index.html" target="_blank">Click 
						Here</A> for more information.</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="center" width="159" colSpan="1">PartnerId&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="txtpayflowlinkPartnerId" Runat="server"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="center" width="159" colSpan="1">MerchantId:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="txtPayFlowLinkMerchantId" Runat="server"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="center" width="159" colSpan="1">Password:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="txtPayFlowLinkPassword" Runat="server"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">&nbsp;&nbsp;</TD>
				<TD class="content" align="center" width="463" colSpan="2">
					<asp:RadioButton id="rbPayFlowLinkAuthorize" Runat="server" GroupName="PayFlowLink" Text="Authorize Only"></asp:RadioButton></TD>
				<TD class="content" align="left" colSpan="2">
					<asp:RadioButton id="rbPayFlowLinkAuthCapt" Runat="server" GroupName="PayFlowLink" Text="Authorize and Capture"></asp:RadioButton></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
		</asp:panel>
		<asp:panel id="WorldPay" runat="server">
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="center" width="159" colSpan="1"><IMG src="images/worldpay.jpg"></TD>
				<TD class="content" align="left" colSpan="4">WorldPay provides International 
					businesses the ability to process and accept online payments in over 120 
					different currencies of their choice. <A href="https://secure.worldpay.com/app/application.pl?brand=affiliate&amp;partnerId41250"
						target="_blank">Click Here</A> for more information.</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" colSpan="7" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">&nbsp;&nbsp;</TD>
				<TD class="content" align="left" width="463" colSpan="2">
					<asp:RadioButton id="WPTestMode" Runat="server" GroupName="WPTM" Text="Test Mode"></asp:RadioButton></TD>
				<TD class="content" align="left" colSpan="2">
					<asp:RadioButton id="WPLiveMode" Runat="server" GroupName="WPTM" Text="Live Mode"></asp:RadioButton></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">Installation ID:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="WPMerchantID" runat="server"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
		</asp:panel>
		<asp:panel id="SecureSource" runat="server">
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="center" width="159" colSpan="1"><IMG src="images/wellsfargo.jpg"></TD>
				<TD class="content" align="left" colSpan="4">Wells Fargo SecureSource(SM) 
					WebPayment Suite is an integrated merchant service featuring: Wells Fargo 
					Internet Merchant Account, Authorize.net payment gateway and proprietary risk 
					and fraud technology. <A href="https://www.wellsfargo.com/biz/products/merchant/merchant" target="_blank">Click 
						Here</A> to apply and begin processing payments today.</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" colSpan="7" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">Merchant ID:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="SSUserName" runat="server" MaxLength="100"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">Transaction Key:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="SSPassword" runat="server" MaxLength="100"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">&nbsp;&nbsp;</TD>
				<TD class="content" align="left" width="463" colSpan="2">
					<asp:RadioButton id="SSAuthOnly" Runat="server" GroupName="SSAuth" Text="Authorize Only"></asp:RadioButton></TD>
				<TD class="content" align="left" colSpan="2">
					<asp:RadioButton id="SSAuthCapture" Runat="server" GroupName="SSAuth" Text="Authorize And Capture"></asp:RadioButton></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
		</asp:panel>
		<asp:panel id="PlanetPayment" runat="server">
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="center" width="159" colSpan="1"><IMG src="images/planetpayment.jpg"></TD>
				<TD class="content" align="left" colSpan="4">Planet Payment's industry-leading 
					transaction processing system enables businesses to authorize, process, and 
					manage credit card transactions securely in a real-time, online environment.
				</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" colSpan="7" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="883" colSpan="7"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">Client ID:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="PPUserName" runat="server" MaxLength="100"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">Transaction Key:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="PPPassword" runat="server" MaxLength="100"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">&nbsp;&nbsp;</TD>
				<TD class="content" align="left" width="463" colSpan="2">
					<asp:RadioButton id="PPAuthOnly" Runat="server" GroupName="PPAuth" Text="Authorize Only"></asp:RadioButton></TD>
				<TD class="content" align="left" colSpan="2">
					<asp:RadioButton id="PPAuthCapture" Runat="server" GroupName="PPAuth" Text="Authorize And Capture"></asp:RadioButton></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
		</asp:panel>
		<asp:panel id="AuthorizeNet" runat="server">
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="center" width="159" colSpan="1"><IMG src="images/authorizenet.jpg"></TD>
				<TD class="content" align="left" colSpan="4">Authorize.Net offers secure, scalable 
					solutions, enabling merchants to process transactions from Internet, broadband, 
					wireless, call center and retail sources, using the same Authorize.Net account. <A href="http://www.authorize.net" target="_self">
						Click Here</A> for more information.</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" colSpan="7" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">Client ID:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="ANUserName" runat="server" MaxLength="100"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">Transaction Key:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="ANPassword" runat="server" MaxLength="100"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">&nbsp;&nbsp;</TD>
				<TD class="content" align="left" width="463" colSpan="2">
					<asp:RadioButton id="ANAuthOnly" Runat="server" GroupName="ANAuth" Text="Authorize Only"></asp:RadioButton></TD>
				<TD class="content" align="left" colSpan="2">
					<asp:RadioButton id="ANAuthCapture" Runat="server" GroupName="ANAuth" Text="Authorize And Capture"></asp:RadioButton></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
		</asp:panel>
		<asp:panel id="QuickCommerce" runat="server">
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="center" width="159" colSpan="1"><IMG src="images/ecommerceexchange.jpg"></TD>
				<TD class="content" align="left" colSpan="4">E-Commerce Exchange's Quick Commerce 
					makes it easy, affordable, and secure for businesses to accept credit cards and 
					checks on the Internet. <A href="http://www.ecenow.com/" target="_blank">Click Here</A>
					for more information.</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" colSpan="7" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">Merchant Login:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="QCUserName" runat="server"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">Transaction Key:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="QCPassword" runat="server"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">&nbsp;&nbsp;</TD>
				<TD class="content" align="left" width="463" colSpan="2">
					<asp:RadioButton id="QCAuthOnly" Runat="server" GroupName="QCAuth" Text="Authorize Only"></asp:RadioButton></TD>
				<TD class="content" align="left" colSpan="2">
					<asp:RadioButton id="QCAuthCapture" Runat="server" GroupName="QCAuth" Text="Authorize And Capture"></asp:RadioButton></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
		</asp:panel>
		<asp:panel id="Barclay" runat="server">
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="center" width="159" colSpan="1"><IMG src="images/barclaycard.jpg"></TD>
				<TD class="content" align="left" colSpan="4">Barclaycard Merchant Services operates 
					the largest online, real-time bank-owned electronic funds transfer at the point 
					of sale system in the UK. <A href="http://www.barclaycardmerchantservices.co.uk/" target="_blank">
						Click Here</A> for more information.
				</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" colSpan="7" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">Client ID:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="BCUserName" runat="server" MaxLength="100"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">Password:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="BCPassword" runat="server" MaxLength="100"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">&nbsp;&nbsp;</TD>
				<TD class="content" align="left" width="463" colSpan="2">
					<asp:RadioButton id="BCAuthOnly" Runat="server" GroupName="BCAuth" Text="Authorize Only"></asp:RadioButton></TD>
				<TD class="content" align="left" colSpan="2">
					<asp:RadioButton id="BCAuthCapture" Runat="server" GroupName="BCAuth" Text="Authorize And Capture"></asp:RadioButton></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
		</asp:panel>
		<asp:panel id="Paradata" Runat="server">
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="center" width="159" colSpan="1">
					<asp:Image id="imgParaData" Runat="server"></asp:Image></TD>
				<TD class="content" align="left" colSpan="4"><A href="http://www.paradata.com/products/lagarde.html" target="_blank">Paradata
					</A>is a secure, real-time electronic paymente service endorsed by leading 
					Banks and Processors in North America.
					<BR>
					Paradata offers affordable, scaleable solutions that are industry specific with 
					superior technical support.<BR>
				</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" colSpan="7" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">Account Token:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="PDPassword" runat="server" MaxLength="100" TextMode="MultiLine" CssClass="content"
						Width="400px"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">&nbsp;&nbsp;</TD>
				<TD class="content" align="left" width="463" colSpan="2">
					<asp:RadioButton id="PDAuthOnly" Runat="server" GroupName="PDAuth" Text="Authorization"></asp:RadioButton></TD>
				<TD class="content" align="left" colSpan="2">
					<asp:RadioButton id="PDAuthCapture" Runat="server" GroupName="PDAuth" Text="Sale"></asp:RadioButton></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
		</asp:panel>
		<asp:panel id="ParadataSFGateway" Runat="server">
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="center" width="159" colSpan="1">
					<asp:Image id="imgSFGateway1" Runat="server" ImageUrl="images/sfpaygateway.jpg"></asp:Image></TD>
				<TD class="content" align="left" colSpan="4"><A href="http://www.paradata.com/products/lagarde.html" target="_blank">Paradata
					</A>is a secure, real-time electronic paymente service endorsed by leading 
					Banks and Processors in North America.
					<BR>
					Paradata offers affordable, scaleable solutions that are industry specific with 
					superior technical support.<BR>
				</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" colSpan="7" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">Account Token:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="SFPassword" runat="server" MaxLength="100" TextMode="MultiLine" CssClass="content"
						Width="400px"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">&nbsp;&nbsp;</TD>
				<TD class="content" align="left" width="463" colSpan="2">
					<asp:RadioButton id="SFAuthOnly" Runat="server" GroupName="SFAuth" Text="Authorization"></asp:RadioButton></TD>
				<TD class="content" align="left" colSpan="2">
					<asp:RadioButton id="SFAuthCapture" Runat="server" GroupName="SFAuth" Text="Sale"></asp:RadioButton></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
		</asp:panel>
		<asp:panel id="Orbital" Runat="server">
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="center" width="159" colSpan="1">
					<asp:Image id="imgOrbital" Runat="server"></asp:Image></TD>
				<TD class="content" align="left" colSpan="4">Paymentech, the largest processor of 
					ecommerce payment transactions in the nation, has developed a proprietary 
					payment gateway that provides you connectivity, processing and acquiring 
					services all in one. Paymentech's Orbital(R) Gateway system securely delivers 
					and encrypts your customers' payment information during electronic transmittal. 
					 <A href="http://www.paymentech.com/solgat.do" target="_blank">Click here </A>
					for more information.<BR>
					<BR>
				</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" colSpan="7" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">&nbsp;&nbsp;</TD>
				<TD class="content" align="left" width="463" colSpan="2">
					<asp:RadioButton id="OrbTest" Runat="server" GroupName="ORBTM" Text="Test Mode"></asp:RadioButton></TD>
				<TD class="content" align="left" colSpan="2">
					<asp:RadioButton id="OrbLive" Runat="server" GroupName="ORBTM" Text="Live Mode" Checked="True"></asp:RadioButton></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">Merchant Account Number:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="ORMerchantID" runat="server" MaxLength="100"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">&nbsp;&nbsp;</TD>
				<TD class="content" align="left" width="463" colSpan="2">
					<asp:RadioButton id="ORAuthOnly" Runat="server" GroupName="ORAuth" Text="Authorize Only"></asp:RadioButton></TD>
				<TD class="content" align="left" colSpan="2">
					<asp:RadioButton id="ORAuthCapture" Runat="server" GroupName="ORAuth" Text="Authorize And Capture"></asp:RadioButton></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
		</asp:panel>
		<asp:panel id="CCommerce" Runat="server">
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="center" width="159" colSpan="1">
					<asp:Image id="imgCCommerce" Runat="server" ImageUrl="images/cclogo80_120.jpg"></asp:Image></TD>
				<TD class="content" align="left" colSpan="4">Increase revenue and protect profits 
					with ClearCommerce. The ClearCommerce QuickStart hosted solution provides 
					industry leading fraud detection, real-time payment processing, and payer 
					authentication services for online financial trsactions.<BR>
					<A href="http://www.clearcommerce.com/" target="_blank">Click Here</A>
					to learn more.
				</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" colSpan="7" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">&nbsp;&nbsp;</TD>
				<TD class="content" align="left" width="463" colSpan="2">
					<asp:RadioButton id="CCTest" Runat="server" GroupName="CCBT" Text="Test Mode"></asp:RadioButton></TD>
				<TD class="content" align="left" colSpan="2">
					<asp:RadioButton id="CCLive" Runat="server" GroupName="CCBT" Text="Live Mode"></asp:RadioButton></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">Client ID:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="txtCCClientID" runat="server" MaxLength="100"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">User ID:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="txtCCUserId" runat="server" MaxLength="100"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">Password:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="txtCCPassword" runat="server" MaxLength="100"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">&nbsp;&nbsp;</TD>
				<TD class="content" align="left" width="463" colSpan="2">
					<asp:RadioButton id="CCAuthOnly" Runat="server" GroupName="CCAuth" Text="Authorize Only"></asp:RadioButton></TD>
				<TD class="content" align="left" colSpan="1">
					<asp:RadioButton id="CCAuthCap" Runat="server" GroupName="CCAuth" Text="Authorize And Capture"></asp:RadioButton></TD>
				<TD class="content" align="right">&nbsp;&nbsp;</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
		</asp:panel>
		<asp:panel id="PayFuse" Runat="server">
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="center" width="159" colSpan="1">
					<asp:Image id="imgPFuse" Runat="server" ImageUrl="images/payfuse.jpg"></asp:Image></TD>
				<TD class="content" align="left" colSpan="4">Accept payments over the Internet 
					securely and efficiently with PayFuse, the all-in-one Internet payment 
					processing solution.<BR>
					<A href="http://www.firstnationalmerchants.com/msup/products/internet/payfuse_short.asp"
						target="_blank">Click Here</A> to learn more.
				</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" colSpan="7" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">&nbsp;&nbsp;</TD>
				<TD class="content" align="left" width="463" colSpan="2">
					<asp:RadioButton id="PFTest" Runat="server" GroupName="PFBT" Text="Test Mode"></asp:RadioButton></TD>
				<TD class="content" align="left" colSpan="2">
					<asp:RadioButton id="PFLive" Runat="server" GroupName="PFBT" Text="Live Mode"></asp:RadioButton></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">Client ID:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="txtPFClientId" runat="server" MaxLength="100"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">User ID:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="txtPFUserId" runat="server" MaxLength="100"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">Password:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="txtPFPassword" runat="server" MaxLength="100"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">&nbsp;&nbsp;</TD>
				<TD class="content" align="left" width="463" colSpan="2">
					<asp:RadioButton id="PFAuthOnly" Runat="server" GroupName="PFAuth" Text="Authorize Only"></asp:RadioButton></TD>
				<TD class="content" align="left" colSpan="1">
					<asp:RadioButton id="PFAuthCap" Runat="server" GroupName="PFAuth" Text="Authorize And Capture"></asp:RadioButton></TD>
				<TD class="content" align="right">&nbsp;&nbsp;</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
		</asp:panel>
		<asp:panel id="StoreFrontPayment" Runat="server">
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="center" width="159" colSpan="1">
					<asp:Image id="SfPaymentImage" Runat="server" ImageUrl="../images/sfpaygateway.jpg"></asp:Image></TD>
				<TD class="content" align="left" colSpan="4">Accept and process credit card orders 
					at the time of sale with online processing services from StoreFront. Get 
					secure, reliable payment processing at highly competitive rates.<BR>
					<A href="http://www.storefront.net/services/payments/" target="_blank">Click Here</A>
					for more information or to sign up today.
				</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" colSpan="7" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">&nbsp;&nbsp;</TD>
				<TD class="content" align="left" width="463" colSpan="2">
					<asp:RadioButton id="SFTest" Runat="server" GroupName="SFBT" Text="Test Mode"></asp:RadioButton></TD>
				<TD class="content" align="left" colSpan="2">
					<asp:RadioButton id="SFLive" Runat="server" GroupName="SFBT" Text="Live Mode"></asp:RadioButton></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">Client ID:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="txtSFClientId" runat="server" MaxLength="100"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">User ID:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="txtSFUserId" runat="server" MaxLength="100"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">Password:&nbsp;&nbsp;</TD>
				<TD class="content" align="left" colSpan="4">
					<asp:TextBox id="txtSFPassword" runat="server" MaxLength="100"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="159">&nbsp;&nbsp;</TD>
				<TD class="content" align="left" width="463" colSpan="2">
					<asp:RadioButton id="SFAuth" Runat="server" GroupName="SFAuth" Text="Authorize Only"></asp:RadioButton></TD>
				<TD class="content" align="left" colSpan="1">
					<asp:RadioButton id="SFAuthCap" Runat="server" GroupName="SFAuth" Text="Authorize And Capture"></asp:RadioButton></TD>
				<TD class="content" align="right">&nbsp;&nbsp;</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
		</asp:panel>
		<asp:panel id="pnlPayerAuth" Runat="server" Visible="False">
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" colSpan="5">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					<asp:CheckBox id="chkEnableCardinalAuth" AutoPostBack="True" Runat="server" Text="Use Payer Authentication"></asp:CheckBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<asp:Panel id="pnlPayerAuthMain" Runat="server">
				<TR>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="ContentTableHeader" noWrap align="left" colSpan="5">&nbsp;&nbsp;Payer 
						Authentication &nbsp;</TD>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="content" vAlign="middle" width="621" colSpan="2">&nbsp;&nbsp;&nbsp;&nbsp;Providers: 
						&nbsp;&nbsp;
						<asp:DropDownList id="ddPayerAuthProviders" AutoPostBack="True" Runat="server"></asp:DropDownList></TD>
					<TD class="content" colSpan="3"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="contentTable" colSpan="7" height="1"></TD>
				</TR>
				<asp:Panel id="pnlCardinalPayerAuth" Runat="server" Visible="False">
					<TR>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="content" align="center" width="159" colSpan="1"><IMG src="images/cc_logo_LaGarde.gif"></TD>
						<TD class="content" align="left" colSpan="4"><BR>
							CardinalCommerce is the leading provider of Verified by Visa, MasterCard® 
							SecureCode™ and JCB J/Secure. These initiatives eliminate fraud and "I didn't 
							do it" chargebacks, guarantee payment to merchant and provide a secure channel 
							for safe international ecommerce; while also protecting cardholders from 
							fraudulent use of their credit cards. &nbsp;<A href="https://billing.cardinalcommerce.com/centinel/registration/storefront_landing.asp"
								target="_blank">Click Here</A> for more information.</TD>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</TR>
					<TR>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="content" width="1" colSpan="5"><IMG height="10" src="images/clear.gif"></TD>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</TR>
					<TR>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</TR>
					<TR>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="content" noWrap width="159">&nbsp;Cardinal Merchant Id:&nbsp;&nbsp;</TD>
						<TD class="content" colSpan="4">
							<asp:TextBox id="txtCardinalMerchantId" Runat="server"></asp:TextBox></TD>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</TR>
					<TR>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="content" noWrap width="159">&nbsp;Cardinal Processor Id:&nbsp;&nbsp;</TD>
						<TD class="content" colSpan="4">
							<asp:TextBox id="txtCardinalProcessorId" Runat="server"></asp:TextBox></TD>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</TR>
					<TR>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="content" noWrap width="159">&nbsp;Centinel MAPS URL:&nbsp;&nbsp;</TD>
						<TD class="content" colSpan="4">
							<asp:TextBox id="txtCentinelMapsUrl" Runat="server" Width="75%">https://centineltest.cardinalcommerce.com/maps/txns.asp</asp:TextBox></TD>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</TR>
				</asp:Panel>
				<asp:Panel id="pnlCCommercePayerAuth" Runat="server" Visible="False">
					<TR>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="content" align="right" width="159">Payer Authentication Client 
							Id:&nbsp;&nbsp;</TD>
						<TD class="content" align="left" colSpan="4">
							<asp:textbox id="txtPayerAuthClientId" runat="server" maxlength="100"></asp:textbox></TD>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</TR>
					<TR>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="content" align="right" width="159">Payer Authentication Service 
							URL:&nbsp;&nbsp;</TD>
						<TD class="content" align="left" colSpan="4">
							<asp:textbox id="txtPayerauthenticationURL" runat="server" Width="632px" maxlength="300"></asp:textbox></TD>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</TR>
					<TR>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
						<TD class="content" align="right" width="159">Fraud Detection Service:&nbsp;&nbsp;</TD>
						<TD class="content" align="left" colSpan="4">
							<asp:DropDownList id="lstCCServices" runat="server" Width="150px">
								<asp:ListItem value="NoFraudDetection">No Fraud Detection</asp:ListItem>
								<asp:ListItem Selected="True" value="FraudShield">FraudShield</asp:ListItem>
								<asp:ListItem value="FraudAnalyst">FraudAnalyst</asp:ListItem>
							</asp:DropDownList></TD>
						<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					</TR>
				</asp:Panel>
			</asp:Panel>
		</asp:panel>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" colSpan="7" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<tr>
			<TD class="Content" width="1" colSpan="7">&nbsp;</TD>
		</tr>
		<TR>
			<td class="content" align="right" width="75%" colSpan="7"><asp:linkbutton id="cmdSave" Runat="server">
					<asp:Image BorderWidth="0" ID="imgSave" runat="server" ImageUrl="../images/save.jpg" AlternateText="Save"></asp:Image>
				</asp:linkbutton></td>
		</TR>
	</TBODY></TABLE>
