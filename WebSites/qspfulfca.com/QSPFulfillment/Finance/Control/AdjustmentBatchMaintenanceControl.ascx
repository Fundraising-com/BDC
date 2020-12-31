<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.Finance.Control" Assembly="QSPFulfillment" %>
<%@ Register TagPrefix="uc1" TagName="AdjustmentListControl" Src="AdjustmentListControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../../Common/DateEntry.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="AdjustmentBatchMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.Finance.Control.AdjustmentBatchMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table cellpadding="0" cellspacing="10" border="0">
	<tr>
		<td>
			<cc2:adjustmenttypedropdownlist id="ddlAdjustmentType" runat="server" enableviewstate="False" initialtext="Please select..."
				initialvalue="0" isrequired="True"></cc2:adjustmenttypedropdownlist>
		</td>
		<td>
			<uc1:dateentry id="dteDateFrom" runat="server" required="True"></uc1:dateentry>
		</td>
		<td>
			<uc1:dateentry id="dteDateTo" runat="server" required="True"></uc1:dateentry>
		</td>
		<td>
			<asp:button id="btnPreview" runat="server" text="Preview" cssclass="boxlook"></asp:button>&nbsp;<asp:button id="btnGenerate" runat="server" text="Generate" cssclass="boxlook"></asp:button>&nbsp;<asp:button id="btnCancel" runat="server" text="Cancel" causesvalidation="False" cssclass="boxlook"></asp:button>
		</td>
	</tr>
</table>
<br>
<uc1:adjustmentlistcontrol id="ctrlAdjustmentListControl" runat="server" isnested="False" showadjustmentid="False"></uc1:adjustmentlistcontrol>
