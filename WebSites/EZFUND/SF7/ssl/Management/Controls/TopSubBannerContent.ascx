<%@ Register TagPrefix="uc1" TagName="SFExpressUploadControl" Src="SFExpressUploadControl.ascx"%>
<%@ Register TagPrefix="uc1" TagName="NavObjects" Src="NavObjects.ascx" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="TopSubBannerContent.ascx.vb" Inherits="StoreFront.StoreFront.TopSubBannerContent" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<table class="Content" width="100%" cellpadding="0" cellspacing="0">
	<tr>
		<td class="ContentTableHeader" colspan="3">Content
		</td>
	</tr>
	<tr>
		<td class="Contenttable" width="1"><IMG src="images/clear.gif"></td>
		<td class="ContentTableSubHeader"><asp:CheckBox ID="chkDisplayDynamicContent" Runat="server" Text="Display Dynamic Content" TextAlign="Right"></asp:CheckBox>
		</td>
		<td class="Contenttable" width="1"><IMG src="images/clear.gif"></td>
	</tr>
	<tr>
		<td class="Contenttable" width="1"><IMG src="images/clear.gif"></td>
		<td class="Content">&nbsp;
		</td>
		<td class="Contenttable" width="1"><IMG src="images/clear.gif"></td>
	</tr>
	<tr>
		<td class="Contenttable" width="1"><IMG src="images/clear.gif"></td>
		<td class="Content"><asp:RadioButton ID="PageName" Runat="server" GroupName="displayType" Text="Page Name" CssClass="Content"></asp:RadioButton></td>
		<td class="Contenttable" width="1"><IMG src="images/clear.gif"></td>
	</tr>
	<tr>
		<td class="Contenttable" width="1"><IMG src="images/clear.gif"></td>
		<td class="Content">&nbsp;
		</td>
		<td class="Contenttable" width="1"><IMG src="images/clear.gif"></td>
	</tr>
	<tr>
		<td colspan="3" class="Contenttable" height="1"><IMG src="images/clear.gif"></td>
	</tr>
	<tr>
		<td class="Contenttable" width="1"><IMG src="images/clear.gif"></td>
		<td class="Content">&nbsp;
		</td>
		<td class="Contenttable" width="1"><IMG src="images/clear.gif"></td>
	</tr>
	<tr>
		<td class="Contenttable" width="1"><IMG src="images/clear.gif"></td>
		<td class="Content"><asp:RadioButton ID="rbBannerImage" Runat="server" GroupName="displayType" Text="Banner Image" CssClass="Content"></asp:RadioButton>
			&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBannerImage" Runat="server"></asp:TextBox>
			<asp:ImageButton ID="btnBrowse" OnClick="UploadImage" ImageUrl="../Images/icon_browse.gif" Runat="server"></asp:ImageButton>
		</td>
		<td class="Contenttable" width="1"><IMG src="images/clear.gif"></td>
	</tr>
	<tr>
		<td class="Contenttable" width="1"><IMG src="images/clear.gif"></td>
		<td><uc1:SFExpressUploadControl id="ucUploadImage" Visible="False" runat="server"></uc1:SFExpressUploadControl></td>
		<td class="Contenttable" width="1"><IMG src="images/clear.gif"></td>
	</tr>
	<tr>
		<td class="Contenttable" width="1"><IMG src="images/clear.gif"></td>
		<td class="Content">&nbsp;
		</td>
		<td class="Contenttable" width="1"><IMG src="images/clear.gif"></td>
	</tr>
	<tr>
		<td colspan="3" class="Contenttable" height="1"><IMG src="images/clear.gif"></td>
	</tr>
	<tr>
		<td class="Contenttable" width="1"><IMG src="images/clear.gif"></td>
		<td class="Content">&nbsp;
		</td>
		<td class="Contenttable" width="1"><IMG src="images/clear.gif"></td>
	</tr>
	<tr>
		<td class="Contenttable" width="1"><IMG src="images/clear.gif"></td>
		<td class="Content"><asp:RadioButton ID="NavigationalObjects" Runat="server" GroupName="displayType" Text="Navigational Objects"
				CssClass="Content"></asp:RadioButton></td>
		<td class="Contenttable" width="1"><IMG src="images/clear.gif"></td>
	</tr>
	<tr>
		<td class="Contenttable" width="1"><IMG src="images/clear.gif"></td>
		<td class="Content">
			<table border="0" cellpadding="3" cellspacing="3" width="100%">
				<tr>
					<td><uc1:NavObjects id="NavObjects1" runat="server"></uc1:NavObjects></td>
				</tr>
			</table>
		</td>
		<td class="Contenttable" width="1"><IMG src="images/clear.gif"></td>
	</tr>
	<tr class="Content">
		<td class="Contenttable" width="1"><IMG src="images/clear.gif"></td>
		<td class="Content">&nbsp;
		</td>
		<td class="Contenttable" width="1"><IMG src="images/clear.gif"></td>
	</tr>
	<tr>
		<td colspan="3" class="Contenttable" height="1"><IMG src="images/clear.gif"></td>
	</tr>
</table>
