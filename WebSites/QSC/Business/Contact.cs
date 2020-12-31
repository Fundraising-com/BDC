using System;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.ComponentModel;
using DAL;

namespace Business
{
	/// <summary>
	/// Summary description for Contact.
	/// </summary>
	public class Contact : QBusinessObject
	{
		#region Class Members

		private DAL.ContactDataAccess aTable;

		protected int ContactIDM = -1;
		[DAL.DataColumn("ContactID")]
		public int ContactID
		{
			get{ return this.ContactIDM; }
			set{ this.ContactIDM = value;  }
		}
		protected int ContactListIDM;
		[DAL.DataColumn("ContactListID")]
		public int ContactListID
		{
			get{ return this.ContactListIDM; }
			set{ this.ContactListIDM=value;  }
		}
		protected int CAccountIDM;
		[DAL.DataColumn("CAccountID")]
		public int CAccountID
		{
			get{ return this.CAccountIDM; }
			set{ this.CAccountIDM=value;  }
		}
		protected string TitleM;
		[DAL.DataColumn("Title")]
		public string Title
		{
			get{ return this.TitleM; }
			set{ this.TitleM=value;  }
		}
		protected string FirstNameM;
		[DAL.DataColumn("FirstName")]
		public string FirstName
		{
			get{ return this.FirstNameM; }
			set{ this.FirstNameM=value;  }
		}
		protected string LastNameM;
		[DAL.DataColumn("LastName")]
		public string LastName
		{
			get{ return this.LastNameM; }
			set{ this.LastNameM=value;  }
		}
		protected string MiddleInitialM;
		[DAL.DataColumn("MiddleInitial")]
		public string MiddleInitial
		{
			get{ return this.MiddleInitialM; }
			set{ this.MiddleInitialM=value;  }
		}
//		protected int TypeIdM;
//		[DAL.DataColumn("TypeId")]
//		public int TypeId
//		{
//			get{ return this.TypeIdM; }
//			set{ this.TypeIdM=value;  }
//		}
		protected string FunctionM;
		[DAL.DataColumn("Function")]
		public string Function
		{
			get{ return this.FunctionM; }
			set{ this.FunctionM=value;  }
		}
		protected string EmailM;
		[DAL.DataColumn("Email")]
		public string Email
		{
			get{ return this.EmailM; }
			set{ this.EmailM=value;  }
		}
		protected int AddressIDM;
		[DAL.DataColumn("AddressID")]
		public int AddressID
		{
			get{ return this.AddressIDM; }
			set{ this.AddressIDM=value;  }
		}
		protected int PhoneListIDM;
		[DAL.DataColumn("PhoneListID")]
		public int PhoneListID
		{
			get{ return this.PhoneListIDM; }
			set{ this.PhoneListIDM=value;  }
		}
		protected bool DeletedTFM;
		[DAL.DataColumn("DeletedTF")]
		public bool DeletedTF
		{
			get{ return this.DeletedTFM; }
			set{ this.DeletedTFM=value;  }
		}

		private bool	ValidGUIM;
		///<summary>Gets or sets value indicating if the CA has passsed user interface level validation</summary>
		public bool		ValidGUI	{ get{ return this.ValidGUIM; } set{this.ValidGUIM = value; } }

		private bool	ValidBIM;
		///<summary>Gets or sets value indicating if the CA has passsed biz intelligence level validation</summary>
		public bool		ValidBI		{ get{ return this.ValidBIM;  } set{this.ValidBIM  = value; } }

		private string	ErrorGUIM;
		///<summary>Gets or sets error string associatated with user interface level validation</summary>
		public string	ErrorGUI	{ get{ return this.ErrorGUIM; } set{this.ErrorGUIM = value; } }

		private string	ErrorBIM;
		///<summary>Gets or sets error string associatated with biz intelligence level validation</summary>
		public string	ErrorBI		{ get{ return this.ErrorBIM;  } set{this.ErrorBIM  = value; } }
		#endregion  Class Members

		#region Constructors
		public Contact()
		{
			aTable = new DAL.ContactDataAccess();
			this.PopulateWithNewInfo();
		}
		#endregion Constructors

		#region ValidateAndSave
		///<summary>check it then submit it</summary>
		override public bool ValidateAndSave()
		{
			if(this.Validate() == true)
			{
				this.ErrorBI = "this.Validate() returned true, lets try a save !<br>" + this.ErrorBI;
				return this.Save();
			}
			else
			{
				return false;
			}
		}
		///<summary>Check for compliance with biz rules</summary>
		public bool Validate()
		{
			/* setup variables to track validation */
			bool blValid = true;
			//string stError = "";

			return blValid;
		}
		///<summary>Save a Contact to the db</summary>
		///<returns>bool: Was saving successful ? </returns>
		public bool Save()
		{
			return ( Save_Contact() );
		}

		///<summary>Save the Contact to the db</summary>
		///<returns>bool: Was saving successful ? </returns>
		private bool Save_Contact()
		{
			bool blSave = false;
			//this.ErrorBI += "this.Save_Contact() ContactIDM (before):" + this.ContactIDM.ToString() + "\r\n";
			if(this.ContactIDM == -1)
			{
				//this.ErrorBI += "this.Save_Contact() in insert\r\n";
				blSave = aTable.Insert(
					this.ContactListIDM,	this.CAccountIDM,	this.TitleM,
					this.FirstNameM,		this.LastNameM,		this.MiddleInitialM,
					this.FunctionM,			this.EmailM, 
					out this.AddressIDM,out this.PhoneListIDM,	out this.ContactIDM);
			}
			else
			{
				//this.ErrorBI += "this.Save_Contact() in update\r\n";
				blSave = aTable.Update(
					this.ContactIDM,		
					this.ContactListIDM,	this.CAccountIDM,	this.TitleM,
					this.FirstNameM,		this.LastNameM,		this.MiddleInitialM,
					this.FunctionM,			this.EmailM);
			}
			//this.ErrorBI += "this.Save_Contact() IDM (after):" + ContactIDM.ToString() + "\r\n";
			//this.ErrorBI += "this.Save_Contact() PhoneListIDM:" + PhoneListIDM.ToString() + "\r\n";
			//this.ErrorBI += "this.Save_Contact() AddressIDM:" + AddressIDM.ToString() + "\r\n";
			//this.ErrorBI += "this.Save_Contact() blSave:" + blSave.ToString() + "\r\n";
			return blSave;
		}

		public bool DeleteContact()
		{
			return aTable.Delete(this.ContactID);
		}
		#endregion ValidateAndSave

		#region Populate Functions
		public void PopulateFromDB()
		{
			DataTable dt = aTable.GetContact(this.ContactID);
			string rowsStr = dt.Rows.Count.ToString();
			if(dt.Rows.Count < 1)
			{
				throw new ArgumentException("There is no data for the ContactID", this.ContactID.ToString());
			}
			else if(dt.Rows.Count > 1)
			{
				rowsStr += " rows of data were returned when 1 was expected";
				throw new ArgumentException(rowsStr, this.ContactID.ToString());
			}
			else
			{
				/* one row returned, good stuff */
				DataRow DR = dt.Rows[0];
				//Fill(DR, this.GetType());
				this.ContactID			= Convert.ToInt32(DR["ContactID"]);
				this.ContactListID		= Convert.ToInt32(DR["ContactListID"]);
				this.CAccountID			= Convert.ToInt32(DR["CAccountID"]);
				this.Title				= Convert.ToString(DR["Title"]);
				this.FirstName			= Convert.ToString(DR["FirstName"]);
				this.LastName			= Convert.ToString(DR["LastName"]);
				//this.TypeId			= Convert.ToInt32(DR["TypeId"]);
				this.Function			= Convert.ToString(DR["Function"]);
				this.Email				= Convert.ToString(DR["Email"]);
				this.AddressID			= Convert.ToInt32(DR["AddressID"]);
				this.PhoneListID		= Convert.ToInt32(DR["PhoneListID"]);
			}
		}

		public void PopulateWithNewInfo()
		{
			this.ContactID			= -1;
			this.ContactListID		= -1;
			this.CAccountID			= -1;
			this.Title				= "";
			this.FirstName			= "";
			this.LastName			= "";
			//this.TypeId			= -1;
			this.Function			= "";
			this.Email				= "";
			this.AddressID			= -1;
			this.PhoneListID		= -1;
		}
		#endregion Populate Functions
	}
}
