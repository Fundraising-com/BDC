<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ControlerOrderHeaderForProductReplacement.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerOrderHeaderForProductReplacement" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="ControlerProductSelect" Src="ControlerProductSelect.ascx" %>
<div style="BORDER-RIGHT: silver 1px solid; PADDING-RIGHT: 10px; BORDER-TOP: silver 1px solid; PADDING-LEFT: 10px; PADDING-BOTTOM: 15px; BORDER-LEFT: silver 1px solid; PADDING-TOP: 15px; BORDER-BOTTOM: silver 1px solid">
	<table style="WIDTH: 528px">
		<tr>
			<td style="WIDTH: 182px"><asp:label id="Label1" runat="server" cssclass="CSPlainText">Teacher First Name</asp:label></td>
			<td><asp:textbox id="tbxTeacherFirstName" runat="server" maxlength="50" columns="50"></asp:textbox></td>
		</tr>
		<tr>
			<td style="WIDTH: 182px"><asp:label id="Label2" runat="server" cssclass="CSPlainText">Teacher Last Name</asp:label></td>
			<td><asp:textbox id="tbxTeacherLastName" runat="server" maxlength="50" columns="50"></asp:textbox></td>
		</tr>
		<tr>
			<td style="WIDTH: 182px"><asp:label id="Label4" runat="server" cssclass="CSPlainText">Student First Name</asp:label></td>
			<td><asp:textbox id="tbxStudentFirstName" runat="server" maxlength="50" columns="50"></asp:textbox></td>
		</tr>
		<tr>
			<td style="WIDTH: 182px"><asp:label id="Label8" runat="server" cssclass="CSPlainText">Student Last Name</asp:label></td>
			<td><asp:textbox id="tbxStudentLastName" runat="server" maxlength="50" columns="50"></asp:textbox></td>
		</tr>
	</table>
	<uc1:controlerproductselect id="ctrlControlerProductSelect" showpriceinformation="True" showterm="False" showcheckboxes="False"
		showsearch="False" runat="server" showcatalogname="False" showproductreplacementreason="True" showlanguage="False" allowpaging="False"></uc1:controlerproductselect>
	<br>
	<asp:linkbutton id="lnkEditProducts" runat="server" causesvalidation="False" onclick="lnkEditProducts_Click">Edit Products</asp:linkbutton>
	&nbsp;
	<asp:linkbutton id="lnkRemoveOrder" runat="server" causesvalidation="False" onclick="lnkRemoveOrder_Click">Remove Order</asp:linkbutton>
</div>
<br>
<br>
<br>
