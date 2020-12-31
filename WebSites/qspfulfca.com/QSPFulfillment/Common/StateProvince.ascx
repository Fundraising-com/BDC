<%@ Control Language="c#" AutoEventWireup="True" Codebehind="StateProvince.ascx.cs" Inherits="QSPFulfillment.CommonWeb.UC.StateProvince" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<asp:DropDownList ID=DDL Runat=server>
	<asp:ListItem Value="" Text="" />
	<asp:ListItem Value="AB" Text="Alberta" />
	<asp:ListItem Value="BC" Text="British Columbia" />
	<asp:ListItem Value="MB" Text="Manitoba" />
	<asp:ListItem Value="NB" Text="New Brunswick" />
	<asp:ListItem Value="NL" Text="Newfoundland and Labrador" />
	<asp:ListItem Value="NT" Text="Northwest Territories" />
	<asp:ListItem Value="NS" Text="Nova Scotia" />
	<asp:ListItem Value="NU" Text="Nunavut" />
	<asp:ListItem Value="ON" Text="Ontario" />
	<asp:ListItem Value="PE" Text="Prince Edward Island" />
	<asp:ListItem Value="QC" Text="Quebec" />
	<asp:ListItem Value="SK" Text="Saskatchewan" />
	<asp:ListItem Value="YT" Text="Yukon" />
</asp:DropDownList>
<asp:RequiredFieldValidator id="VAL_RQ" runat="server" ControlToValidate="DDL" Display="Dynamic" Text="*" ErrorMessage="Please select a state or province." />