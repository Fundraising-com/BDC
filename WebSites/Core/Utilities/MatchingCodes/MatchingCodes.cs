using System;
using GA.BDC.Core.Diagnostics;
using System.Text;
using System.Diagnostics;
using Microsoft.VisualBasic;


namespace GA.BDC.Core.Utilities.MatchingCodes
{
	/// <summary>
	/// Summary description for MatchingCodes.
	/// This class builds an address code from an an an address to be able to compare 
	/// 2 addresses with less chance of error
	/// </summary>
	public class MatchingCodes
	{
		public MatchingCodes()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		
		//This method will return the matching code of the address
		public static string GetMatchingCode(string streetAddress, string zip)
		{
			string matchingCode = "invalid";
			try
			{
				if (streetAddress != "" && zip != "")
				{
					matchingCode = GenerateMatchingCode(streetAddress, zip);
				}
			}
			catch(Exception ex)
			{
				Logger.LogError("Error in GetMatchingCode", ex);
			}
			
			return matchingCode;
		}


		//This method will build the matching code with a simple logic by removing unwanted characters and words 
		//and taking the first charaters of the address and the zip code
		private static string GenerateMatchingCode(string streetAddress, string zip)
		{	
			string matchingCode = "";
			
			try
			{
				streetAddress = streetAddress.Trim().ToLower();
				zip = zip.Trim();
			
				bool valid = true;
				string addressNumberCode = "";  //99
				string streetNameCode = "";     //aa
				string zipCode = "";            //zzzzz
				StringBuilder addressNumber = new StringBuilder("");
				StringBuilder streetName = new StringBuilder("");

				//check if address has po box
				if (!(IsPOBox(streetAddress)))
				{
					//put address in a Array
					char [] charArray = streetAddress.ToCharArray(0,streetAddress.Length);
					
					//Separate the number from the address
					//go through address one character at a time 
					bool numberEnd = false;
					bool numberStart = false;
					for (int i = 0; i < streetAddress.Length;i++)
					{
						//check if digit, only interested in numbers until letters begin
						//dont want any letters before the first digit, an address must start with a number
						if (Information.IsNumeric(charArray[i]) && !numberEnd)
						{
							addressNumber.Append(charArray[i]);
							numberStart = true;
						}
						else
						{  
							//check if we started with letters, otherwise the letters are discarded
							if (numberStart)
							{
								numberEnd = true;
								streetName.Append(charArray[i]); 
							}
						
						}
					}
				}
				else
				{
					valid = false;
				}

				//Only the 2 first digit of the address numbers are taken
				if (valid)
				{
					addressNumberCode = CreateAddressNumberCode(addressNumber, ref valid);
				}

				//takes 2 first char of address
				if (valid) 
				{
					streetNameCode = CreateStreetNameCode(streetName, ref valid);
				}
               
				//takes first 5 digits of address
				if (valid)
				{
					zipCode = CreateZipCode(zip, ref valid);
				}
				
				//add up matching code
				if (valid)
				{
					matchingCode = zipCode + streetNameCode + addressNumberCode;
				}
				else
				{
					matchingCode = "invalid";
				}
	
			}
			catch(Exception ex)
			{
				Logger.LogError("Error in GenerateMatchingCode. Address is:" + streetAddress + ". Zip:" + zip, ex);

			}

			return matchingCode;

		}


		//this method takes out any charcater or word than can cause problems in the address code
		private static StringBuilder ReplaceUselessCharacters(StringBuilder streetName)
		{
			try
			{
				streetName.Replace(".","");
				streetName.Replace(",","");
				streetName.Replace("-","");
				streetName.Replace("/","");
				streetName.Replace("\\","");
				streetName.Replace("'","");
			
				streetName.Replace(" north ","");
				streetName.Replace(" south ","");
				streetName.Replace(" east ","");
				streetName.Replace(" west ","");
				streetName.Replace(" n ","");
				streetName.Replace(" s ","");
				streetName.Replace(" e ","");
				streetName.Replace(" w ","");
				streetName.Replace(" sth ","");
				streetName.Replace(" nrth ","");
				
				streetName.Replace(" se ","");
				streetName.Replace(" sw ","");
				streetName.Replace(" ne ","");
				streetName.Replace(" nw ","");
				streetName.Replace(" southeast ","");
				streetName.Replace(" southwest ","");
				streetName.Replace(" notheast ","");
				streetName.Replace(" northwest ","");
			
				//Highway and route is an exception since its not at the end of the address like road, street etc
				streetName.Replace(" highway ","");
				streetName.Replace(" hway ","");
				streetName.Replace(" hwy ","");
				streetName.Replace(" route ","");
				streetName.Replace(" rte ","");

			}
			catch(Exception ex)
			{
				Logger.LogError("Error in ReplaceUselessCharaters. StreetName = " + streetName, ex);
			}
			return streetName;

		}

		//Method generates part of the address code, the address number code
		private static string CreateAddressNumberCode(StringBuilder addressNumber, ref bool valid)
		{
			string addressNumberCode = "";

			try
			{
				//remove the #
				addressNumber.Replace("#","");
			
				if (addressNumber.Length == 1)
				{
					addressNumberCode = "0" + addressNumber[0];
				}
				else if (addressNumber.Length > 1)
				{
					addressNumberCode = addressNumber[0].ToString() + addressNumber[1].ToString();
				}
				else
				{
					valid = false;
				}
			}
			catch(Exception ex)
			{
				Logger.LogError("Error in CreateAddressNumberCode. AddressNumber = " + addressNumber, ex);
			}

			return addressNumberCode;

		}

		//Method generates part of the address code, the street name code
		private static string CreateStreetNameCode(StringBuilder streetName, ref bool valid)
		{
			string streetNameCode = "";

			try
			{
				//Replace useless charcaters
				streetName = ReplaceUselessCharacters(streetName);
				//get rid of any spaces
				streetName.Replace(" ","");
 		
			
				//Only take 2 first characters of street
				if (streetName.Length == 1)
				{
					streetNameCode = "0" + streetName[0].ToString();
				}
				else if (streetName.Length > 1)
				{
					streetNameCode = streetName[0].ToString() + streetName[1].ToString();
				}
				else
				{
					valid = false;
				}
				
			}
			catch(Exception ex)
			{
				Logger.LogError("Error in CreateStreetNameCode. StreetName = " + streetName, ex);
			}

			return streetNameCode;
		}

		//Method generates part of the address code, the zip address code
		private static string CreateZipCode(string zip, ref bool valid)
		{
			string zipCode = "";

			try
			{
				//remove spaces
				zip = zip.Replace(" ","");
				if (zip.Length >= 5)
				{
					//take first 5 digits
					zipCode = zip.Substring(0,5) ;
				}
				else
				{
					valid = false;
				}
				
			}
			catch(Exception ex)
			{
				Logger.LogError("Error in CreateZiprCode. Zip = " + zip, ex);
			}

			return zipCode;

		}

		//check if address is a po box
		private static bool IsPOBox(string streetAddress)
		{
			bool isPOBox = false;
			
			try
			{
				//IF the address starts with a number, the address is good even though there might be a po box after
				//good: 227 De Soto street. P.O. Box 511
				//bad:  Rte 2 Box 255
				//bad:  PO Box 225

				if (!(Information.IsNumeric(streetAddress.Substring(0,1))))
				{
					int pos = streetAddress.ToLower().IndexOf(" box ");
					if (pos > -1)
					{
						isPOBox = true;
					}
				}
			}
			catch(Exception ex )
			{

			}

			return isPOBox;
		}



		/*
		 * CASES TO CONSIDER THAT MAY NOT WORK
				
		-101-610 ... (101 etant l'appartement)
		-333A - 4th Avenue N5 ou 33-F --> le A entrer dans le nom de la rue
		-APP# (si j'ai un building de 350 appart, ils ont tous le meme matching code...)
		
		-po Box 98 / 2577 Route 11 North
		-320 East Third Street matchera pas avec 320 East 3rd Street
		-So. pour south ETC
		-addresses en francais
		 ** 915 West Hwy 199 --> address code va etre 1991, donc seulement des chiffre, un peu moins sure que des lettres !!!
		*/
	}
}
