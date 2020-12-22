<%@ Page Language="vb" AutoEventWireup="false" Codebehind="compilesite.aspx.vb" Inherits="StoreFront.StoreFront.CompileSite"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<%
'@BEGINVERSIONINFO

'@APPVERSION: 7.0.0

'@STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'@ENDCOPYRIGHT

'@ENDVERSIONINFO
%>

<HTML>
	<HEAD>
		<title>CompileSite</title>
		
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/nav4-0" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
					<form id="Form1" method="post" runat="server">
									<TABLE id="Table1" cellSpacing="1" cellPadding="1" border="0">
										<TR>
											<TD>
												<asp:Label id="Label3" runat="server">Label</asp:Label></TD>
										</TR>
										<TR>
											<TD>&nbsp;</TD>
										</TR>
										<TR>
											<TD>
												<asp:Label id="Label1" runat="server">Compiler Errors</asp:Label></TD>
										</TR>
										<TR>
											<TD>
												<asp:DataGrid id="DataGrid1" runat="server" AutoGenerateColumns="False" Width="100%">
													<Columns>
														<asp:BoundColumn HeaderText="Line" DataField="Line"></asp:BoundColumn>
														<asp:BoundColumn HeaderText="Error Number" DataField="ErrorNumber"></asp:BoundColumn>
														<asp:BoundColumn HeaderText="Error" DataField="Error"></asp:BoundColumn>
														<asp:BoundColumn HeaderText="FileName" DataField="FileName"></asp:BoundColumn>
													</Columns>
												</asp:DataGrid></TD>
										</TR>
										<TR>
											<TD>&nbsp;</TD>
										</TR>
										<TR>
											<TD>
												<asp:Label id="Label2" runat="server">VBC Output</asp:Label></TD>
										</TR>
										<TR>
											<TD>
												<asp:DataList id="DataList2" runat="server">
													<ItemTemplate>
														<%# DataBinder.Eval(Container.DataItem,"ErrorText") %>
													</ItemTemplate>
												</asp:DataList></TD>
										</TR>
									</TABLE>
					</form>
	</body>
</HTML>
