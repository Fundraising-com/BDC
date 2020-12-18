<%@ Page language="c#" Codebehind="test.aspx.cs" AutoEventWireup="True" Inherits="CRMWeb.test" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TITLE>test</TITLE>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body ms_positioning="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<P><asp:datalist id="DataList1" runat="server">
					<ItemTemplate>
						&nbsp;
						<TABLE id="Table7" style="WIDTH: 408px; HEIGHT: 46px" borderColor="#6695c3" cellSpacing="0"
							cellPadding="0" width="408" border="0">
							<TR>
								<TD style="WIDTH: 127px; HEIGHT: 30px" vAlign="bottom">
									<asp:label id="Label33" runat="server" ForeColor="#6795C3" Font-Names="Microsoft Sans Serif"
										Font-Bold="True" Font-Size="8.5pt">Lead_ID:</asp:label></TD>
								<TD style="WIDTH: 233px; HEIGHT: 30px" vAlign="bottom">
									<asp:label id="Label8" runat="server" Width="80px" ForeColor="#6795C3" Font-Names="Microsoft Sans Serif"
										Font-Bold="True" Font-Size="8.5pt">Lead Name:</asp:label></TD>
								<TD style="WIDTH: 229px; HEIGHT: 30px" vAlign="bottom">
									<asp:label id="Label5" runat="server" Width="112px" ForeColor="#6795C3" Font-Names="Microsoft Sans Serif"
										Font-Bold="True" Font-Size="8.5pt">FC:</asp:label></TD>
								<TD style="WIDTH: 59px; HEIGHT: 30px" vAlign="bottom">
									<asp:label id="Label4" runat="server" Width="112px" ForeColor="#6795C3" Font-Names="Microsoft Sans Serif"
										Font-Bold="True" Font-Size="8.5pt">Time Received:</asp:label></TD>
								<TD style="WIDTH: 59px; HEIGHT: 30px" vAlign="bottom">
									<asp:label id="Label16" runat="server" Width="112px" ForeColor="#6795C3" Font-Names="Microsoft Sans Serif"
										Font-Bold="True" Font-Size="8.5pt">Score:</asp:label></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 127px; HEIGHT: 16px" vAlign="top">
									<asp:label id=lblFirstName runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt" Text='<%# DataBinder.Eval(Container.DataItem, "Consultant_ID") %>'>
									</asp:label></TD>
								<TD style="WIDTH: 233px; HEIGHT: 16px" vAlign="top">
									<asp:label id="Label1" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"></asp:label></TD>
								<TD style="WIDTH: 229px; HEIGHT: 16px" vAlign="top">
									<asp:label id="Label2" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"></asp:label></TD>
								<TD style="WIDTH: 59px; HEIGHT: 16px" vAlign="top">
									<asp:label id="Label3" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"></asp:label></TD>
								<TD style="WIDTH: 59px; HEIGHT: 16px" vAlign="top">
									<asp:label id="Label6" runat="server" Font-Names="Microsoft Sans Serif" Font-Size="9pt"></asp:label></TD>
							</TR>
						</TABLE>
					</ItemTemplate>
				</asp:datalist></P>
			<TABLE id="Table5" cellSpacing="1" cellPadding="1" width="300" border="1">
				<TR>
					<TD vAlign="top">
						<asp:ImageButton id="ImageButton1" runat="server"></asp:ImageButton></TD>
					<TD>
						<asp:Panel id="Panel1" runat="server" Height="136px">
							<TABLE id="Table1" style="WIDTH: 560px; HEIGHT: 71px" borderColor="#6695c3" cellSpacing="0"
								cellPadding="0" width="560" border="0">
								<TR>
									<TD style="WIDTH: 127px; HEIGHT: 7px" vAlign="bottom"></TD>
									<TD style="WIDTH: 233px; HEIGHT: 7px" vAlign="bottom">
										<asp:label id="Label12" runat="server" Width="80px" ForeColor="#6795C3" Font-Names="Microsoft Sans Serif"
											Font-Bold="True" Font-Size="8.5pt">Credit Status:</asp:label></TD>
									<TD style="WIDTH: 229px; HEIGHT: 7px" vAlign="bottom">
										<asp:label id="Label11" runat="server" Width="112px" ForeColor="#6795C3" Font-Names="Microsoft Sans Serif"
											Font-Bold="True" Font-Size="8.5pt">Permanent Status:</asp:label></TD>
									<TD style="WIDTH: 59px; HEIGHT: 7px" vAlign="bottom">
										<asp:label id="Label10" runat="server" Width="112px" ForeColor="#6795C3" Font-Names="Microsoft Sans Serif"
											Font-Bold="True" Font-Size="8.5pt">Valid Until:</asp:label></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 127px; HEIGHT: 22px" vAlign="top"></TD>
									<TD style="WIDTH: 233px; HEIGHT: 22px" vAlign="top">
										<asp:dropdownlist id="DropDownList1" runat="server" Height="7px" Width="120px" Font-Size="9pt" BackColor="White">
											<asp:ListItem Value="Accepted" Selected="True">Accepted</asp:ListItem>
										</asp:dropdownlist></TD>
									<TD style="WIDTH: 229px; HEIGHT: 22px" vAlign="top">
										<asp:dropdownlist id="DropDownList2" runat="server" Height="1px" Width="120px" Font-Size="9pt" BackColor="White">
											<asp:ListItem Value="Accepted" Selected="True">Very Good</asp:ListItem>
										</asp:dropdownlist></TD>
									<TD style="WIDTH: 59px; HEIGHT: 22px" vAlign="top">
										<asp:textbox id="TextBox1" runat="server" Width="112px">12/25/2006</asp:textbox></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 127px; HEIGHT: 1px" vAlign="bottom"></TD>
									<TD style="WIDTH: 233px; HEIGHT: 1px" vAlign="bottom">
										<asp:label id="Label9" runat="server" ForeColor="#6795C3" Font-Names="Microsoft Sans Serif"
											Font-Bold="True" Font-Size="8.5pt"> Max amount:</asp:label></TD>
									<TD style="WIDTH: 229px; HEIGHT: 1px" vAlign="bottom">
										<asp:label id="Label7" runat="server" ForeColor="#6795C3" Font-Names="Microsoft Sans Serif"
											Font-Bold="True" Font-Size="8.5pt"> Max amount:</asp:label></TD>
									<TD style="WIDTH: 59px; HEIGHT: 1px" vAlign="bottom">
										<asp:label id="Label13" runat="server" Width="78px" ForeColor="#6795C3" Font-Names="Microsoft Sans Serif"
											Font-Bold="True" Font-Size="8.5pt"> Max amount:</asp:label></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 127px; HEIGHT: 2px" vAlign="top"></TD>
									<TD style="WIDTH: 233px; HEIGHT: 2px" vAlign="top">
										<asp:TextBox id="TextBox5" runat="server"></asp:TextBox></TD>
									<TD style="WIDTH: 229px; HEIGHT: 2px" vAlign="top"></TD>
									<TD style="WIDTH: 59px; HEIGHT: 2px" vAlign="top"></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 127px; HEIGHT: 15px" vAlign="top"></TD>
									<TD style="WIDTH: 233px; HEIGHT: 15px" vAlign="top"></TD>
									<TD style="WIDTH: 229px; HEIGHT: 15px" vAlign="top"></TD>
									<TD style="WIDTH: 59px; HEIGHT: 15px" vAlign="top"></TD>
								</TR>
							</TABLE>
						</asp:Panel></TD>
					<TD></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
