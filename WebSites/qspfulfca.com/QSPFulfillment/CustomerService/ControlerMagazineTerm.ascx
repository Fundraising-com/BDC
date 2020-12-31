<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ControlerMagazineTerm.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerMagazineTerm" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<br>
<div id="divSearch" runat="server">
	<TABLE id="Table3" width="100%" cellSpacing="0" cellPadding="0" bgColor="#cecece" border="0">
		<TR>
			<TD>
				<TABLE id="Table4" width="100%" cellSpacing="1" cellPadding="2">
					<TR>
						<TD vAlign="top" height="20"><asp:Label Runat="server" ID="lblTitle2" CssClass="CSTitle">Search</asp:Label>
						</TD>
					</TR>
					<TR bgColor="#ffffff">
						<TD vAlign="top">
							<TABLE id="Table1" cellSpacing="0" cellPadding="2" border="0" width="100%">
								<TR>
									<TD width="70"><asp:label id="Label1s" runat="server" CssClass="CSPlainText">Title Code</asp:label><br>
										<asp:textbox id="tbxTitleCode" runat="server" Width="70"></asp:textbox></TD>
									<TD width="300"><asp:label id="Label3s" runat="server" CssClass="CSPlainText">Title</asp:label><br>
										<asp:textbox id="tbxTitle" runat="server" Width="300"></asp:textbox></TD>
									<TD width="60" valign="top">
										<asp:label id="Label2" CssClass="CSPlainText" runat="server">Term</asp:label><br>
										<TABLE id="Table2" cellSpacing="0" cellPadding="0" border="0">
											<TR>
												<TD>
													<asp:textbox id="tbxTerm" runat="server" Width="40"></asp:textbox></TD>
												<TD style="WIDTH: 10px">
													<asp:RangeValidator id="RangeValidator5" runat="server" ErrorMessage="Term must be between 1 and 48."
														Type="Integer" ControlToValidate="tbxTerm" MaximumValue="48" MinimumValue="1">*</asp:RangeValidator>
												</TD>
											</TR>
										</TABLE>
									</TD>
									<TD valign="middle">
										<TABLE cellSpacing="0" cellPadding="0" border="0">
											<TR>
												<TD align="center"><asp:button id="btnSearch" runat="server" Text="Search"></asp:button>&nbsp;&nbsp;&nbsp;</TD>
												<TD align="center"><INPUT onclick="Reset('divSearch')" type="button" value="Reset"></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
	</TABLE>
	<br>
</div>
<asp:label id="lblMessage" runat="server"></asp:label>
<cc2:datagridobject id="dtgMain" runat="server" AllowPaging="True" SearchMode="0" width="100%" CellPadding="3"
	BackColor="White" AllowPagging="true" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
	AutoGenerateColumns="False">
	<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
	<ItemStyle ForeColor="#000066" CssClass="CSSearchResult"></ItemStyle>
	<HeaderStyle Font-Bold="True" ForeColor="White" CssClass="CSSearchResult" BackColor="#006699"></HeaderStyle>
	<FooterStyle ForeColor="#000066" CssClass="CSSearchResult" BackColor="White"></FooterStyle>
	<Columns>
		<asp:TemplateColumn Visible="False">
			<ItemTemplate>
				<asp:Label id=lblMagInstance runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MagPrice_instance") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Title Code">
			<ItemTemplate>
				<asp:Label id=lblProductCode runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Product_code") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Title">
			<ItemTemplate>
				<asp:Label id=lblTitle runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Product_sort_name") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Term">
			<ItemTemplate>
				<asp:Label id=lblTerm runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Term") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Price">
			<ItemTemplate>
				<asp:Label id=lblPrice runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Price") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Language">
			<ItemTemplate>
				<asp:Label id="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.lang") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Catalog Name">
			<ItemTemplate>
				<asp:Label id="Label5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Catalog_Name") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="ProductType">
			<ItemTemplate>
				<asp:Label ID="lblProductType" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ProductType") %>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="New/Renew">
			<ItemTemplate>
				<asp:DropDownList id="ddlNewRenew" runat="server">
					<asp:ListItem Value="S">Select</asp:ListItem>
					<asp:ListItem Value="N">New</asp:ListItem>
					<asp:ListItem Value="R">Renew</asp:ListItem>
				</asp:DropDownList>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="true" HeaderText="">
			<ItemTemplate>
				<asp:LinkButton CausesValidation="True" Runat="server" CommandName="Select">Select</asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
	<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" CssClass="CSPager"
		Mode="NumericPages"></PagerStyle>
</cc2:datagridobject>
