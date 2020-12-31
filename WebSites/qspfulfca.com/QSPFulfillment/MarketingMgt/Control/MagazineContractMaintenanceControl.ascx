<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../../Common/DateEntry.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="MagazineContractMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.MagazineContractMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%"  >
	<TR>
		<TR width=100%>
			<TD vAlign="top" align="left"><iewc:tabstrip id="tbsMainPage" runat="server" TargetID="mupMainPage" Height="40" SelectedIndex="0"
					TabSelectedStyle="background-color :#ffffff;color:#000000" TabHoverStyle="background-color:#777777" TabDefaultStyle="background-color:#000064;font-family:verdana;&#13;&#10;&#9;&#9;&#9;&#9;&#9;font-weight:bold;font-size:8pt;color:#ffffff;width:79;height:21;&#13;&#10;&#9;&#9;&#9;&#9;&#9;text-align:center">
					<iewc:Tab Text="General" ID="iewcGeneral" Enabled="TRUE"></iewc:Tab>
					<iewc:Tab Text="Pricing-Remit" Enabled="TRUE"></iewc:Tab>
					<iewc:Tab Text="Catalog-printer" Enabled="TRUE"></iewc:Tab>
				</iewc:tabstrip></TD>
		</TR>
		<tr>
			<td colSpan="2"><iewc:multipage id="mupMainPage" runat="server">
					<iewc:pageview id="pavGeneral">
						<table class="CSTable">
							<tr>
								<td><br>
									<asp:label id="Label1" runat="server" cssclass="csPlainText">Product Code </asp:label></td>
								<td><br>
									<asp:label id="lblProductCode" runat="server" cssclass="csPlainText"></asp:label></td>
							</tr>
							<tr>
								<td>
									<asp:label id="Label2" runat="server" cssclass="csPlainText">Product Name </asp:label></td>
								<td>
									<asp:label id="lblProductName" runat="server" cssclass="csPlainText"></asp:label></td>
							</tr>
							<tr>
								<td>
									<asp:label id="Label5" runat="server" cssclass="csPlainText">Year </asp:label></td>
								<td>
									<asp:label id="lblYear" runat="server" cssclass="csPlainText"></asp:label></td>
							</tr>
							<tr>
								<td>
									<asp:label id="Label6" runat="server" cssclass="csPlainText">Season </asp:label></td>
								<td>
									<asp:label id="lblSeason" runat="server" cssclass="csPlainText"></asp:label></td>
							</tr>
							<tr>
								<td>
									<asp:label id="Label9" runat="server" cssclass="csPlainText"> Product Contract Status *</asp:label></td>
								<td>
									<asp:radiobuttonlist id="rblContractStatus" runat="server" cssclass="csPlainText" repeatdirection="Horizontal">
										<asp:listitem value="30600" selected="True">Active</asp:listitem>
										<asp:listitem value="30601">Inactive</asp:listitem>
										<asp:listitem value="30602">Pending</asp:listitem>
									</asp:radiobuttonlist></td>
							</tr>
							<tr>
								<td>
									<asp:label id="Label25" runat="server" cssclass="csPlainText">Form Received</asp:label></td>
								<td>
									<asp:radiobuttonlist id="rblContractFormReceived" runat="server" cssclass="csPlainText" repeatdirection="Horizontal">
										<asp:listitem value="1">Yes</asp:listitem>
										<asp:listitem value="0" selected="True">No</asp:listitem>
									</asp:radiobuttonlist></td>
							</tr>
							<tr>
								<td style="HEIGHT: 22px"></td>
								<td style="HEIGHT: 22px"></td>
							</tr>
							<tr>
								<td>
									<asp:label id="Label8" runat="server" cssclass="csPlainText">Effective Date *</asp:label></td>
								<td>
									<uc1:dateentry id="dteEffectiveDate" runat="server" required="true"></uc1:dateentry></td>
							</tr>
							<tr>
								<td>
									<asp:label id="Label14" runat="server" cssclass="csPlainText">End Date *</asp:label></td>
								<td>
									<uc1:dateentry id="dteEndDate" runat="server" required="true"></uc1:dateentry></td>
							</tr>
							<tr>
								<td>
									<asp:label id="Label3" runat="server" cssclass="csPlainText">Date Submitted *</asp:label></td>
								<td>
									<uc1:dateentry id="dteDateSubmitted" runat="server" required="true"></uc1:dateentry></td>
							</tr>
							<tr>
								<td>
									<asp:label id="Label26" runat="server" cssclass="csPlainText">Comment</asp:label></td>
								<td>
									<cc1:textboxsearch id="tbxComment" runat="server" contenttype="string" parametername="Comment" textmode="MultiLine"
										height="112px" maxlength="200"></cc1:textboxsearch></td>
							</tr>
							<tr>
								<td>
									<asp:label id="Label28" runat="server" cssclass="csPlainText">Printer Comment</asp:label></td>
								<td>
									<cc1:textboxsearch id="tbxPrinterComment" runat="server" contenttype="string" parametername="Comment"
										textmode="MultiLine" height="112px" width="100%" maxlength="500"></cc1:textboxsearch></td>
							</tr>
							<tr>
								<td>
									<asp:label id="Label31" runat="server" cssclass="csPlainText">Contract Comment</asp:label></td>
								<td>
									<cc1:textboxsearch id="tbxContractComment" runat="server" contenttype="string" parametername="Comment"
										textmode="MultiLine" height="112px" width="100%" maxlength="500"></cc1:textboxsearch></td>
							</tr>
						</table>
					</iewc:pageview>
					<iewc:pageview id="pavPricing">
						<table class="CSTable">
							<tr>
								<td>
									<asp:label id="Label38" runat="server" cssclass="csPlainText">Currency</asp:label></td>
								<td>
									
								<asp:label id="txtCurrency" runat="server" cssclass="csPlainText" font-bold="True"></asp:label></td>									
							</tr>	
							<tr>
								<td>
									<asp:label id="Label37" runat="server" cssclass="csPlainText">Base Remit Rate (%)*</asp:label></td>
								<td>
									<cc1:textboxfloat id="tbxBaseRemitRate" runat="server" required="True" errormsgregexp="The field Base Remit Rate on Base Price has to be a number."
										errormsgrequired="The field Remit Rate on Base Price is mandatory." emptyvalue="0"></cc1:textboxfloat></td>
							</tr>
							<tr>
								<td>
									<asp:label id="Label41" runat="server" cssclass="csPlainText">Base Price Without Postage*</asp:label></td>
								<td>
									<cc1:textboxfloat id="tbxBasePriceSansPostage" runat="server" required="True" errormsgregexp="The field Base Price in Contract has to be a number."
										errormsgrequired="The field Base Price in Contract is mandatory." emptyvalue="0"></cc1:textboxfloat></td>
							</tr>	
							<tr>
								<td>
									<asp:label id="Label43" runat="server" cssclass="csPlainText">Postage Remit Rate (%) </asp:label></td>
								<td>
									<cc1:textboxfloat id="tbxPostageRemitRate" runat="server" required="True" errormsgregexp="The field Remit Rate on Base Price has to be a number."
										errormsgrequired="The field Remit Rate on Base Price is mandatory." emptyvalue="0"></cc1:textboxfloat></td>
							</tr>
							<tr>
								<td>
									<asp:label id="Label42" runat="server" cssclass="csPlainText">Postage Amount</asp:label></td>
								<td>
									<cc1:textboxfloat id="tbxPostageAmount" runat="server" required="True" errormsgregexp="The field Base Price in Contract has to be a number."
										errormsgrequired="The field Base Price in Contract is mandatory." emptyvalue="0"></cc1:textboxfloat></td>
							</tr>						
							
							<tr>
								<td>
									<asp:label id="Label30" runat="server" cssclass="csPlainText">Base Price</asp:label></td>
								<td>
									
								<asp:label id="tbxBasePrice" runat="server" cssclass="csPlainText" font-bold="True"></asp:label></td>									
							</tr>							
							<tr>
								<td>
									<asp:label id="Label15" runat="server" cssclass="csPlainText">Remit Rate (%)</asp:label></td>
								<td>
								
								<asp:label id="tbxRemitRate" runat="server" cssclass="csPlainText" font-bold="True"></asp:label></td>
		
							<tr>
								<td>
									<asp:label id="Label11" runat="server" cssclass="csPlainText">Converted CAD Base Price</asp:label></td>
								<td>
									<asp:label id="lblConvertedCADBasePrice" runat="server" cssclass="csPlainText" font-bold="True"></asp:label></td>
							</tr>
							<tr>
								<td>
									<asp:label id="Label13" runat="server" cssclass="csPlainText">QSP Price</asp:label></td>
								<td>
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td width="50%"><span class="csPlainText">GST:&nbsp;&nbsp;</span>
												<asp:label id="lblQSPPriceGST" runat="server" cssclass="csPlainText" font-bold="True"></asp:label></td>
											<td width="50%"><span class="csPlainText">HST:&nbsp;&nbsp;</span>
												<asp:label id="lblQSPPriceHST" runat="server" cssclass="csPlainText" font-bold="True"></asp:label></td>
										</tr>
									</table>
								</td>
							</tr>
							
							
							<tr>
								<td>
									<asp:label id="Label16" runat="server" cssclass="csPlainText">Conversion Rate (%) *</asp:label></td>
								<td>
									<cc1:textboxfloat id="tbxConversionRate" runat="server" required="True" errormsgregexp="The field Conversion Rate has to be a number."
										errormsgrequired="The field Conversion Rate is mandatory." emptyvalue="0"></cc1:textboxfloat></td>
							</tr>

							<tr>
								<td style="HEIGHT: 22px"></td>
								<td style="HEIGHT: 22px"></td>
							</tr>
							<tr>
								<td style="HEIGHT: 22px"></td>
								<td style="HEIGHT: 22px"></td>
							</tr>
							<tr>
								<td>
									<asp:label id="Label17" runat="server" cssclass="csPlainText">Newstand Price per Issue ($)</asp:label></td>
								<td>
									<cc1:currency id="tbxNewstandPricePerIssue" runat="server" required="False" errormsgregexp="The field Newstand Price per Issue is invalid. Ex: 2.40"
										errormsgrequired="The field Newstand Price per Issue is mandatory." emptyvalue="0"></cc1:currency></td>
							</tr>	
							<tr>
								<td>
									<asp:label id="Label40" runat="server" cssclass="csPlainText">Total Catalog Newstand price($)</asp:label></td>
								<td>
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td width="50%"><span class="csPlainText">GST:&nbsp;&nbsp;</span>
												<asp:label id="lblTotCatNewsGST" runat="server" cssclass="csPlainText" font-bold="True"></asp:label></td>
											<td width="50%"><span class="csPlainText">HST:&nbsp;&nbsp;</span>
												<asp:label id="lblTotCatNewsHST" runat="server" cssclass="csPlainText" font-bold="True"></asp:label></td>
										</tr>
									</table>
								</td>
							</tr>	
							<tr>
								<td>
									<asp:label id="Label44" runat="server" cssclass="csPlainText">Total Savings($)</asp:label></td>
								<td>
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td width="50%"><span class="csPlainText">GST:&nbsp;&nbsp;</span>
												<asp:label id="lblTotSavingsGST" runat="server" cssclass="csPlainText" font-bold="True"></asp:label></td>
											<td width="50%"><span class="csPlainText">HST:&nbsp;&nbsp;</span>
												<asp:label id="lblTotSavingsHST" runat="server" cssclass="csPlainText" font-bold="True"></asp:label></td>
										</tr>
									</table>
								</td>
							</tr>		
							<tr>
								<td style="HEIGHT: 22px"></td>
								<td style="HEIGHT: 22px"></td>
							</tr>																		
							<tr>
								<td>
									<asp:label id="Label29" runat="server" cssclass="csPlainText">Effort Key</asp:label></td>
								<td>
									<asp:textbox id="tbxEffortKey" runat="server" maxlength="40"></asp:textbox></td>
							</tr>
							<tr>
								<td>
									<asp:label id="Label7" runat="server" cssclass="csPlainText">Number Of Issues *</asp:label></td>
								<td>
									<cc1:textboxinteger id="tbxNumberOfIssues" runat="server" required="True" errormsgregexp="The field Number of Issues has to be a number."
										errormsgrequired="The field Number of Issues is mandatory." emptyvalue="0"></cc1:textboxinteger></td>
							</tr>
							
							<tr>
								<td>
									<asp:label id="Label34" runat="server" cssclass="csPlainText">Internet Approval</asp:label></td>
								<td>
									<asp:radiobuttonlist id="rblInternetApproval" runat="server" cssclass="csPlainText" repeatdirection="Horizontal">
										<asp:listitem value="True" selected="True">Yes</asp:listitem>
										<asp:listitem value="False">No</asp:listitem>
									</asp:radiobuttonlist></td>
							</tr>
							<tr>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td>
									<asp:label id="Label35" runat="server" cssclass="csPlainText">ABC Code</asp:label></td>
								<td>
									<cc1:textboxsearch id="tbxABCCode" runat="server" contenttype="string" parametername="ABCCode" maxlength="20"></cc1:textboxsearch></td>
							</tr>
							<tr>
								<td>
									<asp:label id="Label4" runat="server" cssclass="csPlainText">QSP Premium</asp:label></td>
								<td>
									<cc1:dropdownlistinteger id="ddlPremium" runat="server"></cc1:dropdownlistinteger></td>
							</tr>
							<tr>
								<td>
									<asp:label id="Label10" cssclass="csPlainText" runat="server">Premium Indicator</asp:label></td>
								<td>
									<asp:textbox id="tbxPremiumIndicator" runat="server" maxlength="1" columns="1"></asp:textbox>
								</td>
							</tr>
							<tr>
								<td>
									<asp:label id="Label12" cssclass="csPlainText" runat="server">Premium Code</asp:label></td>
								<td>
									<asp:textbox id="tbxPremiumCode" runat="server" maxlength="50"></asp:textbox>
								</td>
							</tr>
							<tr>
								<td>
									<asp:label id="Label18" cssclass="csPlainText" runat="server">Premium Copy</asp:label></td>
								<td>
									<asp:textbox id="tbxPremiumCopy" runat="server" maxlength="500" width="98%" height="112px" textmode="MultiLine"></asp:textbox>
								</td>
							</tr>
							<tr>
								<td>
									<asp:label id="Label45" runat="server" cssclass="csPlainText">List Agent Code</asp:label></td>
								<td>
									<asp:textbox id="tbxListAgentCode" runat="server" maxlength="5"></asp:textbox></td>
							</tr>
							<tr>
								<td>
									<asp:label id="Label46" runat="server" cssclass="csPlainText">QSP Agency Code</asp:label></td>
								<td>
									<asp:textbox id="tbxQSPAgencyCode" runat="server" maxlength="20"></asp:textbox></td>
							</tr>
						</table>
					</iewc:pageview>
					<iewc:pageview id="pavListingInformation">
						<table class="CSTable">
							<tr>
								<td>
									<asp:label id="Label19" runat="server" cssclass="csPlainText">Listing Level</asp:label></td>
								<td>
									<cc1:dropdownlistinteger id="ddlListingLevel" runat="server" contenttype="int" parametername="ListingLevel"></cc1:dropdownlistinteger></td>
							</tr>
							<tr>
								<td>
									<asp:label id="Label20" runat="server" cssclass="csPlainText">Listing Copy Text</asp:label></td>
								<td>
									<cc1:textboxsearch id="tbxListingCopyText" runat="server" contenttype="string" parametername="ListingCopyText"
										textmode="MultiLine" height="112px" width="98%" maxlength="500"></cc1:textboxsearch></td>
							</tr>
							<tr>
								<td>
									<asp:label id="Label27" runat="server" cssclass="csPlainText">QSP CA Listing Copy Text</asp:label></td>
								<td>
									<cc1:textboxsearch id="tbxQSPCAListingCopyText" runat="server" contenttype="string" parametername="ListingCopyText"
										textmode="MultiLine" height="112px" width="98%" maxlength="500"></cc1:textboxsearch></td>
							</tr>
							<tr>
								<td style="HEIGHT: 22px"></td>
								<td style="HEIGHT: 22px"></td>
							</tr>
							<tr>
								<td>
									<asp:label id="Label21" runat="server" cssclass="csPlainText">Advertising in QSP Catalog</asp:label></td>
								<td>
									<asp:radiobuttonlist id="rblAdvertising" runat="server" cssclass="csPlainText" repeatdirection="Horizontal">
										<asp:listitem value="1" selected="True">Yes</asp:listitem>
										<asp:listitem value="0">No</asp:listitem>
									</asp:radiobuttonlist></td>
							</tr>
							<tr>
								<td>
									<asp:label id="Label22" runat="server" cssclass="csPlainText">Ad Page Size</asp:label></td>
								<td>
									<cc1:dropdownlistinteger id="ddlAdPageSize" runat="server" contenttype="int" parametername="AdPageSize"></cc1:dropdownlistinteger></td>
							</tr>
							<tr>
								<td>
									<asp:label id="Label23" runat="server" cssclass="csPlainText">Ad / Payment Currency</asp:label></td>
								<td>
									<cc1:dropdownlistinteger id="ddlAdPaymentCurrency" runat="server" contenttype="int" parametername="AdPaymentCurrency"></cc1:dropdownlistinteger></td>
							</tr>
							<tr>
								<td>
									<asp:label id="Label24" runat="server" cssclass="csPlainText">Ad Cost</asp:label></td>
								<td>
									<cc1:textboxfloat id="tbxAdCost" runat="server" errormsgregexp="The field Ad Cost has to be a number."
										emptyvalue="0"></cc1:textboxfloat></td>
							</tr>
							<tr>
								<td style="HEIGHT: 22px"></td>
								<td style="HEIGHT: 22px"></td>
							</tr>
							<tr>
								<td>
									<asp:label id="Label32" runat="server" cssclass="csPlainText">Magazine Cover Filename</asp:label></td>
								<td>
									<cc1:textboxsearch id="tbxMagazineCoverFilename" runat="server" contenttype="string" parametername="tbxMagazineCoverFilename"
										textmode="MultiLine" height="112px" width="100%" maxlength="100"></cc1:textboxsearch>								
									
								</td>
							</tr>
							<tr>
								<td>
									<asp:label id="Label33" runat="server" cssclass="csPlainText">Catalog Ad Filename</asp:label></td>
								<td>
								
									<cc1:textboxsearch id="tbxCatalogAdFilename" runat="server" contenttype="string" parametername="tbxMagazineCoverFilename"
										textmode="MultiLine" height="112px" width="100%" maxlength="100"></cc1:textboxsearch>										
								</td>
							</tr>
							<tr>
								<td>
									<asp:label id="Label36" runat="server" cssclass="csPlainText">Catalog Page Number</asp:label></td>
								<td>
									<cc1:textboxinteger id="tbxCatalogPageNumber" runat="server" required="False" emptyvalue="0"></cc1:textboxinteger></td>
							</tr>
							<tr>
								<td>
									<asp:label id="Label39" runat="server" cssclass="csPlainText">Placement Level</asp:label></td>

									<td>
									<cc1:dropdownlistinteger id="ddlPlacementLevel" runat="server" contenttype="int" parametername="PlacementLevel"></cc1:dropdownlistinteger></td>
							</tr>
						
						</table>
					</iewc:pageview>
				</iewc:multipage></td>
			</TD></tr>
	</TR>
</TABLE>
</TR><tr>
	<td colspan="2" align="right"><asp:button id="btnSubmit" runat="server"  cssclass="boxlook" text="Save" onclick="btnSubmit_Click"></asp:button><asp:button id="btnCancel" runat="server" cssclass="boxlook" text="Cancel" causesvalidation="False" onclick="btnCancel_Click"></asp:button></td>
</tr>
</TBODY></TABLE>
