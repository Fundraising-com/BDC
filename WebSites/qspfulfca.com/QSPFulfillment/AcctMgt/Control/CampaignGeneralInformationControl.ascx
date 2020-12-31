<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="CampaignGeneralInformationControl.ascx.cs" Inherits="QSPFulfillment.AcctMgt.Control.CampaignGeneralInformationControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="DatePicker" Src="../../Common/DateEntry.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AddressViewerControl" Src="AddressViewerControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AccountIDPickerControl" Src="AccountIDPickerControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ContactMaintenanceControl" Src="ContactMaintenanceControl.ascx" %>
<style type="text/css">
    .style1
    {
        width: 17%;
        height: 26px;
    }
    .style2
    {
        width: 64.69%;
        height: 26px;
    }
    .style3
    {
        width: 16%;
        height: 26px;
    }
</style>
<table id="Table3" cellspacing="0" cellpadding="0" width="100%" bgcolor="#cecece" border="0">
	<tr>
		<td>
			<table class="CSTable" id="Table4" cellspacing="1" cellpadding="2" width="100%">
				<tr>
					<td valign="top" height="20"><asp:label id="lblTitle2" cssclass="CSTitle" runat="server">General Campaign Information</asp:label></td>
				</tr>
				<tr bgcolor="#ffffff">
					<td valign="top">
						<table id="Table1" cellspacing="0" cellpadding="2" width="896" border="0" style="WIDTH: 896px; HEIGHT: 267px">
							<tr>
								<td style="WIDTH: 17%"><asp:label id="Label4" cssclass="CSPlainText" runat="server">FM *</asp:label></td>
								<td style="WIDTH: 17%"><cc1:dropdownlistreq id="ddlFieldManager" runat="server" initialtext="Please select..." clientscript="False"></cc1:dropdownlistreq></td>
								<td style="WIDTH: 17%"><asp:label id="Label18" cssclass="csPlainText" runat="server">Date Submitted</asp:label></td>
								<td style="WIDTH: 17%"><uc1:datepicker id="dteDateSubmitted" runat="server"></uc1:datepicker></td>
								<td style="WIDTH: 64.69%"><asp:label id="Label17" cssclass="CSPlainText" runat="server">Renewal</asp:label></td>
								<td style="WIDTH: 16%"><cc1:radiobuttonlistitemattributes id="rblRenewalStatus" cssclass="csPlainText" runat="server" repeatdirection="Horizontal">
										<asp:listitem value="False" selected="True">New</asp:listitem>
										<asp:listitem value="True">Renewal</asp:listitem>
									</cc1:radiobuttonlistitemattributes></td>
							</tr>
							<tr>
								<td style="WIDTH: 17%"><asp:label id="Label9" cssclass="CSPlainText" runat="server"> Start *</asp:label></td>
								<td style="WIDTH: 17%"><uc1:datepicker id="dteStartDate" runat="server" autopostback="True"></uc1:datepicker></td>
								<td style="WIDTH: 17%"><asp:label id="Label10" cssclass="CSPlainText" runat="server">End *</asp:label></td>
								<td style="WIDTH: 17%"><uc1:datepicker id="dteEndDate" runat="server"></uc1:datepicker></td>
								<td style="WIDTH: 64.69%"><asp:label id="Label11" cssclass="CSPlainText" runat="server"> Est Gross $</asp:label></td>
								<td style="WIDTH: 16%"><cc1:textboxfloat id="tbxEstimatedGross" runat="server" errormsgrequired="The field Estimated Gross is mandatory."
										errormsgregexp="The field Estimated Gross has to be a number." maxlength="25"></cc1:textboxfloat></td>
							</tr>
							<tr>
								<td style="WIDTH: 17%"><asp:label id="Label12" cssclass="CSPlainText" runat="server"> Participants *</asp:label></td>
								<td style="WIDTH: 17%"><cc1:textboxinteger id="tbxNumberOfParticipants" runat="server" errormsgregexp="The field Number of participants has to be a number."
										maxlength="25"></cc1:textboxinteger></td>
								<td style="WIDTH: 17%"><asp:label id="Label13" cssclass="CSPlainText" runat="server"> Classrooms *</asp:label></td>
								<td style="WIDTH: 17%"><cc1:textboxinteger id="tbxNumberOfClassrooms" runat="server" errormsgregexp="The field Number of classrooms has to be a number."
										maxlength="25"></cc1:textboxinteger></td>
								<td style="WIDTH: 64.69%"><asp:label id="Label14" cssclass="CSPlainText" runat="server"> Staff *</asp:label></td>
								<td style="WIDTH: 16%"><cc1:textboxinteger id="tbxNumberOfStaff" runat="server" errormsgregexp="The field Number of staff has to be a number."
										maxlength="25"></cc1:textboxinteger></td>
							</tr>
							<tr>
								<td style="WIDTH: 17%"><asp:label id="Label7" cssclass="CSPlainText" runat="server">Staff Order</asp:label></td>
								<td style="WIDTH: 17%"><cc1:radiobuttonlistitemattributes id="rblStaffOrder" cssclass="csPlainText" runat="server" repeatdirection="Horizontal">
										<asp:listitem value="True">Yes</asp:listitem>
										<asp:listitem value="False" selected="True">No</asp:listitem>
									</cc1:radiobuttonlistitemattributes></td>
								<td style="WIDTH: 17%"><asp:label id="Label1s" cssclass="CSPlainText" runat="server">Bill Incentives to</asp:label></td>
								<td style="WIDTH: 17%"><cc1:dropdownlistreq id="ddlIncentivesBillTo" runat="server" initialtext="Please select..." initialvalue="0"></cc1:dropdownlistreq></td>
								<td style="WIDTH: 64.69%">
									<asp:label id="Label5" runat="server" cssclass="CSPlainText">Incentives Distr.</asp:label></td>
								<td style="WIDTH: 16%"><cc1:dropdownlistreq id="ddlIncentivesDistribution" runat="server" initialtext="Please select..." initialvalue="0"></cc1:dropdownlistreq></td>
							<TR>
								<TD style="WIDTH: 17%"><asp:label id="Label15" cssclass="CSPlainText" runat="server">Language *</asp:label></TD>
								<TD style="WIDTH: 17%"><cc1:dropdownlistreq id="ddlLanguage" runat="server">
										<asp:listitem value="  " selected="True">Please select...</asp:listitem>
										<asp:listitem value="EN">EN</asp:listitem>
										<asp:listitem value="FR">FR</asp:listitem>
									</cc1:dropdownlistreq></TD>
								<TD style="WIDTH: 17%"><asp:label id="Label2" cssclass="CSPlainText" runat="server">Status</asp:label></TD>
								<TD style="WIDTH: 17%"><cc1:dropdownlistreq id="ddlStatus" runat="server" initialtext="Please select..." errormsgrequired="The field Status is mandatory."
										initialvalue="-1"></cc1:dropdownlistreq></TD>
								<TD style="WIDTH: 64.69%">
									<asp:Label id="Label6" runat="server" Width="64px" CssClass="CSPlainText">Extra 1UPs</asp:Label></TD>
								<TD style="WIDTH: 16%">
									<cc1:textboxinteger id="tbxExtra1ups" runat="server" errormsgregexp="The field Extra 1UPS has to be a number."
										Width="64px"></cc1:textboxinteger></TD>
							</TR>
							<TR>
								<TD class="style1">
									<asp:label id="Label19" runat="server" cssclass="CSPlainText">Online Only Programs*</asp:label></TD>
								<TD class="style1">
									<cc1:radiobuttonlistitemattributes id="rblOnlineOnly" runat="server" cssclass="csPlainText" repeatdirection="Horizontal"
										autopostback="True">
										<asp:ListItem Value="True">Yes</asp:ListItem>
										<asp:ListItem Value="False" Selected="True">No</asp:ListItem>
									</cc1:radiobuttonlistitemattributes></TD>
								<TD class="style1">
									<asp:label id="Label1" cssclass="CSPlainText" runat="server">Cookie Dough Delivery Date</asp:label></TD>
								<TD class="style1">
                                    <uc1:datepicker id="dteCookieDoughDeliveryDate" runat="server"></uc1:datepicker>
                                </TD>
								<TD class="style2">
									<asp:Label id="ExtraGiftFormsLabel" runat="server" CssClass="CSPlainText">Extra Gift Forms</asp:Label></TD>
								<TD class="style3">
									<cc1:textboxinteger id="tbxExtraGiftForm" runat="server" errormsgregexp="The field Extra Gift Form has to be a number."
										Width="64px"></cc1:textboxinteger></TD>
							</TR>
							<tr>
								<td class="style1"><asp:label id="lblOnlineNutFree" runat="server" cssclass="CSPlainText">Online Nut Free</asp:label></td>
								<td class="style1">
									<cc1:radiobuttonlistitemattributes id="rblOnlineNutFree" runat="server" cssclass="csPlainText" repeatdirection="Horizontal"
										autopostback="True">
										<asp:ListItem Value="True">Yes</asp:ListItem>
										<asp:ListItem Value="False" Selected="True">No</asp:ListItem>
									</cc1:radiobuttonlistitemattributes></td>
								<td style="WIDTH: 17%; HEIGHT: 9px">
								<td style="WIDTH: 17%; HEIGHT: 9px">
                                </td>
								<td style="WIDTH: 64.69%">
									<asp:Label id="Label20" runat="server" CssClass="CSPlainText">Extra Mag Brochures</asp:Label></td>
								<td style="WIDTH: 16%" valign="bottom">
									<cc1:textboxinteger id="tbxExtraMagBrochure" runat="server" errormsgregexp="The field Extra Mag Brochure has to be a number."
										Width="64px"></cc1:textboxinteger></td>
							</tr>
                            <tr>
								<td class="style1"><asp:label id="lblOnlineMagazineTRTOnly" runat="server" cssclass="CSPlainText">Online Magazine Only</asp:label></td></td>
								<td class="style1">
									<cc1:radiobuttonlistitemattributes id="rblOnlineMagazineTRTOnly" runat="server" cssclass="csPlainText" repeatdirection="Horizontal"
										autopostback="True">
										<asp:ListItem Value="True">Yes</asp:ListItem>
										<asp:ListItem Value="False" Selected="True">No</asp:ListItem>
									</cc1:radiobuttonlistitemattributes></td>
                                <td style="WIDTH: 17%;">
									<asp:label id="ForceStatementPrintLabel" runat="server" cssclass="CSPlainText">Force Statement</asp:label></td>
                                <td style="WIDTH: 17%;">
                                    <asp:CheckBox ID="ForceStatementPrintCheckBox" runat="server" />
                                </td>
								<td style="WIDTH: 64.69%">
									<asp:Label id="Label8" runat="server" CssClass="CSPlainText">Cool Cards Boxes</asp:Label></td>
								<td style="WIDTH: 16%" valign="bottom">
									<cc1:textboxinteger id="tbxCoolCardsBoxes" runat="server" errormsgregexp="The field Cool Cards Boxes has to be a number."
										Width="64px"></cc1:textboxinteger></td>
                                </td>
                            </tr>
                            <tr>
								<td class="style1">&nbsp;</td>
								<td class="style1">
									&nbsp;</td>
                                <td style="WIDTH: 17%;">
									<asp:label id="DisableStatementPrintLabel" runat="server" cssclass="CSPlainText">Disable Statement</asp:label></td>
                                <td style="WIDTH: 17%;">
                                    <asp:CheckBox ID="DisableStatementPrintCheckBox" runat="server" />
                                </td>
                                <td style="WIDTH: 16%;  HEIGHT: 9px">&nbsp;</td>
                                <td class="style1">
                                    &nbsp;</td>
                            </tr>
							<tr>
								<td style="WIDTH: 17%; HEIGHT: 9px"><uc1:addressviewercontrol id="ctrlAddressViewerControlShipTo" runat="server"></uc1:addressviewercontrol></td>
								<td style="WIDTH: 17%; HEIGHT: 9px"></td>
								<td style="WIDTH: 17%; HEIGHT: 9px"><uc1:addressviewercontrol id="ctrlAddressViewerControlBillTo" runat="server"></uc1:addressviewercontrol></td>
								<td style="WIDTH: 17%; HEIGHT: 9px"></td>
								<td style="WIDTH: 64.69%"><asp:label id="Label16" cssclass="CSPlainText" runat="server">Special Instructions</asp:label></td>
								<td style="WIDTH: 16%" valign="bottom"><asp:textbox id="tbxSpecialInstructions" runat="server" maxlength="1000" textmode="MultiLine"
										width="100%" height="85px"></asp:textbox></td>
							</tr>
							<tr>
								<td valign="top" colspan="2"></td>
								<td valign="top" colspan="2"></td>
								<td style="WIDTH: 64.69%">&nbsp;</td>
								<td style="WIDTH: 16%" valign="bottom">&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
<br>
<br>
<table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
	<tr>
		<td style="width:49%;padding-right:1%">
			<table class="CSTable" id="Table4" cellspacing="1" cellpadding="2" width="100%">
				<tr bgcolor="#cecece">
					<td valign="top" height="20"><asp:label id="Label3" cssclass="CSTitle"  runat="server">Campaign Contact Information</asp:label></td>
				</tr>
				<tr bgcolor="#ffffff" style="height:216px!important;" >
					<td valign="top">
						<table id="Table1" cellspacing="0" cellpadding="2" width="100%" border="0">
							<tr>
								<td><uc1:contactmaintenancecontrol id="ctrlContactMaintenanceControl" runat="server" addressclientvisible="False" required="False"
										isphonerequired="False" showonephone="true" layout="Horizontal" removebuttonvisible="False"></uc1:contactmaintenancecontrol></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
        <td style="width:49%;vertical-align:top;padding-left:1%;" runat="server" id="tdNotes">
            <table class="CSTable" id="Table2" cellspacing="1" cellpadding="2" width="100%" >
				<tr bgcolor="#cecece">
					<td valign="top" height="20"><asp:label id="Label21" cssclass="CSTitle" runat="server">Notes</asp:label></td>
				</tr>
				<tr bgcolor="#ffffff" style="height:216px!important;" >
					<td valign="top">
						<table id="Table5" cellspacing="0" cellpadding="2" width="100%" border="0">
							<tr>
                                <td style="width:10%;padding:10px;vertical-align:top;" ><asp:label id="Label22" cssclass="CSPlainText" runat="server">Notes</asp:label></td>
								<td style="width:90%;padding:6px;"><asp:TextBox id="tbxNotes" runat="server" maxlength="4000" textmode="MultiLine"
										width="80%" height="193px" margin-top="20px!important"></asp:TextBox></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
            </td>
	</tr>
</table>
