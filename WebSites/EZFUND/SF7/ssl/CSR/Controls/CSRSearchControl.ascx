<%@ Register TagPrefix="uc1" TagName="CAttributeControl" Src="CSRAttributes.ascx" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="CSRSearchControl.ascx.vb" Inherits="StoreFront.StoreFront.CSRSearchControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD class="Content" noWrap align="left"><b>Search For:&nbsp;
				<asp:textbox id="SimpleKeyword" CssClass="Inputs" runat="server" Columns="20"></asp:textbox>&nbsp;&nbsp;In:&nbsp;&nbsp;
				<asp:dropdownlist id="SimpleCategory" CssClass="Inputs" runat="server" DataTextField="Name" DataValueField="ID"
					DataMember="Categories">
					<asp:ListItem Value="-1" Selected="True">All Categories</asp:ListItem>
				</asp:dropdownlist>&nbsp;</b>
			<asp:linkbutton id="btnSearch" Runat="server">
				<asp:Image BorderWidth="0" ImageUrl="../images/icon_go.gif" ID="imgSearch" Runat="server" AlternateText="Search"></asp:Image>
			</asp:linkbutton></TD>
	</TR>
</TABLE>
<br>
<asp:panel id="ResultInfo" CssClass="Content" Runat="server"><B>Search Results</B><BR>Search for: 
<asp:Label id="lblKeyword" runat="server"></asp:Label>&nbsp;in 
<asp:Label id="lblCategory" runat="server">Category:</asp:Label>
<asp:Label id="lblCategoryName" runat="server" CssClass="Content"></asp:Label>&nbsp;returned 
<asp:label id="lblCount" runat="server" CssClass="Content"></asp:label>&nbsp; 
<asp:Label id="lblProducts" CssClass="Content" Runat="server">Products</asp:Label><BR></asp:panel>
<asp:datagrid id="dgSearch" runat="server" AllowPaging="True" AutoGenerateColumns="False" ShowHeader="False"
	CellPadding="0" Width="100%" BorderWidth="0px">
	<Columns>
		<asp:TemplateColumn>
			<ItemTemplate>
				<TABLE id="ResultTable" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
					<TR id="SeperatorRow">
						<TD class="ContentTableHeader" colSpan="22" height="1"><IMG height="1" src="images/clear.gif"></TD>
					</TR>
					<TR>
						<TD class="ContentTable" style="WIDTH: 1px" width="1"><IMG height="1" src="images/clear.gif"></TD>
						<TD class="Content" width="100%" colSpan="20">&nbsp;</TD>
						<TD class="ContentTable" style="WIDTH: 1px" width="1"><IMG height="1" src="images/clear.gif"></TD>
					</TR>
					<TR id="ContentRow">
						<TD class="ContentTable" style="WIDTH: 1px" width="1"><IMG height="1" src="images/clear.gif"></TD>
						<TD class="Content">&nbsp;</TD>
						<TD class="Content" vAlign="top" align="left" width="1%">
							<asp:Image ID="smallImage" Visible=False Runat=server ImageUrl='<%# "../" & DataBinder.Eval(Container.DataItem,"SmallImage")%>'>
							</asp:Image>
						</TD>
						<TD class="Content">&nbsp;</TD>
						<TD class="Content" vAlign="top" align="left" width="1%">
							<asp:Label id="lblProdCode" runat="server">
								<%#DataBinder.Eval(Container.DataItem,"ProductCode")%>
							</asp:Label></TD>
						<TD class="Content">&nbsp;&nbsp;</TD>
						<TD class="Content" vAlign="top">
							<asp:Label id="lblProdName" runat="server">
								<%#DataBinder.Eval(Container.DataItem,"Name")%>
							</asp:Label></TD>
						<TD class="Content">&nbsp;</TD>
						<TD class="Content" vAlign="top" align="left" width="1%">
							<asp:Label Visible="False" id="Label1" runat="server">
								<%#DataBinder.Eval(Container.DataItem,"ShortDescription")%>
							</asp:Label></TD>
						<TD class="Content">&nbsp;</TD>
						<TD class="Content" vAlign="top" align="left" width="1%">
							<asp:Label Visible="False" id="Label2" runat="server">
								<%#DataBinder.Eval(Container.DataItem,"Description")%>
							</asp:Label></TD>
						<TD class="Content">&nbsp;&nbsp;</TD>
						<TD class="Content" vAlign="top" width="50%">
							<uc1:CAttributeControl id="CAttributeControl1" runat="server"></uc1:CAttributeControl>
						</TD>
						<TD class="Content">&nbsp;</TD>
						<td class="Content" vAlign="top">
							<asp:Label ID="lblPrice" Runat="server">Price:&nbsp;</asp:Label>
							<asp:TextBox ID="NewPrice" Text='' Runat="server" Width="60" Enabled='<%# AllowOverridePricing() %>'></asp:TextBox>
							<input type="hidden" id="OldPrice" runat="server" value=''>
						</td>
						<TD class="Content">&nbsp;</TD>
						<td class="Content" vAlign="top">
							<asp:Label ID="lblQuantity" Runat="server">Quantity:&nbsp;</asp:Label>
							<asp:TextBox ID="NewQuantity" Runat="server" Width="30" Text="1"></asp:TextBox>
						</td>
						<TD>&nbsp;&nbsp;</TD>
						<TD vAlign="top" align="right" width="20%">
							<asp:LinkButton id="btnAddToCart" onclick="AddCart" Runat="server" CommandName='<%# DataBinder.Eval(Container.DataItem,"ProductCode") %>'>
								<asp:Image BorderWidth="0" ID="imgAddToCart" runat="server" AlternateText="Add To Cart" ImageUrl="../images/add.jpg"></asp:Image>
							</asp:LinkButton></TD>
						<TD class="Content">&nbsp;</TD>
						<TD class="Content">&nbsp;</TD>
						<TD class="ContentTable" style="WIDTH: 1px" width="1"><IMG height="1" src="images/clear.gif"></TD>
					</TR>
					<TR>
						<TD class="ContentTable" style="WIDTH: 1px" width="1"><IMG height="1" src="images/clear.gif"></TD>
						<TD class="Content" colSpan="20">&nbsp;</TD>
						<TD class="ContentTable" style="WIDTH: 1px" width="1"><IMG height="1" src="images/clear.gif"></TD>
					</TR>
					<TR id="FooterRow">
						<TD class="ContentTableHeader" colSpan="22" height="1"><IMG height="1" src="images/clear.gif"></TD>
					</TR>
				</TABLE>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
	<PagerStyle NextPageText="Next" PrevPageText="Previous" HorizontalAlign="Right" Position="TopAndBottom"
		CssClass="Content" Mode="NumericPages"></PagerStyle>
</asp:datagrid>
