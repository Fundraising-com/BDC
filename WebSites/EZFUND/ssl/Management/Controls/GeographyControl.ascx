<%@ Control Language="vb" AutoEventWireup="false" Codebehind="GeographyControl.ascx.vb" Inherits="StoreFront.StoreFront.GeographyControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<%
'@BEGINVERSIONINFO

'@APPVERSION: 6.0.1.0

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
<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TBODY>
		<tr>
			<TD class="Content" width="1" colSpan="7" align="middle">
			</TD>
		</tr>
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="ContentTableHeader" noWrap align="left" colSpan="5">&nbsp;&nbsp;Supported 
				Countries&nbsp;
				<asp:Label id="CErrorMessage" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
			</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right" colspan="1" valign="top">Accept Orders From The 
				Following Countries:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left" colspan="3" valign="top"><asp:ListBox Width="400" id="ActiveCountries" runat="server"></asp:ListBox></td>
			<td class="content" align="middle" colspan="1" valign="bottom">
				<asp:LinkButton ID="cmdMoveCountryToInactive" Runat="server">
					<asp:Image BorderWidth="0" ID="imgMoveCountryToInactive" runat="server" ImageUrl="../images/remove.jpg" AlternateText="Remove"></asp:Image>
				</asp:LinkButton>
				&nbsp;</td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right" nowrap colspan="1">Country 
				Name:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left" colspan="1"><asp:textbox id="NewCountry" runat="server" MaxLength="100"></asp:textbox></td>
			<td class="content" align="right" nowrap colspan="1">Abbreviation:&nbsp;</td>
			<td class="content" align="left" colspan="1"><asp:textbox id="NewCountryAbbrev" runat="server" MaxLength="3"></asp:textbox></td>
			<td class="content" align="middle" colspan="1">
				<asp:LinkButton ID="cmdAddCountryToActive" Runat="server">
					<asp:Image BorderWidth="0" ID="imgAddCountryToActive" runat="server" ImageUrl="../images/add.jpg" AlternateText="Add"></asp:Image>
				</asp:LinkButton>
				&nbsp;</td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right" colspan="1" valign="top">Unsupported 
				Countries:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left" colspan="3" valign="top"><asp:ListBox Width="400" id="InactiveCountries" runat="server"></asp:ListBox></td>
			<td class="content" align="middle" colspan="1" valign="bottom">
				<asp:LinkButton ID="cmdDeleteCountry" Runat="server">
					<asp:Image BorderWidth="0" ID="imgDeleteCountry" runat="server" ImageUrl="../images/delete.jpg" AlternateText="Delete"></asp:Image>
				</asp:LinkButton>
				&nbsp;<br>
				<asp:LinkButton ID="cmdMoveCountryToActive" Runat="server">
					<asp:Image BorderWidth="0" ID="imgMoveCountryToActive" runat="server" ImageUrl="../images/add.jpg" AlternateText="Add"></asp:Image>
				</asp:LinkButton>
				&nbsp;</td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" colSpan="7" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<tr>
			<TD class="Content" width="1" colSpan="7">&nbsp;</TD>
		</tr>
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="ContentTableHeader" noWrap align="left" colSpan="4">&nbsp;&nbsp;Supported 
				States/Provinces&nbsp;
				<asp:Label id="SErrorMessage" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
			</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right" colspan="1" valign="top">Accept Orders From The 
				Following States/Provinces:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left" colspan="3" valign="top"><asp:ListBox Width="400" id="ActiveStates" runat="server"></asp:ListBox></td>
			<td class="content" align="middle" colspan="1" valign="bottom">
				<asp:LinkButton ID="cmdMoveStateToInactive" Runat="server">
					<asp:Image BorderWidth="0" ID="imgMoveStateToInactive" runat="server" ImageUrl="../images/remove.jpg" AlternateText="Remove"></asp:Image>
				</asp:LinkButton>
				&nbsp;</td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right" nowrap colspan="1">State 
				Name:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left" colspan="1"><asp:textbox id="NewStateName" runat="server" MaxLength="100"></asp:textbox></td>
			<td class="content" align="right" nowrap colspan="1">Abbreviation:&nbsp;</td>
			<td class="content" align="left" colspan="1"><asp:textbox id="NewStateAbbrev" runat="server" MaxLength="3"></asp:textbox></td>
			<td class="content" align="middle" colspan="1">
				<asp:LinkButton ID="cmdAddStateToActive" Runat="server">
					<asp:Image BorderWidth="0" ID="imgAddStateToActive" runat="server" ImageUrl="../images/add.jpg" AlternateText="Add"></asp:Image>
				</asp:LinkButton>
				&nbsp;</td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right" colspan="1" valign="top">Unsuported 
				States/Provinces:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left" colspan="3" valign="top"><asp:ListBox Width="400" id="InactiveStates" runat="server"></asp:ListBox></td>
			<td class="content" align="middle" colspan="1" valign="bottom">
				<asp:LinkButton ID="cmdDeleteState" Runat="server">
					<asp:Image BorderWidth="0" ID="imgDeleteState" runat="server" ImageUrl="../images/delete.jpg" AlternateText="Delete"></asp:Image>
				</asp:LinkButton>
				&nbsp;<br>
				<asp:LinkButton ID="cmdMoveStateToActive" Runat="server">
					<asp:Image BorderWidth="0" ID="imgMoveStateToActive" runat="server" ImageUrl="../images/add.jpg" AlternateText="Add"></asp:Image>
				</asp:LinkButton>
				&nbsp;</td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" colSpan="7" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<tr>
			<TD class="Content" width="1" colSpan="7">&nbsp;</TD>
		</tr>
		<TR>
			<td class="content" align="left" width="75%" colSpan="7">
				<asp:LinkButton ID="cmdSave" Runat="server">
					<asp:Image BorderWidth="0" ID="imgSave" runat="server" ImageUrl="../images/save.jpg" AlternateText="Save"></asp:Image>
				</asp:LinkButton>
			</td>
		</TR>
	</TBODY></TABLE>
