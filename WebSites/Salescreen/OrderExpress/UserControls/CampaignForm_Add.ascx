<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.CampaignForm_Add" Codebehind="CampaignForm_Add.ascx.cs" %>
<LINK href="Style.css" type="text/css" rel="stylesheet">
<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td align="left"> <!--Section Title --></td>
	</tr>
	<tr>
		<td>
			<table id="Table2" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td><asp:label id="Label3" runat="server" CssClass="StandardLabel">
							1- Select the Account for which you want to create a campaign :
						</asp:label>
						<br>
						<br>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td align="left"> <!--Section Body -->
			<table id="Table3" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td>&nbsp;&nbsp;&nbsp;&nbsp;
					</td>
					<td><asp:textbox id="txtAccountID" runat="server" CssClass="StandardLabel" ForeColor="#993300" Width="100px"></asp:textbox></td>
					<td>&nbsp;
					</td>
					<td><asp:textbox id="txtOrganizationName" runat="server" CssClass="StandardLabel" ForeColor="#993300"
							Width="400px" ReadOnly="True"></asp:textbox></td>
					<td>&nbsp;
						<asp:requiredfieldvalidator id="reFldVal_CampID" runat="server" CssClass="LabelError" ControlToValidate="txtCampaignID"
							ErrorMessage="The Campaign is required.">*</asp:requiredfieldvalidator></td>
					<td><asp:imagebutton id="imgBtnSelect" runat="server" CausesValidation="False" ImageUrl="~/images/arrow.gif"></asp:imagebutton></td>
				</tr>
			</table>
			<br>
			<br>
		</td>
	</tr>
	<tr>
		<td>
			<table id="Table6" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td>
						<br>
						<br>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
