<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchCampaignByAccount.aspx.cs" Inherits="QSPFulfillment.AcctMgt.SearchCampaignByAccount" %>

<%@ Register Assembly="QSPFulfillment" Namespace="QSPFulfillment.CustomerService"
    TagPrefix="cc2" %>

<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register src="Control/CampaignListControl.ascx" tagname="CampaignListControl" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div id="divSearch" name="divSearch">
	<table id="Table3" cellspacing="0" cellpadding="0" width="100%" bgcolor="#cecece" border="0">
		<tr>
			<td>
				<table class="CSTable" id="Table4" cellspacing="1" cellpadding="2" width="100%">
					<tr>
						<td valign="top" height="20"><asp:label id="lblTitle2" runat="server" cssclass="CSTitle">Search</asp:label></td>
					</tr>
					<tr bgcolor="#ffffff">
						<td valign="top">
							<table id="Table1" cellspacing="0" cellpadding="2" width="100%" border="0">
								<tr>
									<td style="WIDTH: 33%" valign="bottom">
										<asp:label id="Label5" runat="server" cssclass="CSPlainText">Account&nbsp;ID</asp:label><br>
										<cc1:textboxinteger id="tbxGroupID" runat="server" maxlength="25" errormsgregexp="The field Account ID has to be a number."></cc1:textboxinteger>
									</td>
									<td style="WIDTH: 33%" valign="bottom"><asp:label id="Label2" runat="server" cssclass="CSPlainText">Account&nbsp;Name</asp:label><br>
										<cc1:textbox id="tbxGroupName" runat="server" maxlength="50"></cc1:textbox></td>
									<td style="WIDTH: 34%" valign="bottom">
										<br>
										<table id="Table2" cellspacing="0" cellpadding="0" border="0">
											<tr>
												<td><asp:button id="btnSearch" runat="server" cssclass="boxlook" text="Search" 
                                                        Width="64px" onclick="btnSearch_Click" ></asp:button>&nbsp;&nbsp;&nbsp;
												</td>
												<td align="center">
                                                    <asp:Button ID="btnReset" runat="server" Text="Reset" Width="64px" 
                                                        CausesValidation="False" onclick="btnReset_Click"/>
												</td>
												
												

												
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td style="WIDTH: 33%" valign="bottom">
										<br>
									</td>
									<td style="WIDTH: 33%" valign="bottom">
										<br>
									</td>
									<td style="WIDTH: 33%" valign="bottom">
										<br>
									</td>
								</tr>
								
							</table>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</div>
<div style="PADDING-RIGHT: 1px;PADDING-LEFT: 1px;PADDING-BOTTOM: 1px;PADDING-TOP: 1px">
	<asp:datagrid id="dtgMain" runat="server" autogeneratecolumns="False" 
        borderwidth="1px" borderstyle="None"
		bordercolor="#CCCCCC" backcolor="White" cellpadding="3" width="100%" 
        searchmode="0" allowpaging="True">
		<footerstyle forecolor="#000066" cssclass="CSSearchResult" backcolor="White"></footerstyle>
		<selecteditemstyle font-bold="True" forecolor="White" backcolor="#008A8C"></selecteditemstyle>
		<itemstyle forecolor="#000066" cssclass="CSSearchResult"></itemstyle>
		<headerstyle font-bold="True" forecolor="White" cssclass="CSSearchResult" backcolor="#006699"></headerstyle>
		<columns>
			<asp:templatecolumn headertext="Campaign ID">
				<itemtemplate>
					<asp:label id="lblCampaignID" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.CampaignID") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="FM ID" visible="False">
				<itemtemplate>
					<asp:label id="lblFMID" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.FMID") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="FM">
				<itemtemplate>
					<asp:label id="lblFMName" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.FMName") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="Start">
				<itemtemplate>
					<asp:label id="lblStartDate" runat="server" text='<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.StartDate")) != new DateTime(1995, 01, 01) ? Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.StartDate")).ToString("MM/dd/yyyy") : "" %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="End">
				<itemtemplate>
					<asp:label id="lblEndDate" runat="server" text='<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.EndDate")) != new DateTime(1995, 01, 01) ? Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.EndDate")).ToString("MM/dd/yyyy") : "" %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn visible="False">
				<itemtemplate>
					<asp:label id="lblStatus" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Status") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>
			<asp:templatecolumn headertext="Status">
				<itemtemplate>
					<asp:label id="lblStatusName" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.StatusName").ToString().Replace("campaign status - ", "") %>'>
					</asp:label>
				</itemtemplate>
			</asp:templatecolumn>

		
			<asp:templatecolumn>
				<itemtemplate>
				    <asp:LinkButton ID="LinkButton1" runat="server" 
                        CommandArgument='<%# DataBinder.Eval(Container, "DataItem.CampaignID") %>' 
                        oncommand="LinkButton1_Command">Select</asp:LinkButton>
				</itemtemplate>
				<footertemplate>
				</footertemplate>
			</asp:templatecolumn>
			
		</columns>
		<pagerstyle horizontalalign="Left" forecolor="#000066" backcolor="White" cssclass="CSPager"
			mode="NumericPages"></pagerstyle>
	</asp:datagrid>
</div>
    </div>
    </form>
</body>
</html>
