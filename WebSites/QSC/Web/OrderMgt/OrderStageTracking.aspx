<%@ Register TagPrefix="dbwc" Namespace="DBauer.Web.UI.WebControls" Assembly="DBauer.Web.UI.WebControls.HierarGrid" %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../Common/DateEntry.ascx" %>
<%@ Page language="c#" Codebehind="OrderStageTracking.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.OrderMgt.OrderStageTracking" %>
<%@ Register TagPrefix="uc1" TagName="FieldManagerDDL" Src="../Common/FieldManagerDDL.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment"  %>
<%@ Register TagPrefix="uc3" TagName="OrderQualifier" Src="UC/OrderQualifier.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>OrderStageTracking</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	    <style type="text/css">
            .style1
            {
                width: 148px;
            }
            .style2
            {
                width: 173px;
            }
            .style3
            {
                width: 155px;
            }
        </style>
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" --><asp:label id="lblOrderStageTracking" style="Z-INDEX: 100; LEFT: 40px; POSITION: absolute; TOP: 40px"
				runat="server" Font-Names="Verdana" Font-Bold="True" Width="232px">Order Stage Tracking</asp:label>
			<TABLE id="Table4" style="Z-INDEX: 105; LEFT: 40px; WIDTH: 848px; POSITION: absolute; TOP: 240px; HEIGHT: 24px"
				cellSpacing="1" cellPadding="1" width="848" border="0">
				<TR>
					<TD align="center"><asp:label id="lblErrorMessage" runat="server" forecolor="Red" font-names="Verdana" font-size="XX-Small"
							width="328px" font-bold="True"></asp:label></TD>
				</TR>
			</TABLE>
			<TABLE id="Table3" style="Z-INDEX: 102; LEFT: 40px; WIDTH: 848px; POSITION: absolute; TOP: 72px; HEIGHT: 24px"
				cellSpacing="1" cellPadding="1" width="848" bgColor="gainsboro" border="0">
				<TR>
					<TD><asp:label id="lblSearch" runat="server" font-names="Verdana" font-size="X-Small" width="112px"
							font-bold="True" height="8px">Search</asp:label></TD>
				</TR>
			</TABLE>
			<TABLE id="Table1" style="Z-INDEX: 103; LEFT: 40px; WIDTH: 848px; POSITION: absolute; TOP: 96px; HEIGHT: 144px"
				cellSpacing="1" cellPadding="1" width="848" border="0" frame="box">
				<TR>
					<TD style="WIDTH: 237px; HEIGHT: 19px"><asp:label id="lblGroupId" runat="server" Font-Names="Verdana" Font-Bold="True" Font-Size="XX-Small">Group ID</asp:label></TD>
					<TD style="HEIGHT: 19px" colspan="2"><asp:label id="lblGroupname" runat="server" Font-Names="Verdana" Font-Bold="True" Font-Size="XX-Small">Group Name</asp:label></TD>
					<TD style="HEIGHT: 19px"><asp:label id="lblCAId" runat="server" Font-Names="Verdana" Font-Bold="True" Font-Size="XX-Small">Campaign ID</asp:label></TD>
					<TD style="HEIGHT: 19px">&nbsp;</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 237px; HEIGHT: 15px"><asp:textbox id="tbGroupId" runat="server" Width="112px"></asp:textbox></TD>
					<TD style="HEIGHT: 15px" colspan="2"><asp:textbox id="tbGroupName" runat="server" Width="216px"></asp:textbox></TD>
					<TD style="HEIGHT: 15px"><asp:textbox id="tbCAId" runat="server" Width="96px"></asp:textbox></TD>
					<TD style="HEIGHT: 15px">&nbsp;</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 237px; HEIGHT: 15px"><asp:label id="lblFM" runat="server" Font-Names="Verdana" Font-Bold="True" Font-Size="XX-Small">Field Manager</asp:label></TD>
					<TD style="HEIGHT: 15px" colspan="2"><asp:label id="lblDateFrom" runat="server" Font-Names="Verdana" Font-Bold="True" Font-Size="XX-Small">Date From</asp:label></TD>
					<TD style="HEIGHT: 15px"><asp:label id="lblDateTo" runat="server" Font-Names="Verdana" Font-Bold="True" Font-Size="XX-Small">Date To</asp:label></TD>
					<TD style="HEIGHT: 15px">&nbsp;</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 237px; HEIGHT: 22px"><uc1:fieldmanagerddl id="ucFMddl" runat="server"></uc1:fieldmanagerddl><asp:label id="lblLoggedFMId" runat="server" Font-Names="Verdana" Font-Bold="True" Width="60px"
							Font-Size="XX-Small"></asp:label></TD>
					<TD style="HEIGHT: 22px" colspan="2"><uc1:dateentry id="ucDateFrom" runat="server"></uc1:dateentry></TD>
					<TD style="HEIGHT: 22px"><uc1:dateentry id="ucDateTo" runat="server"></uc1:dateentry></TD>
					<TD style="HEIGHT: 22px">&nbsp;</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 237px; HEIGHT: 11px"><asp:label id="lblOrdId" runat="server" Font-Names="Verdana" Font-Bold="True" Font-Size="XX-Small">Order ID</asp:label></TD>
					<TD style="HEIGHT: 11px" colspan="2"><asp:label id="lblStatus" runat="server" Font-Names="Verdana" Font-Bold="True" Font-Size="XX-Small">Status</asp:label></TD>
					<TD style="HEIGHT: 11px"><asp:label id="lblOrderQualifier" runat="server" 
                            Font-Names="Verdana" Font-Bold="True" Font-Size="XX-Small">Order Qualifier</asp:label></TD>
					<TD style="HEIGHT: 11px"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 237px"><asp:textbox id="tbOrderId" runat="server" Width="112px"></asp:textbox></TD>
					<TD align="left" rowSpan="1" class="style1"><asp:dropdownlist id="ddlStagingStatus" runat="server" Width="136px" DataValueField="instance" DataTextField="Description"
							AutoPostBack="False"></asp:dropdownlist></TD>
					<TD align="left" rowSpan="1" class="style2">
                        <asp:CheckBox ID="ShowOrdersPastStageCheckBox" runat="server" Checked="True" 
                            Text="Include orders past this Stage" />
                    </TD>
                    <TD>
					    <uc3:orderqualifier id="ucOrderQualifier" runat="server" AllQualifiersOption="true"></uc3:orderqualifier>
                    </TD>
					<TD class="style3"><asp:button id="pbSearch" runat="server" Width="72px" Text="Search"></asp:button><asp:button id="pbReset" runat="server" Width="80px" Text="Reset"></asp:button></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 237px; HEIGHT: 11px"><asp:label id="ProductTypeLabel" runat="server" Font-Names="Verdana" Font-Bold="True" Font-Size="XX-Small">Product Type</asp:label></TD>
					<TD style="HEIGHT: 15px">&nbsp;</TD>
					<TD style="HEIGHT: 15px">&nbsp;</TD>
					<TD style="HEIGHT: 15px">&nbsp;</TD>
				</TR>
				<TR>
					<TD align="left" rowSpan="1" class="style1"><asp:dropdownlist id="ddlProductType" runat="server" DataValueField="instance" DataTextField="Description" AutoPostBack="False"></asp:dropdownlist></TD>
					<TD style="HEIGHT: 15px">&nbsp;</TD>
               <TD style="HEIGHT: 15px">&nbsp;</TD>
               <TD style="HEIGHT: 15px">&nbsp;</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" style="Z-INDEX: 104; LEFT: 40px; WIDTH: 800px; POSITION: absolute; TOP: 350px; HEIGHT: 75px"
				cellSpacing="1" cellPadding="1" width="800" border="0">
				<TR>
					<TD><dbwc:hierargrid id="dgTrackingFiles" runat="server" Font-Names="Verdana" Width="930px" PageSize="8"
							AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" TemplateCachingBase="Tablename"
							LoadControlMode="UserControl" templatedatamode="Table" rowexpanded="DBauer.Web.UI.WebControls.RowStates">
							<PagerStyle VerticalAlign="Middle" Font-Size="8pt" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
								ForeColor="White" BackColor="Navy" Mode="NumericPages"></PagerStyle>
							<ItemStyle Font-Names="Verdana"></ItemStyle>
							<HeaderStyle Font-Size="9pt" Font-Names="Verdana" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
								BackColor="Navy"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn></asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="CampaignId" HeaderText="CA ID">
									<ItemStyle Font-Size="9pt" Font-Names="Verdana" HorizontalAlign="Left"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=CAID runat="server" Width="5px" Text='<%# DataBinder.Eval(Container.DataItem, "CampaignId") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="CA Type">
									<ItemStyle Font-Size="9pt"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=CAType runat="server" Width="61px" Text='<%# DataBinder.Eval(Container.DataItem, "CampaignType") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Programs">
									<ItemStyle Font-Size="9pt" Font-Names="Verdana" HorizontalAlign="Left"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=Label1 runat="server" Width="200px" Text='<%# DataBinder.Eval(Container.DataItem, "ProgramList" ) %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="GroupId" HeaderText="Group ID">
									<ItemStyle Font-Size="9pt" Font-Names="Verdana" HorizontalAlign="Left"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=AcctId runat="server" Width="5px" Text='<%# DataBinder.Eval(Container.DataItem, "GroupId") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Group Name">
									<HeaderStyle Width="100px"></HeaderStyle>
									<ItemStyle Font-Size="9pt" Font-Names="Verdana" HorizontalAlign="Left"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=AccountName runat="server" Width="200px" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "GroupName" ) %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="FMName" HeaderText="FM Name">
									<HeaderStyle Width="100px"></HeaderStyle>
									<ItemStyle Font-Size="9pt" Font-Names="Verdana"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=FmName runat="server" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem, "FMName") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemStyle Font-Size="7pt"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblCA" runat="server" Visible="False">CA</asp:Label>
										<cc2:RSGenerationLinkButton id="rsGenerationOnlineStatementReport" runat="server" Width="98px" Font-Bold="True"
											ForeColor="Blue"></cc2:RSGenerationLinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="False" HeaderText="online">
									<ItemTemplate>
										<asp:Label id=lblOnLine runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "HasOnlineOrder") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="False" HeaderText="CAStart">
									<ItemTemplate>
										<asp:Label id=CAStart runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CAStart") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="False" HeaderText="CAEnd">
									<ItemTemplate>
										<asp:Label id=CAEnd runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CAEnd") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</dbwc:hierargrid></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
