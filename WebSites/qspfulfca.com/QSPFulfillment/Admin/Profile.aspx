<%@ Page debug="true" language="c#" Codebehind="Profile.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.Admin.Profile" %>
<%@ Register  TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Profile Update</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<style type="text/css">
			.group { FONT-WEIGHT: bold; FONT-SIZE: 90%; COLOR: #2f4f88; FONT-FAMILY: Verdana }
			.noBorder { BORDER-RIGHT: 0px solid; BORDER-TOP: 0px solid; BORDER-LEFT: 0px solid; BORDER-BOTTOM: 0px solid }
			.mand { FONT-WEIGHT: bold; COLOR: red }
		</style>
	</HEAD>
	<body topmargin="0" leftmargin="0" marginheight="0" marginwidth="0">
		<form runat="server" ID="Form1">
			<!-- #include file="../Includes/Menu.inc" -->
			<table id="tbl1" cellSpacing="3" cellPadding="3" width="80%" align="center" border="0">
				<tr>
					<td class="label" align="left">
						<asp:label id="lblFName" runat="server" />
					</td>
				</tr>
				<tr>
					<td class="label" align="left">
						<asp:label id="lblMsg" runat="server" />
					</td>
				</tr>
				<tr>
					<td class="label" align="left">
						<asp:label id="lblMsg2" runat="server" Text="" />
						<asp:label id="lblMsg3" runat="server" Text=""></asp:label>
						<asp:label id="lblRequired" runat="server" Text="" Visible="False"></asp:label>
					</td>
				</tr>
				<tr>
					<td align="left">
						<asp:Label Runat="server" ForeColor="RoyalBlue" Font-Bold="True" id="Label1">
						Please Note: You may now submit address/phone number changes.<br />
						All changes are distributed within 48 business hours to QSP Home Office, RDA, EDS, BMG, etc.
						</asp:Label>
					</td>
				</tr>
			</table>
			<asp:repeater id="RepeaterAddr" Runat="server" EnableViewState="True" OnItemDataBound="RepeaterAddr_ItemDataBound">
				<ItemTemplate>
					<table width="80%" align="center" border="0" cellspacing="3" cellpadding="3">
						<tr>
							<td colspan="2" align="left"><span class="mand">* - Required Fields</span></td>
						</tr>
						<tr>
							<td colspan="2" align="left"><span class="group">Personal</span></td>
						</tr>
						<tr>
							<td colspan="2" align="left"></td>
						</tr>
						<asp:PlaceHolder ID="plh_authenticationInfo" Runat="server">
							<tr>
								<td align="right" class="label">User Name<span class="mand">*</span>:</td>
								<td class="field" align="left">
									<asp:TextBox ID="txtusername" Columns="20" MaxLength="20" OnTextChanged="lblMessageChanged" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UserName")%>' />
									<asp:RequiredFieldValidator runat="server" id="rq_username" ControlToValidate="txtusername" ErrorMessage="Please provide User Name!"
										display="Dynamic" />
								</td>
							</tr>
							<tr>
								<td align="right" class="label">Password<span class="mand">*</span>:</td>
								<td align="left" class="field">
									<asp:TextBox ID="txtPassword" Columns="20" MaxLength="20" OnTextChanged="lblMessageChanged" Runat="server" value='<%# DataBinder.Eval(Container.DataItem, "Password")%>' />
									<asp:RequiredFieldValidator runat="server" id="rq_password" ControlToValidate="txtPassword" ErrorMessage="Please provide a Password!"
										display="Dynamic" />
								</td>
							</tr>
							<tr>
								<td align="right" class="label">Verify Password<span class="mand">*</span>:</td>
								<td align="left" class="field">
									<asp:TextBox ID="txtVerifyPassword" Columns="20" MaxLength="20" OnTextChanged="lblMessageChanged" Runat="server" value='<%# DataBinder.Eval(Container.DataItem, "Password")%>' />
									<asp:RequiredFieldValidator runat="server" id="rq_verifypassword" ControlToValidate="txtVerifyPassword" ErrorMessage="Please Verify your Password!"
										display="Dynamic" />
								</td>
							</tr>
						</asp:PlaceHolder>
						<tr>
							<td align="right" class="label">First Name:</td>
							<td align="left" class="field">
								<asp:TextBox ID="txtFirstName" Columns="20" ReadOnly=True CssClass="noBorder" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FirstName")%>' />
							</td>
						</tr>
						<tr>
							<td align="right" class="label">Last Name<span class="mand">*</span>:</td>
							<td align="left" class="field">
								<asp:TextBox ID="txtLastName" Columns="30" MaxLength="30" OnTextChanged="lblMessageChanged" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LastName")%>' />
								<asp:RequiredFieldValidator runat="server" id="rq_LastName" ControlToValidate="txtLastName" ErrorMessage="Please enter your Last Name!"
									display="Dynamic" />
							</td>
						</tr>
						<tr>
							<td align="right" class="label">E-mail Address:</td>
							<td align="left" class="field">
								<asp:TextBox ID="txtEmail" Columns="50" MaxLength="50" Runat="server" OnTextChanged="lblMessageChanged" Text='<%# DataBinder.Eval(Container.DataItem, "Email")%>' />
							</td>
						</tr>
						<tr>
							<td align="right" class="label">Spouse / Significant Other:</td>
							<td align="left" class="field">
								<asp:TextBox ID="txtSigOther" Columns="30" MaxLength="30" OnTextChanged="lblMessageChanged" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SignificantOther")%>' />
							</td>
						</tr>
						<tr>
							<td colspan="2" align="left"><span class="group">Phones</span></td>
						</tr>
						<tr>
							<td colspan="2" align="left"></td>
						</tr>
						<tr>
							<td align="right" class="label">Voice Mail Extension:</td>
							<td align="left" class="field">
								<asp:TextBox ID="txtVoiceMail" Columns="4" MaxLength="4" Runat="server" OnTextChanged="lblMessageChanged" Text='<%# DataBinder.Eval(Container.DataItem, "VoiceMailExt")%>' />
							</td>
						</tr>
						<tr>
							<td align="right" class="label">Home Phone:</td>
							<td align="left" class="field">
								<asp:TextBox ID="txtHomePhone" Columns="20" MaxLength="20" Runat="server" OnTextChanged="lblMessageChanged" Text='<%# DataBinder.Eval(Container.DataItem, "HomePhone")%>'/>
								<asp:RegularExpressionValidator id="reg_HomePhone" ErrorMessage="Please enter your phone number, area code included, without formatting."
									runat="server" Display="Dynamic" ControlToValidate="txtHomePhone" ValidationExpression="\(?\s*\d{3}\s*[\)\.\-]?\s*\d{3}\s*[\-\.]?\s*\d{4}" />
							</td>
						</tr>
						<tr>
							<td align="right" class="label">Work Phone:</td>
							<td align="left" class="field">
								<asp:TextBox ID="txtWorkPhone" Columns="20" MaxLength="20" Runat="server" OnTextChanged="lblMessageChanged" Text='<%# DataBinder.Eval(Container.DataItem, "WorkPhone")%>' />
								<asp:RegularExpressionValidator id="Reg_WorkPhone" ErrorMessage="Please enter your phone number, area code included, without formatting."
									runat="server" Display="Dynamic" ControlToValidate="txtWorkPhone" ValidationExpression="\(?\s*\d{3}\s*[\)\.\-]?\s*\d{3}\s*[\-\.]?\s*\d{4}" />
							</td>
						</tr>
						<tr>
							<td align="right" class="label">Fax Number:</td>
							<td align="left" class="field">
								<asp:TextBox ID="txtFax" Columns="20" MaxLength="20" Runat="server" OnTextChanged="lblMessageChanged" Text='<%# DataBinder.Eval(Container.DataItem, "FAXPhone")%>' />
								<asp:RegularExpressionValidator id="Reg_FAX" ErrorMessage="Please enter your phone number, area code included, without formatting."
									runat="server" Display="Dynamic" ControlToValidate="txtFAX" ValidationExpression="\(?\s*\d{3}\s*[\)\.\-]?\s*\d{3}\s*[\-\.]?\s*\d{4}" />
							</td>
						</tr>
						<tr>
							<td align="right" class="label">Toll Free Number:</td>
							<td align="left" class="field">
								<asp:TextBox ID="txtTollFreeNbr" Columns="20" MaxLength="20" Runat="server" OnTextChanged="lblMessageChanged" Text='<%# DataBinder.Eval(Container.DataItem, "TollFreePhone")%>' />
								<asp:RegularExpressionValidator id="Reg_800" ErrorMessage="Please enter your phone number, area code included, without formatting."
									runat="server" Display="Dynamic" ControlToValidate="txtTollFreeNbr" ValidationExpression="\(?\s*\d{3}\s*[\)\.\-]?\s*\d{3}\s*[\-\.]?\s*\d{4}" />
							</td>
						</tr>
						<tr>
							<td align="right" class="label">Cell Phone:</td>
							<td align="left" class="field">
								<asp:TextBox ID="txtMobilePhone" Columns="20" MaxLength="20" Runat="server" OnTextChanged="lblMessageChanged" Text='<%# DataBinder.Eval(Container.DataItem, "MobilePhone")%>' />
								<asp:RegularExpressionValidator id="Reg_MobilePhone" ErrorMessage="Please enter your phone number, area code included, without formatting."
									runat="server" Display="Dynamic" ControlToValidate="txtMobilePhone" ValidationExpression="\(?\s*\d{3}\s*[\)\.\-]?\s*\d{3}\s*[\-\.]?\s*\d{4}" />
							</td>
						</tr>
						<tr>
							<td align="right" class="label">Pager:</td>
							<td align="left" class="field">
								<asp:TextBox ID="txtPager" Columns="20" MaxLength="20" Runat="server" OnTextChanged="lblMessageChanged" Text='<%# DataBinder.Eval(Container.DataItem, "PagerPhone")%>' />
								<asp:RegularExpressionValidator id="Reg_Pager" ErrorMessage="Please enter your phone number, area code included, without formatting."
									runat="server" Display="Dynamic" ControlToValidate="txtPager" ValidationExpression="\(?\s*\d{3}\s*[\)\.\-]?\s*\d{3}\s*[\-\.]?\s*\d{4}" />
							</td>
						</tr>
						<tr>
							<td colspan="2" align="left"><span class="group">Mailing Address</span></td>
						</tr>
						<tr>
							<td colspan="2" align="left"></td>
						</tr>
						<tr>
							<td align="right" class="label">Street Address 1:</td>
							<td align="left" class="field">
								<asp:TextBox ID="txtMailAddr1" Columns="50" MaxLength="50" Runat="server" OnTextChanged="lblMessageChanged" Text='<%# DataBinder.Eval(Container.DataItem, "MailAddress1")%>' />
							</td>
						</tr>
						<tr>
							<td align="right" class="label">Street Address 2:</td>
							<td align="left" class="field">
								<asp:TextBox ID="txtMailAddr2" Columns="50" MaxLength="50" Runat="server" OnTextChanged="lblMessageChanged" Text='<%# DataBinder.Eval(Container.DataItem, "MailAddress2")%>' />
							</td>
						</tr>
						<tr>
							<td align="right" class="label">City:</td>
							<td align="left" class="field">
								<asp:TextBox ID="txtMailCity" Columns="30" MaxLength="30" Runat="server" OnTextChanged="lblMessageChanged" Text='<%# DataBinder.Eval(Container.DataItem, "MailCity")%>' />
							</td>
						</tr>
						<tr>
							<td align="right" class="label">State:</td>
							<td align="left" class="field">
								<asp:TextBox ID="txtMailState" Columns="2" MaxLength="2" Runat="server" OnTextChanged="lblMessageChanged" Text='<%# DataBinder.Eval(Container.DataItem, "MailState")%>' />
							</td>
						</tr>
						<tr>
							<td align="right" class="label">Postal Code:</td>
							<td align="left" class="field">
								<asp:TextBox ID="txtMailPostalCode" Columns="10" MaxLength="10" Runat="server" OnTextChanged="lblMessageChanged" Text='<%# DataBinder.Eval(Container.DataItem, "MailPostalCode")%>' />
							</td>
						</tr>
						<tr>
							<td colspan="2" align="left"><span class="group">Shipping Address</span></td>
						</tr>
						<tr>
							<td colspan="2" align="left"></td>
						</tr>
						<tr>
							<td align="right" class="label">Street Address 1:</td>
							<td align="left" class="field">
								<asp:TextBox ID="txtShipAddr1" Columns="50" MaxLength="50" Runat="server" OnTextChanged="lblMessageChanged" Text='<%# DataBinder.Eval(Container.DataItem, "ShipAddress1")%>' />
							</td>
						</tr>
						<tr>
							<td align="right" class="label">Street Address 2:</td>
							<td align="left" class="field">
								<asp:TextBox ID="txtShipAddr2" Columns="50" MaxLength="50" Runat="server" OnTextChanged="lblMessageChanged" Text='<%# DataBinder.Eval(Container.DataItem, "ShipAddress2")%>' />
							</td>
						</tr>
						<tr>
							<td align="right" class="label">City:</td>
							<td align="left" class="field">
								<asp:TextBox ID="txtShipCity" Columns="30" MaxLength="30" Runat="server" OnTextChanged="lblMessageChanged" Text='<%# DataBinder.Eval(Container.DataItem, "ShipCity")%>' />
							</td>
						</tr>
						<tr>
							<td align="right" class="label">State:</td>
							<td align="left" class="field">
								<asp:TextBox ID="txtShipState" Columns="2" MaxLength="2" Runat="server" OnTextChanged="lblMessageChanged" Text='<%# DataBinder.Eval(Container.DataItem, "ShipState")%>' />
							</td>
						</tr>
						<tr>
							<td align="right" class="label">Postal Code:</td>
							<td align="left" class="field">
								<asp:TextBox ID="txtShipPostalCode" Columns="10" MaxLength="10" Runat="server" OnTextChanged="lblMessageChanged" Text='<%# DataBinder.Eval(Container.DataItem, "ShipPostalCode")%>' />
							</td>
						</tr>
						<tr>
							<td colspan="2" align="left"><span class="group">Prize Invoice Contact Info</span></td>
						</tr>
						<tr>
							<td colspan="2" align="left"></td>
						</tr>
						<tr>
							<td align="right" class="label">Street Address 1:</td>
							<td align="left" class="field">
								<asp:TextBox ID="txtInvAddr1" Columns="50" MaxLength="50" Runat="server" OnTextChanged="lblMessageChanged" Text='<%# DataBinder.Eval(Container.DataItem, "InvAddress1")%>' />
							</td>
						</tr>
						<tr>
							<td align="right" class="label">Street Address 2:</td>
							<td align="left" class="field">
								<asp:TextBox ID="txtInvAddr2" Columns="50" MaxLength="50" Runat="server" OnTextChanged="lblMessageChanged" Text='<%# DataBinder.Eval(Container.DataItem, "InvAddress2")%>' />
							</td>
						</tr>
						<tr>
							<td align="right" class="label">City:</td>
							<td align="left" class="field">
								<asp:TextBox ID="txtInvCity" Columns="30" MaxLength="30" Runat="server" OnTextChanged="lblMessageChanged" Text='<%# DataBinder.Eval(Container.DataItem, "InvCity")%>' />
							</td>
						</tr>
						<tr>
							<td align="right" class="label">State:</td>
							<td align="left" class="field">
								<asp:TextBox ID="txtInvState" Columns="2" MaxLength="2" Runat="server" OnTextChanged="lblMessageChanged" Text='<%# DataBinder.Eval(Container.DataItem, "InvState")%>' />
							</td>
						</tr>
						<tr>
							<td align="right" class="label">Postal Code:</td>
							<td align="left" class="field">
								<asp:TextBox ID="txtInvPostalCode" Columns="10" MaxLength="10" Runat="server" OnTextChanged="lblMessageChanged" Text='<%# DataBinder.Eval(Container.DataItem, "InvPostalCode")%>' />
							</td>
						</tr>
						<tr>
							<td align="right" class="label">Phone:</td>
							<td align="left" class="field">
								<asp:TextBox ID="txtInvPhone" Columns="20" MaxLength="20" Runat="server" OnTextChanged="lblMessageChanged" Text='<%# DataBinder.Eval(Container.DataItem, "InvPhone")%>' />
								<asp:RegularExpressionValidator id="Reg_InvPhone" ControlToValidate="txtInvPhone" ErrorMessage="Please enter your phone number, area code included, without formatting."
									runat="server" Display="Dynamic" ValidationExpression="\(?\s*\d{3}\s*[\)\.\-]?\s*\d{3}\s*[\-\.]?\s*\d{4}" />
							</td>
						</tr>
						<tr>
							<td colspan="2" align="left"><span class="group">Prize Invoice Settings</span></td>
						</tr>
						<tr>
							<td colspan="2" align="left"></td>
						</tr>
						<tr>
							<td align="right" class="label">Default Term:</td>
							<td align="left" class="field">
								<asp:TextBox ID="txtDefaultTerm" Columns="2" MaxLength="2" Runat="server" OnTextChanged="lblMessageChanged" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceTerm")%>' />
							</td>
						</tr>
						<tr>
							<td align="right" class="label">Make Invoice Checks Payable To:</td>
							<td align="left" class="field">
								<asp:TextBox ID="txtInvoiceChecks" Columns="50" MaxLength="50" Runat="server" OnTextChanged="lblMessageChanged" Text='<%# DataBinder.Eval(Container.DataItem, "MakeCheckPayableTo")%>' />
							</td>
						</tr>
						<tr>
							<td align="right" class="label">Default Message:</td>
							<td align="left" class="field">
								<asp:TextBox ID="txtDefaultMsg1" TextMode=MultiLine Rows=5 Columns=25 runat="server" OnTextChanged="lblMessageChanged" Text='<%# DataBinder.Eval(Container.DataItem, "DefaultInvMsg1")%>' />
							</td>
						</tr>
						<tr>
							<td align="right" class="label">Default Message:</td>
							<td align="left" class="field">
								<asp:TextBox ID="txtDefaultMsg2" TextMode=MultiLine Rows=5 Columns=25 MaxLength="50" Runat="server" OnTextChanged="lblMessageChanged" Text='<%# DataBinder.Eval(Container.DataItem, "DefaultInvMsg2")%>' />
							</td>
						</tr>
						<tr>
							<td align="right" class="label">Default Message:</td>
							<td align="left" class="field">
								<asp:TextBox ID="txtDefaultMsg3" TextMode=MultiLine Rows=5 Columns=25 MaxLength="50" Runat="server" OnTextChanged="lblMessageChanged" Text='<%# DataBinder.Eval(Container.DataItem, "DefaultInvMsg3")%>' />
							</td>
						</tr>
						<tr>
							<td colspan="2" align="left"><span class="group">Account Track Directory Settings</span></td>
						</tr>
						<tr>
							<td colspan="2" align="left"></td>
						</tr>
						<tr>
							<td align="right" class="label">Time Zone:</td>
							<td align="left" class="field">
								<asp:TextBox ID="TimeZoneTB" Runat=server Visible=False Text=' <% # DataBinder.Eval(Container.DataItem, "TimeZone") %>' />
								<asp:DropDownList ID="TimeZoneDDL" Runat="server">
									<asp:ListItem Value="0" Text="Please select a TimeZone" />
									<asp:ListItem Value="-4" Text="Atlantic Standard Zone" />
									<asp:ListItem Value="-5" Text="Eastern Time Zone" />
									<asp:ListItem Value="-6" Text="Central Time Zone" />
									<asp:ListItem Value="-7" Text="Mountain Time Zone" />
									<asp:ListItem Value="-8" Text="Pacific Time Zone" />
									<asp:ListItem Value="-9" Text="Alaska Standard Time" />
									<asp:ListItem Value="-10" Text="Hawaiian Time Zone" />
								</asp:DropDownList>
							</td>
						</tr>
						<tr>
							<td align="right" class="label">Day Light Savings observance:</td>
							<td align="left" valign="middle" class="field">
								<asp:CheckBox ID="cb_DST" Runat="server" OnCheckedChanged="lblMessageChanged" Checked='<%# DataBinder.Eval(Container.DataItem, "DST")%>' />
							</td>
						</tr>
						<tr>
							<td colspan="2" align="center" valign="top">
								<span style='color:blue;font-size:smaller'>(In most areas in the US, Daylight 
									Savings Time is observed;
									<br />
									this value <span style='color:red;text-decoration:italic'>normally</span> shouldn't 
									need to change.)</span>
							</td>
						</tr>
					</table>
				</ItemTemplate>
			</asp:repeater>
			<table cellSpacing="3" cellPadding="3" width="80%" align="center" border="0">
				<tr>
					<td class="button" align="center"><asp:button id="btnSubmit" runat="server" Text="Submit"></asp:button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
