<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerInformation.ascx.cs" Inherits="EFundraisingCRMWeb.Components.User.ClientControls.CustomerInformation" %>

<table cellpadding="0" cellspacing="0">
	<TR>
		<TD style="HEIGHT: 137px; width: 620px;" vAlign="top">
			<asp:Panel id="Panel1" runat="server" BorderStyle="Solid" BorderWidth="2px" Width="625px" CssClass="Frame">
				<TABLE class="NormalText" id="Table2" cellSpacing="1" cellPadding="1" border="0" style="width: 630px">
					<TR>
						<TD style="WIDTH: 220px">
							<asp:Label id="Label2" Width="104px" runat="server">Client ID:</asp:Label></TD>
						<TD style="WIDTH: 108px">
							<asp:TextBox id="ClientIDTextBox" CssClass="NormalText normalTextBox" BorderStyle="Solid" runat="server"
								ReadOnly="True"></asp:TextBox></TD>
						<TD></TD>
                        <td style="width: 37px">
                        </td>
						<TD style="WIDTH: 243px">
							<asp:Label id="Label5" Width="72px" runat="server">Email:</asp:Label></TD>
						<TD style="WIDTH: 318px">
							<asp:TextBox id="EmailTextBox" CssClass="NormalText normalTextBox" BorderStyle="Solid" runat="server" Width="162px"></asp:TextBox></TD>
						<TD style="width: 2px"></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 220px">
							<asp:Label id="Label1" Width="104px" runat="server">Sequence Code:</asp:Label></TD>
						<TD style="WIDTH: 108px">
							<asp:dropdownlist id="sequenceCodedropdownlist" CssClass="NormalText" Runat="server" Enabled="True"></asp:dropdownlist></TD>
						<TD></TD>
                        <td style="width: 37px">
                        </td>
						<TD style="WIDTH: 243px">
							<asp:Label id="Label6" Width="88px" runat="server">Day Phone:</asp:Label></TD>
						<TD style="WIDTH: 318px">
							<asp:TextBox id="dPhone1" CssClass="NormalText normalTextBox" Width="28px" BorderStyle="Solid"
								runat="server" MaxLength="3"></asp:TextBox>
							<asp:TextBox id="dPhone2" CssClass="NormalText normalTextBox" Width="28px" BorderStyle="Solid"
								runat="server" MaxLength="3"></asp:TextBox>-
							<asp:TextBox id="dPhone3" CssClass="NormalText normalTextBox" Width="34px" BorderStyle="Solid"
								runat="server" MaxLength="4"></asp:TextBox>
							<asp:Label id="Label11" Width="10px" runat="server">x</asp:Label>
							<asp:TextBox id="dPhone4" CssClass="NormalText normalTextBox" Width="34px" BorderStyle="Solid"
								runat="server" MaxLength="5"></asp:TextBox>
							<asp:CheckBox id="dCheckBox" Width="17px" runat="server" Text=" " Visible="False"></asp:CheckBox>
							<asp:Label ID="dayPhoneErrorLabel" runat="server" ForeColor="Red">*</asp:Label></TD>
						<TD style="width: 2px">
							&#160;</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 220px; HEIGHT: 27px">
							<asp:Label id="Label3" Width="104px" runat="server">First Name:</asp:Label></TD>
						<TD style="WIDTH: 108px; HEIGHT: 27px">
							<asp:TextBox id="FirstNameTextBox" CssClass="NormalText normalTextBox" BorderStyle="Solid" runat="server"></asp:TextBox></TD>
						<TD style="HEIGHT: 27px">
							<asp:Label id="FirstNameErrorLabel" runat="server" ForeColor="Red">*</asp:Label></TD>
                        <td style="width: 37px; height: 27px">
                        </td>
						<TD style="WIDTH: 243px; HEIGHT: 27px">
							<asp:Label id="Label7" Width="104px" runat="server">Evening Phone:</asp:Label></TD>
						<TD style="WIDTH: 318px; HEIGHT: 27px">
							<asp:TextBox id="ePhone1" CssClass="NormalText normalTextBox" Width="28px" BorderStyle="Solid"
								runat="server" MaxLength="3"></asp:TextBox>
							<asp:TextBox id="ePhone2" CssClass="NormalText normalTextBox" Width="28px" BorderStyle="Solid"
								runat="server" MaxLength="3"></asp:TextBox>-
							<asp:TextBox id="ePhone3" CssClass="NormalText normalTextBox" Width="34px" BorderStyle="Solid"
								runat="server" MaxLength="4"></asp:TextBox>
							<asp:Label id="Label10" Width="10px" runat="server">x</asp:Label>
							<asp:TextBox id="ePhone4" CssClass="NormalText normalTextBox" Width="34px" BorderStyle="Solid"
								runat="server" MaxLength="5"></asp:TextBox>
							<asp:CheckBox id="eCheckBox" Width="17px" runat="server" Text=" " Visible="False"></asp:CheckBox>
							<asp:Label ID="EvePhoneErrorLabel" runat="server" ForeColor="Red">*</asp:Label></TD>
						<TD style="HEIGHT: 27px; width: 2px;">
							&#160;</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 220px">
							<asp:Label id="Label4" Width="104px" runat="server">Last Name:</asp:Label></TD>
						<TD style="WIDTH: 108px">
							<asp:TextBox id="LastnameTextbox" CssClass="NormalText normalTextBox" BorderStyle="Solid" runat="server"></asp:TextBox></TD>
						<TD>
							<asp:Label id="LastNameErrorLabel" runat="server" ForeColor="Red">*</asp:Label></TD>
                        <td style="width: 37px">
                        </td>
						<TD style="WIDTH: 243px">
							<asp:Label id="Label8" Width="104px" runat="server">Fax:</asp:Label></TD>
						<TD style="WIDTH: 318px">
							<asp:TextBox id="fPhone1" CssClass="NormalText normalTextBox" Width="28px" BorderStyle="Solid"
								runat="server" MaxLength="3"></asp:TextBox>
							<asp:TextBox id="fPhone2" CssClass="NormalText normalTextBox" Width="28px" BorderStyle="Solid"
								runat="server" MaxLength="3"></asp:TextBox>-
							<asp:TextBox id="fPhone3" CssClass="NormalText normalTextBox" Width="34px" BorderStyle="Solid"
								runat="server" MaxLength="4"></asp:TextBox></TD>
						<TD style="width: 2px"></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 220px; height: 26px;">Organization</TD>
						<TD style="WIDTH: 108px; height: 26px;">
							<asp:TextBox id="OrganizationTextBox" CssClass="NormalText normalTextBox" BorderStyle="Solid"
								runat="server"></asp:TextBox></TD>
						<TD style="height: 26px;">
							<asp:Label id="OrganizationErrorLabel" runat="server" ForeColor="Red">*</asp:Label></TD>
                        <td style="width: 37px; height: 26px">
                        </td>
						<TD style="WIDTH: 243px; height: 26px;">
							<asp:Label id="Label9" Width="120px" runat="server">Best Time To Call:</asp:Label></TD>
						<TD style="WIDTH: 318px; height: 26px;">
							<asp:dropdownlist id="TimeToCallDropdownlist" CssClass="NormalText normalTextBox" Runat="server" Enabled="True">
								<asp:ListItem Value="Any Time" Selected="True">Any Time</asp:ListItem>
								<asp:ListItem Value="Morning">Morning</asp:ListItem>
								<asp:ListItem Value="Afternoon">Afternoon</asp:ListItem>
								<asp:ListItem Value="Evening">Evening</asp:ListItem>
							</asp:dropdownlist></TD>
						<TD style="width: 2px; height: 26px"></TD>
					</TR>
				</TABLE>
			</asp:Panel>
		</TD>
	</TR>
</table>
