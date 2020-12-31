<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ControlerPhoneList.ascx.cs" Inherits="QSPFulfillment.AcctMgt.Control.ControlerPhoneList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="Phone" Src="../../Common/Phone.ascx" %>
<cc2:datagridobject id="dtgMain" allowpaging="True" cssclass="CSTableItems" searchmode="0" showfooter="True"
	autogeneratecolumns="False" runat="server" width="100%" datakeyfield="ID" bordercolor="#CCCCCC"
	borderstyle="None" borderwidth="1px" backcolor="White" cellpadding="3">
	<selecteditemstyle font-bold="True" forecolor="White" backcolor="#008A8C"></selecteditemstyle>
	<itemstyle forecolor="#000066" cssclass="CSSearchResult"></itemstyle>
	<headerstyle font-bold="True" forecolor="White" backcolor="#006699" cssclass="CSSearchResult"></headerstyle>
	<footerstyle forecolor="#000066" backcolor="White" cssclass="CSSearchResult"></footerstyle>
	<pagerstyle horizontalalign="Left" forecolor="#000066" backcolor="White" cssclass="CSPager"
		mode="NumericPages"></pagerstyle>
	<columns>
		<asp:templatecolumn headertext="Type">
			<itemtemplate>
				<asp:Label id="label1" runat="server" text='<%#DataBinder.Eval(Container, "DataItem.typedescription") %>' />
			</itemtemplate>
			<edititemtemplate>
				<asp:DropDownList ID="ddlType" Runat="server" SelectedValue='<%#DataBinder.Eval(Container, "DataItem.type") %>'>
					<asp:listitem value="30500" text="" />
					<asp:listitem value="30503" text="Fax" />
					<asp:listitem value="30502" text="Home" />
					<asp:listitem value="30507" text="Mobile/Cell Phone" />
					<asp:listitem value="30505" text="Main" />
					<asp:listitem value="30508" text="Office/Fax" />
					<asp:listitem value="30504" text="Other" />
					<asp:listitem value="30506" text="Pager" />
					<asp:listitem value="30510" text="Toll Free Line" />
					<asp:listitem value="30501" text="Work" />
				</asp:dropdownlist>
			</edititemtemplate>
			<footertemplate>
				<asp:dropdownlist id="ddlTypeInsert" runat="server">
					<asp:listitem value="30500" text="Select a value" selected="true" />
					<asp:listitem value="30503" text="Fax" />
					<asp:listitem value="30502" text="Home" />
					<asp:listitem value="30507" text="Mobile/Cell Phone" />
					<asp:listitem value="30505" text="Main" />
					<asp:listitem value="30508" text="Office/Fax" />
					<asp:listitem value="30504" text="Other" />
					<asp:listitem value="30506" text="Pager" />
					<asp:listitem value="30510" text="Toll Free Line" />
					<asp:listitem value="30501" text="Work" />
				</asp:dropdownlist>
			</footertemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Best Time To Call">
			<itemtemplate>
				<asp:Label id="label2" Text='<%#DataBinder.Eval(Container, "DataItem.BestTimeToCall") %>' runat="server" />
			</itemtemplate>
			<edititemtemplate>
				<asp:TextBox id="ctrlBestTime" Text='<%#DataBinder.Eval(Container, "DataItem.BestTimeToCall") %>' runat="server" />
			</edititemtemplate>
			<footertemplate>
				<asp:textbox id="ctrlBestTimeInsert" runat="server" />
			</footertemplate>
		</asp:templatecolumn>
		<asp:templatecolumn headertext="Phone Number">
			<itemtemplate>
				<asp:Label id="label3" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.Number") %>' />
			</itemtemplate>
			<edititemtemplate>
				<uc1:Phone id="ctrlPhone" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.Number") %>' />
			</edititemtemplate>
			<footertemplate>
				<uc1:phone id="ctrlPhoneInsert" runat="server" />
			</footertemplate>
		</asp:templatecolumn>
		<asp:templatecolumn>
			<itemtemplate>
				<asp:LinkButton runat="server" Text="Edit" CommandName="Edit" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.ID") %>' CausesValidation="false" ID="Linkbutton1" />
			</itemtemplate>
			<footertemplate>
				<asp:linkbutton runat="server" text="Insert" commandname="Insert" causesvalidation="true" />
			</footertemplate>
			<edititemtemplate>
				<asp:LinkButton runat="server" Text="Update" CommandName="Update" CausesValidation="true" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.ID") %>' ID="Linkbutton2" />&nbsp;
				<asp:linkbutton runat="server" text="Cancel" commandname="Cancel" causesvalidation="false" id="Linkbutton3" />
			</edititemtemplate>
		</asp:templatecolumn>
	</columns>
	<pagerstyle mode="NumericPages"></pagerstyle>
</cc2:datagridobject>
<asp:label id="lblMessage" runat="server" />
