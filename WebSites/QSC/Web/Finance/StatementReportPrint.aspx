<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment"  %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Page language="c#" Codebehind="StatementReportPrint.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.Finance.StatementReportPrint" %>
<html>
	<head>
		<title>CA Fulfill System - Statement Report Print</title>
		<link rel="stylesheet" href="../Includes/MagSysStyle.css" type="text/css">
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
			
			function confirmPrint(form) 
			{ 	// loop through all elements
				for (i=0; i<form.length; i++) 
				{
					// Look for our checkboxes only
					if (form.elements[i].name.indexOf("PrintThis") !=-1) 
					{
						// If any are checked then confirm alert
						if(form.elements[i].checked) 
						{
							return confirm ('Are you sure you want to print your selection(s)?')
						}
					}
				}//for
			}//end function

			</script>
	</head>
	<body topmargin="0" leftmargin="0">
		<form method="post" runat="server" id="StatementForm">
			<!-- #include file="../Includes/Menu.inc" -->
			<br>
			<center><h3><font face="Verdana" color="#2f4f88">Print Statement Reports</font></h3>
			</center>
			<p></p>
			<table border="0" cellspacing="0" cellpadding="0" width="100%">
				<tr>
					<td align="right"><b><font face="Verdana" size="2" color="#2f4f88">Select Printer</font></b>
						&nbsp;<asp:DropDownList AutoPostBack=True CssClass="boxlookW" runat="server" ID="ddlPrinters" 
									DataSource='<%# GetPrinterList() %>' 
									DataTextField="PrinterName"
									DataValueField="PrinterName"/>
					</td>
					<td>&nbsp;</td>
				</tr>
			</table>
			<br>
			<table border="0" cellspacing="0" cellpadding="0" width="100%" align="center">
				<tr>
					<td align="center">
						<b><font face="Verdana" size="2" color="#2f4f88">Search</font></b>&nbsp;
						<asp:textbox id="Search" runat="server" cssclass="boxlook" />
						&nbsp;<b><font face="Verdana" size="2" color="#2f4f88">By</font></b>&nbsp;
						<asp:dropdownlist id="ddlStatus" cssclass="boxlookW" runat="server">
							<asp:listitem text="Acct Name" value="AcctName" />
							<asp:listitem text="Acct ID" value="AcctID" />
							<asp:listitem text="FM ID" value="FMID" />
							<asp:listitem text="Last Name" value="LastName" />
						</asp:dropdownlist>
					</td>
					<td>
						<b><font face="Verdana" size="2" color="#2f4f88">Start:</font></b>&nbsp;
						<asp:textbox width="90" maxlength="11" id="FromDate" runat="server" cssclass="boxlook" />&nbsp;
						<asp:requiredfieldvalidator runat="server" id="Req8" controltovalidate="FromDate" errormessage="From Date" display="Dynamic" />
						<asp:regularexpressionvalidator runat="server" id="RegExp8" controltovalidate="FromDate" validationexpression="^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][u]l|[aA][Uu][gG]|[Ss][eE][pP]|[oO][Cc]|[Nn][oO][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$"
							errormessage="<br>Please enter start date like 01-Jan-2000" display="Dynamic" />
						<b><font face="Verdana" size="2" color="#2f4f88">End:</font></b>&nbsp;
						<asp:textbox width="90" maxlength="11" id="ToDate" runat="server" cssclass="boxlook" />&nbsp;
						<asp:requiredfieldvalidator runat="server" id="Req9" controltovalidate="ToDate" errormessage="To Date" display="Dynamic" />
						<asp:regularexpressionvalidator runat="server" id="RegExp9" controltovalidate="ToDate" validationexpression="^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][u]l|[aA][Uu][gG]|[Ss][eE][pP]|[oO][Cc]|[Nn][oO][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$"
							errormessage="<br>Please enter end date like 01-Jan-2000" display="Dynamic" />
						&nbsp;&nbsp;<asp:linkbutton causesvalidation="False" id="BtnSearch" runat="server" cssclass="boxlook" onclick="SearchButtonClick"
							text="<font face='Verdana' color='#2f4f88'> Go </font>" />
					</td>
					<td>
						<asp:label runat="server" id="lblStatement" cssclass="ClearTextBoxG" />
					</td>
				</tr>
			</table>
			<table border="0" cellspacing="0" cellpadding="2" width="100%" align="center">
				<tr>
					<td>
						<asp:datagrid id="StatementReportDG" onitemdatabound="StatementReportDG_ItemDataBound" headerstyle-font-bold="True"
							allowsorting="true" onsortcommand="StatementReportDG_Sort" allowpaging="True" pagesize="10"
							pagerstyle-position="Bottom" pagerstyle-mode="NumericPages" pagerstyle-horizontalalign="Center"
							pagerstyle-pagebuttoncount="20" pagerstyle-width="100%" pagerstyle-backcolor="#2f4f88" pagerstyle-forecolor="white"
							onpageindexchanged="StatementReportDG_Page" backcolor="#CCCCCC" runat="server" datakeyfield="AccountId"
							autogeneratecolumns="False" width="100%" bordercolor="black" borderwidth="1" gridlines="Both"
							cellpadding="2" cellspacing="0" font-name="Verdana" font-size="8pt" headerstyle-backcolor="#2f4f88">
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
								</asp:templatecolumn>
								<asp:templatecolumn headerstyle-verticalalign="Top" itemstyle-verticalalign="Top" headertext="Account ID"
									sortexpression="AccountID" headerstyle-wrap="False">
									<itemtemplate>
										<asp:Label ID="AccountID" Visible=false Text='<%# DataBinder.Eval(Container.DataItem, "AccountID") %>' Runat="Server" />
										<asp:Label ID="Lang" Visible=false Text='<%# DataBinder.Eval(Container.DataItem, "Lang") %>' Runat="Server" />
										<cc2:rsgenerationlinkbutton id="rsGenerationStatementReport" runat="server" causesvalidation="false" font-underline="True"
											forecolor="RoyalBlue" />
									</itemtemplate>
								</asp:templatecolumn>
								<asp:templatecolumn headerstyle-verticalalign="Top" itemstyle-verticalalign="Top" headertext="Account Name"
									sortexpression="Name" headerstyle-wrap="False">
									<itemtemplate>
										<%# DataBinder.Eval(Container.DataItem, "Name") %>
									</itemtemplate>
								</asp:templatecolumn>
								<asp:templatecolumn headerstyle-verticalalign="Top" itemstyle-verticalalign="Top" headertext="FMID"
									sortexpression="FMID" headerstyle-wrap="False">
									<itemtemplate>
										<%# DataBinder.Eval(Container.DataItem, "FMID") %>
									</itemtemplate>
								</asp:templatecolumn>
								<asp:templatecolumn headerstyle-verticalalign="Top" itemstyle-verticalalign="Top" headertext="FM Name"
									sortexpression="LastName" headerstyle-wrap="False">
									<itemtemplate>
										<%# DataBinder.Eval(Container.DataItem, "LastName") %>
									</itemtemplate>
								</asp:templatecolumn>
							</columns>
						</asp:datagrid>
					</td>
				</tr>
			</table>
			<br>
			<center><asp:button cssclass="boxlook" causesvalidation="False" text="Print Items" onclick="DoPrint"
					id="PrintItems" runat="server" />
				<br>
				<br>
				<asp:label runat="server" id="LabelMsg" cssclass="ClearTextBoxR" />
			</center>
		</form>
		<!-- #Include File="../Includes/Footer.inc" -->
	</body>
</html>
