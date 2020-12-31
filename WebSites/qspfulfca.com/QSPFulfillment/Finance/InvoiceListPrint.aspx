<%@ Register TagPrefix="uc1" TagName="FiscalYearSelectControl" Src="../Common/FiscalYearSelectControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment"  %>
<%@ Page language="c#" Codebehind="InvoiceListPrint.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.Finance.InvoiceListPrint" %>
<HTML>
	<HEAD>
		<title>CA Fulfill System - Invoice List Print</title>
		<link href="../Includes/MagSysStyle.css" type="text/css" rel="stylesheet">
			<script language="javascript">
			function DoCheckboxes (chkVal, idVal) 
			{ 
				var form = document.forms[0];
				// Loop through all elements
				for (i=0; i<form.length; i++) 
				{
					// check header checkbox first
					if (idVal.indexOf ('CheckAll') != -1) 
					{
						// Check if main checkbox is checked, then select or deselect datagrid checkboxes 
						if(chkVal == true) 
						{
							form.elements[i].checked = true;
						} 
						else 
						{
							form.elements[i].checked = false;
						}
					// now check the item template's multiple checkboxes
					} 
					else if (idVal.indexOf ('PrintThis') != -1) 
					{
						// Check if any of the checkboxes are not checked, and then uncheck top select all checkbox
						if(form.elements[i].checked == false) 
						{
							form.elements[1].checked = false; //Uncheck main select all checkbox
						}
					}
				}//end for
			}//end function
			
			function confirmPrint(form, verifyChecked) 
			{ 	// loop through all elements
				for (i=0; i<form.length; i++) 
				{
					// Look for our checkboxes only
					if (!verifyChecked || form.elements[i].name.indexOf("PrintThis") !=-1) 
					{
						// If any are checked then confirm alert
						if(!verifyChecked || form.elements[i].checked) 
						{
							return confirm ('Are you sure you want to mark your selection(s) as printed?')
						}
					}
				}//for
			}//end function

			</script>
	</HEAD>
	<body leftmargin="0" topmargin="0">
		<form id="InvoiceForm" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" --><br>
			<center>
				<h3><font face="Verdana" color="#2f4f88">Print Invoices</font></h3>
			</center>
			<p></p>
			<table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
				<tr>
					<td align="center"><b><font face="Verdana" color="#2f4f88" size="2">Search</font></b>&nbsp;
						<asp:textbox id="Search" runat="server" cssclass="boxlook"></asp:textbox>&nbsp;<b><font face="Verdana" color="#2f4f88" size="2">By</font></b>&nbsp;
						<asp:dropdownlist id="ddlStatus" runat="server" cssclass="boxlookW">
							<asp:ListItem Value="Name">Acct Name</asp:ListItem>
							<asp:ListItem Value="InvoiceID">Invoice ID</asp:ListItem>
							<asp:ListItem Value="AccountID">Account ID</asp:ListItem>
							<asp:ListItem Value="CampaignID">Campaign ID</asp:ListItem>
						</asp:dropdownlist>
                        <asp:Button ID="btnSearchGo" runat="server" class="boxlook" Text="Go" 
                            onclick="btnSearchGo_Click" />
						</td>
					<td>
					    <font face="Verdana" size="2" color="#2f4f88">Fiscal&nbsp;Year:</font>&nbsp;
                        <asp:dropdownlist id="ddlFiscalYear" runat="server" cssclass="boxlookW" onselectedindexchanged="changeFY" autopostback="True"></asp:dropdownlist>
                    </td>
					<td id="tdShowInvoiceType" runat="server">
					    <font face="Verdana" size="2" color="#2f4f88">Show invoices:</font>&nbsp;
                        <asp:dropdownlist id="ddlShowInvoiceType" runat="server" cssclass="boxlookW" onselectedindexchanged="changeShowInvoiceType" autopostback="True">
							<asp:listitem text="All" value="All" />
							<asp:listitem text="In owing only" value="Owing" />
                        </asp:dropdownlist>
                    </td>
					<td><b><asp:label id="lblPrinted" runat="server"><font face="Verdana" color="#2f4f88" size="2">Printed:</font></asp:label></b>
						<asp:dropdownlist id="ddlPrinted" runat="server" cssclass="boxlookW" onselectedindexchanged="changePrinted"
							autopostback="True">
							<asp:ListItem Selected="True">All</asp:ListItem>
							<asp:listitem text="No" value="N" />
							<asp:listitem text="Yes" value="Y" />
						</asp:dropdownlist></td>
                     <td>
                        <b><asp:label id="lblOEFUReport" runat="server"><font face="Verdana" color="#2f4f88" size="2">Include OEFU Report:</font></asp:label></b>
					         <asp:CheckBox ID="chkIncludeOEFUReport" runat="server" />	
                    </td>
                     <td>
                        <b><asp:label id="lblShowNonPrinted" runat="server"><font face="Verdana" color="#2f4f88" size="2">Show Nonprinted:</font></asp:label></b>
					         <asp:CheckBox ID="chkShowNonPrinted" runat="server" />	
                    </td>
					<td><asp:label id="lblInvoice" runat="server" cssclass="ClearTextBoxG"></asp:label></td>
				</tr>
			</table>
			<table cellspacing="0" cellpadding="2" width="100%" align="center" border="0">
				<tr>
					<td><asp:datagrid 
					    id="InvoiceListDG" 
					    runat="server" 
					    width="100%" 
					    headerstyle-backcolor="#2f4f88" 
					    font-size="8pt"
					    font-name="Verdana" 
					    cellpadding="2" 
					    borderwidth="1px" 
					    bordercolor="Black"
					    autogeneratecolumns="False" 
					    datakeyfield="Invoice_Id" 
					    backcolor="#CCCCCC" 
					    pagerstyle-forecolor="white" 
					    pagerstyle-backcolor="#2f4f88" 
					    pagerstyle-width="100%" 
					    pagerstyle-pagebuttoncount="20"
					    pagerstyle-horizontalalign="Center" 
					    pagerstyle-mode="NumericPages" 
					    pagerstyle-position="Bottom" 
					    allowpaging="True" 
					    onsortcommand="InvoiceListDG_Sort" 
					    allowsorting="True" 
					    headerstyle-font-bold="True"
					    Font-Names="Verdana" 
					    onpageindexchanged="InvoiceListDG_Page"
					    onitemdatabound="InvoiceListDG_ItemDataBound" 
                        ondeletecommand="InvoiceListDG_DeleteCommand">
<PagerStyle Mode="NumericPages" PageButtonCount="20" HorizontalAlign="Center" BackColor="#2F4F88" ForeColor="White" Width="100%"></PagerStyle>
							<columns>
								<asp:templatecolumn headerstyle-verticalalign="Top" itemstyle-verticalalign="Top">
									<headertemplate>
										<asp:checkbox id="CheckAll" onclick="javascript: return DoCheckboxes (this.checked, this.id);"
											runat="server" />
									</headertemplate>
									<itemtemplate>
										<asp:checkbox id="PrintThis" onclick="javascript: return DoCheckboxes (this.checked, this.id);"
											runat="server" />
									</itemtemplate>

<HeaderStyle VerticalAlign="Top"></HeaderStyle>

<ItemStyle VerticalAlign="Top"></ItemStyle>
								</asp:templatecolumn>
								<asp:ButtonColumn CommandName="Delete" DataTextField="Invoice_ID" 
                                    SortExpression="Invoice_ID" HeaderText="Invoice ID"></asp:ButtonColumn>
								<asp:templatecolumn headerstyle-verticalalign="Top" 
                                    itemstyle-verticalalign="Top" headertext="Invoice ID"
									sortexpression="Invoice_ID" headerstyle-wrap="False" Visible="False">
									<itemtemplate>
										<asp:Literal ID="InvoiceID" Visible=false Text='<%# DataBinder.Eval(Container.DataItem, "Invoice_ID") %>' Runat="Server" />
										<!--
										<asp:hyperlink id="lnk_PrintInvoice" runat="server" font-underline="True" forecolor="RoyalBlue"
											target="_blank" />
										-->
										<cc2:rsgenerationlinkbutton id="rsGenerationPrintInvoice" runat="server" CausesValidation="false" font-underline="True"
											forecolor="RoyalBlue"></cc2:rsgenerationlinkbutton>
									</itemtemplate>

<HeaderStyle VerticalAlign="Top" Wrap="False"></HeaderStyle>

<ItemStyle VerticalAlign="Top"></ItemStyle>
								</asp:templatecolumn>
								<asp:templatecolumn headerstyle-verticalalign="Top" itemstyle-verticalalign="Top" headertext="Type"
									sortexpression="Type" headerstyle-wrap="False">
									<itemtemplate>
										<asp:Literal ID="Type" Visible=true Text='<%# DataBinder.Eval(Container.DataItem, "Type") %>' Runat="Server" />
									</itemtemplate>

<HeaderStyle VerticalAlign="Top" Wrap="False"></HeaderStyle>

<ItemStyle VerticalAlign="Top"></ItemStyle>
								</asp:templatecolumn>
								<asp:templatecolumn headerstyle-verticalalign="Top" itemstyle-verticalalign="Top" headertext="Language"
									sortexpression="Lang" headerstyle-wrap="False">
									<itemtemplate>
										<asp:Literal ID="Lang" Visible=true Text='<%# DataBinder.Eval(Container.DataItem, "Lang") %>' Runat="Server" />
									</itemtemplate>

<HeaderStyle VerticalAlign="Top" Wrap="False"></HeaderStyle>

<ItemStyle VerticalAlign="Top"></ItemStyle>
								</asp:templatecolumn>
								<asp:templatecolumn headerstyle-verticalalign="Top" itemstyle-verticalalign="Top" headertext="Order ID"
									sortexpression="Order_ID" headerstyle-wrap="False">
									<itemtemplate>
										<asp:label id="lblOrderID" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "Order_ID") %>'>
										</asp:label>
									</itemtemplate>

<HeaderStyle VerticalAlign="Top" Wrap="False"></HeaderStyle>

<ItemStyle VerticalAlign="Top"></ItemStyle>
								</asp:templatecolumn>
								<asp:templatecolumn headerstyle-verticalalign="Top" itemstyle-verticalalign="Top" headertext="Acct ID"
									sortexpression="Account_ID" headerstyle-wrap="False">
									<itemtemplate>
										<asp:label id="lblAccountID" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "Account_ID") %>'>
										</asp:label>
									</itemtemplate>

<HeaderStyle VerticalAlign="Top" Wrap="False"></HeaderStyle>

<ItemStyle VerticalAlign="Top"></ItemStyle>
								</asp:templatecolumn>
								<asp:templatecolumn headerstyle-verticalalign="Top" itemstyle-verticalalign="Top" headertext="Account Type"
									sortexpression="AccountType" headerstyle-wrap="False">
									<itemtemplate>
										<asp:label id="lblAccountType" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "AccountType") %>'>
										</asp:label>
									</itemtemplate>

<HeaderStyle VerticalAlign="Top" Wrap="False"></HeaderStyle>

<ItemStyle VerticalAlign="Top"></ItemStyle>
								</asp:templatecolumn>
								<asp:templatecolumn headerstyle-verticalalign="Top" itemstyle-verticalalign="Top" headertext="Account Name"
									sortexpression="Group_Name" headerstyle-wrap="False">
									<itemtemplate>
										<asp:label id="lblGroupName" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "Group_Name") %>'>
										</asp:label>
									</itemtemplate>

<HeaderStyle VerticalAlign="Top" Wrap="False"></HeaderStyle>

<ItemStyle VerticalAlign="Top"></ItemStyle>
								</asp:templatecolumn>
								<asp:templatecolumn visible="False">
									<itemtemplate>
										<asp:label id="lblCampaignID" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "CampaignID") %>'>
										</asp:label>
									</itemtemplate>
								</asp:templatecolumn>
								<asp:templatecolumn headerstyle-verticalalign="Top" itemstyle-horizontalalign="Right" itemstyle-verticalalign="Top"
									headertext="Invoice Date" sortexpression="Invoice_Date" headerstyle-wrap="False">
									<itemtemplate>
										<asp:label id="lblInvoiceDate" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "Invoice_Date", "{0:dd-MMM-yyyy}") %>'>
										</asp:label>
									</itemtemplate>

<HeaderStyle VerticalAlign="Top" Wrap="False"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
								</asp:templatecolumn>
								<asp:templatecolumn headerstyle-verticalalign="Top" itemstyle-horizontalalign="Right" itemstyle-verticalalign="Top"
									headertext="Invoice Due" sortexpression="Invoice_Due_Date" headerstyle-wrap="False">
									<itemtemplate>
										<asp:label id="lblInvoiceDueDate" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "Invoice_Due_Date", "{0:dd-MMM-yyyy}") %>'>
										</asp:label>
									</itemtemplate>

<HeaderStyle VerticalAlign="Top" Wrap="False"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
								</asp:templatecolumn>
								<asp:templatecolumn headerstyle-verticalalign="Top" itemstyle-horizontalalign="Right" itemstyle-verticalalign="Top"
									headertext="Invoice Amount" sortexpression="Invoice_Amount" headerstyle-wrap="False">
									<itemtemplate>
										<asp:label id="lblInvoiceAmount" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "Invoice_Amount", "{0:c}") %>'>
										</asp:label>
									</itemtemplate>

<HeaderStyle VerticalAlign="Top" Wrap="False"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
								</asp:templatecolumn>
								<asp:templatecolumn headerstyle-verticalalign="Top" itemstyle-horizontalalign="Right" itemstyle-verticalalign="Top"
									headertext="Printed?" sortexpression="Is_Printed" headerstyle-wrap="False">
									<itemtemplate>
										<asp:label id="lblIsPrinted" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "Is_Printed") %>'>
										</asp:label>
									</itemtemplate>

<HeaderStyle VerticalAlign="Top" Wrap="False"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
								</asp:templatecolumn>
								<asp:templatecolumn visible="False">
									<itemtemplate>
										<asp:label id="lblApprovedDate" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "DateTime_Approved") %>'>
										</asp:label>
									</itemtemplate>
								</asp:templatecolumn>
                        
                        <asp:templatecolumn headerstyle-verticalalign="Top" itemstyle-horizontalalign="Right" itemstyle-verticalalign="Top"
									headertext="Campaign Balance" sortexpression="CampaignBalance" headerstyle-wrap="False">
									<itemtemplate>
										<asp:label id="lblCampaignBalance" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "CampaignBalance", "{0:c}") %>'>
										</asp:label>
									</itemtemplate>

<HeaderStyle VerticalAlign="Top" Wrap="False"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
								</asp:templatecolumn>
							</columns>

<HeaderStyle BackColor="#2F4F88" Font-Bold="True"></HeaderStyle>
						</asp:datagrid></td>
				</tr>
			</table>
			<br>
			<div style="TEXT-ALIGN: center">
				<table height="59" cellspacing="6" width="375" id="tblPrintButtons" runat="server">
					<tr>
						<td width="33%"><asp:button id="btnPrintChecked" runat="server" cssclass="boxlook" 
                                width="100%" text="Print checked"
								causesvalidation="False"></asp:button></td>
						<td id="tdPrintItems" runat="server" width="33%"><asp:button id="PrintItems" runat="server" cssclass="boxlook" width="100%" text="Mark checked as Printed"
								causesvalidation="False"></asp:button></td>
						<td id="tdUnprintItems" runat="server" width="34%"><asp:button id="UnprintItems" runat="server" cssclass="boxlook" width="100%" text="Mark checked as Unprinted"
								causesvalidation="False"></asp:button></td>
					</tr>
					<tr>
						<td><input class="boxlook" id="btnPrintList" style="WIDTH: 100%" type="button" value="Print whole list"
								runat="server">
						</td>
						<td id="tdPrintList" runat="server" width="33%"><asp:button id="btnMarkWholeListPrinted" runat="server" cssclass="boxlook" width="100%" text="Mark whole list as Printed"
								causesvalidation="False"></asp:button></td>
						<td id="tdUnprintList" runat="server" width="34%"><asp:button id="btnMarkWholeListUnprinted" runat="server" cssclass="boxlook" width="100%" text="Mark whole list as Unprinted"
								causesvalidation="False"></asp:button></td>
					</tr>
				</table>
				<br>
				<br>
				<asp:label id="LabelMsg" runat="server" cssclass="ClearTextBoxR"></asp:label></div>
		</form>
		<!-- #Include File="../Includes/Footer.inc" -->
	</body>
</HTML>
