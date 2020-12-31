<%@ Page language="c#" Codebehind="personnel_directory.aspx.cs" AutoEventWireup="false" Inherits="QSPFulfillment.Admin.pdir" %>
<%@ Register  TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" >
<HTML>
	<HEAD>
		<title>QSP Personnel Directory</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="jcaesar@rd.com" name="author">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="home.css" rel="stylesheet" type="text/css">
	</HEAD>
	<body leftMargin="0" topMargin="0" marginwidth="0" marginheight="0" bgcolor="#ffffff">
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<br>
			<br>
			<table align="center" class="fields2">
				<tr class="fields2">
					<td colspan="4" align="right">
						<div align="center">
							<p align="left">
								<font color="#cc6600" size="4"><img src="../Images/QSPlogo.gif"> </font>
							</p>
							<p>
								<font color="#cc6600" size="3" face="Arial, Helvetica, sans-serif"><strong><font color="#000099">
											QUICK REFERENCE</font> </strong></font><font color="#000099" size="4"><font size="5" face="Arial, Helvetica, sans-serif">
										<strong>
											<br>
											DIRECTORY </strong></font></font>
							</p>
						</div>
						<br>
						<hr>
						<p align="center">
							<span class="bodytext" style='COLOR:#ff0000'>The Directory is considered 
								proprietary information<br>
								and is intended for use within QSP, Inc. only.<br>
								<br>
								The reader is hereby notified that any dissemination,<br>
								distribution or copying of this information is prohibited. </span>
						</p>
						<br>
					</td>
				</tr>
				<tr class="fields2">
					<td align="right"><font color="#000099"><strong>Last Name:</strong></font></td>
					<td align="left">
						<input name="lname" type="text" class="fields" id="LastName" size="20" maxLength="20" runat="server">
					</td>
					<td align="right"><font color="#000099"><strong>First Name:</strong></font></td>
					<td align="left">
						<input name="fname" type="text" class="fields" id="FirstName" size="20" maxLength="20"
							runat="server">
					</td>
				</tr>
				<tr class="fields2">
					<td colSpan="4" align="right">
						<div align="center">
							<asp:CheckBox ID="displaytime" Checked="True" Runat="server" />
							&nbsp;&nbsp; Display the local time with each listing.
						</div>
						<br>
					</td>
				</tr>
				<tr class="fields2">
					<td colSpan="4" align="right">
						<div align="center">
							<span class="fields"><input name="Submit1" type="submit" class="fields2" id="Submit1" value="Search Names" runat="server">
							</span>
						</div>
					</td>
				</tr>
				<!--
				<tr class="fields2">
					<td align="right"><div align="right"><img src="images/xls.gif" width="17" height="16"></div>
					</td>
					<td colSpan="3" align="right">
						<div align="left">
							&nbsp;&nbsp;<a href="Account%20Track%20DirectoryMaster.xls">Export the entire directory into Excel</a>
						</div>
					</td>
				</tr>
				-->
			</table>
			<div align="center">
				<asp:label id="EmptySearch_lbl" CssClass="error" Runat="server">Please enter search criteria.</asp:label>
				<asp:label id="BadSearch_lbl" CssClass="error" Runat="server">
			Your search did not match any records. <br />
			Please refine your search and try again. <br />
			Suggestions: <br />
			&nbsp;&nbsp;&nbsp;Make sure all names are spelled correctly.<br />
			&nbsp;&nbsp;&nbsp;Try searching by only last name or only first name instead of both.
		</asp:label>
				<table border="0" class="content">
					<asp:repeater id="Repeater1" Runat="server" OnItemDataBound="Repeater1_ItemDataBound">
						<ItemTemplate>
							<tr>
								<td style="border: black thin solid">
									<table width="100%">
										<tr style="FONT-WEIGHT: bolder">
											<td colspan="2">
												<%# DataBinder.Eval(Container.DataItem,"LName") %>
												,&nbsp;
												<%# DataBinder.Eval(Container.DataItem,"FName") %>
											</td>
											<td>&nbsp;&nbsp;
												<%# DataBinder.Eval(Container.DataItem,"Spouse") %>
											</td>
										</tr>
										<tr>
											<td>&nbsp;</td>
											<td align="right">Home Phone:</td>
											<td align="left"><%# DataBinder.Eval(Container.DataItem,"Home") %></td>
											<td colspan="2" align="right">Voicemail #
												<%# DataBinder.Eval(Container.DataItem,"VoiceMail") %>
											</td>
										</tr>
										<tr>
											<td>&nbsp;</td>
											<td align="right">Business Phone:</td>
											<td align="left"><%# DataBinder.Eval(Container.DataItem,"Work") %></td>
										</tr>
										<tr>
											<td>&nbsp;</td>
											<td align="right">Cell Phone:</td>
											<td align="left"><%# DataBinder.Eval(Container.DataItem,"Mobile") %></td>
										</tr>
										<tr>
											<td>&nbsp;</td>
											<td align="right">Fax Phone:</td>
											<td align="left"><%# DataBinder.Eval(Container.DataItem,"Fax") %></td>
										</tr>
										<tr>
											<td>&nbsp;</td>
											<td align="right">Job Title:</td>
											<td align="left"><%# DataBinder.Eval(Container.DataItem,"JobTitle") %></td>
										</tr>
										<tr runat="server" id="timerow">
											<td>&nbsp;</td>
											<td align="right">Local Time:</td>
											<td align="left">
												<asp:Label Runat="server" ID="time" Text='<%# DataBinder.Eval(Container.DataItem,"TimeZone") %>' />
												<asp:Literal Runat="server" ID="lt_dst" Text='<%# DataBinder.Eval(Container.DataItem,"DST") %>' />
											</td>
										</tr>
										<tr>
											<td>&nbsp;</td>
											<td align="right">City, State:</td>
											<td align="left">
												<%# DataBinder.Eval(Container.DataItem,"Location") %>
											</td>
											<td colspan="2" align="right">
												<a class="email" href='mailto:<%# DataBinder.Eval(Container.DataItem,"Email") %>'>
													<%# DataBinder.Eval(Container.DataItem,"Email") %>
												</a>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</ItemTemplate>
						<AlternatingItemTemplate>
							<tr>
								<td style="BACKGROUND-COLOR: #CCCCCC; border: black thin solid"><%--778899--%>
									<table width="100%" bgcolor="#EBEBEB">
										<tr style="FONT-WEIGHT: bolder">
											<td colspan="2">
												<%# DataBinder.Eval(Container.DataItem,"LName") %>
												,&nbsp;
												<%# DataBinder.Eval(Container.DataItem,"FName") %>
											</td>
											<td>&nbsp;&nbsp;
												<%# DataBinder.Eval(Container.DataItem,"Spouse") %>
											</td>
										</tr>
										<tr>
											<td>&nbsp;</td>
											<td align="right">Home Phone:</td>
											<td align="left"><%# DataBinder.Eval(Container.DataItem,"Home") %></td>
											<td colspan="2" align="right">Voicemail #
												<%# DataBinder.Eval(Container.DataItem,"VoiceMail") %>
											</td>
										</tr>
										<tr>
											<td>&nbsp;</td>
											<td align="right">Business Phone:</td>
											<td align="left"><%# DataBinder.Eval(Container.DataItem,"Work") %></td>
										</tr>
										<tr>
											<td>&nbsp;</td>
											<td align="right">Cell Phone:</td>
											<td align="left"><%# DataBinder.Eval(Container.DataItem,"Mobile") %></td>
										</tr>
										<tr>
											<td>&nbsp;</td>
											<td align="right">Fax Phone:</td>
											<td align="left"><%# DataBinder.Eval(Container.DataItem,"Fax") %></td>
										</tr>
										<tr>
											<td>&nbsp;</td>
											<td align="right">Job Title:</td>
											<td align="left"><%# DataBinder.Eval(Container.DataItem,"JobTitle") %></td>
										</tr>
										<tr runat="server" id="timerow_A">
											<td>&nbsp;</td>
											<td align="right">Local Time:</td>
											<td align="left">
												<asp:Label Runat="server" ID="time_A" Text='<%# DataBinder.Eval(Container.DataItem,"TimeZone") %>' />
												<asp:Literal Runat="server" ID="lt_dst_A" Text='<%# DataBinder.Eval(Container.DataItem,"DST") %>' />
											</td>
										</tr>
										<tr>
											<td>&nbsp;</td>
											<td align="right">City, State:</td>
											<td align="left">
												<%# DataBinder.Eval(Container.DataItem,"Location") %>
											</td>
											<td colspan="2" align="right">
												<a class="email" href='mailto:<%# DataBinder.Eval(Container.DataItem,"Email") %>'>
													<%# DataBinder.Eval(Container.DataItem,"Email") %>
												</a>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</AlternatingItemTemplate>
					</asp:repeater>
				</table>
			</div>
			<div align="center">
				<br>
				<asp:label id="email_lbl" CssClass="error" Runat="server">
					If the data presented here is inaccurate, please email 
					<a href='mailto:QSP_IT@rd.com?subject=phonedir'>QSP IT</a> with the updated information.
				</asp:label>
				<br>
				<br>
			</div>
		</form>
	</body>
</HTML>
