<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ControlerActionReason.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerActionReason" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<script language="javascript">
		
	function SetIDIncident(ID)
	{
		var tbxReferToIncident = document.getElementById('ctrlControlerActionReason_tbxReferToIncident');
		tbxReferToIncident.value = ID;
		
	}
	function SetProblemCode(PCID,Description)
	{
		var tbxProblemCode = document.getElementById('ctrlControlerActionReason_tbxProblemCode');
		tbxProblemCode.value = PCID;
		
		var lblDescription = document.getElementById('ctrlControlerActionReason_lblProblemDescription');
		lblDescription.innerHTML = Description;
	
	}
</script>
<P>
	<TABLE id="Table2" cellSpacing="0" cellPadding="0" border="0">
		<TR>
			<TD style="HEIGHT: 89px">
				<P>
					<asp:Panel id="Panel1" runat="server">
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0">
							<TR>
								<TD>
									<asp:Label id="lblCommunication" runat="server">Communication Chanel</asp:Label></TD>
								<TD>
									<asp:DropDownList id="ddlCommunicationChanel" runat="server">
										<asp:ListItem Value="Email">Email</asp:ListItem>
										<asp:ListItem Value="Fax">Fax</asp:ListItem>
										<asp:ListItem Value="Mail">Mail</asp:ListItem>
										<asp:ListItem Value="Phone">Phone</asp:ListItem>
									</asp:DropDownList></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD>
									<asp:Label id="lblProblemCode" runat="server">Problem Code</asp:Label></TD>
								<TD>
									<asp:TextBox id="tbxProblemCode" runat="server"></asp:TextBox></TD>
								<TD>
									<asp:Label id="lblProblemDescription" runat="server"></asp:Label></TD>
								<TD>
									<asp:HyperLink id="HyperLink1" runat="server" Target="_blank" NavigateUrl="ProblemCode.aspx?ID=true">Find</asp:HyperLink></TD>
							</TR>
							<TR>
								<TD>
									<asp:Label id="lblComment" runat="server">Comment</asp:Label></TD>
								<TD>
									<asp:TextBox id="tbxComment" runat="server"></asp:TextBox></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD>
									<asp:Label id="Label2" runat="server">Refer To Incident</asp:Label></TD>
								<TD>
									<asp:TextBox id="tbxReferToIncident" runat="server"></asp:TextBox></TD>
								<TD></TD>
								<TD>
									<asp:HyperLink id="hypFindIncident" runat="server" Target="_blank" NavigateUrl="findincident.aspx">Find</asp:HyperLink></TD>
							</TR>
							<TR>
								<TD>
									<asp:Label id="lblCommunicationSource" runat="server">CommunicationSource</asp:Label></TD>
								<TD>
									<asp:DropDownList id="ddlCommunicationSource" runat="server">
										<asp:ListItem Value="Customer">Customer</asp:ListItem>
										<asp:ListItem Value="FM">FM</asp:ListItem>
										<asp:ListItem Value="Group">Group</asp:ListItem>
									</asp:DropDownList></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD>
									<asp:Label id="Label3" runat="server" DESIGNTIMEDRAGDROP="62">Status</asp:Label></TD>
								<TD>
									<asp:DropDownList id="ddlStatus" runat="server">
										<asp:ListItem Value="Open">Open</asp:ListItem>
										<asp:ListItem Value="Close">Close</asp:ListItem>
									</asp:DropDownList></TD>
								<TD></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</asp:Panel></P>
			</TD>
		</TR>
		<TR>
			<TD></TD>
		</TR>
	</TABLE>
</P>
