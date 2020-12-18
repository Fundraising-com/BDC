<%@ Reference Control="OrderStep_Confirmation.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrderStep_Personalization" Codebehind="OrderStep_Personalization.ascx.cs" %>

<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
    <TR>
	    <TD>
	    </TD>
    </TR>
    <tr id="trCampInfoTitle" runat="server">
	    <td align="left"> <!--Section Body --><br>
	        <table border=0 cellpadding=0 cellspacing=0>
                <tr>
                    <td>
                        <asp:Image ID="imgBusinessForm" Height=80px runat="server" />
                    </td>
                    <td>
                        &nbsp;&nbsp;
                    </td>
                    <td>
                        <table id="tblCampInfoTitle" cellSpacing="0" cellPadding="0" border="0">
			                <tr id="trAccountInfoTitle" runat="server">
				                <td><asp:label id="Label1" runat="server" CssClass="FormTitleLabel"> Account :
					                </asp:label></td>
				                <td><asp:label id="lblAccountNumber" runat="server" CssClass="FormTitleDescLabel">
						                00000
					                </asp:label></td>
				                <td>&nbsp;-&nbsp;
				                </td>
				                <td><asp:label id="lblAccountName" runat="server" CssClass="FormTitleDescLabel" >
						                Account Name
					                </asp:label></td>
			                </tr>
			                <tr id="trFormInfoTitle" runat="server">
				                <td><asp:label id="Label2" runat="server" CssClass="FormTitleLabel"> Order Form :
					                </asp:label></td>
				                <td align="right"><asp:label id="lblFormID" runat="server" CssClass="FormTitleDescLabel">
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
				    <td>
					    <iframe id="frPersonalization" runat="server" frameBorder="no" height="450" width="930">
					    </iframe>
				    </td>
			    </tr>
		    </table>
	    </td>
    </TR>
    <TR>
        <TD align="center"><br>
	        <TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
		        <TR>
			        <td>
			        </td>
			        <td align="right">
				        <TABLE id="tblSave" runat="server" cellSpacing="0" cellPadding="0" width="535" border="0">
                            <tr>
                                <td align="right" colspan="3">
                                    <asp:Label ID="Label3" runat="server" CssClass="StandardLabel" ForeColor="Red" Text="Note:  Be sure to click on 'Save Personalization' button to avoid losing personalization selections."></asp:Label>
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
								        ImageUrl="~/images/btnSubmitOrder.gif" CausesValidation="False" ToolTip="Click here to submit and process your order" OnClick="imgBtnConfirm_Click"></asp:imagebutton>
						        </td>
					        </TR>
				        </TABLE>
			        </td>
		        </TR>
	        </TABLE>
        </TD>
	</TR>
</TABLE>
