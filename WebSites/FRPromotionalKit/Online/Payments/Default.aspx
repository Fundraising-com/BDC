<%@ Register TagPrefix="uc1" TagName="EventInformation" Src="../../Components/User/OnlineEvent/EventInformation.ascx" %>
<%@ Register TagPrefix="uc1" TagName="GrandFatherInfo" Src="../../Components/User/OnlineEvent/GrandfatherEvent.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PaymentInformation" Src="../../Components/User/PaymentInformation/PaymentInformation.ascx" %>
<%@ Page language="c#" Codebehind="Default.aspx.cs" AutoEventWireup="True" Inherits="efundraising.EFundraisingCRMWeb.Online.Payments._Default" %>
<%@ Register TagPrefix="efundraising" Namespace="efundraising.Web.UI.MasterPages" Assembly="efundraising.Web.UI.MasterPages" %>
<%@ Register TagPrefix="uc1" TagName="SearchForm" Src="../../Components/User/Search/SearchForm.ascx" %>
<efundraising:masterpage id="MasterPage1" runat="server" master="~/MasterPages/CrmView.ascx">
	<efundraising:Content id="Content1" runat="server" ContentPlaceHolderID="cph_PageContent">
		<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
			<TR>
				<TD class="FrameBorder" width="2" height="1"></TD>
				<TD class="FrameBorder" height="1"></TD>
				<TD class="FrameBorder" width="1" height="1"></TD>
			</TR>
			<TR>
				<TD class="FrameBorder" width="1"></TD>
				<TD width="100%">
					<TABLE cellSpacing="10" cellPadding="0" width="100%" border="0">
						<TR>
							<TD vAlign="top">
								<uc1:EventInformation id="EventInformation1" runat="server"></uc1:EventInformation></TD>
							</TR>
					</TABLE>
					<TABLE cellSpacing="10" cellPadding="0" border="0">
						<TR>
							<TD>
								<asp:DataGrid id="PaymentHistoryDataGrid" runat="server" AutoGenerateColumns="False" CellPadding="3"
									BorderWidth="1px" BorderStyle="None" BorderColor="#0A246A" AllowPaging="True" AllowSorting="True">
									<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
									<AlternatingItemStyle BackColor="#D4D0C8"></AlternatingItemStyle>
									<ItemStyle ForeColor="#000066"></ItemStyle>
									<HeaderStyle Font-Names="Arial" Font-Bold="True" ForeColor="White" BackColor="#0A246A"></HeaderStyle>
									<FooterStyle ForeColor="#000066" BackColor="#FFFFFF"></FooterStyle>
									<Columns>
										<asp:TemplateColumn>
											<HeaderTemplate>
												<asp:LinkButton id="PaymentIDLinkButton" Font="Arial" Font-Size="14px" ForeColor="#FFFFFF" runat="server">ID</asp:LinkButton>
											</HeaderTemplate>
											<ItemTemplate>
												<asp:Label id="PartnerIDLabel" runat="server">
													<%# DataBinder.Eval(Container.DataItem, "PaymentID") %>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn>
											<HeaderTemplate>
												<asp:LinkButton id="PaymentPeriodLinkButton" Font="Arial" Font-Size="14px" ForeColor="#FFFFFF" runat="server">Payment Period</asp:LinkButton>
											</HeaderTemplate>
											<ItemTemplate>
												<asp:Label id="Label1" runat="server">
													<%# DataBinder.Eval(Container.DataItem, "PaymentPeriod") %>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn>
											<HeaderTemplate>
												<asp:LinkButton id="PaymentTypeLinkButton" Font="Arial" Font-Size="14px" ForeColor="#FFFFFF" runat="server">Payment Type</asp:LinkButton>
											</HeaderTemplate>
											<ItemTemplate>
												<asp:Label id="Label3" runat="server">
													<%# DataBinder.Eval(Container.DataItem, "PaymentType") %>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn>
											<HeaderTemplate>
												<asp:LinkButton id="TotalPaymentLinkButton" Font="Arial" Font-Size="14px" ForeColor="#FFFFFF" runat="server">Total Payment</asp:LinkButton>
											</HeaderTemplate>
											<ItemTemplate>
												<asp:Label id="Label4" runat="server">
													<%# DataBinder.Eval(Container.DataItem, "TotalPayment") %>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn>
											<HeaderTemplate>
												<asp:LinkButton id="PaymentStatusLinkButton" Font="Arial" Font-Size="14px" ForeColor="#FFFFFF" runat="server">Payment Status</asp:LinkButton>
											</HeaderTemplate>
											<ItemTemplate>
												<asp:Label id="Label5" runat="server">
													<%# DataBinder.Eval(Container.DataItem, "PaymentStatus") %>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn>
											<HeaderTemplate>
												<asp:LinkButton id="PaymentDateLinkButton" Font="Arial" Font-Size="14px" ForeColor="#FFFFFF" runat="server">Payment Date</asp:LinkButton>
											</HeaderTemplate>
											<ItemTemplate>
												<asp:Label id="Label6" runat="server">
													<%# DataBinder.Eval(Container.DataItem, "PaymentDate") %>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn>
											<HeaderTemplate>
												<asp:LinkButton id="Linkbutton1" Font="Arial" Font-Size="14px" ForeColor="#FFFFFF" runat="server">Comments</asp:LinkButton>
											</HeaderTemplate>
											<ItemTemplate>
												<asp:Label id="Label9" runat="server">
													<%# DataBinder.Eval(Container.DataItem, "NumberOfComment") %>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn>
											<HeaderTemplate></HeaderTemplate>
											<ItemTemplate>
												<asp:Button id="SeeCommentButton" Text="See Comments" ToolTip='<%# DataBinder.Eval(Container.DataItem, "PaymentID") %>' runat="server">
												</asp:Button>
											</ItemTemplate>
										</asp:TemplateColumn>
									</Columns>
									<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="#D4D0C8" PageButtonCount="15"
										Mode="NumericPages"></PagerStyle>
								</asp:DataGrid></TD>
						</TR>
						<TR>
							<TD><BR>
							</TD>
						</TR>
						<TR>
							<TD>
								<asp:DataGrid id="PaymentCommentDataGrid" runat="server" AutoGenerateColumns="False" CellPadding="3"
									BorderWidth="1px" BorderStyle="None" BorderColor="#0A246A" AllowPaging="True" AllowSorting="True"
									width="100%">
									<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
									<AlternatingItemStyle BackColor="#D4D0C8"></AlternatingItemStyle>
									<ItemStyle ForeColor="#000066"></ItemStyle>
									<HeaderStyle Font-Names="Arial" Font-Bold="True" ForeColor="White" BackColor="#0A246A"></HeaderStyle>
									<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
									<Columns>
										<asp:TemplateColumn>
											<HeaderTemplate>
												<asp:Label id="PaymentCommentDateLinkButton" Font="Arial" Font-Size="14px" ForeColor="#FFFFFF"
													runat="server">Date</asp:Label>
											</HeaderTemplate>
											<ItemTemplate>
												<asp:Label id="PaymentCommentDateLable" runat="server">
													<%# DataBinder.Eval(Container.DataItem, "CreateDate") %>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn>
											<HeaderTemplate>
												<asp:Label id="PaymentIDLinkButton" Font="Arial" Font-Size="14px" ForeColor="#FFFFFF" runat="server">Payment ID</asp:Label>
											</HeaderTemplate>
											<ItemTemplate>
												<asp:Label id="Label2" runat="server">
													<%# DataBinder.Eval(Container.DataItem, "PaymentID") %>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn>
											<HeaderTemplate>
												<asp:Label id="NtLoginLinkButton" Font="Arial" Font-Size="14px" ForeColor="#FFFFFF" runat="server">User</asp:Label>
											</HeaderTemplate>
											<ItemTemplate>
												<asp:Label id="Label7" runat="server">
													<%# DataBinder.Eval(Container.DataItem, "NtLogin") %>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn>
											<HeaderTemplate>
												<asp:Label id="CommentLinkButton" Font="Arial" Font-Size="14px" ForeColor="#FFFFFF" runat="server">Comment</asp:Label>
											</HeaderTemplate>
											<ItemTemplate>
												<asp:Label id="Label8" runat="server">
													<%# DataBinder.Eval(Container.DataItem, "Comment") %>
												</asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
									</Columns>
									<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="#D4D0C8" PageButtonCount="15"
										Mode="NumericPages"></PagerStyle>
								</asp:DataGrid></TD>
						</TR>
					</TABLE>
					<TABLE cellSpacing="10" cellPadding="0" width="100%" border="0">
						<TR>
							<TD vAlign="top">
								<uc1:GrandFatherInfo id="GrandFatherInfo" runat="server"></uc1:GrandFatherInfo></TD>
							</TR>
					</TABLE>
				</TD>
				<TD class="FrameBorder" width="1"></TD>
			</TR>
			<TR>
				<TD class="FrameBorder" width="1" height="1"></TD>
				<TD class="FrameBorder" height="1"></TD>
				<TD class="FrameBorder" width="1" height="1"></TD>
			</TR>
		</TABLE>
	</efundraising:Content>
</efundraising:masterpage>
