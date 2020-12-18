<%@ Control Language="C#" AutoEventWireup="true" Inherits="QSP.OrderExpress.Web.UserControls.CouponHeaderForm" Codebehind="CouponHeaderForm.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="SubdivisionSelector" Src="SubdivisionSelector.ascx" %>
<%@ Register TagPrefix="uc1" TagName="promo_logo_text_selector" Src="promo_logo_text_selector.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td align="left" style="width: 85px" colspan=2> <!--Section Title --></td>
	</tr>
	<tr>
		<td align="left" style="width: 85px" colspan=2>
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="600" border="0">
				<TBODY>
					<TR>
						<TD class="SectionPageTitleInfo" colSpan="2"><asp:label id="lblTitle" runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 215px" vAlign="top"><SPAN class="StandardLabel">Countract 
								Number&nbsp;:</SPAN>
						</TD>
						<TD vAlign="top"><asp:label id="lblID" runat="server" CssClass="DescInfoLabel" Width="200px"></asp:label></TD>
					</TR>
					<TR id="trNational" runat="server">
	                    <TD style="WIDTH: 85px" vAlign="top"><SPAN class="StandardLabel">National&nbsp;:</SPAN>
	                    </TD>
	                    <TD vAlign="top" width="100%" align="left"><asp:checkbox id="chkNational" Runat="server"></asp:checkbox></TD>
                    </TR>
					<tr id="TrVendorInfo" style="DISPLAY: none" runat="server">
						<td style="WIDTH: 126px"><asp:label id="Label2" runat="server" CssClass="StandardLabel">
											Vendor:&nbsp;
										</asp:label></td>
						<td width="100%"><asp:label id="lblVendorInfo" runat="server" CssClass="DescInfoLabel"></asp:label></td>
					</tr>			
					<TR id="TrVendorEdit" style="DISPLAY: none" vAlign="top" runat="server">
						<TD style="WIDTH: 126px"><asp:label id="Label3" runat="server" CssClass="StandardLabel">
											Vendor:&nbsp;
										</asp:label></TD>
						<TD align="right">
							<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="400" border="0">
								<TR align="right">
									<TD><asp:textbox id="txtVendorID" runat="server" Width="50px" Enabled="True"></asp:textbox></TD>
									<TD style="width: 10px"><asp:comparevalidator id="Comparevalidator1" runat="server" CssClass="LabelError" Operator="DataTypeCheck"
											Type="Integer" ErrorMessage="The Vendor ID is invalid (must be a number)." ControlToValidate="txtVendorID">*</asp:comparevalidator></TD>
									<TD>&nbsp;
										<asp:textbox id="txtVendorName" runat="server" Width="230px" Enabled="True"></asp:textbox></TD>
									<td><asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" CssClass="LabelError" Enabled="False"
											ErrorMessage="The vendor is required." ControlToValidate="txtVendorID">*</asp:requiredfieldvalidator></td>
									<TD vAlign="top" align="right"><asp:imagebutton id="imgBtnSelectVendor" runat="server" CausesValidation="False" ImageUrl="~/images/BtnSelect.gif"></asp:imagebutton></TD>
									<TD vAlign="top" align="right"><IMG style="CURSOR: hand" onclick="ResetVendor();" src="images/BtnDelete.gif">
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<tr id="trFmInfo" style="DISPLAY: none" runat="server">
						<td style="WIDTH: 142px"><asp:label id="Label11" runat="server" CssClass="StandardLabel">
															Field&nbsp;Sales&nbsp;Manager:&nbsp;
														</asp:label></td>
						<td width="100%"><asp:label id="lblFMInfo" runat="server" CssClass="DescInfoLabel"></asp:label></td>
					</tr>
					<TR id="trFmEdit" style="DISPLAY: none" vAlign="top" runat="server">
						<TD style="WIDTH: 142px"><asp:label id="lblLabelFM" runat="server" CssClass="StandardLabel">
															Field&nbsp;Sales&nbsp;Manager:&nbsp;
														</asp:label></TD>
						<TD>
							<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="400" border="0">
								<TR>
									<TD><asp:textbox id="txtFMID" runat="server" Width="50px" Enabled="True"></asp:textbox></TD>
									<TD><asp:comparevalidator id="CompValFMID" runat="server" CssClass="LabelError" Operator="DataTypeCheck" Type="Integer"
											ErrorMessage="The FM ID is invalid (must be a number)." ControlToValidate="txtFMID">*</asp:comparevalidator></TD>
									<TD>&nbsp;
										<asp:textbox id="txtFMName" runat="server" Width="230px" Enabled="True"></asp:textbox></TD>
									<td><asp:requiredfieldvalidator id="ReqFldVal_FMID" runat="server" CssClass="LabelError" Enabled="False" ErrorMessage="The FSM is required."
											ControlToValidate="txtFMID">*</asp:requiredfieldvalidator></td>
									<TD vAlign="top" align="right"><asp:imagebutton id="imgBtnSelectFM" runat="server" CausesValidation="False" ImageUrl="~/images/BtnSelect.gif"></asp:imagebutton></TD>
									<TD vAlign="top" align="right"><IMG style="CURSOR: hand" onclick="ResetFM();" src="images/BtnDelete.gif">
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR></TBODY>
			</TABLE>
		</td>
</tr>
<tr>
	<TD style="WIDTH: 215px" vAlign="top" colspan="2">
		<uc1:promo_logo_text_selector id="ctrlPromo_logo_text_selector" runat="server"></uc1:promo_logo_text_selector>
	</TD>
</tr>
<TR  id="trReceived" runat="server">
	<TD style="WIDTH: 85px" vAlign="top"><SPAN class="StandardLabel">Received&nbsp;:</SPAN>
	</TD>
	<TD vAlign="top"><asp:checkbox id="chkReceived" Runat="server"></asp:checkbox></TD>
</TR>
    <tr>
        <td style="width: 139px" valign="top">
            <span class="StandardLabel">Labeling&nbsp;Start&nbsp;Date&nbsp;:</span></td>
        <td valign="top">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="height: 24px">
                        <asp:TextBox ID="txtLabelingSD" runat="server" Font-Size="9px" Height="14px" Font-Names="Verdana, Arial, Tahoma" Width="100px"></asp:TextBox>
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtLabelingSD"
                                                        Mask="99/99/9999" MessageValidatorTip="false" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" CultureName="en-US" />
                        </td>
                    <td style="height: 24px">
                        <asp:HyperLink ID="hypLnkStartDate" runat="server" ImageUrl="~/images/Calendar.gif"
                            NavigateUrl="javascript:void(0);" ToolTip="Click here to select the start date from a popup calendar !">HyperLink</asp:HyperLink>&nbsp;
                    </td>
                    <td style="height: 24px">
                        <asp:RequiredFieldValidator ID="reqFldVal_StartDate" runat="server" ControlToValidate="txtLabelingSD"
                            CssClass="LabelError" ErrorMessage="The Start Date is required.">*</asp:RequiredFieldValidator><asp:CompareValidator
                                ID="compVal_StartDate" runat="server" ControlToValidate="txtLabelingSD" CssClass="LabelError"
                                ErrorMessage="The Start Date is invalid." Operator="DataTypeCheck" Type="Date">*</asp:CompareValidator><asp:CompareValidator
                                    ID="Comparevalidator2" runat="server" ControlToCompare="txtLabelingED" ControlToValidate="txtLabelingSD"
                                    CssClass="LabelError" ErrorMessage="The Start Date must be less or equal than the End Date."
                                    Operator="LessThanEqual" Type="Date">*</asp:CompareValidator></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="width: 139px" valign="top">
            <span class="StandardLabel">Labeling&nbsp;End&nbsp;Date&nbsp;:</span></td>
        <td valign="top">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:TextBox ID="txtLabelingED" runat="server" Font-Size="9px" Height="14px" Font-Names="Verdana, Arial, Tahoma" Width="100px"></asp:TextBox>
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtLabelingED"
                                                        Mask="99/99/9999" MessageValidatorTip="false" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" CultureName="en-US" />
                        </td>
                    <td>
                        <asp:HyperLink ID="hypLnkEndDate" runat="server" ImageUrl="~/images/Calendar.gif" NavigateUrl="javascript:void(0);"
                            ToolTip="Click here to select the end date from a popup calendar !">HyperLink</asp:HyperLink>&nbsp;
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="reqFldVal_EndDate" runat="server" ControlToValidate="txtLabelingED"
                            CssClass="LabelError" ErrorMessage="The End Date is required.">*</asp:RequiredFieldValidator><asp:CompareValidator
                                ID="compVal_EndDate" runat="server" ControlToValidate="txtLabelingED" CssClass="LabelError"
                                ErrorMessage="The End Date is invalid." Operator="DataTypeCheck" Type="Date">*</asp:CompareValidator></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="width: 139px; height: 42px;" valign="top">
            <span class="StandardLabel">Expiration&nbsp;Date&nbsp;:</span></td>
        <td valign="top" style="height: 42px">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:TextBox ID="txtExpirationDate" runat="server" Font-Size="9px" Height="14px" Font-Names="Verdana, Arial, Tahoma" Width="100px"></asp:TextBox>
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtExpirationDate"
                                                        Mask="99/99/9999" MessageValidatorTip="false" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" CultureName="en-US" />
                        </td>
                    <td>
                        <asp:HyperLink ID="hypLnkExpirationDate" runat="server" ImageUrl="~/images/Calendar.gif" NavigateUrl="javascript:void(0);"
                            ToolTip="Click here to select the expiration date from a popup calendar !">HyperLink</asp:HyperLink>&nbsp;
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtExpirationDate"
                            CssClass="LabelError" ErrorMessage="The Expiration Date is required.">*</asp:RequiredFieldValidator><asp:CompareValidator
                                ID="CompareValidator3" runat="server" ControlToValidate="txtExpirationDate" CssClass="LabelError"
                                ErrorMessage="The Expiration Date is invalid." Operator="DataTypeCheck" Type="Date">*</asp:CompareValidator></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="width: 139px" valign="top">
            <span class="StandardLabel">Description :</span>
        </td>
        <td valign="top">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDescription"
                CssClass="LabelError" ErrorMessage="The Description is required">*</asp:RequiredFieldValidator><br />
            <asp:TextBox ID="txtDescription" runat="server" MaxLength="500" Rows="5" TextMode="MultiLine"
                Width="400px"></asp:TextBox>
        </td>
    </tr>
</table>
<script language=javascript>
function ShowHideSubdivision()
{
	var chkNational = document.getElementById('<%=this.chkNational.ClientID%>');
	var trFMInfo = document.getElementById('<%=this.trFmInfo.ClientID%>');
	var trFMEdit = document.getElementById('<%=this.trFmEdit.ClientID%>');
	
	if(chkNational.checked)
	{
		trFMInfo.style.visibility = "hidden";
		trFMEdit.style.visibility = "hidden";
	}
	else
	{	
		trFMInfo.style.visibility = "visible";	
		trFMEdit.style.visibility = "visible";	
	}
}
function ResetFM()
{
	var fmID = document.getElementById('<%=this.txtFMID.ClientID%>');
	var fmName = document.getElementById('<%=this.txtFMName.ClientID%>');
	fmID.value = "";
	fmName.value = "";
}
function ResetVendor()
{
	var vendorID = document.getElementById('<%=this.txtVendorID.ClientID%>');
	var vendorName = document.getElementById('<%=this.txtVendorName.ClientID%>');
	vendorID.value = "";
	vendorName.value = "";
}
function QuickFill()
{
    var lblEndDate = document.getElementById('<%=this.txtLabelingED.ClientID%>');
    var lblExpDate = document.getElementById('<%=this.txtExpirationDate.ClientID%>');
    var lblStartDate = document.getElementById('<%=this.txtLabelingSD.ClientID%>');
    
    var endate = lblStartDate.value;
    var sdate = endate.split('/');
    //<!-- put && lblexpdate.length == 0 if applicable on when empty -->
    if(sdate.length == 3)
    {
        if(sdate[2].length == 4)
        {
            var date = new Date(endate);
                  
            if( (sdate[0].length > 0) && (sdate[1].length > 0) && (sdate[2].length > 0) )
            {
                lblExpDate.value = sdate[0] + '/' + sdate[1] + '/' + (date.getFullYear() + 3);
                lblEndDate.value = lblExpDate.value;
            }
        }
        else
        {
            lblExpDate.value = "";
            lblEndDate.value = "";
        }  
    }
    else
    {
        lblExpDate.value = "";
        lblEndDate.value = "";
    }
    
}
</script>
