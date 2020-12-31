<%@ Control Language="c#" AutoEventWireup="True" Codebehind="InactiveMagazineLetterTemplateGenerationControl.ascx.cs" Inherits="QSPFulfillment.CustomerService.InactiveMagazineLetterTemplateGenerationControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../Common/DateEntry.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<script language="javascript">

	function SetTitleCodeLetterTemplateGenerationControl(Code,Description)
	{
		var tbxTitleCode = document.getElementById('ctrlLetterBatchGenerationControl_ctrlLetterTemplateGenerationControl_tbxTitleCode');
		tbxTitleCode.value = Code;
		
		var lblDescription = document.getElementById('ctrlLetterBatchGenerationControl_ctrlLetterTemplateGenerationControl_lblCodeDescription');
		lblDescription.innerHTML = Description;
		
		tbxTitleCode.focus();
	
	}

</script>
<table>
	<TR>
		<td>&nbsp;</td>
		<TD width="250" colSpan="2"><asp:radiobutton id="rbRemitBatch" onclick="SetEnabledLetterBatchType()" Text="Remit Batch ID" GroupName="LetterBatchType"
				Checked="True" CssClass="csPlainText" runat="server"></asp:radiobutton><br>
		</TD>
		<TD><br>
			<cc1:textboxinteger id="tbxRunID" runat="server"></cc1:textboxinteger><asp:rangevalidator id="RangeValidator5" runat="server" MinimumValue="1" MaximumValue="2147483647" ControlToValidate="tbxRunID"
				Type="Integer" ErrorMessage="Remit Batch ID must be between 1 and 2147483647.">*</asp:rangevalidator></TD>
	</TR>
	<TR>
		<td></td>
		<TD><asp:radiobutton id="rbDateRange" onclick="SetEnabledLetterBatchType()" Text="Date Range" GroupName="LetterBatchType"
				CssClass="csPlainText" runat="server"></asp:radiobutton></TD>
		<td align="right"><asp:label id="Label1" CssClass="csPlainText" runat="server">From:</asp:label></td>
		<TD><uc1:dateentry id="dteDateFrom" runat="server" EmptyValue="1995-01-01"></uc1:dateentry></TD>
	</TR>
	<TR>
		<td colSpan="2"></td>
		<TD align="right"><asp:label id="Label7" CssClass="csPlainText" runat="server">To:</asp:label></TD>
		<TD><uc1:dateentry id="dteDateTo" runat="server" EmptyValue="1995-01-01"></uc1:dateentry></TD>
	</TR>
	<TR>
		<td></td>
		<TD><asp:label id="Label2" CssClass="csPlainText" runat="server">Title Code</asp:label></TD>
		<td></td>
		<TD><cc1:textboxsearch id="tbxTitleCode" runat="server" ParameterName="sTitleCode" ContentType="string"></cc1:textboxsearch><A id="A1" onclick="javascript:Open('Magazine.aspx?IsNewWindow=true&amp;ID=true&amp;Fct=SetTitleCodeLetterTemplateGenerationControl&amp;SwitchLetter=true&amp;IsOnlyMagazine=true')"
				href="javascript:;" runat="server"><IMG alt="Find" src="images/find.gif" border="0"></A></TD>
	</TR>
	<TR>
		<td></td>
		<TD><asp:label id="Label4" CssClass="csPlainText" runat="server">Magazine Title</asp:label></TD>
		<TD>
		<TD><asp:label id="lblCodeDescription" CssClass="csPlainText" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<td></td>
		<TD><asp:label id="Label3" CssClass="csPlainText" runat="server">Reason</asp:label></TD>
		<TD>
		<td><asp:dropdownlist id="ddlReason" runat="server"></asp:dropdownlist></td>
	</TR>
</table>
