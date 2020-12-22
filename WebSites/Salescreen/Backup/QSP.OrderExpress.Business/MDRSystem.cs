namespace QSPForm.Business
{
	using System;
	using System.Data;
	using QSPForm.Common.DataDef;
	using QSPForm.Data;
	using tableRef = QSPForm.Common.DataDef.CMDRTable;

	///<summary>This class contains the business rules that are used for a MDR organization entry</summary>
	public class MDRSystem : BusinessSystem
	{

		//There is no place for validation
		//cause we don't expect to do insert, update, delete, only select...
		QSPForm.Data.CMDRSchool objDataAccess = new QSPForm.Data.CMDRSchool();


		public MDRSystem()
		{

		}



		
		public tableRef SelectAllBySchoolName(string schoolName)
		{
			//
			// Get the MDR school DataTable from the DataLayer
			//
			return objDataAccess.SelectAllWSchoolNameLogic(schoolName);
		}

		public tableRef SelectOne(string PID)
		{
			//
			// Get the MDR school DataTable from the DataLayer
			//
			return objDataAccess.SelectOne(PID);
		}

		public tableRef SelectAll()
		{
			//
			// Get the MDR school DataTable from the DataLayer
			//
			return objDataAccess.SelectAll();
		}

		public tableRef SelectAll_Search(int SearchType, string Criteria, int OrgTypeID, string StateCode)
		{
			tableRef dTbl;
			//
			// Get the MDR school DataTable from the DataLayer
			//
			dTbl = objDataAccess.SelectAll_Search(SearchType, Criteria, OrgTypeID, StateCode);

			return dTbl;
		}

	}
}
