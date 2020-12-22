<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.BusinessFormStep_Information" Codebehind="BusinessFormStep_Information.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="FormHeaderDetailForm" Src="FormHeaderDetailForm.ascx" %>

<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td align="left"> <!--Section Title --></td>
	</tr>
	<tr>
		<td class="SectionPageTitleInfo">
			<asp:label id="lblTitleFormInfo" runat="server">
				General Information
			</asp:label>
		</td>
	</tr>
	<tr>
		<td><uc1:FormHeaderDetailForm id="HeaderDetail" runat="server"></uc1:FormHeaderDetailForm></td>
	</tr>
	<TR>
		<TD align="center"><br>
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<td><asp:imagebutton id="imgBtnBack" runat="server" CausesValidation="False" ImageUrl="~/images/btnBack.gif"
							AlternateText="Click here to go back to the previous STEP"></asp:imagebutton></td>
					<td width="100%">&nbsp;
					</td>
					<td><asp:imagebutton id="imgBtnNext" runat="server" CausesValidation="False" ImageUrl="~/images/btnNext.gif"
							AlternateText="Click here to confirm your selection and go to the next STEP"></asp:imagebutton></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</table>
