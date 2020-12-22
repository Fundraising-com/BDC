<%@ Control Language="vb" AutoEventWireup="false" Codebehind="CSRAttributes.ascx.vb" Inherits="StoreFront.StoreFront.CSRAttributes" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<TABLE class="Content" cellSpacing="0" cellPadding="0" width="100%">
	<TR class="Content" vAlign="top" runat="server" ID="Tr1">
		<TD class="Content" vAlign="top" runat="server" ID="Td1"><asp:datalist id="DlAttributes" runat="server">
				<SelectedItemStyle Wrap="False"></SelectedItemStyle>
				<EditItemStyle Wrap="False"></EditItemStyle>
				<AlternatingItemStyle Wrap="False"></AlternatingItemStyle>
				<ItemStyle Wrap="False"></ItemStyle>
				<ItemTemplate>
					<asp:TextBox id=AttributeID runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"UID")%>' Visible="False">
					</asp:TextBox>
					<asp:TextBox id=ErrorName runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Name")%>' Visible="False">
					</asp:TextBox>
					<asp:Label id="attName1" Visible="False" CssClass="Content" Runat="server">
						<%#DataBinder.Eval(Container.DataItem,"Name")%>
					</asp:Label>
					<asp:DropDownList id="AttributeName" required=true AutoPostBack="False" DataValueField="UID" DataTextField="Name_Price_Info"
						CssClass="Content" Runat="server"></asp:DropDownList>
					
				</ItemTemplate>
			</asp:datalist></TD>
	</TR>
	<TR class="Content" vAlign="top">
		<TD class="Content" vAlign="top"><asp:datalist id="dlCustomAttributes" runat="server">
				<ItemTemplate>
					<asp:TextBox id="CustomAttributeID" runat="server" Visible=False Text='<%#DataBinder.Eval(Container.DataItem,"AttributeId")%>'>
					</asp:TextBox>
					<asp:TextBox id="CustomRequired" runat="server" Visible=False Text='<%#DataBinder.Eval(Container.DataItem,"Required")%>'>
					</asp:TextBox>
					<asp:TextBox id="attName" runat="server" Visible=False Text='<%#DataBinder.Eval(Container.DataItem,"Name")%>'>
					</asp:TextBox>
					<asp:TextBox id="CustomDetailID" runat="server" Visible=False Text='<%#DataBinder.Eval(Container.DataItem,"UID")%>'>
					</asp:TextBox>
					<asp:Label id="lblDescription" Runat="server" CssClass="Content">
						<%#DataBinder.Eval(Container.DataItem,"Name_Price_Info")%>
					</asp:Label>
					<BR>
					<asp:TextBox required=<%#DataBinder.Eval(Container.DataItem,"required")%> id="txtCustom" Runat="server" CssClass="Content" MaxLength="100"></asp:TextBox>
				</ItemTemplate>
			</asp:datalist></TD>
	</TR>
</TABLE>
