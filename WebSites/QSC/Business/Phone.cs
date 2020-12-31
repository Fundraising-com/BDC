using System;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.ComponentModel;
using DAL;

namespace Business
{
	///<summary>Summary description for Phone.</summary>
	public class Phone : QBusinessObject
	{
		#region Class Members
		protected int IDM=-1;
		[DAL.DataColumn("ID")]
		public int ID
		{
			get{ return this.IDM; }
			set{ this.IDM=value;  }
		}
		protected int TypeM;
		[DAL.DataColumn("Type")]
		public int Type
		{
			get{ return this.TypeM; }
			set{ this.TypeM=value;  }
		}
		protected int PhoneListIDM;
		[DAL.DataColumn("PhoneListID")]
		public int PhoneListID
		{
			get{ return this.PhoneListIDM; }
			set{ this.PhoneListIDM=value;  }
		}
		protected string PhoneNumberM;
		[DAL.DataColumn("PhoneNumber")]
		public string PhoneNumber
		{
			get{ return this.PhoneNumberM; }
			set{ this.PhoneNumberM=value;  }
		}
		protected string BestTimeToCallM;
		[DAL.DataColumn("BestTimeToCall")]
		public string BestTimeToCall
		{
			get{ return this.BestTimeToCallM; }
			set{ this.BestTimeToCallM=value;  }
		}
		#endregion

		#region Constructors
		protected DAL.PhoneDataAccess aTable;
		///<summary>default constructor</summary>
		public Phone()
		{
		}
		#endregion

		#region ValidateAndSave
		///<summary>check it then submit it</summary>
		override public bool ValidateAndSave()
		{
			if(this.Validate() == true)
			{
				return this.Save();
			}
			else
			{
				return false;
			}
		}


		///<summary>See if it exists</summary>
		public bool Exists(int nType, int nPhoneListID)
		{
			bool bExists = false;
			DataTable a = aTable.Exists(nType, nPhoneListID);

			if(a.Rows.Count > 0)
			{
				DataRow dr = a.Rows[0];
				Fill(dr,this.GetType());
				bExists = true;
			}

			return bExists;
		}


		///<summary>Check for compliance with biz rules</summary>
		public bool Validate()
		{
			/* setup variables to track validation */
			bool blValid = true;

			return blValid;
		}


		///<summary>Save a Phone # to the db</summary>
		///<returns>bool: Was saving successful ? </returns>
		public bool Save()
		{
			bool blSave = false;

			if(IDM == -1)
			{
				blSave = aTable.Insert(TypeM, PhoneListIDM, PhoneNumberM, BestTimeToCallM, out IDM);
			}
			else
			{
				blSave = aTable.Update(IDM, TypeM, PhoneListIDM, PhoneNumberM, BestTimeToCallM);
			}

			return blSave;
		}
		#endregion
	}
}

