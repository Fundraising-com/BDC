<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrgStep_MDRSchool" Codebehind="OrgStep_MDRSchool.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="MDRSchoolInfo" Src="MDRSchoolInfo.ascx" %>

<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td align="left"> <!--Section Title --><asp:label id="Label4" Font-Size="small" runat="server" CssClass="StandardLabel"></asp:label></td>
	</tr>
	<tr>
		<td><asp:label id="Label5" runat="server" CssClass="StandardLabel">
				1 - Select a School from our MDR School List :
			</asp:label><br>
			<br>
		</td>
	</tr>
	<tr>
		<td align="left" colSpan="6"> <!--Section Body -->
			<table id="Table2" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td>&nbsp;&nbsp;&nbsp;&nbsp;
					</td>
					<td><asp:textbox id="txtMDRSchoolPID" runat="server" CssClass="StandardLabel" ForeColor="#993300"
							Width="100px"></asp:textbox></td>
					<td>&nbsp;
					</td>
					<td><asp:textbox id="txtMDRSchoolName" runat="server" CssClass="StandardLabel" ForeColor="#993300"
							Width="400px" ReadOnly="True"></asp:textbox>
					</td>
					<td><asp:imagebutton id="imgBtnSelect" runat="server" CausesValidation="False" ImageUrl="~/images/btnSelect.gif"></asp:imagebutton></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td>
			<table id="Table8" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td>&nbsp;&nbsp;&nbsp;&nbsp;
					</td>
					<td>
					</td>
				</tr>
			</table>
			<br>
			<br>
		</td>
	</tr>
	<tr>
		<td>
			<br>
			<br>
		</td>
	</tr>
	<TR>
		<TD align="center"><br>
			<TABLE cellSpacing="0" cellPadding="0" border="0" id="Table4">
				<TR>
					<td><asp:imagebutton id="imgBtnNext" runat="server" CausesValidation="False" ImageUrl="~/images/btnNextBig.gif"
							AlternateText="Click here to confirm your selection and go next to STEP 2"></asp:imagebutton></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</table>
