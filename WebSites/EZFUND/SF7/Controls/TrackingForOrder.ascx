<%@ Control Language="vb" AutoEventWireup="false" Codebehind="TrackingForOrder.ascx.vb" Inherits="StoreFront.StoreFront.TrackingForOrder" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<%@ Register TagPrefix="uc1" TagName="TrackingForOrderAddress" Src="TrackingForOrderAddress.ascx" %>
<asp:datalist id="DataList1" runat="server" Width="100%">
	<ItemTemplate>
		<TABLE cellSpacing="0" cellPadding="0" width="100%">
			<TR>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="ContentTableHeader" colspan=1 width="100%">Ship To: <%# DataBinder.Eval(Container.DataItem,"NickName") %>
				</TD>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="Content" colSpan="1">
					<uc1:TrackingForOrderAddress id="TrackingForOrderAddress1" runat="server" Order=<%# databinder.eval(me,"Order")%> AddressID='<%# DataBinder.Eval(Container.DataItem,"ID") %>'></uc1:TrackingForOrderAddress></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
		</TABLE>
		<br><br>
	</ItemTemplate>
</asp:datalist>
<% me.WriteUPS %>
<asp:Panel ID=UPS Runat=server Visible=false>
<table width='100%' border=0>
<tr>
               
<td class='Content' width='1' align=left valign=top><img src='images/LOGO_S.gif'></td>
<td class='Content' align=left valign=top>UPS®, UPS &amp; Shield Design® and UNITED PARCEL SERVICE® are
<BR>registered trademarks of United Parcel Service of America, Inc.
<br><br>NOTICE: The UPS package tracking systems accessed via this Web Site (the "Tracking Systems") 
and tracking information obtained through this Web Site (the "Information") are the private
property of UPS.  UPS authorizes you to use the Tracking Systems solely to track
shipments tendered by or for you to UPS for delivery and for no other purpose.  Without limitation,
you are not authorized to make the information available on any web site or otherwise reproduce,
distribute, copy, store, use, or sell the information for commercial gain without the express
written consent of UPS.  This is a personal service, thus your right to use the Tracking System
or Information is non-assignable.  Any access or use that is inconsistent with these terms is
unauthorized and strictly prohibited.
 </td>
 </tr>
 </table>
</asp:Panel>