<%@ Register TagPrefix="uc1" TagName="ContactMaintenanceControl" Src="ContactMaintenanceControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="DatePicker" Src="../../Common/DateEntry.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FieldSuppliesMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.AcctMgt.Control.FieldSuppliesMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
&nbsp;
<script type='text/javascript'>
function GenerateFS_CallBack(response)
{
alert(response.value);

window.Refresh();
}

function GenerateFS()
{
	var eleID = document.getElementById('ctrlCampaignMaintenanceControl_ctrlFieldSuppliesMaintenanceControl_hidCampaignID');
	var eleHid = document.getElementById('ctrlCampaignMaintenanceControl_ctrlFieldSuppliesMaintenanceControl_hidDataBind');

	if(window.confirm("Are you sure you want to Generate Field Supplies?"))
	{	
		parent.parent.window.pleasewait();
		eleHid.value="1";
		FieldSuppliesMaintenanceControl.GenerateFieldSupplyForCampaign(eleID.value,GenerateFS_CallBack)
	}
}

</script>
<input type="hidden" value="0" id="hidDataBind" runat="server" NAME="hidDataBind">
<table width="100%" border="0" cellpadding="0" cellspacing="0">
	<tr>
		<td>
			<asp:label id="lblFieldSuppliesGenerated" runat="server" cssclass="csPlainText">Field Supplies have already been generated for this campaign.</asp:label>
			<asp:label id="lblFieldSuppliesInstructions" runat="server" cssclass="csPlainText"> Field Supplies:</asp:label><br>
		</td>
		<td style="TEXT-ALIGN: right">
			<input id="btnEditFSOrder" runat="server" type="button" class="boxlook" value="View Order"
				NAME="btnEditFSOrder"> 
			<!--<td align="center" id="tdGenerateFS" runat="server"><input class="boxlook" onclick="GenerateFS()" type="button" value="Generate FS">
		<input id="hidCampaignID" type="hidden"  runat="server">
		</td>-->
		</td>
	</tr>
</table>
<br>
<table width="100%">
	<tr>
		<td valign="middle">
			<asp:label id="Label3" cssclass="csPlainText" runat="server" font-bold="True">Field Supplies Required</asp:label></td>
		<td valign="middle">
			<asp:checkbox id="chkRequired" runat="server"></asp:checkbox></td>
	</tr>
	<tr>
		<td valign="middle">
			<asp:label id="Label4" cssclass="csPlainText" runat="server" font-bold="True" Visible="False">Extra Field Supplies Required</asp:label></td>
		<td valign="middle">
			<asp:checkbox id="chkExtraRequired" runat="server" Visible="False"></asp:checkbox></td>
	</tr>
	<tr>
		<td valign="middle">
			<asp:label id="Label1" runat="server" cssclass="csPlainText" font-bold="True">Delivery Date</asp:label>
		</td>
		<td valign="middle">
			<uc1:datepicker id="dteDeliveryDate" runat="server" columns="10"></uc1:datepicker>
			<asp:label id="lblDeliveryDate" runat="server" cssclass="csPlainText"></asp:label>
		</td>
	</tr>
	<tr>
		<td valign="middle">
			<asp:label id="Label2" runat="server" cssclass="csPlainText" font-bold="True">Ship Supplies To</asp:label>
		</td>
		<td valign="middle">
			<asp:dropdownlist id="ddlShipSuppliesTo" runat="server" initialtext="Please select..." initialvalue="0"
				autopostback="True" onselectedindexchanged="ddlShipSuppliesTo_SelectedIndexChanged"></asp:dropdownlist>
			<asp:label id="lblShipSuppliesTo" runat="server" cssclass="csPlainText"></asp:label>
		</td>
	</tr>
	<tr>
		<td colspan="2">
			<uc1:contactmaintenancecontrol id="ctrlShipToFSContactMaintenanceControl" runat="server" fixedmode="true" clientvisible="true"
				visible="False"></uc1:contactmaintenancecontrol>
		</td>
	</tr>
</table>
