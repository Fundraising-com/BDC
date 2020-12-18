namespace efundraising.RecaudarFondosWeb.Components.User.AddressHygiene
{
	using System;
	using System.Collections;
	
	/// <summary>
	/// Hashtable of the different Address Hygiene ErrorCodes.
	/// </summary>
	public class AddressHygieneErrorCodes : Hashtable
	{
		public AddressHygieneErrorCodes()
		{
			//Fault Codes
			this["LastLineBadOrMissing"] = "Last line is bad or missing";
			this["NoLocalityBadPostCode"] = "No locality and bad postcode";
			this["BadLocalityNoPostCode"] = "Bad locality and no postcode";
			this["BadLocalityBadPostCode"] = "Bad locality and bad postcode";
			this["BadPostCodeCannotDetermineCity"] = "Bad postcode, can't determine city match to select";
			this["NoPrimaryAddressLineParsed"] = "No primary address line parsed";
			this["StreetNameNotFound"] = "Street name not found in directory";
			this["PossibleStreetNameMatchesTooCloseToChoose"] = "Possible street name matches too close to choose";
			this["PrimaryRangeMissing"] = "Primary range is missing";
			this["PrimaryRangeInvalidForStreetRouteBuilding"] = "Primary range is invalid for street/route/building";
			this["PredirectionalNeededInputWrongOrMissing"] = "Predirectional needed, input is wrong or missing";
			this["SuffixNeededInputWrongOrMissing"] = "Suffix needed, input is wrong or missing";
			this["SuffixAndDirectionalNeededInputWrongOrMissing"] = "Suffix & directional needed, input wrong or missing";
			this["PostdirectionalNeededInputWrongOrMissing"] = "Postdirectional needed, input is wrong or missing";
			this["BadPostCodeCannotDetermineAddressMatch"] = "Bad postcode, can't select an address match";
			this["BadLocalityCannotDetermineAddressMatch"] = "Bad locality, can't select an address match";
			this["PossibleAddressLineMatchesTooCloseToChoose"] = "Possible addr line matches too close to choose one";
			this["UrbanizationNeededInputWrongOrMissing"] = "Urbanization needed, input is wrong or missing";
			this["AddressConflictsWithPostCodeForSameStreet"] = "Address conflicts with postcode for same street";
			this["AddressConflictsWithPostcodeForDifferentStreet"] = "Address conflicts with postcode for diff. street";
			this["NoStreetAssignmentDuplicatePostcodeMatch"] = "No street assignment, duplicate postcode match";
			this["NoStreetAssignmentNoPostCode"] = "No street assignment, no postcode";
			this["NoStreetAssignmentPostcodeNotMatched"] = "No street assignment, postcode not matched";
			this["MultipleMatchesInDifferentDirectoryAreas"] = "Multiple matches in different directory areas";
			this["DeliveryPointIDNotAssignedSecondaryInfoMissing"] = "Delivery Pt ID not assigned, secondary info missing";
			this["HighRankHasOutOfRangeSuiteNumber"] = "High rank has out-of-range suite number";
			this["HighRankHasOutOfRangePOBoxNumber"] = "High rank has out-of-range PO Box number";
			this["POBoxVsCivicPrimaryNamePostalCodeConflict"] = "PO Box vs. civic primary name postal code conflict";
			this["MultipleSuiteRankingConflict"] = "Multiple suite ranking conflict";
			this["OtherError"] = "Other Error";
			this["Foreign"] = "Foreign";
			this["InputRecordEntirelyBlank"] = "Input record entirely blank";
			this["ZIPNotInAreaCoveredByPartialZIP4Directory"] = "ZIP not in area covered by partial ZIP+4 Directory";
			this["OverlappingRangesInZIP4Directory"] = "Overlapping ranges in ZIP+4 directory";
			this["Undeliverable"] = "Marked by USPS as unsuitable for delivery of mail";

			//Suggestion list codes
			this["InvalidPostCode"] = "Invalid postcode";
	    	this["InvalidStreet"] = "Invalid street";
	    	this["InvalidTown"] = "Invalid town";
			this["MoreInformationNeeded"] = "StreetInformationNeeded";
	    	this["NoInputGiven"] = "No input given";
			this["PostCodeNumeric"] = "Postcode numeric";
	    	this["TownNeeded"] = "Town needed";   
			this["TownOrPostCodeNeeded"] = "Town or postcode needed";
	    	this["NoStreetInformationAvailable"] = "No street information available";
			this["CountryBlank"] = "Country blank";
			this["InvalidCountry"] = "Invalid country";
			this["NoResults"] = "No results";	    
			this["AddressNeeded"] = "Address needed";    
			this["PremiseNeeded"] = "Premise needed";
			this["FirmNeeded"] = "Firm needed";
    	}
	}
}
