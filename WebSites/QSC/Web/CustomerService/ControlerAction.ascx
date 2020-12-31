<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ControlerAction.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerAction" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="False"%>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<script language="javascript">
	/*var pageSwitchStateID = CustomerServicePage.SavePageSwitchStateTest("hello").value;
	alert(pageSwitchStateID);*/
</script>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0">
	<TR>
		<TD colspan="2">
			<table cellpadding="0" cellspacing="0" border="0">
				<tr>
					<td><asp:HyperLink Runat="server" NavigateUrl="javascript:Action(2);" ImageUrl='<%#"images/shortcuts_01"+(hypNew.Enabled==false?"_disabled":"") + ".gif"%>'
							id="hypNew"></asp:HyperLink></td>
					<td><asp:HyperLink Runat="server" NavigateUrl="javascript:Action(1);" ImageUrl='<%#"images/shortcuts_02"+(hypCancel.Enabled==false?"_disabled":"") + ".gif"%>'
							id="hypCancel"></asp:HyperLink></td>
					<td><asp:HyperLink Runat="server" NavigateUrl="javascript:Action(7);" ImageUrl='<%#"images/shortcuts_03"+(hypNotByPhone.Enabled==false?"_disabled":"") + ".gif"%>'
							id="hypNotByPhone"></asp:HyperLink></td>
					<td><asp:HyperLink Runat="server" NavigateUrl="javascript:Action(3);" ImageUrl='<%#"images/shortcuts_04"+(hylRefund.Enabled==false?"_disabled":"") + ".gif"%>'
							id="hylRefund"></asp:HyperLink></td>
					<td><asp:HyperLink Runat="server" NavigateUrl="javascript:Action(8);" ImageUrl='<%#"images/shortcuts_05"+(hypNoAction.Enabled==false?"_disabled":"") + ".gif"%>'
							id="hypNoAction"></asp:HyperLink></td>
					<td><asp:HyperLink Runat="server" NavigateUrl="javascript:Action(4);" ImageUrl='<%#"images/shortcuts_06"+(hypCHADD.Enabled==false?"_disabled":"") + ".gif"%>'
							id="hypCHADD"></asp:HyperLink></td>
				</tr>
			</table>
			<br>
		</TD>
	</TR>
	<TR>
		<td><img src="images/spacer.gif" width="4" border="0"></td>
		<TD>
			<asp:Label id="Label2" runat="server" CssClass="csPlainText"> Actions List:<br><br></asp:Label></TD>
	</TR>
	<TR>
		<td>&nbsp;</td>
		<TD>
			<asp:DropDownList id="ddlAction" runat="server" Width="215px"></asp:DropDownList></TD>
	</TR>
	<TR>
		<td>&nbsp;</td>
		<TD>
			<asp:HyperLink Runat="server" NavigateUrl="javascript:Action(0);" id="hylGo">GO</asp:HyperLink>
		</TD>
	</TR>
</TABLE>
