<?php
	//-----------------------------------------------------------------------------------------------------
	//	Author		:	Louis Turmel		(louis.turmel@readersdigest.com)
	//      Supervisor By	: 	Stephen Lim 		(slim@rd.com)
	//	Campagny Name	:	efundraising.com - efundraising.com
	//	Project 	: 	Web Services Implementation 	(http://webservices.efundraising.com)
	//
	//	Writing Date	: 	April 13, 2005
	//	Last Update	:	April 20, 2005
	//	Release Date	:	April 14, 2005
	//	Version 	:	1.0.1
	//------------------------------------------------------------------------------------------------------
	
	@set_time_limit(0);
	@ini_set("memory_limit",-1);
	include("nusoap.php");		
	//include("ErrorTrap.php");	
	// class to implement easily the Web Service inside the PHP Server Side Code
	class eFundDataClient {
		
		var $WSNamespace;
		var $WSClient;
		var $WSProxy;
		var $IsLoginIn = false;
				
		// Function to get the Sell Report between two Dates from efundraising
		// Return value: array variable
		function GetLeadsReport($pStartDate, $pEndDate) {
			$oReportSmr = $this->WSProxy->GetLeadsReport(array('pStartDate' => $pStartDate, 'pEndDate' => $pEndDate));
			return $oReportSmr;
		}		
		
		//	Function to Set the login authentication on webServices.efundraising.com Web Service
		//	Return value : 	bool variable
		//					True, if the login authentication is Accepted
		//					False, if the login authentication is Rejected
		function InitWebServiceConnection($pUsername, $pPassword) {
			$this->WSNamespace = 'http://webservices.efundraising.com/eFundData.asmx?WSDL';
			$this->WSClient = new soapClient($this->WSNamespace,true);
			$this->WSProxy = $this->WSClient->getProxy();						
			$this->IsLoginIn = $this->WSProxy->Login(array('pUsername' => $pUsername, 'pPassword' => $pPassword));
			return $this->IsLoginIn;
		}
		
		//	Function to unload your call on http://webServices.efundraising.com Web Service
		//	Return value : 	bool variable
		//					True, if the logout is Accepted
		//					False, if the logout is Rejected
		function UnloadWebServiceConnection() {
			$IsLogout = $this->WSProxy->call('Logout');
			$this->WSProxy = null;
			return $IsLogout['LogoutResult'];
		}

		//	Function to get if the current user is Logged In on http://webServices.efundraising.com Web Service
		//	Return value : bool variable
		//					True, if the user is Logged In
		//					False, the current user is not authenticate
		function IsLoggedIn() {
			$oIsLoggedin = $this->WSProxy-call('IsLoggedIn');
			return $oIsLoggedIn;
		}					
	}
?>
