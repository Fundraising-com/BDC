using System;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.ComponentModel;
using DAL;

namespace Business
{
	///<summary>
	///	Business Representation of a QSP Campaign Program 
	///</summary>
	public class Program  : QBusinessObject
	{
		#region Class Members
		protected int CampaignIDM = -5;
		///<summary>int: Campaign #</summary>
		public int CampaignID
		{
			get { return this.CampaignIDM;  }
			set { this.CampaignIDM = value; }
		}

		protected int ProgramIDM = -5;
		///<summary>int: Program #</summary>
		public int ProgramID
		{
			get { return this.ProgramIDM;  }
			set { this.ProgramIDM = value; }
		}

		protected bool ProgramChoiceM;
		private bool ProgramChoiceM_Value_Set = false;
		///<summary>bool: is this combo being saved (true), or deleted (false)</summary>
		public bool ProgramChoice
		{
			get { return this.ProgramChoiceM;  }
			set { this.ProgramChoiceM = value; this.ProgramChoiceM_Value_Set = true; }
		}

		protected bool IsPreCollectM;
		private bool IsPreCollectM_Value_Set = false;
		///<summary>bool: Pre Collect, yes or no</summary>
		public bool IsPreCollect
		{
			get { return this.IsPreCollectM;  }
			set { this.IsPreCollectM = value; this.IsPreCollectM_Value_Set = true; }
		}

		protected double AccountProfitM = 0.0;
		///<summary>double: Account Profit Level</summary>
		public double AccountProfit
		{
			get { return this.AccountProfitM;  }
			set { this.AccountProfitM = value; }
		}

		private System.Collections.ArrayList CatalogsM = new System.Collections.ArrayList(5);
		public void AddCatalog(string CCode, bool Selected)
		{
			Business.Content_Catalog CAT = new Business.Content_Catalog();
			CAT.CCode    = CCode;
			CAT.Selected = Selected;
			CatalogsM.Add(CAT);
		}

		public int CatalogsCount 
		{
			get { return this.CatalogsM.Count;	}
		}
		#endregion

		#region Constructors
		protected DAL.CampaignProgramDataAccess aTable = new DAL.CampaignProgramDataAccess();
		///<summary>default constructor</summary>
		public Program()
		{
			this.UserIDModifiedM = -5555;
		}
		#endregion

		#region ValidateAndSave
		///<summary>Check the Campaign/Program/Brochure for compliance with biz rules, then save it</summary>
		///<returns>bool: did both pieces work ?</returns>
		public bool ValidateAndSave(ref int ReturnCode)
		{
			int Valcode = 1;
			if(this.ValidateProgram(ref Valcode) == true)
			{
				return this.Save(ref ReturnCode);
			}
			else
			{
				ReturnCode = Valcode;
				return false;
			}
		}

		///<summary>the ValidateAndSave required for the compiler</summary>
		///<remarks>disregards the return code int</remarks>
		///<returns>bool: did both pieces work ?</returns>
		override public bool ValidateAndSave()
		{
			int ThrowAway = -2;
			return ValidateAndSave(ref ThrowAway);
		}
//		override public bool ValidateAndSave()
//		{
//			throw new Exception("dont use this method");
//		}


		///<summary>Check all member variables are set properly</summary>
		///<remarks>
		///	What biz rules can be set here?
		///	This area is Program Only.
		///	Bix rules related to Campaigns and programs are in CampaignProgram.cs
		///</remarks>
		///<returns>bool: did it validate?</returns>
		public bool ValidateProgram(ref int ReturnCode)
		{
			if (this.CampaignIDM == -5)
			{
				ReturnCode = -10; return false;
			}
			if(this.ProgramChoiceM_Value_Set == false)
			{
				ReturnCode = -11; return false;
			}
			if(this.IsPreCollectM_Value_Set == false)
			{
				ReturnCode = -12; return false;
			}
			if(this.ProgramIDM == -5)
			{
				ReturnCode = -13; return false;
			}
			if(this.UserIDModifiedM < -5)
			{
				ReturnCode = -14; return false;
			}

			//if we got to here.....
			ReturnCode = 0; return true;
		}

		private bool Save(ref int ReturnCode)
		{
			bool blResult = SaveProgram(ref ReturnCode);
			if(blResult == false) { return false; } 

			for (int i = 0; i < this.CatalogsCount; i++)
			{
				blResult = SaveBrochure(ref ReturnCode, ((Business.Content_Catalog) this.CatalogsM[i]) );
				if(blResult == false){ return false; }
			}
			return true;
		}



		protected bool SaveProgram(ref int ReturnCode)
		{
			bool DALReturnCode = aTable.SaveCampaignProgram(
				this.CampaignIDM,
				this.ProgramIDM,
				this.IsPreCollectM,
				this.AccountProfit,
				this.ProgramChoiceM,
				this.UserIDModifiedM);

			if(DALReturnCode == true) { ReturnCode = 0; return true; } 
			else { ReturnCode = -15; return false; } 
		}

		protected bool SaveBrochure(ref int ReturnCode, Business.Content_Catalog CAT)
		{
			bool DALReturnCode = aTable.SaveCampaignToContentCatalog(
				this.CampaignIDM,
				this.ProgramIDM,
				CAT.CCode,
				CAT.Selected,
				this.UserIDModifiedM);

			if(DALReturnCode == true) { ReturnCode = 0; return true; } 
			else { ReturnCode = -16; return false; } 
		}



		#endregion ValidateAndSave
	}

	public class Content_Catalog
	{
		public string CCode;
		//public String Description;
		public bool Selected;
	}
}


