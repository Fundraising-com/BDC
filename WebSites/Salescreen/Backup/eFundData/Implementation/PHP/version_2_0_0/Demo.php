<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<title>webservice.efundraising.com PHP Client TESTING</title>
<style type='text/css' rel='stylesheet'>
	.NormalText {
		font-family: verdana;
		font-size: xx-small;
	}
</style>
</head>
<body>
<?php 		
	// Include required library.
	require_once("eFundDataClient.php");

	// Create a new eFundDataClient object
	$client = new eFundDataClient();

	$isLogin = $client->InitWebServiceConnection('WSTest','test');			

	var_dump($client->IsLoggedIn());	

	if($client->IsLoggedIn()) {	

		// Get report passing start datetime and end datetime.
		$myReport = $client->GetSalesReport("2005-04-10 00:00:00", "2005-04-20 23:59:59");	
		
		// Uncomment below to debug return value
		// var_dump($myReport);

		// Print report
		print "<table cellspacing='0' border='1'>
	       		<tr class='NormalText'><td>FirstName</td>
	       		<td>LastName</td>
	       		<td>Email</td>
	       		<td>Address</td>
	       		<td>City</td>
	       		<td>State</td>
	       		<td>Zip</td>
	       		<td>Country</td>
	       		<td>DayPhone</td>
	       		<td>Evening Phone</td>
	       		<td>Date</td>
	       		<td>OrganizationType</td>
	       		<td>Group Type</td>
	       		<td>Group Size</td>
			<td>Sales ID</td>
			<td>DateSales</td>
			<td>ConfirmDateSales</td>
			<td>ShipDate</td>
			<td>Product Class Description</td>
			<td>Total Products</td>
			<td>Total Amount</td><tr>";

		// Test if return value exists
		if (array_key_exists('GetSalesReportResult', $myReport) && $myReport['GetSalesReportResult'] != "") {

			// Print out the contacts in the array
			$FundraiserList = $myReport['GetSalesReportResult']['Fundraiser'];

			for($i = 0; $i < count($FundraiserList); $i++) {
				print "<tr class='NormalText'>";
				print "<td>" . $FundraiserList[$i]['FirstName'] . "</td>";
				print "<td>" . $FundraiserList[$i]['LastName'] . "</td>";
				print "<td>" . $FundraiserList[$i]['Email'] . "</td>";
				print "<td>" . $FundraiserList[$i]['Address'] . "</td>";
				print "<td>" . $FundraiserList[$i]['City'] . "</td>";
				print "<td>" . $FundraiserList[$i]['State'] . "</td>";
				print "<td>" . $FundraiserList[$i]['Zip'] . "</td>";
				print "<td>" . $FundraiserList[$i]['Country'] . "</td>";
				print "<td>" . $FundraiserList[$i]['DayPhone'] . "</td>";
				print "<td>" . $FundraiserList[$i]['EveningPhone'] . "</td>";
				print "<td>" . $FundraiserList[$i]['Date'] . "</td>";
				print "<td>" . $FundraiserList[$i]['OrganizationType'] . "</td>";
				print "<td>" . $FundraiserList[$i]['GroupType'] . "</td>";
				print "<td>" . $FundraiserList[$i]['GroupSize'] . "</td>";
				print "<td>" . $FundraiserList[$i]['Sale']['SalesID'] . "</td>";
				print "<td>" . $FundraiserList[$i]['Sale']['DateSales'] . "</td>";
				print "<td>" . $FundraiserList[$i]['Sale']['ConfirmDateSales'] . "</td>";
				print "<td>" . $FundraiserList[$i]['Sale']['ShipDate'] . "</td>";
				print "<td>" . $FundraiserList[$i]['Sale']['ProductClassDesc'] . "</td>";
				print "<td>" . $FundraiserList[$i]['Sale']['TotalProduct'] . "</td>";
				print "<td>$" . $FundraiserList[$i]['Sale']['TotalAmount'] . "</td>";
			}
		} else {
			print("<tr class='NormalText'><td colspan='14'>No Results have been found</td></tr>");
		}
		print "</table>";
	} else {
		print("<div class='NormalText'>You are not authenticate on WebServices.efundraising.com</div>");
	}

	// Logout and release resources
	$client->UnloadWebServiceConnection();		
?>
</body>
</html>
