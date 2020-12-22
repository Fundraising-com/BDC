using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace QSPForm.Business
{
    using System;
    using System.Data;
    using QSPForm.Common.DataDef;
    using QSPForm.Data;
    using dataDef = QSPForm.Common.DataDef.AccountTransferAccountTable;
    using dataDefOrg = QSPForm.Common.DataDef.AccountTransferOrganizationTable;
    using dataDefAccount = QSPForm.Common.DataDef.AccountTable;
    using dataAccessRef = QSPForm.Data.FMTransferAccount;

    /// <summary>
    ///     This class contains the business rules that are used for an 
    ///     FM Account Transfer.
    /// </summary>
    public class FMAccountTransferSystem : BusinessSystem
    {
        dataAccessRef orgDataAccess;
        
        public FMAccountTransferSystem()
		{
			orgDataAccess = new dataAccessRef();
		}


    

        public dataDef SelectAllAccountByFMID(string FMID)
        {
            dataDef dTbl;
            //
            // Get the user DataTable from the DataLayer
            //
            dTbl = orgDataAccess.SelectAccount(FMID);

            return dTbl;
        }

        public dataDefOrg SelectAllflagPoleOrganizationByFMID(string FMID)
        {
            dataDefOrg dTbl;
            //
            // Get the user DataTable from the DataLayer
            //
            dTbl = orgDataAccess.SelectOrg(FMID);

            return dTbl;
        }

        public bool UpdateAccountsBYFMID(string accountid, string fromfmid, string tofmid, string salestofmid, DateTime effectiveDate, string reason, int icreateuserId)
        {
            //Variable to handle the operation in One transaction transaction
            String TransactionName = "UpdateAccountsByFMID";
            Data.ConnectionProvider connProvider = new Data.ConnectionProvider();
            bool isSuccess = false;
            string FromFMIDForRow = string.Empty;
            String[] sResult = accountid.Split(',');
            String[] SFMIDs = fromfmid.Split(',');
            
            try
            {
                orgDataAccess.MainConnectionProvider = connProvider;

                connProvider.OpenConnection();
                connProvider.BeginTransaction(TransactionName);

                for (int i = 0; i < sResult.Length; i++)
                {
                    if (SFMIDs[i].Trim().Length > 4)
                    {
                        FromFMIDForRow = SFMIDs[i].Trim().Substring(0, 4);
                    }
                    isSuccess = orgDataAccess.UpdateAccountsByFMID(sResult[i], FromFMIDForRow, tofmid, salestofmid, effectiveDate, reason, icreateuserId);
                }

                //Commit transaction 
                connProvider.CommitTransaction();


            }
            catch (Exception ex)
            {
                if (connProvider != null)
				{
					if (connProvider.DBConnection.State != ConnectionState.Closed)
						connProvider.RollbackTransaction(TransactionName);
				}
                isSuccess = false;
                throw ex;
            }
            finally
            {
                if (connProvider != null)
				{
					if (connProvider.DBConnection.State != ConnectionState.Closed)
						connProvider.CloseConnection(false);
				}
            }

            return isSuccess;

        }


        public dataDef SelectAll_Search(int SearchType, String Criteria, int ProgramType, string SubdivisionCode, string FMID, int FSM_DisplayMode, int StatusCategoryID, string FMName)
        {
            dataDef dTbl;

            //
            // Get the user DataTable from the DataLayer
            //				
            dTbl = orgDataAccess.SelectAll_Search(SearchType, Criteria, ProgramType, SubdivisionCode, FMID, FSM_DisplayMode, StatusCategoryID, FMName);

            return dTbl;
        }	

    }
}
