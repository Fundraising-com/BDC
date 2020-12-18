<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.BusinessRuleForm" Codebehind="BusinessRuleForm.ascx.cs" %>

<table id="tblFormDetailTitle" cellSpacing="0" cellPadding="0" width="600" border="0">
	<tr>
	    <td class="SectionPageTitleInfo">
	        <asp:label id="lblTitleAccountInfo" runat="server">
	            Predefined Business rules
            </asp:label>
       </td>
	</tr>
	<tr>
        <td><br>
            <table id="tblAccountSalesHistory_NbDayInterval" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
	            <TBODY>
		            <tr id="trAccountSalesHistory" runat="server">
			            <td colspan=3 style="height: 3px"><asp:label id="Label55" runat="server" CssClass="StandardSectionLabel">
					            Account Sales History:&nbsp;
				            </asp:label>
				        </td>
		            </tr>
		            <tr id="trAccountSalesHistory_NbDayInterval" runat="server">
			            <td><asp:label id="Label56" runat="server" CssClass="StandardLabel">
					            <nobr>&nbsp;&nbsp;Sales&nbsp;History&nbsp;Interval&nbsp;#&nbsp;Day&nbsp;:&nbsp;</nobr>
				            </asp:label></td>
			            <td></td>
			            <td width="100%">
				            <table cellSpacing="0" cellPadding="0" border="0">
					            <tr>
						            <td><asp:textbox id="txtAccountSalesHistory_NbDayInterval" runat="server" CssClass="DescLabel" Columns=3 MaxLength="3"></asp:textbox></td>
						            <td><asp:comparevalidator id="CompValAccountSalesHistory_NbDayInterval" runat="server" CssClass="LabelError" ControlToValidate="txtAccountSalesHistory_NbDayInterval"
								        ErrorMessage="The interval in Number of Day for the Account Sales History is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator>
						            </td>
					            </tr>							    
				            </table>
			            </td>
		            </tr>
		            <tr id="tr3" runat="server">
			            <td><asp:label id="Label28" runat="server" CssClass="StandardLabel">
					            <nobr>&nbsp;&nbsp;Sales&nbsp;History&nbsp;Minimum&nbsp;Total&nbsp;Amount:&nbsp;</nobr>
				            </asp:label></td>
			            <td></td>
			            <td width="100%">
				            <table cellSpacing="0" cellPadding="0" border="0">
					            <tr>
						            <td><asp:textbox id="txtAccountSalesHistory_MinTotalAmount" runat="server" CssClass="DescLabel" Columns=6 MaxLength="6"></asp:textbox></td>
						            <td><asp:comparevalidator id="CompValAccountSalesHistory_MinTotalAmount" runat="server" CssClass="LabelError" ControlToValidate="txtAccountSalesHistory_MinTotalAmount"
								        ErrorMessage="The Minimum Total Amount in the Account Sales History is invalid (must be a number)." Type="Currency" Operator="DataTypeCheck">*</asp:comparevalidator>
						            </td>
					            </tr>							    
				            </table>
			            </td>
		            </tr>
	            </TBODY>
            </table>
       </td>
    </tr>
    <tr>
        <td>
            <table id="tblDeliveryOption" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
	            <TBODY>
		            <tr id="trDeliveryOption" runat="server">
			            <td colspan=3><asp:label id="Label57" runat="server" CssClass="StandardSectionLabel">
					            Delivery Option:&nbsp;
				            </asp:label>
				        </td>
		            </tr>
		            <tr id="trCommonCarrierName" runat="server">
			            <td><asp:label id="Label58" runat="server" CssClass="StandardLabel">
					            <nobr>&nbsp;&nbsp;Common&nbsp;Carrier&nbsp;Name:&nbsp;</nobr>
				            </asp:label></td>
			            <td></td>
			            <td width="100%">
				            <table cellSpacing="0" cellPadding="0" border="0">
					            <tr>
						            <td><asp:textbox id="txtCommonCarrierName" runat="server" CssClass="DescLabel" Columns=50 MaxLength="50"></asp:textbox></td>
						            <td>
						            </td>
					            </tr>							    
				            </table>
			            </td>
		            </tr>
		            
	            </TBODY>
            </table>
       </td>
    </tr>
	<TR>
		<TD><br>
			<table id="tblProductSection" cellSpacing="0" cellPadding="0"  border="0" runat="server">
			    <TBODY>
			    <tr id="tr2" runat="server">
					    <td colspan=7 class="SectionPageTitleInfo">
					        <asp:label id="Label27" runat="server" Font-Size=14px CssClass="StandardSectionLabel">
					            Order Requirements
					        </asp:label>
						</td>						
				    </tr>
				    <tr id="trProductSectionTitle" runat="server">
					    <td colspan=2>
						</td>
						<td colspan=5 align="center"><asp:label id="Label26" runat="server" CssClass="StandardSectionLabel">
							    Section Type
						    </asp:label>
						</td>
				    </tr>
				    <tr id="tr1" runat="server">
					    <td><asp:label id="Label22" runat="server" CssClass="StandardSectionLabel">For All Sections:&nbsp; 
						    </asp:label></td>
					    <td></td>
					    <td>
						   <nobr><asp:label id="Label23" runat="server" CssClass="StandardSectionLabel">
							    Standard 
						    </asp:label></nobr>
					    </td>
					    <td>&nbsp;&nbsp;</td>
					    <td>
						    <asp:label id="Label24" runat="server" CssClass="StandardSectionLabel">
							   Supply
						    </asp:label>
					    </td>
					    <td>&nbsp;&nbsp;</td>
					    <td>
						    <asp:label id="Label25" runat="server" CssClass="StandardSectionLabel">
							   Other
						    </asp:label>
					    </td>
				    </tr>
				    <tr id="trProductSection_MinNbDayLeadTime" runat="server">
					    <td><asp:label id="Label31" runat="server" CssClass="StandardLabel">
							    <nobr>&nbsp;&nbsp;Minimum&nbsp;#&nbsp;Day&nbsp;Lead-Time:&nbsp;</nobr>
						    </asp:label></td>
					    <td></td>
					    <td>
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td><asp:textbox id="txtProductSection_MinNbDayLeadTime" runat="server" CssClass="DescLabel" Columns=3 MaxLength="3"></asp:textbox></td>
								    <td><asp:comparevalidator id="CompValProductSection_MinNbDayLeadTime" runat="server" CssClass="LabelError" ControlToValidate="txtProductSection_MinNbDayLeadTime"
										ErrorMessage="The Minimum of day lead-time in the Section Product is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator>
								    </td>
							    </tr>							    
						    </table>
					    </td>
					    <td>&nbsp;&nbsp;</td>
					    <td>
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td><asp:textbox id="txtSupplySection_MinNbDayLeadTime" runat="server" CssClass="DescLabel" Columns=3 MaxLength="3"></asp:textbox></td>
								    <td><asp:comparevalidator id="CompValSupplySection_MinNbDayLeadTime" runat="server" CssClass="LabelError" ControlToValidate="txtSupplySection_MinNbDayLeadTime"
										ErrorMessage="The Minimum of day lead-time in the Section Supply is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator>
								    </td>
							    </tr>							    
						    </table>
					    </td>
					    <td>&nbsp;&nbsp;</td>
					    <td>
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td><asp:textbox id="txtOtherSection_MinNbDayLeadTime" runat="server" CssClass="DescLabel" Columns=3 MaxLength="3"></asp:textbox></td>
								    <td><asp:comparevalidator id="CompValOtherSection_MinNbDayLeadTime" runat="server" CssClass="LabelError" ControlToValidate="txtOtherSection_MinNbDayLeadTime"
										ErrorMessage="The Minimum of day lead-time in the Section Other is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator>
								    </td>
							    </tr>							    
						    </table>
					    </td>
				    </tr>
				    <tr id="trProductSection_MinTotalQuantity" runat="server">
					    <td><asp:label id="Label8" runat="server" CssClass="StandardLabel">
							    <nobr>&nbsp;&nbsp;Minimum&nbsp;Total&nbsp;Quantity:&nbsp;</nobr>
						    </asp:label></td>
					    <td ></td>
					    <td >
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td><asp:textbox id="txtProductSection_MinTotalQuantity" runat="server" CssClass="DescLabel" Columns=6 MaxLength="6"></asp:textbox></td>
								    <td><asp:comparevalidator id="CompValProductSection_MinTotalQuantity" runat="server" CssClass="LabelError" ControlToValidate="txtProductSection_MinTotalQuantity"
										ErrorMessage="The Minimum of Total Quantity in the Section Product is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator>
								    </td>
							    </tr>							    
						    </table>
					    </td>
					    <td >&nbsp;&nbsp;</td>
					    <td >
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td><asp:textbox id="txtSupplySection_MinTotalQuantity" runat="server" CssClass="DescLabel" Columns=6 MaxLength="6"></asp:textbox></td>
								    <td><asp:comparevalidator id="CompValSupplySection_MinTotalQuantity" runat="server" CssClass="LabelError" ControlToValidate="txtSupplySection_MinTotalQuantity"
										ErrorMessage="The Minimum of Total Quantity in the Section Supply is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator>
								    </td>
							    </tr>							    
						    </table>
					    </td>
					    <td >&nbsp;&nbsp;</td>
					    <td >
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td><asp:textbox id="txtOtherSection_MinTotalQuantity" runat="server" CssClass="DescLabel" Columns=6 MaxLength="6"></asp:textbox></td>
								    <td><asp:comparevalidator id="CompValOtherSection_MinTotalQuantity" runat="server" CssClass="LabelError" ControlToValidate="txtOtherSection_MinTotalQuantity"
										ErrorMessage="The Minimum of Total Quantity in the Section Other is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator>
								    </td>
							    </tr>							    
						    </table>
					    </td>
				    </tr>
				    <tr id="trProductSection_MinTotalAmount" runat="server">
					    <td><asp:label id="Label9" runat="server" CssClass="StandardLabel">
							    <nobr>&nbsp;&nbsp;Minimum&nbsp;Total&nbsp;Amount:&nbsp;</nobr>
						    </asp:label></td>
					    <td></td>
					    <td>
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td><asp:textbox id="txtProductSection_MinTotalAmount" runat="server" CssClass="DescLabel" Columns=8 MaxLength="8"></asp:textbox></td>
								    <td><asp:comparevalidator id="CompValProductSection_MinTotalAmount" runat="server" CssClass="LabelError" ControlToValidate="txtProductSection_MinTotalAmount"
										ErrorMessage="The Minimum of Total Amount in the Section Product is invalid (must be a number)." Type="Currency" Operator="DataTypeCheck">*</asp:comparevalidator>
								    </td>
							    </tr>							    
						    </table>
					    </td>
					    <td>&nbsp;&nbsp;</td>
					    <td>
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td><asp:textbox id="txtSupplySection_MinTotalAmount" runat="server" CssClass="DescLabel" Columns=8 MaxLength="8"></asp:textbox></td>
								    <td><asp:comparevalidator id="CompValSupplySection_MinTotalAmount" runat="server" CssClass="LabelError" ControlToValidate="txtSupplySection_MinTotalAmount"
										ErrorMessage="The Minimum of Total Amount in the Section Supply is invalid (must be a number)." Type="Currency" Operator="DataTypeCheck">*</asp:comparevalidator>
								    </td>
							    </tr>							    
						    </table>
					    </td>
					    <td>&nbsp;&nbsp;</td>
					    <td>
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td><asp:textbox id="txtOtherSection_MinTotalAmount" runat="server" CssClass="DescLabel" Columns=8 MaxLength="8"></asp:textbox></td>
								    <td><asp:comparevalidator id="CompValOtherSection_MinTotalAmount" runat="server" CssClass="LabelError" ControlToValidate="txtOtherSection_MinTotalAmount"
										ErrorMessage="The Minimum of Total Amount in the Section Other is invalid (must be a number)." Type="Currency" Operator="DataTypeCheck">*</asp:comparevalidator>
								    </td>
							    </tr>							    
						    </table>
					    </td>
				    </tr>
				    <tr id="tr4" runat="server">
					    <td><asp:label id="Label29" runat="server" CssClass="StandardLabel">
							    <nobr>&nbsp;&nbsp;Minimum&nbsp;Line&nbsp;Item&nbsp;Quantity:&nbsp;</nobr>
						    </asp:label></td>
					    <td ></td>
					    <td >
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td><asp:textbox id="txtProductSection_MinLineItemQuantity" runat="server" CssClass="DescLabel" Columns=6 MaxLength="6"></asp:textbox></td>
								    <td><asp:comparevalidator id="CompValProductSection_MinLineItemQuantity" runat="server" CssClass="LabelError" ControlToValidate="txtProductSection_MinLineItemQuantity"
										ErrorMessage="The Minimum of Line Item Quantity in the Section Product is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator>
								    </td>
							    </tr>							    
						    </table>
					    </td>
					    <td >&nbsp;&nbsp;</td>
					    <td >
						    
					    </td>
					    <td >&nbsp;&nbsp;</td>
					    <td >
						   
					    </td>
				    </tr>
				    <tr id="tr7" runat="server">
					    <td><asp:label id="Label33" runat="server" CssClass="StandardLabel">
							    <nobr>&nbsp;&nbsp;Maximum&nbsp;Total&nbsp;Quantity:&nbsp;</nobr>
						    </asp:label></td>
					    <td ></td>
					    <td >
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td><asp:textbox id="txtProductSection_MaxTotalQuantity" runat="server" CssClass="DescLabel" Columns=6 MaxLength="6"></asp:textbox></td>
								    <td><asp:comparevalidator id="CompValProductSection_MaxTotalQuantity" runat="server" CssClass="LabelError" ControlToValidate="txtProductSection_MaxTotalQuantity"
										ErrorMessage="The Maximum of Total Quantity in the Section Product is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator>
								    </td>
							    </tr>							    
						    </table>
					    </td>
					    <td >&nbsp;&nbsp;</td>
					    <td >
						    
					    </td>
					    <td >&nbsp;&nbsp;</td>
					    <td >
						   
					    </td>
				    </tr>
				    <tr id="tr8" runat="server">
					    <td><asp:label id="Label34" runat="server" CssClass="StandardLabel">
							    <nobr>&nbsp;&nbsp;Maximum&nbsp;Total&nbsp;Amount:&nbsp;</nobr>
						    </asp:label></td>
					    <td ></td>
					    <td >
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td><asp:textbox id="txtProductSection_MaxTotalAmount" runat="server" CssClass="DescLabel" Columns=8 MaxLength="8"></asp:textbox></td>
								    <td><asp:comparevalidator id="CompValProductSection_MaxTotalAmount" runat="server" CssClass="LabelError" ControlToValidate="txtProductSection_MaxTotalAmount"
										ErrorMessage="The Maximum of Total Amount in the Section Product is invalid (must be a number)." Type="Currency" Operator="DataTypeCheck">*</asp:comparevalidator>
								    </td>
							    </tr>							    
						    </table>
					    </td>
					    <td >&nbsp;&nbsp;</td>
					    <td >
						    
					    </td>
					    <td >&nbsp;&nbsp;</td>
					    <td >
						   
					    </td>
				    </tr>
			    </TBODY>
		    </table>
		</TD>
	</TR>
	<tr>
	    <td>
	        <table id="tblProductSection1" cellSpacing="0" cellPadding="0" border="0" runat="server">
			    <TBODY>
				    <tr id="trProductSection1" runat="server">
					    <td colspan=3><asp:label id="Label10" runat="server" CssClass="StandardSectionLabel">For Section 1:&nbsp;</asp:label>
						</td>
				    </tr>
				    <tr id="trProductSection1_MinNbDayLeadTime" runat="server">
					    <td><asp:label id="Label11" runat="server" CssClass="StandardLabel">
							    <nobr>&nbsp;&nbsp;Minimum&nbsp;#&nbsp;Day&nbsp;Lead-Time:&nbsp;</nobr>
						    </asp:label></td>
					    <td></td>
					    <td>
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td><asp:textbox id="txtProductSection1_MinNbDayLeadTime" runat="server" CssClass="DescLabel" Columns=3 MaxLength="3"></asp:textbox></td>
								    <td><asp:comparevalidator id="CompValProductSection1_MinNbDayLeadTime" runat="server" CssClass="LabelError" ControlToValidate="txtProductSection1_MinNbDayLeadTime"
										ErrorMessage="The Minimum of day lead-time in the Product Section 1 is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator>
								    </td>
							    </tr>							    
						    </table>
					    </td>
					    <td>&nbsp;&nbsp;</td>
					    <td>
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td><asp:textbox id="txtSupplySection1_MinNbDayLeadTime" runat="server" CssClass="DescLabel" Columns=3 MaxLength="3"></asp:textbox></td>
								    <td><asp:comparevalidator id="CompValSupplySection1_MinNbDayLeadTime" runat="server" CssClass="LabelError" ControlToValidate="txtSupplySection1_MinNbDayLeadTime"
										ErrorMessage="The Minimum of day lead-time in the Supply Section 1 is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator>
								    </td>
							    </tr>							    
						    </table>
					    </td>
					    <td>&nbsp;&nbsp;</td>
					    <td>
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td><asp:textbox id="txtOtherSection1_MinNbDayLeadTime" runat="server" CssClass="DescLabel" Columns=3 MaxLength="3"></asp:textbox></td>
								    <td><asp:comparevalidator id="CompValOtherSection1_MinNbDayLeadTime" runat="server" CssClass="LabelError" ControlToValidate="txtOtherSection1_MinNbDayLeadTime"
										ErrorMessage="The Minimum of day lead-time in the Other Section 1 is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator>
								    </td>
							    </tr>							    
						    </table>
					    </td>
				    </tr>
				    <tr id="trProductSection1_MinTotalQuantity" runat="server">
					    <td><asp:label id="Label12" runat="server" CssClass="StandardLabel">
							    <nobr>&nbsp;&nbsp;Minimum&nbsp;Total&nbsp;Quantity:&nbsp;</nobr>
						    </asp:label></td>
					    <td></td>
					    <td>
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td><asp:textbox id="txtProductSection1_MinTotalQuantity" runat="server" CssClass="DescLabel" Columns=6 MaxLength="6"></asp:textbox></td>
								    <td><asp:comparevalidator id="CompValProductSection1_MinTotalQuantity" runat="server" CssClass="LabelError" ControlToValidate="txtProductSection1_MinTotalQuantity"
										ErrorMessage="The Minimum of Total Quantity in the Product Section 1 is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator>
								    </td>
							    </tr>							    
						    </table>
					    </td>
					    <td>&nbsp;&nbsp;</td>
					    <td>
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td><asp:textbox id="txtSupplySection1_MinTotalQuantity" runat="server" CssClass="DescLabel" Columns=6 MaxLength="6"></asp:textbox></td>
								    <td><asp:comparevalidator id="CompValSupplySection1_MinTotalQuantity" runat="server" CssClass="LabelError" ControlToValidate="txtSupplySection1_MinTotalQuantity"
										ErrorMessage="The Minimum of Total Quantity in the Supply Section 1 is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator>
								    </td>
							    </tr>							    
						    </table>
					    </td>
					    <td>&nbsp;&nbsp;</td>
					    <td>
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td><asp:textbox id="txtOtherSection1_MinTotalQuantity" runat="server" CssClass="DescLabel" Columns=6 MaxLength="6"></asp:textbox></td>
								    <td><asp:comparevalidator id="CompValOtherSection1_MinTotalQuantity" runat="server" CssClass="LabelError" ControlToValidate="txtOtherSection1_MinTotalQuantity"
										ErrorMessage="The Minimum of Total Quantity in the Other Section 1 is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator>
								    </td>
							    </tr>							    
						    </table>
					    </td>
				    </tr>
				    <tr id="trProductSection1_MinTotalAmount" runat="server">
					    <td><asp:label id="Label13" runat="server" CssClass="StandardLabel">
							    <nobr>&nbsp;&nbsp;Minimum Total&nbsp;Amount:&nbsp;</nobr>
						    </asp:label></td>
					    <td></td>
					    <td>
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td><asp:textbox id="txtProductSection1_MinTotalAmount" runat="server" CssClass="DescLabel" Columns=8 MaxLength="8"></asp:textbox></td>
								    <td><asp:comparevalidator id="CompValProductSection1_MinTotalAmount" runat="server" CssClass="LabelError" ControlToValidate="txtProductSection1_MinTotalAmount"
										ErrorMessage="The Minimum of Total Amount in the Product Section 1 is invalid (must be a number)." Type="Currency" Operator="DataTypeCheck">*</asp:comparevalidator>
								    </td>
							    </tr>							    
						    </table>
					    </td>
					    <td>&nbsp;&nbsp;</td>
					    <td>
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td><asp:textbox id="txtSupplySection1_MinTotalAmount" runat="server" CssClass="DescLabel" Columns=8 MaxLength="8"></asp:textbox></td>
								    <td><asp:comparevalidator id="CompValSupplySection1_MinTotalAmount" runat="server" CssClass="LabelError" ControlToValidate="txtSupplySection1_MinTotalAmount"
										ErrorMessage="The Minimum of Total Amount in the Supply Section 1 is invalid (must be a number)." Type="Currency" Operator="DataTypeCheck">*</asp:comparevalidator>
								    </td>
							    </tr>							    
						    </table>
					    </td>
					    <td>&nbsp;&nbsp;</td>
					    <td>
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td><asp:textbox id="txtOtherSection1_MinTotalAmount" runat="server" CssClass="DescLabel" Columns=8 MaxLength="8"></asp:textbox></td>
								    <td><asp:comparevalidator id="CompValOtherSection1_MinTotalAmount" runat="server" CssClass="LabelError" ControlToValidate="txtOtherSection1_MinTotalAmount"
										ErrorMessage="The Minimum of Total Amount in the Other Section 1 is invalid (must be a number)." Type="Currency" Operator="DataTypeCheck">*</asp:comparevalidator>
								    </td>
							    </tr>							    
						    </table>
					    </td>
				    </tr>
				    <tr id="tr5" runat="server">
					    <td><asp:label id="Label30" runat="server" CssClass="StandardLabel">
							    <nobr>&nbsp;&nbsp;Minimum&nbsp;Line&nbsp;Item&nbsp;Quantity:&nbsp;</nobr>
						    </asp:label></td>
					    <td ></td>
					    <td >
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td><asp:textbox id="txtProductSection1_MinLineItemQuantity" runat="server" CssClass="DescLabel" Columns=6 MaxLength="6"></asp:textbox></td>
								    <td><asp:comparevalidator id="CompValProductSection1_MinLineItemQuantity" runat="server" CssClass="LabelError" ControlToValidate="txtProductSection1_MinLineItemQuantity"
										ErrorMessage="The Minimum of Line Item Quantity in the Product Section 1 is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator>
								    </td>
							    </tr>							    
						    </table>
					    </td>
					    <td >&nbsp;&nbsp;</td>
					    <td >
						    
					    </td>
					    <td >&nbsp;&nbsp;</td>
					    <td >
						   
					    </td>
				    </tr>
			    </TBODY>
		    </table>
       </td>
	</tr>
	<tr>
	    <td>
	        <table id="tblProductSection2" cellSpacing="0" cellPadding="0" border="0" runat="server">
			    <TBODY>
				    <tr id="trProductSection2" runat="server">
					    <td colspan=3><asp:label id="Label14" runat="server" CssClass="StandardSectionLabel">For Section 2:&nbsp;</asp:label>
						</td>
				    </tr>
				    <tr id="trProductSection2_MinNbDayLeadTime" runat="server">
					    <td><asp:label id="Label15" runat="server" CssClass="StandardLabel">
							    <nobr>&nbsp;&nbsp;Minimum&nbsp;#&nbsp;Day&nbsp;Lead-Time:&nbsp;</nobr>
						    </asp:label></td>
					    <td></td>
					    <td>
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td><asp:textbox id="txtProductSection2_MinNbDayLeadTime" runat="server" CssClass="DescLabel" Columns=3 MaxLength="3"></asp:textbox></td>
								    <td><asp:comparevalidator id="CompValtxtProductSection2_MinNbDayLeadTime" runat="server" CssClass="LabelError" ControlToValidate="txtProductSection2_MinNbDayLeadTime"
										ErrorMessage="The Minimum of day lead-time in the Product Section 2 is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator>
								    </td>
							    </tr>							    
						    </table>
					    </td>
					    <td>&nbsp;&nbsp;</td>
			            <td>
				            <table cellSpacing="0" cellPadding="0" border="0">
					            <tr>
						            <td><asp:textbox id="txtSupplySection2_MinNbDayLeadTime" runat="server" CssClass="DescLabel" Columns=3 MaxLength="3"></asp:textbox></td>
						            <td><asp:comparevalidator id="CompValtxtSupplySection2_MinNbDayLeadTime" runat="server" CssClass="LabelError" ControlToValidate="txtSupplySection2_MinNbDayLeadTime"
								        ErrorMessage="The Minimum of day lead-time in the Supply Section 2 is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator>
						            </td>
					            </tr>							    
				            </table>
			            </td>
			            <td>&nbsp;&nbsp;</td>
			            <td >
				            <table cellSpacing="0" cellPadding="0" border="0">
					            <tr>
						            <td><asp:textbox id="txtOtherSection2_MinNbDayLeadTime" runat="server" CssClass="DescLabel" Columns=3 MaxLength="3"></asp:textbox></td>
						            <td><asp:comparevalidator id="CompValOtherSection2_MinNbDayLeadTime" runat="server" CssClass="LabelError" ControlToValidate="txtOtherSection2_MinNbDayLeadTime"
								        ErrorMessage="The Minimum of day lead-time in the Other Section 2 is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator>
						            </td>
					            </tr>							    
				            </table>
			            </td>
				    </tr>
				    <tr id="trProductSection2_MinTotalQuantity" runat="server">
					    <td><asp:label id="Label16" runat="server" CssClass="StandardLabel">
							    <nobr>&nbsp;&nbsp;Minimum&nbsp;Total&nbsp;Quantity:&nbsp;</nobr>
						    </asp:label></td>
					    <td></td>
					    <td>
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td><asp:textbox id="txtProductSection2_MinTotalQuantity" runat="server" CssClass="DescLabel" Columns=6 MaxLength="6"></asp:textbox></td>
								    <td><asp:comparevalidator id="CompValProductSection2_MinTotalQuantity" runat="server" CssClass="LabelError" ControlToValidate="txtProductSection2_MinTotalQuantity"
										ErrorMessage="The Minimum of Total Quantity in the Product Section 2 is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator>
								    </td>
							    </tr>							    
						    </table>
					    </td>
					    <td>&nbsp;&nbsp;</td>
			            <td>
				            <table cellSpacing="0" cellPadding="0" border="0">
					            <tr>
						            <td><asp:textbox id="txtSupplySection2_MinTotalQuantity" runat="server" CssClass="DescLabel" Columns=6 MaxLength="6"></asp:textbox></td>
						            <td><asp:comparevalidator id="CompValSupplySection2_MinTotalQuantity" runat="server" CssClass="LabelError" ControlToValidate="txtSupplySection2_MinTotalQuantity"
								        ErrorMessage="The Minimum of Total Quantity in the Product Section 2 is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator>
						            </td>
					            </tr>							    
				            </table>
			            </td>
			            <td>&nbsp;&nbsp;</td>
			            <td>
				            <table cellSpacing="0" cellPadding="0" border="0">
					            <tr>
						            <td><asp:textbox id="txtOtherSection2_MinTotalQuantity" runat="server" CssClass="DescLabel" Columns=6 MaxLength="6"></asp:textbox></td>
						            <td><asp:comparevalidator id="CompValOtherSection2_MinTotalQuantity" runat="server" CssClass="LabelError" ControlToValidate="txtOtherSection2_MinTotalQuantity"
								        ErrorMessage="The Minimum of Total Quantity in the Other Section 2 is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator>
						            </td>
					            </tr>							    
				            </table>
			            </td>
				    </tr>
				    <tr id="trProductSection2_MinTotalAmount" runat="server">
					    <td><asp:label id="Label17" runat="server" CssClass="StandardLabel">
							    <nobr>&nbsp;&nbsp;Minimum Total&nbsp;Amount:&nbsp;</nobr>
						    </asp:label></td>
					    <td></td>
					    <td >
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td><asp:textbox id="txtProductSection2_MinTotalAmount" runat="server" CssClass="DescLabel" Columns=8 MaxLength="8"></asp:textbox></td>
								    <td><asp:comparevalidator id="CompValProductSection2_MinTotalAmount" runat="server" CssClass="LabelError" ControlToValidate="txtProductSection2_MinTotalAmount"
										ErrorMessage="The Minimum of Total Amount in the Product Section 1 is invalid (must be a number)." Type="Currency" Operator="DataTypeCheck">*</asp:comparevalidator>
								    </td>
							    </tr>							    
						    </table>
					    </td>
					    <td>&nbsp;&nbsp;</td>
					    <td >
				            <table cellSpacing="0" cellPadding="0" border="0">
					            <tr>
						            <td><asp:textbox id="txtSupplySection2_MinTotalAmount" runat="server" CssClass="DescLabel" Columns=8 MaxLength="8"></asp:textbox></td>
						            <td><asp:comparevalidator id="CompValSupplySection2_MinTotalAmount" runat="server" CssClass="LabelError" ControlToValidate="txtSupplySection2_MinTotalAmount"
								        ErrorMessage="The Minimum of Total Amount in the Supply Section 1 is invalid (must be a number)." Type="Currency" Operator="DataTypeCheck">*</asp:comparevalidator>
						            </td>
					            </tr>							    
				            </table>
			            </td>
			            <td>&nbsp;&nbsp;</td>
					    <td >
				            <table cellSpacing="0" cellPadding="0" border="0">
					            <tr>
						            <td><asp:textbox id="txtOtherSection2_MinTotalAmount" runat="server" CssClass="DescLabel" Columns=8 MaxLength="8"></asp:textbox></td>
						            <td><asp:comparevalidator id="CompValOtherSection2_MinTotalAmount" runat="server" CssClass="LabelError" ControlToValidate="txtOtherSection2_MinTotalAmount"
								        ErrorMessage="The Minimum of Total Amount in the Other Section 1 is invalid (must be a number)." Type="Currency" Operator="DataTypeCheck">*</asp:comparevalidator>
						            </td>
					            </tr>							    
				            </table>
			            </td>
				    </tr>
				    <tr id="tr6" runat="server">
					    <td><asp:label id="Label32" runat="server" CssClass="StandardLabel">
							    <nobr>&nbsp;&nbsp;Minimum&nbsp;Line&nbsp;Item&nbsp;Quantity:&nbsp;</nobr>
						    </asp:label></td>
					    <td ></td>
					    <td >
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td><asp:textbox id="txtProductSection2_MinLineItemQuantity" runat="server" CssClass="DescLabel" Columns=6 MaxLength="6"></asp:textbox></td>
								    <td><asp:comparevalidator id="CompValProductSection2_MinLineItemQuantity" runat="server" CssClass="LabelError" ControlToValidate="txtProductSection2_MinLineItemQuantity"
										ErrorMessage="The Minimum of Line Item Quantity in the Product Section 2 is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator>
								    </td>
							    </tr>							    
						    </table>
					    </td>
					    <td >&nbsp;&nbsp;</td>
					    <td >
						    
					    </td>
					    <td >&nbsp;&nbsp;</td>
					    <td >
						   
					    </td>
				    </tr>
			    </TBODY>
		    </table>
       </td>
	</tr>
	<tr>
	    <td>
	        <table id="tblProductSection3" cellSpacing="0" cellPadding="0" border="0" runat="server">
			    <TBODY>
				    <tr id="trProductSection3" runat="server">
					    <td colspan=3><asp:label id="Label18" runat="server" CssClass="StandardSectionLabel">For Section 3:&nbsp;</asp:label>
						</td>
				    </tr>
				    <tr id="trProductSection3_MinNbDayLeadTime" runat="server">
					    <td><asp:label id="Label19" runat="server" CssClass="StandardLabel">
							    <nobr>&nbsp;&nbsp;Minimum&nbsp;#&nbsp;Day&nbsp;Lead-Time:&nbsp;</nobr>
						    </asp:label></td>
					    <td></td>
					    <td>
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td><asp:textbox id="txtProductSection3_MinNbDayLeadTime" runat="server" CssClass="DescLabel" Columns=3 MaxLength="3"></asp:textbox></td>
								    <td><asp:comparevalidator id="CompValProductSection3_MinNbDayLeadTime" runat="server" CssClass="LabelError" ControlToValidate="txtProductSection3_MinNbDayLeadTime"
										ErrorMessage="The Minimum of day lead-time in the Product Section 3 is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator>
								    </td>
							    </tr>							    
						    </table>
					    </td>
					    <td>&nbsp;&nbsp;</td>
			            <td >
				            <table cellSpacing="0" cellPadding="0" border="0">
					            <tr>
						            <td><asp:textbox id="txtSupplySection3_MinNbDayLeadTime" runat="server" CssClass="DescLabel" Columns=3 MaxLength="3"></asp:textbox></td>
						            <td><asp:comparevalidator id="CompValSupplySection3_MinNbDayLeadTime" runat="server" CssClass="LabelError" ControlToValidate="txtSupplySection3_MinNbDayLeadTime"
								        ErrorMessage="The Minimum of day lead-time in the Supply Section 3 is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator>
						            </td>
					            </tr>							    
				            </table>
			            </td>
			            <td>&nbsp;&nbsp;</td>
			            <td >
				            <table cellSpacing="0" cellPadding="0" border="0">
					            <tr>
						            <td><asp:textbox id="txtOtherSection3_MinNbDayLeadTime" runat="server" CssClass="DescLabel" Columns=3 MaxLength="3"></asp:textbox></td>
						            <td><asp:comparevalidator id="CompValOtherSection3_MinNbDayLeadTime" runat="server" CssClass="LabelError" ControlToValidate="txtOtherSection3_MinNbDayLeadTime"
								        ErrorMessage="The Minimum of day lead-time in the Other Section 3 is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator>
						            </td>
					            </tr>							    
				            </table>
			            </td>
				    </tr>
				    <tr id="trProductSection3_MinTotalQuantity" runat="server">
					    <td><asp:label id="Label20" runat="server" CssClass="StandardLabel">
							    <nobr>&nbsp;&nbsp;Minimum&nbsp;Total&nbsp;Quantity:&nbsp;</nobr>
						    </asp:label></td>
					    <td></td>
					    <td>
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td><asp:textbox id="txtProductSection3_MinTotalQuantity" runat="server" CssClass="DescLabel" Columns=6 MaxLength="6"></asp:textbox></td>
								    <td><asp:comparevalidator id="CompValProductSection3_MinTotalQuantity" runat="server" CssClass="LabelError" ControlToValidate="txtProductSection3_MinTotalQuantity"
										ErrorMessage="The Minimum of Total Quantity in the Product Section 3 is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator>
								    </td>
							    </tr>							    
						    </table>
					    </td>
					    <td>&nbsp;&nbsp;</td>
			            <td>
				            <table cellSpacing="0" cellPadding="0" border="0">
					            <tr>
						            <td><asp:textbox id="txtSupplySection3_MinTotalQuantity" runat="server" CssClass="DescLabel" Columns=6 MaxLength="6"></asp:textbox></td>
						            <td><asp:comparevalidator id="CompValSupplySection3_MinTotalQuantity" runat="server" CssClass="LabelError" ControlToValidate="txtSupplySection3_MinTotalQuantity"
								        ErrorMessage="The Minimum of Total Quantity in the Supply Section 3 is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator>
						            </td>
					            </tr>							    
				            </table>
			            </td>
			            <td>&nbsp;&nbsp;</td>
			            <td>
				            <table cellSpacing="0" cellPadding="0" border="0">
					            <tr>
						            <td><asp:textbox id="txtOtherSection3_MinTotalQuantity" runat="server" CssClass="DescLabel" Columns=6 MaxLength="6"></asp:textbox></td>
						            <td><asp:comparevalidator id="CompValOtherSection3_MinTotalQuantity" runat="server" CssClass="LabelError" ControlToValidate="txtOtherSection3_MinTotalQuantity"
								        ErrorMessage="The Minimum of Total Quantity in the Other Section 3 is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator>
						            </td>
					            </tr>							    
				            </table>
			            </td>
				    </tr>
				    <tr id="trProductSection3_MinTotalAmount" runat="server">
					    <td><asp:label id="Label21" runat="server" CssClass="StandardLabel">
							    <nobr>&nbsp;&nbsp;Minimum Total&nbsp;Amount:&nbsp;</nobr>
						    </asp:label></td>
					    <td></td>
					    <td>
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td><asp:textbox id="txtProductSection3_MinTotalAmount" runat="server" CssClass="DescLabel" Columns=8 MaxLength="8"></asp:textbox></td>
								    <td><asp:comparevalidator id="CompValProductSection3_MinTotalAmount" runat="server" CssClass="LabelError" ControlToValidate="txtProductSection3_MinTotalAmount"
										ErrorMessage="The Minimum of Total Amount in the Product Section 1 is invalid (must be a number)." Type="Currency" Operator="DataTypeCheck">*</asp:comparevalidator>
								    </td>
							    </tr>							    
						    </table>
					    </td>
					     <td>&nbsp;&nbsp;</td>
					    <td >
				            <table cellSpacing="0" cellPadding="0" border="0">
					            <tr>
						            <td><asp:textbox id="txtSupplySection3_MinTotalAmount" runat="server" CssClass="DescLabel" Columns=8 MaxLength="8"></asp:textbox></td>
						            <td><asp:comparevalidator id="CompValSupplySection3_MinTotalAmount" runat="server" CssClass="LabelError" ControlToValidate="txtSupplySection3_MinTotalAmount"
								        ErrorMessage="The Minimum of Total Amount in the Supply Section 1 is invalid (must be a number)." Type="Currency" Operator="DataTypeCheck">*</asp:comparevalidator>
						            </td>
					            </tr>							    
				            </table>
			            </td>
			             <td>&nbsp;&nbsp;</td>
					    <td >
				            <table cellSpacing="0" cellPadding="0" border="0">
					            <tr>
						            <td><asp:textbox id="txtOtherSection3_MinTotalAmount" runat="server" CssClass="DescLabel" Columns=8 MaxLength="8"></asp:textbox></td>
						            <td><asp:comparevalidator id="CompValOtherSection3_MinTotalAmount" runat="server" CssClass="LabelError" ControlToValidate="txtOtherSection3_MinTotalAmount"
								        ErrorMessage="The Minimum of Total Amount in the Other Section 1 is invalid (must be a number)." Type="Currency" Operator="DataTypeCheck">*</asp:comparevalidator>
						            </td>
					            </tr>							    
				            </table>
			            </td>
				    </tr>
				    <tr id="tr9" runat="server">
					    <td><asp:label id="Label35" runat="server" CssClass="StandardLabel">
							    <nobr>&nbsp;&nbsp;Minimum&nbsp;Line&nbsp;Item&nbsp;Quantity:&nbsp;</nobr>
						    </asp:label></td>
					    <td ></td>
					    <td >
						    <table cellSpacing="0" cellPadding="0" border="0">
							    <tr>
								    <td><asp:textbox id="txtProductSection3_MinLineItemQuantity" runat="server" CssClass="DescLabel" Columns=6 MaxLength="6"></asp:textbox></td>
								    <td><asp:comparevalidator id="CompValProductSection3_MinLineItemQuantity" runat="server" CssClass="LabelError" ControlToValidate="txtProductSection3_MinLineItemQuantity"
										ErrorMessage="The Minimum of Line Item Quantity in the Product Section 2 is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator>
								    </td>
							    </tr>							    
						    </table>
					    </td>
					    <td >&nbsp;&nbsp;</td>
					    <td >
						    
					    </td>
					    <td >&nbsp;&nbsp;</td>
					    <td >
						   
					    </td>
				    </tr>
			    </TBODY>
		    </table>
       </td>
	</tr>
	<tr>
	    <td>
	        <hr color=black size=1px width=100% />
	    </td>
	</tr>    
    <tr>
	    <td>
	        <br>
	    </td>
	</tr>   
	<tr>
	    <td class="SectionPageTitleInfo">
	        <asp:label id="Label7" runat="server">
	            Custom Business rules
            </asp:label>
       </td>
	</tr>
	
	<TR>
		<TD>
			<asp:ImageButton id="imgBtnAddNew" runat="server" ImageUrl="~/images/BtnAdd.gif" CommandName="Add"
				CausesValidation="False"></asp:ImageButton>
		</TD>
	</TR>
	<tr>
		<td><asp:datalist id=dtLstBizRule  runat="server" DataKeyField="business_rule_id" width="500px" DataSource="<%# DVBizRule %>">
				<SeparatorTemplate>
					<hr width="100%" size="1px" color="#003366">
				</SeparatorTemplate>
				<ItemTemplate>
					<TABLE cellSpacing="0" cellPadding="3" border="3" style="BORDER-RIGHT: 1px outset; BORDER-TOP: 1px outset; BORDER-LEFT: 1px outset; BORDER-BOTTOM: 1px outset">
						<TR>
							<TD>
								<TABLE cellSpacing="0" cellPadding="3" width="500px" border="0">
									<TR>
										<TD>
											<asp:label id="lblName" CssClass="StandardLabel" runat="server">Rule&nbsp;Name&nbsp;:&nbsp;</asp:label></TD>
										<TD>
											<table border="0" cellpadding="0" cellspacing="0">
												<tr>
													<td>
														<asp:TextBox id="txtName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.business_rule_name") %>' Width="350px">
														</asp:TextBox>
													</td>
													<td>
														<asp:RequiredFieldValidator id="ReqFldVal_Name" CssClass="LabelError" runat="server" ErrorMessage="The Name is required"
															ControlToValidate="txtName">*</asp:RequiredFieldValidator>
													</td>
												</tr>
											</table>
										</TD>
									</TR>
									<TR>
										<TD>
											<asp:label id="Label3" CssClass="StandardLabel" runat="server">Operation&nbsp;:&nbsp;</asp:label>
										</TD>
										<TD vAlign="top">
											<table border="0" cellpadding="0" cellspacing="0">
												<tr>
													<td>
														<asp:label id="Label1" CssClass="StandardLabel" runat="server">Biz Field</asp:label>
													</td>
													<td>
														&nbsp;
													</td>
													<td>
														<asp:label id="Label2" CssClass="StandardLabel" runat="server">Operator</asp:label>
													</td>
													<td>
														&nbsp;
													</td>
													<td>
														<asp:label id="Label4" CssClass="StandardLabel" runat="server">Value</asp:label></td>
												</tr>
												<tr>
													<td>
														<table border="0" cellpadding="0" cellspacing="0">
															<tr>
																<td>
																	<asp:DropDownList id="ddlBizField" runat="server"  DataSource="<%# tblBizField %>" DataTextField="description" DataValueField="field_id" SelectedIndex='<%# getSelectedIndex(tblBizField,Convert.ToString(DataBinder.Eval(Container, "DataItem.field_id"))) %>'>
																	</asp:DropDownList>
																</td>
																<td>
																	<asp:RequiredFieldValidator id="ReqFldVal_BizField" CssClass="LabelError" runat="server" ErrorMessage="The Biz Field is required"
																		ControlToValidate="ddlBizField">*</asp:RequiredFieldValidator>
																</td>
															</tr>
														</table>
													</td>
													<td>
														&nbsp;
													</td>
													<td>
														<asp:DropDownList id="ddlOperator" runat="server" Width="130px" DataSource="<%# tblOperator %>" DataTextField="logical_operator_name" DataValueField="logical_operator_id" SelectedIndex='<%# getSelectedIndex(tblOperator,Convert.ToString(DataBinder.Eval(Container, "DataItem.logical_operator_id"))) %>'>
														</asp:DropDownList>
													</td>
													<td>
														&nbsp;&nbsp;
													</td>
													<td>
														<asp:TextBox id="txtValueToCompare" runat="server" Width="80px" Text='<%# DataBinder.Eval(Container, "DataItem.value_to_compare") %>' >
														</asp:TextBox>
													</td>
												</tr>
												
											</table>
										</TD>
									</TR>
									<tr>
							            <td>
								            <asp:label id="Label5" CssClass="StandardLabel" runat="server">Section&nbsp;Type:&nbsp;</asp:label>
							            </td>
							            <TD vAlign="top">
											<table border="0" cellpadding="0" cellspacing="0">
												<tr>
							                        <td>
								                        <table border="0" cellpadding="0" cellspacing="0">
									                        <tr>
										                        <td>
											                        <asp:DropDownList id="ddlFormSectionType" runat="server" Width="200px" DataSource='<%# DVFormSectionType %>' DataTextField="form_section_type_name" DataValueField="form_section_type_id" SelectedIndex='<%# getSelectedIndex(DVFormSectionType, Convert.ToString(DataBinder.Eval(Container, "DataItem.form_section_type_id"))) %>'>
											                        </asp:DropDownList>
										                        </td>
										                        <td>
								                                    &nbsp;&nbsp;
							                                    </td>
							                                    <td>
								                                    <asp:label id="Label6" CssClass="StandardLabel" runat="server">Section&nbsp;Number:&nbsp;</asp:label>
							                                    </td>
							                                    <td>
								                                    &nbsp;
							                                    </td>
							                                    <td>
								                                    <asp:textbox id="txtSectionNumber" Columns="1" MaxLength=1 CssClass="StandardTextBox" runat="server"></asp:textbox>
							                                    </td>    
									                        </tr>
								                        </table>
							                        </td>
							                    </TR>
							                  </table>
							               </TD>
						            </tr>
									<tr>
										<td colspan="3">
											<asp:ImageButton id="imgBtnDelete" runat="server" ImageUrl="~/images/BtnDelete.gif" CommandName="Delete"
												CausesValidation="False"></asp:ImageButton>
										</td>
									</tr>
								</TABLE>
							</TD>
						</TR>
					</TABLE>
				</ItemTemplate>
			</asp:datalist>
		</td>
	</tr>
	<tr>
		<td align="center"></td>
	</tr>
</table>
