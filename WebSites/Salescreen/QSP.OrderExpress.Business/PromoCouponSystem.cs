using System;
using QSPForm.Common;

namespace QSPForm.Business
{
	
	using System;
	using System.Data;
	using QSPForm.Common.DataDef;
	using QSPForm.Data;
	using dataDef = QSPForm.Common.DataDef.PromoCouponTable;
	using dataAccessRef = QSPForm.Data.Promo_coupon;
	
	/// <summary>
	///     This class contains the business rules that are used for an 
	///     Product.
	/// </summary>

	public class PromoCouponSystem : BusinessSystem
	{
		
		dataAccessRef objDataAccess;
		
		public PromoCouponSystem()
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
        public int Insert(DataTable dtbl)
        { 
            //temp
            DataRow r = dtbl.Rows[0];
            string description = r[PromoCouponTable.FLD_DESCRIPTION].ToString();
            int text_id = Convert.ToInt32(r[PromoCouponTable.FLD_PROMO_TEXT_ID].ToString());
            int logo_id = Convert.ToInt32(r[PromoCouponTable.FLD_PROMO_LOGO_ID].ToString());
            int vendor_id = Convert.ToInt32(r[PromoCouponTable.FLD_VENDOR_ID].ToString());
            string fm_id = r[PromoCouponTable.FLD_FM_ID].ToString();
            DateTime lbl_start = Convert.ToDateTime(r[PromoCouponTable.FLD_LABELING_START_DATE].ToString());
            DateTime lbl_end = Convert.ToDateTime(r[PromoCouponTable.FLD_LABELING_END_DATE].ToString());
            DateTime exp = Convert.ToDateTime(r[PromoCouponTable.FLD_EXPIRATION_DATE].ToString());
            int user_id = Convert.ToInt32(r[PromoCouponTable.FLD_CREATE_USER_ID].ToString());
            return objDataAccess.Insert(description,
                                        text_id,
                                        logo_id,
                                        vendor_id,
                                        fm_id,
                                        lbl_start,
                                        lbl_end,
                                        exp,
                                        user_id);
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
		//  SELECT All Detail and fill all table of the DataSet
		//***************************************************************
		public PromoCouponData SelectAllDetail(int ID)
		{			
			//This method fill the All Data needed for an organization
			//into a predefined DataSet
			PromoCouponData dts = new PromoCouponData();
			dts.Merge(objDataAccess.SelectOne(ID));
			//Promo			
			Promo dalPromo = new Promo();
			dts.Merge(dalPromo.SelectAllWpromo_coupon_idLogic(ID));	
			//Promo Subdivision
			PromoTable dTblPromo = dts.Promo;
			if (dTblPromo.Rows.Count >0)
			{
				int promoID = Convert.ToInt32(dTblPromo.Rows[0][PromoTable.FLD_PKID]);
				PromoSubdivision dalPromoSubdivision = new PromoSubdivision();
				dts.Merge(dalPromoSubdivision.SelectAllByPromoID(promoID));
			}
			//Document
			Document_entity dalDocument = new Document_entity();
			dts.Merge(dalDocument.SelectAllWentity_idLogic(ID, QSPForm.Common.EntityType.TYPE_PROMO_COUPON));
			//Get Exception if it exists
			Entity_exception dalException = new Entity_exception();
			dts.Merge(dalException.SelectAllWentity_idLogic(ID, QSPForm.Common.EntityType.TYPE_PROMO_COUPON));
			
			
			return dts;
			
		}

		public PromoCouponData InitializePromoCoupon(int UserID)
		{
			//We prepare the DataSet for all step
			//Add a new Row
			PromoCouponData dts = new PromoCouponData();
			
			//Create a new Organization  row at start
			PromoCouponTable promoCouponTable = dts.PromoCoupon;
			DataRow row;
			row = promoCouponTable.NewRow();		
			row[dataDef.FLD_CREATE_USER_ID] = UserID;
            //FormSystem frmSys = new FormSystem();
            //int FormID  = frmSys.GetCurrentFormID(QSPForm.Common.EntityType.TYPE_PROMO_COUPON);
            //if (FormID > 0)
            //    row[dataDef.FLD_FORM_ID] = FormID;

			promoCouponTable.Rows.Add(row);		

			return dts;			
		}

		public bool  UpdateAllDetail(PromoCouponData dts, int UserID)
		{			
			String TransactionName = "PromoCoupon_UpdateAllDetail";						
			Data.ConnectionProvider connProvider = new Data.ConnectionProvider();
			CommonSystem comSys = new CommonSystem();
					
			bool IsSuccess = true;
			try
			{
				int promoID = 0;
				int promoCouponID = 0;
				
				DataRow promoCouponRow;
				promoCouponRow = dts.PromoCoupon.Rows[0];
				
				Data.Promo_coupon coupDataAccess = new Data.Promo_coupon();				
				Data.Promo promoDataAccess = new Data.Promo();
				Data.PromoSubdivision subdDataAccess = new Data.PromoSubdivision();
				
				promoDataAccess.MainConnectionProvider = connProvider;
				subdDataAccess.MainConnectionProvider = connProvider;
				coupDataAccess.MainConnectionProvider = connProvider;
					
				connProvider.OpenConnection();
				connProvider.BeginTransaction(TransactionName);
					

				//We have to process before cause the relation to the document 
				//is in the promo coupon
				SetDocumentRequirement(dts, UserID,	connProvider);
				//-------------------------------------/////
				//Rematch with Document and Coupon
				//-------------------------------------/////
                //if (dts.PromoDocument.Rows.Count >0)
                //{
                //    int promoDocID  = Convert.ToInt32(dts.PromoDocument.Rows[0][DocumentEntityTable.FLD_PKID]);
                //    comSys.UpdateRow(promoCouponRow, PromoCouponTable.FLD_DOCUMENT_ID, promoDocID.ToString());
                //}

				//Rematch and ensure the promoId is populated
				PrepareTransactionWithNewID(dts);
				//Subdivision
////				if (dts.PromoCouponSubdivision.GetChanges() != null)
////				{	
////					subdDataAccess.UpdateBatch(dts.PromoCouponSubdivision);					
//				}
				
				//Promo Coupon
				if (dts.PromoCoupon.GetChanges() != null)
				{
					coupDataAccess.UpdateBatch(dts.PromoCoupon);					
				}
				
				//-------------------------------------/////
				//Rematch Promotion with Promo Coupon
				//-------------------------------------/////
				DataRow promoRow = dts.Promo.Rows[0];
				promoCouponID  = Convert.ToInt32(dts.PromoCoupon.Rows[0][PromoCouponTable.FLD_PKID]);
				comSys.UpdateRow(promoRow, PromoTable.FLD_PROMO_COUPON_ID, promoCouponID.ToString());

				//Update the header at the end
				if (dts.PromoCoupon.GetChanges() != null)
				{
					promoDataAccess.UpdateBatch(dts.PromoCoupon);					
				}

				

				//Business Validation
				//Refresh(dts, UserID, DataOperation.UPDATE, connProvider);
			
				//Commit transaction 
				connProvider.CommitTransaction();
				IsSuccess = true;
				
			}
			catch (Exception ex)
			{
				if (connProvider != null)
				{
					if (connProvider.DBConnection.State != ConnectionState.Closed)
						connProvider.RollbackTransaction(TransactionName);
				}
				IsSuccess = false;
			}
			finally
			{
				if (connProvider != null)
				{
					if (connProvider.DBConnection.State != ConnectionState.Closed)
						connProvider.CloseConnection(false);
				}
			}			

			return IsSuccess;
		
		}

		private void SetDocumentRequirement(PromoCouponData dts, int UserID, Data.ConnectionProvider connProvider)
		{
			bool IsCouponAgrFormRequired = false;
			//If a PromoCoupon coupon row exist we need a Coupon Agreement document
			//we add a row in the DocumentEntityTable whenit's the case
			int promoCouponID = Convert.ToInt32(dts.PromoCoupon.Rows[0][PromoCouponTable.FLD_PKID]);
			DocumentEntityTable promoDoc = dts.PromoDocument;
			IsCouponAgrFormRequired = dts.PromoCoupon.Rows.Count > 0;
			if (IsCouponAgrFormRequired)
			{
				if (promoDoc.Rows.Count == 0)
				{
					DataRow newRow = promoDoc.NewRow();
					newRow[DocumentEntityTable.FLD_ENTITY_ID] = promoCouponID;
					newRow[DocumentEntityTable.FLD_ENTITY_TYPE_ID] = EntityType.TYPE_PROMO_COUPON;
					newRow[DocumentEntityTable.FLD_DOCUMENT_NAME] = "Coupon Agreement Form: PromoCoupon # " + promoCouponID.ToString();
					newRow[DocumentEntityTable.FLD_DOCUMENT_TYPE_ID] = DocumentType.COUPON_AGREEMENT;
					newRow[DocumentEntityTable.FLD_CREATE_USER_ID] = UserID;
					promoDoc.Rows.Add(newRow);

					Data.Document_entity docDataAccess = new Data.Document_entity();
					if (connProvider != null)
						docDataAccess.MainConnectionProvider = connProvider;
					docDataAccess.Insert(promoDoc);
				}				
			}
			else
			{				
				if (promoDoc.Rows.Count > 0)
				{
					//Nothing For Now
					//					DataRow docRow = crdDoc.Rows[0];
					//					docRow[DocumentEntityTable.FLD_UPDATE_USER_ID] = UserID;
					//					docRow.Delete();
					//					DocumentEntitySystem docSys = new DocumentEntitySystem();
					//					docSys.Delete(crdDoc);

				}
			}
			
		}

		private void PrepareTransactionWithNewID(PromoCouponData dts)
		{
			int NewID = Convert.ToInt32(dts.PromoCoupon.Rows[0][PromoCouponTable.FLD_PKID]);
//			foreach(DataRow row in dts.PromoCouponSubdivision.Rows)
//			{
//				if (row.RowState == DataRowState.Added)
//				{
//					row[PromoSubdivisionTable.FLD_PROMO_ID] = NewID;
//				}
//			}
		}	

		/********************************************************************
		 * 
		 * 
		 * *****************************************************************/


		//public dataDef SelectAll_Search(int SearchType, String Criteria, int SubdivisionID, int NationalStatus, int DisplayStatus, string StartDate, string EndDate, string FM_ID, bool IncludeFMReportedTo)
		public dataDef SelectAll_Search(int SearchType, string Criteria, DateTime ExpirationDateMin, DateTime ExpirationDateMax,int isReceived, string subdivision_code, bool IncludeFMReportedTo, int IsNational)
		{			
			dataDef dTbl;
			dTbl = objDataAccess.SelectAll_Search(SearchType,Criteria,ExpirationDateMin,ExpirationDateMax,isReceived,subdivision_code,IncludeFMReportedTo,IsNational);
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

		

	}
}
