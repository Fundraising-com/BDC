<%@ Register TagPrefix="uc1" TagName="PaymentInformation" Src="../../Components/User/PaymentInformation/PaymentInformation.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UserInformation" Src="../../Components/User/BasicUserInformation/UserInformation.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SearchForm" Src="../../Components/User/Search/SearchForm.ascx" %>
<%@ Register TagPrefix="efundraising" Namespace="efundraising.Web.UI.MasterPages" Assembly="efundraising.Web.UI.MasterPages" %>
<%@ Page language="c#" Codebehind="ValidatePayments.aspx.cs" AutoEventWireup="True" Inherits="efundraising.EFundraisingCRMWeb.Online.ProcessChecks.ValidatePayments"  EnableEventValidation ="false"%>
<%@ Register TagPrefix="cc1" Namespace="efundraising.Web.UI.InputControls" Assembly="efundraising.Web.UI.InputControls" %>
<%@ Register assembly="AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" namespace="AjaxControlToolkit" tagprefix="ajaxControlToolkit" %>

<efundraising:masterpage id="MasterPage1" runat="server" master="~/MasterPages/CrmView.ascx">
	<efundraising:Content id="Content1" runat="server" ContentPlaceHolderID="cph_PageContent"> 
<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
			<TR>
				<TD class="FrameBorder" width="40" height="1"></TD>
				<TD class="FrameBorder" height="1"></TD>
				<TD class="FrameBorder" width="1" height="1"></TD>
			</TR>
			<TR>
				<TD class="FrameBorder" height="1"></TD>
				<TD align="left" ><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
				<div>
				<ajaxControlToolkit:Accordion ID="Accordion1" runat="server" ContentCssClass="AccordionContent" HeaderCssClass = "AccordionHeader" AutoSize="Fill" Height=250 BackColor= "#D4D0C8"  >
				    <Panes>
				        <ajaxControlToolkit:AccordionPane ID="DisplayPayments" runat="server" >
				            <Header  ><asp:LinkButton ID = "DisplayLinkButton" runat="server" CssClass="NormalText White">Display Payments to Process</asp:LinkButton> </Header>
				            <Content>
				                <TABLE border=0 cellpadding =5 cellspacing = 5>
				                     <tr><TD class="NormalTextBold">Country (US and CA):&nbsp;
								    <asp:dropdownlist id="CountryDisplayDropdownlist" runat="server" cssClass="NormalText">
									    <asp:ListItem Value="US">US</asp:ListItem>
									    <asp:ListItem Value="CA">Canada</asp:ListItem>
								    </asp:dropdownlist></TD><td><asp:label id="InformationLabel" Runat="server" CssClass="NormalTextBold"></asp:label></td>
				                    </tr>
				                     <tr><td align ="right" ><asp:Button runat="server" id ="DisplayPaymentButton" CssClass="buttonFlat CursorPointer" ButtonType="PushButton" 
									    NOVALIDATION  Text="Dispaly Payments" CausesValidation="False"  /></td>                                                                      
									    <td><asp:Button runat="server" id = "GenerateFileButton" CssClass="buttonFlat CursorPointer" ButtonType="PushButton"
									    NOVALIDATION  Text="Generate File" CausesValidation="False"  /></td><td></td>
									  </tr>
				                    </TABLE>
				                 </Content>
				                </ajaxControlToolkit:AccordionPane>
				                <ajaxControlToolkit:AccordionPane ID="ApproveBatch" runat="server">
				                    <Header><asp:LinkButton ID = "ApproveLinkButton" runat="server" CssClass="NormalText White">Approve Batch</asp:LinkButton></Header>
				                    <Content>
				                         <TABLE border=0 cellpadding =5 cellspacing = 5>
				                         <tr><td class="NormalTextBold">Select File Name: </td><td colspan=2> <asp:dropdownlist id="BatchDisplayDropdownlist" runat="server" cssClass="NormalText" AutoPostBack = "true"/></td></tr>
				                         <tr><td><asp:Button runat="server" id ="RejectBatchtButton" CssClass="buttonFlat CursorPointer" ButtonType="PushButton"
									    NOVALIDATION  Text="Reject" CausesValidation="False"   /></td><td><asp:Button runat="server" id ="ApproveBatchButton" CssClass="buttonFlat CursorPointer" ButtonType="PushButton"
									    NOVALIDATION  Text="Approve" CausesValidation="False"  /></td></tr>
				                         </TABLE>
				                    </Content>
				                </ajaxControlToolkit:AccordionPane>
				                <ajaxControlToolkit:AccordionPane ID="SearchPayments" runat="server">
				                    <Header ><asp:LinkButton ID = "SearchLinkButton" runat="server" CssClass="NormalText White">Search Payments</asp:LinkButton></Header>
				                    <Content>
				                <TABLE border=0 cellpadding =5 cellspacing = 5>
						    <TR>
							    <TD class="NormalText">Country:&nbsp;
								    <asp:dropdownlist id="CountryDropdownlist" runat="server" cssClass="NormalText">
									    <asp:ListItem Value="US">US</asp:ListItem>
									    <asp:ListItem Value="CA">Canada</asp:ListItem>
								    </asp:dropdownlist>
								</TD>
							    <TD class="NormalText">PaymentStatus<asp:dropdownlist id="PaymentStatusDropdownlist" runat="server" cssClass="NormalText"></asp:dropdownlist>
							    </TD>   
							    <TD class="NormalText">&nbsp;Group ID:&nbsp;<asp:dropdownlist id="GroupIdDropdownlist" runat="server" cssClass="NormalText"></asp:dropdownlist>
							    </TD>
								<TD class="NormalText"><asp:Button id = "ResetButton" runat="server" CssClass="buttonFlat CursorPointer" ButtonType="PushButton" NOVALIDATION Text ="Reset"
								CausesValidation = "False"  onclick="ResetButton_Click" ></asp:Button></TD>
						</TR>
							<TR>
							    <TD class="NormalText">File Name - Batch #
								    <asp:dropdownlist id="FileName" runat="server" cssClass="NormalText"></asp:dropdownlist>
							    </TD>
							    <TD class="NormalText">&nbsp;Payable to&nbsp;
								   	<asp:dropdownlist id="PaymentTodropdown" runat="server" cssClass="NormalText">
								   		</asp:dropdownlist>
									</TD>
							    <TD class="NormalText">&nbsp;Check Number:&nbsp;
								    <asp:textbox id="CheckTextBox" runat="server" CssClass="NormalText"></asp:textbox></TD>	
								 <TD class="NormalText"><asp:Button id="ShowButton" runat="server" CssClass="buttonFlat CursorPointer" ButtonType="PushButton"
									    NOVALIDATION Text="Show" CausesValidation="False" onclick="ShowButton_Click"></asp:Button>
									   </TD>
								</TR>						
							<TR>
							    <td ><asp:checkbox id="ValidatedCheckBox" runat="server"  CssClass="NormalText"  Text="Validated"  ></asp:checkbox></td>
								<TD class="NormalText">&nbsp;Payment Period:&nbsp;
								<asp:dropdownlist id="PaymentPeriodDropDown" runat="server" cssClass="NormalText">
								</asp:dropdownlist></TD>
							<TD class="NormalText">Partner: <asp:dropdownlist id="PartnerTypeDropDown" runat="server" cssClass="NormalText" ></asp:dropdownlist></TD>
							<td>&nbsp;</td>
						</TR>
					</TABLE></Content>
					                
				                </ajaxControlToolkit:AccordionPane>
				          </Panes>
				    </ajaxControlToolkit:Accordion>
				    </div>
				</TD>
				<TD class="FrameBorder" height="1"></TD>
			 </TR>
				<TR>
				<TD class="FrameBorder"  height="1"></TD>
				<TD align="left">
					
				</TD>
				<TD class="FrameBorder"  height="1"></TD>
			</TR>
			<TR>
				<TD class="FrameBorder" ></TD>
				<TD align="center">
					<TABLE cellSpacing="10" cellPadding="0" border="0" align="center">
						<TR>
							<TD vAlign="top">
								<asp:DataGrid id="PaymentToProcessDataGrid" runat="server" CssClass="NormalText" PageSize="20"
									DataKeyField="PaymentID" AutoGenerateColumns="False" CellPadding="3" BorderWidth="1px" BorderStyle="None"
									BorderColor="#0A246A" AllowPaging="True" AllowSorting="True">
									<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
									<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
									<AlternatingItemStyle BackColor="#D4D0C8"></AlternatingItemStyle>
									<ItemStyle HorizontalAlign="Center" ForeColor="#000066"></ItemStyle>
									<HeaderStyle Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#0A246A"></HeaderStyle>
									<Columns>
										<asp:TemplateColumn>
											<HeaderTemplate>
												<asp:LinkButton CssClass="NormalText White" id="PaymentIDLinkButton" runat="server">ID</asp:LinkButton>
											</HeaderTemplate>
											<ItemTemplate>
												
												<asp:Label id="PaymentIDLabel" runat="server">
													<%# DataBinder.Eval(Container.DataItem, "payment.PaymentId") %>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn>
											<HeaderTemplate>
												<asp:LinkButton id="PaymentPeriodLinkButton" CssClass="NormalText White" runat="server">Payment Period</asp:LinkButton>
											</HeaderTemplate>
											<ItemTemplate>
												<asp:Label id="PaymentPeriodNameLabel" runat="server">
													<%# DataBinder.Eval(Container.DataItem, "checkSystemPaymentPeriod.StartDate", "{0:MMM-dd, yyyy}")%>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn>
											<HeaderTemplate>
												<asp:LinkButton id="GroupNameLinkButton" CssClass="NormalText White" runat="server">Group Name</asp:LinkButton>
											</HeaderTemplate>
											<ItemTemplate>
												<asp:Label id="GroupNameLabel" runat="server">
													<%# DataBinder.Eval(Container.DataItem, "group.Name") %>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn>
											<HeaderTemplate>
												<asp:LinkButton id="GroupStatusLinkButton" CssClass="NormalText White" runat="server">Group Status</asp:LinkButton>
											</HeaderTemplate>
											<ItemTemplate>
												<asp:Label id="GroupStatusLabel" runat="server">
													<%# DataBinder.Eval(Container.DataItem, "groupStatus.Description")%>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn>
											<HeaderTemplate>
												<asp:LinkButton id="PartnerNameLinkButton" CssClass="NormalText White" runat="server">Partner Name</asp:LinkButton>
											</HeaderTemplate>
											<ItemTemplate>
												<asp:Label id="PartnerNameLabel" runat="server">
													<%# DataBinder.Eval(Container.DataItem, "PartnerName")%>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn>
											<HeaderTemplate>
												<asp:LinkButton id="PaymentAmountLinkButton" CssClass="NormalText White" runat="server">Payment Amount</asp:LinkButton>
											</HeaderTemplate>
											<ItemTemplate>
												<asp:Label id="PaymentAmountLabel" runat="server">
													<%# DataBinder.Eval(Container.DataItem, "payment.PaidAmount", "{0:c}")%>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn>
											<HeaderTemplate>
												<asp:LinkButton id="PaymentStatusLinkButton" CssClass="NormalText White" runat="server">Validated</asp:LinkButton>
											</HeaderTemplate>
											<ItemTemplate>
												<asp:Label id="PaymentStatusLabel" runat="server">
													<%# DataBinder.Eval(Container.DataItem, "payment.IsValidated")%>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn>
											<ItemTemplate>
												<asp:Button CssClass="buttonFlat CursorPointer" id="ViewButton" runat="server" NOVALIDATION ButtonType="PushButton" CommandName="Select" CommandArgument ='<%# DataBinder.Eval(Container.DataItem, "payment.PaymentId") %> ' Text="View" width="40px" ToolTip="View Detailed Information this payment" CausesValidation="false" >
												</asp:Button>
											</ItemTemplate>
										</asp:TemplateColumn>
									</Columns>
									<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="#D4D0C8" PageButtonCount="15"
										Mode="NumericPages"></PagerStyle>
								</asp:DataGrid></TD>
						</TR>
						<TR>
							<asp:Panel id="PaymentInformationPanel" Runat="Server"></TR>
					</TABLE>
					<TABLE cellSpacing="10" cellPadding="0" border="0">
						<TR>
							<TD width="10%"></TD>
							<TD>
								<uc1:PaymentInformation id="PaymentInformation1" runat="server"></uc1:PaymentInformation></TD>
							<TD width="10%"></TD>
						</TR>
						<TR>
							<TD></TD>
							<TD>
								<DIV align="center">
									<asp:Button id="ViewHistoricButton" style="MARGIN-RIGHT: 10px" Runat="server" CssClass="buttonFlat CursorPointer"
										ButtonType="PushButton" Text="View Historic" CausesValidation="false" ToolTip="View History of this Payment" onclick="ViewHistoricButton_Click"></asp:Button>
									<asp:Button id="UpdateButton" style="MARGIN-LEFT: 10px" Runat="server" CssClass="buttonFlat CursorPointer"
										ButtonType="PushButton" Text="Update" CausesValidation="false" ToolTip="Update Detailed Information about Payment" onclick="UpdateButton_Click"></asp:Button></DIV>
							</TD>
							<TD></TD>
						</TR>
					</TABLE>
					</asp:Panel></TD>
			</TR>
		</TABLE></TD><TD class="FrameBorder" width="1"></TD></TR><TR>
			<TD class="FrameBorder" width="1" height="1"></TD>
			<TD class="FrameBorder" height="1"></TD>
			<TD class="FrameBorder" width="1" height="1"></TD>
		</TR></TABLE>
		</efundraising:Content>
</efundraising:masterpage>









