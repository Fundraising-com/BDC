<%@ Register TagPrefix="uc1" TagName="BuildYourOwnControl" Src="Controls/BuildYourOwnControl.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="BuildYourOwn.aspx.vb" Inherits="StoreFront.StoreFront.BuildYourOwn" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="CommonControls/RightColumnNav.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Detail -
			<% WriteProductName() %></title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="<%=ThemesPath()%>Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="general.js"></script>
		<script language="javascript">
			function ValidateSelection(){
				var e = document.getElementsByTagName("INPUT");
				for (i=0; i<e.length; i++){
					if (e[i].id.indexOf("hidSelectable") != -1){
						var selectable = parseInt(e[i].value);
						var stepName = document.getElementById(e[i].id.replace("hidSelectable", "hidStepName")).value;
						var chkList = document.getElementById(e[i].id.replace("hidSelectable", "stepDetails_productList")).getElementsByTagName('*');
						var selected = 0;
						for (j=0; j<chkList.length; j++){
							if (chkList[j].id.indexOf("checkProduct") != -1){
								if (chkList[j].checked){
									selected++;
								}
							}
						}
						if (selected < selectable){
							alert("Please select " + selectable + " item(s) in " + stepName + ".");
							return false;
						}
					}
				}
				return true;
			}
			function limitSelection(stepID, selectable, totalProducts, checkID)
			{
				var i=0,j=0;
				for (i=0;i<numberOfSteps;i++)
				{
					var varname = 'listOfSteps:_ctl' + i*2 + ':stepID';
					var selected = 0;
					if (window.document.Form2.elements[varname].value == stepID)
					{
						for (j=0;j<totalProducts;j++)
						{
							var checkName = 'listOfSteps:_ctl' + i*2 + ':stepDetails:productList:_ctl' + j + ':checkProduct';
							if (window.document.Form2.elements[checkName].checked == true)		
							{
								selected = selected + 1;
							}
						}
						if (selected > selectable)
						{
							window.document.Form2.elements[checkID].checked = false;
							alert('Please select only ' + selectable + ' product(s) in this step.\nTo select this item you must uncheck one of the others.');
							return false;
						}
					}
				}
				return false;
			}
		</script>
		<% Me.PageHeader %>
	</HEAD>
	<body class="GeneralPage" id="BodyTag" runat="server">
		<form id="Form2" method="post" runat="server">
			<table id="PageTable" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
				<tr>
					<td id="PageCell">
						<table id="PageSubTable" cellSpacing="0" cellPadding="0" width="100%" align="center" border="1"
							runat="server">
							<tr>
								<td class="TopBanner" id="TopBannerCell" width="100%" colSpan="3">
									<!-- Top Banner Start --><uc1:topbanner id="TopBanner1" runat="server"></uc1:topbanner>
									<!-- Top Banner End --></td>
							</tr>
							<tr>
								<td class="TopSubBanner" id="TopSubBannerCell" width="100%" colSpan="3">
									<!-- Top Sub Banner Start --><uc1:topsubbanner id="TopSubBanner1" runat="server"></uc1:topsubbanner>
									<!-- Top Sub Banner End --></td>
							</tr>
							<tr>
								<td class="LeftColumn" id="LeftColumnCell">
									<!-- Left Column Start --><uc1:leftcolumnnav id="LeftColumnNav1" runat="server"></uc1:leftcolumnnav>
									<!-- Left Column End --></td>
								<td class="Content" id="ContentCell" vAlign="top">
									<!-- Content Start -->
									<table id="Table2" cellSpacing="3" cellPadding="5" width="100%" border="0">
										<tr>
											<td>
												<!-- Instruction Start --><uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
												<!-- Instruction End --></td>
										</tr>
										<tr>
											<td class="Content">
												<P id="ErrorAlignment" align="center" runat="server"><asp:label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:label></P>
												<P id="MessageAlignment" align="center" runat="server"><asp:label id="Message" runat="server" CssClass="Messages" Visible="False"></asp:label></P>
											</td>
										</tr>
										<tr>
											<td>
												<asp:DataList ID="listOfSteps" Runat="server" ShowFooter="False" ShowHeader="False" Width="100%"
													ItemStyle-VerticalAlign="Top" RepeatDirection="Vertical">
													<ItemTemplate>
														<%-- Tee 8/27/2007 product configurator --%>
														<input id="hidSelectable" runat="server" type="hidden" value='<%# DataBinder.Eval(Container.DataItem, "SelectableQuantity") %>'>
														<input id="hidStepName" runat="server" type="hidden" value='<%# DataBinder.Eval(Container.DataItem, "StepName") %>'>
														<%-- end Tee --%>
														<asp:Label ID="stepNumber" Runat="server" CssClass="Content">
															<%#"Step " & DataBinder.Eval(Container.DataItem, "DisplayOrder") & "<br>Choose " & DataBinder.Eval(Container.DataItem, "SelectableQuantity") & " " & DataBinder.Eval(Container.DataItem, "StepName")%>
														</asp:Label>
														<div style="display:none">
															<asp:TextBox ID="stepID" Runat="server" CssClass="Content" Visible="True" Text='<%#DataBinder.Eval(Container.DataItem, "StepID")%>'>
															</asp:TextBox>
														</div>
														<uc1:BuildYourOwnControl id="stepDetails" runat="server" ProductDetails='<%#DataBinder.Eval(Container.DataItem, "ProductDetails")%>' StepID='<%#DataBinder.Eval(Container.DataItem, "StepID")%>' Selectable='<%#DataBinder.Eval(Container.DataItem, "SelectableQuantity")%>'>
														</uc1:BuildYourOwnControl>
													</ItemTemplate>
													<SeparatorTemplate>
														<hr>
													</SeparatorTemplate>
												</asp:DataList>
											</td>
										</tr>
										<tr>
											<td class="Content" align="right">
												<asp:LinkButton ID="btnContinue" Runat="server">
													<asp:Image ID="imgAddToCart" Runat="server" AlternateText="Add to cart" ImageAlign="Right"></asp:Image>
												</asp:LinkButton>
											</td>
										</tr>
									</table>
									<!-- Content End --></td>
								<td class="RightColumn" id="RightColumnCell">
									<!-- Right Column Start --><uc1:rightcolumnnav id="RightColumnNav1" runat="server"></uc1:rightcolumnnav>
									<!-- Right Column End --></td>
							</tr>
							<tr>
								<td colSpan="3" class="Footer" id="FooterCell">
									<!-- Footer Start -->
									<uc1:footer id="Footer1" runat="server"></uc1:footer>
									<!-- Footer End -->
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<%Me.RegisterJavascriptArray%>
		</form>
	</body>
</HTML>
