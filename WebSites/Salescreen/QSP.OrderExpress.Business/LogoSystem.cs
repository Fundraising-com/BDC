using System;
using QSPForm.Common;
using System.Web;

namespace QSPForm.Business
{
	
	using System;
	using System.Data;
	using QSPForm.Common.DataDef;
	using QSPForm.Data;
	using dataDef = QSPForm.Common.DataDef.LogoTable;
	using dataAccessRef = QSPForm.Data.Logo;
	
	/// <summary>
	///     This class contains the business rules that are used for an 
	///     Product.
	/// </summary>

	public class LogoSystem : BusinessSystem
	{
		
		dataAccessRef objDataAccess;
		
		public LogoSystem()
		{
			objDataAccess = new dataAccessRef();
		}
		public bool Insert(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			return this.Insert(Table, objDataAccess);			
		}

		public bool Update(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			return this.Update(Table, objDataAccess);			
		}

		public bool UpdateBatch(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			return this.UpdateBatch(Table, objDataAccess);			
		}

		public bool Delete(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			return this.Delete(Table, objDataAccess);			
		}

		//----------------------------------------------------------------
		// Function Validate:
		//   Validates Product row
		// Returns:
		//   true if validation is successful 
		//   false if invalid fields exist 
		// Parameters:
		//   [in]  row: DataRow to be validated
		//   [out] row: Returns row data.  If there are fields
		//              that contain errors they are individually marked.  
		//----------------------------------------------------------------
		protected override bool Validate(DataRow row)
		{
			bool isValid = true;
            /*
			//Clear all errors
			row.ClearErrors();
			
			if ((row.RowState == DataRowState.Modified) || (row.RowState == DataRowState.Added))
			{
				//Apply Mandatory rules
				isValid = IsValid_RequiredFields(row);
				//Apply Maxlength rules
				isValid &= IsValid_FieldsLength(row);	
				//apply any other rules like unicity, integrity ...
				//Not for now
			}
			//Validation only for Delete Operation
			else if (row.RowState == DataRowState.Deleted)
			{
				isValid = IsValid_Integrity(row);
			}						
            */
			return isValid;
		}
        
		//----------------------------------------------------------------
		// Function IsValid_FieldLength:
		//   Validates a specific Product Ownership Table field against his maxlength 
		// Returns:
		//   False if field fails the validation rules.
		// Parameters:
		//   [in]  row: DataRow of DataTable to be validated
		//   [in]  fieldName: field in DataTable to be validated
		//   [in]  maxLen: max length for the field
		//----------------------------------------------------------------
		private bool IsValid_FieldsLength(DataRow row)
		{
			return true;	
		}


		//----------------------------------------------------------------
		// Function IsValid_RequiredField:
		//   Validates a specific DataTable field as Mandatory 
		// Returns:
		//   False if field fails the validation rules.
		// Parameters:
		//   [in]  row: DataRow from DataTable to be validated
		//----------------------------------------------------------------
		private bool IsValid_RequiredFields(DataRow row)
		{
			return true;
		}
        

		private bool IsValid_Unicity(DataRow row)
		{
			return true;
		}

		private bool IsValid_Integrity(DataRow row)
		{
		
			return true;
		
		}
		
		
		//***************************************************************
		// 
		//***************************************************************
		public LogoData SelectAllDetail(int ID)
		{			
			//This method fill the All Data needed for an organization
			//into a predefined DataSet
			LogoData dts = new LogoData();
			dts.Merge(objDataAccess.SelectOne(ID));
			//Business rule
			LogoSubdivision dalLogoSubdivision = new LogoSubdivision();
			dts.Merge(dalLogoSubdivision.SelectAllByLogoID(ID));
			return dts;
			
		}

		public LogoData InitializeLogo(int UserID)
		{
			//We prepare the DataSet for all step
			//Add a new Row
			LogoData dts = new LogoData();
			
			//Create a new Organization  row at start
			LogoTable promoTable = dts.Logo;
			DataRow row;
			row = promoTable.NewRow();		
			row[dataDef.FLD_CREATE_USER_ID] = UserID;

			promoTable.Rows.Add(row);		
		
//			//Add a biz rule by default
//			BusinessRuleTable ruleTable = dts.BusinessRule;
//			DataRow ruleRow;
//			ruleRow = ruleTable.NewRow();
//			ruleRow[BusinessRuleTable.FLD_NAME] = "New Business Rules";
//			ruleRow[BusinessRuleTable.FLD_CREATE_USER_ID] = UserID;
//			ruleTable.Rows.Add(ruleRow);

			return dts;
			
		}

		public bool UpdateAllDetail(LogoData dts)
		{			
			bool IsSuccess = true;
			//This method fill the All Data needed for an organization
			//into a predefined DataSet			
			if (dts.Logo.GetChanges() != null)
			{
				IsSuccess = UpdateBatch(dts.Logo);
				if (!IsSuccess)
					return IsSuccess;			
			}
			PrepareTransactionWithNewID(dts);
			//Business Rule
			if (dts.LogoSubdivision.GetChanges() != null)
			{
				LogoSubdivisionSystem regSys = new LogoSubdivisionSystem();
				IsSuccess = regSys.UpdateBatch(dts.LogoSubdivision);
				if (!IsSuccess)
					return IsSuccess;
			}
			return IsSuccess;
			
		}

		private void PrepareTransactionWithNewID(LogoData dts)
		{
			int NewID = Convert.ToInt32(dts.Logo.Rows[0][LogoTable.FLD_PKID]);
			foreach(DataRow row in dts.LogoSubdivision.Rows)
			{
				if (row.RowState == DataRowState.Added)
				{
					row[LogoSubdivisionTable.FLD_LOGO_ID] = NewID;
				}
			}
		}	

		/********************************************************************
		 * 
		 * 
		 * *****************************************************************/


		//public dataDef SelectAll_Search(int SearchType, String Criteria, int SubdivisionID, int NationalStatus, int DisplayStatus, string StartDate, string EndDate, string FM_ID, bool IncludeFMReportedTo)
		public dataDef SelectAll_Search(int searchType, string criteria, int logoType, int displayStatus, string fm_id, int imageCategory)
        {
            if (criteria.Length == 0)
            {
                searchType = -1;
            }

            return objDataAccess.SelectAll_Search(searchType, criteria, logoType, displayStatus, fm_id, imageCategory);
        }
		public dataDef SelectAll()
		{			
			dataDef dTbl;
			dTbl = objDataAccess.SelectAll();		
			return dTbl;	
		}
		public dataDef SelectOne(int ID)
		{			
			dataDef dTbl;
			dTbl = objDataAccess.SelectOne(ID);		
			return dTbl;		
		}
		public AV_AssetTable SelectAllLogoURLByOrderID(int OrderID)
		{
			dataDef dTbl;
			dTbl = SelectAllByOrderID(OrderID);
			AV_AssetTable tbl = new AV_AssetTable();
			string path = HttpRuntime.AppDomainAppVirtualPath;
			path = QSPForm.Common.QSPFormConfiguration.LogoImagePreviewPath;
			string previewFileExt = QSPForm.Common.QSPFormConfiguration.ImagePreviewFileExtension;
			foreach(DataRow r in dTbl.Rows)
			{
				DataRow newRow = tbl.NewRow();				
				string sImageUrl = path + r[dataDef.FLD_PKID]+ "." + previewFileExt; //				
				newRow[AV_AssetTable.FLD_ASSET_ID] = Convert.ToInt32(r[dataDef.FLD_PKID].ToString());

				if (sImageUrl.Length > 0)
				{
					try
					{
						
						newRow[AV_AssetTable.FLD_ASSET_PATH] = HttpContext.Current.Server.MapPath(sImageUrl);
					}
					catch
					{
						
					}
				}
				string sApplicationURL = ""; 
				sApplicationURL = HttpContext.Current.Request.Url.AbsoluteUri;
				int iIndex = sApplicationURL.IndexOf(HttpContext.Current.Request.ApplicationPath);
				sApplicationURL = sApplicationURL.Substring(0, iIndex + HttpContext.Current.Request.ApplicationPath.Length);

				sImageUrl = sApplicationURL + "/" + sImageUrl;
				newRow[AV_AssetTable.FLD_ASSET_URL] = sImageUrl;
				newRow[AV_AssetTable.FLD_ASSET_HR_FILE_EXTENSION] = r[dataDef.FLD_FILE_EXTENSION].ToString();
				newRow[AV_AssetTable.FLD_ASSET_NAME] = r[dataDef.FLD_LOGO_NAME].ToString();
				newRow[AV_AssetTable.FLD_ASSET_DESCRIPTION] = r[dataDef.FLD_DESCRIPTION].ToString();
				newRow[AV_AssetTable.FLD_ASSET_CATEGORY] = r[dataDef.FLD_CATEGORY].ToString();
				tbl.Rows.Add(newRow);
			}
			tbl.AcceptChanges();
			return tbl;
		}

		private dataDef SelectAllByOrderID(int OrderID)
		{	
			return objDataAccess.SelectAllLogoURLByOrderID(OrderID);
		}

        
        public dataDef SelectAllFavoriteByFMID(string FMID)
        {
            return objDataAccess.SelectAllFavoriteByFMID(FMID);
        }
        

		public byte[] GetImage(int LogoID, int Resolution)
		{	
			try
			{			
				string path = ""; 
				string previewFileExt = QSPForm.Common.QSPFormConfiguration.ImagePreviewFileExtension;
				switch(Resolution)
				{
					case 1:
						path = QSPForm.Common.QSPFormConfiguration.LogoImagePreviewPath;
						break;
					case 2:
						path = QSPForm.Common.QSPFormConfiguration.LogoImagePath;
						break;
				}

				DataTable dtbl = SelectOne(LogoID);
				if (dtbl.Rows.Count > 0)
				{
					if (Resolution == 1)
						path += LogoID + "." + previewFileExt;
					else
						path += LogoID + "." + dtbl.Rows[0][dataDef.FLD_FILE_EXTENSION].ToString();

					//Get the Physical path
					path = HttpContext.Current.Server.MapPath(path);
					if (System.IO.File.Exists(path))
					{
						System.IO.FileStream inFile;     
						byte[] binaryData;

						inFile = new System.IO.FileStream(path,
							System.IO.FileMode.Open,
							System.IO.FileAccess.Read);
						binaryData = new Byte[inFile.Length];
						long bytesRead = inFile.Read(binaryData, 0,
							(int)inFile.Length);
						inFile.Close();

						return binaryData;
					}
					else
					{
						return null;
					}
				}
				else
				{
					return null;
				}
			}
			catch
			{
				return null;
			}
		}
	}
}
