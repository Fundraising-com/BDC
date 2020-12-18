<%@ import namespace="Microsoft.Web.UI.WebControls" %>
<%@ Register TagPrefix="uc1" TagName="SearchModuleSelector" Src="SearchModuleSelector.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="qsp" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.ExpressionBuilder" Codebehind="ExpressionBuilder.ascx.cs" %>


<asp:Label id="lblError" runat="server" Visible="False" Font-Bold="True" ForeColor="Red" Font-Size="xx-small"></asp:Label>
<TABLE id="Table5" cellSpacing="0" cellPadding="0" border="0" width="100%">
	<TR align="left" valign="top" height="200">
		<TD align="left" width="200" class="SimpleBorder" bgcolor="#ffffff">
			<div class="ExpressionBuilderCell">
			    <!--ChildFunction="onclick='doClick(this)';"Text="Variables"-->
				<asp:TreeView runat="server" ID="tvBusinessRule" NodeIndent="10" Font-Names="Arial" Font-Size=XX-Small OnSelectedNodeChanged="tvBusinessRule_SelectedNodeChanged" ShowLines="True">
					 <Nodes >
					    <asp:TreeNode text="Variables" Value="Variables" >		
					        <asp:TreeNode text="[MinDayLeadTime]" Value="[MinDayLeadTime]"></asp:TreeNode>
					            <asp:TreeNode text="[MinTotalAmount]" Value="[MinTotalAmount]"></asp:TreeNode>
					            <asp:TreeNode text="[MinDayLeadTime]" Value="[MinDayLeadTime]"></asp:TreeNode>
					            <asp:TreeNode text="[MinTotalQuantity]" Value="[MinTotalQuantity]"></asp:TreeNode>
					            <asp:TreeNode text="[IsNewAccount]" Value="[IsNewAccount]"></asp:TreeNode>
					            <asp:TreeNode text="[IsCommonCarrier]" Value="[IsCommonCarrier]"></asp:TreeNode>
					            <asp:TreeNode text="[IsWareHouse]" Value="[IsWareHouse]"></asp:TreeNode>
					            <asp:TreeNode text="[IsNewCreditApp]" Value="[IsNewCreditApp]"></asp:TreeNode>
					            <asp:TreeNode text="[MinTotalAmount]" Value="[MinTotalAmount]"></asp:TreeNode>
					            <asp:TreeNode text="[MinTotalQuantity]" Value="[MinTotalQuantity]"></asp:TreeNode>
					            <asp:TreeNode text="[IsWareHouse]" Value="[IsWareHouse]"></asp:TreeNode>
					            <asp:TreeNode text="[IsCommonCarrier]" Value="[IsCommonCarrier]"></asp:TreeNode>
					            <asp:TreeNode text="[MinDayLeadTime_W]" Value="[MinDayLeadTime_W]"></asp:TreeNode>
					            <asp:TreeNode text="[Supply_MinDayLeadTime]" Value="[Supply_MinDayLeadTime]"></asp:TreeNode>
					            <asp:TreeNode text="[IsNewAccount]" Value="[IsNewAccount]"></asp:TreeNode>
					            <asp:TreeNode text="[IsNewOrder]" Value="[IsNewOrder]"></asp:TreeNode>
					            <asp:TreeNode text="[Supply_MinDayLeadTime]" Value="[Supply_MinDayLeadTime]"></asp:TreeNode>
					            <asp:TreeNode text="[IsNewOrder]" Value="[IsNewOrder]"></asp:TreeNode>
					            <asp:TreeNode text="[IsPublicSchool]" Value="[IsPublicSchool]"></asp:TreeNode>
					            <asp:TreeNode text="[IsCatholicSchool]" Value="[IsCatholicSchool]"></asp:TreeNode>
					            <asp:TreeNode text="[IsCreditApproved]" Value="[IsCreditApproved]"></asp:TreeNode>
					            <asp:TreeNode text="[MinInactiveMonth]" Value="[MinInactiveMonth]"></asp:TreeNode>
					            <asp:TreeNode text="[IsTaxExempted]" Value="[IsTaxExempted]"></asp:TreeNode>
					            <asp:TreeNode text="[IsApproved]" Value="[IsApproved]"></asp:TreeNode>
					            <asp:TreeNode text="[IsNoCreditRequired]" Value="[IsNoCreditRequired]"></asp:TreeNode>
					            <asp:TreeNode text="[IsMaxTotAmount]" Value="[IsMaxTotAmount]"></asp:TreeNode>
					            <asp:TreeNode text="[IsRequiredCreditCheck]" Value="[IsRequiredCreditCheck]"></asp:TreeNode>
					            <asp:TreeNode text="[IsMaxTotAmount]" Value="[IsMaxTotAmount]"></asp:TreeNode>
					            <asp:TreeNode text="[IsReceived]" Value="[IsReceived]"></asp:TreeNode>			        
					    </asp:TreeNode>
					</Nodes>
                    <ParentNodeStyle Font-Bold="False" />
                    <HoverNodeStyle BackColor="#CCCCCC" BorderColor="#888888" BorderStyle="Solid" Font-Underline="True" />
                    <SelectedNodeStyle BackColor="White" BorderColor="#888888" BorderStyle="Solid" BorderWidth="1px"
                        Font-Underline="False" HorizontalPadding="3px" VerticalPadding="1px" />
                    <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                        NodeSpacing="1px" VerticalPadding="2px" />
				</asp:TreeView>
			</div>
		</TD>
		<td width="200" class="SimpleBorder" bgcolor="#ffffff">
			<div class="ExpressionBuilderCell">
				<asp:TreeView runat="server" ID="tvOperation" NodeIndent="10" Font-Names="Arial" Font-Size=8pt OnSelectedNodeChanged="tvOperation_SelectedNodeChanged" ShowLines="True">
					<Nodes>
					    <asp:TreeNode text="Operations" Value="Operations">
							    <asp:TreeNode text="Simple" Value="Simple">
								    <asp:TreeNode text="+" Value="+"></asp:TreeNode>
								    <asp:TreeNode text="-" Value="-"></asp:TreeNode>
								    <asp:TreeNode text="*" Value="*"></asp:TreeNode>
								    <asp:TreeNode text="/" Value="/"></asp:TreeNode>
								    <asp:TreeNode text="%" Value="%"></asp:TreeNode>
							    </asp:TreeNode>
							    <asp:TreeNode text="Agregate" Value="Agregate">
								    <asp:TreeNode text="Sum()" Value="Sum()"></asp:TreeNode>
								    <asp:TreeNode text="Avg()" Value="Avg()"></asp:TreeNode>
								    <asp:TreeNode text="Min()" Value="Min()"></asp:TreeNode>
								    <asp:TreeNode text="Max()" Value="Max()"></asp:TreeNode>
								    <asp:TreeNode text="Count()" Value="Count()"></asp:TreeNode>
								    <asp:TreeNode text="StDev()" Value="StDev()"></asp:TreeNode>
								    <asp:TreeNode text="Var()" Value="Var()"></asp:TreeNode>
							    </asp:TreeNode>
							    <asp:TreeNode text="Logical" Value="Logical">
								    <asp:TreeNode text="&lt;" Value="&lt;"></asp:TreeNode>
								    <asp:TreeNode text="&gt;" Value="&gt;"></asp:TreeNode>
								    <asp:TreeNode text="&lt;=" Value="&lt;="></asp:TreeNode>
								    <asp:TreeNode Text="&amp;lte;" Value="&amp;lte;"></asp:TreeNode>
								    <asp:TreeNode text="&lt;&gt;" Value="&lt;&gt;"></asp:TreeNode>
								    <asp:TreeNode text="=" Value="="></asp:TreeNode>
								    <asp:TreeNode text="~" Value="~"></asp:TreeNode>
								    <asp:TreeNode text="IN" Value="IN"></asp:TreeNode>
								    <asp:TreeNode text="LIKE" Value="LIKE"></asp:TreeNode>
								    <asp:TreeNode text="&amp;" Value="&amp;"></asp:TreeNode>
								    <asp:TreeNode text="|" Value="|"></asp:TreeNode>
								    <asp:TreeNode text="^" Value="^"></asp:TreeNode>
							    </asp:TreeNode>
							    <asp:TreeNode text="Text" Value="Text">
								    <asp:TreeNode text="\n" Value="\n"></asp:TreeNode>
								    <asp:TreeNode text="\t" Value="\t"></asp:TreeNode>
								    <asp:TreeNode text="\r" Value="\r"></asp:TreeNode>
								    <asp:TreeNode text="(" Value="("></asp:TreeNode>
								    <asp:TreeNode text=")" Value=")"></asp:TreeNode>
								    <asp:TreeNode text="[" Value="["></asp:TreeNode>
								    <asp:TreeNode text="]" Value="]"></asp:TreeNode>
								    <asp:TreeNode text="[]" Value="[]"></asp:TreeNode>
								    <asp:TreeNode text="'" Value="'"></asp:TreeNode>
								    <asp:TreeNode text='&quot;' Value="&quot;"></asp:TreeNode>
								    <asp:TreeNode text="#" Value="#"></asp:TreeNode>
							    </asp:TreeNode>
							</asp:TreeNode>
					</Nodes>
                    <ParentNodeStyle Font-Bold="False" />
                    <HoverNodeStyle BackColor="#CCCCCC" BorderColor="#888888" BorderStyle="Solid" Font-Underline="True" />
                    <SelectedNodeStyle BackColor="White" BorderColor="#888888" BorderStyle="Solid" BorderWidth="1px"
                        Font-Underline="False" HorizontalPadding="3px" VerticalPadding="1px" />
                    <NodeStyle Font-Names="Verdana" Font-Size="xx-small" ForeColor="Black" HorizontalPadding="5px"
                        NodeSpacing="1px" VerticalPadding="2px" />
				</asp:TreeView>
			</div>
		</td>
		<td valign="top" align="left" width="200" class="SimpleBorder" bgcolor="#ffffff">
			<div class="ExpressionBuilderCell">
				<asp:TreeView runat="server" ID="tvFunction" NodeIndent="10"  Font-Names="Arial" Font-Size=XX-Small OnSelectedNodeChanged="tvFunction_SelectedNodeChanged" ShowLines="True">
					<Nodes>
					    <asp:TreeNode Text="Functions" Value="Functions">
						    <asp:TreeNode text="CONVERT()" Value="CONVERT()"></asp:TreeNode>
						    <asp:TreeNode text="LEN()" Value="LEN()"></asp:TreeNode>
						    <asp:TreeNode text="ISNULL()" Value="ISNULL()"></asp:TreeNode>
						    <asp:TreeNode text="IIF()" Value="IIF()"></asp:TreeNode>
						    <asp:TreeNode text="TRIM()" Value="TRIM()"></asp:TreeNode>
						    <asp:TreeNode text="SUBSTRING()" Value="SUBSTRING()"></asp:TreeNode>
					    </asp:TreeNode>
					  </Nodes>
                    <ParentNodeStyle Font-Bold="False" />
                    <HoverNodeStyle BackColor="#CCCCCC" BorderColor="#888888" BorderStyle="Solid" Font-Underline="True" />
                    <SelectedNodeStyle BackColor="White" BorderColor="#888888" BorderStyle="Solid" BorderWidth="1px"
                        Font-Underline="False" HorizontalPadding="3px" VerticalPadding="1px" />
                    <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                        NodeSpacing="1px" VerticalPadding="2px" />
				</asp:TreeView>
			</div>
		</td>
	</TR>
	<tr>
		<td colspan="3">
			<asp:TextBox CssClass="SimpleBorder" id="txtExpression" runat="server" TextMode="MultiLine" Width="600"
				Height="128px"></asp:TextBox>
		</td>
	</tr>
	<TR>
		<TD colspan="3" align="center"><br>
			<TABLE cellSpacing="0" cellPadding="0" border="0" id="Table1">
				<TR>
					<td><asp:imagebutton id="imgBtnOK" runat="server" Visible="true" AlternateText="Click here to confirm your selection"
							ImageUrl="~/images/btnOK.gif" CausesValidation="False"></asp:imagebutton></td>
					<td><asp:hyperlink id="hypLnkCancel" runat="server" Visible="False" ImageUrl="~/images/BtnCancel.gif"
							NavigateUrl="javascript:window.close();">Cancel</asp:hyperlink></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>
<script>
	function doClick(obj)
	{
		var txt = obj.innerHTML.replace("&lt;","<").replace("&gt;",">").replace("&amp;","&");
		insertAtCursor("<%=txtExpression.ClientID%>",txt);
	}
		
	function insertAtCursor(myFieldID, myValue) 
	{	
		var myField = document.getElementById(myFieldID);
		if (!document.selection) 
		{			
			myField.focus();
			sel = document.selection.createRange();
			sel.text = " "+myValue.replace(/&nbsp;/g," ");;
		}
		else if (myField.selectionStart || myField.selectionStart == '0') 
		{
			var startPos = myField.selectionStart;
			var endPos = myField.selectionEnd;
			myField.value = myField.value.substring(0, startPos)
			+ " "+myValue.replace(/&nbsp;/g," ");
			+ myField.value.substring(endPos, myField.value.length);
		} 
		else 
		{
			myField.value += " "+myValue.replace(/&nbsp;/g," ");
		}
	}
</script>
