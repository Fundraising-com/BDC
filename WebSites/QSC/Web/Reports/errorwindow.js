		<div id="dwindow" style="DISPLAY:none;LEFT:0px;CURSOR:hand;POSITION:absolute;TOP:0px;BACKGROUND-COLOR:#a6a6a6"
			onMousedown="initializedrag(event)" onMouseup="stopdrag()" onSelectStart="return false">
			<div align="right" style="BACKGROUND-IMAGE:url(/QSPFulfillment/customerservice/images/errormessagetitle.gif);BACKGROUND-COLOR:#a6a6a6"><img src="/QSPFulfillment/customerservice/images/close.gif" onClick="closeit()"></div>
			<div id="dwindowcontent" style="HEIGHT:100%">
				<table bgcolor="#a6a6a6" cellspacing="0" cellpadding="2" width="100%" height="100%">
					<tr>
						<td>
							<iframe id="cframe" src="/blank.htm" width="100%" height="100%" SCROLLING="yes"></iframe>
						</td>
					</tr>
				</table>
			</div>
		</div>
		<div id="dpleasewait" style="DISPLAY:none;LEFT:0px;CURSOR:hand;POSITION:absolute;TOP:0px;background-color:#FFFFFF">
			<asp:Label Runat="server" ID="lblWait" Font-Name="Verdana" Font-Size="large" Font-Bold="True" ForeColor="#ff0000">Please wait while refreshing...</asp:Label>
		</div>