<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ControlerProductSelectForKanata.ascx.cs" Inherits="QSPFulfillment.OrderMgt.ControlerProductSelectForKanata" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<LINK href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
<br>
<div id="divSearch" runat="server">
	<table id="Table3" cellSpacing="0" cellPadding="0" width="100%" bgColor="#cecece" border="0">
		<tr>
			<td>
				<table id="Table4" cellSpacing="1" cellPadding="2" width="100%">
					<tr>
						<td vAlign="top" height="20"><asp:label id="lblTitle2" runat="server" cssclass="CSTitle">Search</asp:label></td>
					</tr>
					<tr bgColor="#ffffff">
						<td vAlign="top">
							<table id="Table1" cellSpacing="0" cellPadding="2" width="100%" border="0">
								<tr>
									<td style="WIDTH: 101px" width="101"><asp:label id="Label1s" runat="server" cssclass="CSPlainText" Height="22px" Width="88px">Product Code</asp:label><br>
										<asp:textbox id="tbxTitleCode" runat="server" width="70"></asp:textbox></td>
									<td width="300"><asp:label id="Label3s" runat="server" cssclass="CSPlainText" Height="22px"> Product Descripton</asp:label><br>
										<asp:textbox id="tbxTitle" runat="server" width="300"></asp:textbox></td>
									<td style="WIDTH: 28px" vAlign="top" width="28"><asp:label id="Label2" runat="server" cssclass="CSPlainText" Visible="False">Term</asp:label><br>
										<table id="Table2" cellSpacing="0" cellPadding="0" border="0">
											<tr>
												<td><asp:textbox id="tbxTerm" runat="server" width="16px" Visible="False"></asp:textbox></td>
												<td style="WIDTH: 10px"><asp:rangevalidator id="RangeValidator5" runat="server" Visible="False" minimumvalue="1" maximumvalue="48"
														controltovalidate="tbxTerm" type="Integer" errormessage="Term must be between 1 and 48.">*</asp:rangevalidator></td>
											</tr>
										</table>
									</td>
									<TD style="WIDTH: 290px" vAlign="middle" noWrap colSpan="1" rowSpan="1"><asp:label id="Label4" runat="server" cssclass="CSPlainText" Height="19px" Width="72px">Catalog</asp:label><asp:dropdownlist id="ddlCatalogType" runat="server" Width="300px" AutoPostBack="True" DataTextField="program_Type"
											DataValueField="program_Type"></asp:dropdownlist></TD>
									<td vAlign="middle">
										<table style="WIDTH: 134px; HEIGHT: 24px" cellSpacing="0" cellPadding="0" border="0">
											<TR>
												<TD style="WIDTH: 89px; HEIGHT: 13px" align="center"></TD>
												<TD style="HEIGHT: 13px" align="center"></TD>
											</TR>
											<tr>
												<td style="WIDTH: 89px" align="center"><asp:button id="btnSearch" runat="server" text="Search"></asp:button>&nbsp;&nbsp;&nbsp;</td>
												<td align="center"><asp:button id="btnReset" runat="server" text="Reset"></asp:button></td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<br>
</div>
<P><cc2:datagridobject id="dtgMain" runat="server" width="871px" ShowFooter="True" autogeneratecolumns="False"
		borderwidth="1px" borderstyle="None" bordercolor="#CCCCCC" backcolor="White" cellpadding="3" searchmode="0"
		allowpaging="True"><SELECTEDITEMSTYLE BackColor="#008A8C" ForeColor="White" Font-Bold="True"></SELECTEDITEMSTYLE>
		<ITEMSTYLE ForeColor="#000066" CssClass="CSSearchResult"></ITEMSTYLE>
		<HEADERSTYLE BackColor="#006699" ForeColor="White" Font-Bold="True" CssClass="CSSearchResult"></HEADERSTYLE>
		<FOOTERSTYLE BackColor="White" ForeColor="#000066" CssClass="CSSearchResult"></FOOTERSTYLE>
		<COLUMNS>
			<ASP:TEMPLATECOLUMN>
				<ITEMTEMPLATE>
					<asp:checkbox id="chkSelect" runat="server"></asp:checkbox>
				</ITEMTEMPLATE>
			</ASP:TEMPLATECOLUMN>
			<ASP:TEMPLATECOLUMN Visible="False">
				<ITEMTEMPLATE>
					<asp:Label id=lblMagInstance runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MagPrice_instance") %>'>
					</asp:Label>
				</ITEMTEMPLATE>
			</ASP:TEMPLATECOLUMN>
			<ASP:TEMPLATECOLUMN Visible="False">
				<ITEMTEMPLATE>
					<asp:Label id="lblTransID" runat="server" Text='<%
					# DataBinder.Eval(Container, "DataItem.TransID") %>'>
					</asp:Label>
				</ITEMTEMPLATE>
			</ASP:TEMPLATECOLUMN>
			<ASP:TEMPLATECOLUMN HeaderText="Product Code">
				<ITEMTEMPLATE>
					<asp:Label id=lblProductCode runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Product_code") %>'>
					</asp:Label>
				</ITEMTEMPLATE>
			</ASP:TEMPLATECOLUMN>
			<ASP:TEMPLATECOLUMN HeaderText="Product Description">
				<ITEMTEMPLATE>
					<asp:Label id=lblTitle runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Product_sort_name") %>'>
					</asp:Label>
				</ITEMTEMPLATE>
				<FOOTERTEMPLATE>
					<asp:Label id="Label7" runat="server" Width="59px" ForeColor="#404040" Font-Bold Text="Total:"
						Font-Size="xsmall"></asp:Label>
				</FOOTERTEMPLATE>
			</ASP:TEMPLATECOLUMN>
			<ASP:TEMPLATECOLUMN Visible="False" HeaderText="Term">
				<ITEMTEMPLATE>
					<asp:Label id=lblTerm runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Term") %>'>
					</asp:Label>
				</ITEMTEMPLATE>
			</ASP:TEMPLATECOLUMN>
			<ASP:TEMPLATECOLUMN Visible="false" HeaderText="Quantity">
				<ITEMTEMPLATE>
					<asp:Label id=lblQty2 runat="server" Width="58px" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>'>
					</asp:Label>
				</ITEMTEMPLATE>
				<FOOTERTEMPLATE>
					<asp:Label id="Label8" runat="server" Width="75px" ForeColor="#404040" Font-Bold Text="" Font-Size="xsmall"></asp:Label>
				</FOOTERTEMPLATE>
			</ASP:TEMPLATECOLUMN>
			<ASP:TEMPLATECOLUMN Visible="true" HeaderText="Quantity">
				<ITEMTEMPLATE>
					<cc1:textboxinteger id=tbxQuantity runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>' errormsgregexp="The field Quantity has to be a number." required="True" errormsgrequired="The field Quantity is mandatory." columns="4">
					</cc1:textboxinteger>
					<asp:rangevalidator id="ravQuantity" runat="server" errormessage="The Quantity has to be more than 0."
						type="Integer" controltovalidate="tbxQuantity" maximumvalue="2147483647" minimumvalue="0" display="Dynamic">*</asp:rangevalidator>
				</ITEMTEMPLATE>
				<FOOTERTEMPLATE>
					<asp:Label id="lblTotalCount" runat="server" Width="75px" ForeColor="#404040" Font-Bold Text=""
						Font-Size="xsmall"></asp:Label>
				</FOOTERTEMPLATE>
			</ASP:TEMPLATECOLUMN>
			<ASP:TEMPLATECOLUMN HeaderText="Catalog Price">
				<ITEMTEMPLATE>
					<asp:Label id=lblPrice runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Catalog_Price") %>'>
					</asp:Label>
				</ITEMTEMPLATE>
			</ASP:TEMPLATECOLUMN>
			<ASP:TEMPLATECOLUMN Visible="False" HeaderText="Total Price">
				<ITEMTEMPLATE>
					<asp:Label id="lblTotalAmount" runat="server" Width="97px">Label</asp:Label>
				</ITEMTEMPLATE>
			</ASP:TEMPLATECOLUMN>
			<ASP:TEMPLATECOLUMN Visible="False" HeaderText="Language">
				<ITEMTEMPLATE>
					<asp:Label id=lblLang runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Lang") %>'>
					</asp:Label>
				</ITEMTEMPLATE>
			</ASP:TEMPLATECOLUMN>
			<ASP:TEMPLATECOLUMN HeaderText="Catalog Name">
				<ITEMTEMPLATE>
					<asp:Label id=lblCatalogName runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Catalog_Name") %>'>
					</asp:Label>
				</ITEMTEMPLATE>
			</ASP:TEMPLATECOLUMN>
			<ASP:TEMPLATECOLUMN HeaderText="Product Year">
				<ITEMTEMPLATE>
					<asp:Label id="lblProductYear" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Product_Year") %>'>
					</asp:Label>
				</ITEMTEMPLATE>
			</ASP:TEMPLATECOLUMN>
			<ASP:TEMPLATECOLUMN HeaderText="Product Season">
				<ITEMTEMPLATE>
					<asp:Label id="lblProductSeason" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Product_Season") %>'>
					</asp:Label>
				</ITEMTEMPLATE>
			</ASP:TEMPLATECOLUMN>			
			<ASP:TEMPLATECOLUMN Visible="False" HeaderText="ProductType">
				<ITEMTEMPLATE>
					<asp:Label id=lblProductType Text='<%# DataBinder.Eval(Container, "DataItem.ProductType") %>' Runat="server">
					</asp:Label>
				</ITEMTEMPLATE>
			</ASP:TEMPLATECOLUMN>
			<ASP:TEMPLATECOLUMN Visible="False" HeaderText="Price">
				<ITEMTEMPLATE>
					<cc1:textboxfloat id=tbxPrice runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EnterredPrice") %>' errormsgregexp="The field Price has to be a number." required="True" errormsgrequired="The field Price is mandatory." columns="6" maxlength="25">
					</cc1:textboxfloat>
				</ITEMTEMPLATE>
				<FOOTERTEMPLATE>
					<asp:Label id="lblTotalPrice" runat="server" Width="59px" ForeColor="#404040" Text=""></asp:Label>
				</FOOTERTEMPLATE>
			</ASP:TEMPLATECOLUMN>
			<ASP:TEMPLATECOLUMN Visible="False" HeaderText="Entered Price">
				<ITEMTEMPLATE>
					<asp:Label id="lblEnterredPrice" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EnterredPrice") %>'>
					</asp:Label>
				</ITEMTEMPLATE>
			</ASP:TEMPLATECOLUMN>
			<ASP:TEMPLATECOLUMN Visible="False" HeaderText="Total">
				<ITEMTEMPLATE>
					<asp:Label id=Label5 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EnterredPrice") %>'>
					</asp:Label>
				</ITEMTEMPLATE>
				<FOOTERTEMPLATE>
					<asp:Label id="Label6" runat="server" Width="59px" ForeColor="#404040" Font-Bold Text="" Font-Size="xsmall"></asp:Label>
				</FOOTERTEMPLATE>
			</ASP:TEMPLATECOLUMN>
			<ASP:TEMPLATECOLUMN Visible="False" HeaderText="IsDeleted">
				<ITEMTEMPLATE>
					<asp:Label id="IsDeleted" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IsDeleted") %>'>
					</asp:Label>
				</ITEMTEMPLATE>
			</ASP:TEMPLATECOLUMN>
		</COLUMNS>
		<PAGERSTYLE BackColor="White" ForeColor="#000066" CssClass="CSPager" Mode="NumericPages" HorizontalAlign="Left"></PAGERSTYLE>
	</cc2:datagridobject></P>
<P><asp:label id="lblMessage" runat="server"></asp:label></P>
