<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.Promo_TextHeaderForm" Codebehind="Promo_TextHeaderForm.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="SubdivisionSelector" Src="SubdivisionSelector.ascx" %>

<meta content="False" name="vs_snapToGrid">
<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td align="left"> <!--Section Title --></td>
	</tr>
	<tr>
		<td align="left">
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="600" border="0">
				<TR>
					<TD class="SectionPageTitleInfo" colSpan="2"><asp:label id="lblTitle" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 215px" vAlign="top"><SPAN class="StandardLabel">ID&nbsp;:</SPAN>
					</TD>
					<TD vAlign="top"><asp:label id="lblID" runat="server" Width="200px" CssClass="DescInfoLabel"></asp:label></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 215px" vAlign="top"><SPAN class="StandardLabel">Code&nbsp;:</SPAN>
					</TD>
					<TD vAlign="top"><asp:textbox id="txtCode" Runat="server"></asp:textbox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 215px" vAlign="top"><SPAN class="StandardLabel">Name&nbsp;:</SPAN>
					</TD>
					<TD vAlign="top"><asp:textbox id="txtName" Runat="server"></asp:textbox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 215px" vAlign="top"><SPAN class="StandardLabel">Description&nbsp;:</SPAN>
					</TD>
					<TD vAlign="top"><asp:textbox id="txtDescription" Width="400px" Runat="server" Rows="5" MaxLength="500"></asp:textbox></TD>
				</TR>
				<TR id="trFM" runat="server">
					<TD colSpan="2">
						<table id="Table2" cellSpacing="0" cellPadding="0">
							<tr id="trFmInfo" style="DISPLAY: none" runat="server">
								<td style="WIDTH: 142px"><asp:label id="Label11" runat="server" CssClass="StandardLabel">
											Field&nbsp;Sales&nbsp;Manager:&nbsp;
										</asp:label></td>
								<td width="100%"><asp:label id="lblFMInfo" runat="server" CssClass="DescInfoLabel"></asp:label></td>
							</tr>
							<TR id="trFmEdit" style="DISPLAY: none" vAlign="top" runat="server">
								<TD style="WIDTH: 142px"><asp:label id="lblLabelFM" runat="server" CssClass="StandardLabel">
											Field&nbsp;Sales&nbsp;Manager:&nbsp;
										</asp:label></TD>
								<TD>
									<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="400" border="0">
										<TR>
											<TD><asp:textbox id="txtFMID" runat="server" Width="50px" Enabled="True" ReadOnly="True"></asp:textbox></TD>
											<TD><asp:comparevalidator id="CompValFMID" runat="server" CssClass="LabelError" ControlToValidate="txtFMID"
													ErrorMessage="The FM ID is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator></TD>
											<TD>&nbsp;
												<asp:textbox id="txtFMName" runat="server" Width="230px" Enabled="True" ReadOnly="True"></asp:textbox></TD>
											<td><asp:requiredfieldvalidator id="ReqFldVal_FMID" runat="server" CssClass="LabelError" Enabled="False" ControlToValidate="txtFMID"
													ErrorMessage="The FSM is required.">*</asp:requiredfieldvalidator></td>
											<TD align="right" valign="top"><asp:imagebutton id="imgBtnSelectFM" runat="server" ImageUrl="~/images/BtnSelect.gif" CausesValidation="False"></asp:imagebutton></TD>
											<TD align="right" valign="top"><IMG src="images/BtnDelete.gif" style="CURSOR: hand" onclick="ResetFM();">
											</TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</table>
					</TD>
				</TR>
				<TR id="TrVendor" runat="server">
					<TD colSpan="2">
						<table id="Table5" cellSpacing="0" cellPadding="0">
							<tr id="TrVendorInfo" style="DISPLAY: none" runat="server">
								<td style="WIDTH: 126px"><asp:label id="Label2" runat="server" CssClass="StandardLabel">
											Vendor:&nbsp;
										</asp:label></td>
								<td width="100%"><asp:label id="lblVendorInfo" runat="server" CssClass="DescInfoLabel"></asp:label></td>
							</tr>
							<TR id="TrVendorEdit" style="DISPLAY: none" vAlign="top" runat="server">
								<TD style="WIDTH: 126px"><asp:label id="Label3" runat="server" CssClass="StandardLabel">
											Vendor:&nbsp;
										</asp:label></TD>
								<TD align="right">
									<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="400" border="0">
										<TR>
											<TD><asp:textbox id="txtVendorID" runat="server" Width="50px" Enabled="True" ReadOnly="True"></asp:textbox></TD>
											<TD><asp:comparevalidator id="Comparevalidator1" runat="server" CssClass="LabelError" ControlToValidate="txtVendorID"
													ErrorMessage="The Vendor ID is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator></TD>
											<TD>&nbsp;
												<asp:textbox id="txtVendorName" runat="server" Width="230px" Enabled="True" ReadOnly="True"></asp:textbox></TD>
											<td><asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" CssClass="LabelError" Enabled="False"
													ControlToValidate="txtVendorID" ErrorMessage="The vendor is required.">*</asp:requiredfieldvalidator></td>
											<TD align="right" valign="top"><asp:imagebutton id="imgBtnSelectVendor" runat="server" ImageUrl="~/images/BtnSelect.gif" CausesValidation="False"></asp:imagebutton></TD>
											<TD align="right" valign="top"><IMG src="images/BtnDelete.gif" style="CURSOR: hand" onclick="ResetVendor();">
											</TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</table>
					</TD>
				</TR>
				<TR id="trNational" runat="server">
					<TD style="WIDTH: 215px" vAlign="top"><SPAN class="StandardLabel">National&nbsp;:</SPAN>
					</TD>
					<TD vAlign="top"><asp:checkbox id="chkNational" Runat="server"></asp:checkbox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 215px" vAlign="top"><SPAN class="StandardLabel">Enabled&nbsp;:</SPAN>
					</TD>
					<TD vAlign="top"><asp:checkbox id="chkEnabled" Runat="server"></asp:checkbox></TD>
				</TR>
				<TR id="trSubdivision" runat="server">
					<TD style="WIDTH: 215px" vAlign="top"><SPAN class="StandardLabel">Subdivision&nbsp;:</SPAN>
					</TD>
					<TD vAlign="top"><uc1:subdivisionselector id="ctrlSubdivisionSelector" runat="server"></uc1:subdivisionselector></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 215px" vAlign="top"><SPAN class="StandardLabel">Text&nbsp;:</SPAN>
					</TD>
					<TD vAlign="top"><asp:textbox id="txtText" Width="400px" Runat="server" Rows="5" TextMode="MultiLine" MaxLength="1000"></asp:textbox></TD>
				</TR>
			</TABLE>
		</td>
	</tr>
</table>
<script>
function ShowHideFMSelector()
{
	var chkNational = document.getElementById('<%=this.chkNational.ClientID%>');
	var trFM = document.getElementById('<%=this.trFM.ClientID%>');
	
	if(chkNational.checked)
	{
		trFM.style.display = "none";
	}
	else
	{
		trFM.style.display = "block";	
	}
}
function ShowHideSubdivision()
{
	var chkNational = document.getElementById('<%=this.chkNational.ClientID%>');
	var trSubdivision = document.getElementById('<%=this.trSubdivision.ClientID%>');
	var trFM = document.getElementById('<%=this.trFM.ClientID%>');
	
	if(chkNational.checked)
	{
		trSubdivision.style.display = "none";
		trFM.style.display = "none";
	}
	else
	{
		trSubdivision.style.display = "block";
		trFM.style.display = "block";	
	}
}
function ResetFM()
{
	var fmID = document.getElementById('<%=this.txtFMID.ClientID%>');
	var fmName = document.getElementById('<%=this.txtFMName.ClientID%>');
	fmID.value = "";
	fmName.value = "";
}
function ResetVendor()
{
	var vendorID = document.getElementById('<%=this.txtVendorID.ClientID%>');
	var vendorName = document.getElementById('<%=this.txtVendorName.ClientID%>');
	vendorID.value = "";
	vendorName.value = "";
}
</script>
