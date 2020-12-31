<%@ Control Language="c#" AutoEventWireup="false" Codebehind="CampaignProgramMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.AcctMgt.Control.CampaignProgramMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table width="100%">
	<tr>
		<td style="WIDTH: 217px" noWrap>
			<asp:label id="lblNameTitle" runat="server" cssclass="csPlainText" font-bold="True" Width="200px">Program</asp:label>
		</td>
		<td style="WIDTH: 100px">
			<asp:label id="lblLandedTitle" runat="server" cssclass="csPlainText" font-bold="True" Width="90px">Landed</asp:label>
		</td>
        <td style="WIDTH: 107px">
			<asp:label id="lblPacket" font-bold="True" cssclass="csPlainText" runat="server" Width="88px"
				BorderWidth="15px" BorderStyle="None">Packet</asp:label>
		</td>
        <td style="WIDTH: 107px">
			<asp:label id="lblAllowOnlineAccountDelivery" font-bold="True" cssclass="csPlainText" runat="server" Width="119px"
				BorderWidth="15px" BorderStyle="None">Allow Online Account Delivery</asp:label>
		</td>		
		<TD style="WIDTH: 32px">
			<asp:label id="lblGroupProfitTitle" font-bold="True" cssclass="csPlainText" runat="server"
				Width="95px">Group Profit</asp:label></TD>
	</tr>
	<asp:placeholder id="plhProgramList" runat="server"></asp:placeholder>
</table>
