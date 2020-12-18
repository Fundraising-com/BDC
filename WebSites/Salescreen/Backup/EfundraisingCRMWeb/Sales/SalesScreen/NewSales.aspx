<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="NewSales.aspx.cs" Inherits="EFundraisingCRMWeb.Sales.SalesScreen.NewSales" %>
<%@ Register TagPrefix="uc1" TagName="Status" Src="../../Components/User/Sales/Status.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SaleDates" Src="../../Components/User/Sales/SaleDates.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PaymentOptions" Src="../../Components/User/PaymentInformation/PaymentOptions.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Items" Src="../../Components/User/Sales/Items.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SaleInfo" Src="../../Components/User/Sales/SaleInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientHeader" Src="../../Components/User/ClientHeader.ascx" %>

<asp:Content id="Content3" ContentPlaceHolderID=ContentPlaceHolder1 runat=server> <!--CssClass="GeneralBackGround" -->
		<TABLE height="100%" cellSpacing="0" cellPadding="0" width="845" border="0" id="test111">
		 
    <asp:Panel ID="pnlModal" runat="server" CssClass="modalPopup" Style="display: none;">
         <asp:Image ID="Image1" runat="server" 
            ImageUrl="~/Ressources/Images/warning.jpg" />
        &nbsp;&nbsp;<asp:Label ID="ModalLabel" runat="server" Font-Bold="False" 
            Font-Size="12pt"></asp:Label>
        <br /><br />
        <asp:Button ID="btnClose" runat="server" Text="Close" />
    </asp:Panel>
    <asp:ModalPopupExtender TargetControlID="DummyButton" ID="pnlModal_ModalPopupExtender"
        runat="server" BackgroundCssClass="modalBackground"
        PopupControlID="pnlModal" CancelControlID="btnClose" DropShadow="true">
    </asp:ModalPopupExtender>
    
			<TR>
				<TD class="FrameBorder" width="1" height="1"></TD>
				<TD class="FrameBody" align="center">&nbsp;&nbsp;
				</TD>
				<TD class="FrameBody" align="center">
					<uc1:ClientHeader id="ClientHeader1" runat="server"></uc1:ClientHeader></TD>
				<TD class="FrameBorder" width="1" height="1"></TD>
			</TR>
			<TR>
				<TD class="FrameBorder" width="1" height="22"></TD>
				<TD class="FrameBody" align="center" height="22"></TD>
				<TD class="FrameBody" align="center" height="22">
					<HR width="100%" color="gainsboro" noShade SIZE="2">
					<asp:label id="errorMainLabel" runat="server" Visible="False" ForeColor="Red"></asp:label></TD>
				<TD class="FrameBorder" width="1" height="22"></TD>
			</TR>
			<TR>
				<TD class="FrameBorder" width="1" height="1"></TD>
				<TD class="FrameBody" height="10"></TD>
				<TD class="FrameBody" height="10">
					<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
						<TR>
							<TD vAlign="top" width="320">
								<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="320" border="0">
									<TR>
										<TD vAlign="top" height="63">
											<asp:Label id="Label2" runat="server" CssClass="FrameTitleColor">Sale Info</asp:Label><BR>
											<uc1:SaleInfo id="SaleInfo1" runat="server"></uc1:SaleInfo></TD>
									</TR>
									<TR>
										<TD></TD>
									</TR>
									<TR>
										<TD>
											<asp:Label id="Label3" runat="server" CssClass="FrameTitleColor">Status</asp:Label><BR>
											<uc1:Status id="SaleStatus1" runat="server"></uc1:Status></TD>
									</TR>
								</TABLE>
							</TD>
							<TD vAlign="top" width="257">
								<asp:Label id="Label1" runat="server" CssClass="FrameTitleColor">Sale Dates</asp:Label><BR>
								<table border="0" style="width: 100%; height: 256px;">
                                    <tr>
                                        <td>
								<uc1:SaleDates id="SaleDates1" runat="server"></uc1:SaleDates>
                               
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table style="width:100%;">
                                                <tr>
                                                    <td class="style2" style="width: 86px; height: 1px">
                                                        <asp:Label id="Label8" runat="server" CssClass="FrameTitleColor">External Data</asp:Label>
                                                    </td>
                                                    <td style="height: 1px">
                                                    </td>
                                                    <td style="height: 1px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style2" style="width: 86px">
											<asp:label id="Label10" runat="server" Font-Bold="True" ForeColor="Gray">Ship Date</asp:label>
                                                    </td>
                                                    <td>
											<asp:label id="Label11" runat="server" Font-Bold="True" ForeColor="Gray">Account Id</asp:label>
                                                    </td>
                                                    <td>
											<asp:label id="Label4" runat="server" Font-Bold="True" ForeColor="Gray">QSP id</asp:label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style2" style="width: 86px">
                                                        <asp:textbox id=ShipDateTextBox runat="server" 
                                                            CssClass="NormalText specialTextBox" Columns="9" BorderStyle="Solid" 
                                                            ReadOnly="True" Width="76px"></asp:textbox>
                                                    </td>
                                                    <td>
                                                        <asp:textbox id=AccountIDTextBox runat="server" 
                                                            CssClass="NormalText specialTextBox" Columns="9" BorderStyle="Solid" 
                                                            ReadOnly="True" Width="76px"></asp:textbox>
                                                    </td>
                                                    <td>
                                                        <asp:textbox id=QSPIdTextBox runat="server" 
                                                            CssClass="NormalText specialTextBox" Columns="9" BorderStyle="Solid" 
                                                            
                                                            ReadOnly="True" Width="57px"></asp:textbox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                               
                            </TD>
							<TD vAlign="top" align="left">
								<asp:Label id="Label5" runat="server" CssClass="FrameTitleColor">Payment Options</asp:Label>
								<uc1:PaymentOptions id="PaymentOptions1" runat="server"></uc1:PaymentOptions>
								<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="240" border="0" 
                                    style="height: 210px">
									<TR>
										<TD style="width: 93px; height: 9px;"></TD>
									</TR>
									<TR>
										<TD style="width: 93px; height: 3px;">
											<asp:Label id="Label6" runat="server" CssClass="FrameTitleColor">Comment</asp:Label></TD>
									</TR>
									<TR>
										<TD style="width: 93px; height: 34px;">
											<asp:TextBox id="CommentTextBox" runat="server" Width="240px" CssClass="NormalText normalTextBox"
												Height="60px" TextMode="MultiLine" BorderStyle="Solid"></asp:TextBox></TD>
									</TR>
								
									<TR>
										<TD class="NormalText" >
											&nbsp;&nbsp;<table style="width:102%; height: 24px;">
                                                <tr>
                                                    <td style="height: 7px">
											            &nbsp;</td>
                                                    <td style="width: 106px; height: 7px;" class="style2">
											            &nbsp;</td>
                                                          <td style="width: 334px; height: 7px;">
											                  &nbsp;</td>
                                                  
                                                </tr>
                                         
                                                <tr>
                                                    <td style="height: 7px">
											<asp:label id="Label7" runat="server" class="NormalText" Font-Bold="True" ForeColor="Gray">OE 
                                                        id</asp:label>
                                                    </td>
                                                    <td style="width: 106px; height: 7px;" class="style2">
											<asp:label id="Label9" runat="server" Font-Bold="True" ForeColor="Gray" Width="81px">Order Status</asp:label>
                                                    </td>
                                                          <td style="width: 334px; height: 7px;">
											<asp:label id="Label12" runat="server" Font-Bold="True" ForeColor="Gray">Account Status</asp:label>
											                  </td>
                                                  
                                                </tr>
                                         
                                                <tr>
                                                    <td>
                                            <asp:textbox id=OEIdTextBox runat="server" CssClass="NormalText specialTextBox" 
                                                Columns="9" BorderStyle="Solid" ReadOnly="True" Width="57px"></asp:textbox>
                                                    </td>
                                                    <td style="width: 106px" class="style2">
                                                        <asp:textbox id=StatusTextbox runat="server" CssClass="NormalText specialTextBox" 
                                                            Columns="9" BorderStyle="Solid" ReadOnly="True" Width="80px"></asp:textbox></td>
                                                            <td style="width: 334px">
                                                        <asp:textbox id=AccountStatusTextbox runat="server" CssClass="NormalText specialTextBox" 
                                                            Columns="9" BorderStyle="Solid" ReadOnly="True" Width="94px"></asp:textbox></td>
                                                                                                         
                                            </table>
                                        </TD>
									</TR>
									
								</TABLE>
							</TD>
						</TR>
					</TABLE>
				</TD>
				<TD class="FrameBorder" width="1" height="1"></TD>
			</TR>
			<TR>
				<TD class="FrameBorder" width="1" height="44"></TD>
				<TD class="FrameBody" height="44"></TD>
				<TD class="FrameBody" height="44">
					<TABLE class="TBBorder" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
						<TR>
							<TD class="BigTextBold" vAlign="top">
								<uc1:Items id="Items1" runat="server"></uc1:Items></TD>
						</TR>
					</TABLE>
				</TD>
				<TD class="FrameBorder" width="1" height="44"></TD>
			</TR>
			<TR>
				<TD class="FrameBorder" width="1" height="20"></TD>
				<TD class="FrameBody" align="right" height="20"></TD>
				<TD class="FrameBody" align="right" height="20">&nbsp;</TD>
				<TD class="FrameBorder" width="1" height="20"></TD>
			</TR>
			<TR>
				<TD class="FrameBorder" width="1" height="24"></TD>
				<TD class="FrameBody" align="right" height="24"></TD>
				<TD class="FrameBody" align="right" height="24">
				
					<asp:Button id="BackButton" tabIndex="4" runat="server" Text="Back To Client" onclick="BackButton_Click"></asp:Button>
					<asp:Button ID="RefundButton" runat="server" onclick="RefundButton_Click" 
                        Text="Refund" Visible="False" />
					<asp:Button id="PrintPOButton" tabIndex="3" runat="server" Text="Print Quote" 
                        onclick="PrintPOButton_Click"></asp:Button>
					<asp:Button id="CreditCheckButton" tabIndex="2" runat="server" Text="Run Credit Check" onclick="CreditCheckButton_Click"></asp:Button>
					<asp:Button id="ValidateSaleButton" tabIndex="1" runat="server" Text="Recalculate / Validate" onclick="ValidateSaleButton_Click"></asp:Button>
					<asp:Button id="SaveButton" runat="server" Text="Save" 
                        onclick="SaleButton_Click"></asp:Button>&nbsp;&nbsp;&nbsp;
				</TD>
				<TD class="FrameBorder" width="1" height="24"></TD>
			</TR>
			<TR>
				<TD class="FrameBorder" width="1" height="1"></TD>
				<TD class="FrameBorder" align="right" height="1"></TD>
				<TD class="FrameBorder" align="right" height="1">
					<asp:label id="errorLabel" runat="server" ForeColor="Red"></asp:label></TD>
				<TD class="FrameBorder" width="1" height="1"></TD>
			</TR>
		</TABLE>
		<asp:TextBox id="DoPostBackTextBox" Width="0" CssClass="hiddenTextBox" Height="0" AutoPostBack="True"
			Runat="server"></asp:TextBox>
    <asp:Label ID="versionError" runat="server" ForeColor="#CCCCCC" Text="v1.201"></asp:Label>
				
					<asp:Button ID="DummyButton" runat="server" Height="1px" 
        Width="1px" BackColor="White" BorderStyle="None" />
					</asp:Content>
