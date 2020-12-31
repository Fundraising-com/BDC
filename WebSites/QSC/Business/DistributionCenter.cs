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
	/// Summary description for DistributionCenter.
	/// </summary>
	public class DistributionCenter : QBusinessObject
	{

		protected int IDM=-1;
		[DAL.DataColumn("Account_ID")]
		public int Account_ID
		{
			get{ return this.IDM; }
			set{ this.IDM=value;  }
		}

		protected string NameM;
		[DAL.DataColumn("Name")]
		public string Name
		{
			get{ return this.NameM; }
			set{ this.NameM=value;  }
		}

		protected string DescriptionM;
		[DAL.DataColumn("Description")]
		public string Description
		{
			get{ return this.DescriptionM; }
			set{ this.DescriptionM=value;  }
		}

		public DistributionCenter()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public DataTable GetAllDistributionCenters()
		{
			DAL.DistributionCenterDataAccess oDC = new DistributionCenterDataAccess();
			return oDC.GetDistributionCenters();
		}

		public DataTable GetDistributionCenter(int Id)
		{
			DAL.DistributionCenterDataAccess oDC = new DistributionCenterDataAccess();
			return oDC.GetDistributionCenter(Id);
		}

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
			return true;
		}

		public bool Save()
		{
			return true;
		}

		private string	ErrorGUIM;
		///<summary>Gets or sets error string associatated with user interface level validation</summary>
		public string	ErrorGUI	{ get{ return this.ErrorGUIM; } set{this.ErrorGUIM = value; } }
		
		private string	ErrorBIM;
		///<summary>Gets or sets error string associatated with biz intelligence level validation</summary>
		public string	ErrorBI		{ get{ return this.ErrorBIM;  } set{this.ErrorBIM  = value; } }


	}
}
