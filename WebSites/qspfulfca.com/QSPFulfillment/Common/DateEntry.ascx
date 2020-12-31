<%@ Control Language="c#" AutoEventWireup="True" Codebehind="DateEntry.ascx.cs" Inherits="QSPFulfillment.CommonWeb.UC.DateEntry" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table border="0" cellpadding="0" cellspacing="0">
	<tr>
		<td>
			<asp:TextBox id="tb_DATE" autopostback="false" runat="server"></asp:TextBox>
		</td>
		<td><asp:RequiredFieldValidator id="rq_date" runat="server" ControlToValidate="tb_DATE" display="Dynamic" ErrorMessage="Please select a date">
*</asp:RequiredFieldValidator>
		</td>
		<td>
			<asp:RegularExpressionValidator id="reg_date" runat="server" ControlToValidate="tb_DATE" Display="Dynamic" ErrorMessage="An invalid date was entered. Please select a date or enter it in the following format: MM/DD/YYYY"
				ValidationExpression="^(?:(?:(?:0?[13578]|1[02])(\/)31)\1|(?:(?:0?[1,3-9]|1[0-2])(\/)(?:29|30)\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:0?2(\/)29\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:(?:0?[1-9])|(?:1[0-2]))(\/)(?:0?[1-9]|1\d|2[0-8])\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$">
*</asp:RegularExpressionValidator>
		</td>
		<td>
		<A tabindex="-1" onclick="cal=window.open('/QSPFulfillment/Common/DatePicker.aspx?caller=<%=this.ClientID+"_tb_DATE"%>&callerButton=<%=this.ClientID+"_PostBackButton"%>','cal','width=250,height=225,left=270,top=180');cal.focus()" href="javascript:;">
				<img src="/QSPFulfillment/Images/SmallCalendar.gif" border="0"></A></td>
	</tr>
</table>
<div style="display: none; width: 100px;"><asp:Button ID="PostBackButton" runat="server" autopostback="true" Visible="true" Width="0px" Height="0px"/></div>
