<%@ Register TagPrefix="uc1" TagName="NavObjects" Src="NavObjects.ascx" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="Content.ascx.vb" Inherits="StoreFront.StoreFront.Content" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<table class="Content" cellSpacing="0" cellPadding="0" width="100%">
	<tr>
		<td colSpan="3" class="ContentTableHeader">Content
		</td>
	</tr>
	<tr>
		<td class="Contenttable" width="1"><IMG src="images/clear.gif"></td>
		<td class="Content">&nbsp;
		</td>
		<td class="Contenttable" width="1"><IMG src="images/clear.gif"></td>
	</tr>
	<tr>
		<td class="Contenttable" width="1"><IMG src="images/clear.gif"></td>
		<td class="Content">&nbsp;&nbsp;Navigational Objects</td>
		<td class="Contenttable" width="1"><IMG src="images/clear.gif"></td>
	</tr>
	<tr>
		<td class="Contenttable" width="1"><IMG src="images/clear.gif"></td>
		<td class="Content">
			<table border="0" cellpadding="3" cellspacing="3" width="100%">
				<tr>
					<td><uc1:navobjects id="NavObjects1" runat="server"></uc1:navobjects></td>
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
		<td class="Contenttable" colSpan="3" height="1"><IMG src="images/clear.gif"></td>
	</tr>
</table>
