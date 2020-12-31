<%@ Register TagPrefix="uc1" TagName="FiscalYearSelectControl" Src="../Common/FiscalYearSelectControl.ascx" %>
<%@ Register TagPrefix="uc2" TagName="StatementRunSelectControl" Src="~/Finance/Control/StatementRunSelectControl.ascx" %>
<%@ Register TagPrefix="uc3" TagName="DatePicker" Src="../Common/DateEntry.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu" %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment" %>

<%@ Page Language="c#" CodeBehind="StatementReportPrintByCampaign.aspx.cs" AutoEventWireup="false"
    Inherits="QSPFulfillment.Finance.StatementReportPrintByCampaign" %>

<html>
<head>
    <title>CA Fulfill System - Campaign Statement Report Print</title>
    <link rel="stylesheet" href="../Includes/MagSysStyle.css" type="text/css">
    </script>
    <style type="text/css">
        .style1
        {
            width: 88px;
        }
        .style2
        {
            height: 19px;
            width: 150px;
        }
        .style4
        {
            width: 361px;
        }
        .style5
        {
            width: 160px;
        }
        #OfficialStatement
        {
            width: 20px;
        }
        #RealtimeStatement
        {
            width: 20px;
        }
        .style6
        {
            width: 315px;
        }
    </style>
</head>
<body topmargin="0" leftmargin="0">
    <form method="post" runat="server" id="StatementForm">
    <!-- #include file="../Includes/Menu.inc" -->
    <br>
    <center>
        <h3>
            <font face="Verdana" color="#2f4f88">Print Campaign Statement Reports</font></h3>
    </center>
    <p>
    </p>
    <br>
    <table border="0" cellspacing="0" cellpadding="0" width="100%" align="center">
        <tr>
            <td align="center" class="style4">
                <b><font face="Verdana" size="2" color="#2f4f88">Search</font></b>&nbsp;
                <asp:TextBox ID="Search" runat="server" CssClass="boxlook" />
                &nbsp;<b><font face="Verdana" size="2" color="#2f4f88">By</font></b>&nbsp;
                <asp:DropDownList ID="ddlStatus" CssClass="boxlookW" runat="server">
                    <asp:ListItem Text="Acct Name" Value="AcctName" />
                    <asp:ListItem Text="Acct ID" Value="AcctID" />
                    <asp:ListItem Text="Camp ID" Value="CampID" />
                    <asp:ListItem Text="FM ID" Value="FMID" />
                    <asp:ListItem Text="Last Name" Value="LastName" />
                </asp:DropDownList>
            </td>
            <td class="style1">
                <font face="Verdana" size="2" color="#2f4f88">
                    <asp:Label ID="Label3" runat="server" Style="font-weight: 700">Fiscal&nbsp;Year</asp:Label></font>
            </td>
            <td class="style2">
                <uc1:FiscalYearSelectControl ID="ctrlFiscalYearSelect" runat="server" ParameterName="FiscalYear">
                </uc1:FiscalYearSelectControl>
            </td>
            <td class="style6" title="asdf">
                <asp:RadioButtonList ID="ctrlStatementType" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="ctrlStatementType_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Value="Realtime"><font face="Verdana" size="2" color="#2f4f88">Realtime Statement</font></asp:ListItem>
                    <asp:ListItem Selected="False" Value="Official"><font face="Verdana" size="2" color="#2f4f88">Official Statement from list</font></asp:ListItem>
                </asp:RadioButtonList>
                <asp:Panel runat="server" ID ="pnlShowTransactionsDateFrom" Visible="true" >    
                    <table>
                        <tr>
                            <td><asp:label id="lblShowTransactionsFrom" runat="server" cssclass="CSPlainText"><font face="Verdana" size="2" color="#2f4f88">Show Transactions From</font></asp:label>		</td>
                            <td><uc3:datepicker id="dteFromDeliveryDate" runat="server" columns="10"></uc3:datepicker>	</td>
                        </tr>
                    </table>				
                </asp:Panel>  								
                <uc2:StatementRunSelectControl id="ctrlStatementRunSelect" runat="server" ParameterName="StatementRun" Visible="false"></uc2:StatementRunSelectControl>
                
            </td>
            <td class="style5">
                <asp:LinkButton CausesValidation="False" ID="BtnSearch" runat="server" CssClass="boxlook"
                    OnClick="SearchButtonClick" Text="<font face='Verdana' color='#2f4f88'> Go </font>" />
            </td>
            <td>
                <asp:Label runat="server" ID="lblStatement" CssClass="ClearTextBoxG" />
            </td>
        </tr>
    </table>
    <table border="0" cellspacing="0" cellpadding="2" width="100%" align="center">
        <tr>
            <td>
                <asp:DataGrid ID="CampaignStatementReportDG" OnItemDataBound="CampaignStatementReportDG_ItemDataBound"
                    HeaderStyle-Font-Bold="True" AllowSorting="true" OnSortCommand="CampaignStatementReportDG_Sort"
                    AllowPaging="True" PageSize="10" PagerStyle-Position="Bottom" PagerStyle-Mode="NumericPages"
                    PagerStyle-HorizontalAlign="Center" PagerStyle-PageButtonCount="20" PagerStyle-Width="100%"
                    PagerStyle-BackColor="#2f4f88" PagerStyle-ForeColor="white" OnPageIndexChanged="CampaignStatementReportDG_Page"
                    BackColor="#CCCCCC" runat="server" DataKeyField="AccountId" AutoGenerateColumns="False"
                    Width="100%" BorderColor="black" BorderWidth="1" GridLines="Both" CellPadding="2"
                    CellSpacing="0" font-name="Verdana" Font-Size="8pt" HeaderStyle-BackColor="#2f4f88">
                    <Columns>
                        <asp:TemplateColumn HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top"
                            HeaderText="Account ID" SortExpression="AccountID" HeaderStyle-Wrap="False">
                            <ItemTemplate>
                                <asp:Label ID="AccountID" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "AccountID") %>'
                                    runat="Server" />
                                <asp:Label ID="lblCampaignID" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "CampaignID") %>'
                                    runat="Server" />
                                <asp:Label ID="Lang" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Lang") %>'
                                    runat="Server" />
                                <asp:Label ID="lblStatementId" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "StatementId") %>'
                                    runat="Server" />
                                <cc2:RSGenerationLinkButton ID="rsGenerationStatementReportByCampaign" runat="server"
                                    CausesValidation="false" Font-Underline="True" ForeColor="RoyalBlue" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top"
                            HeaderText="Campaign ID" SortExpression="CampaignID" HeaderStyle-Wrap="False">
                            <ItemTemplate>
                                <asp:Label ID="CampaignID" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "CampaignID") %>'
                                    runat="Server" />
                                <%# DataBinder.Eval(Container.DataItem, "CampaignID") %>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top"
                            HeaderText="Account Name" SortExpression="Name" HeaderStyle-Wrap="False">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "Name") %>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top"
                            HeaderText="FMID" SortExpression="FMID" HeaderStyle-Wrap="False">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "FMID") %>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top"
                            HeaderText="FM Name" SortExpression="LastName" HeaderStyle-Wrap="False">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "LastName") %>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top"
                            HeaderText="Statement Date" SortExpression="StatementDate" HeaderStyle-Wrap="False">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "StatementDate", "{0:dddd, dd/MMM/yyyy}")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top"
                            HeaderText="Statement Run ID" SortExpression="StatementRunId" HeaderStyle-Wrap="False"
                            Visible="false">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "StatementRunId")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top"
                            HeaderText="Statement Id" SortExpression="StatementId" HeaderStyle-Wrap="False"
                            Visible="false">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "StatementId")%>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
            </td>
        </tr>
    </table>
    </center>
    </form>
    <!-- #Include File="../Includes/Footer.inc" -->
</body>
</html>
