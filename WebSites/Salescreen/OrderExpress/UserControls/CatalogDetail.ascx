<%@ Register TagPrefix="uc1" TagName="CatalogItemSubList" Src="CatalogItemSubList.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.CatalogDetail" Codebehind="CatalogDetail.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="CatalogItemCategorySubList" Src="CatalogItemCategorySubList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CatalogGroupCatalogSubList" Src="CatalogGroupCatalogSubList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CatalogHeaderDetailForm" Src="CatalogHeaderDetailForm.ascx" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>

<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td>
			<asp:label id="lblCatalogTitle" runat="server" CssClass="StandardLabel"></asp:label>
			<br>
			<br>
		</td>
	</tr>
	<TR>
		<TD style="BACKGROUND-COLOR:transparent">
			<iewc:tabstrip id="TbStrp_Form" runat="server" AutoPostBack="False" SepDefaultStyle="border-bottom:solid 1px #000000; background: transparent;"
				TargetID="multPage_Form" TabDefaultStyle="border-bottom: #006699 2px solid; background-color: transparent;"
				width="700px" BackColor="LightGoldenrodYellow" >
				<iewc:Tab DefaultImageUrl="images/tabForm_GeneralInfo_off.gif" SelectedImageUrl="images/tabForm_GeneralInfo_on.gif"
					ToolTip="General Information"></iewc:Tab>
				<iewc:TabSeparator></iewc:TabSeparator>
				<iewc:Tab DefaultImageUrl="images/tabCatalog_CatalogGroup_off.gif" SelectedImageUrl="images/tabCatalog_CatalogGroup_on.gif"
					ToolTip="Catalog Group"></iewc:Tab>
				<iewc:TabSeparator></iewc:TabSeparator>
				<iewc:Tab DefaultImageUrl="images/tabCatalog_CatalogItem_off.gif" SelectedImageUrl="images/tabCatalog_CatalogItem_on.gif"
					ToolTip="Catalog Item"></iewc:Tab>
				<iewc:TabSeparator></iewc:TabSeparator>
				<iewc:Tab DefaultImageUrl="images/tabCatalog_Category_off.gif" SelectedImageUrl="images/tabCatalog_Category_on.gif"
					ToolTip="Category"></iewc:Tab>
				<iewc:TabSeparator DefaultStyle="width:100%;background-color:transparent;"></iewc:TabSeparator>
			</iewc:tabstrip>
			<iewc:multipage id="multPage_Form" style="BORDER-RIGHT: #bfbfbf 2px outset; PADDING-RIGHT: 5px; BORDER-TOP: medium none; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; BORDER-LEFT: #bfbfbf 2px outset; PADDING-TOP: 5px; BORDER-BOTTOM: #bfbfbf 2px outset"
				runat="server" Height="100%" width="700px">
				<iewc:PageView>
					<table border="0" cellpadding="0" cellspacing="0">
						<tr>
							<td width="5px">
								&nbsp;&nbsp;
							</td>
							<td>
								<asp:Label Runat="server" ID="lbl1" CssClass="NoteLabel">
								Use the General Information Template to add, modify and/or delete data below.
								</asp:Label><br>
								<br>
							</td>
						</tr>
						<tr>
							<td width="5px">
								&nbsp;&nbsp;
							</td>
							<td>
								<uc1:CatalogHeaderDetailForm id="CatalogDetailForm" runat="server"></uc1:CatalogHeaderDetailForm>
								<br>
							</td>
						</tr>
					</table>
				</iewc:PageView>
				<iewc:PageView>
					<table border="0" cellpadding="0" cellspacing="0">
						<tr>
							<td width="5px">
								&nbsp;&nbsp;
							</td>
							<td>
								<asp:Label Runat="server" ID="Label1" CssClass="NoteLabel">
								Use the List below to include/exclude the current Catalog to a Catalog Group.
								</asp:Label><br>
								<br>
							</td>
						</tr>
						<tr>
							<td width="5px">
								&nbsp;&nbsp;
							</td>
							<td>
								<uc1:CatalogGroupCatalogSubList id="CatalogGroupCatalogSubList_Ctrl" runat="server"></uc1:CatalogGroupCatalogSubList>
								<br>
								<br>
							</td>
						</tr>
					</table>
				</iewc:PageView>
				<iewc:PageView>
					<table border="0" cellpadding="0" cellspacing="0">
						<tr>
							<td width="5px">
								&nbsp;&nbsp;
							</td>
							<td>
								<asp:Label Runat="server" ID="Label2" CssClass="NoteLabel">
								Use the Exception Template to add, modify and/or delete data below.
								</asp:Label><br>
								<br>
							</td>
						</tr>
						<tr>
							<td width="5px">
								&nbsp;&nbsp;
							</td>
							<td>
								<uc1:CatalogItemSubList id="CatalogItemSubList_Ctrl" runat="server"></uc1:CatalogItemSubList>
								<br>
								<br>
							</td>
						</tr>
					</table>
				</iewc:PageView>
				<iewc:PageView>
					<table border="0" cellpadding="0" cellspacing="0">
						<tr>
							<td width="5px">
								&nbsp;&nbsp;
							</td>
							<td>
								<asp:Label Runat="server" ID="Label3" CssClass="NoteLabel">
								Use the Task Template to add, modify and/or delete data below.
								</asp:Label><br>
								<br>
							</td>
						</tr>
						<tr>
							<td width="5px">
								&nbsp;&nbsp;
							</td>
							<td>
								<uc1:CatalogItemCategorySubList id="CatalogItemCategorySubList_Ctrl" runat="server"></uc1:CatalogItemCategorySubList>
								<br>
								<br>
							</td>
						</tr>
					</table>
				</iewc:PageView>
			</iewc:multipage>
			<br>
		</TD>
	</TR>
	<tr>
		<td align="center">
			<table border="0" cellpadding="0" cellspacing="0" width="400" id="Table1">
				<tr>
					<td align="center">
						<asp:ImageButton id="imgBtnDelete" runat="server" CausesValidation="False" ImageUrl="~/images/btnDelete.gif"
							AlternateText="Delete"></asp:ImageButton>
					</td>
					<td align="center">
						<asp:ImageButton id="imgBtnSave" runat="server" CausesValidation="False" ImageUrl="~/images/btnSave.gif"
							AlternateText="Save"></asp:ImageButton>
					</td>
					<td align="center">
						<asp:HyperLink id="hypLnkCancel" runat="server" ImageUrl="~/images/btnClose.gif" NavigateUrl="javascript:window.close();">Close</asp:HyperLink>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</TABLE>
