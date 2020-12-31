using System;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Summary description for OrderHeader.
	/// </summary>
	[Serializable]
	public class OrderHeader
	{
		private OrderHeaderCollection parentCollection = null;
		private ProductItemCollection productItems = null;
		private int collectionID = -1;
		private int customerOrderHeaderInstance = 0;
		private int accountID = 0;
		private int campaignID = 0;
		private string teacherFirstName = String.Empty;
		private string teacherLastName = String.Empty;
		private string studentFirstName = String.Empty;
		private string studentLastName = String.Empty;
		private int customerBillToInstance = 0;

		public OrderHeaderCollection ParentCollection 
		{
			get 
			{
				return parentCollection;
			}
			set 
			{
				parentCollection = value;
			}
		}

		public ProductItemCollection ProductItems 
		{
			get 
			{
				if(productItems == null) 
				{
					productItems = new ProductItemCollection();
					productItems.ParentOrderHeader = this;
				}

				return productItems;
			}
			set 
			{
				productItems = value;
				productItems.ParentOrderHeader = this;
			}
		}

		public int CollectionID 
		{
			get 
			{
				return collectionID;
			}
			set 
			{
				collectionID = value;
			}
		}

		public int CustomerOrderHeaderInstance 
		{
			get 
			{
				return customerOrderHeaderInstance;
			}
			set 
			{
				customerOrderHeaderInstance = value;
			}
		}

		public int AccountID 
		{
			get 
			{
				return accountID;
			}
			set 
			{
				accountID = value;
			}
		}

		public int CampaignID 
		{
			get 
			{
				return campaignID;
			}
			set 
			{
				campaignID = value;
			}
		}

		public int CustomerBillToInstance 
		{
			get 
			{
				return customerBillToInstance;
			}
			set 
			{
				customerBillToInstance = value;
			}
		}

		public string TeacherFirstName 
		{
			get 
			{
				return teacherFirstName;
			}
			set 
			{
				teacherFirstName = value;
			}
		}

		public string TeacherLastName 
		{
			get 
			{
				return teacherLastName;
			}
			set 
			{
				teacherLastName = value;
			}
		}

		public string StudentFirstName 
		{
			get 
			{
				return studentFirstName;
			}
			set 
			{
				studentFirstName = value;
			}
		}

		public string StudentLastName 
		{
			get
			{
				return studentLastName;
			}
			set 
			{
				studentLastName = value;
			}
		}

		public override bool Equals(object obj)
		{
			OrderHeader orderHeader = obj as OrderHeader;
			bool isEqual = false;
			
			if(orderHeader != null) 
			{
				isEqual = (CollectionID == orderHeader.CollectionID);
			}

			return isEqual;
		}

		public override int GetHashCode()
		{
			return CollectionID;
		}
	}
}
