<%@ Register TagPrefix="uc1" TagName="CampaignMaintenanceControl" Src="Control/CampaignMaintenanceControl.ascx" %>
<%@ Page language="c#" Codebehind="CampaignMaintenance.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.AcctMgt.CampaignMaintenance" %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<HTML>
	<HEAD>
		<title>Campaign Maintenance</title>
		<link href="../Includes/QSPFulfillment.css" type="text/css" rel="stylesheet">
      <script src="../Includes/jquery-3.2.1.min.js"></script>
	</HEAD>
	<body id="BodyTag" runat="server" bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0"
		marginwidth="0" marginheight="0">
		<!--#include file="../CustomerService/fctjavascriptall.js"-->
		<script>
         $(document).ready(function () {

            $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl44_chkAllowOnlineAccountDelivery").on("click.readonly", function (event) { event.preventDefault(); }).css("opacity", "0.3");

            var isLandedCheckedCookieDough = $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl44_chkLanded").is(':checked');
            if (isLandedCheckedCookieDough) {
               $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl44_chkFieldSupplyPacket").off('.readonly').removeAttr("readonly").css("opacity", "1");
            }
            else {
               $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl44_chkFieldSupplyPacket").on("click.readonly", function (event) { event.preventDefault(); }).css("opacity", "0.3");
            }

            $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl70_chkAllowOnlineAccountDelivery").on("click.readonly", function (event) { event.preventDefault(); }).css("opacity", "0.3");

            var isLandedCheckedRiseNShine = $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl53_chkLanded").is(':checked');
            if (isLandedCheckedRiseNShine) {
               $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl53_chkAllowOnlineAccountDelivery").on("click.readonly", function (event) { event.preventDefault(); }).css("opacity", "0.3");
            }
            else {
               $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl53_chkAllowOnlineAccountDelivery").off('.readonly').removeAttr("readonly").css("opacity", "1");
            }

            var isLandedCheckedBloom = $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl55_chkLanded").is(':checked');
            if (isLandedCheckedBloom) {
               $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl55_chkAllowOnlineAccountDelivery").on("click.readonly", function (event) { event.preventDefault(); }).css("opacity", "0.3");
            }
            else {
               $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl55_chkAllowOnlineAccountDelivery").off('.readonly').removeAttr("readonly").css("opacity", "1");
            }

            var isLandedCheckedSweet = $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl59_chkLanded").is(':checked');
            if (isLandedCheckedSweet) {
               $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl59_chkAllowOnlineAccountDelivery").on("click.readonly", function (event) { event.preventDefault(); }).css("opacity", "0.3");
            }
            else {
               $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl59_chkAllowOnlineAccountDelivery").off('.readonly').removeAttr("readonly").css("opacity", "1");
            }

            var isLandedCheckedSavingsPass = $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl64_chkLanded").is(':checked');
            if (isLandedCheckedSavingsPass) {
                $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl64_chkAllowOnlineAccountDelivery").on("click.readonly", function (event) { event.preventDefault(); }).css("opacity", "0.3");
            }
            else {
                $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl64_chkAllowOnlineAccountDelivery").off('.readonly').removeAttr("readonly").css("opacity", "1");
            }

            var isLandedCheckedLeapLabels = $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl72_chkLanded").is(':checked');
            if (isLandedCheckedLeapLabels) {
                $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl72_chkAllowOnlineAccountDelivery").on("click.readonly", function (event) { event.preventDefault(); }).css("opacity", "0.3");
            }
            else {
                $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl64_chkAllowOnlineAccountDelivery").off('.readonly').removeAttr("readonly").css("opacity", "1");
            }

            var isLandedCheckedGiftCard = $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl65_chkLanded").is(':checked');
            if (isLandedCheckedGiftCard) {
               $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl65_chkAllowOnlineAccountDelivery").on("click.readonly", function (event) { event.preventDefault(); }).css("opacity", "0.3");
            }
            else {
               $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl65_chkAllowOnlineAccountDelivery").off('.readonly').removeAttr("readonly").css("opacity", "1");
            }

            var isLandedCheckedTervis = $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl67_chkLanded").is(':checked');
            if (isLandedCheckedTervis) {
                $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl67_chkAllowOnlineAccountDelivery").on("click.readonly", function (event) { event.preventDefault(); }).css("opacity", "0.3");
            }
            else {
                $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl67_chkAllowOnlineAccountDelivery").off('.readonly').removeAttr("readonly").css("opacity", "1");
            }

            var isLandedCheckedTheCure = $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl69_chkLanded").is(':checked');
            if (isLandedCheckedTheCure) {
                $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl69_chkAllowOnlineAccountDelivery").on("click.readonly", function (event) { event.preventDefault(); }).css("opacity", "0.3");
            }
            else {
                $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl69_chkAllowOnlineAccountDelivery").off('.readonly').removeAttr("readonly").css("opacity", "1");
            }

            var isLandedCheckedCoolCards = $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl73_chkLanded").is(':checked');
            if (isLandedCheckedCoolCards) {
                $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl73_chkAllowOnlineAccountDelivery").on("click.readonly", function (event) { event.preventDefault(); }).css("opacity", "0.3");
            }
            else {
                $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl73_chkAllowOnlineAccountDelivery").off('.readonly').removeAttr("readonly").css("opacity", "1");
            }

            $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl44_chkLanded").click(function () {
               var isLandedCheckedCookieDough = $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl44_chkLanded").is(':checked');
               if (isLandedCheckedCookieDough) {
                  $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl44_chkFieldSupplyPacket").prop("checked", true);
                  $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl44_chkFieldSupplyPacket").off('.readonly').removeAttr("readonly").css("opacity", "1");
                  $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl44_chkAllowOnlineAccountDelivery").prop("checked", true);
               }
               else {
                  $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl44_chkFieldSupplyPacket").prop("checked", false);
                  $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl44_chkFieldSupplyPacket").on("click.readonly", function (event) { event.preventDefault(); }).css("opacity", "0.3");
                  $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl44_chkAllowOnlineAccountDelivery").prop("checked", false);
              }
           });

           $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl70_chkLanded").click(function () {
               var isLandedCheckedTastyTreats = $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl70_chkLanded").is(':checked');
               if (isLandedCheckedTastyTreats) {
                   $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl70_chkAllowOnlineAccountDelivery").prop("checked", true);
               }
               else {
                   $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl70_chkAllowOnlineAccountDelivery").prop("checked", false);
               }
            });

            $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl53_chkLanded").click(function () {
               var isLandedCheckedRiseNShine = $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl53_chkLanded").is(':checked');
               if (isLandedCheckedRiseNShine)
               {
                  $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl53_chkAllowOnlineAccountDelivery").on("click.readonly", function (event) { event.preventDefault(); }).css("opacity", "0.3");
                  $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl53_chkAllowOnlineAccountDelivery").prop("checked", true);
               }
               else
               {
                  $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl53_chkAllowOnlineAccountDelivery").off('.readonly').removeAttr("readonly").css("opacity", "1");
                  $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl53_chkAllowOnlineAccountDelivery").prop("checked", false);
               }
            });

            $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl55_chkLanded").click(function () {
               var isLandedCheckedBloom = $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl55_chkLanded").is(':checked');
               if (isLandedCheckedBloom) {
                  $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl55_chkAllowOnlineAccountDelivery").on("click.readonly", function (event) { event.preventDefault(); }).css("opacity", "0.3");
                  $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl55_chkAllowOnlineAccountDelivery").prop("checked", true);
               }
               else {
                  $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl55_chkAllowOnlineAccountDelivery").off('.readonly').removeAttr("readonly").css("opacity", "1");
                  $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl55_chkAllowOnlineAccountDelivery").prop("checked", false);
               }
            });

            $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl59_chkLanded").click(function () {
               var isLandedCheckedSweet = $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl59_chkLanded").is(':checked');
               if (isLandedCheckedSweet) {
                  $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl59_chkAllowOnlineAccountDelivery").on("click.readonly", function (event) { event.preventDefault(); }).css("opacity", "0.3");
                  $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl59_chkAllowOnlineAccountDelivery").prop("checked", true);
               }
               else {
                  $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl59_chkAllowOnlineAccountDelivery").off('.readonly').removeAttr("readonly").css("opacity", "1");
                  $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl59_chkAllowOnlineAccountDelivery").prop("checked", false);
               }
            });

            $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl64_chkLanded").click(function () {
                var isLandedCheckedSavingsPass = $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl64_chkLanded").is(':checked');
                if (isLandedCheckedSavingsPass) {
                    $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl64_chkAllowOnlineAccountDelivery").on("click.readonly", function (event) { event.preventDefault(); }).css("opacity", "0.3");
                    $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl64_chkAllowOnlineAccountDelivery").prop("checked", true);
                }
                else {
                    $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl64_chkAllowOnlineAccountDelivery").off('.readonly').removeAttr("readonly").css("opacity", "1");
                    $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl64_chkAllowOnlineAccountDelivery").prop("checked", false);
                }
            });

            $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl72_chkLanded").click(function () {
                var isLandedCheckedLeapLabels = $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl72_chkLanded").is(':checked');
                if (isLandedCheckedLeapLabels) {
                    $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl72_chkAllowOnlineAccountDelivery").on("click.readonly", function (event) { event.preventDefault(); }).css("opacity", "0.3");
                    $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl72_chkAllowOnlineAccountDelivery").prop("checked", true);
                }
                else {
                    $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl72_chkAllowOnlineAccountDelivery").off('.readonly').removeAttr("readonly").css("opacity", "1");
                    $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl72_chkAllowOnlineAccountDelivery").prop("checked", false);
                }
            });

            $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl65_chkLanded").click(function () {
               var isLandedCheckedGiftCard = $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl65_chkLanded").is(':checked');
               if (isLandedCheckedGiftCard) {
                  $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl65_chkAllowOnlineAccountDelivery").on("click.readonly", function (event) { event.preventDefault(); }).css("opacity", "0.3");
                  $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl65_chkAllowOnlineAccountDelivery").prop("checked", true);
               }
               else {
                  $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl65_chkAllowOnlineAccountDelivery").off('.readonly').removeAttr("readonly").css("opacity", "1");
                  $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl65_chkAllowOnlineAccountDelivery").prop("checked", false);
               }
            });

            $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl67_chkLanded").click(function () {
                var isLandedCheckedTervis = $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl67_chkLanded").is(':checked');
                if (isLandedCheckedTervis) {
                    $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl67_chkAllowOnlineAccountDelivery").on("click.readonly", function (event) { event.preventDefault(); }).css("opacity", "0.3");
                    $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl67_chkAllowOnlineAccountDelivery").prop("checked", true);
                }
                else {
                    $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl67_chkAllowOnlineAccountDelivery").off('.readonly').removeAttr("readonly").css("opacity", "1");
                    $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl67_chkAllowOnlineAccountDelivery").prop("checked", false);
                }
            });

            $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl69_chkLanded").click(function () {
                var isLandedCheckedTheCure = $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl69_chkLanded").is(':checked');
                if (isLandedCheckedTheCure) {
                    $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl69_chkAllowOnlineAccountDelivery").on("click.readonly", function (event) { event.preventDefault(); }).css("opacity", "0.3");
                    $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl69_chkAllowOnlineAccountDelivery").prop("checked", true);
                }
                else {
                    $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl69_chkAllowOnlineAccountDelivery").off('.readonly').removeAttr("readonly").css("opacity", "1");
                    $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl69_chkAllowOnlineAccountDelivery").prop("checked", false);
                }
            });

            $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl73_chkLanded").click(function () {
                var isLandedCheckedCoolCards = $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl73_chkLanded").is(':checked');
                if (isLandedCheckedCoolCards) {
                    $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl73_chkAllowOnlineAccountDelivery").on("click.readonly", function (event) { event.preventDefault(); }).css("opacity", "0.3");
                    $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl73_chkAllowOnlineAccountDelivery").prop("checked", true);
                }
                else {
                    $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl73_chkAllowOnlineAccountDelivery").off('.readonly').removeAttr("readonly").css("opacity", "1");
                    $("#ctrlCampaignMaintenanceControl_ctrlCampaignProgramMaintenanceControl_ctrlProgramMaintenanceControl73_chkAllowOnlineAccountDelivery").prop("checked", false);
                }
            });
         });
		</script>
      
      <script language="javascript">
			function SetProgramEnabledOld(evt) {
            var myEvent = ((window.event) ? (event) : (evt));
            var Element = ((window.event) ? (event.srcElement) : (evt.currentTarget));

				var clientIDPrefix = Element.id.replace("chkLanded", "");
				var bEnabled = document.getElementById(clientIDPrefix + "chkLanded").checked;

				if (document.getElementById(clientIDPrefix + "chkAllowOnlineAccountDelivery") != null) 
				{
				    document.getElementById(clientIDPrefix + "chkAllowOnlineAccountDelivery").checked = bEnabled;
                   
				    if (document.getElementById(clientIDPrefix + "lblProgramName").innerText != "Cookie Dough")
				    {
                        if (bEnabled) 
				        {
				            document.getElementById(clientIDPrefix + "chkAllowOnlineAccountDelivery").parentNode.disabled = true;
				            document.getElementById(clientIDPrefix + "chkAllowOnlineAccountDelivery").setAttribute("disabled", "disabled");
				        }
				        else 
				        {
				            document.getElementById(clientIDPrefix + "chkAllowOnlineAccountDelivery").parentNode.disabled = false;
				            document.getElementById(clientIDPrefix + "chkAllowOnlineAccountDelivery").removeAttribute("disabled");
				        }
                    }
				}				   

				if (document.getElementById(clientIDPrefix + "chkFieldSupplyPacket") != null) {
				   document.getElementById(clientIDPrefix + "chkFieldSupplyPacket").checked = bEnabled;
				   if (bEnabled) {
				      document.getElementById(clientIDPrefix + "chkFieldSupplyPacket").parentNode.disabled = false;
				      document.getElementById(clientIDPrefix + "chkFieldSupplyPacket").removeAttribute("disabled");
				   }
				   else {
				      document.getElementById(clientIDPrefix + "chkFieldSupplyPacket").parentNode.disabled = true;
				      document.getElementById(clientIDPrefix + "chkFieldSupplyPacket").setAttribute("disabled", "disabled");
				   }
				}
			}
		</script>
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<cc2:enhancedsmartnavigationcontrol id="ctrlEnhancedSmartNavigationControl" runat="server"></cc2:enhancedsmartnavigationcontrol>
			<div style="PADDING-LEFT: 5%; WIDTH: 95%"><br>
				<br>
				<h3>Confirmation Agreement Maintenance</h3>
				<br>
				<uc1:campaignmaintenancecontrol id="ctrlCampaignMaintenanceControl" runat="server"></uc1:campaignmaintenancecontrol></div>
			<asp:validationsummary id="ValidationSummary1" runat="server" showsummary="False" showmessagebox="True"></asp:validationsummary>
			<input type="hidden" id="hidDataBind" runat="server" value="0">
		</form>
		<!--#include file="../CustomerService/errorwindow.js"-->
	</body>
</HTML>
