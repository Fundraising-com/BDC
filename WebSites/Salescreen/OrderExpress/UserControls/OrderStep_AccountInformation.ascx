<%@ Register TagPrefix="uc1" TagName="OrderHeaderDetailForm" Src="OrderHeaderDetailForm.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrderStep_AccountInformation" Codebehind="OrderStep_AccountInformation.ascx.cs" %>

<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td align="left"> <!--Section Title --></td>
	</tr>
	<tr>
		<td align="left"> <!--Section Body -->
			<table id="Table52" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td><asp:label id="Label2" runat="server" CssClass="StandardLabel" Visible="False">
							4 - Please review the Account Information and complete the Order information :
						</asp:label>&nbsp;&nbsp;&nbsp;
						
					</td>
					<td vAlign="top"></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td>
			<uc1:OrderHeaderDetailForm id="HeaderDetail" SectionAccount_Visible="True" SectionOrder_Visible="False" runat="server"></uc1:OrderHeaderDetailForm>
		</td>
	</tr>
	<TR>
		<TD align="center"><br>
			<TABLE cellSpacing="0" cellPadding="0" border="0" id="Table2">
			    <tr runat="server" id="trQCAPOrderIntimation">
				    <td colspan="3" align="right">
				        <asp:Label ID="lblQCAPOrderIntimation" runat="server" CssClass="NoteLabel" Text="NOTE: This order is initiated from QCAP"></asp:Label>
				        <br />
				    </td>
			    </tr>
				<TR>
					<td><asp:imagebutton id="imgBtnBack" runat="server" CausesValidation="False" ImageUrl="~/images/btnBack.gif"
							AlternateText="Click here to go back to the previous STEP"></asp:imagebutton></td>
					<td width="100%">&nbsp;
					</td>
					<td><asp:imagebutton id="imgBtnNext" runat="server" ImageUrl="~/images/btnNext.gif" AlternateText="Click here to confirm your selection and go to the next STEP"
							CausesValidation="False"></asp:imagebutton></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</table>
