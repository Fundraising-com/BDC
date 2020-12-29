using System;

namespace GA.BDC.Core.ESubsGlobal
{
	/// <summary>
	/// Summary description for QSPSchool.
	/// </summary>
	public class QSPSchool
	{
		
		#region Fields

        int schoolId = int.MinValue;
		string name;
		string city;
		string state;
		string zip;
        int accountId = int.MinValue;

		#endregion

		public QSPSchool()
		{
			
		}

		#region Data Source Methods
		public static QSPSchool[] GetQSPSchools(string schoolName, string state, int languageId) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetQSPSchools(schoolName, state, languageId);
		}

		public static bool GetValidateAccountId(int fulfAccountId, int businessDivision) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetValidateAccountId(fulfAccountId, businessDivision);
		}

		public static int GetQspSupporterIdByEmail(string email)
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetQspSupporterIdByEmail(email);
		}

		public static string GetSchoolAccountIdByFulfId(int fulfId)
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetSchoolAccountIdByFulfId(fulfId).ToString();
		}

		public static string GetSchoolFulfIdById(int accountId)
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetSchoolFulfIdById(accountId).ToString();
		}


		#endregion

		#region Properties

		public string City
		{
			get { return this.city; }
			set { this.city = value; }
		}

		public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}

		public int SchoolId
		{
			get { return this.schoolId; }
			set { this.schoolId = value; }
		}

		public string State
		{
			get { return this.state; }
			set { this.state = value; }
		}

		public string Zip
		{
			get { return this.zip; }
			set { this.zip = value; }
		}

		public string CityAndState
		{
			get { return this.City + ", " + this.State; }
		}

		public int AccountId
		{
			get { return this.accountId; }
			set { this.accountId = value; }
		}

		#endregion
	}
}
