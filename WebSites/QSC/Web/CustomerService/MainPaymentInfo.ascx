<%@ Control Language="c#" AutoEventWireup="True" Codebehind="MainPaymentInfo.ascx.cs" Inherits="QSPFulfillment.CustomerService.MainPaymentInfo" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="ControlerPaymentInfo" Src="ControlerPaymentInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ControlerSubscriptionForCOHI" Src="ControlerSubscriptionForCOHI.ascx" %>
<br>
<table width="80%">
	<tr>
		<td align="left" valign="top">
			<table width="60%">
				<tr>
					<td>
						<uc1:ControlerPaymentInfo id="ctrlControlerPaymentInfo" runat="server" ShowCreditCardStatus="true"></uc1:ControlerPaymentInfo>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<TR>
		<TD vAlign="top" align="left" width="50%"><br>
			<TABLE id="tblCreditCard" runat="server" cellSpacing="0" cellPadding="1" width="100%" border="1">
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="0" width="100%" bgColor="#ffffff" border="0">
							<TR>
								<TD class="CSTableHeader" noWrap align="center">Subscriptions&nbsp;paid by this 
									Credit Card</TD>
							</TR>
							<TR>
								<TD align="center">
									<uc1:ControlerSubscriptionForCOHI id="ctrlControlerSubscriptionForCOHI" runat="server" ShowCheckBoxesForCHADD="false"></uc1:ControlerSubscriptionForCOHI></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</table>
