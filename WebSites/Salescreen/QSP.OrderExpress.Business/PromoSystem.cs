using System;
using QSPForm.Common;
using System.Web;

namespace QSPForm.Business
{
	
	using System;
	using System.Data;
	using QSPForm.Common.DataDef;
	using QSPForm.Data;
	using dataDef = QSPForm.Common.DataDef.PromoTable;
	using dataAccessRef = QSPForm.Data.Promo;
	
	/// <summary>
	///     This class contains the business rules that are used for an 
	///     Product.
	/// </summary>

	public class PromoSystem : BusinessSystem
	{
		
		dataAccessRef objDataAccess;
		
		public PromoSystem()
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
		public PromoData SelectAllDetail(int ID)
		{			
			//This method fill the All Data needed for an organization
			//into a predefined DataSet
			PromoData dts = new PromoData();
			dts.Merge(objDataAccess.SelectOne(ID));
			//Business rule
			PromoSubdivision dalPromoSubdivision = new PromoSubdivision();
			dts.Merge(dalPromoSubdivision.SelectAllByPromoID(ID));
			return dts;
			
		}

		public PromoData InitializePromo(int UserID)
		{
			//We prepare the DataSet for all step
			//Add a new Row
			PromoData dts = new PromoData();
			
			//Create a new Organization  row at start
			PromoTable promoTable = dts.Promo;
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

		public bool UpdateAllDetail(PromoData dts)
		{			
			bool IsSuccess = true;
			//This method fill the All Data needed for an organization
			//into a predefined DataSet			
			if (dts.Promo.GetChanges() != null)
			{
				IsSuccess = UpdateBatch(dts.Promo);
				if (!IsSuccess)
					return IsSuccess;			
			}
			PrepareTransactionWithNewID(dts);
			//Business Rule
			if (dts.PromoSubdivision.GetChanges() != null)
			{
				PromoSubdivisionSystem regSys = new PromoSubdivisionSystem();
				IsSuccess = regSys.UpdateBatch(dts.PromoSubdivision);
				if (!IsSuccess)
					return IsSuccess;
			}
			return IsSuccess;
			
		}

		private void PrepareTransactionWithNewID(PromoData dts)
		{
			int NewID = Convert.ToInt32(dts.Promo.Rows[0][PromoTable.FLD_PKID]);
			foreach(DataRow row in dts.PromoSubdivision.Rows)
			{
				if (row.RowState == DataRowState.Added)
				{
					row[PromoSubdivisionTable.FLD_PROMO_ID] = NewID;
				}
			}
		}	

		/********************************************************************
		 * 
		 * 
		 * *****************************************************************/


		//public dataDef SelectAll_Search(int SearchType, String Criteria, int SubdivisionID, int NationalStatus, int DisplayStatus, string StartDate, string EndDate, string FM_ID, bool IncludeFMReportedTo)
		public dataDef SelectAll_Search(int SearchType, String Criteria, string SubdivisionID, int NationalStatus, int DisplayStatus, DateTime StartDate, DateTime EndDate, string FM_ID, bool IncludeFMReportedTo)
		{			
			dataDef dTbl;
			dTbl = objDataAccess.SelectAll_Search(SearchType, Criteria, SubdivisionID, NationalStatus, DisplayStatus, StartDate, EndDate, FM_ID, IncludeFMReportedTo);		
			return dTbl;	
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

		public AV_AssetTable SelectAllPromoURLByOrderID(int OrderID)
		{
			dataDef dTbl;
			dTbl = SelectAllByOrderID(OrderID);
			AV_AssetTable tbl = new AV_AssetTable();
			string path = QSPForm.Common.QSPFormConfiguration.PromoImagePreviewPath;
			string previewFileExt = QSPForm.Common.QSPFormConfiguration.ImagePreviewFileExtension;
			foreach(DataRow r in dTbl.Rows)
			{
				DataRow newRow = tbl.NewRow();				
				newRow[QSPForm.Common.DataDef.AV_AssetTable.FLD_ASSET_ID] = Convert.ToInt32(r[dataDef.FLD_PKID].ToString());
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
				newRow[QSPForm.Common.DataDef.AV_AssetTable.FLD_ASSET_NAME] = r[dataDef.FLD_PROMO_NAME].ToString();
				newRow[QSPForm.Common.DataDef.AV_AssetTable.FLD_ASSET_DESCRIPTION] = r[dataDef.FLD_DESCRIPTION].ToString();
				newRow[QSPForm.Common.DataDef.AV_AssetTable.FLD_ASSET_CATEGORY] = r[dataDef.FLD_CATEGORY].ToString();
				tbl.Rows.Add(newRow);
			}
			tbl.AcceptChanges();
			return tbl;
		}
		private dataDef SelectAllByOrderID(int OrderID)
		{	
			return objDataAccess.SelectAllPromoURLByOrderID(OrderID);
		}

		//public string GetImage(int PromoID, int Resolution)
		public byte[] GetImage(int PromoID, int Resolution)
		{	
			try
			{
				string path = ""; 
				string previewFileExt = QSPForm.Common.QSPFormConfiguration.ImagePreviewFileExtension;
				switch(Resolution)
				{
					case 1:
						path = QSPForm.Common.QSPFormConfiguration.PromoImagePreviewPath;//HttpContext.Current.Server.MapPath(QSPForm.Common.QSPFormConfiguration.PromoImagePreviewPath);
						break;
					case 2:
						path = QSPForm.Common.QSPFormConfiguration.PromoImagePath;//HttpContext.Current.Server.MapPath(QSPForm.Common.QSPFormConfiguration.PromoImagePath);
						break;
				}

				DataTable dtbl = SelectOne(PromoID);
				if (dtbl.Rows.Count > 0)
				{
					if (Resolution == 1)
						path += PromoID + "." + previewFileExt;
					else
						path += PromoID + "." + dtbl.Rows[0][dataDef.FLD_FILE_EXTENSION].ToString();

					path = HttpContext.Current.Server.MapPath(path);
					//System.IO.Stream stream = System.IO.File.OpenRead(path); 
					//return stream;
					//System.Drawing.Image img = System.Drawing.Image.FromStream(stream); 
					//return img;
					//********************************************************************//
					System.IO.FileStream inFile;     
					byte[] binaryData;
					if (System.IO.File.Exists(path))
					{
						inFile = new System.IO.FileStream(path,
							System.IO.FileMode.Open,
							System.IO.FileAccess.Read);
						binaryData = new Byte[inFile.Length];
						long bytesRead = inFile.Read(binaryData, 0,
							(int)inFile.Length);
						inFile.Close();

						//return System.Convert.ToBase64String(binaryData,0,binaryData.Length);
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
