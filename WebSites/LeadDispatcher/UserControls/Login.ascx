<%@ Control Language="c#" AutoEventWireup="True" Codebehind="Login.ascx.cs" Inherits="CRMWeb.UserControls.Login" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="300" border="0">
	<TR>
		<TD style="WIDTH: 169px; HEIGHT: 87px"></TD>
		<TD style="HEIGHT: 87px"></TD>
		<TD style="HEIGHT: 87px"></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 169px"></TD>
		<TD>
			<TABLE id="Table1" style="WIDTH: 288px; HEIGHT: 175px" borderColor="#6695c3" cellSpacing="0"
				cellPadding="1" width="288" border="1">
				<TR>
					<TD bgColor="#f7f7f7">
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="280" border="0" style="WIDTH: 280px; HEIGHT: 150px">
							<TR>
								<TD style="WIDTH: 282px" vAlign="middle">&nbsp;
									<asp:Label id="Label1" Font-Names="Microsoft Sans Serif" Font-Bold="True" ForeColor="#294585"
										Width="120px" runat="server">Account Login</asp:Label></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 282px" vAlign="middle"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 282px; HEIGHT: 82px" vAlign="middle" colSpan="">
									<TABLE id="Table3" style="WIDTH: 256px; HEIGHT: 52px" cellSpacing="0" cellPadding="0" width="256"
										border="0">
										<TR>
											<TD style="WIDTH: 17px; HEIGHT: 18px"></TD>
											<TD style="WIDTH: 35px; HEIGHT: 18px">
												<asp:Label id="Label2" Font-Names="Microsoft Sans Serif" Font-Bold="True" ForeColor="#6795C3"
													runat="server" Font-Size="8.5pt" Width="64px">User name</asp:Label></TD>
											<TD style="HEIGHT: 18px" align="right">
												<asp:TextBox id="txtUserName" Width="168px" runat="server" BorderWidth="1px" BorderColor="#6695C3"
													BorderStyle="Solid" ontextchanged="txtUserName_TextChanged"></asp:TextBox></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 17px; HEIGHT: 22px"></TD>
											<TD style="WIDTH: 35px; HEIGHT: 22px">
												<asp:Label id="Label3" Font-Names="Microsoft Sans Serif" Font-Bold="True" ForeColor="#6795C3"
													runat="server" Font-Size="8.5pt">Password</asp:Label></TD>
											<TD style="HEIGHT: 22px" align="right">
												<asp:TextBox id="txtPassword" Width="168px" runat="server" BorderWidth="1px" BorderColor="#6695C3"
													BorderStyle="Solid" TextMode="Password"></asp:TextBox></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 17px; HEIGHT: 18px"></TD>
											<TD style="WIDTH: 35px; HEIGHT: 18px" colSpan="2">
												<asp:label id="lblError" runat="server" ForeColor="Red" Font-Size="9pt" Visible="False" Width="224px">**Invalid User Name or Password</asp:label></TD>
											<TD style="HEIGHT: 18px" align="right"></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 282px" align="center" vAlign="top">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:ImageButton id="cmdLogin" runat="server" ImageUrl="../images/signIn7.gif"></asp:ImageButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</TD>
		<TD></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 169px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		</TD>
		<TD></TD>
		<TD></TD>
	</TR>
</TABLE>
