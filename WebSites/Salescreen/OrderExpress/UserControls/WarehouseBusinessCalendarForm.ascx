<%@ Register TagPrefix="uc1" TagName="BusinessCalendarControlForm" Src="BusinessCalendarControlForm.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.WarehouseBusinessCalendarForm" Codebehind="WarehouseBusinessCalendarForm.ascx.cs" %>

<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td class="SectionPageTitleInfo" colSpan="2">
			<asp:label id="Label3" runat="server" Visible="False">
				Warehouse Business Calendar
			</asp:label></td>
	</tr>
	<tr>
		<td>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td>
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD align="center">
									<uc1:BusinessCalendarControlForm id="BizCalendar_Ctrl" runat="server"></uc1:BusinessCalendarControlForm>
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
