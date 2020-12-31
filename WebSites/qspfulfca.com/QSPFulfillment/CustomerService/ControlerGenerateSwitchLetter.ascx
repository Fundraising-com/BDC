<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../Common/DateEntry.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ControlerGenerateSwitchLetter.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerGenerateSwitchLetter" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<script language="javascript">

	function SetTitleCode(Code,Description)
	{
		var tbxTitleCode = document.getElementById('ctrlControlerGenerateSwitchLetter_tbxTitleCode');
		tbxTitleCode.value = Code;
		
		var lblDescription = document.getElementById('ctrlControlerGenerateSwitchLetter_lblCodeDescription');
		lblDescription.innerHTML = Description;
		
		tbxTitleCode.focus();
	
	}

</script>
<div style="TEXT-ALIGN: center">
	<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0" width="500">
		<TR>
			<TD>
				<asp:Label id="Label5" Text="Generate Switch Letter Batch" CssClass="CSPageTitle" Runat="server"></asp:Label></TD>
		</TR>
		<tr>
			<td><br>
				<asp:Label Runat="server" ID="lblDirections" CssClass="CSDirections">Please enter a remit batch id and title code OR enter a date range and a title code to generate a switch letter batch.</asp:Label></td>
		</tr>
	</TABLE>
	<br>
	<table bgcolor="#000000" cellpadding="1" cellspacing="0" border="0" width="500">
		<tr>
			<td>
				<TABLE id="Table1ss" cellSpacing="0" cellPadding="2" border="0" width="100%" bgcolor="#ffffff">
					<TR>
						<TD class="CSTableHeader" colSpan="3">Switch Letter Information</TD>
					</TR>
					<TR>
						<td>&nbsp;</td>
						<TD width="250"><br>
							<asp:Label id="Label1" runat="server" CssClass="csPlainText">Remit Batch ID</asp:Label></TD>
						<TD><br>
							<cc1:TextBoxSearch id="tbxRemitBatchID" runat="server" ParameterName="iRemitBatchID" ContentType="int"></cc1:TextBoxSearch>
							<asp:RangeValidator id="RangeValidator5" runat="server" ErrorMessage="Remit Batch ID must be between 1 and 2147483647."
								Type="Integer" ControlToValidate="tbxRemitBatchID" MaximumValue="2147483647" MinimumValue="1">*</asp:RangeValidator></TD>
					</TR>
					<TR>
						<td></td>
						<TD>
							<asp:Label id="Label6" CssClass="csPlainText" runat="server">From</asp:Label></TD>
						<TD>
							<uc1:dateentry id="ctrlDateEntryFrom" runat="server" ParameterName="dFrom" ContentType="DateTime"></uc1:dateentry></TD>
					</TR>
					<TR>
						<td></td>
						<TD>
							<asp:Label id="Label7" CssClass="csPlainText" runat="server">To</asp:Label></TD>
						<TD>
							<uc1:dateentry id="ctrlDateEntryTo" runat="server" ParameterName="dTo" ContentType="DateTime"></uc1:dateentry></TD>
					</TR>
					<TR>
						<td></td>
						<TD>
							<asp:Label id="Label2" runat="server" CssClass="csPlainText">Title Code</asp:Label></TD>
						<TD>
							<cc1:TextBoxSearch id="tbxTitleCode" runat="server" ParameterName="sTitleCode" ContentType="string"></cc1:TextBoxSearch>
							<a onClick="javascript:Open('Magazine.aspx?IsNewWindow=true&amp;ID=true&amp;SwitchLetter=true&amp;IsOnlyMagazine=true')"
								href="javascript:;" runat="server" ID="A1"><img src="images/find.gif" alt="Find" border="0"></a></TD>
					</TR>
					<TR>
						<td></td>
						<TD>
							<asp:Label id="Label4" runat="server" CssClass="csPlainText">Magazine Title</asp:Label></TD>
						<TD>
							<asp:Label id="lblCodeDescription" runat="server" CssClass="csPlainText"></asp:Label></TD>
					</TR>
					<TR>
						<td></td>
						<TD>
							<asp:Label id="Label3" runat="server" CssClass="csPlainText">Reason</asp:Label></TD>
						<TD>
							<asp:DropDownList id="ddlReason" runat="server"></asp:DropDownList><br>
							<br>
						</TD>
					</TR>
				</TABLE>
			</td>
		</tr>
	</table>
	<br>
	<TABLE cellSpacing="0" cellPadding="2" border="0" width="500" bgcolor="#ffffff">
		<TR>
			<TD align="center">
				<asp:Button id="btnGenerate" runat="server" Text="Generate"></asp:Button>
			</TD>
			<td align="center">
				<asp:Button id="btnPreview" runat="server" Text="Preview"></asp:Button>
			</td>
		</TR>
	</TABLE>
	<cc3:rsgeneration id="rsGenerationSwitchLetter" runat="server" reportname="SwitchLetter" Mode="PopUp"></cc3:rsgeneration>
</div>
