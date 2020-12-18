<%@ Control Language="c#" AutoEventWireup="True" Codebehind="MyCalendar.ascx.cs" Inherits="CRMWeb.UserControls.MyCalendar" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<P>
	<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="194" border="0" style="WIDTH: 194px; HEIGHT: 192px">
		<TR>
			<TD>
				<asp:calendar id="Cal" runat="server" ShowGridLines="True" Font-Size="11pt" Height="192px" Font-Names="Microsoft Sans Serif"
					ForeColor="Black" BorderWidth="3px" BorderStyle="Solid" BorderColor="#6695C3" BackColor="#F7F7F7"
					Width="200px" CssClass="Calendar" FirstDayOfWeek="Sunday" DayNameFormat="FirstTwoLetters">
					<TodayDayStyle Font-Bold="True" CssClass="CalTodayStyle"></TodayDayStyle>
					<DayStyle CssClass="CalDayStyle"></DayStyle>
					<NextPrevStyle BorderColor="Red"></NextPrevStyle>
					<DayHeaderStyle Font-Size="8pt" ForeColor="Navy" CssClass="CalDayHeader" VerticalAlign="Top" BackColor="#F7F7F7"></DayHeaderStyle>
					<SelectedDayStyle BorderWidth="2px" BorderStyle="Solid" BorderColor="Red" CssClass="CalSelectedDay"></SelectedDayStyle>
					<TitleStyle Font-Size="10pt" CssClass="CalTitle" BackColor="#6695C3"></TitleStyle>
					<WeekendDayStyle CssClass="CalWeekEndDay" BackColor="Info"></WeekendDayStyle>
					<OtherMonthDayStyle ForeColor="Silver" CssClass="CalOtherMonthDay"></OtherMonthDayStyle>
				</asp:calendar></TD>
		</TR>
	</TABLE>
</P>
