<?php
	//-----------------------------------------------------------------------------------------------------
	//	Author		:	Louis Turmel		(louis.turmel@readersdigest.com)
	//      Supervisor By	: 	Stephen Lim 		(slim@rd.com)
	//	Campagny Name	:	efundraising.com - efundraising.com
	//	Project 	: 	Web Services Implementation 	(http://webservices.efundraising.com)
	//
	//	Last Update	:	July 27, 2005
	//	Release Date	:	May 2, 2005
	//	Version 	:	2.0.0
	//------------------------------------------------------------------------------------------------------
	
	@set_time_limit(0);
	@ini_set("memory_limit",-1);

	include("nusoap.php");		

	// class to implement easily the Web Service inside the PHP Server Side Code
	class eFundDataClient {
		
		var $WSNamespace;
		var $WSClient;
		var $WSProxy;
		var $IsLoginIn = false;
		
		//	Function adding a new Lead entry on http://efundraising.com Web Site
		//	Return value : None.	
		function AddNewLead($pFirstName, $pLastName, $pEmail, $pAddress, $pCity,
			$pState, $pZip, $pCountry, $pDayPhone, $pEveningPhone, $pGroupSize,
			$pOrganizationName, $pPromotionID, $pTitle, $pEveningPhoneExt, $pDayPhoneExt,
			$pBestTimeToCall, $pOrganizationTypeID, $pGroupTypeID, $pFundraisingDate,
			$pDecisionMaker, $pProductsInterestIn, $pOnEmailList, $pComments) {
			
			$this->WSProxy->AddNewLead(array('pFirstName' => $pFirstName, 'pLastName' => $pLastName, 
				'pEmail' => $pEmail, 'pAddress' => $pAddress, 'pCity' => $pCity,
                        	'pState' => $pState, 'pZip' => $pZip, 'pCountry' => $pCountry, 
				'pDayPhone' => $pDayPhone, 'pEveningPhone' => $pEveningPhone, 
				'pGroupSize' => $pGroupSize, 'pOrganizationName' => $pOrganizationName, 
				'pPromotionID' => $pPromotionID, 'pTitle' => $pTitle, 
				'pEveningPhoneExt' => $pEveningPhoneExt, 'pDayPhoneExt' => $pDayPhoneExt,
                       	 	'pBestTimeToCall' => $pBestTimeToCall, 'pOrganizationTypeID' => $pOrganizationTypeID, 
				'pGroupTypeID' => $pGroupTypeID, 'pFundraisingDate' => $pFundraisingDate,
                        	'pDecisionMaker' => $pDecisionMaker, 'pProductsInterestIn' => $pProductsInterestIn, 
				'pOnEmailList' => $pOnEmailList, 'pComments' => $pComments));
		}

		// 	Function to get the Sell Report between two Dates from efundraising
		//	Return value: Fundraiser variable
		function GetSalesReport($pStartDate, $pEndDate) {
			$oReportSmr = $this->WSProxy->GetSalesReport(array('pStartDate' => $pStartDate, 'pEndDate' => $pEndDate));
			return $oReportSmr;
		}

		//	Function to get the Leads list of Partner generated between two dates
		//	Return value : Fundraiser variable
		function GetLeadsReport($pStartDate, $pEndDate) {
			$oReportLds = $this->WSProxy->GetLeadsReport(array('pStartDate' => $pStartDate, 'pEndDate' => $pEndDate));
			return $oReportLds;
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
			$IsLogout = $this->WSProxy->Logout(array());
			$this->WSProxy = null;
			return $IsLogout['LogoutResult'];
		}

		//	Function to get if the current user is Logged In on http://webServices.efundraising.com Web Service
		//	Return value : bool variable
		//					True, if the user is Logged In
		//					False, the current user is not authenticate
		function IsLoggedIn() {
			$oIsLoggedIn = $this->WSProxy->IsLoggedIn(array());
			return $oIsLoggedIn['IsLoggedInResult'];
		}

		//	Function returning the last error encounted on http://webServices.efundraising.com Web Service
		//	Return value : Error variable
		function GetLastError() {
			$oLastError = $this->WSProxy->GetLastError(array());
			return $oLastError['GetLastErrorResult'];
		}				
	}
?>
