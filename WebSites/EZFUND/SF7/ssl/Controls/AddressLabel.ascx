<%@ Control Language="vb" AutoEventWireup="false" Codebehind="AddressLabel.ascx.vb" Inherits="StoreFront.StoreFront.AddressLabel" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<asp:DataList id="DataList1" runat="server">
	<ItemTemplate>
		<TABLE id="Table2" cellSpacing="0" cellPadding="0" border="0">
			<TR>
				<TD class="Content" noWrap><%#  DataBinder.Eval(Container.DataItem,"Name") %></TD>
			</TR>
			<TR>
				<TD class="Content" noWrap><%#  DataBinder.Eval(Container.DataItem,"Company") %></TD>
			</TR>
			<TR>
				<TD class="Content" noWrap><%#  DataBinder.Eval(Container.DataItem,"Address1") %></TD>
			</TR>
			<TR>
				<TD class="Content" noWrap><%#  DataBinder.Eval(Container.DataItem,"Address2") %></TD>
			</TR>
			<TR>
				<TD class="Content" noWrap><%#  DataBinder.Eval(Container.DataItem,"City") %>
					,&nbsp;
					<%#  DataBinder.Eval(Container.DataItem,"State") %>
					&nbsp;&nbsp;
					<%#  DataBinder.Eval(Container.DataItem,"Zip") %>
				</TD>
			</TR>
			<TR>
				<TD class="Content" noWrap><%#  DataBinder.Eval(Container.DataItem,"CountryName") %></TD>
			</TR>
			<TR>
				<TD class="Content" noWrap><%#  DataBinder.Eval(Container.DataItem,"Phone") %></TD>
			</TR>
			<TR>
				<TD class="Content" noWrap><%#  DataBinder.Eval(Container.DataItem,"Fax") %></TD>
			</TR>
			<TR>
				<TD class="Content">&nbsp;</TD>
			</TR>
		</TABLE>
	</ItemTemplate>
</asp:DataList>
