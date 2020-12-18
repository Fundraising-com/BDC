<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.FavoriteList" Codebehind="FavoriteList.ascx.cs" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>

<TABLE id="Table5" cellSpacing="0" cellPadding="0" align="left" border="0">
    <tr id="trUserSelector" runat=server>
        <td>
                <TABLE id="Table6" cellSpacing="0" cellPadding="0" width="400" border="0">
                    <tr valign=middle id="FMSelector" runat=server align=right>
					    <td style="height: 45px"><asp:label id="Label55" runat="server" CssClass="ModuleSearchText" >Field&nbsp;Sales&nbsp;Manager:
							</asp:label></td>
						<TD colspan=4 valign=middle style="height: 45px">												
	                        <TABLE id="Table4" cellSpacing="0" cellPadding="0" border="0">
		                        <TR>
			                        <TD><asp:textbox id="txtFMID" runat="server" Width="50px" Enabled="True" ></asp:textbox></TD>
			                        <TD><asp:comparevalidator id="CompValFMID" runat="server" CssClass="LabelError" ControlToValidate="txtFMID"
					                        ErrorMessage="The FM ID is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator></TD>
			                        <TD>&nbsp;
				                        <asp:textbox id="txtFMName" runat="server" Width="230px" Enabled="True" ></asp:textbox></TD>
			                        <td><asp:requiredfieldvalidator id="ReqFldVal_FMID" runat="server" CssClass="LabelError" Enabled="False" ControlToValidate="txtFMID"
					                        ErrorMessage="The FSM is required.">*</asp:requiredfieldvalidator></td>
			                        <TD align="right" valign="top"><asp:imagebutton id="imgBtnSelectFM" runat="server" ImageUrl="~/images/BtnSelect.gif" CausesValidation="False"></asp:imagebutton></TD>
			                        <TD vAlign="bottom" align="right"><asp:imagebutton id="imgBtnRefresh" runat="server" CausesValidation="False" ImageUrl="~/images/BtnRefresh.gif" OnClick="imgBtnRefresh_Click"></asp:imagebutton></TD>
		                        </TR>
	                        </TABLE>
						</TD>
					</tr>
				    
			    </TABLE>
        </td>
    </tr>
	<tr>
		<td><BR>
			<TABLE cellSpacing="0" cellPadding="0" width="400" border="0">
				<TR>
					<TD align="left"><asp:label id="lblCurrentIndex" runat="server" CssClass="CurrentPageIndexLabel">Page 1 of 1</asp:label></TD>
					<td align="right"><asp:label id="Labelsss4" runat="server" CssClass="FilterNoteDesc">Click on Column Headings to Resort Data.</asp:label></td>
				</TR>
			</TABLE>
		</td>
	</tr>
	<TR>
		<TD><!--DataGrid  --><cc2:sorteddatagrid id=dtgLogo runat="server" SearchMode="0" width="700px" PageSize="10" AllowPaging="True" AllowSorting="True" CellPadding="3" BackColor="White" BorderWidth="1px" BorderStyle="None" BorderColor="#CCCCCC" DataSource="<%# DVLogo %>" AutoGenerateColumns="False" ShowFooter="True" Font-Size="10pt"><SELECTEDITEMSTYLE CssClass="SelectedItemStyle"></SELECTEDITEMSTYLE>
				<ALTERNATINGITEMSTYLE CssClass="AlternatingItemStyle_off"></ALTERNATINGITEMSTYLE>
				<ITEMSTYLE CssClass="ItemStyle_off"></ITEMSTYLE>
				<HEADERSTYLE CssClass="HeaderItemStyle" ForeColor="White" Wrap="False"></HEADERSTYLE>
				<FOOTERSTYLE CssClass="FooterItemStyle"></FOOTERSTYLE>
				<COLUMNS>
					<ASP:TEMPLATECOLUMN>
						<ITEMSTYLE Wrap="False" Width="100px"></ITEMSTYLE>
						<ITEMTEMPLATE>
<ASP:IMAGEBUTTON id="imgBtnDetail" runat="server" ImageUrl="" CommandName="Select" CausesValidation="False" Width="100" __designer:wfdid="w1"></ASP:IMAGEBUTTON> 
</ITEMTEMPLATE>
					</ASP:TEMPLATECOLUMN>
					<ASP:TEMPLATECOLUMN HeaderText="ID" SortExpression="logo_id">
						<ITEMTEMPLATE>
							<asp:Label id=lblID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.logo_id") %>'>
							</asp:Label>
						
</ITEMTEMPLATE>
					</ASP:TEMPLATECOLUMN>
					<ASP:TEMPLATECOLUMN HeaderText="Name" SortExpression="logo_name">
						<ITEMTEMPLATE>
							<asp:Label id=Name runat="server" Text='<%# ((String)DataBinder.Eval(Container, "DataItem.logo_name")).Replace(" ","&nbsp;") %>'>
							</asp:Label>
						
</ITEMTEMPLATE>
					</ASP:TEMPLATECOLUMN>
					<ASP:TEMPLATECOLUMN HeaderText="Description" SortExpression="Description">
						<ITEMTEMPLATE>
							<asp:Label id=Description runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.description") %>'>
							</asp:Label>
						
</ITEMTEMPLATE>
					</ASP:TEMPLATECOLUMN>
					<ASP:TEMPLATECOLUMN HeaderText="FSM&#160;ID" SortExpression="FM_ID">
						<ITEMTEMPLATE>
							<CENTER>
								<asp:Label id=lblFM_ID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FM_ID") %>'>
								</asp:Label></CENTER>
						
</ITEMTEMPLATE>
					</ASP:TEMPLATECOLUMN>
					<ASP:TEMPLATECOLUMN HeaderText="National">
						<ITEMTEMPLATE>
							<CENTER>
								<asp:CheckBox id=chkNational Runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container, "DataItem.IsNational")) %>'>
								</asp:CheckBox></CENTER>
						
</ITEMTEMPLATE>
						<HEADERSTYLE Width="10px"></HEADERSTYLE>
					</ASP:TEMPLATECOLUMN>
					<ASP:TEMPLATECOLUMN HeaderText="Deleted" visible="False">
						<ITEMTEMPLATE>
							<asp:CheckBox id=chkArchived Runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.deleted")%>'>
							</asp:CheckBox>
						
</ITEMTEMPLATE>
						<HEADERSTYLE Width="10px"></HEADERSTYLE>
					</ASP:TEMPLATECOLUMN>
					<ASP:TEMPLATECOLUMN>
					    <ITEMSTYLE Width="50px"></ITEMSTYLE>
						<ITEMTEMPLATE>
						    <center>
<ASP:IMAGEBUTTON id="imgBtnRemoveFromFavorite" runat="server" ImageUrl="~/images/BtnRemoveFromFavorite.gif" CommandName="RemoveFromFavorite" CausesValidation="False" __designer:wfdid="w3">
								</ASP:IMAGEBUTTON>
								<asp:Label id="lblDefaultFavorite" runat="server" Text='From&nbsp;QSP&nbsp;Favorites'></asp:Label>
                                    </center>
</ITEMTEMPLATE>
					</ASP:TEMPLATECOLUMN>
				</COLUMNS>
				<PAGERSTYLE CssClass="PagerItemStyle" Mode="NumericPages"></PAGERSTYLE>
			</cc2:sorteddatagrid></TD>
	</TR>
	<TR>
		<TD>
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<td><br>
						<asp:label id="lblTotal" runat="server" CssClass="TotalListLabel">
							Number of Logo(s):
						</asp:label></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>
