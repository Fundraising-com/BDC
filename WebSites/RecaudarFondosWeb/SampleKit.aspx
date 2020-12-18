<%@ Page language="c#" Codebehind="SampleKit.aspx.cs" AutoEventWireup="false" Inherits="efundraising.RecaudarFondosWeb.SampleKit" %>
<%@ Register TagPrefix="uc1" TagName="AddressHygiene" Src="Components/User/AddressHygene/AddressHygiene.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExplicitAddressConfirmation" Src="Components/User/AddressHygene/ExplicitAddressConfirmation.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Kit de información de eFundraising</title>
		<meta content="Gracias por su interés en eFundraising: soluciones de recaudación de fondos para escuelas, equipos deportivos, y  otros grupos no lucrativos."
			name="DESCRIPTION">
		<meta content="campaña de recaudación de fondos, recaudación de fondos, recaudar fondos, kit de información, kit de recaudación de fondos."
			name="KEYWORDS">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Resources/css/_RecaudarFondos_/_classic_/es-us/rdstyles.css" type="text/css"
			rel="stylesheet">
		<script language="javascript" src="Resources/javascript/JavaScript.js" type="text/javascript"></script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="768" align="center" summary="Reader's Digest"
				border="0">
				<TBODY>
					<tr>
						<td vAlign="top" align="left"><A href="Default.aspx"><IMG height="51" alt="RECAUDAR-FONDOS" src="~/Resources/images/_RecaudarFondos_/_classic_/es-us/Top_01.gif"
									width="768" border="0" runat="server"></A></td>
					</tr>
					<tr>
						<td vAlign="top" align="left">
							<table cellSpacing="0" cellPadding="0" width="100%" summary="Reader's Digest" border="0">
								<tr vAlign="top" align="left">
									<td><A href="Scratchcard.aspx"><IMG height="27" alt="Tarjetas Sorpresa" src="~/Resources/images/_RecaudarFondos_/_classic_/es-us/Tarjetas_Sorpresa.jpg"
												width="154" border="0" runat="server"></A></td>
									<td><A href="Chocolate.aspx"><IMG height="27" alt="Chocolate" src="~/Resources/images/_RecaudarFondos_/_classic_/es-us/Chocolate.jpg"
												width="101" border="0" runat="server"></A></td>
									<td><A href="CookieDough.aspx"><IMG height="27" alt="Masapara Hornear Galletas" src="~/Resources/images/_RecaudarFondos_/_classic_/es-us/Masapara_Hornear_Galletas.jpg"
												width="216" border="0" runat="server"></A></td>
									<td><!--<a href="http://www.magfundraising.com/?gid=F8C621E5-62E9-4193-A44B-FD1CBC67C476&amp;pr_id=4159"
											target="_blank"><IMG height="27" alt="Revista En Linea" src="~/Resources/images/_RecaudarFondos_/_classic_/es-us/Revista_En_Linea.jpg"
												width="127" border="0" runat="server"></a>--><asp:PlaceHolder ID="MagFunTop" Runat="server"></asp:PlaceHolder></td>
									<td><A href="Gift.aspx"><IMG height="27" alt="Folletos Con Regalos" src="~/Resources/images/_RecaudarFondos_/_classic_/es-us/Folletos_Con_Regalos.jpg"
												width="170" border="0" runat="server"></A></td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td>
							<table cellSpacing="0" cellPadding="0" width="100%" summary="Reader's Digest" border="0">
								<TBODY>
									<tr>
										<td vAlign="top" align="left" width="200%" colSpan="2"><table cellSpacing="0" cellPadding="0" width="100%" summary="Reader's Digest" border="0">
												<TBODY>
													<tr vAlign="top" align="left">
														<td width="22%">
															<table cellSpacing="0" cellPadding="0" width="100%" summary="Reader's Digest" border="0">
																<tr>
																	<td vAlign="top" align="left"><A href="http://www.fundraising.com/free-fundraising-kit.aspx" target="_blank"><IMG height="156" alt="Kit" src="~/Resources/images/_RecaudarFondos_/_classic_/es-us/f_kit.gif"
																				width="153" border="0" runat="server"></A></td>
																</tr>
																<tr>
																	<td vAlign="top" align="left"><IMG height="262" alt="RECAUDAR-FONDOS" src="~/Resources/images/_RecaudarFondos_/_classic_/es-us/f_boy.jpg"
																			width="171" runat="server"></td>
																</tr>
																<tr>
																	<td vAlign="top" align="left"><A href="Tips.aspx"><IMG height="44" alt="Consejos" src="~/Resources/images/_RecaudarFondos_/_classic_/es-us/f_tips.gif"
																				width="151" border="0" runat="server"></A></td>
																</tr>
															</table>
															<asp:label id="Label1" runat="server" Height="176px" Width="160px"></asp:label></td>
														<td style="PADDING-TOP: 25px" align="center" width="78%">
															<table cellSpacing="0" cellPadding="5" width="95%" align="center" summary="Reader's Digest"
																border="0">
																<tr>
																	<td class="PageContent" colSpan="2"><span class="Header">Masa para hornear galletas – 
																			hasta 45% de ganancias</span></td>
																</tr>
																<tr>
																	<td class="PageContent" colSpan="2">
																		<p>PIDA SU KIT DE INFORMACIÓN GRATIS HOY<br>
																			¡Y recibirá todo lo que necesita para comenzar con su campaña de recaudación de 
																			fondos!</p>
																	</td>
																</tr>
																<tr>
																	<td class="PageContent" width="68%">
																		<ul>
																			<li>
																			Descripción de los productos, precios y tablas de ganancias
																			<li>
																			Consejos para recaudar fondos
																			<li>
																				Una tarjeta sorpresa gratis que incluye mas de $500 en ahorros de las 
																				siguientes compañías:
																			</li>
																		</ul>
																	</td>
																	<td class="PageContent" width="32%" rowSpan="2"><IMG height="130" alt="RECAUDAR-FONDOS" src="~/Resources/images/_RecaudarFondos_/_classic_/es-us/form_image.jpg"
																			width="170" runat="server"></td>
																</tr>
																<tr>
																	<td class="PageContent" vAlign="top" align="center"><IMG height="36" alt="RECAUDAR-FONDOS" src="~/Resources/images/_RecaudarFondos_/_classic_/es-us/form_logos.gif"
																			width="315" runat="server">
																	</td>
																</tr>
																<tr>
																	<td class="PageContent" colSpan="2">Simplemente llene la solicitud adjunta o llame 
																		al 1-866-570-2821 para recibir su kit directamente a su puerta. ¡Cuando llame, 
																		no olvide solicitar el envió gratis con FedEx.</td>
																</tr>
																<tr>
																	<td class="PageContent" colSpan="2">
																		<p class="RedContent">No existe ninguna obligación y su información no será vendida 
																			o alquilada a ninguna otra empresa. Deseamos avisarle que el kit de recaudación 
																			de fondos está en ingles.</p>
																	</td>
																</tr>
																<tr>
																	<td class="FormContent" colSpan="2"><asp:validationsummary id="ValidationSummary" runat="server" Height="40px" Width="544px" DisplayMode="List"
																			Font-Names="Verdana" Font-Size="7pt"></asp:validationsummary>
																		<uc1:AddressHygiene id="AddressHygiene1" runat="server"></uc1:AddressHygiene>
																		<uc1:ExplicitAddressConfirmation id="ExplicitAddressConfirmation1" runat="server"></uc1:ExplicitAddressConfirmation></td>
																</tr>
																<tr id="tdForm" runat="server">
																	<td align="left" colSpan="2">
																		<table class="PageContent" cellSpacing="0" cellPadding="4" width="100%" align="center"
																			summary="Reader's Digest" border="0" id="tblConfirm" runat="server">
																			<tr>
																				<td align="left" valign="top">Thank you for your interest in our fundraising 
																					programs. Your request is being processed and your kit will be mailed to you 
																					shortly.
																				</td>
																			</tr>
																		</table>
																		<table class="FormContent" cellSpacing="0" cellPadding="4" width="100%" align="center"
																			summary="Reader's Digest" border="0">
																			<TBODY id="tblForm" runat="server">
																				<tr>
																					<td align="right" colSpan="3">Los espacios marcados con un asterisco *deben ser 
																						llenados.</td>
																				</tr>
																				<tr>
																					<td width="45%">Apellido<span class="Required">*</span></td>
																					<td style="WIDTH: 282px" width="282"><asp:textbox id="txtFirstName" runat="server" CssClass="tbox"></asp:textbox></td>
																					<td align="left" width="5%"><asp:requiredfieldvalidator id="rfvFirstName" runat="server" ControlToValidate="txtFirstName" ErrorMessage="You have to enter your FirstName">*</asp:requiredfieldvalidator></td>
																				</tr>
																				<tr>
																					<td vAlign="top" align="left">Nombre<span class="Required">*</span></td>
																					<td style="WIDTH: 282px"><asp:textbox id="txtLastName" runat="server" CssClass="tbox"></asp:textbox></td>
																					<td align="left"><asp:requiredfieldvalidator id="rfvLastName" runat="server" ControlToValidate="txtLastName" ErrorMessage="You have to enter your LastName">*</asp:requiredfieldvalidator></td>
																				</tr>
																				<tr>
																					<td vAlign="top" align="left">Título</td>
																					<td style="WIDTH: 282px"><asp:dropdownlist id="ddlTitle" runat="server" CssClass="tbox">
																							<asp:ListItem Value="99" Selected="True">----- Please Select -----</asp:ListItem>
																							<asp:ListItem Value="1">Activities Director / Coordinator</asp:ListItem>
																							<asp:ListItem Value="2">Assistant Coach</asp:ListItem>
																							<asp:ListItem Value="3">Athletic Director / Coordinator</asp:ListItem>
																							<asp:ListItem Value="4">Coach / Trainer</asp:ListItem>
																							<asp:ListItem Value="14">Director / Manager / Organizer</asp:ListItem>
																							<asp:ListItem Value="19">District Administrator</asp:ListItem>
																							<asp:ListItem Value="11">Fundraising Chairman / Committee</asp:ListItem>
																							<asp:ListItem Value="20">Information Officer</asp:ListItem>
																							<asp:ListItem Value="15">Leader / Captain</asp:ListItem>
																							<asp:ListItem Value="18">Member / Volunteer</asp:ListItem>
																							<asp:ListItem Value="17">Minister / Youth Minister</asp:ListItem>
																							<asp:ListItem Value="99">Other</asp:ListItem>
																							<asp:ListItem Value="5">Parent</asp:ListItem>
																							<asp:ListItem Value="16">Pastor / Youth Pastor</asp:ListItem>
																							<asp:ListItem Value="7">President / Vice President</asp:ListItem>
																							<asp:ListItem Value="6">Principal / Ass. Principal</asp:ListItem>
																							<asp:ListItem Value="13">PTA / PTO</asp:ListItem>
																							<asp:ListItem Value="12">Secretary</asp:ListItem>
																							<asp:ListItem Value="8">Student</asp:ListItem>
																							<asp:ListItem Value="9">Teacher</asp:ListItem>
																							<asp:ListItem Value="10">Treasurer</asp:ListItem>
																						</asp:dropdownlist></td>
																				</tr>
																				<tr>
																					<td vAlign="top" align="left">Dirección de correo<span class="Required">*</span></td>
																					<td style="WIDTH: 282px"><asp:textbox id="txtEMail" runat="server" CssClass="tbox"></asp:textbox></td>
																					<td align="left">
																						<table class="FormContent" cellSpacing="0" cellPadding="0">
																							<tr>
																								<td><asp:requiredfieldvalidator id="rfvEmail" runat="server" Height="5px" Width="5px" ControlToValidate="txtEMail"
																										ErrorMessage="You have to enter your Email Address">*</asp:requiredfieldvalidator></td>
																							</tr>
																							<tr>
																								<td><asp:regularexpressionvalidator id="revEmail" runat="server" Height="5px" Width="5px" ControlToValidate="txtEMail"
																										ErrorMessage="You have to enter your Email address" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:regularexpressionvalidator></td>
																							</tr>
																						</table>
																					</td>
																				</tr>
																				<tr>
																					<td vAlign="top" align="left">Nombre del grupo<span class="Required">*</span></td>
																					<td style="WIDTH: 282px"><asp:textbox id="txtGroupName" runat="server" CssClass="tbox"></asp:textbox></td>
																					<td align="left"><asp:requiredfieldvalidator id="rfvGroupName" runat="server" ControlToValidate="txtGroupName" ErrorMessage="You have to provide your GroupName">*</asp:requiredfieldvalidator></td>
																				</tr>
																				<tr>
																					<td vAlign="top" align="left">Dirección<span class="Required">*</span></td>
																					<td style="WIDTH: 282px"><asp:textbox id="txtAddress" runat="server" CssClass="tbox"></asp:textbox></td>
																					<td><asp:requiredfieldvalidator id="rfvAddress" runat="server" ControlToValidate="txtAddress" ErrorMessage="Address is invalid">*</asp:requiredfieldvalidator></td>
																	</td>
																</tr>
																<tr>
																	<td style="HEIGHT: 26px" vAlign="top" align="left">Ciudad<span class="Required">*</span></td>
																	<td style="WIDTH: 282px; HEIGHT: 26px"><asp:textbox id="txtCity" runat="server" CssClass="tbox"></asp:textbox></td>
																	<td align="left"><asp:requiredfieldvalidator id="rfvCity" runat="server" ControlToValidate="txtCity" ErrorMessage="City is invalid">*</asp:requiredfieldvalidator></td>
																</tr>
																<tr>
																	<td vAlign="top" align="left">País<span class="Required">*</span></td>
																	<td style="WIDTH: 282px"><asp:dropdownlist id="ddlCountry" runat="server" CssClass="tbox" AutoPostBack="True">
																			<asp:ListItem value="0">----- Please Select -----</asp:ListItem>
																			<asp:ListItem value="AU">Australia</asp:ListItem>
																			<asp:ListItem value="BM">Bermuda</asp:ListItem>
																			<asp:ListItem value="BR">Brasil</asp:ListItem>
																			<asp:ListItem value="CA">Canada</asp:ListItem>
																			<asp:ListItem value="CK">Cook Islands</asp:ListItem>
																			<asp:ListItem value="FR">France</asp:ListItem>
																			<asp:ListItem value="GU">Guam</asp:ListItem>
																			<asp:ListItem value="ID">India</asp:ListItem>
																			<asp:ListItem value="MX">Mexico</asp:ListItem>
																			<asp:ListItem value="NZ">New Zealand</asp:ListItem>
																			<asp:ListItem value="na">Other</asp:ListItem>
																			<asp:ListItem value="PR">Puerto Rico</asp:ListItem>
																			<asp:ListItem value="SG">Singapore</asp:ListItem>
																			<asp:ListItem value="ZA">South Africa</asp:ListItem>
																			<asp:ListItem value="TG">TOGO</asp:ListItem>
																			<asp:ListItem value="UK">United Kingdom</asp:ListItem>
																			<asp:ListItem selected="True" value="US">USA</asp:ListItem>
																			<asp:ListItem value="VI">Virgin Islands</asp:ListItem>
																		</asp:dropdownlist></td>
																</tr>
																<tr>
																	<td vAlign="top" align="left">Estado / provincia<span class="Required">*</span></td>
																	<td style="WIDTH: 282px"><asp:dropdownlist id="ddlState" runat="server" Width="272px" CssClass="tbox">
																			<asp:ListItem Value="0" Selected="True">----- Please Select -----</asp:ListItem>
																			<asp:ListItem Value="AL">Alabama</asp:ListItem>
																			<asp:ListItem Value="AK">Alaska</asp:ListItem>
																			<asp:ListItem Value="AZ">Arizona</asp:ListItem>
																			<asp:ListItem Value="AR">Arkansas</asp:ListItem>
																			<asp:ListItem Value="AA">Armed Forces Americas</asp:ListItem>
																			<asp:ListItem Value="AE">Armed Forces Europe/Middle East/Canada</asp:ListItem>
																			<asp:ListItem Value="AP">Armed Forces Pacific</asp:ListItem>
																			<asp:ListItem Value="CA">California</asp:ListItem>
																			<asp:ListItem Value="CO">Colorado</asp:ListItem>
																			<asp:ListItem Value="CT">Connecticut</asp:ListItem>
																			<asp:ListItem Value="DE">Delaware</asp:ListItem>
																			<asp:ListItem Value="DC">District of Columbia</asp:ListItem>
																			<asp:ListItem Value="FL">Florida</asp:ListItem>
																			<asp:ListItem Value="GA">Georgia</asp:ListItem>
																			<asp:ListItem Value="HI">Hawaii</asp:ListItem>
																			<asp:ListItem Value="ID">Idaho</asp:ListItem>
																			<asp:ListItem Value="IL">Illinois</asp:ListItem>
																			<asp:ListItem Value="IN">Indiana</asp:ListItem>
																			<asp:ListItem Value="IA">Iowa</asp:ListItem>
																			<asp:ListItem Value="KS">Kansas</asp:ListItem>
																			<asp:ListItem Value="KY">Kentucky</asp:ListItem>
																			<asp:ListItem Value="LA">Louisiana</asp:ListItem>
																			<asp:ListItem Value="ME">Maine</asp:ListItem>
																			<asp:ListItem Value="MD">Maryland</asp:ListItem>
																			<asp:ListItem Value="MA">Massachusetts</asp:ListItem>
																			<asp:ListItem Value="MI">Michigan</asp:ListItem>
																			<asp:ListItem Value="MN">Minnesota</asp:ListItem>
																			<asp:ListItem Value="MS">Mississippi</asp:ListItem>
																			<asp:ListItem Value="MO">Missouri</asp:ListItem>
																			<asp:ListItem Value="MT">Montana</asp:ListItem>
																			<asp:ListItem Value="NE">Nebraska</asp:ListItem>
																			<asp:ListItem Value="NV">Nevada</asp:ListItem>
																			<asp:ListItem Value="NH">New Hampshire</asp:ListItem>
																			<asp:ListItem Value="NJ">New Jersey</asp:ListItem>
																			<asp:ListItem Value="NM">New Mexico</asp:ListItem>
																			<asp:ListItem Value="NY">New York</asp:ListItem>
																			<asp:ListItem Value="NC">North Carolina</asp:ListItem>
																			<asp:ListItem Value="ND">North Dakota</asp:ListItem>
																			<asp:ListItem Value="OH">Ohio</asp:ListItem>
																			<asp:ListItem Value="OK">Oklahoma</asp:ListItem>
																			<asp:ListItem Value="OR">Oregon</asp:ListItem>
																			<asp:ListItem Value="PA">Pennsylvania</asp:ListItem>
																			<asp:ListItem Value="RI">Rhode Island</asp:ListItem>
																			<asp:ListItem Value="SC">South Carolina</asp:ListItem>
																			<asp:ListItem Value="SD">South Dakota</asp:ListItem>
																			<asp:ListItem Value="TN">Tennessee</asp:ListItem>
																			<asp:ListItem Value="TX">Texas</asp:ListItem>
																			<asp:ListItem Value="UT">Utah</asp:ListItem>
																			<asp:ListItem Value="VT">Vermont</asp:ListItem>
																			<asp:ListItem Value="VA">Virginia</asp:ListItem>
																			<asp:ListItem Value="WA">Washington</asp:ListItem>
																			<asp:ListItem Value="WV">West Virginia</asp:ListItem>
																			<asp:ListItem Value="WI">Wisconsin</asp:ListItem>
																			<asp:ListItem Value="WY">Wyoming</asp:ListItem>
																		</asp:dropdownlist></td>
																</tr>
																<tr>
																	<td vAlign="top" align="left">Código postal<span class="Required">*</span>
																	</td>
																	<td style="WIDTH: 282px"><asp:textbox id="txtZipCode" runat="server" CssClass="tbox" MaxLength="25"></asp:textbox></td>
																	<td align="left"><asp:requiredfieldvalidator id="rfvZipCode" runat="server" ControlToValidate="txtZipCode" ErrorMessage="You have to enter your Zip/Postal Code">*</asp:requiredfieldvalidator></td>
																</tr>
																<tr>
																	<td vAlign="top" align="left">Tel. de día;<span class="Required">*</span>
																	</td>
																	<td style="WIDTH: 282px"><asp:textbox id="txtDayPhone" style="DISPLAY: none" runat="server" Width="44px" CssClass="tbox"></asp:textbox><asp:textbox onkeypress="ResetControls('txtDayPhone', event);return validateNumber('txtDayPhone1',event);"
																			id="txtDayPhone1" size="3" onkeyup="return setPhone('txtDayPhone',1,this,3,event);" runat="server" Width="44px" CssClass="tbox" MaxLength="3"></asp:textbox>&nbsp;-&nbsp;
																		<asp:textbox onkeypress="ResetControls('txtDayPhone', event);return validateNumber('txtDayPhone2',event);"
																			id="txtDayPhone2" size="3" onkeyup="return setPhone('txtDayPhone',2,this,3,event);" runat="server"
																			Width="44px" CssClass="tbox" MaxLength="3"></asp:textbox>&nbsp;-&nbsp;
																		<asp:textbox onkeypress="ResetControls('txtDayPhone', event);return validateNumber('txtDayPhone3',event);"
																			id="txtDayPhone3" size="4" onkeyup="return setPhone('txtDayPhone',3, this, 4, event);" runat="server"
																			Width="44px" CssClass="tbox" MaxLength="4"></asp:textbox>&nbsp;Ext:&nbsp;
																		<asp:textbox onkeypress="return validateNumber('txtDayPhoneExt',event);" id="txtDayPhoneExt"
																			size="3" runat="server" Width="44px" CssClass="tbox" MaxLength="4"></asp:textbox></td>
																	<td align="left">
																		<table class="FormContent" cellSpacing="0" cellPadding="0">
																			<tr>
																				<td><asp:regularexpressionvalidator id="revDayPhone" runat="server" ControlToValidate="txtDayPhone" ErrorMessage="DayPhone Number is invalid"
																						ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}">*</asp:regularexpressionvalidator></td>
																			</tr>
																			<tr>
																				<td><asp:requiredfieldvalidator id="rfvDayPhone" runat="server" ControlToValidate="txtDayPhone" ErrorMessage="DayPhone Number is invalid">*</asp:requiredfieldvalidator></td>
																			</tr>
																		</table>
																	</td>
																</tr>
																<tr>
																	<td vAlign="top" align="left">Tel. de noche
																	</td>
																	<td style="WIDTH: 282px"><asp:textbox id="txtEveningPhone" style="DISPLAY: none" runat="server" Width="44px" CssClass="tbox"></asp:textbox><asp:textbox onkeypress="ResetControls('txtEveningPhone', event);return validateNumber('txtEveningPhone1',event);"
																			id="txtEveningPhone1" size="3" onkeyup="return setPhone('txtEveningPhone',1,this,3,event);" runat="server" Width="44px" CssClass="tbox" MaxLength="3"></asp:textbox>&nbsp;-&nbsp;
																		<asp:textbox onkeypress="ResetControls('txtEveningPhone', event);return validateNumber('txtEveningPhone2',event);"
																			id="txtEveningPhone2" size="3" onkeyup="return setPhone('txtEveningPhone',2,this,3,event);"
																			runat="server" Width="44px" CssClass="tbox" MaxLength="3"></asp:textbox>&nbsp;-&nbsp;
																		<asp:textbox onkeypress="ResetControls('txtEveningPhone', event);return validateNumber('txtEveningPhone3',event);"
																			id="txtEveningPhone3" size="4" onkeyup="return setPhone('txtEveningPhone',3, this, 4, event);"
																			runat="server" Width="44px" CssClass="tbox" MaxLength="4"></asp:textbox>&nbsp;Ext:&nbsp;
																		<asp:textbox size="3" onkeypress="return validateNumber('txtEveningPhoneExt',event);" id="txtEveningPhoneExt"
																			runat="server" Width="44px" CssClass="tbox" MaxLength="4"></asp:textbox></td>
																	<td align="left"></td>
																</tr>
																<tr>
																	<td vAlign="top" align="left">Mejor momento para llamar<span class="Required">*</span>
																	</td>
																	<td style="WIDTH: 282px"><asp:dropdownlist id="ddlBestTime" runat="server" CssClass="tbox">
																			<asp:ListItem Value="4" Selected="True">----- Please Select -----</asp:ListItem>
																			<asp:ListItem Value="1">Morning</asp:ListItem>
																			<asp:ListItem Value="2">Afternoon</asp:ListItem>
																			<asp:ListItem Value="3">Evening</asp:ListItem>
																			<asp:ListItem Value="4">Any Time</asp:ListItem>
																		</asp:dropdownlist></td>
																</tr>
																<tr>
																	<td vAlign="top" align="left">Tipo de organización<span class="Required">*</span>
																	</td>
																	<td style="WIDTH: 282px"><asp:dropdownlist id="ddlOrgType" runat="server" CssClass="tbox">
																			<asp:ListItem Value="99" Selected="True">----- Please Select -----</asp:ListItem>
																			<asp:ListItem Value="10">Amateur Athletic Union</asp:ListItem>
																			<asp:ListItem Value="9">Amateur Softball Association</asp:ListItem>
																			<asp:ListItem Value="18">American Youth Soccer Association (AYSC)</asp:ListItem>
																			<asp:ListItem Value="21">Basketball Congress International</asp:ListItem>
																			<asp:ListItem Value="23">Catholic Youth Organization (CYO)</asp:ListItem>
																			<asp:ListItem Value="27">Church</asp:ListItem>
																			<asp:ListItem Value="7">College</asp:ListItem>
																			<asp:ListItem Value="28">DECA</asp:ListItem>
																			<asp:ListItem Value="3">Elementary</asp:ListItem>
																			<asp:ListItem Value="29">High School</asp:ListItem>
																			<asp:ListItem Value="5">Junior High School</asp:ListItem>
																			<asp:ListItem Value="26">Key Club</asp:ListItem>
																			<asp:ListItem Value="11">Little League Baseball</asp:ListItem>
																			<asp:ListItem Value="4">Middle School</asp:ListItem>
																			<asp:ListItem Value="1">Non School Or Organizational Affiliation</asp:ListItem>
																			<asp:ListItem Value="99">Other</asp:ListItem>
																			<asp:ListItem Value="12">Police Athletic League</asp:ListItem>
																			<asp:ListItem Value="2">Preschool</asp:ListItem>
																			<asp:ListItem Value="14">Reviving Baseball In Inner Cities</asp:ListItem>
																			<asp:ListItem Value="6">Senior High School</asp:ListItem>
																			<asp:ListItem Value="17">Soccer Association For Youth</asp:ListItem>
																			<asp:ListItem Value="25">TD Club</asp:ListItem>
																			<asp:ListItem Value="15">United States Flag &amp; Touch Football</asp:ListItem>
																			<asp:ListItem Value="16">United States Youth Soccer (USYS)</asp:ListItem>
																			<asp:ListItem Value="8">University</asp:ListItem>
																			<asp:ListItem Value="19">US Amateur Soccer Association</asp:ListItem>
																			<asp:ListItem Value="22">USA Hockey</asp:ListItem>
																			<asp:ListItem Value="30">USA Wrestling</asp:ListItem>
																			<asp:ListItem Value="20">USSSA Basketball</asp:ListItem>
																			<asp:ListItem Value="24">YMCA</asp:ListItem>
																			<asp:ListItem Value="13">Youth Basketball Of America</asp:ListItem>
																			<asp:ListItem Value="34">YWCA</asp:ListItem>
																		</asp:dropdownlist></td>
																</tr>
																<tr>
																	<td vAlign="top" align="left" style="HEIGHT: 19px">Tipo de grupo<span class="Required">*</span>
																	</td>
																	<td style="WIDTH: 282px; HEIGHT: 19px"><asp:dropdownlist id="ddlGroupType" runat="server" CssClass="tbox">
																			<asp:ListItem Value="99" Selected="True">----- Please Select -----</asp:ListItem>
																			<asp:ListItem Value="28">Badminton</asp:ListItem>
																			<asp:ListItem Value="1">Bands</asp:ListItem>
																			<asp:ListItem Value="2">Baseball</asp:ListItem>
																			<asp:ListItem Value="3">Basketball</asp:ListItem>
																			<asp:ListItem Value="36">Booster</asp:ListItem>
																			<asp:ListItem Value="29">Bowling</asp:ListItem>
																			<asp:ListItem Value="4">Cheerleading</asp:ListItem>
																			<asp:ListItem Value="19">Children's Club</asp:ListItem>
																			<asp:ListItem Value="37">Civic / Community Group</asp:ListItem>
																			<asp:ListItem Value="38">Club</asp:ListItem>
																			<asp:ListItem Value="27">Company</asp:ListItem>
																			<asp:ListItem Value="30">Crew</asp:ListItem>
																			<asp:ListItem Value="43">Dance</asp:ListItem>
																			<asp:ListItem Value="31">Drill Team</asp:ListItem>
																			<asp:ListItem Value="32">Field Hockey</asp:ListItem>
																			<asp:ListItem Value="6">Football</asp:ListItem>
																			<asp:ListItem Value="42">Foundation</asp:ListItem>
																			<asp:ListItem Value="40">Fraternity</asp:ListItem>
																			<asp:ListItem Value="33">Golf</asp:ListItem>
																			<asp:ListItem Value="7">Gymnastics</asp:ListItem>
																			<asp:ListItem Value="44">Handball</asp:ListItem>
																			<asp:ListItem Value="8">Hockey</asp:ListItem>
																			<asp:ListItem Value="21">Junior High School</asp:ListItem>
																			<asp:ListItem Value="23">Lacrosse</asp:ListItem>
																			<asp:ListItem Value="9">Music &amp; Art</asp:ListItem>
																			<asp:ListItem Value="18">Non-Profit</asp:ListItem>
																			<asp:ListItem Value="20">Nursing Students</asp:ListItem>
																			<asp:ListItem Value="16">Organization</asp:ListItem>
																			<asp:ListItem Value="99">Other</asp:ListItem>
																			<asp:ListItem Value="34">Pom Pom</asp:ListItem>
																			<asp:ListItem Value="5">Religious</asp:ListItem>
																			<asp:ListItem Value="26">Ringette</asp:ListItem>
																			<asp:ListItem Value="10">School</asp:ListItem>
																			<asp:ListItem Value="11">Scouts</asp:ListItem>
																			<asp:ListItem Value="25">Skating</asp:ListItem>
																			<asp:ListItem Value="12">Soccer</asp:ListItem>
																			<asp:ListItem Value="13">Softball</asp:ListItem>
																			<asp:ListItem Value="41">Sorority</asp:ListItem>
																			<asp:ListItem Value="14">Swimming</asp:ListItem>
																			<asp:ListItem Value="35">Tennis</asp:ListItem>
																			<asp:ListItem Value="17">Track</asp:ListItem>
																			<asp:ListItem Value="15">Volleyball</asp:ListItem>
																			<asp:ListItem Value="24">Wrestling</asp:ListItem>
																			<asp:ListItem Value="22">Youth Support</asp:ListItem>
																		</asp:dropdownlist></td>
																</tr>
																<tr>
																	<td vAlign="top" align="left">Nombre de miembros del grupo<span class="Required">*</span>
																	</td>
																	<td style="WIDTH: 282px"><asp:textbox onkeypress="return validateNumber('txtGroupNumber',event);" id="txtGroupNumber"
																			runat="server" CssClass="tbox" MaxLength="5"></asp:textbox></td>
																	<td><asp:requiredfieldvalidator id="rfvGroupNumber" runat="server" ControlToValidate="txtGroupNumber" ErrorMessage="Please specify your Group's size">*</asp:requiredfieldvalidator></td>
																</tr>
																<tr>
																	<td style="WIDTH: 533px" vAlign="top" align="left" colSpan="2">Los meses que su 
																		grupo recauda fondos<span class="Required">*&nbsp;&nbsp;&nbsp;
																			<asp:textbox id="txtMonthList" style="DISPLAY: none" runat="server"></asp:textbox></span></td>
																	<td><asp:requiredfieldvalidator id="rfvMonthList" runat="server" ControlToValidate="txtMonthList" ErrorMessage="Please select a date for your fundraiser to start">*</asp:requiredfieldvalidator></td>
																</tr>
																<tr>
																	<td style="WIDTH: 533px" vAlign="top" align="left" colSpan="2">
																		<table class="FormContent" id="chkMonthList" onclick="CheckBoxValidation(this)" cellSpacing="0"
																			cellPadding="2" width="80%" align="left" summary="Reader's Digest" border="0">
																			<tr>
																				<td vAlign="middle" align="left" width="25%"><asp:checkbox id="chkMonthList0" runat="server" Text="January"></asp:checkbox></td>
																				<td vAlign="middle" align="left" width="25%"><asp:checkbox id="chkMonthList1" runat="server" Text="February"></asp:checkbox></td>
																				<td vAlign="middle" align="left" width="25%"><asp:checkbox id="chkMonthList2" runat="server" Text="March"></asp:checkbox></td>
																				<td vAlign="middle" align="left" width="25%"><asp:checkbox id="chkMonthList3" runat="server" Text="April"></asp:checkbox></td>
																			</tr>
																			<tr>
																				<td vAlign="middle" align="left"><asp:checkbox id="chkMonthList4" runat="server" Text="May"></asp:checkbox></td>
																				<td vAlign="middle" align="left"><asp:checkbox id="chkMonthList5" runat="server" Text="June"></asp:checkbox></td>
																				<td vAlign="middle" align="left"><asp:checkbox id="chkMonthList6" runat="server" Text="July"></asp:checkbox></td>
																				<td vAlign="middle" align="left"><asp:checkbox id="chkMonthList7" runat="server" Text="August"></asp:checkbox></td>
																			</tr>
																			<tr>
																				<td vAlign="middle" align="left"><asp:checkbox id="chkMonthList8" runat="server" Text="September"></asp:checkbox></td>
																				<td vAlign="middle" align="left"><asp:checkbox id="chkMonthList9" runat="server" Text="October"></asp:checkbox></td>
																				<td vAlign="middle" align="left"><asp:checkbox id="chkMonthList10" runat="server" Text="November"></asp:checkbox></td>
																				<td vAlign="middle" align="left"><asp:checkbox id="chkMonthList11" runat="server" Text="December"></asp:checkbox></td>
																			</tr>
																		</table>
																	</td>
																</tr>
																<tr>
																	<td vAlign="middle" align="left">¿Es usted quien toma las decisiones?<span class="Required">*</span>
																	</td>
																	<td style="WIDTH: 282px" valign="middle">&nbsp;
																		<table class="FormContent" id="rdbDecision" cellSpacing="0" align="left" summary="Reader's Digest"
																			border="0" cellPadding="0">
																			<tr>
																				<td>
																					<asp:radiobutton id="rdbDecision0" runat="server" Text="Yes" GroupName="rdbDecisions" Checked="True"></asp:radiobutton>&nbsp;
																					<asp:radiobutton id="rdbDecision1" runat="server" Text="No" GroupName="rdbDecisions"></asp:radiobutton>&nbsp;
																					<asp:textbox id="txtDecision" style="DISPLAY: none" runat="server">Yes</asp:textbox>
																				</td>
																			</tr>
																		</table>
																	</td>
																	<td valign="middle"><asp:requiredfieldvalidator id="rfvDecision" runat="server" ControlToValidate="txtDecision" ErrorMessage="Please specify your role in choosing a fundraising option">*</asp:requiredfieldvalidator></td>
																</tr>
																<tr>
																	<td style="WIDTH: 533px" vAlign="top" align="left" colSpan="2">¿En que programa de 
																		recaudación de fondos está interesado?
																	</td>
																</tr>
																<tr>
																	<td style="WIDTH: 533px" vAlign="top" align="left" colSpan="2">
																		<table class="FormContent" cellSpacing="0" cellPadding="2" width="90%" summary="Reader's Digest"
																			border="0">
																			<tr vAlign="middle" align="left">
																				<td width="39%"><asp:checkbox id="chkProgramList0" runat="server" Text="Magazines"></asp:checkbox></td>
																				<td width="30%"><asp:checkbox id="chkProgramList1" runat="server" Text="Gift Brouchures"></asp:checkbox></td>
																				<td width="30%"><asp:checkbox id="chkProgramList2" runat="server" Text="Scratchcards"></asp:checkbox></td>
																			</tr>
																			<tr vAlign="middle" align="left">
																				<td><asp:checkbox id="chkProgramList3" runat="server" Text="World's Finest Chocolate"></asp:checkbox></td>
																				<td>&nbsp;</td>
																				<td>&nbsp;</td>
																			</tr>
																		</table>
																	</td>
																</tr>
																<tr>
																	<td style="WIDTH: 533px" vAlign="top" align="left" colSpan="2">¡Si, envíenme mi 
																		correo mensual!
																		<asp:checkbox id="chkNewsLetter" runat="server" Text="&amp;nbsp;" Checked="True"></asp:checkbox></td>
																</tr>
																<tr>
																	<td vAlign="top" align="left">Comentarios o preguntas
																	</td>
																	<td style="WIDTH: 282px">&nbsp;</td>
																</tr>
																<tr>
																	<td style="WIDTH: 533px; HEIGHT: 74px" vAlign="top" align="left" colSpan="2"><asp:textbox id="txtComments" runat="server" Height="134px" Width="296px" TextMode="MultiLine"></asp:textbox></td>
																</tr>
																<tr>
																	<td style="WIDTH: 533px" vAlign="top" align="left" colSpan="2">&nbsp;</td>
																</tr>
																<tr>
																	<td style="WIDTH: 533px" vAlign="top" align="left" colSpan="2"><asp:imagebutton id="Submit" runat="server" AlternateText="Enviame mi kit gratis" ImageUrl="~/Resources/images/_RecaudarFondos_/_classic_/es-us/button_form.gif"
																			BorderWidth="0"></asp:imagebutton></td>
																</tr>
															</table>
														</td>
													</tr>
													<tr>
														<td class="PageContent" align="left" colSpan="2">&nbsp;</td>
													</tr>
												</TBODY>
											</table>
										</td>
									</tr>
								</TBODY>
							</table>
						</td>
					</tr>
				</TBODY>
			</table>
			</TD></TR>
			<tr>
				<td class="Footer" style="HEIGHT: 40px" vAlign="middle" align="center" bgColor="#febc1d"><asp:PlaceHolder Runat="server" ID="EfunBottom"></asp:PlaceHolder><!--<a class="FooterLink" href="http://www.efundraising.com">English</a>-->
					&nbsp;| &nbsp;<A class="FooterLink" href="Default.aspx">Pagina Principal</A> &nbsp;|&nbsp;
					<A class="FooterLink" href="Scratchcard.aspx">Tarjetas Sorpresa</A> &nbsp;|&nbsp;
					<A class="FooterLink" href="Chocolate.aspx">Chocolate</A> &nbsp;|&nbsp; <!--<a class="FooterLink" href="http://www.magfundraising.com/?gid=F8C621E5-62E9-4193-A44B-FD1CBC67C476&amp;pr_id=4159"
						target="_blank">Revista en línea </a>--><asp:PlaceHolder Runat="server" ID="MagBottom"></asp:PlaceHolder>&nbsp;|&nbsp;
					<A class="FooterLink" href="CookieDough.aspx">Masa para hornear galletas</A> &nbsp;|<br>
					<A class="FooterLink" href="Gift.aspx">Folletos con regalos</A> &nbsp;|&nbsp; <A class="FooterLink" href="Tips.aspx">
						Consejos para recaudar fondos</A> &nbsp;|&nbsp; <A class="FooterLink" href="http://www.fundraising.com/free-fundraising-kit.aspx" target="_blank">
						Kit de información gratis</A> &nbsp;|&nbsp; <A class="FooterLink" href="ContactUs.aspx">
						Sobre Nosotros</A></td>
			</tr>
			</TBODY></TABLE></form>
	</body>
</HTML>
