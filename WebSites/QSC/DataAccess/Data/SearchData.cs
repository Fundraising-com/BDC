using System;
using System.Data.SqlClient;
using System.Data;

namespace QSPFulfillment.DataAccess.Data
{
	/// <summary>
	/// Summary description for SearchData.
	/// </summary>
	public class SearchData:DBInteractionBase
	{
		public SearchData()
		{
			
		}

		public void SelectSearchOrder(DataTable Table,ParameterValueList List)
		{
			SqlCommand CmdToExecute = new SqlCommand();
			CmdToExecute.CommandText = "pr_SelectSearchOrder";
			CmdToExecute.CommandType = CommandType.StoredProcedure;
			AddParameters(CmdToExecute,List);
			CmdToExecute.Parameters.Add(new SqlParameter("@Query",SqlDbType.NVarChar,4000,ParameterDirection.Output,true,1,1,"",DataRowVersion.Current,""));
			Select(CmdToExecute,Table);
		}
		public void SelectSearchShippement(DataTable Table,ParameterValueList List)
		{
			SqlCommand CmdToExecute = new SqlCommand();
			CmdToExecute.CommandText = "pr_SelectSearchShipment";
			CmdToExecute.CommandType = CommandType.StoredProcedure;
			AddParameters(CmdToExecute,List);
			CmdToExecute.Parameters.Add(new SqlParameter("@Query",SqlDbType.NVarChar,4000,ParameterDirection.Output,true,1,1,"",DataRowVersion.Current,""));
			Select(CmdToExecute,Table);
		}
		public void SelectSearchSubscription(DataTable Table,ParameterValueList List)
		{
			SqlCommand CmdToExecute = new SqlCommand();
			CmdToExecute.CommandText = "pr_SelectSearchSubscription";
			CmdToExecute.CommandType = CommandType.StoredProcedure;
			AddParameters(CmdToExecute,List);
			CmdToExecute.Parameters.Add(new SqlParameter("@Query",SqlDbType.NVarChar,4000,ParameterDirection.Output,true,1,1,"",DataRowVersion.Current,""));
			Select(CmdToExecute,Table);
		}
		public void SelectSearchSubscription(DataTable Table,DataTable ListOrderID,ParameterValueList List)
		{
			SqlCommand CmdToExecute = new SqlCommand();
			CmdToExecute.CommandText = "pr_SelectSearchSubscription";
			CmdToExecute.CommandType = CommandType.StoredProcedure;
			
			CmdToExecute.Parameters.Add("@List",GetList(ListOrderID,"OrderID"));
			AddParameters(CmdToExecute,List);
			CmdToExecute.Parameters.Add(new SqlParameter("@Query",SqlDbType.NVarChar,4000,ParameterDirection.Output,true,1,1,"",DataRowVersion.Current,""));
			Select(CmdToExecute,Table);
		}
		public void SelectSearchMagazine(DataTable Table,ParameterValueList List)
		{
			SqlCommand CmdToExecute = new SqlCommand();
			CmdToExecute.CommandText = "pr_SelectSearchMagazine";
			CmdToExecute.CommandType = CommandType.StoredProcedure;
			
			AddParameters(CmdToExecute,List);
			//CmdToExecute.Parameters.Add(new SqlParameter("@iProductYpe",SqlDbType.Int,4,ParameterDirection.Output,true,1,1,"",DataRowVersion.Current,ItemType));
			Select(CmdToExecute,Table);
		}
		public void SelectSearchCreditCard(DataTable Table,ParameterValueList List)
		{
			SqlCommand CmdToExecute = new SqlCommand();
			CmdToExecute.CommandText = "pr_SelectSearchCreditCard";
			CmdToExecute.CommandType = CommandType.StoredProcedure;
			AddParameters(CmdToExecute,List);
			CmdToExecute.Parameters.Add(new SqlParameter("@Query",SqlDbType.NVarChar,4000,ParameterDirection.Output,true,1,1,"",DataRowVersion.Current,""));
			Select(CmdToExecute,Table);
		}
		public void SelectSearchProduct(DataTable Table,DataTable ListOrderID,int ItemType)
		{
			SqlCommand CmdToExecute = new SqlCommand();
			CmdToExecute.CommandText = "pr_SelectSearchProduct";
			CmdToExecute.CommandType = CommandType.StoredProcedure;
			
			CmdToExecute.Parameters.Add("@List",GetList(ListOrderID,"OrderID"));
			CmdToExecute.Parameters.Add(new SqlParameter("@iProductType",SqlDbType.Int,4,ParameterDirection.Input,true,1,1,"",DataRowVersion.Current,ItemType));
			CmdToExecute.Parameters.Add(new SqlParameter("@Query",SqlDbType.NVarChar,4000,ParameterDirection.Output,true,1,1,"",DataRowVersion.Current,""));
			Select(CmdToExecute,Table);
		}
		public void SelectSearchCreditCardDetails(DataTable Table,DataTable List)
		{
			SqlCommand CmdToExecute = new SqlCommand();
			CmdToExecute.CommandText = "pr_SelectSearchCreditCardDetails";
			CmdToExecute.CommandType = CommandType.StoredProcedure;
			
			CmdToExecute.Parameters.Add("@List",GetList(List,"CustomerOrderHeaderInstance"));
			CmdToExecute.Parameters.Add(new SqlParameter("@Query",SqlDbType.NVarChar,4000,ParameterDirection.Output,true,1,1,"",DataRowVersion.Current,""));
			Select(CmdToExecute,Table);
		}

		public void SelectCampaignsForStatementsByAdjustmentBatchID(DataTable Table, int AdjustmentBatchID) 
		{
			SqlCommand CmdToExecute = new SqlCommand();
			CmdToExecute.CommandText = "pr_Statements_SelectCampaignsByAdjustmentBatchID";
			CmdToExecute.CommandType = CommandType.StoredProcedure;
			CmdToExecute.Parameters.Add("@iAdjustmentBatchID", AdjustmentBatchID);
			Select(CmdToExecute,Table);
		}


        public void SelectCampaignsForCAStatement(DataTable Table, int StatementPrintRequestBatchID) 
		{
			SqlCommand CmdToExecute = new SqlCommand();
            CmdToExecute.CommandText = "QSPCanadaFinance..StatementPrintRequest_Select";
			CmdToExecute.CommandType = CommandType.StoredProcedure;
            CmdToExecute.Parameters.Add("@StatementPrintRequestBatchID", StatementPrintRequestBatchID);
			Select(CmdToExecute,Table);
		}

		public void SelectCCProcessList(DataTable table) 
		{
			SqlCommand cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "pr_TempCCProcess_SelectAll";
			cmdToExecute.CommandType = CommandType.StoredProcedure;

			Select(cmdToExecute, table);
		}

		public void UpdateCCProcessList(string creditCardNumber, string authorizationCode) 
		{
			SqlCommand cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "pr_TempCCProcess_Update";
			cmdToExecute.CommandType = CommandType.StoredProcedure;

			cmdToExecute.Parameters.Add("@zCreditCardNumber", creditCardNumber);
			cmdToExecute.Parameters.Add("@zAuthorizationCode", authorizationCode);
			ExecuteCmd(cmdToExecute);
		}

		private string GetList(DataTable List,string Column)
		{
			bool IsFirst = true;
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append(Column + " in (");

			foreach(DataRow row in List.Rows)
			{
				
				if(!IsFirst)
					sb.Append(",");

				sb.Append(row[Column].ToString());
				
				IsFirst = false;
			}
			sb.Append(")");
			return sb.ToString();
		}
		

	}
}
