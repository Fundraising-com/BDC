namespace QSPForm.Business
{
	
	using System;
	using System.Data;
	using QSPForm.Common.DataDef;
	using QSPForm.Data;
	using dataDef = QSPForm.Common.DataDef.CUserTable;
		
	/// <summary>
	///     This class contains the business rules that are used for a 
	///     user.
	/// </summary>
	public class CUserSystem : BusinessSystem
	{		
		
		//There is no place for validation
		//cause we don't expect to do insert, update, delete, only select...
		QSPForm.Data.CUserProfile objDataAccess = new QSPForm.Data.CUserProfile();

		public CUserSystem()
		{
			
		}
		public dataDef SelectAllByUserName(String userName)
		{
			//
			// Get the user DataTable from the DataLayer
			//
							
			return objDataAccess.SelectAllWUserNameLogic(userName);
						
		}

		public dataDef SelectOne(int CUserID)
		{	
			//
			// Get the user DataTable from the DataLayer
			//
			
			return objDataAccess.SelectOne(CUserID);	
		}

		public dataDef SelectOne(string FMID)
		{	
			//
			// Get the user DataTable from the DataLayer
			//
			
			return objDataAccess.SelectOneWfm_idLogic(FMID);	
		}

		public dataDef SelectAllFM()
		{	
			//
			// Get the user DataTable from the DataLayer
			//
			
			return objDataAccess.SelectAllFM();	
		}
		
		public dataDef SelectAllFM_Search(int SearchType, String Criteria)
		{			
			dataDef dTbl;
			
			//
			// Get the user DataTable from the DataLayer
			//				
			dTbl = objDataAccess.SelectAllFM_Search(SearchType, Criteria);				
			
			return dTbl;			
		}
	
		public dataDef SelectByFMID(string sFMID)
		{	
			//
			// Get the user DataTable from the DataLayer
			//
			
			return objDataAccess.SelectByFMID(sFMID);	
		}
	}
}
