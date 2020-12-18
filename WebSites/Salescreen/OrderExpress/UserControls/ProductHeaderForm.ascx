<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.ProductHeaderForm" Codebehind="ProductHeaderForm.ascx.cs" %>
<meta content="False" name="vs_snapToGrid">

<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td align="left"> <!--Section Title --></td>
	</tr>
	<tr>
		<td align="left">
			<table id="Table3" cellSpacing="0" cellPadding="0" width="600" border="0">
				<tr align="left">
					<td class="SectionPageTitleInfo" colSpan="2" align="left"><asp:label id="Label5" runat="server">
							Product Information
						</asp:label></td>
				</tr>
				<tr>
					<td valign="top"><span class="StandardLabel">ID&nbsp;:</span>
					</td>
					<td valign="top"><asp:label id="lblID" runat="server" Width="200px" CssClass="DescLabel"></asp:label></td>
				</tr>
				<tr>
					<td valign="top"><span class="StandardLabel">Product Type&nbsp;:</span>
					</td>
					<td valign="top"><asp:dropdownlist id="ddlProductType" runat="server" CssClass="DescLabel"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td valign="top"><span class="StandardLabel">Product Code&nbsp;:</span>
					</td>
					<td valign="top"><asp:textbox id="txtProductCode" runat="server" CssClass="DescLabel"></asp:textbox></td>
				</tr>
				<tr>
					<td valign="top"><span class="StandardLabel">Product Name&nbsp;:</span>
					</td>
					<td valign="top"><asp:textbox id="txtProductName" runat="server" CssClass="DescLabel"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="The field Product Name is mandatory"
							ControlToValidate="txtProductName">*</asp:requiredfieldvalidator></td>
				</tr>
				<tr>
					<td  valign="top"style="HEIGHT: 26px"><span class="StandardLabel">Vendor&nbsp;:</span>
					</td>
					<td valign="top" style="HEIGHT: 28px"><asp:dropdownlist id="ddlVendor" runat="server" CssClass="DescLabel"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td valign="top" class="StandardLabel">Description:</td>
					<td valign="top"><asp:textbox id="txtDescription" CssClass="DescLabel" Runat="server" Width="377px" TextMode="MultiLine" Height="80px"></asp:textbox></td>
				</tr>
				<tr>
					<td valign="top" class="StandardLabel" style="HEIGHT: 26px">Number of Unit&nbsp;:</td>
					<td  valign="top"style="HEIGHT: 26px"><asp:textbox id="txtNbUnit" CssClass="DescLabel" Runat="server"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="The field Number of Unit is mandatory"
							ControlToValidate="txtNbUnit">*</asp:requiredfieldvalidator><asp:rangevalidator id="RangeValidator1" runat="server" ErrorMessage="Number of unit must be between 0 and 65000"
							MinimumValue="0" MaximumValue="65000" ControlToValidate="txtNbUnit">*</asp:rangevalidator><asp:regularexpressionvalidator id="RegularExpressionValidator1" runat="server" ErrorMessage="Enter a number" ControlToValidate="txtNbUnit"
							ValidationExpression="[0-9]*">*</asp:regularexpressionvalidator></td>
				</tr>
				<tr>
					<td valign="top" class="StandardLabel">Unit Cost&nbsp;:</td>
					<td valign="top"><asp:textbox id="txtUnitCost" CssClass="DescLabel" Runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td valign="top"><span class="StandardLabel">Number of day lead time:</span>
					</td>
					<td valign="top"><asp:textbox id="txtNbDayLeadTime" CssClass="DescLabel" Runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td valign="top"><span class="StandardLabel">Vendor Item Code&nbsp;:</span>
					</td>
					<td valign="top"><asp:textbox id="txtVendorItem" CssClass="DescLabel" Runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td valign="top"><span class="StandardLabel">Oracle code:</span>
					</td>
					<td valign="top"><asp:textbox id="txtOracleCode" CssClass="DescLabel" Runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td valign="top"><span class="StandardLabel">Commission&nbsp;:</span>
					</td>
					<td valign="top"><asp:textbox id="txtCommission" runat="server" CssClass="DescLabel"></asp:textbox></td>
				</tr>
				<tr>
					<td valign="top"><span class="StandardLabel">Is free sample&nbsp;:</span>
					</td>
					<td valign="top"><asp:checkbox id="chkIsFreeSample" runat="server" CssClass="DescLabel"></asp:checkbox></td>
				</tr>
				<tr>
					<td valign="top"><span class="StandardLabel">Image Url&nbsp;:</span>
					</td>
					<td valign="top"><asp:textbox id="txtImageURL" runat="server" CssClass="DescLabel"></asp:textbox></td>
				</tr>
				<tr>
					<td valign="top"><span class="StandardLabel">Business Division &nbsp;:</span>
					</td>
					<td valign="top"><asp:dropdownlist id="ddlBusinessDivision" runat="server" CssClass="DescLabel"></asp:dropdownlist></td>
				</tr>
			</table>
			<table id="tblAudit" runat="server" visible="false">
				<tr>
					<td class="StandardLabel">Created by :</td>
					<td><asp:label id="lbCreateBy" CssClass="DescLabel" Runat="server"></asp:label></td>
					<td class="StandardLabel">Created date :</td>
					<td><asp:label id="lbCreateDT" CssClass="DescLabel" Runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="StandardLabel">Updated by :</td>
					<td><asp:label id="lbUpdateBy" CssClass="DescLabel" Runat="server"></asp:label></td>
					<td class="StandardLabel">Update date :</td>
					<td><asp:label id="lbUpdateDT" CssClass="DescLabel" Runat="server"></asp:label></td>
				</tr>
			</table>
		</td>
	</tr>
</table>
