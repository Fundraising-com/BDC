<%@ Register TagPrefix="uc1" TagName="CAttributeControl" Src="../controls/CSRAttributes.ascx" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="CSRShippingPackages.ascx.vb" Inherits="StoreFront.StoreFront.CSRShippingPackages" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<asp:datalist id="packages" CellSpacing="0" Width="100%" RepeatLayout="Table" runat="server" cellpadding="0">
	<HeaderTemplate>
		<TR>
			<TD class="ContentTableHeader" align="left" colspan="8" style="padding-left:3px;">Shipping 
				Information</TD>
		</TR>
	</HeaderTemplate>
	<ItemTemplate>
		<TR>
			<TD class="ContentTableHeader" align="left" width="1"></TD>
			<TD class="ErrorMessages" align="left" colspan="6">
				<asp:Label ID="BackupError" Runat="server"></asp:Label>
			<TD class="ContentTableHeader" align="left" width="1"></TD>
		</TR>
		<TR>
			<TD class="ContentTableHeader" align="left" width="1"><img src="images/clear.gif" width="1"></TD>
			<TD class="Content" align="left" nowrap width="130px" style="padding-left:5px; line-height:20px;"><b>Package&nbsp;
					<asp:Label ID="lblIndex" Runat=server text="<%# Container.itemindex + 1 %>">
					</asp:Label>:&nbsp;&nbsp;<%# DataBinder.Eval(Container.DataItem,"Address.NickName") %>&nbsp;</b>
				<br />
				<asp:LinkButton ID="Recalculate" Runat="server" CommandArgument='<%# Container.ItemIndex %>' OnClick=RecalculateShipping >
					<asp:Image ID="imgRecalculate" Runat="server" ImageUrl="../images/calculate.jpg"></asp:Image>
				</asp:LinkButton></TD>
			<TD class="Content" align="left" nowrap="true" valign="bottom">
				<asp:Label ID="lblDisplayShippingMethod" Runat="server"></asp:Label>
				<asp:CheckBox ID="PremiumShipping" Runat="server" OnCheckedChanged="SetPremiumShipping" AutoPostBack="True"
					Runat="server"></asp:CheckBox>
			</TD>
			<TD class="Content" align="left" width="25%">
				<asp:Label ID="lblShippingCarrier" Runat="server">Shipping&nbsp;Carrier:&nbsp;</asp:Label>
				<asp:DropDownList AutoPostBack="True" OnSelectedIndexChanged="ChangeCarrier" ID="ShippingCarrier"
					Runat="server"></asp:DropDownList></TD>
			<TD class="Content" align="left" width="25%" width="1">
				<asp:Label ID="lblShippingMethod" Runat="server">Shipping&nbsp;Method:&nbsp;</asp:Label>
				<asp:DropDownList AutoPostBack="True" OnSelectedIndexChanged="ChangeMethod" ID="ShippingMethod" Runat="server"></asp:DropDownList></TD>
			<TD class="Content" align="left" colspan="2" nowrap style="padding-right:2px;">
				<asp:CheckBox ID="chkNoShipping" EnableViewState="True" AutoPostBack="True" Runat="server" Text="Do Not 
				Charge Shipping" Enabled='<%# AllowNoShipping() %>'></asp:CheckBox><br>
				<asp:CheckBox ID="chkNoTax" EnableViewState="True" AutoPostBack="True" Runat="server" Text="Do&nbsp;Not 
				Charge&nbsp;Tax" Enabled='<%# AllowNoTaxes() %>'></asp:CheckBox></TD>
			<TD class="ContentTableHeader" align="left" width="1"><img src="images/clear.gif" width="1"></TD>
		</TR>
		<TR>
			<TD class="ContentTableHeader" align="left" width="1"></TD>
			<TD class="Content" align="left" colspan="4" style="padding-left:5px; line height:25px;">
				<asp:LinkButton OnClick=SetSpecialInstructions CommandArgument='<%# Container.ItemIndex %>' ID="cmdSpecialInstructions" Runat=server>Special Instructions</asp:LinkButton>
				<asp:TextBox ID="SpecialInstructions" Runat="server" TextMode="MultiLine" columns="40"></asp:TextBox></TD>
			<TD class="Content" align="left" colspan="2" nowrap style="padding-right:2px;">
			</TD>
			<TD class="ContentTableHeader" align="left" width="1"></TD>
		</TR>
		<TR>
			<TD class="ContentTableHeader" align="left" width="1" colspan="8" height="1"></TD>
		</TR>
	</ItemTemplate>
</asp:datalist>
