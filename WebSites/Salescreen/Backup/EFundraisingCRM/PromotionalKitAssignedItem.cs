using System;

namespace efundraising.EFundraisingCRM
{
	/// <summary>
	/// Summary description for PromotionalKitAssignedItem.
	/// </summary>
	public class PromotionalKitAssignedItem:EFundraisingCRMDataObject
	{
		private PromotionalKit promoKit = null;
		private KitType kitType = null;
		private PostalAddress address = null;

		public PromotionalKitAssignedItem(int promoKitID)
		{
			promoKit = PromotionalKit.GetPromotionalKitByID(promoKitID);
			kitType = KitType.GetKitTypeByID(promoKit.KitTypeId);
			address = PostalAddress.GetPostalAddressByID(promoKit.PostalAddressId);
		}
		public string PromotionalKitID
		{
			get{ return promoKit.PromotionalKitId.ToString();}
		}
		public string TypeOfKit
		{
			get { return kitType.Description;}
		}
		public string CreateDate
		{
			get {return promoKit.CreateDate.ToString();}
		}
		public string DateSent
		{
			get
			{
				if(promoKit.SentDate == DateTime.MinValue)
				{
					return "Not Sent" as string;
				}
				else
				{
					return promoKit.SentDate.ToString();
				}
			}
		}
		
		public PromotionalKit PromoKit
		{
			get{return promoKit;}
			set{promoKit = value;}
		}
		public KitType KType
		{
			get { return kitType;}
			set { kitType = value;}
		}
		public bool isEditable
		{
			get{ return promoKit.SentDate == DateTime.MinValue || promoKit.Validated != 1;}
		}

		public string Address
		{
			get{ 
				if(address.Address == null)
					return string.Empty;
				else
					return address.Address.Trim(); 
			}
		}

		public string City
		{
			get
			{
				if(address.Address == null)
					 return string.Empty;
				else
					return address.City.Trim(); 
			}
		}

		public string State
		{
			get{ if (address != null && address.SubdivisionCode != null) 
				return address.SubdivisionCode.Trim().Substring(address.SubdivisionCode.Trim().Length - 2, 2);
				else return string.Empty;
			}
		}

		public string ZipCode
		{
			get{
				
				if(address.Address == null)
					return string.Empty;
				else
					return address.ZipCode.Trim(); }
		}

		public string Country
		{
			#region Hack 
			// to get rid of "1s" for countries
			get
			{ 
				if(address.CountryCode.Trim().StartsWith("1".ToString()))
					return "US".ToString();
				else
				{
					if(address.Address == null)
						return string.Empty;
					else
						return address.CountryCode.Trim(); 
				}
			}
			#endregion
		}
	}
}
