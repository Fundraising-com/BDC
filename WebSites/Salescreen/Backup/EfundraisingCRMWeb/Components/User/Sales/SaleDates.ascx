<%@ Register TagPrefix="uc1" TagName="PickDay" Src="../PickDay.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="SaleDates.ascx.cs" Inherits="EFundraisingCRMWeb.Components.User.Sales.SaleDates" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table cellSpacing="0" cellPadding="0" style="HEIGHT: 216px">

	<tr vAlign="top" height="20">
		<td class="NormalText" width="102" style="WIDTH: 102px">
			<asp:Label id="Label2" runat="server">Sale Date</asp:Label></td>
		<td vAlign="top">
			<uc1:PickDay id="saleDatePickDay" runat="server"></uc1:PickDay></td>
	</tr>
	<tr vAlign="top" height="20">
		<td class="NormalText" style="WIDTH: 103px">
			<asp:Label id="Label1" runat="server">Payment Due</asp:Label></td>
		<td vAlign="top">
			<uc1:PickDay id="paymentDueDatePickday" runat="server"></uc1:PickDay></td>
	</tr>
	<tr vAlign="top" height="20">
		<td class="NormalText" style="WIDTH: 103px">
			<asp:Label id="Label3" runat="server">Sched. Delivery</asp:Label></td>
		<td vAlign="top">
			<uc1:PickDay id="schDeliveryPickday" runat="server" ReadOnly="False"></uc1:PickDay></td>
	</tr>
	<tr vAlign="top" height="20">
		<td class="NormalText" style="WIDTH: 103px">
			<asp:Label id="Label10" runat="server">Sched. Time</asp:Label></td>
		<td vAlign="top">
			<asp:DropDownList ID="cboSchedTimeHour" runat="server" Width="38px" 
                Font-Size="8pt">
            </asp:DropDownList>
            <asp:DropDownList ID="cboSchedTimeMin" runat="server" Font-Size="8pt" 
                Width="38px">
            </asp:DropDownList>
            <asp:DropDownList ID="cboSchedTimePM" runat="server" Font-Size="8pt" 
                Width="41px">
            </asp:DropDownList>
        </td>
	    
	</tr>
	<tr vAlign="top" height="20">
		<td class="NormalText" style="WIDTH: 103px">
			<asp:Label id="Label4" runat="server">Sched. Ship</asp:Label></td>
		<td vAlign="top">
			<uc1:PickDay id="schShipPickday" runat="server"></uc1:PickDay></td>
	</tr>
	<tr vAlign="top" height="20">
		<td class="NormalText" style="WIDTH: 103px">
			<asp:Label id="Label5" runat="server">Actual Delivery</asp:Label></td>
		<td vAlign="top">
			<uc1:PickDay id="actDeliveryDatePickday" runat="server"></uc1:PickDay></td>
	</tr>
	<tr vAlign="top" height="20">
		<td class="NormalText" style="WIDTH: 103px; HEIGHT: 13px">
			<asp:Label id="Label6" runat="server">Actual Ship</asp:Label></td>
		<td vAlign="top" style="HEIGHT: 13px">
			<uc1:PickDay id="actShipDatePickday" runat="server"></uc1:PickDay></td>
	</tr>
	<tr vAlign="top" height="20">
		<td class="NormalText" style="WIDTH: 103px">
			<asp:Label id="Label7" runat="server">Box Return</asp:Label></td>
		<td vAlign="top">
			<uc1:PickDay id="boxReturnDatePickday" runat="server"></uc1:PickDay></td>
	</tr>
	<tr vAlign="top" height="20">
		<td class="NormalText" style="WIDTH: 103px">
			<asp:Label id="Label8" runat="server">Box Reship</asp:Label></td>
		<td vAlign="top">
			<uc1:PickDay id="boxReshipDatePickday" runat="server"></uc1:PickDay></td>
	</tr>
	<tr vAlign="top" height="20">
		<td class="NormalText" style="WIDTH: 103px">
			<asp:Label id="Label9" runat="server">Confirmed</asp:Label></td>
		<td vAlign="top">
			<uc1:PickDay id="confirmedPickday" runat="server"></uc1:PickDay></td>
	</tr>
</table>
