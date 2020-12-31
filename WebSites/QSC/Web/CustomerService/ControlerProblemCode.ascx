<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ControlerProblemCode.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerProblemCode" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<script language="javascript">

	function SetProblemCode(PCID,Description)
	{
		self.close();
		window.opener.SetProblemCode(PCID,Description);
	
	}

</script>
<asp:Label runat="server" id="lblMessage"></asp:Label>
<cc2:DataGridObject id="dtgMain" DataKeyField="Instance" width="100%" runat="server" AutoGenerateColumns="False"
										AllowPaging="True" 
										BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
		BackColor="White" CellPadding="3">
	<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
	<ItemStyle ForeColor="#000066" cssClass="CSSearchResult"></ItemStyle>
	<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#006699" cssClass="CSSearchResult"></HeaderStyle>
	<FooterStyle ForeColor="#000066" BackColor="White"  cssClass="CSSearchResult"></FooterStyle>
	<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White"  cssClass="CSPager" Mode="NumericPages"></PagerStyle>
	<COLUMNS>
		<ASP:TEMPLATECOLUMN HeaderText="Problem Code">
			<ITEMTEMPLATE>
				<asp:Label id="Label1" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.Instance")%>'>
				</asp:Label>
			</ITEMTEMPLATE>
			<FOOTERTEMPLATE>
				<asp:TextBox id="tbxInstanceInsert" runat="server" Visible="False"></asp:TextBox>
			</FOOTERTEMPLATE>
			<EDITITEMTEMPLATE>
				<asp:Label id="lblProblemCodeID" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.Instance")%>'>
				</asp:Label>
			</EDITITEMTEMPLATE>
		</ASP:TEMPLATECOLUMN>
		<ASP:TEMPLATECOLUMN HeaderText="Description">
			<ITEMTEMPLATE>
				<asp:Label id="Label2" runat="server" Text='<%#DataBinder.Eval (Container,"DataItem.Description")%>'>
				</asp:Label>
			</ITEMTEMPLATE>
			<FOOTERTEMPLATE>
				<asp:TextBox id="tbxDescriptionInsert" runat="server"></asp:TextBox>
			</FOOTERTEMPLATE>
			<EDITITEMTEMPLATE>
				<asp:TextBox id="tbxDescriptionUpdate" runat="server" Text='<%#DataBinder.Eval (Container,"DataItem.Description")%>'>
				</asp:TextBox>
			</EDITITEMTEMPLATE>
		</ASP:TEMPLATECOLUMN>
		<ASP:TEMPLATECOLUMN>
			<ITEMTEMPLATE>
				<asp:LinkButton id="LinkButton1" runat="server" Text="Edit" CommandName="Edit" CausesValidation="false"></asp:LinkButton>
			</ITEMTEMPLATE>
			<FOOTERTEMPLATE>
				<asp:LinkButton id="lbtnInsert" runat="server" CommandName="Insert">Insert</asp:LinkButton>
			</FOOTERTEMPLATE>
			<EDITITEMTEMPLATE>
<asp:LinkButton id="LinkButton3" runat="server" Text="Update" CommandName="Update" CommandArgument='<%#DataBinder.Eval(Container,"DataItem.Instance")%>'>
				</asp:LinkButton>&nbsp; 
<asp:LinkButton id="LinkButton2" runat="server" Text="Cancel" CommandName="Cancel" CommandArgument='<%#DataBinder.Eval(Container,"DataItem.Instance")%>' CausesValidation="false">
				</asp:LinkButton></EDITITEMTEMPLATE>
		</ASP:TEMPLATECOLUMN>
		<asp:TemplateColumn visible="false">
			<ItemTemplate>
				<a href='javascript:void(0);' onclick="<%#"javascript:SetProblemCode("+DataBinder.Eval (Container,"DataItem.Instance")+",'"+DataBinder.Eval (Container,"DataItem.Description")+"');"%>">
					Select</a>
			</ItemTemplate>
			<EditItemTemplate></EditItemTemplate>
		</asp:TemplateColumn>
	</COLUMNS>
</cc2:DataGridObject>
