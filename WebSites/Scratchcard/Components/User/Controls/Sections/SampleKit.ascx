<%@ Register TagPrefix="uc1" TagName="ExplicitAddressConfirmation" Src="../../AddressHygene/ExplicitAddressConfirmation.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AddressHygiene" Src="../../AddressHygene/AddressHygiene.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SampleKit.ascx.cs" Inherits="GA.BDC.WEB.ScratchcardWeb.Components.User.Controls.Sections.SampleKit" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="buttonpanel" Namespace="GA.BDC.Core.Web.UI.UIControls" Assembly="GA.BDC.Core.Web.UI.UIControls" %>
<script language="javascript" src="resources/jsScript/eFundWeb.js"></script>
<%
	Response.Write("<script language='javascript'>" + 
		"var fullNameContainer = '" + fullNameContainer + "';" + 
		"</script>");
%>
<script language="javascript">		
			
			var GlobalCountPropertyChanged = 0;
			
			function ValidateDropDown(source, argument) {
				var oCtrlName = source.id.replace('rfv','');
				var isValid = false;
				
				if(argument.Value == "0") {
					isValid = false;
					argument.IsValid = false;
					var oLabel = document.getElementById(source.id.replace('rfv','lbl'));
					//oLabel.style.color = 'red';
					oLabel.style.color = 'black';
				} else { 
					isValid = true;
					argument.IsValid = true;
					var oLabel = document.getElementById(source.id.replace('rfv','lbl'));
					oLabel.style.color = 'black';
				}
				return isValid;
			}
			
			function CheckBoxValidation(source) {
				
					var oSelectedMonth = ""; 
					//var oName = source.id.replace('chk',''));
					var oHiddenCtrl = document.getElementById(fullNameContainer + '_txtMonthList')
					oHiddenCtrl.value = "";
					var oVal = document.getElementById(fullNameContainer + '_rfvMonthList');
					var oLbl = document.getElementById(fullNameContainer + '_lblMonthList');
					for(var i=0;i<12;i++) {
						var oMonthCtrl = document.getElementById(source.id + '_' + i);
						if(oMonthCtrl.checked) {
							oHiddenCtrl.value += i + " ";
							oVal.IsValid = true;
							oVal.style.visibility = 'hidden';
							oLbl.style.color = 'black';
						}					
					}
					if(oHiddenCtrl.value == "") {			
						oVal.IsValid = false;
						oVal.style.visibility = 'visible';
					}
				
				//var oErrorListAnchor = document.getElementById("ErrorsList");
				//oErrorListAnchor.focus();
			}
			
			function UpdateLabel(source) {
				
					var name = source.id.replace('rfv','');
					var name2 = null;
					var oHasTwoCtrl = false;
					if(name.indexOf('2') >= 0) {
						name2 = name;
						name = name.replace('2','');
						oHasTwoCtrl = true;				
					}
					var txt = document.getElementById(source.id.replace('rfv','txt'));
					if(txt.value == "--")
						txt.value = "";
			
					if(document.getElementById(fullNameContainer + '_rfv' + name).style.visibility == "visible") {
						var oLabel = document.getElementById(fullNameContainer + '_lbl' + name);
						// oLabel.style.color = 'red';					
						oLabel.style.color = 'black';					
					} else if(document.getElementById(fullNameContainer + '_rfv' + name).style.visibility == 'hidden') {
						var oLabel = document.getElementById(fullNameContainer + '_lbl' + name);
						oLabel.style.color = 'black';					
					}
				
				
					if(oHasTwoCtrl && name2 != null) {
						var oVal = document.getElementById(fullNameContainer + '_rfv' + name2);
						if(oVal.style.visibility == "visible" && oVal.IsValid == null) {
							var oCtrl = document.getElementById(fullNameContainer + '_txt' + name);
							var oLabel = document.getElementById(fullNameContainer + '_lbl' + name);
							oVal.IsValid = false;
							//oLabel.style.color = 'red';
							oLabel.style.color = 'black';
						}
					}
				
			}
			
			function ResetControls(source, Event) {
				
					if((Event.keyCode == 8) && source.value == "") {
						var oCtrlName = source.id.replace('txt','');					
						var oLabel = document.getElementById(fullNameContainer + '_lbl' + oCtrlName);
						var oValidator = document.getElementById(fullNameContainer + '_rfv' + oCtrlName);
						oValidator.IsValid = false;
						oValidator.style.visibility = 'visible';
						oLabel.style.color = 'red';
					} else if(source.value != "") {
						var oCtrlName = null;
						if(source.id != null)
							oCtrlName = source.id.replace('txt','');
						else
							oCtrlName = source.replace('txt','');					
						var oLabel = document.getElementById(fullNameContainer + '_lbl' + oCtrlName);
						var oValidator = document.getElementById(fullNameContainer + '_rfv' + oCtrlName);
						oValidator.style.visibility = 'hidden';
						oLabel.style.color = 'black';
					}
				
			}
		
			var isNN = (navigator.appName.indexOf("Netscape")!=-1);
			function setPhone(pCtrlName, index, input,len, e) {
				
					var oCtrl = document.getElementById(fullNameContainer + "_" + pCtrlName);
					oCtrl.value = document.getElementById(fullNameContainer + "_" + pCtrlName + "1").value + "-" + 
									document.getElementById(fullNameContainer + "_" + pCtrlName + "2").value + "-" + 
									document.getElementById(fullNameContainer + "_" + pCtrlName + "3").value;	
					if(e.keyCode == 8) {
						oCtrl.value = document.getElementById(fullNameContainer + "_" + pCtrlName + "1").value + "-" + 
									document.getElementById(fullNameContainer + "_" + pCtrlName + "2").value + "-" + 
									document.getElementById(fullNameContainer + "_" + pCtrlName + "3").value;		
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
					
					document.getElementById(fullNameContainer + '_ReturnToTop').focus();				
			
			}
</script>
<A id="ReturnToTop" href="#"></A>
<table class="normal_text" width="100%" cellpadding="0" cellspacing="1">
	<tr>
		<td align="left" colSpan="4"><buttonpanel:contentpanelcontrol id="ContentPanelControl2" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Components\User\Controls\Sections\SampleKit.ascx"></buttonpanel:contentpanelcontrol><br>
			<asp:image id="Image1" runat="server" ImageUrl="../../../../Resources/images/_ScratchcardWeb_/_classic_/en-US/common/Title/doted_line.gif"></asp:image><br>
			<P></P>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="normal_text" colSpan="2">
						<P>
							<TABLE id="Table1" borderColor="#0" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD colSpan="2"><buttonpanel:contentpanelcontrol id="ContentPanelControl3" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Components\User\Controls\Sections\SampleKit.ascx"></buttonpanel:contentpanelcontrol></TD>
								</TR>
								<TR>
									<TD>
										<buttonpanel:contentpanelcontrol id="ContentPanelControl4" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Components\User\Controls\Sections\SampleKit.ascx"></buttonpanel:contentpanelcontrol><br>
										&nbsp;&nbsp;&nbsp;&nbsp;<IMG src="Resources/images/_ScratchcardWeb_/_classic_/en-US/forms/sponsors.gif"></TD>
									<TD><IMG src="Resources/images/_ScratchcardWeb_/_classic_/en-US/forms/sc_form.jpg"></TD>
								</TR>
								<TR>
									<TD colspan="2">
										<buttonpanel:contentpanelcontrol id="ContentPanelControl5" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Components\User\Controls\Sections\SampleKit.ascx"></buttonpanel:contentpanelcontrol>
									</TD>
								</TR>
							</TABLE>
						<p><asp:image id="Image2" runat="server" ImageUrl="../../../../Resources/images/_ScratchcardWeb_/_classic_/en-US/common/Title/doted_line.gif"></asp:image></p>
					</td>
				</tr>
				<tr>
					<td colSpan="2"><asp:validationsummary id="Validationsummary1" onpropertychange="document.getElementById('ReturnToTop').focus();"
							DisplayMode="List" HeaderText=" " CssClass="error" runat="server"></asp:validationsummary>
							<uc1:AddressHygiene id="AddressHygiene1" runat="server"></uc1:AddressHygiene>
						<uc1:ExplicitAddressConfirmation id="ExplicitAddressConfirmation1" runat="server"></uc1:ExplicitAddressConfirmation></td>
				</tr>
				<tr>
					<td class="normal_text" align="right" colSpan="2"><buttonpanel:contentpanelcontrol id="ContentPanelControl1" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Components\User\Controls\Sections\SampleKit.ascx"></buttonpanel:contentpanelcontrol></td>
				</tr>
			</table>
			<br>
		</td>
	</tr>
	<tr>
		<td style="HEIGHT: 14px" align="center" colSpan="5">&nbsp;
			<buttonpanel:contentpanelcontrol id="ContentPanelControl6" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Components\User\Controls\Sections\SampleKit.ascx"></buttonpanel:contentpanelcontrol></td>
	</tr>
	<tr>
		<td style="WIDTH: 5px"></td>
		<td></td>
		<td class="SmallText" style="WIDTH: 582px"><asp:label id="lblFirstName" runat="server">First Name</asp:label>*</td>
		<td><asp:textbox id="txtFirstName" onkeyup="ResetControls(this, event);" runat="server" MaxLength="50"></asp:textbox></td>
		<td align="left"><asp:requiredfieldvalidator id="rfvFirstName" onpropertychange="UpdateLabel(this);" CssClass="Error" runat="server"
				ControlToValidate="txtFirstName" ErrorMessage="You have to enter your First Name">*</asp:requiredfieldvalidator></td>
	</tr>
	<tr>
		<td style="WIDTH: 5px"></td>
		<td></td>
		<td class="SmallText" style="WIDTH: 582px; HEIGHT: 26px"><asp:label id="lblLastName" runat="server">Last Name</asp:label>*</td>
		<td style="HEIGHT: 26px"><asp:textbox id="txtLastName" onkeyup="ResetControls(this, event);" runat="server" MaxLength="50"></asp:textbox></td>
		<td style="HEIGHT: 26px" align="left"><asp:requiredfieldvalidator id="rfvLastName" onpropertychange="UpdateLabel(this);" CssClass="Error" runat="server"
				ControlToValidate="txtLastName" ErrorMessage="You have to enter your Last name" Text="*"></asp:requiredfieldvalidator></td>
	</tr>
	<tr>
		<td style="WIDTH: 5px"></td>
		<td></td>
		<td class="SmallText" style="WIDTH: 582px"><asp:label id="lblTitle" runat="server">Title</asp:label></td>
		<td><asp:dropdownlist id="ddlTitle" runat="server" DataTextField="TitleDescription" DataValueField="TitleId"></asp:dropdownlist></td>
		<td align="center"></td>
	</tr>
	<tr>
		<td style="WIDTH: 5px"></td>
		<td></td>
		<td class="SmallText" style="WIDTH: 582px"><asp:label id="lblEmail" runat="server">E-Mail Address</asp:label>*</td>
		<td><asp:textbox id="txtEmail" onkeyup="ResetControls(this, event);" runat="server" MaxLength="50"></asp:textbox></td>
		<td align="left"><asp:regularexpressionvalidator id="rfvEmail" onpropertychange="" CssClass="Error" runat="server" ControlToValidate="txtEmail"
				ErrorMessage="You have to enter your E-Mail Address" ValidationExpression="^[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z0-9]{1,6}$">*</asp:regularexpressionvalidator><br>
			<asp:requiredfieldvalidator id="rfvEmail2" onpropertychange="UpdateLabel(this);" CssClass="Error" runat="server"
				ControlToValidate="txtEmail" ErrorMessage="You have to enter your E-Mail Address">*</asp:requiredfieldvalidator></td>
	</tr>
	<tr>
		<td style="WIDTH: 5px"></td>
		<td></td>
		<td class="SmallText" style="WIDTH: 582px"><asp:label id="lblGroupName" runat="server">Group Name</asp:label>*</td>
		<td><asp:textbox id="txtGroupName" onkeyup="ResetControls(this, event);" runat="server" MaxLength="50"></asp:textbox></td>
		<td align="left"><asp:requiredfieldvalidator id="rfvGroupName" onpropertychange="UpdateLabel(this);" CssClass="Error" runat="server"
				ControlToValidate="txtGroupName" ErrorMessage="You have to provide your group's name" Text="*">*</asp:requiredfieldvalidator></td>
	</tr>
	<tr>
		<td style="WIDTH: 5px"></td>
		<td></td>
		<td class="SmallText" style="WIDTH: 582px"><asp:label id="lblAddress" runat="server">Address</asp:label>*</td>
		<td><asp:textbox id="txtAddress" onkeyup="ResetControls(this, event);" runat="server" MaxLength="50"></asp:textbox></td>
		<td align="left"><asp:requiredfieldvalidator id="rfvAddress" onpropertychange="UpdateLabel(this);" CssClass="Error" runat="server"
				ControlToValidate="txtAddress" ErrorMessage="Address is invalid" Text="*"></asp:requiredfieldvalidator></td>
	</tr>
	<tr>
		<td style="WIDTH: 5px"></td>
		<td></td>
		<td class="SmallText" style="WIDTH: 582px"><asp:label id="lblCity" runat="server">City</asp:label>*</td>
		<td><asp:textbox id="txtCity" onkeyup="ResetControls(this, event);" runat="server" MaxLength="50"></asp:textbox></td>
		<td align="left"><asp:requiredfieldvalidator id="rfvCity" onpropertychange="UpdateLabel(this);" CssClass="Error" runat="server"
				ControlToValidate="txtCity" ErrorMessage="The city is invalid" Text="*"></asp:requiredfieldvalidator></td>
	</tr>
	<tr>
		<td style="WIDTH: 5px"></td>
		<td></td>
		<td class="SmallText" style="WIDTH: 582px; HEIGHT: 27px"><asp:label id="lblCountry" runat="server">Country</asp:label>*</td>
		<TD style="HEIGHT: 27px"><asp:dropdownlist id="ddlCountry" runat="server" DataTextField="CountryName" DataValueField="CountryCode"
				AutoPostBack="True"></asp:dropdownlist></TD>
		<TD style="HEIGHT: 27px" align="left"><asp:customvalidator id="rfvTitle" CssClass="Error" runat="server" ControlToValidate="ddlCountry" ErrorMessage="You have to provide your group's name"
				ClientValidationFunction="ValidateDropDown">*</asp:customvalidator></TD>
	</tr>
	<tr>
		<td style="WIDTH: 5px"></td>
		<td></td>
		<td class="SmallText" style="WIDTH: 582px"><asp:label id="lblState" runat="server">State / Province</asp:label>*</td>
		<TD><asp:dropdownlist id="ddlState" runat="server" DataTextField="StateName" DataValueField="StateCode"></asp:dropdownlist></TD>
		<td align="left"><asp:customvalidator id="rfvState" onpropertychange="UpdateLabel(this);" CssClass="Error" runat="server"
				ControlToValidate="ddlState" ErrorMessage="The State / Province is invalid" ClientValidationFunction="ValidateDropDown">*</asp:customvalidator></td>
	</tr>
	<tr>
		<td style="WIDTH: 5px"></td>
		<td></td>
		<td class="SmallText" style="WIDTH: 582px"><asp:label id="lblZipCode" runat="server">Zip / Postal Code</asp:label>*</td>
		<td><asp:textbox id="txtZipCode" onkeyup="ResetControls(this, event);" runat="server" MaxLength="25"></asp:textbox></td>
		<td align="left"><asp:requiredfieldvalidator id="rfvZipCode" onpropertychange="UpdateLabel(this);" CssClass="Error" runat="server"
				ControlToValidate="txtZipCode" ErrorMessage="You have to enter your Zip / Postal Code" Text="*"></asp:requiredfieldvalidator></td>
	</tr>
	<tr>
		<td style="WIDTH: 5px"></td>
		<td></td>
		<td class="SmallText" style="WIDTH: 582px; HEIGHT: 26px" vAlign="top"><asp:label id="lblDayPhone" runat="server">Day Phone; incl. area code</asp:label>*</td>
		<td style="HEIGHT: 26px"><asp:textbox onkeypress="ResetControls('txtDayPhone', event);" id="txtDayPhone1" onkeyup="return setPhone('txtDayPhone',1,this,3,event);"
				runat="server" MaxLength="3" Width="30px" size="3"></asp:textbox>-<asp:textbox onkeypress="ResetControls('txtDayPhone', event);" id="txtDayPhone2" onkeyup="return setPhone('txtDayPhone',2,this,3,event);"
				runat="server" MaxLength="3" Width="30px" size="3"></asp:textbox>-<asp:textbox onkeypress="ResetControls('txtDayPhone', event);" id="txtDayPhone3" onkeyup="return setPhone('txtDayPhone',3, this, 4, event);"
				runat="server" MaxLength="4" Width="35px" size="4"></asp:textbox>Ext:
			<asp:textbox id="txtDayPhnExt" runat="server" MaxLength="4" Width="35px" size="4"></asp:textbox><asp:textbox id="txtDayPhone" runat="server" Width="111px"></asp:textbox></td>
		<td align="left"><asp:regularexpressionvalidator id="rfvDayPhone" onpropertychange="" CssClass="Error" runat="server" ControlToValidate="txtDayPhone"
				ErrorMessage="Day Phone Number is invalid" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}">*</asp:regularexpressionvalidator><br>
			<asp:requiredfieldvalidator id="rfvDayPhone2" onpropertychange="UpdateLabel(this);" CssClass="Error" runat="server"
				ControlToValidate="txtDayPhone" ErrorMessage="Day Phone Number is invalid" Text="*">*</asp:requiredfieldvalidator></td>
	</tr>
	<tr>
		<td style="WIDTH: 5px"></td>
		<td></td>
		<td class="SmallText" style="WIDTH: 582px" vAlign="top"><asp:label id="lblEvnPhone" runat="server">Evening Phone; incl. area code</asp:label></td>
		<td><asp:textbox onkeypress="ResetControls(this, event);" id="txtEvnPhone1" onkeyup="return setPhone('txtEvnPhone',1, this, 3, event);"
				runat="server" MaxLength="3" Width="30px" size="3"></asp:textbox>-<asp:textbox id="txtEvnPhone2" onkeyup="return setPhone('txtEvnPhone',2, this, 3, event);" runat="server"
				MaxLength="3" Width="30px" size="3"></asp:textbox>-<asp:textbox id="txtEvnPhone3" onkeyup="return setPhone('txtEvnPhone',3, this, 4, event);" runat="server"
				MaxLength="4" Width="35px" size="4"></asp:textbox>Ext:
			<asp:textbox id="txtEvnPhnExt" runat="server" MaxLength="4" Width="35px" size="4"></asp:textbox><asp:textbox id="txtEvnPhone" runat="server" Width="112px"></asp:textbox></td>
		<td align="left"></td>
	</tr>
	<tr>
		<td style="WIDTH: 5px"></td>
		<td></td>
		<td class="SmallText" style="WIDTH: 582px"><asp:label id="lblBestTimeToCall" runat="server">Best Time to Call</asp:label>*</td>
		<td><asp:dropdownlist id="ddlBestTimeToCall" runat="server" DataTextField="TimeToCallDescription" DataValueField="TimeToCallValue"></asp:dropdownlist></td>
		<td align="left"><asp:customvalidator id="rfvBestTimeToCall" onpropertychange="UpdateLabel(this);" CssClass="Error" runat="server"
				ControlToValidate="ddlBestTimeToCall" ErrorMessage="You have to choose an option for the Best Time to Call" ClientValidationFunction="ValidateDropDown">*</asp:customvalidator></td>
	</tr>
	<tr height="100">
		<td align="center" colSpan="5"><buttonpanel:contentpanelcontrol id="ContentPanelControl7" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Components\User\Controls\Sections\SampleKit.ascx"></buttonpanel:contentpanelcontrol></td>
	</tr>
	<tr>
		<td style="WIDTH: 5px"></td>
		<td></td>
		<td class="SmallText" style="WIDTH: 582px"><asp:label id="lblOrganization" runat="server">Organization Type</asp:label>*</td>
		<td><asp:dropdownlist id="ddlOrgType" runat="server" DataTextField="OrganizationDescription" DataValueField="OrganizationId"></asp:dropdownlist></td>
		<td align="left"><asp:customvalidator id="rfvOrganization" onpropertychange="UpdateLabel(this);" CssClass="Error" runat="server"
				ControlToValidate="ddlOrgType" ErrorMessage="Please select an organization" ClientValidationFunction="ValidateDropDown">*</asp:customvalidator></td>
	</tr>
	<tr>
		<td style="WIDTH: 5px"></td>
		<td></td>
		<td class="SmallText" style="WIDTH: 582px; HEIGHT: 18px"><asp:label id="lblGroupType" runat="server">Group Type</asp:label>*</td>
		<td style="HEIGHT: 18px"><asp:dropdownlist id="ddlGroupType" runat="server" DataTextField="Description" DataValueField="GroupTypeId"></asp:dropdownlist></td>
		<td style="HEIGHT: 18px" align="left"><asp:customvalidator id="rfvGroupType" onpropertychange="UpdateLabel(this);" CssClass="Error" runat="server"
				ControlToValidate="ddlGroupType" ErrorMessage="Please select a group category" ClientValidationFunction="ValidateDropDown">*</asp:customvalidator></td>
	</tr>
	<tr>
		<td style="WIDTH: 5px"></td>
		<td></td>
		<td class="SmallText" style="WIDTH: 582px"><asp:label id="lblGroupNumber" runat="server">Number of Group Members</asp:label>*</td>
		<td><asp:textbox id="txtGroupNumber" onkeyup="ResetControls(this, event);" runat="server" MaxLength="5"></asp:textbox></td>
		<td align="left"><asp:regularexpressionvalidator id="rfvGroupNumber" onpropertychange="" CssClass="Error" runat="server" ControlToValidate="txtGroupNumber"
				ErrorMessage="Please specify your group's size" ValidationExpression="^[1-9]\d*">*</asp:regularexpressionvalidator><br>
			<asp:requiredfieldvalidator id="rfvGroupNumber2" onpropertychange="UpdateLabel(this);" CssClass="Error" runat="server"
				ControlToValidate="txtGroupNumber" ErrorMessage="Please specify your group's size">*</asp:requiredfieldvalidator></td>
	</tr>
	<tr>
		<td style="WIDTH: 5px"></td>
		<td></td>
		<td class="SmallText" vAlign="top" colSpan="2"><asp:label id="lblMonthList" runat="server">Months during which your group typically raises funds<br> (you may choose more than one)</asp:label>*</td>
		<td vAlign="top" align="left"><asp:requiredfieldvalidator id="rfvMonthList" onpropertychange="UpdateLabel(this);" CssClass="Error" runat="server"
				ControlToValidate="txtMonthList" ErrorMessage="Please select a date for your fundraiser to start">*</asp:requiredfieldvalidator></td>
	</tr>
	<tr>
		<td style="WIDTH: 5px" vAlign="top"></td>
		<td></td>
		<td colSpan="3"><asp:checkboxlist id="chkMonthList" onclick="CheckBoxValidation(this);" CssClass="normal_text" runat="server"
				RepeatColumns="4" RepeatDirection="Horizontal">
				<asp:ListItem Value="January">January</asp:ListItem>
				<asp:ListItem Value="February">February</asp:ListItem>
				<asp:ListItem Value="March">March</asp:ListItem>
				<asp:ListItem Value="April">April</asp:ListItem>
				<asp:ListItem Value="May">May</asp:ListItem>
				<asp:ListItem Value="June">June</asp:ListItem>
				<asp:ListItem Value="July">July</asp:ListItem>
				<asp:ListItem Value="August">August</asp:ListItem>
				<asp:ListItem Value="September">September</asp:ListItem>
				<asp:ListItem Value="October">October</asp:ListItem>
				<asp:ListItem Value="November">November</asp:ListItem>
				<asp:ListItem Value="December">December</asp:ListItem>
			</asp:checkboxlist></td>
	</tr>
	<tr>
		<td vAlign="middle"></td>
		<td></td>
		<td colSpan="2">
			<table style="WIDTH: 312px; HEIGHT: 42px" height="42" cellSpacing="0" cellPadding="0" border="0">
				<tr class="normal_text">
					<td class="SmallText" style="WIDTH: 198px" vAlign="middle"><asp:label id="lblDecision" runat="server">Are You the Decision Maker ?</asp:label>*</td>
					<td vAlign="middle" align="center"><asp:radiobuttonlist id="rdbDecision" CssClass="SmallText" runat="server" RepeatColumns="2">
							<asp:ListItem Value="true">Yes</asp:ListItem>
							<asp:ListItem Value="false">No</asp:ListItem>
						</asp:radiobuttonlist></td>
				</tr>
			</table>
		</td>
		<td vAlign="top" align="left"><br>
			<asp:requiredfieldvalidator id="rfvDecision" onpropertychange="UpdateLabel(this);" CssClass="Error" runat="server"
				ControlToValidate="rdbDecision" ErrorMessage="Please specify your role in choosing a fundraising option"
				Text="*"></asp:requiredfieldvalidator></td>
	</tr>
	<tr>
		<td style="WIDTH: 5px"></td>
		<td></td>
		<td class="SmallText" colSpan="2">Yes, please send me my monthly eNewsLetter!
			<asp:checkbox id="chkNewsLetter" runat="server" Checked="True"></asp:checkbox></td>
	</tr>
	<tr>
		<td style="WIDTH: 5px"></td>
		<td></td>
		<td class="SmallText" style="HEIGHT: 4px" colSpan="2">Comments or Questions</td>
	</tr>
	<tr>
		<td style="WIDTH: 5px"></td>
		<td></td>
		<td style="HEIGHT: 120px" align="center" colSpan="2"><asp:textbox id="txtComment" runat="server" MaxLength="200" Width="352px" Height="114px" TextMode="MultiLine"></asp:textbox></td>
	</tr>
	<tr>
		<td style="WIDTH: 5px; HEIGHT: 25px"></td>
		<td style="HEIGHT: 25px"></td>
		<td style="HEIGHT: 25px" align="center" colSpan="2"><buttonpanel:buttonpanelcontrol id="imgSendFreeKit" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Components\User\Controls\Sections\SampleKit.ascx"
				CausesValidation="True" ButtonType="IMAGE" CodeName="imgFreeKitSubmit"></buttonpanel:buttonpanelcontrol></td>
	</tr>
</table>
<buttonpanel:pagepanelcontrol id="PagePanelControl1" style="Z-INDEX: 101; LEFT: 616px; POSITION: absolute; TOP: 288px"
	runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\Components\User\Controls\Sections\SampleKit.ascx"></buttonpanel:pagepanelcontrol><asp:textbox id="txtMonthList" style="VISIBILITY: hidden" runat="server"></asp:textbox>
<script language="javascript">
		HideFullPhone();
</script>
