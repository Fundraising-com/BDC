<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ProgramMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.AcctMgt.Control.ProgramMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<tr>
	<td>
		<asp:label id="lblProgramName" runat="server" cssclass="csPlainText"></asp:label>
	</td>
	<td>
		<asp:checkbox id="chkLanded" runat="server"></asp:checkbox>
	</td>
   <td>
        <asp:CheckBox id="chkFieldSupplyPacket" runat="server" TextAlign="Left"></asp:CheckBox>
   </td>
   <td>
        <asp:CheckBox id="chkAllowOnlineAccountDelivery" runat="server" TextAlign="Left"></asp:CheckBox>
   </td>
	<td>
		<asp:DropDownList id="ddlGroupProfit" runat="server" TextAlign="Left" cssclass="csPlainText"></asp:DropDownList>
	</td>
</tr>
