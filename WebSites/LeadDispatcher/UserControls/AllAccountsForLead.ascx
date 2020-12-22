<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AllAccountsForLead.ascx.cs" Inherits="CRMWeb.UserControls.AllAccountsForLead" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="300" border="0">
	<TR>
		<TD><asp:repeater id="myRepeaterAltSep" runat="server">
				<ItemTemplate>
					<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="300" border="0">
						<TR>
							<TD style="WIDTH: 66px">
								<asp:Label id="Label1" runat="server" Width="56px">FSM ID:</asp:Label></TD>
							<TD style="WIDTH: 108px">
								<asp:Label id="Label2" runat="server">
									<%# DataBinder.Eval(Container.DataItem, "fm_Id") %>
								</asp:Label></TD>
							<TD></TD>
							<TD style="WIDTH: 114px">
								<asp:Label id="Label3" runat="server" Width="96px">Home Phone:</asp:Label></TD>
							<TD>
								<asp:Label id="Label4" runat="server" Width="184px">
									<%# DataBinder.Eval(Container.DataItem, "home_Phone") %>
								</asp:Label></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 66px; HEIGHT: 23px">
								<asp:Label id="Label5" runat="server" Width="88px">FSM Name:</asp:Label></TD>
							<TD style="WIDTH: 108px; HEIGHT: 23px">
								<asp:Label id="Label6" runat="server">
									<%# DataBinder.Eval(Container.DataItem, "Name") %>
								</asp:Label></TD>
							<TD style="HEIGHT: 23px"></TD>
							<TD style="WIDTH: 114px; HEIGHT: 23px">
								<asp:Label id="Label7" runat="server" Width="88px">Work Phone:</asp:Label></TD>
							<TD style="HEIGHT: 23px">
								<asp:Label id="Label8" runat="server">
									<%# DataBinder.Eval(Container.DataItem, "Work_Phone") %>
								</asp:Label></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 66px">
								<asp:Label id="Label9" runat="server" Width="96px">FSM Email:</asp:Label></TD>
							<TD style="WIDTH: 108px">
								<asp:Label id="Label10" runat="server">
									<%# DataBinder.Eval(Container.DataItem, "email") %>
								</asp:Label></TD>
							<TD></TD>
							<TD style="WIDTH: 114px">
								<asp:Label id="Label11" runat="server" Width="88px">Mobile Phone:</asp:Label></TD>
							<TD>
								<asp:Label id="Label12" runat="server">
									<%# DataBinder.Eval(Container.DataItem, "Mobile_Phone") %>
								</asp:Label></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 66px"></TD>
							<TD style="WIDTH: 108px"></TD>
							<TD>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							</TD>
							<TD style="WIDTH: 114px"></TD>
							<TD></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 66px">
								<asp:Label id="Label13" runat="server" Width="112px">Account Number:</asp:Label></TD>
							<TD style="WIDTH: 108px">
								<asp:Label id="Label14" runat="server" Font-Bold="True">
									<%# DataBinder.Eval(Container.DataItem, "account_no") %>
								</asp:Label></TD>
							<TD></TD>
							<TD style="WIDTH: 114px"></TD>
							<TD></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 66px">
								<asp:Label id="Label15" runat="server" Width="112px">Account Name:</asp:Label></TD>
							<TD style="WIDTH: 108px">
								<asp:Label id="Label16" runat="server" Width="160px">
									<%# DataBinder.Eval(Container.DataItem, "account_Name") %>
								</asp:Label></TD>
							<TD></TD>
							<TD style="WIDTH: 114px"></TD>
							<TD></TD>
						</TR>
					</TABLE>
				</ItemTemplate>
				<SeparatorTemplate>
					<asp:Label id="Label23" runat="server" Width="312px">------------------------------------------</asp:Label></TD>
		</SeparatorTemplate> </asp:repeater></TD></TR>
</TABLE>
