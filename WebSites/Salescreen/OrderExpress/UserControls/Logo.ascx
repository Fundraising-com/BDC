<%@ Register TagPrefix="uc1" TagName="SubdivisionSelector" Src="SubdivisionSelector.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.Logo" Codebehind="Logo.ascx.cs" %>

<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td align="left"> <!--Section Title --></td>
	</tr>
	<tr>
		<td align="left">		        
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="600" border="0">
				<TR >
					<TD class="SectionPageTitleInfo" colSpan="2" ><asp:label id="lblTitle" runat="server"></asp:label></TD>
				</TR>
				<TR align="left">
					<TD align="left" ><SPAN class="StandardLabel">ID:</SPAN>
					</TD>
					<TD align="left"><asp:label id="lblID" runat="server" Width="200px" CssClass="DescInfoLabel"></asp:label></TD>
				</TR>
				<TR align="left">
					<TD align="left" ><SPAN class="StandardLabel">Name:</SPAN>
					</TD>
					<TD align="left"><asp:textbox id="txtName" Runat="server" CssClass="DescLabel"></asp:textbox></TD>
				</TR>
				<TR align="left">
					<TD style="WIDTH: 215px" vAlign="top"><SPAN class="StandardLabel">Category:</SPAN>
					</TD>
					<TD vAlign="top"><asp:DropDownList id="ddlImageCategory" Runat="server"></asp:DropDownList>
						<asp:CompareValidator id="CompareValidator2" runat="server" ControlToValidate="ddlImageCategory" ErrorMessage="The Category is required"
							ValueToCompare="0" Operator="GreaterThan">*</asp:CompareValidator></TD>
				</TR>
				<TR align="left">
					<TD style="WIDTH: 215px" vAlign="top"><SPAN class="StandardLabel">Image:</SPAN>
					</TD>
					<TD vAlign="top"><asp:image id="imgDetail" runat="server" Width="100" Height="100" ImageUrl=""></asp:image>
					    <br />
                        <asp:Label ID="lblNote" runat="server" CssClass="DescInfoLabel" Font-Bold="True" Text="Note:  Image will appear distorted until it is saved."></asp:Label></TD>
				</TR>
				<TR id="trUpload" style="DISPLAY: none" runat="server" align="left">
					<TD style="WIDTH: 215px" vAlign="top"><SPAN class="StandardLabel">Upload&nbsp;Image:</SPAN>
					</TD>
					<TD vAlign="top"><INPUT id="ctrlUpload" style="WIDTH: 250px; HEIGHT: 22px" type="file" size="19" name="ctrlUpload"
							runat="server">
						<asp:button id="btnUpload" runat="server" Width="72" Height="22" Text="Upload" CausesValidation="False" onclick="btnUpload_Click"></asp:button></TD>
				</TR>
				<TR id="trFM" runat="server" align="left">
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
											<TD align="right" valign="top"><IMG src="../images/BtnDelete.gif" style="CURSOR: hand" onclick="ResetFM();">
											</TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</table>
					</TD>
				</TR>
				<TR id="trNational" runat="server" align="left">
					<TD style="WIDTH: 215px; height: 20px;" vAlign="top"><SPAN class="StandardLabel">QSP Image:</SPAN>
					</TD>
					<TD vAlign="top" style="height: 20px"><asp:checkbox id="chkNational" Runat="server"></asp:checkbox></TD>
				</TR>
				<TR align="left">
					<TD style="WIDTH: 215px" vAlign="top"><SPAN class="StandardLabel">Active:</SPAN>
					</TD>
					<TD vAlign="top"><asp:checkbox id="chkEnabled" Runat="server" Checked="True"></asp:checkbox></TD>
				</TR>
				<TR id="trSubdivision" runat="server" align="left">
					<TD style="WIDTH: 215px" vAlign="top"><SPAN class="StandardLabel">Subdivision:</SPAN>
					</TD>
					<TD vAlign="top"><uc1:subdivisionselector id="ctrlSubdivisionSelector" runat="server"></uc1:subdivisionselector></TD>
				</TR>
				<TR align="left">
					<TD style="WIDTH: 215px" vAlign="top"><SPAN class="StandardLabel">Description:</SPAN>
					</TD>
					<TD vAlign="top"><asp:textbox id="txtDescription" Width="400px" Runat="server" Rows="5" TextMode="MultiLine" ></asp:textbox></TD>
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
	var ddlImageCategory = document.getElementById('<%= this.ddlImageCategory.ClientID %>');
	
	if(chkNational.checked)
	{
		trFM.style.display = "none";
	}
	else
	{
		trFM.style.display = "block";
		ddlImageCategory.value = <%= PERSONAL_IMAGES_CATEGORY_ID %>
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
</script>
