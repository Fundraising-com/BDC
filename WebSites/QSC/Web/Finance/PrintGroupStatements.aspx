<%@ Page language="c#" Codebehind="PrintGroupStatements.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.Finance.PrintGroupStatements" %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment"  %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<HTML>
	<HEAD>
		<title>CA Fulfill System - Campaign Statement Report Print</title>
		<LINK href="../Includes/MagSysStyle.css" type="text/css" rel="stylesheet">
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
	</HEAD>
	<body leftMargin="0" topMargin="0">
		<form id="StatementForm" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" --><br>
			<center>
				<h3><font face="Verdana" color="#2f4f88">Print&nbsp;Group Statements</font></h3>
			</center>
			<p></p>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td align="right">&nbsp;&nbsp;<asp:dropdownlist id=ddlPrinters runat="server" AutoPostBack="True" CssClass="boxlookW" DataSource="<%# GetPrinterList() %>" DataTextField="PrinterName" DataValueField="PrinterName" Visible="False"></asp:dropdownlist></td>
					<td>&nbsp;</td>
				</tr>
			</table>
			<br>
			<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
				<tr>
					<td align="center" width="514"><b><font face="Verdana" color="#2f4f88" size="2">Search <B><FONT face="Verdana" color="#2f4f88" size="2">
										By</FONT></B>&nbsp;&nbsp;</font></b>&nbsp;
						<asp:dropdownlist id="ddlStatus" runat="server" AutoPostBack="True" cssclass="boxlookW"></asp:dropdownlist><asp:textbox id="Search" runat="server" cssclass="boxlook"></asp:textbox><asp:dropdownlist id="ddlFM" runat="server" CssClass="boxlookW" Width="183px"></asp:dropdownlist>&nbsp;</td>
					<td width="319"><b><font face="Verdana" color="#2f4f88" size="2">Start:</font></b>&nbsp;
						<asp:textbox id="FromDate" runat="server" cssclass="boxlook" width="82px" maxlength="11"></asp:textbox>&nbsp;
						<asp:requiredfieldvalidator id="Req8" runat="server" controltovalidate="FromDate" errormessage="From Date" display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="RegExp8" runat="server" controltovalidate="FromDate" errormessage="<br>Please enter start date like 01-Jan-2000"
							display="Dynamic" validationexpression="^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][u]l|[aA][Uu][gG]|[Ss][eE][pP]|[oO][Cc]|[Nn][oO][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$"></asp:regularexpressionvalidator><b><font face="Verdana" color="#2f4f88" size="2">End:</font></b>&nbsp;
						<asp:textbox id="ToDate" runat="server" cssclass="boxlook" width="82px" maxlength="11"></asp:textbox>&nbsp;
						<asp:requiredfieldvalidator id="Req9" runat="server" controltovalidate="ToDate" errormessage="To Date" display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="RegExp9" runat="server" controltovalidate="ToDate" errormessage="<br>Please enter end date like 01-Jan-2000"
							display="Dynamic" validationexpression="^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][u]l|[aA][Uu][gG]|[Ss][eE][pP]|[oO][Cc]|[Nn][oO][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$"></asp:regularexpressionvalidator>&nbsp;&nbsp;<asp:linkbutton id="BtnSearch" onclick="SearchButtonClick" runat="server" cssclass="boxlook" causesvalidation="False"
							text="<font face='Verdana' color='#2f4f88'> Go </font>"></asp:linkbutton>
					</td>
					<td><asp:label id="lblStatement" runat="server" cssclass="ClearTextBoxG"></asp:label></td>
				</tr>
			</table>
			<table cellSpacing="0" cellPadding="2" width="100%" align="center" border="0">
				<tr>
					<td><asp:datagrid id="CampaignStatementReportDG" runat="server" width="100%" onitemdatabound="CampaignStatementReportDG_ItemDataBound"
							headerstyle-font-bold="True" allowsorting="true" onsortcommand="CampaignStatementReportDG_Sort"
							allowpaging="True" pagesize="10" pagerstyle-position="Bottom" pagerstyle-mode="NumericPages"
							pagerstyle-horizontalalign="Center" pagerstyle-pagebuttoncount="20" pagerstyle-width="100%"
							pagerstyle-backcolor="#2f4f88" pagerstyle-forecolor="white" onpageindexchanged="CampaignStatementReportDG_Page"
							backcolor="#CCCCCC" datakeyfield="AccountId" autogeneratecolumns="False" bordercolor="black"
							borderwidth="1" gridlines="Both" cellpadding="2" cellspacing="0" font-name="Verdana" font-size="8pt"
							headerstyle-backcolor="#2f4f88">
							<HeaderStyle Font-Bold="True" BackColor="#2F4F88"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn>
									<HeaderStyle VerticalAlign="Top"></HeaderStyle>
									<ItemStyle VerticalAlign="Top"></ItemStyle>
									<HeaderTemplate>
										<asp:checkbox id="CheckAll" onclick="javascript: return DoCheckboxes (this.checked, this.id);"
											runat="server" AutoPostBack="True"></asp:checkbox>
									</HeaderTemplate>
									<ItemTemplate>
										<asp:checkbox id="PrintThis" onclick="javascript: return DoCheckboxes (this.checked, this.id);"
											runat="server" AutoPostBack="True"></asp:checkbox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="AccountID" HeaderText="Account ID">
									<HeaderStyle Wrap="False" VerticalAlign="Top"></HeaderStyle>
									<ItemStyle VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label ID="AccountID" Visible=false Text='<%# DataBinder.Eval(Container.DataItem, "AccountID") %>' Runat="Server" />
										<asp:Label ID="lblCampaignID" Visible=false Text='<%# DataBinder.Eval(Container.DataItem, "CampaignID") %>' Runat="Server" />
										<asp:Label ID="Lang" Visible=false Text='<%# DataBinder.Eval(Container.DataItem, "Lang") %>' Runat="Server" />
										<cc2:rsgenerationlinkbutton id="rsGenerationStatementReportByCampaign" runat="server" causesvalidation="false"
											font-underline="True" forecolor="RoyalBlue" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="CampaignID" HeaderText="Campaign ID">
									<HeaderStyle Wrap="False" VerticalAlign="Top"></HeaderStyle>
									<ItemStyle VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<asp:Label ID="CampaignID" Visible=false Text='<%# DataBinder.Eval(Container.DataItem, "CampaignID") %>' Runat="Server" />
										<%# DataBinder.Eval(Container.DataItem, "CampaignID") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Name" HeaderText="Account Name">
									<HeaderStyle Wrap="False" VerticalAlign="Top"></HeaderStyle>
									<ItemStyle VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "Name") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="FMID" HeaderText="FMID">
									<HeaderStyle Wrap="False" VerticalAlign="Top"></HeaderStyle>
									<ItemStyle VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "FMID") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="LastName" HeaderText="FM Name">
									<HeaderStyle Wrap="False" VerticalAlign="Top"></HeaderStyle>
									<ItemStyle VerticalAlign="Top"></ItemStyle>
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "LastName") %>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle Width="100%" HorizontalAlign="Center" ForeColor="White" BackColor="#2F4F88" PageButtonCount="20"
								Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
			<br>
			<center><asp:button id="PrintItems" onclick="DoPrint" runat="server" cssclass="boxlook" causesvalidation="False"
					text="Print Checked Items"></asp:button><br>
				<br>
				<asp:label id="LabelMsg" runat="server" cssclass="ClearTextBoxR"></asp:label></center>
		</form>
		<!-- #Include File="../Includes/Footer.inc" -->
	</body>
</HTML>
