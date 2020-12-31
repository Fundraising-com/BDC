<%@ Register  TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Page language="c#" Codebehind="PrintMagQueueProcessed.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.OrderMgt.PrintMagQueueProcessed" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BatchList</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body leftmargin="0" topmargin="0">
		<CENTER>
			<form id="Form1" method="post" runat="server">
				<!-- #include virtual="/Qspfulfillment/Includes/Menu.inc" -->
				<P>
					<STRONG><FONT size="2" color="green">The magazine queue orders were processed 
							successfully.<BR>
							<P><STRONG><FONT color="#000000" size="2">You may click on the orders below to get 
										report status.</FONT></STRONG></P>
							<P><STRONG><FONT color="green" size="2"></FONT></STRONG>&nbsp;</P>
							<P><STRONG><FONT color="green" size="2">
										<asp:Repeater id="Repeater1" runat="server">
											<ItemTemplate>
												<b><a href='ReportStatus.aspx?BatchOrderId=<%# Container.DataItem %>'>
														<%# Container.DataItem %>
													</a></b>
												<br>
											</ItemTemplate>
										</asp:Repeater></P>
							<P>
								<BR>
								<BR>
								<BR>
								<asp:Button id="Button1" runat="server" Text="Process More Magazine Orders" CssClass="Button4" onclick="Button1_Click"></asp:Button>
						</FONT></STRONG>
				</P>
			</form>
		</CENTER>
		</FONT></STRONG>
	</body>
</HTML>
