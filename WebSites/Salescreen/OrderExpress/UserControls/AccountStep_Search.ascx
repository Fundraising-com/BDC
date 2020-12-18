<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.AccountStep_Search" Codebehind="AccountStep_Search.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="OrganizationSelector" Src="OrganizationSelector.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MDRSchoolSelector" Src="MDRSchoolSelector.ascx" %>


<table id="Table1222" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td align="left"> <!--Section Title --></td>
	</tr>
	<tr>
		<td><asp:label id="Label5" CssClass="StandardLabel" runat="server" Visible="False">
			<br>
			1 - Select an Organization from our two existing Directories or create a brand new one:
			<br>
			<br>
		</asp:label>
		</td>
	</tr>
	<tr>
		<td>
			<table id="Table14" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td>&nbsp;&nbsp;&nbsp;&nbsp;
					</td>
					<td><asp:radiobuttonlist id="radBtnLstSearchMode" CssClass="StandardLabel" runat="server" AutoPostBack="True"
							RepeatDirection="Horizontal" CellPadding="3" onselectedindexchanged="radBtnLstSearchMode_SelectedIndexChanged">
							<asp:ListItem Value="0" Selected="True">QSP&nbsp;Organization&nbsp;Directory</asp:ListItem>
							<asp:ListItem Value="1">MDR&nbsp;Directory</asp:ListItem>
							<asp:ListItem Value="2">Create&nbsp;New&nbsp;Organization</asp:ListItem>
						</asp:radiobuttonlist></td>
				</tr>
			</table>
			<br>
		</td>
	</tr>
	<tr>
		<td align="left" colSpan="6"> <!--Section Body --><uc1:organizationselector id="OrganizationSelectorStep" runat="server" ></uc1:organizationselector></td>
	</tr>
	<tr>
		<td style="HEIGHT: 24px" align="left" colSpan="6"> <!--Section Body --><uc1:mdrschoolselector id="MDRSchoolSelectorStep" runat="server" Visible="False"></uc1:mdrschoolselector></td>
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
