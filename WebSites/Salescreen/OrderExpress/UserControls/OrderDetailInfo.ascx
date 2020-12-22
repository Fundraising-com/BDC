<%@ Reference Control="~/UserControls/ToolBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="OrderInfo" Src="OrderInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AuditControlInfo" Src="AuditControlInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ToolBar" Src="~/UserControls/ToolBar.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrderDetailInfo" Codebehind="OrderDetailInfo.ascx.cs" %>

<table cellPadding="4" width="100%">
	<tr id="trCampInfoTitle" runat="server">
		<td align="left"> <!--Section Body --><br>
			<table border=0 cellpadding=0 cellspacing=0>
	            <tr>
	                <td>
                        <asp:Image ID="imgBusinessForm" runat="server" Height=80px />
	                </td>
	                <td>
	                    &nbsp;&nbsp;
	                </td>
	                <td>
	                    <table id="tblCampInfoTitle" cellSpacing="0" cellPadding="0" border="0">
				            <tr id="trAccountInfoTitle" runat="server">
					            <td><asp:label id="Label2" runat="server" CssClass="FormTitleLabel"> Account :
						            </asp:label></td>
					            <td><asp:label id="lblAccountNumber" runat="server" CssClass="FormTitleDescLabel">
							            00000
						            </asp:label></td>
					            <td>&nbsp;-&nbsp;
					            </td>
					            <td><asp:label id="lblAccountName" runat="server" CssClass="FormTitleDescLabel">
							            Account Name
						            </asp:label></td>
				            </tr>
				            <tr id="trFormInfoTitle" runat="server">
					            <td><asp:label id="Label3" runat="server" CssClass="FormTitleLabel"> Order Form :
						            </asp:label></td>
					            <td align="right"><asp:label id="lblFormID" runat="server" CssClass="FormTitleDescLabel" >
							            
						            </asp:label></td>
					            <td>&nbsp;-&nbsp;
					            </td>
					            <td><asp:label id="lblFormName" runat="server" CssClass="FormTitleDescLabel" >
							            Unspecified
						            </asp:label></td>
				            </tr>
			            </table>
	                </td>
	            </tr>
	        </table>
			<asp:ImageButton id="imgBtnPrint" runat="server" ImageUrl="~/images/btnPrint.jpg" ImageAlign="Right"
				Visible="False"></asp:ImageButton>
			
		</td>
	</tr>
	<tr>
		<td>
			<uc1:orderinfo id="OrderInfo1" runat="server"></uc1:orderinfo>
		</td>
	</tr>
	<tr>
		<td align="center"><br>
			<uc1:AuditControlInfo id="AuditControlInfo1" HideHistoryLink=False runat="server"></uc1:AuditControlInfo>
			<br>
		</td>
	</tr>
	<tr>
		<td align="center">
            &nbsp;</td>
	</tr>
	<tr>
		<td align="center">
			<TABLE cellSpacing="0" cellPadding="2" width="100%" border="0">
	            <TR>
		            <TD>
			            <HR width="100%" SIZE="2">
		            </TD>
	            </TR>
	            <TR id="trReadOnlyMode" runat="Server" >
		            <TD align="center">
			            <table border="0" cellpadding="0" cellspacing="0">
				            <tr>
					            <td align="center">
						            <asp:ImageButton ID=imgEditOrder ImageUrl="~/images/btnEditOrder.gif" runat=server OnClick="imgEditOrder_Click" />
					            </td>
					            <td>
						            &nbsp;&nbsp;
					            </td>
					            <td align="center">
						            <asp:ImageButton ID=imgEditOrderPE ImageUrl="~/images/BtnEditPersonalizationOnly.gif" runat=server OnClick="imgEditOrderPE_Click" />
					            </td>
					            <td>
					                &nbsp;&nbsp;
					            </td>
					            <td align="center">
					                <asp:HyperLink id="hypLnkClose" runat="server" ImageUrl="~/images/btnClose.gif" NavigateUrl="javascript:window.close();">Close</asp:HyperLink>
					            </td>
				            </tr>
			            </table>
		            </TD>
	            </TR>
	            <TR>
		            <TD>
			            <HR width="100%" SIZE="2">
		            </TD>
	            </TR>
            </TABLE>
		</td>
	</tr>
</table>
