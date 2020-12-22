<%@ Reference Control="~/UserControls/ToolBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ToolBar" Src="~/UserControls/ToolBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WarehouseInventory" Src="WarehouseInventory.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.WarehouseDetailInfo" Codebehind="WarehouseDetailInfo.ascx.cs" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="uc1" TagName="WarehouseInfo" Src="WarehouseInfo.ascx" %>

<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<td align="left">
			<iewc:tabstrip id="TbStrp_Form" runat="server" AutoPostBack="False" SepDefaultStyle="border-bottom:solid 1px #000000; background: transparent;"
				TargetID="multPage_Form" TabDefaultStyle="border-bottom: #006699 2px solid; background-color: transparent;"
				width="600px" BackColor="LightGoldenrodYellow">
				<iewc:Tab DefaultImageUrl="images/tabForm_GeneralInfo_off.gif" SelectedImageUrl="images/tabForm_GeneralInfo_on.gif"
					ToolTip="General Information"></iewc:Tab>
				<iewc:TabSeparator></iewc:TabSeparator>
				<iewc:Tab DefaultImageUrl="images/tabForm_ProductInventory_off.gif" SelectedImageUrl="images/tabForm_ProductInventory_on.gif"
					ToolTip="General Information"></iewc:Tab>
				<iewc:TabSeparator DefaultStyle="width:100%;background-color:transparent;"></iewc:TabSeparator>
			</iewc:tabstrip>
			<iewc:multipage id="multPage_Form" style="BORDER-RIGHT: #bfbfbf 2px outset; PADDING-RIGHT: 5px; BORDER-TOP: medium none; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; BORDER-LEFT: #bfbfbf 2px outset; PADDING-TOP: 5px; BORDER-BOTTOM: #bfbfbf 2px outset"
				runat="server" Height="100%" width="600px">
				<iewc:PageView id="WarehouseInfoPage">
					<uc1:WarehouseInfo id="WarehouseInfo_Ctrl" runat="server"></uc1:WarehouseInfo>
				</iewc:PageView>
				<iewc:PageView id="InventoryPage" width="100%">
					<uc1:WarehouseInventory id="CtrlWarehouseInventory" runat="server"></uc1:WarehouseInventory>
				</iewc:PageView>
			</iewc:multipage><br>
		</td>
	</TR>
	<tr>
		<td align="center"><uc1:toolbar id="QSPToolBar" runat="server"></uc1:toolbar></td>
	</tr>
</TABLE>
