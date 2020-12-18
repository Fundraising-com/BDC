<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrderAwardVisionDetail" Codebehind="OrderAwardVisionDetail.ascx.cs" %>

<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
    <tr>
        <td>
            <table border=0 cellpadding=0 cellspacing=0>
	            <tr>
	                <td>
                        <asp:Image Height=80px ID="imgBusinessForm" runat="server" />
	                </td>
                    <td>
                        &nbsp;&nbsp;
                    </td>
	                <td>
	                    <table id="tblCampInfoTitle" cellSpacing="0" cellPadding="0" border="0">
				            <tr id="trAccountInfoTitle" runat="server">
					            <td><asp:label id="Label2" runat="server" CssClass="FormTitleLabel"> Account :
						            </asp:label></td>
					            <td><asp:label id="lblAccountNumber" runat="server" CssClass="FormTitleDescLabel" >
							            00000
						            </asp:label></td>
					            <td>&nbsp;-&nbsp;
					            </td>
					            <td><asp:label id="lblAccountName" runat="server" CssClass="FormTitleDescLabel" >
							            Account Name
						            </asp:label></td>
				            </tr>
				            <tr id="trFormInfoTitle" runat="server">
					            <td><asp:label id="Label3" runat="server" CssClass="FormTitleLabel"> Order Form :
						            </asp:label></td>
					            <td align="right"><asp:label id="lblFormID" runat="server" CssClass="FormTitleDescLabel" >
							            23
						            </asp:label></td>
					            <td>&nbsp;-&nbsp;
					            </td>
					            <td><asp:label id="lblFormName" runat="server" CssClass="FormTitleDescLabel" >
							            WFC WarehouseStock Order Form
						            </asp:label></td>
				            </tr>
			            </table>
	                </td>
	            </tr>
	        </table>
        </td>
    </tr>
    
	<TR>
		<td align="left"> <!--Section Body -->
			<table id="Table4" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td style="height: 600px">
						<iframe id="frPersonalization" runat="server" frameBorder="no" height="600" width="930">
						</iframe>
					</td>
				</tr>
			</table>
		</td>
	</TR>
	<tr>
		<td align="center">
			<table border="0" cellpadding="0" cellspacing="0" width="100%">
				<tr>
				    <td align="center">
						<asp:HyperLink id="hypLnkCancel" runat="server" ImageUrl="~/images/btnCancel.gif" NavigateUrl="javascript:window.close();">Cancel</asp:HyperLink>
					</td>
					<td align="right">
					    <TABLE id="tblSave" runat="server" cellSpacing="0" cellPadding="0" width="535" border="0">
                            <tr>
                                <td align="right" colspan="3">
                                    <asp:Label ID="Label1" runat="server" CssClass="StandardLabel" ForeColor="Red" Text="Note:  Be sure to click on 'Save Personalization' button to avoid losing personalization selections."></asp:Label>
                                    <br />
                                    <br />
                                </td>
                                <td align="right" valign="top">
                                </td>
                            </tr>
					        <TR>
                                <td style="width: 110px;">
                                    &nbsp;
                                </td>
						        <td align="right">
							        <asp:label id="lblSaveForLaterDesc" runat="server" CssClass="StandardLabel">
								        Save . . . Hold Order/Process Later
							        </asp:label>
						        </td>
						        <td align="right" valign="middle">
							        <asp:imagebutton id="imgBtnSaveForLater" runat="server" AlternateText="Click here to save for later process"
								        OnClick="imgBtnSaveForLater_Click" ImageUrl="~/images/btnSaveOrder.gif" CausesValidation="False"></asp:imagebutton><br>
							        <br>
						        </td>
					        </TR>
					        <TR id=trConfirmationButton runat=server>
                                <td style="width: 110px;">
                                    &nbsp;
                                </td>
						        <td align="right">
							        <asp:label id="lblConfirmDesc" runat="server" CssClass="StandardLabel">
								        Submit . . . Process Order Immediately
							        </asp:label>
						        </td>
						        <td align="right" valign="middle">
							        <asp:imagebutton id="imgBtnConfirm" runat="server" AlternateText="Click here to confirm your order"
								        ImageUrl="~/images/btnSubmitOrder.gif" CausesValidation="False" 
                                        ToolTip="Click here to submit and process your order" 
                                        OnClick="imgBtnConfirm_Click" style="height: 22px"></asp:imagebutton>
						        </td>
					        </TR>
				        </TABLE>
					</td>
					
				</tr>
			</table>
		</td>
	</tr>
</TABLE>
