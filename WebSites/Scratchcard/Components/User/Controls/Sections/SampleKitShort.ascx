<%@ Register TagPrefix="buttonpanel" Namespace="GA.BDC.Core.Web.UI.UIControls" Assembly="GA.BDC.Core.Web.UI.UIControls" %>
<%@ Register TagPrefix="uc1" TagName="ExplicitAddressConfirmation" Src="../../AddressHygene/ExplicitAddressConfirmation.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AddressHygiene" Src="../../AddressHygene/AddressHygiene.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SampleKitShort.ascx.cs" Inherits="GA.BDC.WEB.ScratchcardWeb.Components.User.Controls.Sections.SampleKitShort" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<script language="javascript" src="resources/jsScript/eFundWeb.js"></script>
<%
	Response.Write("<script language='javascript'>" + 
		"var fullNameContainer = '" + fullNameContainer + "';" + 
		"</script>");
%>
<script language="javascript">		
			
		var GlobalCountPropertyChanged = 0;
	function ValidateDropDown(source, argument) {
		var oCtrlName = source.id.replace('rfv','lbl');
		if(argument.Value == "0" || argument.Value == "99") {
			argument.IsValid = false;
		} else { 
			argument.IsValid = true;
		}
	}

	var isNN = (navigator.appName.indexOf("Netscape")!=-1);
	function setPhone(pCtrlName, index, input,len, e) {
			pCtrlName = fullNameContainer + "_" + pCtrlName;
			var oCtrl = document.getElementById(pCtrlName);
			oCtrl.value = document.getElementById(pCtrlName + "1").value + "-" + 
							document.getElementById(pCtrlName + "2").value + "-" + 
							document.getElementById(pCtrlName + "3").value;	
			if(e.keyCode == 8) {
				oCtrl.value = document.getElementById(pCtrlName + "1").value + "-" + 
							document.getElementById(pCtrlName + "2").value + "-" + 
							document.getElementById(pCtrlName + "3").value;		
				if(oCtrl.value == "--")
					oCtrl.value = "";
			}
			var keyCode = (isNN) ? e.which : e.keyCode; 
			var filter = (isNN) ? [0,8,9] : [0,8,9,16,17,18,37,38,39,40,46];
			
			if(input.value.length >= len && !containsElement(filter,keyCode)) {
				input.value = input.value.slice(0, len);
				input.form[(getIndex(input)+1) % input.form.length].focus();
			}
		

		function containsElement(arr, ele) {
			var found = false, index = 0;
			
			while(!found && index < arr.length)
				if(arr[index] == ele)
					found = true;
				else
					index++;
			
			return found;
		}
		
		function getIndex(input) {
			var index = -1, i = 0, found = false;
			
				while (i < input.form.length && index == -1)
					if (input.form[i] == input)
						index = i;
					else 
						i++;
			
			return index;
		}
		return true;
	}

	function HideFullPhone() {			
			document.getElementById(fullNameContainer + "_txtDayPhone").style.visibility = "hidden";
			document.getElementById(fullNameContainer + "_txtEvnPhone").style.visibility = "hidden";
			
			
			var oEnv = document.getElementById(fullNameContainer + "_txtEvnPhone");
			
			if(oEnv.value == "--")
				oEnv.value= "";
			
			document.getElementById('ReturnToTop').focus();				

	}
</script>
<A id="ReturnToTop" href="#"></A>
<table class="normal_text" cellSpacing="3" cellPadding="0" width="100%" border="0">
	<tr>
		<td align="left" colSpan="4"><buttonpanel:contentpanelcontrol id="ContentPanelControl2" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Components\User\Controls\Sections\SampleKitShort.ascx"
				runat="server"></buttonpanel:contentpanelcontrol><br>
			<asp:image id="Image1" runat="server" ImageUrl="../../../../Resources/images/_ScratchcardWeb_/_classic_/en-US/common/Title/doted_line.gif"></asp:image><br>
			<P></P>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="normal_text" colSpan="2">
						<P>
							<TABLE id="Table1" borderColor="#0" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD colSpan="2"><buttonpanel:contentpanelcontrol id="ContentPanelControl3" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Components\User\Controls\Sections\SampleKitShort.ascx"
											runat="server"></buttonpanel:contentpanelcontrol></TD>
								</TR>
								<TR>
									<TD><buttonpanel:contentpanelcontrol id="ContentPanelControl4" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Components\User\Controls\Sections\SampleKitShort.ascx"
											runat="server"></buttonpanel:contentpanelcontrol><br>
										&nbsp;&nbsp;&nbsp;&nbsp;</TD>
									<TD></TD>
								</TR>
								<TR>
									<TD colSpan="2"><buttonpanel:contentpanelcontrol id="ContentPanelControl5" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Components\User\Controls\Sections\SampleKitShort.ascx"
											runat="server"></buttonpanel:contentpanelcontrol></TD>
								</TR>
							</TABLE>
						<p><asp:image id="Image2" runat="server" ImageUrl="../../../../Resources/images/_ScratchcardWeb_/_classic_/en-US/common/Title/doted_line.gif"></asp:image></p>
					</td>
				</tr>
				<tr>
					<td colSpan="2"><asp:validationsummary id="Validationsummary1" onpropertychange="document.getElementById('ReturnToTop').focus();"
							runat="server" CssClass="error" HeaderText=" " DisplayMode="List"></asp:validationsummary><uc1:addresshygiene id="AddressHygiene1" runat="server"></uc1:addresshygiene><uc1:explicitaddressconfirmation id="ExplicitAddressConfirmation1" runat="server"></uc1:explicitaddressconfirmation></td>
				</tr>
				<tr>
					<td class="normal_text" align="right" colSpan="2"><buttonpanel:contentpanelcontrol id="ContentPanelControl1" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Components\User\Controls\Sections\SampleKitShort.ascx"
							runat="server"></buttonpanel:contentpanelcontrol></td>
				</tr>
			</table>
			<br>
		</td>
	</tr>
	<tr>
		<td style="HEIGHT: 14px" align="center" colSpan="5">&nbsp;</td>
	</tr>
	<tr>
		<td style="WIDTH: 5px"></td>
		<td></td>
		<td class="SmallText" style="WIDTH: 582px"><asp:label id="lblFirstName" runat="server">First Name</asp:label>*</td>
		<td><asp:textbox id="txtFirstName" onkeyup="ResetControls(this, event);" runat="server" MaxLength="50"></asp:textbox></td>
		<td align="left"><asp:requiredfieldvalidator id="rfvFirstName" runat="server" CssClass="Error" ErrorMessage="You have to enter your First Name"
				ControlToValidate="txtFirstName">*</asp:requiredfieldvalidator></td>
	</tr>
	<tr>
		<td style="WIDTH: 5px"></td>
		<td></td>
		<td class="SmallText" style="WIDTH: 582px; HEIGHT: 26px"><asp:label id="lblLastName" runat="server">Last Name</asp:label>*</td>
		<td style="HEIGHT: 26px"><asp:textbox id="txtLastName" onkeyup="ResetControls(this, event);" runat="server" MaxLength="50"></asp:textbox></td>
		<td style="HEIGHT: 26px" align="left"><asp:requiredfieldvalidator id="rfvLastName" runat="server" CssClass="Error" ErrorMessage="You have to enter your Last name"
				ControlToValidate="txtLastName" Text="*"></asp:requiredfieldvalidator></td>
	</tr>
	<tr>
		<td style="WIDTH: 5px; HEIGHT: 26px"></td>
		<td style="HEIGHT: 26px"></td>
		<td class="SmallText" style="WIDTH: 582px; HEIGHT: 26px"><asp:label id="lblEmail" runat="server">E-Mail Address</asp:label>*</td>
		<td style="HEIGHT: 26px"><asp:textbox id="txtEmail" onkeyup="ResetControls(this, event);" runat="server" MaxLength="50"></asp:textbox></td>
		<td style="HEIGHT: 26px" align="left"><asp:regularexpressionvalidator id="rfvEmail" onpropertychange="" runat="server" CssClass="Error" ErrorMessage="You have to enter your E-Mail Address"
				ControlToValidate="txtEmail" ValidationExpression="^[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z0-9]{1,6}$">*</asp:regularexpressionvalidator>
			<asp:requiredfieldvalidator id="rfvEmail2" runat="server" CssClass="Error" ErrorMessage="You have to enter your E-Mail Address"
				ControlToValidate="txtEmail">*</asp:requiredfieldvalidator></td>
	</tr>
	<tr>
		<td style="WIDTH: 5px"></td>
		<td></td>
		<td class="SmallText" style="WIDTH: 582px"><asp:label id="lblGroupName" runat="server">Group Name</asp:label>*</td>
		<td><asp:textbox id="txtGroupName" onkeyup="ResetControls(this, event);" runat="server" MaxLength="50"></asp:textbox></td>
		<td align="left"><asp:requiredfieldvalidator id="rfvGroupName" runat="server" CssClass="Error" ErrorMessage="You have to provide your group's name"
				ControlToValidate="txtGroupName" Text="*">*</asp:requiredfieldvalidator></td>
	</tr>
	<tr>
		<td style="WIDTH: 5px"></td>
		<td></td>
		<td class="SmallText" style="WIDTH: 582px"><asp:label id="lblAddress" runat="server">Address</asp:label>*</td>
		<td><asp:textbox id="txtAddress" onkeyup="ResetControls(this, event);" runat="server" MaxLength="50"></asp:textbox></td>
		<td align="left"><asp:requiredfieldvalidator id="rfvAddress" runat="server" CssClass="Error" ErrorMessage="Address is invalid"
				ControlToValidate="txtAddress" Text="*"></asp:requiredfieldvalidator></td>
	</tr>
	<tr>
		<td style="WIDTH: 5px"></td>
		<td></td>
		<td class="SmallText" style="WIDTH: 582px"><asp:label id="lblCity" runat="server">City</asp:label>*</td>
		<td><asp:textbox id="txtCity" onkeyup="ResetControls(this, event);" runat="server" MaxLength="50"></asp:textbox></td>
		<td align="left"><asp:requiredfieldvalidator id="rfvCity" runat="server" CssClass="Error" ErrorMessage="The city is invalid"
				ControlToValidate="txtCity" Text="*"></asp:requiredfieldvalidator></td>
	</tr>
	<tr>
		<td style="WIDTH: 5px"></td>
		<td></td>
		<td class="SmallText" style="WIDTH: 582px; HEIGHT: 27px"><asp:label id="lblCountry" runat="server">Country</asp:label>*</td>
		<TD style="HEIGHT: 27px"><asp:dropdownlist id="ddlCountry" runat="server" AutoPostBack="True" DataValueField="CountryCode"
				DataTextField="CountryName"></asp:dropdownlist></TD>
		<TD style="HEIGHT: 27px" align="left"></TD>
	</tr>
	<tr>
		<td style="WIDTH: 5px"></td>
		<td></td>
		<td class="SmallText" style="WIDTH: 582px"><asp:label id="lblState" runat="server">State / Province</asp:label>*</td>
		<TD><asp:dropdownlist id="ddlState" runat="server" DataValueField="StateCode" DataTextField="StateName"></asp:dropdownlist></TD>
		<td align="left"><asp:customvalidator id="rfvState" runat="server" CssClass="Error" ErrorMessage="The State / Province is invalid"
				ControlToValidate="ddlState" ClientValidationFunction="ValidateDropDown">*</asp:customvalidator></td>
	</tr>
	<tr>
		<td style="WIDTH: 5px"></td>
		<td></td>
		<td class="SmallText" style="WIDTH: 582px"><asp:label id="lblZipCode" runat="server">Zip / Postal Code</asp:label>*</td>
		<td><asp:textbox id="txtZipCode" onkeyup="ResetControls(this, event);" runat="server" MaxLength="25"></asp:textbox></td>
		<td align="left"><asp:requiredfieldvalidator id="rfvZipCode" runat="server" CssClass="Error" ErrorMessage="You have to enter your Zip / Postal Code"
				ControlToValidate="txtZipCode" Text="*"></asp:requiredfieldvalidator></td>
	</tr>
	<tr>
		<td style="WIDTH: 5px"></td>
		<td></td>
		<td class="SmallText" style="WIDTH: 582px"><asp:label id="Label2" runat="server">Address Type</asp:label></td>
		<td>
			<asp:DropDownList id="AddressZoneDropDownList" runat="server"></asp:DropDownList></td>
		<td align="left"></td>
	</tr>
	<tr>
		<td style="WIDTH: 5px"></td>
		<td></td>
		<td class="SmallText" style="WIDTH: 582px; HEIGHT: 26px" vAlign="top"><asp:label id="lblDayPhone" runat="server">Day Phone</asp:label>*</td>
		<td style="HEIGHT: 26px"><asp:textbox onkeypress="ResetControls('txtDayPhone', event);" id="txtDayPhone1" onkeyup="return setPhone('txtDayPhone',1,this,3,event);"
				runat="server" MaxLength="3" size="3" Width="30px"></asp:textbox>-<asp:textbox onkeypress="ResetControls('txtDayPhone', event);" id="txtDayPhone2" onkeyup="return setPhone('txtDayPhone',2,this,3,event);"
				runat="server" MaxLength="3" size="3" Width="30px"></asp:textbox>-<asp:textbox onkeypress="ResetControls('txtDayPhone', event);" id="txtDayPhone3" onkeyup="return setPhone('txtDayPhone',3, this, 4, event);"
				runat="server" MaxLength="4" size="4" Width="35px"></asp:textbox>Ext:
			<asp:textbox id="txtDayPhnExt" runat="server" MaxLength="4" size="4" Width="35px"></asp:textbox></td>
		<td align="left"><asp:regularexpressionvalidator id="rfvDayPhone" onpropertychange="" runat="server" CssClass="Error" ErrorMessage="Day Phone Number is invalid"
				ControlToValidate="txtDayPhone" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}">*</asp:regularexpressionvalidator>
			<asp:requiredfieldvalidator id="rfvDayPhone2" runat="server" CssClass="Error" ErrorMessage="Day Phone Number is invalid"
				ControlToValidate="txtDayPhone" Text="*">*</asp:requiredfieldvalidator></td>
	</tr>
	<tr>
		<td style="WIDTH: 5px"></td>
		<td></td>
		<td class="SmallText" style="WIDTH: 582px" vAlign="top"><asp:label id="lblEvnPhone" runat="server">Evening Phone</asp:label></td>
		<td><asp:textbox onkeypress="ResetControls('txtEvnPhone', event);" id="txtEvnPhone1" onkeyup="return setPhone('txtEvnPhone',1, this, 3, event);"
				runat="server" MaxLength="3" size="3" Width="30px"></asp:textbox>-<asp:textbox onkeypress="ResetControls('txtEvnPhone', event);" id="txtEvnPhone2" onkeyup="return setPhone('txtEvnPhone',2, this, 3, event);"
				runat="server" MaxLength="3" size="3" Width="30px"></asp:textbox>-<asp:textbox onkeypress="ResetControls('txtEvnPhone', event);" id="txtEvnPhone3" onkeyup="return setPhone('txtEvnPhone',3, this, 4, event);"
				runat="server" MaxLength="4" size="4" Width="35px"></asp:textbox>Ext:
			<asp:textbox id="txtEvnPhnExt" runat="server" MaxLength="4" size="4" Width="35px"></asp:textbox></td>
		<td align="left"></td>
	</tr>
	<tr>
		<td style="WIDTH: 5px"></td>
		<td></td>
		<td class="SmallText" style="WIDTH: 582px"><asp:label id="lblBestTimeToCall" runat="server">Best Time to Call</asp:label>*</td>
		<td><asp:dropdownlist id="ddlBestTimeToCall" runat="server" DataValueField="TimeToCallValue" DataTextField="TimeToCallDescription"></asp:dropdownlist></td>
		<td align="left"><asp:customvalidator id="rfvBestTimeToCall" runat="server" CssClass="Error" ErrorMessage="You have to choose an option for the Best Time to Call"
				ControlToValidate="ddlBestTimeToCall" ClientValidationFunction="ValidateDropDown">*</asp:customvalidator></td>
	</tr>
	<tr>
		<td style="WIDTH: 5px"></td>
		<td></td>
		<td class="SmallText" style="WIDTH: 582px"><asp:label id="lblGroupNumber" runat="server">Number of Group Members</asp:label>*</td>
		<td><asp:textbox id="txtGroupNumber" onkeyup="ResetControls(this, event);" runat="server" MaxLength="5"></asp:textbox></td>
		<td align="left"><asp:regularexpressionvalidator id="rfvGroupNumber" onpropertychange="" runat="server" CssClass="Error" ErrorMessage="Please specify your group's size"
				ControlToValidate="txtGroupNumber" ValidationExpression="^[1-9]\d*">*</asp:regularexpressionvalidator>
			<asp:requiredfieldvalidator id="rfvGroupNumber2" runat="server" CssClass="Error" ErrorMessage="Please specify your group's size"
				ControlToValidate="txtGroupNumber">*</asp:requiredfieldvalidator></td>
	</tr>
	<tr>
		<td style="WIDTH: 5px; HEIGHT: 25px"></td>
		<td style="HEIGHT: 25px"></td>
		<td style="HEIGHT: 25px" align="center" colSpan="2">
			<P><buttonpanel:buttonpanelcontrol id="imgSendFreeKit" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Components\User\Controls\Sections\SampleKitShort.ascx"
					runat="server" CodeName="imgShortKitSubmit" ButtonType="IMAGE" CausesValidation="True"></buttonpanel:buttonpanelcontrol></P>
			<P>&nbsp;</P>
		</td>
	</tr>
</table>
<asp:textbox id="txtMonthList" style="VISIBILITY: hidden" runat="server"></asp:textbox><buttonpanel:pagepanelcontrol id="PagePanelControl1" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Components\User\Controls\Sections\SampleKitShort.ascx"
	runat="server"></buttonpanel:pagepanelcontrol>
	<asp:textbox id="txtEvnPhone" runat="server" Width="112px"></asp:textbox>
	<asp:textbox id="txtDayPhone" runat="server" Width="111px"></asp:textbox>
<script language="javascript">
		HideFullPhone();
</script>
