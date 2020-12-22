namespace EFundraisingCRMWeb.Components.User.AddressHygiene
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
			this["AddressValidatedInMultipleCountries"] = "Address validated in multiple countries.";
			this["NoCountrySet"] = "No country found by Country ID or no country set for the record.";
			this["InvalidCharacter"] = "The address contains at least one character that is not part of the character set supported by the engine.";
			this["UnhandledCountry"] = "The engine does not support the selected country.";
			this["UnableToIdentifyLocalityRegionAndOrPostcodeOnInput"] = "Unable to identify locality, region, and/or postcode information on input.";
			this["UnableToIdentifyLocalityAndInvalidPostcode"] = "Unable to identify locality and invalid postcode found.";
			this["UnableToIdentifyPostCodeAndInvalidLocality"] = "Unable to identify postcode. Invalid locality is preventing a possible address correction.";
			this["InvalidLocalityAndPostcode"] = "Invalid locality and postcode are preventing a possible address correction.";
			this["InvalidPostcodePreventingLocalitySelection"] = "Invalid postcode is preventing a locality selection.";
			this["UnableToIdentifyPrimaryAddressLine"] = "Locality, region, and postcode are valid. Unable to identify the primary address line.";
			this["UnableToMatchPrimaryNameToDirectory"] = "Locality, region, and postcode are valid. Unable to match primary name to directory.";
			this["PossiblePrimaryNameMatchesTooCloseToChooseOne"] = "Possible primary name matches are too close to choose one.";
			this["MissingPrimaryRange"] = "Address is valid through primary name. Primary range is missing on input.";
			this["InvalidPrimaryRange"] = "Address is valid through primary name. Primary range is not valid.";
			this["InvalidOrMissingPrimaryType"] = "An invalid or missing primary type is preventing a possible address match.";
			this["MissingPrimaryTypeAndDirectional"] = "A missing primary type and prefix/postfix (directional) is preventing a possible address match.";
			this["InvalidDirectional"] = "An invalid or missing prefix/postfix (directional) is preventing a possible address match.";
			this["InvalidOrMissingPostcode"] = "An invalid or missing postcode is preventing a possible address match.";
			this["InvalidLocality"] = "An invalid locality is preventing a possible address match.";
			this["PossibleAddressLineMatchesTooCloseToChooseOne"] = "Possible address-line matches are too close to choose one.";
			this["AddressConflictsWithPostcode"] = "Address conflicts with postcode and the same primary name has a different postcode.";
			this["InvalidOrMissingSecondaryAddressLine"] = "Locality, region, postcode and primary address line are valid. An invalid or missing secondary address line is preventing a possible secondary address line match.";
			this["PossibleSecondaryAddressLineMatchesTooCloseToChooseOne"] = "Possible secondary address line matches are too close to choose one.";
			this["Undeliverable"] = "The address is valid, but the postal authority classified this address as undeliverable.";
			this["AddressNotInSpecifiedCountry"] = "The address does not reside in the specified country.";
			this["InputRecordEntirelyBlank"] = "The entire input record was blank.";
			this["UnclassifiedError"] = "Unclassified error.";

			//Suggestion list codes
			this["InvalidSelection"] = "Invalid Selection";
			this["InvalidSuggestionEntry"] = "Invalid Suggestion Entry";
			this["InvalidSuggestionRange"] = "Invalid Suggestion Range";
	    	this["InvalidSuggestionSecondaryRange"] = "Invalid Suggestion Secondary Range";
    	}
	}
}
