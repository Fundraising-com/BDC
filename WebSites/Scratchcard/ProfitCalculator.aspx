<%@ Page language="c#" Codebehind="ProfitCalculator.aspx.cs" AutoEventWireup="false" Inherits="GA.BDC.WEB.ScratchcardWeb.ProfitCalculator" %>
<%@ Register TagPrefix="contentpanel" Namespace="GA.BDC.Core.Web.UI.UIControls" Assembly="GA.BDC.Core.Web.UI.UIControls" %>
<%@ Register TagPrefix="efundraising" Namespace="GA.BDC.Core.Web.UI.MasterPages" Assembly="GA.BDC.Core.Web.UI.MasterPages" %>
<efundraising:MASTERPAGE id="MasterPage1" runat="server" master="~/MasterPage/SiteTemplate1.ascx">
	<efundraising:Content id="Content1" runat="server" ContentPlaceHolderID="cph_PageContent">
		<contentpanel:contentpanelcontrol id="ContentPanelControl3" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\ProfitCalculator.aspx"></contentpanel:contentpanelcontrol>
		<BR>
		<asp:image id="Image1" runat="server" ImageUrl="Resources/images/_ScratchcardWeb_/_classic_/en-US/common/Title/doted_line.gif"></asp:image>
		<BR>
		<P>
			<TABLE class="normal_text" id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD><B>
							<contentpanel:contentpanelcontrol id="ContentPanelControl1" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\ProfitCalculator.aspx"></contentpanel:contentpanelcontrol></B><BR>
						<contentpanel:contentpanelcontrol id="ContentPanelControl2" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\ProfitCalculator.aspx"></contentpanel:contentpanelcontrol></TD>
				</TR>
				<TR>
					<TD>
						<DIV style="MARGIN-LEFT: 10px">
							<P>&nbsp;</P>
							<P><B>
									<contentpanel:contentpanelcontrol id="ContentPanelControl4" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\ProfitCalculator.aspx"></contentpanel:contentpanelcontrol></B></P>
							<P>
								<contentpanel:contentpanelcontrol id="ContentPanelControl5" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\ProfitCalculator.aspx"></contentpanel:contentpanelcontrol></P>
						</DIV>
					</TD>
				</TR>
				<TR>
					<TD>
						<DIV style="MARGIN-LEFT: 10px"><BR>
							<B>
								<contentpanel:contentpanelcontrol id="ContentPanelControl9" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\ProfitCalculator.aspx"></contentpanel:contentpanelcontrol></B></DIV>
						<asp:image id="Image2" runat="server" ImageUrl="Resources/images/_ScratchcardWeb_/_classic_/en-US/common/Title/doted_line.gif"></asp:image></TD>
				</TR>
				<TR>
					<TD>
						<DIV style="MARGIN-LEFT: 20px"><BR>
							<TABLE cellSpacing="0" cellPadding="0" width="95%" align="center" border="0">
								<TR>
									<TD vAlign="bottom" width="45%"><B>
											<contentpanel:contentpanelcontrol id="ContentPanelControl6" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\ProfitCalculator.aspx"></contentpanel:contentpanelcontrol></B></TD>
									<TD vAlign="bottom">
										<asp:textbox id="txtGroupMembers" runat="server" MaxLength="5" Width="55px" AutoPostBack="True"
											size="6"></asp:textbox></TD>
									<TD>
										<contentpanel:buttonpanelcontrol id="ButtonPanelControl1" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\ProfitCalculator.aspx"
											ButtonType="IMAGE"></contentpanel:buttonpanelcontrol></TD>
								</TR>
								<TR>
									<TD vAlign="bottom" colSpan="3"><SPAN style="COLOR: #ff0000">
											<contentpanel:contentpanelcontrol id="lblError" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\ProfitCalculator.aspx"></contentpanel:contentpanelcontrol>
										</SPAN></TD>
								</TR>
							</TABLE>
							<TABLE class="normal_text" cellSpacing="0" cellPadding="0" width="97%" border="0">
								<TR>
									<TD><SPAN style="FONT-WEIGHT: bold; COLOR: #ff0000">
											<contentpanel:contentpanelcontrol id="ContentPanelControl7" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\ProfitCalculator.aspx"></contentpanel:contentpanelcontrol>
										</SPAN></TD>
									<TD width="65%"><B>$&nbsp;&nbsp;</B>
										<asp:textbox id="txtRaise" runat="server" Width="90px" size="11" ReadOnly="True"></asp:textbox></TD>
								</TR>
								<TR>
									<TD><SPAN style="FONT-WEIGHT: bold; COLOR: #ff0000">
											<contentpanel:contentpanelcontrol id="ContentPanelControl8" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\ProfitCalculator.aspx"></contentpanel:contentpanelcontrol>
										</SPAN></TD>
									<TD width="65%"><B>$&nbsp;&nbsp;</B>
										<asp:textbox id="txtProfits" runat="server" Width="90px" size="11" ReadOnly="True"></asp:textbox></TD>
								</TR>
							</TABLE>
							<P>&nbsp;</P>
							<P>
								<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="300" border="0">
									<TR>
										<TD width="137">
											<contentpanel:buttonpanelcontrol id="Buttonpanelcontrol3" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\ProfitCalculator.aspx"
												ButtonType="IMAGE"></contentpanel:buttonpanelcontrol><A href="SampleKit.aspx"></A></TD>
										<TD><A href="SampleKit.aspx"><IMG src="Resources/images/_ScratchcardWeb_/_classic_/en-US/common/b_ordernow.gif" border="0"></A></TD>
									</TR>
								</TABLE>
							</P>
						</DIV>
					</TD>
				</TR>
			</TABLE>
		</P>
	</efundraising:Content>
</efundraising:MASTERPAGE>
<ContentPanel:PagePanelControl id="PagePanelControl1" runat="server" ASPXFileName="d:\efundraising\source\EFundraisingSolution\ScratchcardWeb\ProfitCalculator.aspx"></ContentPanel:PagePanelControl>
