<%@ Control Language="c#" AutoEventWireup="True" Codebehind="SaleInfo.ascx.cs" Inherits="EFundraisingCRMWeb.Components.User.Sales.SaleInfo" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table cellSpacing="0" cellPadding="1" width="296" style="WIDTH: 296px; HEIGHT: 149px">
	<tr vAlign="top" height="20">
		<td class="NormalText" style="WIDTH: 130px; HEIGHT: 12px" width="130">Consultant 
			Name</td>
		<td style="HEIGHT: 12px" vAlign="top"><asp:dropdownlist id="consultantDropDownList" CssClass="NormalText" Runat="server" Width="160px"></asp:dropdownlist></td>
	</tr>
	<tr vAlign="top" height="20">
		<td class="NormalText" style="WIDTH: 130px; HEIGHT: 6px">Sale ID
		</td>
		<td vAlign="top" style="HEIGHT: 6px"><asp:textbox id="saleIdTextBox" CssClass="NormalText normalTextBox" Runat="server" ReadOnly="True"
				BorderStyle="Solid"></asp:textbox></td>
	</tr>
	<tr vAlign="top" height="20">
		<td class="NormalText" style="WIDTH: 130px; HEIGHT: 3px">PO Status
		</td>
		<td vAlign="top" style="HEIGHT: 3px"><asp:dropdownlist id="poNoStatusDropdownlist" CssClass="NormalText normalTextBox" Runat="server" ></asp:dropdownlist>
            <asp:CheckBox ID="chkPOComm" runat="server"  Visible="true" Checked="false"/>
        </td>
	</tr>
	<tr vAlign="top" height="20">
		<td class="NormalText" style="WIDTH: 130px; HEIGHT: 2px">Carrier
		</td>
		<td vAlign="top" style="HEIGHT: 2px"><asp:dropdownlist id="carrierDropdownlist" CssClass="NormalText" Runat="server"></asp:dropdownlist></td>
	</tr>
	<tr vAlign="top" height="20">
		<td class="NormalText" style="WIDTH: 130px; HEIGHT: 1px">Shipping Option
		</td>
		<td vAlign="top" style="HEIGHT: 1px"><asp:dropdownlist id="shippingOptionDropdownlist" CssClass="NormalText" Runat="server"></asp:dropdownlist></td>
	</tr>
	<tr vAlign="top" height="20">
		<td class="NormalText" style="WIDTH: 130px">Waybill #
		</td>
		<td vAlign="top"><asp:textbox id="waybillNoTextBox" 
                CssClass="NormalText specialTextBox" Runat="server" BorderStyle="Solid" 
                ReadOnly="True"></asp:textbox></td>
	</tr>
</table>
