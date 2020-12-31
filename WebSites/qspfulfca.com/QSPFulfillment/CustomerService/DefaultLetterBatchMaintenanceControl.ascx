<%@ Control Language="c#" AutoEventWireup="false" Codebehind="DefaultLetterBatchMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.CustomerService.DefaultLetterBatchMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../Common/DateEntry.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment"  %>
<%@ Register TagPrefix="cc1" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment" %>
<div id="divSearch" runat="server">
	<table cellSpacing="0" cellPadding="1" width="97%" bgColor="#000000" border="0">
		<tr>
			<td>
				<TABLE id="Table1ss" cellSpacing="0" cellPadding="2" width="100%" bgColor="#ffffff" border="0">
					<TR>
						<TD class="CSTableHeader" colSpan="2">Letter Batch History</TD>
					</TR>
					<tr bgColor="#ffffff">
						<td vAlign="top">
							<table id="Table1" cellSpacing="0" cellPadding="2" width="100%" border="0">
								<tr>
									<td><asp:label id="Label1s" runat="server" cssclass="CSPlainText"> Template</asp:label><br>
										<cc2:lettertemplateselectiondropdownlist id="ddlLetterTemplateSearch" runat="server" InitialText="Please select..." InitialValue="-1"
											Required="False" AutoPostBack="true"></cc2:lettertemplateselectiondropdownlist></td>
									<td><asp:label id="Label1" runat="server" cssclass="csPlainText">Date Created From</asp:label><br>
										<uc1:dateentry id="dteDateCreatedFromSearch" runat="server" EmptyValue="1995-01-01"></uc1:dateentry></td>
									<td><asp:label id="Label2" runat="server" cssclass="csPlainText">Date From</asp:label><br>
										<uc1:dateentry id="dteDateFromSearch" runat="server" EmptyValue="1995-01-01"></uc1:dateentry></td>
									<td><asp:label id="Label4" runat="server" cssclass="csPlainText">Remit Batch ID From</asp:label><br>
										<cc3:textboxinteger id="tbxRemitBatchIDFromSearch" runat="server"></cc3:textboxinteger></td>
									<td><asp:label id="Label5" runat="server" cssclass="csPlainText">Is Printed</asp:label><br>
										<cc3:dropdownlistinteger id="ddlIsPrintedSearch" Runat="server">
											<asp:ListItem Value="-1" Selected="True">Please select...</asp:ListItem>
											<asp:ListItem Value="1">Yes</asp:ListItem>
											<asp:ListItem Value="0">No</asp:ListItem>
										</cc3:dropdownlistinteger></td>
									<td valign="bottom">
										<asp:button id="btnSearch" runat="server" cssclass="boxlook" text="Search"></asp:button>
									</td>
								</tr>
								<tr>
									<td><asp:label id="Label3s" runat="server" cssclass="CSPlainText">Type</asp:label><br>
										<cc3:dropdownlistinteger id="ddlLetterBatchTypeSearch" InitialText="Please select..." InitialValue="-1" Runat="server"></cc3:dropdownlistinteger></td>
									<td><asp:label id="Label7" runat="server" cssclass="csPlainText">To</asp:label><br>
										<uc1:dateentry id="dteDateCreatedToSearch" runat="server" EmptyValue="1995-01-01"></uc1:dateentry></td>
									<td><asp:label id="Label8" runat="server" cssclass="csPlainText">To</asp:label><br>
										<uc1:dateentry id="dteDateToSearch" runat="server" EmptyValue="1995-01-01"></uc1:dateentry></td>
									<td><asp:label id="Label9" runat="server" cssclass="csPlainText">To</asp:label><br>
										<cc3:textboxinteger id="tbxRemitBatchIDToSearch" runat="server"></cc3:textboxinteger></td>
									<td><asp:label id="Label6" runat="server" cssclass="csPlainText">Status</asp:label><br>
										<cc3:dropdownlistinteger id="ddlIsLockedSearch" Runat="server">
											<asp:ListItem Value="-1" Selected="True">Please select...</asp:ListItem>
											<asp:ListItem Value="0">Open</asp:ListItem>
											<asp:ListItem Value="1">Closed</asp:ListItem>
										</cc3:dropdownlistinteger></td>
									<td valign="bottom">
										<input class="boxlook" onclick="Reset('divSearch')" type="button" value="Reset">
									</td>
								</tr>
								<tr>
									<td colspan="6">
										&nbsp;
									</td>
								</tr>
								<tr>
									<td colspan="6">
										<asp:datagrid id="dtgMain" runat="server" cellpadding="3" backcolor="White" borderwidth="1px"
											borderstyle="None" bordercolor="#CCCCCC" allowpaging="True" searchmode="0" autogeneratecolumns="False"
											showfooter="True">
											<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
											<ItemStyle ForeColor="#000066" CssClass="CSSearchResult"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="White" CssClass="CSSearchResult" BackColor="#006699"></HeaderStyle>
											<FooterStyle ForeColor="#000066" CssClass="CSSearchResult" BackColor="White"></FooterStyle>
											<Columns>
												<asp:TemplateColumn Visible="False">
													<ItemTemplate>
														<asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="LetterTemplateName" HeaderText="Template"></asp:BoundColumn>
												<asp:TemplateColumn Visible="False">
													<ItemTemplate>
														<asp:Label id="lblLetterTemplateID" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LetterTemplateID") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="DateCreated" HeaderText="Date Created" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
												<asp:TemplateColumn Visible="False">
													<ItemTemplate>
														<asp:Label id="lblDateCreated" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DateCreated") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="SubscriptionCount" HeaderText="Count"></asp:BoundColumn>
												<asp:BoundColumn DataField="LetterBatchTypeDescription" HeaderText="Type"></asp:BoundColumn>
												<asp:TemplateColumn Visible="False">
													<ItemTemplate>
														<asp:Label id="lblLetterBatchType" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LetterBatchType") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="DateFrom" HeaderText="Date From" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
												<asp:TemplateColumn Visible="False">
													<ItemTemplate>
														<asp:Label id="lblDateFrom" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DateFrom") != DBNull.Value ? DataBinder.Eval(Container, "DataItem.DateFrom") : "1995-01-01" %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="DateTo" HeaderText="Date To" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
												<asp:TemplateColumn Visible="False">
													<ItemTemplate>
														<asp:Label id="lblDateTo" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DateTo") != DBNull.Value ? DataBinder.Eval(Container, "DataItem.DateTo") : "1995-01-01" %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Remit Batch ID">
													<ItemTemplate>
														<asp:Label id="lblRunIDDisplay" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RunID") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn Visible="False">
													<ItemTemplate>
														<asp:Label id="lblRunID" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RunID") != DBNull.Value ? DataBinder.Eval(Container, "DataItem.RunID") : "0" %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Is Printed">
													<ItemTemplate>
														<asp:Label id="lblIsPrintedDescription" Runat="server" Text='<%# Convert.ToBoolean(DataBinder.Eval(Container, "DataItem.IsPrinted")) ? "Yes" : "No" %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn Visible="False">
													<ItemTemplate>
														<asp:Label id="lblIsPrinted" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IsPrinted") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="DatePrinted" HeaderText="Date Printed" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
												<asp:TemplateColumn Visible="False">
													<ItemTemplate>
														<asp:Label id="lblDatePrinted" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DatePrinted") != DBNull.Value ? DataBinder.Eval(Container, "DataItem.DatePrinted") : "1995-01-01" %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Status">
													<ItemTemplate>
														<asp:Label id="lblIsLockedDescription" Runat="server" Text='<%# Convert.ToBoolean(DataBinder.Eval(Container, "DataItem.IsLocked")) || Convert.ToInt32(DataBinder.Eval(Container, "DataItem.LetterBatchType")) != 67003 ? "Closed" : "Open" %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn Visible="False">
													<ItemTemplate>
														<asp:Label id="lblIsLocked" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IsLocked") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="UserNameCreated" HeaderText="Created By"></asp:BoundColumn>
												<asp:TemplateColumn Visible="False">
													<ItemTemplate>
														<asp:Label id="lblUserIDCreated" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UserIDCreated") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn Visible="False">
													<ItemTemplate>
														<asp:Label id="lblDeletedTF" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DeletedTF") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn Visible="False">
													<ItemTemplate>
														<asp:Label ID="lblReportName" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ReportName") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<ItemTemplate>
														<asp:LinkButton id="lnkCancel" runat="server" CommandName="Cancel">Cancel</asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<ItemTemplate>
														<asp:LinkButton id="lnkReprint" runat="server" CommandName="Reprint" Visible='<%# Convert.ToBoolean(DataBinder.Eval(Container, "DataItem.IsLocked")) || Convert.ToInt32(DataBinder.Eval(Container, "DataItem.LetterBatchType")) != 67003 %>'>Print</asp:LinkButton>
														<asp:LinkButton id="lnkClose" runat="server" CommandName="CloseBatch" Visible='<%# !Convert.ToBoolean(DataBinder.Eval(Container, "DataItem.IsLocked")) && Convert.ToInt32(DataBinder.Eval(Container, "DataItem.LetterBatchType")) == 67003 %>'>Close</asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Mark As">
													<ItemTemplate>
														<asp:LinkButton id="lnkMarkPrinted" runat="server" CommandName="MarkPrinted" Visible='<%# !Convert.ToBoolean(DataBinder.Eval(Container, "DataItem.IsPrinted")) %>'>Printed</asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="#999999" CssClass="CSPager"
												Mode="NumericPages"></PagerStyle>
										</asp:datagrid>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</TABLE>
			</td>
		</tr>
	</table>
</div>
<br>
<cc1:rsgeneration id="rsGenerationSwitchLetter" runat="server" reportname="SwitchLetter"></cc1:rsgeneration>
