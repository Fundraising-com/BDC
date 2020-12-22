<%@ Page language="c#" EnableEventValidation="false" MasterPageFile="~/Site1.Master" Codebehind="CreditCheckSA.aspx.cs" AutoEventWireup="True" Inherits="EFundraisingCRMWeb.Sales.SalesScreen.CreditCheckSA" %>

<%@ Register TagPrefix="uc1" TagName="CreditRequestDetails_SaleScreen" Src="../../Components/User/CreditRequestDetails_SaleScreen.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CreditInfo" Src="../../Components/User/CreditCheck/CreditInfo.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" cellSpacing="0"
				cellPadding="0" width="850" border="0">
				<TR>
					<TD class="FrameBody" align="left" height="22"></TD>
					<TD class="FrameBody" align="left" height="22"><asp:label id="ErrorClientInfoLabel" runat="server" ForeColor="Red" Font-Size="10pt" Visible="False">Correct the invalid fields (*)</asp:label></TD>
				</TR>
				<TR>
					<TD class="FrameBody"></TD>
					<TD class="FrameBody"><uc1:creditinfo id="CreditInfo1" runat="server"></uc1:creditinfo></TD>
				</TR>
				<TR>
					<TD class="FrameBody"></TD>
					<TD class="FrameBody"><uc1:creditrequestdetails_salescreen id="CreditRequestDetails_SaleScreen1" runat="server"></uc1:creditrequestdetails_salescreen></TD>
				</TR>
				<TR>
					<TD class="FrameBody"></TD>
					<TD class="FrameBody"><asp:button id="CloseButton" runat="server" Text="Close" onclick="GoBackButton_Click"></asp:button></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD></TD>
				</TR>
			</TABLE>
</asp:Content>
