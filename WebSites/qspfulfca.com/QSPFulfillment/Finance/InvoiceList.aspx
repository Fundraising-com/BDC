<%@ Page language="c#" Codebehind="InvoiceList.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.Finance.InvoiceList" %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<HTML>
	<HEAD>
		<title>CA Fulfill System - Invoice List</title>
		<link REL="stylesheet" HREF="../Includes/MagSysStyle.css" TYPE="text/css">
	</HEAD>
	<body topmargin="0" leftmargin="0">
		<form method="post" runat="server" ID="InvoiceForm">
			<!--#include file="../Includes/Menu.inc"-->
			<br>
			<center>
				<h3><font face="Verdana" color="#2f4f88">Invoice List</font></h3>
			</center>
			<p></p>
			<table BORDER="0" CELLSPACING="0" CELLPADDING="0" WIDTH="100%" ALIGN="center">
				<tr>
					<td align="center">
						<b><font face="Verdana" size="2" color="#2f4f88">Search</font></b>&nbsp;
						<asp:TextBox ID="Search" Runat="server" CssClass="boxlook" />
						&nbsp;<b><font face="Verdana" size="2" color="#2f4f88">By</font></b>&nbsp;
						<asp:DropDownList id="ddlStatus" CssClass="boxlookW" runat="server">
							<asp:ListItem Text="Acct Name" Value="Name" />
							<asp:ListItem Text="Account ID" Value="AccountID" />
							<asp:ListItem Text="Order ID" Value="OrderID" />
							<asp:ListItem Text="Invoice ID" Value="InvoiceID" />
							<asp:ListItem Text="Campaign ID" Value="CampaignID" />
						</asp:DropDownList>
					</td>
					<td>
						<b><font face="Verdana" size="2" color="#2f4f88">Start:</font></b>&nbsp;
						<asp:TextBox Width="90" MaxLength="11" ID="FromDate" Runat="server" CssClass="boxlook" />&nbsp;
						<asp:RequiredFieldValidator Runat="server" id="Req8" ControlToValidate="FromDate" ErrorMessage="From Date" Display="Dynamic" />
						<asp:RegularExpressionValidator Runat="server" ID="RegExp8" ControlToValidate="FromDate" ValidationExpression="^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][u]l|[aA][Uu][gG]|[Ss][eE][pP]|[oO][Cc][Tt]|[Nn][oO][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$"
							ErrorMessage="<br>Please enter start date like 01-Jan-2000" Display="Dynamic" />
						<b><font face="Verdana" size="2" color="#2f4f88">End:</font></b>&nbsp;
						<asp:TextBox Width="90" MaxLength="11" ID="ToDate" Runat="server" CssClass="boxlook" />&nbsp;
						<asp:RequiredFieldValidator Runat="server" id="Req9" ControlToValidate="ToDate" ErrorMessage="To Date" Display="Dynamic" />
						&nbsp;&nbsp;<asp:LinkButton CausesValidation="False" ID="BtnSearch" Runat="server" CssClass="boxlook" OnClick="SearchButtonClick"
							Text="<font face='Verdana' color='#2f4f88'> Go </font>" />
					</td>
					<td>
						<asp:Label Runat="server" ID="lblInvoice" CssClass="ClearTextBoxG" />
					</td>
				</tr>
			</table>
			<!--INVOICE -->
			<table BORDER="0" CELLSPACING="0" CELLPADDING="2" WIDTH="100%" ALIGN="center">
				<tr>
					<td>
						<asp:DataGrid ID="InvoiceListDG" OnItemCommand="InvoiceListDG_Select" SelectedItemStyle-ForeColor="black"
							SelectedItemStyle-BackColor="#AAAAAA" HeaderStyle-Font-Bold="True" AllowSorting="true" OnSortCommand="InvoiceListDG_Sort"
							AllowPaging="True" PageSize="10" PagerStyle-Position="Bottom" PagerStyle-Mode="NumericPages"
							PagerStyle-HorizontalAlign="Center" PagerStyle-PageButtonCount="20" PagerStyle-Width="100%"
							PagerStyle-BackColor="#2f4f88" PagerStyle-ForeColor="white" OnPageIndexChanged="InvoiceListDG_Page"
							BackColor="#CCCCCC" runat="server" DataKeyField="Invoice_Id" AutoGenerateColumns="False" Width="100%"
							BorderColor="black" BorderWidth="1" GridLines="Both" CellPadding="2" CellSpacing="0" Font-Name="Verdana"
							Font-Size="8pt" HeaderStyle-BackColor="#2f4f88" onselectedindexchanged="InvoiceListDG_SelectedIndexChanged">
							<Columns>
								<asp:ButtonColumn Text="<img border=0 align=center alt='Adjustment' src='../Images/adj.gif'>" CommandName="Adjustment" />
								<asp:ButtonColumn Text="<img border=0 align=center alt='Payment' src='../Images/pay.gif'>" CommandName="Payment" />
								<asp:ButtonColumn Text="<img border=0 align=center alt='Details' src='../Images/details.gif'>" CommandName="Product" />
								<asp:TemplateColumn HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top" HeaderText="Invoice<br>ID"
									SortExpression="Invoice_ID" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "Invoice_ID") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top" HeaderText="Order<br>ID"
									SortExpression="Order_ID" HeaderStyle-Wrap="False">
									<ItemTemplate>
                                        <%# GetOrderID(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "Order_ID"))) %>										
                                      <asp:HiddenField ID="InvoiceListDG_OrderId" Value='<%# Convert.ToInt32(invOrderValue) %>'  Runat="Server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top" HeaderText="Campaign<br>ID"
									SortExpression="CampaignID" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "CampaignID") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top" HeaderText="Acct<br>ID"
									SortExpression="Account_ID" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "Account_ID") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top" HeaderText="Acct<br>Type"
									SortExpression="AccountType" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "AccountType") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top" HeaderText="Account Name"
									SortExpression="Group_Name" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "Group_Name") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
									HeaderText="Invoice<br>Date" SortExpression="Invoice_Date" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "Invoice_Date", "{0:dd-MMM-yyyy}") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
									HeaderText="Invoice<br>Due" SortExpression="Invoice_Due_Date" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "Invoice_Due_Date", "{0:dd-MMM-yyyy}") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
									HeaderText="Invoice<br>Amount" SortExpression="Invoice_Amount" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "Invoice_Amount", "{0:c}") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
									HeaderText="Adj" SortExpression="Adjustments" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "Adjustments", "{0:c}") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
									HeaderText="Pymt" SortExpression="Payments" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "Payments", "{0:c}") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
									HeaderText="Balance" SortExpression="Balance" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "Balance") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
									HeaderText="Printed?" SortExpression="Is_Printed" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "Is_Printed") %>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
				<!--ADJUSTMENT -->
				<tr>
					<td>
					<table width="100%">
							<tr>
								<td align="left">
									<asp:Label Runat="server" ID="lblAdjTitle" CssClass="ClearTextBoxB" />
								</td>
								<td>
									<asp:ValidationSummary Width="70%" DisplayMode="BulletList" CssClass="boxlookW" id="ValidationSummary1"
										runat="server" HeaderText="The following fields are required or incorrect:" />
								</td>
								<td align="right">
									<asp:Label Runat="server" ID="lblAdjPageNumber" CssClass="ClearTextBoxDarkGrey" />
								</td>
								<td align="right">
									<asp:Label Runat="server" ID="lblAdjustmentRecords" CssClass="ClearTextBoxDarkGrey" />
								</td>
								<td align="right">
									<asp:Label Runat="server" ID="lblShowAdj" CssClass="ClearTextBoxDarkGrey" />
									<asp:DropDownList Visible="False" id="ddlNumInvAdj" AutoPostBack="True" OnSelectedIndexChanged="Adjustment_PageIndexChanged"
										CssClass="boxlookW" runat="server">
										<asp:ListItem Text="10" Value="10" />
										<asp:ListItem Text="25" Value="25" />
										<asp:ListItem Text="50" Value="50" />
										<asp:ListItem Text="All" Value="All" />
									</asp:DropDownList>
								</td>
							</tr>
						</table>
						<asp:DataGrid ID="InvoiceListAdjustmentDG" OnItemDataBound="InvoiceListAdjustmentDG_ItemDataBound"
							DataKeyField="ADJUSTMENT_ID" OnSortCommand="InvoiceListAdjustmentDG_Sort" OnPageIndexChanged="InvoiceListAdjustmentDG_Page"
							AllowPaging="True" OnItemCommand="doAddAdjustment" ShowFooter="True" HeaderStyle-Font-Bold="True"
							AllowSorting="true" PageSize="10" PagerStyle-Mode="NumericPages" PagerStyle-HorizontalAlign="Center"
							PagerStyle-PageButtonCount="20" PagerStyle-Width="100%" PagerStyle-BackColor="#2f4f88" PagerStyle-ForeColor="white"
							BackColor="#CCCCCC" runat="server" AutoGenerateColumns="False" Width="100%" BorderColor="black"
							BorderWidth="1" GridLines="Both" CellPadding="2" CellSpacing="0" Font-Name="Verdana" Font-Size="8pt"
							HeaderStyle-BackColor="#2f4f88">
							<Columns>
								<asp:TemplateColumn FooterStyle-Width="30" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top"
									ItemStyle-VerticalAlign="Top" HeaderText="<font color='white'>Add</font>" HeaderStyle-Wrap="False">
									<FooterTemplate>
										<asp:ImageButton CommandName="Add" ImageUrl="../Images/add.gif" ID="btnAdd" Runat="server" />
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn FooterStyle-Width="30" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top"
									ItemStyle-VerticalAlign="Top" HeaderText="<font color='white'>GL</font>" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<asp:Label ID="AdjustmentID" Visible=false Text='<%# DataBinder.Eval(Container.DataItem, "ADJUSTMENT_ID") %>' Runat="Server" />
										<asp:Label ID="GLEntryID" Visible=false Text='<%# DataBinder.Eval(Container.DataItem, "GL_Entry_ID") %>' Runat="Server" />
										<asp:HyperLink id="Hyperlink1" runat="server" Text="GL" NavigateUrl='<%# "GLTransaction.aspx?GLEntryID=" + DataBinder.Eval(Container.DataItem, "GL_Entry_ID") + "&AdjustmentID=" + DataBinder.Eval(Container.DataItem, "ADJUSTMENT_ID") %>' Font-Underline="True" ForeColor="#2f4f88" Target="_blank" />
									</ItemTemplate>
									<FooterTemplate>
										&nbsp;
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="False" HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top" HeaderText="Inv ID"
									SortExpression="Invoice_ID" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# GetInvID(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "Invoice_ID"))) %>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label ID="add_InvoiceID" Text='<%# Convert.ToInt32(invIdValue) %>' Runat="Server" />
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="False" HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top" HeaderText="Acct ID"
									SortExpression="Account_ID" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# GetActID(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "Account_ID"))) %>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label ID="add_AccountID" Text='<%# Convert.ToInt32(invActValue) %>' Runat="Server" />
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="False" HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top" HeaderText="Order ID"
									SortExpression="Order_ID" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# GetOrderID(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "Order_ID"))) %>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label ID="add_OrderId" Text='<%# Convert.ToInt32(invOrderValue) %>' Runat="Server" />
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="False" HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top" HeaderText="Campaign ID"
									SortExpression="Campaign_ID" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# GetCampaignID(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "Campaign_ID"))) %>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label ID="add_CampaignID" Text='<%# Convert.ToInt32(invCampaignValue) %>' Runat="Server" />
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn FooterStyle-HorizontalAlign="Justify" FooterStyle-VerticalAlign="Middle" HeaderStyle-VerticalAlign="Top"
									ItemStyle-VerticalAlign="Top" HeaderText="Adjustment<br>Type" SortExpression="AdjustmentType" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "AdjustmentType") %>
									</ItemTemplate>
									<FooterTemplate>
										<asp:HyperLink id="lnk_AdjustmentType" runat="server" ImageUrl="../images/up.gif" />
										<asp:TextBox ID="add_AdjustmentText" ReadOnly="true" CssClass="boxlookW" Runat="Server" /><br>
										<asp:RequiredFieldValidator Runat="server" id="Requiredfieldvalidator5" ControlToValidate="add_AdjustmentText"
											ErrorMessage="Adjustment Type" Display="None" />
										<input type="hidden" id="add_AdjustmentValue" runat="server" />
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn FooterStyle-HorizontalAlign="Justify" FooterStyle-VerticalAlign="Middle" HeaderStyle-VerticalAlign="Top"
									ItemStyle-VerticalAlign="Top" HeaderText="Effective<br>Date" SortExpression="Adjustment_Effective_Date"
									HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "Adjustment_Effective_Date", "{0:dd-MMM-yyyy}") %>
									</ItemTemplate>
									<FooterTemplate>
										&nbsp;
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn FooterStyle-HorizontalAlign="Justify" FooterStyle-VerticalAlign="Middle" HeaderStyle-VerticalAlign="Top"
									ItemStyle-VerticalAlign="Top" HeaderText="Internal Comment" SortExpression="Internal_Comment" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "Internal_Comment") %>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox width="100%" TextMode="MultiLine" Rows="3" ID="add_InternalComment" MaxLength="95"
											CssClass="boxlookW" Runat="Server" />
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn FooterStyle-HorizontalAlign="Justify" FooterStyle-VerticalAlign="Middle" HeaderStyle-VerticalAlign="Top"
									ItemStyle-VerticalAlign="Top" HeaderText="Adjustment<br>Amount" SortExpression="Adjustment_Amount"
									HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "Adjustment_Amount") %>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox ID="add_AdjustmentAmount" CssClass="boxlookW" Runat="Server" />
										<!-- Matches positive and negative numbers -->
										<asp:RegularExpressionValidator Runat="server" ID="RegExp2" ControlToValidate="add_AdjustmentAmount" ValidationExpression="^-{0,1}\d*\.{0,1}\d+$"
											ErrorMessage="Please enter a valid amount" Display="None" />
										<asp:RequiredFieldValidator Runat="server" id="Requiredfieldvalidator3" ControlToValidate="add_AdjustmentAmount"
											ErrorMessage="Adjustment Amount" Display="None" />
										<asp:CompareValidator Runat="server" id="CompareValidator1" Display="none" ControlToValidate="add_AdjustmentAmount"
											ValueToCompare="0" ErrorMessage="Amount must be greater than 0" Type="Double" Operator="GreaterThan" />
									</FooterTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
				<!--PAYMENT -->
				<tr>
					<td>
						<table width="100%">
							<tr>
								<td align="left">
									<asp:Label Runat="server" ID="lblPayTitle" CssClass="ClearTextBoxB" />
								</td>
								<td align="right">
									<asp:Label Visible="False" Runat="server" ID="lblPaymentPageNumber" CssClass="ClearTextBoxDarkGrey" />
								</td>
								<td align="right">
									<asp:Label Visible="False" Runat="server" ID="lblPaymentRecords" CssClass="ClearTextBoxDarkGrey" />
								</td>
								<td align="right">
									<asp:Label Visible="False" Runat="server" ID="lblShowPayment" CssClass="ClearTextBoxDarkGrey" />
									<asp:DropDownList Visible="False" id="ddlNumInvPay" AutoPostBack="True" OnSelectedIndexChanged="Payment_PageIndexChanged"
										CssClass="boxlookW" runat="server">
										<asp:ListItem Text="10" Value="10" />
										<asp:ListItem Text="25" Value="25" />
										<asp:ListItem Text="50" Value="50" />
										<asp:ListItem Text="All" Value="All" />
									</asp:DropDownList>
								</td>
							</tr>
						</table>
						<asp:DataGrid Visible="False" OnItemDataBound="InvoiceListPaymentDG_ItemDataBound" ID="InvoiceListPaymentDG"
							OnItemCommand="doAddPayment" OnSortCommand="InvoiceListPaymentDG_Sort" OnSelectedIndexChanged="Payment_PageIndexChanged"
							AllowPaging="True" ShowFooter="True" OnPageIndexChanged="InvoiceListPaymentDG_Page" HeaderStyle-Font-Bold="True"
							AllowSorting="true" PageSize="10" PagerStyle-Mode="NumericPages" PagerStyle-HorizontalAlign="Center"
							PagerStyle-PageButtonCount="20" PagerStyle-Width="100%" PagerStyle-BackColor="#2f4f88" PagerStyle-ForeColor="white"
							BackColor="#CCCCCC" runat="server" DataKeyField="Invoice_Id" AutoGenerateColumns="False" Width="100%"
							BorderColor="black" BorderWidth="1" GridLines="Both" CellPadding="2" CellSpacing="0" Font-Name="Verdana"
							Font-Size="8pt" HeaderStyle-BackColor="#2f4f88">
							<Columns>
								<asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top"
									HeaderText="<font color='white'>Add</font>" HeaderStyle-Wrap="False">
									<FooterTemplate>
										<asp:ImageButton CommandName="Add" ImageUrl="..\Images/add.gif" ID="Imagebutton2" Runat="server" />
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn FooterStyle-Width="30" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top"
									ItemStyle-VerticalAlign="Top" HeaderText="<font color='white'>GL</font>" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<asp:Label ID="PaymentID" Visible=false Text='<%# DataBinder.Eval(Container.DataItem, "PAYMENT_ID") %>' Runat="Server" />
										<asp:HyperLink id="Hyperlink2" runat="server" Text="GL" NavigateUrl='<%# "GLPaymentTransaction.aspx?InvoiceID=" + Convert.ToInt32(InvoiceListDG.DataKeys[InvoiceListDG.SelectedIndex] is DBNull ? 0 : InvoiceListDG.DataKeys[InvoiceListDG.SelectedIndex]) + "&OrderID=" + DataBinder.Eval(Container.DataItem, "Order_ID") + "&PaymentID=" + DataBinder.Eval(Container.DataItem, "PAYMENT_ID") %>' Font-Underline="True" ForeColor="#2f4f88" Target="_blank" />
									</ItemTemplate>
									<FooterTemplate>
										&nbsp;
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="False" HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top" HeaderText="Inv ID"
									SortExpression="Invoice_ID" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# GetInvID(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "Invoice_ID"))) %>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label ID="add_PaymentInvoiceID" Text='<%# Convert.ToInt32(invIdValue) %>' Runat="Server" />
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="False" HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top" HeaderText="Acct ID"
									SortExpression="Account_ID" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# GetActID(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "Account_ID"))) %>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label ID="add_PaymentAccountID" Text='<%# Convert.ToInt32(invActValue) %>' Runat="Server" />
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="False" HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top" HeaderText="Order ID"
									SortExpression="Order_ID" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# GetOrderID(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "Order_ID"))) %>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label ID="add_PaymentOrderId" Text='<%# Convert.ToInt32(invOrderValue) %>' Runat="Server" />
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="False" HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top" HeaderText="Campaign ID"
									SortExpression="Campaign_ID" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# GetCampaignID(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "Campaign_ID"))) %>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label ID="add_PaymentCampaignID" Text='<%# Convert.ToInt32(invCampaignValue) %>' Runat="Server" />
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top" HeaderText="Payment Method"
									SortExpression="PaymentMethod" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "PaymentMethod") %>
									</ItemTemplate>
									<FooterTemplate>
										<asp:HyperLink id="lnk_PaymentMethod" runat="server" ImageUrl="../images/up.gif" /><br>
										<asp:TextBox TextMode="MultiLine" Rows="2" ID="add_PaymentMethodText" ReadOnly="true" CssClass="boxlookW"
											Runat="Server" /><br>
										<asp:RequiredFieldValidator Runat="server" id="Requiredfieldvalidator7" ControlToValidate="add_PaymentMethodText"
											ErrorMessage="Payment Method" Display="None" />
										<input type="hidden" id="add_PaymentMethodValue" runat="server" />
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top" HeaderText="Check<br>Number"
									SortExpression="Cheque_Number" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "Cheque_Number") %>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox CssClass="boxlookW" runat="server" ID="add_PaymentCheckNumber" />
										<asp:RegularExpressionValidator Runat="server" ID="RegExp3" ControlToValidate="add_PaymentCheckNumber" ValidationExpression="0*[1-9][0-9]*"
											ErrorMessage="Please enter a valid check number" Display="None" />
										<asp:
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top" HeaderText="Cheque Date"
									SortExpression="Cheque_Date" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "Cheque_Date", "{0:dd-MMM-yyyy}") %>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox CssClass="boxlookW" runat="server" ID="add_PaymentCheckDate" />
										<asp:RegularExpressionValidator Runat="server" ID="RegExp4" ControlToValidate="add_PaymentCheckDate" ValidationExpression="^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][u]l|[aA][Uu][gG]|[Ss][eE][pP]|[oO][Cc][Tt]|[Nn][oO][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$"
											ErrorMessage="Please enter check date like 01-Jan-2000" Display="None" />
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top" HeaderText="Check Payer"
									SortExpression="Cheque_Payer" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "Cheque_Payer") %>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox Width="100%" CssClass="boxlookW" runat="server" ID="add_PaymentCheckPayer" />
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top" HeaderText="Credit Card<br>Owner"
									SortExpression="Credit_Card_Owner" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "Credit_Card_Owner") %>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox CssClass="boxlookW" runat="server" Width="100%" ID="add_CreditCardOwner" />
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top" HeaderText="Credit Card<br>Auth Number"
									SortExpression="CREDIT_CARD_AUTHORIZATION" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "CREDIT_CARD_AUTHORIZATION") %>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox CssClass="boxlookW" runat="server" Width="100%" ID="add_CreditCardAuthNumber" />
										<asp:RegularExpressionValidator Runat="server" ID="RegExp5" ControlToValidate="add_CreditCardAuthNumber" ValidationExpression="^([^.][-0-9.]+[^.-])$"
											ErrorMessage="Please enter a valid auth number" Display="None" />
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top" HeaderText="Payment<br>Amount"
									SortExpression="Payment_Amount" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "Payment_Amount") %>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox CssClass="boxlookW" runat="server" Width="100%" ID="add_PaymentAmt" />
										<asp:RegularExpressionValidator Runat="server" ID="RegExp6" ControlToValidate="add_PaymentAmt" ValidationExpression="^-{0,1}\d*\.{0,1}\d+$"
											ErrorMessage="Please enter a valid payment amount" Display="None" />
										<asp:RequiredFieldValidator Runat="server" id="Requiredfieldvalidator4" ControlToValidate="add_PaymentAmt" ErrorMessage="Payment Amount"
											Display="None" />
										<asp:CompareValidator Runat="server" id="CompareValidator2" Display="none" ControlToValidate="add_PaymentAmt"
											ValueToCompare="0" ErrorMessage="Amount must be greater than 0" Type="Double" Operator="GreaterThan" />
									</FooterTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
				<tr>
					<td align="center">
						<asp:Label Runat="server" ID="lblMsg" CssClass="ClearTextBoxR" />
					</td>
				</tr>
				<!--PRODUCT -->
				<tr>
					<td>
						<table width="100%">
							<tr>
								<td align="left">
									<asp:Label Runat="server" ID="lblProductsTitle" CssClass="ClearTextBoxB" />
								</td>
								<td align="right">
									<asp:Label Visible="False" Runat="server" ID="lblProductPageNumber" CssClass="ClearTextBoxDarkGrey" />
								</td>
								<td align="right">
									<asp:Label Visible="False" Runat="server" ID="lblProduct" CssClass="ClearTextBoxG" />
								</td>
								<td align="right">
									<asp:Label Visible="False" Runat="server" ID="lblShowProduct" CssClass="ClearTextBoxB" />
									<asp:DropDownList Visible="False" id="ddlNumInvProduct" AutoPostBack="True" OnSelectedIndexChanged="Product_PageIndexChanged"
										CssClass="boxlookW" runat="server">
										<asp:ListItem Text="10" Value="10" />
										<asp:ListItem Text="25" Value="25" />
										<asp:ListItem Text="50" Value="50" />
										<asp:ListItem Text="All" Value="All" />
									</asp:DropDownList>
								</td>
							</tr>
						</table>
						<asp:DataGrid Visible="False" ID="ProductDetailsDG" AllowPaging="True" HeaderStyle-Font-Bold="True"
							AllowSorting="true" OnSortCommand="ProductDetailsDG_Sort" PageSize="10" PagerStyle-Mode="NumericPages"
							PagerStyle-HorizontalAlign="Center" PagerStyle-PageButtonCount="20" PagerStyle-Width="100%"
							PagerStyle-BackColor="#2f4f88" PagerStyle-ForeColor="white" OnPageIndexChanged="ProductDetailsDG_Page"
							BackColor="#CCCCCC" runat="server" DataKeyField="ProductCode" AutoGenerateColumns="False"
							Width="100%" BorderColor="black" BorderWidth="1" GridLines="Both" CellPadding="2" CellSpacing="0"
							Font-Name="Verdana" Font-Size="8pt" HeaderStyle-BackColor="#2f4f88">
							<Columns>
								<asp:TemplateColumn HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top" HeaderText="Product Name"
									SortExpression="ProductName" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "ProductName") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top" HeaderText="Title Code"
									SortExpression="ProductCode" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "ProductCode") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-VerticalAlign="Top"
									ItemStyle-VerticalAlign="Top" HeaderText="Qty Ordered" SortExpression="QtyOrdered" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "QTYOrdered") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-VerticalAlign="Top"
									ItemStyle-VerticalAlign="Top" HeaderText="Item Price" SortExpression="Price" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "Price", "{0:c}") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-VerticalAlign="Top"
									ItemStyle-VerticalAlign="Top" HeaderText="Total Price" SortExpression="TotalPrice" HeaderStyle-Wrap="False">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "TotalPrice", "{0:c}") %>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
			</table>
		</form>
		<!-- #Include File="../Includes/Footer.inc" -->
	</body>
</HTML>
