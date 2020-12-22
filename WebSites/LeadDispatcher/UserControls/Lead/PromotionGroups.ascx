<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PromotionGroups.ascx.cs" Inherits="CRMWeb.UserControls.Lead.PromotionGroups" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table3" style="WIDTH: 632px; HEIGHT: 469px" borderColor="#6695c3" cellSpacing="0"
	cellPadding="0" width="632" border="0">
	<TR>
		<TD vAlign="top">
			<TABLE id="Table1" style="WIDTH: 552px; HEIGHT: 216px" cellSpacing="0" cellPadding="0"
				width="552" border="0">
				<TR>
					<TD style="WIDTH: 217px; HEIGHT: 10px" vAlign="bottom"></TD>
					<TD style="WIDTH: 32px; HEIGHT: 10px"></TD>
					<TD style="HEIGHT: 10px"></TD>
					<TD style="WIDTH: 220px; HEIGHT: 10px" vAlign="bottom"></TD>
					<TD style="WIDTH: 7px; HEIGHT: 10px"></TD>
					<TD style="HEIGHT: 10px"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 217px; HEIGHT: 32px" vAlign="bottom" align="left">
						<asp:label id="lblClassified" ForeColor="#294585" Font-Bold="True" Font-Size="10pt" Font-Names="Microsoft Sans Serif"
							Width="200px" runat="server" Height="12px"> Indirect Promotions:</asp:label></TD>
					<TD style="WIDTH: 32px; HEIGHT: 32px"></TD>
					<TD style="HEIGHT: 32px"></TD>
					<TD style="WIDTH: 220px; HEIGHT: 32px" vAlign="bottom">
						<asp:label id="lblUnclassifed" ForeColor="#294585" Font-Bold="True" Font-Size="10pt" Font-Names="Microsoft Sans Serif"
							Width="200px" runat="server">Unclassified Promotions:</asp:label></TD>
					<TD style="WIDTH: 7px; HEIGHT: 32px"></TD>
					<TD style="HEIGHT: 32px"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 217px; HEIGHT: 215px" vAlign="top">
						<asp:DropDownList id="cboPromoGroup" ForeColor="Black" Font-Size="10pt" Width="208px" runat="server"
							AutoPostBack="True" onselectedindexchanged="cboPromoGroup_SelectedIndexChanged"></asp:DropDownList>
						<asp:ListBox id="lstPromoGroup" Width="208px" runat="server" Height="199px" SelectionMode="Multiple"></asp:ListBox></TD>
					<TD style="WIDTH: 32px; HEIGHT: 215px" vAlign="middle" align="center">
						<TABLE id="Table2" style="WIDTH: 32px; HEIGHT: 53px" cellSpacing="0" cellPadding="0" width="32"
							border="0">
							<TR>
								<TD>
									<asp:ImageButton id="cmdAssign" runat="server" ImageUrl="../../images/goBack.gif"></asp:ImageButton></TD>
							</TR>
							<TR>
								<TD>
									<asp:ImageButton id="cmdUnassign" runat="server" ImageUrl="../../images/Continue.gif"></asp:ImageButton></TD>
							</TR>
							<TR style="HEIGHT: 40px">
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
					<TD style="HEIGHT: 215px"></TD>
					<TD style="WIDTH: 220px; HEIGHT: 215px" vAlign="top">
						<asp:ListBox id="lstUnclassified" Width="200px" runat="server" Height="216px" SelectionMode="Multiple"></asp:ListBox></TD>
					<TD style="WIDTH: 7px; HEIGHT: 215px" vAlign="top">&nbsp;&nbsp;&nbsp;&nbsp;
					</TD>
					<TD style="HEIGHT: 215px" vAlign="top">
						<TABLE id="Table4" style="WIDTH: 128px; HEIGHT: 165px" cellSpacing="0" cellPadding="0"
							width="128" border="0">
							<TR>
								<TD style="WIDTH: 97px; HEIGHT: 27px">
									<asp:Label id="Label2" ForeColor="#6795C3" Font-Bold="True" Font-Size="8.5pt" Font-Names="Microsoft Sans Serif"
										runat="server">Promo Type:</asp:Label>
									<asp:DropDownList id="cboPromoType" Width="120px" runat="server"></asp:DropDownList></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 97px; HEIGHT: 11px">
									<asp:Label id="Label1" ForeColor="#6795C3" Font-Bold="True" Font-Size="8.5pt" Font-Names="Microsoft Sans Serif"
										runat="server">Partner:</asp:Label>
									<asp:DropDownList id="cboPartner" Width="120px" runat="server"></asp:DropDownList></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 97px; HEIGHT: 13px" vAlign="middle"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 97px">
									<TABLE id="Table10" style="WIDTH: 112px; HEIGHT: 57px" cellSpacing="0" cellPadding="0"
										width="112" border="0">
										<TR>
											<TD style="WIDTH: 44px; HEIGHT: 35px">
												<asp:imagebutton id="cmdDetails" runat="server" ImageUrl="../../images/Search.gif"></asp:imagebutton></TD>
											<TD style="HEIGHT: 35px">
												<asp:label id="Label13" Font-Size="9pt" Font-Names="Microsoft Sans Serif" Width="72px" runat="server">Apply Filter</asp:label></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 44px; HEIGHT: 35px" align="center">
												<asp:imagebutton id="cmdDetailsUnclassified" runat="server" ImageUrl="../../images/quote.gif"></asp:imagebutton></TD>
											<TD style="HEIGHT: 35px">
												<asp:label id="Label5" Font-Size="9pt" Font-Names="Microsoft Sans Serif" Width="128px" runat="server">Details (Unclassified)</asp:label></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 44px" align="center">
												<asp:imagebutton id="cmdDetailsClassified" runat="server" ImageUrl="../../images/quote.gif"></asp:imagebutton></TD>
											<TD>
												<asp:label id="Label12" Font-Size="9pt" Font-Names="Microsoft Sans Serif" Width="120px" runat="server">Details (Classified)</asp:label></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
						&nbsp;
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 217px; HEIGHT: 30px" vAlign="top"></TD>
					<TD style="WIDTH: 32px; HEIGHT: 30px" align="center"></TD>
					<TD style="HEIGHT: 30px"></TD>
					<TD style="WIDTH: 220px; HEIGHT: 30px" vAlign="top"></TD>
					<TD style="WIDTH: 7px; HEIGHT: 30px" vAlign="top"></TD>
					<TD style="HEIGHT: 30px" vAlign="top"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 217px">
						<asp:label id="Label10" ForeColor="#294585" Font-Bold="True" Font-Size="11pt" Font-Names="Microsoft Sans Serif"
							Width="112px" runat="server">Details</asp:label></TD>
					<TD style="WIDTH: 32px"></TD>
					<TD></TD>
					<TD style="WIDTH: 220px"></TD>
					<TD style="WIDTH: 7px"></TD>
					<TD></TD>
				</TR>
			</TABLE>
			<TABLE id="Table6" style="WIDTH: 581px; HEIGHT: 96px" borderColor="#6695c3" cellSpacing="0"
				cellPadding="0" width="581" bgColor="#f7f7f7" border="1">
				<TR>
					<TD vAlign="top">
						<TABLE id="Table7" style="WIDTH: 576px; HEIGHT: 88px" borderColor="#6695c3" cellSpacing="0"
							cellPadding="0" width="576" border="0">
							<TR>
								<TD style="WIDTH: 266px; HEIGHT: 9px" vAlign="bottom">
									<asp:Label id="Label6" ForeColor="#6795C3" Font-Bold="True" Font-Size="8.5pt" Font-Names="Microsoft Sans Serif"
										runat="server">Promotion:</asp:Label></TD>
								<TD style="WIDTH: 162px; HEIGHT: 9px" vAlign="bottom">
									<asp:Label id="Label8" ForeColor="#6795C3" Font-Bold="True" Font-Size="8.5pt" Font-Names="Microsoft Sans Serif"
										Width="125px" runat="server">Promotion Type Code:</asp:Label></TD>
								<TD style="WIDTH: 211px; HEIGHT: 9px" vAlign="bottom">
									<asp:Label id="Label14" ForeColor="#6795C3" Font-Bold="True" Font-Size="8.5pt" Font-Names="Microsoft Sans Serif"
										Width="112px" runat="server">Promotion Type:</asp:Label></TD>
								<TD style="HEIGHT: 9px" vAlign="bottom">
									<asp:Label id="Label3" ForeColor="#6795C3" Font-Bold="True" Font-Size="8.5pt" Font-Names="Microsoft Sans Serif"
										Width="112px" runat="server">Promotion Group:</asp:Label></TD>
								<TD style="HEIGHT: 9px"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 266px; HEIGHT: 9px" vAlign="top">
									<asp:Label id="lblPromo" Font-Size="9pt" Font-Names="Microsoft Sans Serif" runat="server"></asp:Label></TD>
								<TD style="WIDTH: 162px; HEIGHT: 9px" vAlign="top">
									<asp:Label id="lblPromoTypeCode" Font-Size="9pt" Font-Names="Microsoft Sans Serif" runat="server"></asp:Label></TD>
								<TD style="WIDTH: 211px; HEIGHT: 9px" vAlign="top">
									<asp:Label id="lblPromoType" Font-Size="9pt" Font-Names="Microsoft Sans Serif" runat="server"></asp:Label></TD>
								<TD style="HEIGHT: 9px" vAlign="top">
									<asp:Label id="lblPromoGroup" Font-Size="9pt" Font-Names="Microsoft Sans Serif" runat="server"></asp:Label></TD>
								<TD style="HEIGHT: 9px"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 266px; HEIGHT: 17px" vAlign="bottom">
									<asp:Label id="Label7" ForeColor="#6795C3" Font-Bold="True" Font-Size="8.5pt" Font-Names="Microsoft Sans Serif"
										runat="server">Script Name:</asp:Label></TD>
								<TD style="WIDTH: 162px; HEIGHT: 17px" vAlign="bottom">
									<asp:Label id="Label11" ForeColor="#6795C3" Font-Bold="True" Font-Size="8.5pt" Font-Names="Microsoft Sans Serif"
										runat="server">Advertiser:</asp:Label></TD>
								<TD style="WIDTH: 211px; HEIGHT: 17px" vAlign="bottom">
									<asp:Label id="Label4" ForeColor="#6795C3" Font-Bold="True" Font-Size="8.5pt" Font-Names="Microsoft Sans Serif"
										runat="server">Partner:</asp:Label></TD>
								<TD style="HEIGHT: 17px" vAlign="bottom">
									<asp:Label id="Label9" ForeColor="#6795C3" Font-Bold="True" Font-Size="8.5pt" Font-Names="Microsoft Sans Serif"
										runat="server">Promotion ID:</asp:Label></TD>
								<TD style="HEIGHT: 17px"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 266px; HEIGHT: 25px" vAlign="top">
									<asp:Label id="lblScript" Font-Size="9pt" Font-Names="Microsoft Sans Serif" runat="server"></asp:Label></TD>
								<TD style="WIDTH: 162px; HEIGHT: 25px" vAlign="top">
									<asp:Label id="lblAdvertiser" Font-Size="9pt" Font-Names="Microsoft Sans Serif" runat="server"></asp:Label></TD>
								<TD style="WIDTH: 211px; HEIGHT: 25px" vAlign="top">
									<asp:Label id="lblPartner" Font-Size="9pt" Font-Names="Microsoft Sans Serif" runat="server"></asp:Label></TD>
								<TD style="HEIGHT: 25px" vAlign="top">
									<asp:Label id="lblPromoID" Font-Size="9pt" Font-Names="Microsoft Sans Serif" runat="server"></asp:Label></TD>
								<TD style="HEIGHT: 25px"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>
