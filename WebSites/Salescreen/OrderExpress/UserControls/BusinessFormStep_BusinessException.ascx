<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.BusinessFormStep_BusinessException" Codebehind="BusinessFormStep_BusinessException.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="BusinessExceptionForm" Src="BusinessExceptionForm.ascx" %>

<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td class="SectionPageTitleInfo">
			<asp:label id="lblTitleFormInfo" runat="server">
				Business Exceptions List
			</asp:label>
		</td>
	</tr>
	<tr>
		<td align="left"> <!--Section Body -->
			<table id="Table4" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td>
						<uc1:BusinessExceptionForm id="BusinessExceptionFormStep" runat="server"></uc1:BusinessExceptionForm>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<TR>
		<TD align="left">
			<br>
			<br>
			<TABLE cellSpacing="0" cellPadding="0" border="0" id="Table2" width="100%">
				<TR>
					<td>
						<asp:ImageButton id="imgBtnBack" runat="server" CausesValidation="False" ImageUrl="~/images/btnBack.gif"
							AlternateText="Click here to go back to the previous STEP"></asp:ImageButton>
					</td>
					<td align="right">
						<asp:ImageButton id="imgBtnNext" runat="server" CausesValidation="False" ImageUrl="~/images/btnNext.gif"
							AlternateText="Click here to go to the next STEP" ToolTip="Click here to go to the next STEP"></asp:ImageButton>
					</td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</table>
