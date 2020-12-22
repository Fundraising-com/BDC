<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.BusinessCalendarControlForm" Codebehind="BusinessCalendarControlForm.ascx.cs" %>
<style>
A 
{
	text-decoration:none
}

A:link
{
	text-decoration:none
}
</style>
<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
	<tr>
		<td align="center"><asp:calendar id="calBusiness" runat="server" CellPadding="4" BorderColor="#999999" Font-Names="Verdana"
				Font-Size="8pt" Height="180px" ForeColor="Black" DayNameFormat="FirstLetter" Width="200px" BackColor="White" onselectionchanged="calBusiness_SelectionChanged" onprerender="calBusiness_Render">
				<SelectorStyle BackColor="#CCCCCC"></SelectorStyle>
				<NextPrevStyle VerticalAlign="Bottom"></NextPrevStyle>
				<DayHeaderStyle Font-Size="7pt" Font-Bold="True" BackColor="#CCCCCC" Font-Underline="false"></DayHeaderStyle>
				<SelectedDayStyle Font-Bold="True" ForeColor="White" BackColor="#666666"></SelectedDayStyle>
				<TitleStyle Font-Bold="True" BorderColor="Black" BackColor="#999999"></TitleStyle>
				<WeekendDayStyle BackColor="#FFFFCC"></WeekendDayStyle>
				<OtherMonthDayStyle ForeColor="Gray"></OtherMonthDayStyle>
			</asp:calendar>
		</td>
	</tr>
</table>
