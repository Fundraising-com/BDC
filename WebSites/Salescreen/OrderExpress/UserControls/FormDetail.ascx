<%@ Register TagPrefix="uc1" TagName="Footer" Src="Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Header.ascx" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="uc1" TagName="FormHeaderDetailForm" Src="FormHeaderDetailForm.ascx" %>
<%@ Register TagPrefix="uc1" TagName="BusinessTaskForm" Src="BusinessTaskForm.ascx" %>
<%@ Register TagPrefix="uc1" TagName="BusinessExceptionForm" Src="BusinessExceptionForm.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FormVersionSubList" Src="FormVersionSubList.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.FormDetail" Codebehind="FormDetail.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="FormSectionList" Src="FormSectionList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="BusinessRuleForm" Src="BusinessRuleForm.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FormDeliveryMethodList" Src="FormDeliveryMethodList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FormOrderTypeList" Src="FormOrderTypeList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FormProfitRateList" Src="FormProfitRateList.ascx" %>

<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td style="height: 86px">
		    <table border=0 cellpadding=0 cellspacing=0 width="100%" >
		        <tr>
		            <td >
		                <asp:label id="lblFormTitle" runat="server" CssClass="StandardLabel"></asp:label>
			            <asp:Image  style="left: 500px; position: absolute; top: 80px" ID=imgForm runat=server />
		            	<br>			
		            </td>
		                      
		        </tr>
		    </table>
			<br>
		</td>
	</tr>
	<TR>
		<TD style="BACKGROUND-COLOR:transparent">
			<iewc:tabstrip id="TbStrp_Form" runat="server" AutoPostBack="False" SepDefaultStyle="border-bottom:solid 1px #000000; background: transparent;"
				TargetID="multPage_Form" TabDefaultStyle="border-bottom: #006699 2px solid; background-color: transparent;" BackColor="#f9f0c7" Width="770px"
				>
				<iewc:Tab DefaultImageUrl="images/tabForm_GeneralInfo_off.gif" SelectedImageUrl="images/tabForm_GeneralInfo_on.gif"
					ToolTip="General Information"></iewc:Tab>
				<iewc:TabSeparator></iewc:TabSeparator>
				<iewc:Tab DefaultImageUrl="images/tabForm_BizRule_off.gif" SelectedImageUrl="images/tabForm_BizRule_on.gif"></iewc:Tab>
				<iewc:TabSeparator></iewc:TabSeparator>
				<iewc:Tab DefaultImageUrl="images/tabForm_Exception_off.gif" SelectedImageUrl="images/tabForm_Exception_on.gif"
					ToolTip="Exception"></iewc:Tab>
				<iewc:TabSeparator></iewc:TabSeparator>
				<iewc:Tab DefaultImageUrl="images/tabForm_BizTask_off.gif" SelectedImageUrl="images/tabForm_BizTask_on.gif"
					ToolTip="Task"></iewc:Tab>
				<iewc:TabSeparator></iewc:TabSeparator>
				<iewc:Tab DefaultImageUrl="images/tabCatalog_CatalogGroup_off.gif" SelectedImageUrl="images/tabCatalog_CatalogGroup_on.gif"
					ToolTip="Catalog Group"></iewc:Tab>
				<iewc:TabSeparator></iewc:TabSeparator>
				<iewc:Tab DefaultImageUrl="images/tabOtherInformation_off.gif" SelectedImageUrl="images/tabOtherInformation_on.gif"
					ToolTip="Other Information"></iewc:Tab>
				<iewc:TabSeparator></iewc:TabSeparator>
				<iewc:Tab DefaultImageUrl="images/tabForm_Version_off.gif" SelectedImageUrl="images/tabForm_Version_on.gif"
					ToolTip="Version"></iewc:Tab>
				<iewc:TabSeparator DefaultStyle="width:100%;background-color:transparent;"></iewc:TabSeparator>
			</iewc:tabstrip>
			<iewc:multipage id="multPage_Form" style="BORDER-RIGHT: #bfbfbf 2px outset; PADDING-RIGHT: 5px; BORDER-TOP: medium none; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; BORDER-LEFT: #bfbfbf 2px outset; PADDING-TOP: 5px; BORDER-BOTTOM: #bfbfbf 2px outset"
				runat="server" Height="100%" width="750px">
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
								<uc1:FormHeaderDetailForm id="FormDetailForm" runat="server"></uc1:FormHeaderDetailForm>
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
								Use the Business Rule Template to add, modify and/or delete data below.
								</asp:Label><br>
								<br>
							</td>
						</tr>
						<tr>
							<td width="5px">
								&nbsp;&nbsp;
							</td>
							<td>
								<br>
								<uc1:BusinessRuleForm id="BusinessRuleList" runat="server"></uc1:BusinessRuleForm>
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
								<br>
								<uc1:BusinessExceptionForm id="BusinessExceptionList" runat="server"></uc1:BusinessExceptionForm>
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
								<br>
								<uc1:BusinessTaskForm id="BusinessTaskList" runat="server"></uc1:BusinessTaskForm>
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
								<asp:Label Runat="server" ID="Label5" CssClass="NoteLabel">
								Use the Section Template to add, modify and/or delete data below.
								</asp:Label><br>
								<br>
							</td>
						</tr>
						<tr>
							<td width="5px">
								&nbsp;&nbsp;
							</td>
							<td>
								<br>
								<uc1:FormSectionList id="FormSectionListCtrl" runat="server"></uc1:FormSectionList>
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
								<asp:Label Runat="server" ID="Label6" CssClass="NoteLabel">
								Use the Delivery Method List below to define available option.
								</asp:Label><br>
							</td>
						</tr>
						<tr>
							<td width="5px">
								&nbsp;&nbsp;
							</td>
							<td>
								<uc1:FormDeliveryMethodList id="FormDeliveryMethodListCtrl" runat="server"></uc1:FormDeliveryMethodList>
								<br>
							</td>
						</tr>
						<tr>
							<td width="5px">
								&nbsp;&nbsp;
							</td>
							<td>
								<asp:Label Runat="server" ID="Label7" CssClass="NoteLabel">
								Use the Order Type List below to define available option.
								</asp:Label>
								<br>
							</td>
						</tr>
						<tr>
							<td width="5px">
								&nbsp;&nbsp;
							</td>
							<td>
								<uc1:FormOrderTypeList id="FormOrderTypeListCtrl" runat="server"></uc1:FormOrderTypeList>
								<br>
							</td>
						</tr>
						<tr>
							<td width="5px">
								&nbsp;&nbsp;
							</td>
							<td>
								<asp:Label Runat="server" ID="Label8" CssClass="NoteLabel">
								Use the profit Rate List below to define available option.
								</asp:Label>
								<br>
							</td>
						</tr>
						<tr>
							<td width="5px">
								&nbsp;&nbsp;
							</td>
							<td>
								<uc1:FormProfitRateList id="FormProfitRateListCtrl" runat="server"></uc1:FormProfitRateList>
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
								<asp:Label Runat="server" ID="Label4" CssClass="NoteLabel">
								This list display all version of this Business Form.
								</asp:Label><br>
								<br>
							</td>
						</tr>
						<tr>
							<td width="5px">
								&nbsp;&nbsp;
							</td>
							<td>
								<br>
								<uc1:FormVersionSubList id="VersionList" runat="server"></uc1:FormVersionSubList>
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
		<td align="center"><br>
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
			<table border="0" cellpadding="0" cellspacing="0" width="400" id="Table2">
				<tr>
					<td align="center">
						<asp:ImageButton id="imgBtnCreateNewVersion" runat="server" CausesValidation="False" ImageUrl="~/images/btnCreateNewVersion.gif"
							AlternateText="Create New Version" OnClick="imgBtnCreateNewVersion_Click" Visible="False"></asp:ImageButton>
					</td>
					<td align="center">
						<asp:ImageButton id="imgBtnCopyAsNewForm" runat="server" CausesValidation="False" ImageUrl="~/images/btnCopyAsNewForm.gif"
							AlternateText="Copy As New Form" OnClick="imgBtnCopyAsNewForm_Click" Visible="False"></asp:ImageButton>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</TABLE>
