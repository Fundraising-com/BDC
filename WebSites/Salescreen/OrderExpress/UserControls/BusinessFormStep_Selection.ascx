<%@ Register TagPrefix="uc1" TagName="MDRSchoolSelector" Src="MDRSchoolSelector.ascx" %>
<%@ Register TagPrefix="uc1" TagName="OrganizationSelector" Src="OrganizationSelector.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.BusinessFormStep_Selection" Codebehind="BusinessFormStep_Selection.ascx.cs" %>

<table id="Table1222" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td align="left"> <!--Section Title --></td>
	</tr>
	<tr>
		<td><asp:label id="Label5" CssClass="StandardLabel" runat="server" Visible="False">
			<br>
			1 - Select on what your business Form is based on:
			<br>
			<br>
		</asp:label>
		</td>
	</tr>
	<tr>
		<td><br>
			<br>
			<table id="Table14" cellSpacing="0" cellPadding="0" border="0" width="600">
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td vAlign="top" align="center"><asp:imagebutton id="imgBtnAccount" runat="server" AlternateText="Click here to go to Account List !"
										ImageUrl="~/images/icon/icon_account.gif" Width="100px"></asp:imagebutton></td>
							</tr>
							<tr>
								<td vAlign="middle" align="center">
									<asp:label id="Label1" runat="server" CssClass="StandardLabel">
										Account:
									</asp:label>
								</td>
							</tr>
						</table>
					</td>
					<td>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td vAlign="top" align="center">
									<asp:imagebutton id="imgBtnOrder" runat="server" AlternateText="Click here to go to Order List !"
										ImageUrl="~/images/icon/icon_order.gif" Width="100px"></asp:imagebutton></td>
							</tr>
							<tr>
								<td vAlign="middle" align="center">
									<asp:label id="Label3" runat="server" CssClass="StandardLabel">
										Order:
									</asp:label>
								</td>
							</tr>
						</table>
					</td>
					<td>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td vAlign="top" align="center"><asp:imagebutton id="imgBtnCreditApplication" runat="server" AlternateText="Click here to go to Organization List !"
										ImageUrl="~/images/icon/icon_credit_app.gif" Width="100px"></asp:imagebutton>
								</td>
							</tr>
							<tr>
								<td valign="middle" align="center">
									<asp:label id="lblCrdAppTitle" runat="server" CssClass="StandardLabel">
										Credit Application:
									</asp:label>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<br>
		</td>
	</tr>
	<tr>
		<td align="left" colSpan="6"> <!--Section Body --></td>
	</tr>
	<tr>
		<td align="center"><br>
			<br>
		</td>
	</tr>
	<TR>
		<TD align="center"><br>
			<TABLE id="Table4" cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<td><asp:imagebutton id="imgBtnNext" runat="server" Visible="False" ImageUrl="~/images/btnNext.gif" CausesValidation="False"
							AlternateText="Click here to confirm your selection and go next to STEP 2"></asp:imagebutton></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</table>
