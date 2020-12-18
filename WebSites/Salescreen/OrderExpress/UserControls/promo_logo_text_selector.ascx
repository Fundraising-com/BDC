<%@ Register TagPrefix="obout" Namespace="OboutInc.Combobox" Assembly="obout_Combobox_Net" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.promo_logo_text_selector" Codebehind="promo_logo_text_selector.ascx.cs" %>

<table id="Table1">
	<tr>
		<td style="width: 196px"><asp:label id="Label2" CssClass="StandardLabel" runat="server">Linked&nbsp;Image&nbsp;and&nbsp;Promotion&nbsp;:</asp:label></td>
		<td><asp:checkbox id="chkShowLinked" runat="server"></asp:checkbox></td>
		<td></td>
	</tr>
	<tr>
		<td style="width: 196px"><asp:label id="Label11" CssClass="StandardLabel" runat="server">Image&nbsp;:</asp:label></td>
		<td>
			<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="400" border="0">
				<TR>
					<TD style="height: 24px"><asp:textbox id="txtImgID" runat="server" Enabled="True" Width="50px"></asp:textbox></TD>
					<TD style="height: 24px"><asp:comparevalidator id="CompValFMID" CssClass="LabelError" runat="server" ControlToValidate="txtImgID"
							ErrorMessage="The FM ID is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator></TD>
					<TD style="height: 24px">&nbsp;
						<asp:textbox id="txtImgDesc" runat="server" Enabled="True" Width="230px"></asp:textbox></TD>
					<td style="height: 24px"><asp:requiredfieldvalidator id="ReqFldVal_FMID" CssClass="LabelError" runat="server" Enabled="False" ControlToValidate="txtImgID"
							ErrorMessage="The FSM is required.">*</asp:requiredfieldvalidator></td>
					<TD vAlign="top" align="right" style="height: 24px"><asp:imagebutton id="imgBtnSelectImage" runat="server" ImageUrl="~/images/BtnSelect.gif" CausesValidation="False"></asp:imagebutton></TD>
				</TR>
			</TABLE>
		</td>
		<td></td>
	</tr>
	<tr>
		<td style="width: 196px"><asp:label id="Label1" CssClass="StandardLabel" runat="server">Offer&nbsp;:</asp:label></td>
		<td>
			<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="400" border="0">
				<TR>
					<TD><asp:textbox id="txtPromoID" runat="server" Enabled="True" Width="50px"></asp:textbox></TD>
					<TD><asp:comparevalidator id="Comparevalidator1" CssClass="LabelError" runat="server" ControlToValidate="txtPromoID"
							ErrorMessage="The FM ID is invalid (must be a number)." Type="Integer" Operator="DataTypeCheck">*</asp:comparevalidator></TD>
					<TD>&nbsp;
						<asp:textbox id="txtPromoDesc" runat="server" Enabled="True" Width="230px"></asp:textbox></TD>
					<td><asp:requiredfieldvalidator id="Requiredfieldvalidator1" CssClass="LabelError" runat="server" Enabled="False"
							ControlToValidate="txtPromoID" ErrorMessage="The FSM is required.">*</asp:requiredfieldvalidator></td>
					<TD vAlign="top" align="right"><img src="../images/BtnSelect.gif" id="imgBtnSelectText" OnClick="OpenPromo_TextSelector()"
							style="CURSOR: hand"></TD>
				</TR>
			</TABLE>
		</td>
		<td></td>
	</tr>
	<tr>
		<td vAlign="middle" align="right" style="width: 196px"><INPUT id="hidImg" style="WIDTH: 45px; HEIGHT: 22px" type="hidden" size="2" name="hidImg"
				runat="server"><INPUT id="hidPromoText" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="hidPromoText"
				runat="server"><asp:image id="imgPromo" Width="100px" ImageUrl="" Runat="server" Height="100px"></asp:image></td>
		<td><asp:textbox id="txtPromotion" Width="325px" Runat="server" Height="100px" MaxLength="1000" TextMode="MultiLine"
				Visible="False"></asp:textbox><asp:label id="lblPromotion" CssClass="DescLabelWhiteBackground" Width="325px" Runat="server"
				Height="100px" Font-Size="X-Small"></asp:label></td>
		<td vAlign="bottom" align="right"><asp:imagebutton id="imgBtnEdit" ImageUrl="~/images/btnEdit.gif" Runat="server" Visible="False"></asp:imagebutton></td>
	</tr>
</table>
<script language="javascript">

	var Promo_TextSelector;
	function OpenPromo_TextSelector() {
	
		
		if (Promo_TextSelector != null) {
			Promo_TextSelector.close();
		}
		
		var chkBox = document.getElementById('<%=this.chkShowLinked.ClientID%>');
		var ImgID = document.getElementById('<%=this.txtImgID.ClientID%>');
		
		var urlstr;
		urlstr = 'Promo_TextSelector.aspx?IDRefCtrl=<%=this.txtPromoID.ClientID%>&NameRefCtrl=<%=this.txtPromoDesc.ClientID%>';
		urlstr += '&hidTxtRefCtrl=<%=this.hidPromoText.ClientID%>&TxtRefCtrl=<%=this.lblPromotion.ClientID%>&lnkImg='+chkBox.checked;
		urlstr += '&imgID='+ImgID.value;
		
		Promo_TextSelector = window.open(urlstr,'','toolbars=yes,scrollbars=yes,width=650,height=700,resizable=yes',false);		
	}

</script>
