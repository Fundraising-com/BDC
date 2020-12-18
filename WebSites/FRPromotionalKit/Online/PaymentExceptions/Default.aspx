<%@ Register TagPrefix="uc1" TagName="EventInformation" Src="../../Components/User/OnlineEvent/EventInformation.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PaymentInformation" Src="../../Components/User/PaymentInformation/PaymentInformation.ascx" %>
<%@ Page language="c#" Codebehind="Default.aspx.cs" AutoEventWireup="True" Inherits="efundraising.EFundraisingCRMWeb.Online.PaymentExceptions._Default" %>
<%@ Register TagPrefix="cc1" Namespace="efundraising.Web.UI.InputControls" Assembly="efundraising.Web.UI.InputControls" %>
<%@ Register TagPrefix="efundraising" Namespace="efundraising.Web.UI.MasterPages" Assembly="efundraising.Web.UI.MasterPages" %>
<%@ Register TagPrefix="uc1" TagName="SearchForm" Src="../../Components/User/Search/SearchForm.ascx" %>

		<head>
            <style type="text/css">
                .style2
                {
                    width: 170px;
                }
                .style4
                {
                    width: 205px;
                }
                .style5
                {
                    height: 27px;
                    width: 136px;
                }
                .style7
                {
                    width: 388px;
                }
                .style9
                {
                    width: 239px;
                }
                .style10
                {
                    width: 128px;
                }
                .style11
                {
                    height: 27px;
                    width: 99px;
                }
                .style12
                {
                    width: 99px;
                }
                .style13
                {
                    width: 857px;
                }
                .style14
                {
                    height: 27px;
                    width: 113px;
                }
                #Table1
                {
                    width: 822px;
                }
                .style16
                {
                    height: 27px;
                    width: 187px;
                }
                .style17
                {
                    width: 187px;
                }
                .style18
                {
                    width: 113px;
                }
                .style19
                {
                    width: 125px;
                }
            </style>
</head>
<efundraising:masterpage id="MasterPage1" runat="server" master="~/MasterPages/CrmView.ascx">
	<efundraising:Content id="Content1" runat="server" ContentPlaceHolderID="cph_PageContent">-
	
		<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
			<TR>
				<TD class="FrameBorder" width="1" height="1"></TD>
				<TD height="1">
                    <table style="width:897px;">
                        <tr>
                          
                            <td class="style7">
                            <fieldset id="fieldset1" >
							 <legend id="legend1" runat="server" 
                        
                                    style="font-family: 'Century Gothic'; color: #0000FF; font-weight: bolder; font-size: x-small;">
                                 Process File / Batch                 </legend>
                                <table style="width: 435px; height: 106px;">
                                    <tr>
                                        <td class="style19">
                                            Payment Period</td>
                                        <td class="style2">
                                            <asp:DropDownList ID="PaymentPeriodProcessDropDownList" runat="server" 
                                                Width="180px">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="style10">
                                            <asp:LinkButton ID="NewPeriodLinkButton" runat="server" Font-Size="8pt" 
                                                CausesValidation="False" onclick="NewPeriodLinkButton_Click">Add Next Period</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style19">
                                            Country
                                        </td>
                                        <td class="style2">
                                            <asp:DropDownList ID="CountryProcessDropDownList" runat="server" Width="150px">
                                                <asp:ListItem Selected="True" Value="US">U.S.</asp:ListItem>
                                                <asp:ListItem Value="CA">Canada</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="style10" valign="middle">
                                            <asp:Label ID="SingleEventLabel" runat="server" Font-Size="7pt" Text="Event ID"></asp:Label>
                                            <asp:TextBox ID="SingleEventTextBox" col="8" runat="server" Width="58px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style19">
                                            &nbsp;</td>
                                        <td class="style2">
                                             <asp:Label ID="ProcessStatusLabel" runat="server"></asp:Label>
                                            &nbsp;</td>
                                        <td class="style10">
                                            <asp:Button ID="GeneratePaymentsButton" runat="server" Text="Generate Payments" 
                                                Width="128px" CausesValidation="False" NOVALIDATION
                                                onclick="GeneratePaymentsButton_Click" />
                                        </td>
                                    </tr>
                                </table>
                                </legend>
                            </td>
                              <td align="right" class="style4" width="400">
                              <fieldset id="fieldset2" >
							 <legend id="legend2" runat="server" style="font-family: 'Century Gothic'; color: #0000FF; font-weight: bolder; font-size: x-small;">
                                 Validate Payments</legend>
                                  <table style="width: 308px">
                                      <tr>
                                          <td class="style9">
                                              Country</td>
                                          <td>
                                              &nbsp;</td>
                                      </tr>
                                      <tr>
                                          <td class="style9">
                                            <asp:DropDownList ID="CountryValidateDropDownList" runat="server" Width="150px" CausesValidation="False" NOVALIDATION >
                                                <asp:ListItem Selected="True" Value="US">U.S.</asp:ListItem>
                                                <asp:ListItem Value="CA">Canada</asp:ListItem>
                                            </asp:DropDownList>
                                          </td>
                                          <td>
                                              &nbsp;</td>
                                      </tr>
                                      <tr>
                                          <td class="style9">
                                              <asp:Label ID="Label1" runat="server" Font-Size="8pt" 
                                                  Text="****All Payments with status In_process with exception corrected or without exceptions will be updated as Validated and processed in the next check run."></asp:Label>
                                              <asp:Label ID="ValidateErrorLabel" runat="server" Font-Bold="True" 
                                                  Font-Size="10pt" ForeColor="#FF3300" Text="Error While Updating"></asp:Label>
                                          </td>
                                          <td>
                                              <asp:Button ID="ValidateButton" runat="server" Text="Update"
                                                  CausesValidation="False" NOVALIDATION onclick="ValidateButton_Click" />
                                          </td>
                                      </tr>
                                  </table>
                                  </legend>
                            </td>
                        </tr>
                    </table>
                </TD>
				<TD class="FrameBorder" width="1" height="1"></TD>
			</TR>
			<TR>
				<TD class="FrameBorder" width="1"></TD>
				<TD align="right">
					<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" align="center" border="0">
						<TR>
							<TD class="style13">
							<fieldset id="fieldset" >
							 <legend id="legend" runat="server" 
                        
                                    style="font-family: 'Century Gothic'; color: #0000FF; font-weight: bolder; font-size: x-small;">
                                 Search                 </legend>
								<TABLE class="NormalText" id="Table1" cellSpacing="2" cellPadding="2" 
                                    border="0">
									<TR>
										<TD class="NormalText" width="108" style="height: 27px">Exception Type</TD>
										<TD class="style16">
											<asp:DropDownList id="ExceptionTypeDropDownList" runat="server" Width="180px" 
                                                CssClass="NormalText"></asp:DropDownList></TD>
										<TD class="style11">Payment Status</TD>
										<TD class="style14">
											<asp:DropDownList id="PaymentStatusDropDownList" runat="server" Width="129px" CssClass="NormalText"></asp:DropDownList></TD>
										<TD width="98" style="height: 27px">Group Status</TD>
										<TD class="style5">
											<asp:DropDownList id="GroupStatusDropDownList" runat="server" Width="100px" 
                                                CssClass="NormalText"></asp:DropDownList></TD>
									</TR>
									<TR>
										<TD class="NormalText" width="108">Payment Period</TD>
										<TD class="style17">
											<asp:DropDownList id="PaymentPeriodDropDownList" runat="server" Width="180px" 
                                                CssClass="NormalText"></asp:DropDownList></TD>
										<TD class="style12">Group ID</TD>
										<TD class="style18">
											<cc1:IntegerTextBox id="GroupIDTextBox" runat="server" CssClass="NormalText" Columns="10" Nullable="True"></cc1:IntegerTextBox></TD>
										<TD class="NormalText" width="98">Check Number</TD>
										<TD>
											<cc1:IntegerTextBox id="CheckNoTextBox" runat="server" CssClass="NormalText" 
                                                Columns="10" Nullable="True" Width="116px"></cc1:IntegerTextBox></TD>
									</TR>
                                    <tr>
                                        <td class="NormalText" width="108">
                                            Exclude prior to</td>
                                        <td class="style17">
											<asp:TextBox id="period" runat="server" CssClass="NormalText" 
                                                Columns="10"></asp:TextBox>
                                        </td>
                                        <td class="NormalText" width="108">
                                            Max Results</td>
                                        <td class="style17">
											<asp:TextBox id="maxResults" runat="server" CssClass="NormalText" 
                                                Columns="10"></asp:TextBox>
                                        </td>
                                        <td class="NormalText" width="98">
                                        </td>
                                        <td align="right" >
											<asp:Button id="ShowButton" runat="server" CssClass="NormalText" NOVALIDATION ButtonType="PushButton"
												Text="Search" CausesValidation="False" onclick="ShowButton_Click"></asp:Button>
                                        </td>
                                    </tr>
								</TABLE>
								 </fieldset>
								
								
							</TD>
							<TD width="10"></TD>
						</TR>
					</TABLE>
				</TD>
				<TD class="FrameBorder" width="1"></TD>
			</TR>
			<TR>
				<TD class="FrameBorder" width="1" height="483"></TD>
				<TD height="483">
					<TABLE cellSpacing="10" cellPadding="0" border="0">
						<TR>
							<TD vAlign="top">
								<asp:DataGrid id="PaymentExceptionDataGrid" runat="server" 
                                    CssClass="NormalText" AllowSorting="True"
									AllowPaging="True" BorderColor="#0A246A" BorderStyle="None" BorderWidth="1px" CellPadding="3"
									AutoGenerateColumns="False" Width="878px">
									<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
									<AlternatingItemStyle BackColor="#D4D0C8"></AlternatingItemStyle>
									<ItemStyle ForeColor="#000066"></ItemStyle>
									<HeaderStyle Font-Names="Arial" Font-Bold="True" ForeColor="White" BackColor="#0A246A"></HeaderStyle>
									<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
									<Columns>
										<asp:BoundColumn DataField="ExceptionType" SortExpression="ExceptionType" HeaderText="Exception Type">
											<HeaderStyle Width="20%"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn Visible="False" DataField="ExceptionTypeID" SortExpression="ExceptionTypeID" HeaderText="ID"></asp:BoundColumn>
										<asp:BoundColumn DataField="PaymentID" SortExpression="PaymentID" HeaderText="ID">
											<HeaderStyle Width="5%"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="PaymentPeriod" SortExpression="PaymentPeriod" HeaderText="Payment Period">
											<HeaderStyle Width="18%"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="PartnerName" SortExpression="PartnerName" HeaderText="Partner Name">
											<HeaderStyle Width="15%"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="PaymentType" SortExpression="PartnerType" HeaderText="Payment Type">
											<HeaderStyle Width="10%"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="TotalPayment" SortExpression="TotalPayment" HeaderText="Total Payment"
											DataFormatString="{0:C}">
											<HeaderStyle Width="8%"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="PaymentAmount" SortExpression="PaymentAmount" HeaderText="Payment Amount"
											DataFormatString="{0:C}">
											<HeaderStyle Width="8%"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="PaymentStatus" SortExpression="PaymentStatus" HeaderText="Payment Status">
											<HeaderStyle Width="10px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="GroupStatus" SortExpression="GroupStatus" HeaderText="Group Status">
											<HeaderStyle Width="8%"></HeaderStyle>
										</asp:BoundColumn>
										<asp:ButtonColumn Visible="False" Text="Select" CommandName="Select"></asp:ButtonColumn>
										<asp:TemplateColumn>
											<ItemTemplate>
												<asp:Button id="ViewButton" CssClass="buttonFlat CursorPointer" runat="server" NOVALIDATION ButtonType="PushButton" CommandName="Select" CommandArgument ='<%# DataBinder.Eval(Container.DataItem, "PaymentID") %> ' Text="View" width="40px" ToolTip="View Detailed Information this payment" CausesValidation="false" >
												</asp:Button>
											</ItemTemplate>
										</asp:TemplateColumn>
									</Columns>
									<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="#D4D0C8" PageButtonCount="15"
										Mode="NumericPages"></PagerStyle>
								</asp:DataGrid>
							<TD>&nbsp;</TD>
							<TD vAlign="top"></TD>
						</TR>
					</TABLE>
					<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="100%" border="0">
						<TR>
							<TD vAlign="top"></TD>
							<TD align="center">
								<asp:Label id="errorLabel" runat="server" ForeColor="Red" Visible="False">ErrorLabel</asp:Label></TD>
						</TR>
						<TR>
							<TD vAlign="top"></TD>
							<TD align="left">
								<TABLE cellSpacing="10" cellPadding="0" border="0">
									<TR>
										<TD>
											<uc1:PaymentInformation id="PaymentInformation1" runat="server"></uc1:PaymentInformation></TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
					</TABLE>
				</TD>
				<TD class="FrameBorder" width="1" height="483"></TD>
			</TR>
			<TR>
				<TD class="FrameBorder" width="1" height="1"></TD>
				<TD align="center" height="1">
					<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="300" border="0">
						<TR>
							<TD width="114">
								<asp:Button id="ViewHistoricButton" runat="server" CssClass="buttonFlat CursorPointer" Text="View Historic" onclick="ViewHistoricButton_Click"></asp:Button></TD>
							<TD width="240">
								<asp:Button id="ReProcessButton" style="MARGIN-LEFT: 10px; MARGIN-RIGHT: 10px" CssClass="NormalText"
									ButtonType="PushButton" Text="Re-Process Payment" CausesValidation="false" Visible="False"
									Runat="server" ToolTip="View Detailed Information about payment"></asp:Button></TD>
							<TD width="77">
								<asp:Button id="UpdateButton" runat="server" CssClass="buttonFlat CursorPointer" Text="Update" onclick="UpdateButton_Click"></asp:Button></TD>
							<TD>&nbsp;</TD>
							<TD>
								<asp:Button id="IsCorrectedButton" runat="server" CssClass="buttonFlat CursorPointer" NOVALIDATION
									ButtonType="PushButton" Text="Is Corrected" CausesValidation="False" ToolTip="Remove From Exceptions" onclick="IsCorrectedButton_Click"></asp:Button></TD>
						
									
						</TR>
					</TABLE>
				</TD>
				<TD class="FrameBorder" width="1" height="1"></TD>
			</TR>
	</efundraising:Content>
</efundraising:masterpage>















