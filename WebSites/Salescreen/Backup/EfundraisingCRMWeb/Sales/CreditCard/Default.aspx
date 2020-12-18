<%@ Page language="c#" Codebehind="Default.aspx.cs" AutoEventWireup="True" Inherits="efundraising.EFundraisingCRMWeb.Sales.CreditCard._Default" %>
<head runat="server" id="Header" />
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CreditCard Process</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Resources/Css/TallySale.css" type="text/css" rel="stylesheet">
		<LINK href="../../Resources/Css/style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<asp:label id="LabelStatus" runat="server" Visible="False" Font-Bold="True" 
                ForeColor="Maroon"></asp:label>
			<asp:LinkButton id="VoidTransactionLinkButton" onclick="VoidTransactionLinkButton_Click" runat="server"
				Visible="False">Click here to void this transaction.</asp:LinkButton>
			<table class="NormalText" border="0">
				<tr>
					<td class="BigTextBold" colSpan="2">Credit Card Info</td>
				</tr>
				<tr>
					<td>Card Type :</td>
					<td><asp:dropdownlist id="DropDownListCCType" runat="server">
							<asp:ListItem Value="2" Selected="True">Visa</asp:ListItem>
							<asp:ListItem Value="3">MasterCard</asp:ListItem>
							<asp:ListItem Value="8">Amex</asp:ListItem>
							<asp:ListItem Value="9">Discover</asp:ListItem>
						</asp:dropdownlist></td>
				</tr>
				<tr>
					<td>Credit Card Number :
					</td>
					<td><asp:textbox id="TextBoxCCNumber" runat="server" MaxLength="16"></asp:textbox></td>
				</tr>
				<tr>
					<td style="HEIGHT: 3px">Expiry Date :
					</td>
					<td style="HEIGHT: 3px"><asp:dropdownlist id="DropDownListMonth" runat="server">
							<asp:ListItem Value="01" Selected="True">01</asp:ListItem>
							<asp:ListItem Value="02">02</asp:ListItem>
							<asp:ListItem Value="03">03</asp:ListItem>
							<asp:ListItem Value="04">04</asp:ListItem>
							<asp:ListItem Value="05">05</asp:ListItem>
							<asp:ListItem Value="06">06</asp:ListItem>
							<asp:ListItem Value="07">07</asp:ListItem>
							<asp:ListItem Value="08">08</asp:ListItem>
							<asp:ListItem Value="09">09</asp:ListItem>
							<asp:ListItem Value="10">10</asp:ListItem>
							<asp:ListItem Value="11">11</asp:ListItem>
							<asp:ListItem Value="12">12</asp:ListItem>
						</asp:dropdownlist>&nbsp;/&nbsp;
						<asp:dropdownlist id="DropDownListYear" runat="server">
							
							<asp:ListItem Value="10" Selected="True">2010</asp:ListItem>
							<asp:ListItem Value="11">2011</asp:ListItem>
							<asp:ListItem Value="12">2012</asp:ListItem>
							<asp:ListItem Value="13">2013</asp:ListItem>
							<asp:ListItem Value="14">2014</asp:ListItem>
							<asp:ListItem Value="15">2015</asp:ListItem>
							<asp:ListItem Value="16">2016</asp:ListItem>
							<asp:ListItem Value="17">2017</asp:ListItem>
							<asp:ListItem Value="18">2018</asp:ListItem>
							<asp:ListItem Value="19">2019</asp:ListItem>
							<asp:ListItem Value="20">2020</asp:ListItem>
						</asp:dropdownlist></td>
				</tr>
				<tr>
					<td>Name on CC :
					</td>
					<td><asp:textbox id="TextBoxName" runat="server" Width="184px"></asp:textbox></td>
				</tr>
				<tr>
					<td vAlign="bottom">Security Number :
					</td>
					<td>
						Verify Security Code ? (Does NOT work with AMEX)&nbsp;:<asp:RadioButtonList id="SecurityRadioButtonList" runat="server" Width="96px" Height="24px" RepeatDirection="Horizontal"
							CssClass="NormalText">
							<asp:ListItem Value="true">Yes</asp:ListItem>
							<asp:ListItem Value="false" Selected="True">No</asp:ListItem>
						</asp:RadioButtonList><asp:textbox id="TextBoxCVV2" runat="server" Width="64px"></asp:textbox></td>
				</tr>
				<tr>
					<td>Street Address :</td>
					<td><asp:textbox id="TextBoxStreetAddress" runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td>City :</td>
					<td><asp:textbox id="TextBoxCity" runat="server" Width="144px"></asp:textbox></td>
				</tr>
				<tr>
					<td>State :</td>
					<td><asp:textbox id="TextBoxState" runat="server" Width="32px"></asp:textbox></td>
				</tr>
				<tr>
					<td>Zip Code :</td>
					<td><asp:textbox id="TextBoxZipCode" runat="server" Width="56px"></asp:textbox></td>
				</tr>
				<tr>
					<td>Country:</td>
					<td><asp:dropdownlist id="DropDownListCountry" runat="server">
							<asp:ListItem Value="US" Selected="True">US</asp:ListItem>
							<asp:ListItem Value="CA">CA</asp:ListItem>
							
						</asp:dropdownlist></td>
				</tr>
			</table>
			<P><span class="BigTextBold">Client Pending Sales :</span><br>
				<br>
				<asp:datagrid id="SalesDataGrid" runat="server" Width="650px" ShowFooter="True" CellPadding="2"
					AutoGenerateColumns="False" Font-Names="Arial" Font-Size="11pt">
					<AlternatingItemStyle BackColor="#DEDEE7"></AlternatingItemStyle>
					<HeaderStyle Font-Size="10pt" Font-Bold="True" BackColor="#6795C3"></HeaderStyle>
					<FooterStyle Font-Size="10pt" Font-Bold="True" BackColor="#6795C3"></FooterStyle>
					<Columns>
						<asp:TemplateColumn>
							<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
							<HeaderTemplate>
								<asp:Label id="Label2" runat="server">Select :</asp:Label><BR>
							</HeaderTemplate>
							<ItemTemplate>
								<asp:RadioButton id="CheckBoxInclude" runat="server"  OnCheckedChanged="CheckBoxInclude_CheckeckChanged"
									AutoPostBack="True" ValidationGroup="a"></asp:RadioButton>
							</ItemTemplate>
							<FooterStyle HorizontalAlign="Center" VerticalAlign="Middle"></FooterStyle>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="sale_id" HeaderText="SaleID"></asp:BoundColumn>
						<asp:BoundColumn DataField="product_class" HeaderText="Product Class"></asp:BoundColumn>
						<asp:BoundColumn DataField="sale_status" HeaderText="Sale Status"></asp:BoundColumn>
						<asp:BoundColumn Visible="False" DataField="amount"></asp:BoundColumn>
						<asp:BoundColumn DataField="amount_display" HeaderText="Amount"></asp:BoundColumn>
					</Columns>
				</asp:datagrid>
                <asp:Label ID="DevLabel" runat="server" Font-Bold="True" ForeColor="#CC3300" 
                    Text="Credit Card will not be processed. Contact IT"></asp:Label>
                <br>
				<asp:label id="Label1" runat="server">
					Amount to charge on Credit Card :  $</asp:label>&nbsp;&nbsp;<asp:textbox id="AmountTextBox" runat="server" Width="64px">0</asp:textbox>&nbsp;&nbsp;<asp:button 
                    id="newProcessButton" runat="server" Text="Process" 
                    onclick="newProcessButton_Click"></asp:button>
                &nbsp;<asp:Label ID="Label4" runat="server" ForeColor="#FF3300" 
                    Text="Click Only Once"></asp:Label>
                &nbsp;&nbsp; <asp:regularexpressionvalidator id="revAmount" runat="server" 
                    ErrorMessage="RegularExpressionValidator" ValidationExpression="[\d.]+"
					ControlToValidate="AmountTextBox" Font-Bold="True">This is not a valid amount.</asp:regularexpressionvalidator>
                <asp:button id="ProcessButton0" runat="server" Text="test" 
                    onclick="ProcessButtontest_Click" Visible="False"></asp:button>
				<asp:Label ID="versionLabel" runat="server" ForeColor="#CCCCCC" Text="v.2.0"></asp:Label>
                <asp:button 
                    id="ProcessButton" runat="server" Text="Old Process" 
                    onclick="ProcessButton_Click" Visible="False"></asp:button>
				.</P>
		    <p>
                &nbsp;</p>
		</form>
	</body>
</HTML>
