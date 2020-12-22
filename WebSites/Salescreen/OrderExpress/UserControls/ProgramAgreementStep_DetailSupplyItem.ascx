<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.ProgramAgreementStep_DetailSupplyItem" Codebehind="ProgramAgreementStep_DetailSupplyItem.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="ProgramAgreementSupplyForm" Src="ProgramAgreementSupplyForm.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AddressControlForm" Src="AddressControlForm.ascx" %>

<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td align="left"> <!--Section Title --><asp:label id="Label4" Font-Size="small" runat="server" CssClass="StandardLabel"></asp:label></td>
	</tr>
	<tr>
		<td align="left"> <!--Section Body -->
			<table id="Table5" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td><asp:label id="Label2" runat="server" CssClass="StandardLabel" Visible="False"> 6 - Enter All Supply products :
						</asp:label>&nbsp;&nbsp;&nbsp;
						
					</td>
					<td vAlign="top"></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td align="left"> <!--Section Body -->
			<table id="Table4" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td><uc1:ProgramAgreementSupplyForm id="SupplyForm" runat="server"></uc1:ProgramAgreementSupplyForm></td>
				</tr>
			</table>
		</td>
	</tr>
	<TR>
		<TD align="center"><br>
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<td><asp:imagebutton id="imgBtnBack" runat="server" AlternateText="Click here to go back to the previous STEP"
							ImageUrl="~/images/btnBack.gif" CausesValidation="False"></asp:imagebutton></td>
					<td width="100%">&nbsp;
					</td>
					<td><asp:imagebutton id="imgBtnNext" runat="server" AlternateText="Click here to confirm your selection and go to the next STEP"
							ImageUrl="~/images/btnNext.gif" CausesValidation="False"></asp:imagebutton></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</table>
