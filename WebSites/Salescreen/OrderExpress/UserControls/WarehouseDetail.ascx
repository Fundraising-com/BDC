<%@ Reference Control="WarehouseDetailInfo.ascx" %>
<%@ Reference Control="~/UserControls/ToolBar.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.WarehouseDetail" Codebehind="WarehouseDetail.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="WarehouseBusinessCalendarForm" Src="WarehouseBusinessCalendarForm.ascx" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="uc1" TagName="WarehouseForm" Src="WarehouseForm.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ToolBar" Src="~/UserControls/ToolBar.ascx" %>

<table border="0" cellpadding="0" cellspacing="0" width="100%" id="Table1">
	<TR>
		<TD style="BACKGROUND-COLOR:transparent">
			<iewc:tabstrip id="TbStrp_Warehouse" runat="server" AutoPostBack="False" SepDefaultStyle="border-bottom:solid 1px #000000; background: transparent;"
				TargetID="multPage_Warehouse" TabDefaultStyle="border-bottom: #006699 2px solid; background-color: transparent;"
				width="700px" BackColor="LightGoldenrodYellow">
				<iewc:Tab DefaultImageUrl="images/tabForm_GeneralInfo_off.gif" SelectedImageUrl="images/tabForm_GeneralInfo_on.gif"
					ToolTip="General Information"></iewc:Tab>
				<iewc:TabSeparator></iewc:TabSeparator>
				<iewc:Tab DefaultImageUrl="images/tabForm_BizCalendar_off.gif" SelectedImageUrl="images/tabForm_BizCalendar_on.gif"></iewc:Tab>
				<iewc:TabSeparator DefaultStyle="width:100%;background-color:transparent;"></iewc:TabSeparator>
			</iewc:tabstrip>
			<iewc:multipage id="multPage_Warehouse" style="BORDER-RIGHT: #bfbfbf 2px outset; PADDING-RIGHT: 5px; BORDER-TOP: medium none; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; BORDER-LEFT: #bfbfbf 2px outset; PADDING-TOP: 5px; BORDER-BOTTOM: #bfbfbf 2px outset"
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
								<uc1:WarehouseForm id="WarehouseForm_Ctrl" runat="server"></uc1:WarehouseForm>
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
								Use the Business Calendar Template to add, modify and/or delete data below.
								</asp:Label><br>
								<br>
							</td>
						</tr>
						<tr>
							<td width="5px">
								&nbsp;&nbsp;
							</td>
							<td>
								<uc1:WarehouseBusinessCalendarForm id="WarehouseBizCal" runat="server"></uc1:WarehouseBusinessCalendarForm>
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
		<td>
			<br>
		</td>
	</tr>
	<tr>
		<td align="center">
			<uc1:ToolBar id="QSPToolBar" runat="server"></uc1:ToolBar>
		</td>
	</tr>
</table>
<script language="javascript">
	
	var x = 800;//window.document.body.clientHeight;// .offsetWidth;
	var y = 850;//window.document.body.clientWidth; //.offsetHeight;
	window.resizeTo(x,y);	
</script>
