<%@ Control Language="vb" AutoEventWireup="false" Codebehind="UploadProductFileControl.ascx.vb" Inherits="StoreFront.StoreFront.UploadProductFileControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<body>
	<TABLE id="tblDataFile" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="ContentTableHeader" noWrap align="left" width="50%" colSpan="2">&nbsp;&nbsp;Select 
				Data File:&nbsp;</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="Content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" align="right" width="25%">File Type:</TD>
			<TD class="content" align="left" width="75%"><asp:dropdownlist id="FileType" runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="Content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR id="SelectFile">
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right" width="25%">Select File:</td>
			<td class="content" align="left" width="75%"><INPUT id="ImportFile" type="file" name="FileName" runat="server"></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="Content" width="1" colSpan="2">&nbsp;</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR id="DBase">
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right" width="25%">Select .dbt File:</td>
			<td class="content" align="left" width="75%"><INPUT id="DBTFile" type="file" name="DBTFileName" runat="server"><br>
				&nbsp;</td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<TR id="Delimiter">
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right" width="25%">File Delimiter:</td>
			<td class="content" align="left" width="75%"><asp:dropdownlist id="FileDelimiter" runat="server"></asp:dropdownlist>&nbsp;&nbsp;&nbsp;If 
				Other, Specify:&nbsp;&nbsp;
				<asp:textbox id="OtherDelimiter" runat="server" Width="25"></asp:textbox><br>
				&nbsp;</td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<TR>
			<TD class="ContentTable" colSpan="4" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<TR>
			<td class="content" align="middle" width="75%" colSpan="4">
				<asp:LinkButton ID="Continue" Runat="server">
					<asp:Image BorderWidth="0" ID="imgContinue" runat="server" ImageUrl="../images/continue.jpg" AlternateText="Continue"></asp:Image>
				</asp:LinkButton>
			</td>
		</TR>
	</TABLE>
	<TABLE id="tblImport" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
		<TBODY>
			<TR>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="ContentTableHeader" noWrap align="left" width="50%" colSpan="2">&nbsp;&nbsp;Import 
					Options:&nbsp; <INPUT id="ImportFileName" type="hidden" name="ImportFileName" runat="server">
					<INPUT id="DBTFileName" type="hidden" name="DBTFileName" runat="server"> <INPUT id="DelimiterToUse" type="hidden" name="DilimiterToUse" runat="server">
					<INPUT id="FileTypeToUse" type="hidden" name="FileTypeToUse" runat="server">
				</TD>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<tr>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</tr>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<td class="content" align="right" width="25%">Select An Import Type:</td>
				<td class="content" align="left" width="75%"><asp:dropdownlist id="ImportType" runat="server"></asp:dropdownlist></td>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<tr>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="Content" width="1" colSpan="2">&nbsp;</TD>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</tr>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" colSpan="2"><asp:checkbox id="ColumnHeadings" runat="server"></asp:checkbox>&nbsp;First 
					Row&nbsp;Contains Column Headings
				</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<tr>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="Content" width="1" colSpan="2">&nbsp;</TD>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</tr>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" colSpan="2"><asp:checkbox id="ActivateAll" runat="server"></asp:checkbox>&nbsp;Activate 
					All Imported Products
				</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" colSpan="2"><asp:checkbox id="ActivateShipping" runat="server"></asp:checkbox>&nbsp;Activate 
					Shipping on All Imported Products
				</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" colSpan="2"><asp:checkbox id="ActivateLocalTax" runat="server"></asp:checkbox>&nbsp;Activate 
					Local Tax on All Imported Products
				</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" colSpan="2"><asp:checkbox id="ActivateStateTax" runat="server"></asp:checkbox>&nbsp;Activate 
					State Tax on All Imported Products
				</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" colSpan="2"><asp:checkbox id="ActivateCountryTax" runat="server"></asp:checkbox>&nbsp;Activate 
					Country Tax on All Imported Products
				</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" colSpan="2"><asp:checkbox id="LinkAll" runat="server"></asp:checkbox>&nbsp;Link 
					All Products to Detail Page
				</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="content" colSpan="4" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
			<TR>
				<TD class="content" colSpan="4">&nbsp;</TD>
			</TR>
			<TR>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="ContentTableHeader" noWrap align="left" width="50%" colSpan="2">&nbsp;&nbsp;Match 
					Data Fields:&nbsp;
				</TD>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<tr>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</tr>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<td class="content" align="middle" colSpan="2"><asp:repeater id="DataFields" runat="server">
						<HeaderTemplate>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="80%" border="0">
								<TR>
									<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
									<TD class="ContentTableHeader" noWrap align="left">&nbsp;&nbsp;StoreFront 
										Field&nbsp;
									</TD>
									<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
									<TD class="ContentTableHeader" noWrap align="left">&nbsp;&nbsp;Column 1&nbsp;</TD>
									<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
									<TD class="ContentTableHeader" noWrap align="left">&nbsp;&nbsp;Column 2&nbsp;</TD>
									<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
									<TD class="ContentTableHeader" noWrap align="left">&nbsp;&nbsp;Column 3&nbsp;</TD>
									<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
									<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
								</TR>
						</HeaderTemplate>
						<ItemTemplate>
							<TR>
								<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
								<TD class="Content" noWrap align="left">&nbsp;&nbsp;
									<asp:dropdownlist id="SFFields" runat="server"></asp:dropdownlist>&nbsp;</TD>
								<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
								<TD class="Content" align="left">&nbsp;&nbsp;<%# DataBinder.Eval(Container.DataItem,"0") %>&nbsp;</TD>
								<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
								<TD class="Content" align="left">&nbsp;&nbsp;<%# DataBinder.Eval(Container.DataItem,"1") %>&nbsp;</TD>
								<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
								<TD class="Content" align="left">&nbsp;&nbsp;<%# DataBinder.Eval(Container.DataItem,"2") %>&nbsp;</TD>
								<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
							</TR>
							<TR>
								<TD class="ContentTable" colSpan="9" height="1"><IMG height="1" src="images/clear.gif"></TD>
							</TR>
						</ItemTemplate>
						<FooterTemplate>
	</TABLE>
	</FooterTemplate> </asp:repeater></TD>
	<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</TR>
	<TR>
		<TD class="ContentTable" colSpan="4" height="1"><IMG height="1" src="images/clear.gif"></TD>
	</TR>
	<TR>
		<td class="content" align="middle" width="75%" colSpan="4">
			<asp:LinkButton ID="cmdImport" Runat="server">
				<asp:Image BorderWidth="0" ID="imgImport" runat="server" ImageUrl="../images/submit.jpg" AlternateText="Import"></asp:Image>
			</asp:LinkButton>
		</td>
	</TR>
	</TBODY></TABLE>
	<TABLE id="tblResults" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
		<TBODY>
			<TR>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="ContentTableHeader" width="1">&nbsp;</TD>
				<TD class="ContentTableHeader" noWrap align="left">&nbsp;&nbsp;Results Of Product 
					Import:&nbsp;<INPUT id="hResultsSummary" type="hidden" name="hResultsSummary" runat="server">
					<INPUT id="hResultsDetails" type="hidden" name="hResultsDetails" runat="server">
					<INPUT id="hResultsImages" type="hidden" name="hResultsImages" runat="server">
				</TD>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<tr>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1">&nbsp;</TD>
				<TD class="content" width="1"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</tr>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1">&nbsp;</TD>
				<td class="content" align="left"><b>Data Import Results:</b></td>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1">&nbsp;</TD>
				<td class="content" align="left"><%=arrOutput.Item(0)%></td>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<tr>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1">&nbsp;</TD>
				<TD class="content" width="1">&nbsp;</TD>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</tr>
			<TR id="ResultsHidden">
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1">&nbsp;</TD>
				<TD class="content" align="left">
					<asp:linkbutton id="LBResultsHidden" runat="server">
						<b>Result Details:</b>
					</asp:linkbutton></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR id="ResultsShown">
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1">&nbsp;</TD>
				<TD class="content" align="left">
					<asp:linkbutton id="LBResultsShown" runat="server">
						<b>Result Details:</b>
					</asp:linkbutton></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR id="results">
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1">&nbsp;</TD>
				<td class="content" align="left"><%=arrOutput.Item(1)%></td>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<tr>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1">&nbsp;</TD>
				<TD class="content" width="1">&nbsp;</TD>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</tr>
			<TR id="ImagesHidden">
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1">&nbsp;</TD>
				<td class="content" align="left"><asp:linkbutton id="LBImagesHidden" runat="server"><b>Images 
							to Upload:</b></asp:linkbutton></td>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR id="ImagesShown">
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1">&nbsp;</TD>
				<td class="content" align="left"><asp:linkbutton id="LBImagesShown" runat="server"><b>Images 
							to Upload:</b></asp:linkbutton></td>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR id="Images">
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1">&nbsp;</TD>
				<td class="content" align="left"><%=arrOutput.Item(2)%><br>
					&nbsp;</td>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<tr>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1">&nbsp;</TD>
				<TD class="content" width="1">&nbsp;</TD>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</tr>
			<TR>
				<TD class="ContentTable" colSpan="4" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
		</TBODY></TABLE>
	<table id="tblPleaseWait" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
		<tbody>
			<TR>
				<TD class="ContentTable" colSpan="3" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
			<tr>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="middle"><b>Please wait while products are imported...</b><br>
				</TD>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</tr>
			<TR>
				<TD class="ContentTable" colSpan="3" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
		</tbody>
	</table>
</body>
