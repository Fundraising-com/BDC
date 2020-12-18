<%@ Control Language="c#" AutoEventWireup="True" Codebehind="CreditInfo.ascx.cs" Inherits="EFundraisingCRMWeb.Components.User.CreditCheck.CreditInfo" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table cellpadding="0" cellspacing="0" style="WIDTH: 472px; HEIGHT: 176px">
	<TR>
		<TD class="NormalTextBold" width="222" style="WIDTH: 222px; HEIGHT: 18px" colSpan="2">
			<asp:Label id="Label2" CssClass="FrameTitleColor" runat="server">Credit Check Request</asp:Label></TD>
		<TD class="NormalTextBold" style="WIDTH: 20px; HEIGHT: 18px" width="20"></TD>
		<TD class="NormalTextBold" width="60" style="WIDTH: 60px; HEIGHT: 18px"></TD>
		<TD vAlign="top" style="HEIGHT: 18px"></TD>
		<TD style="WIDTH: 11px; HEIGHT: 18px" vAlign="top"></TD>
	</TR>
	<TR vAlign="top" height="20">
		<TD class="NormalTextBold" width="97" style="WIDTH: 97px; HEIGHT: 22px">Sale ID</TD>
		<TD vAlign="top" style="WIDTH: 131px; HEIGHT: 22px">
			<asp:textbox id=SaleIdTextBox runat="server" CssClass="NormalText normalTextBox" Width="136px" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>' Columns="9" BorderStyle="Solid" ReadOnly="True" ontextchanged="SaleIdTextBox_TextChanged">
			</asp:textbox></TD>
		<TD class="NormalTextBold" style="WIDTH: 20px; HEIGHT: 22px" width="20"></TD>
		<TD class="NormalTextBold" width="60" style="WIDTH: 60px; HEIGHT: 22px">Address</TD>
		<TD vAlign="top" style="HEIGHT: 22px">
			<asp:textbox id=AddressTextBox runat="server" CssClass="NormalText normalTextBox" Width="152px" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>' Columns="9" BorderStyle="Solid">
			</asp:textbox></TD>
		<TD vAlign="top" style="WIDTH: 11px; HEIGHT: 22px" align="left">
			<asp:Label id="AddressValidatorLabel" runat="server" ForeColor="Red" Visible="False">*</asp:Label></TD>
	</TR>
	<TR vAlign="top" height="20">
		<TD class="NormalTextBold" style="WIDTH: 97px">First Name</TD>
		<TD vAlign="top" style="WIDTH: 131px">
			<asp:textbox id=FirstNameTextBox runat="server" CssClass="NormalText normalTextBox" Width="136px" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>' Columns="9" BorderStyle="Solid">
			</asp:textbox></TD>
		<TD class="NormalTextBold" style="WIDTH: 20px">
			<asp:Label id="FirstNameValidatorLabel" runat="server" ForeColor="Red" Visible="False">*</asp:Label></TD>
		<TD class="NormalTextBold" style="WIDTH: 60px">State</TD>
		<TD vAlign="top">
			<asp:DropDownList id="StateDropDownList" Width="152px" runat="server"></asp:DropDownList></TD>
		<TD vAlign="top" style="WIDTH: 11px"></TD>
	</TR>
	<TR vAlign="top" height="20">
		<TD class="NormalTextBold" style="WIDTH: 97px; HEIGHT: 22px">Last Name</TD>
		<TD vAlign="top" style="WIDTH: 131px; HEIGHT: 22px">
			<asp:textbox id=LastNameTextBox runat="server" CssClass="NormalText normalTextBox" Width="136px" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>' Columns="9" BorderStyle="Solid">
			</asp:textbox></TD>
		<TD class="NormalTextBold" style="WIDTH: 20px; HEIGHT: 22px">
			<asp:Label id="LastNameValidatorLabel" runat="server" ForeColor="Red" Visible="False">*</asp:Label></TD>
		<TD class="NormalTextBold" style="WIDTH: 60px; HEIGHT: 22px">City</TD>
		<TD vAlign="top" style="HEIGHT: 22px">
			<asp:textbox id=CityTextBox runat="server" CssClass="NormalText normalTextBox" Width="152px" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>' Columns="9" BorderStyle="Solid">
			</asp:textbox></TD>
		<TD vAlign="top" style="WIDTH: 11px; HEIGHT: 22px">
			<asp:Label id="CityValidatorLabel" runat="server" ForeColor="Red" Visible="False">*</asp:Label></TD>
	</TR>
	<TR vAlign="top" height="20">
		<TD class="NormalTextBold" style="WIDTH: 97px">Amount</TD>
		<TD vAlign="top" style="WIDTH: 131px">
			<asp:textbox id=AmountTextBox runat="server" CssClass="NormalText normalTextBox" Width="136px" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>' Columns="9" BorderStyle="Solid">
			</asp:textbox></TD>
		<TD class="NormalTextBold" style="WIDTH: 20px" width="20">
			<asp:Label id="AmountValidatorLabel" runat="server" ForeColor="Red" Visible="False">*</asp:Label></TD>
		<TD class="NormalTextBold" width="60" style="WIDTH: 60px">Zip</TD>
		<TD vAlign="top">
			<asp:textbox id=ZipTextBox runat="server" CssClass="NormalText normalTextBox" Width="152px" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>' Columns="9" BorderStyle="Solid">
			</asp:textbox></TD>
		<TD vAlign="top" style="WIDTH: 11px">
			<asp:Label id="ZipValidatorLabel" runat="server" ForeColor="Red" Visible="False">*</asp:Label></TD>
	</TR>
	<TR vAlign="top" height="20">
		<TD class="NormalTextBold" style="WIDTH: 97px">SSN</TD>
		<TD vAlign="top" style="WIDTH: 131px">
			<asp:textbox id=SSNTextBox runat="server" CssClass="NormalText normalTextBox" Width="136px" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>' Columns="9" BorderStyle="Solid">
			</asp:textbox></TD>
		<TD class="NormalTextBold" style="WIDTH: 20px">
			<asp:Label id="SSNValidatorLabel" runat="server" ForeColor="Red" Visible="False">*</asp:Label></TD>
		<TD class="NormalTextBold" style="WIDTH: 60px"></TD>
		<TD vAlign="top"></TD>
		<TD vAlign="top" style="WIDTH: 11px"></TD>
	</TR>
	<TR>
		<TD colSpan="4" height="38" style="WIDTH: 313px; HEIGHT: 38px" vAlign="top">
			<asp:Label id="MessageLabel" runat="server" Visible="False" Font-Size="9pt" ForeColor="Red"></asp:Label></TD>
		<TD style="WIDTH: 12px; HEIGHT: 38px" height="38" vAlign="top" align="center">
			<asp:Button id="SendRequestButton" runat="server" Text="Send Request" CausesValidation="False" onclick="SendRequestButton_Click"></asp:Button></TD>
		<TD style="WIDTH: 11px; HEIGHT: 38px" height="38"></TD>
	</TR>
	<TR>
		<TD colSpan="4" style="WIDTH: 313px"></TD>
		<TD style="WIDTH: 12px"></TD>
		<TD style="WIDTH: 11px"></TD>
	</TR>
</table>
