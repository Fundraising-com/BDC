using System;
using System.Data;
using System.Data.SqlClient;
using GA.BDC.Core.EnterpriseComponents;
using System.Configuration;
using System.Diagnostics;

namespace CRMWeb {
	/// <summary>
	/// Summary description for DatabaseObjects.
	/// </summary>
	public class DatabaseObjects {
			
		public DatabaseObjects() {
			
			//
			// TODO: Add constructor logic here
			//
		}


        

		public static DataTable GetPayments(int salesID) {
			DatabaseInterface db = new DatabaseInterface();
			DataTable dt = new DataTable();

			try {

				DataParameters[] parameters = new DataParameters[2];
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@intSalesID";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = salesID;			
				

				parameters[1] = new DataParameters();
				parameters[1].ParameterName = "@intPaymentNo";
				parameters[1].DataType = DbType.Int32;
				parameters[1].ParamDirection = ParameterDirection.Input;
				parameters[1].Value = 0;			
				
				/////////////
				///

				
				string connStr = ConfigurationSettings.AppSettings["ConnectionString"];
				dt = db.ExecuteFetchDataTable("crm_get_payment_details", CommandType.StoredProcedure, parameters);

 			
				
				
			}catch (Exception ex) {
				throw new Global.CRMException("",ex,0,"DataBaseObjects.GetPayments");
			}

			return(dt);
		}


		public static DataTable GetKitTypes(){
			DatabaseInterface db = new DatabaseInterface();
			DataTable dt = new DataTable();

			try {

				DataParameters[] parameters = new DataParameters[0];
				
				string connStr = ConfigurationSettings.AppSettings["ConnectionString"];
		        dt = db.ExecuteFetchDataTable("select kit_type_id, description from kit_type",CommandType.Text, parameters);
		
				
			}catch (Exception ex) {
               throw new Global.CRMException("",ex,0,"DataBaseObjects.GetKitTypes");
			}

			return(dt);
				
		}


		public static DataTable GetPromotionDetail(int promotionID){
			DatabaseInterface db = new DatabaseInterface();
			DataTable dt = new DataTable();

			try {

				DataParameters[] parameters = new DataParameters[1];
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@promotion_id";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = promotionID;	
				
				string connStr = ConfigurationSettings.AppSettings["ConnectionString"];
				dt = db.ExecuteFetchDataTable("crm_get_promotion_detail",CommandType.StoredProcedure, parameters);
		
				
			}catch (Exception ex) {
				throw new Global.CRMException("",ex,0,"DataBaseObjects.GetPromotionDetails");
			}

			return(dt);
				
		}

		public static DataTable GetConsultantList(int GroupType){
			DatabaseInterface db = new DatabaseInterface();
			DataTable dt = new DataTable();

			try {

				DataParameters[] parameters = new DataParameters[0];
				
				
				string connStr = ConfigurationSettings.AppSettings["ConnectionString"];
				//attendre que les id soit stable
				//dt = db.ExecuteFetchDataTable("crm_get_sale_consultants",CommandType.StoredProcedure, parameters);
				dt = db.ExecuteFetchDataTable("SELECT Consultant_ID, Name FROM Consultant WHERE Is_Active=1 AND CSR_Consultant=0 AND Consultant.Is_Fm = 0 AND Consultant.Department_ID in(7,17,18) order by name",CommandType.Text, parameters);

		
				
			}catch (Exception ex) {
				throw new Global.CRMException("",ex,0,"DataBaseObjects.GetConsultantList");
			}

			return(dt);
				
		}

		public static DataTable GetAdjustments(int salesID) {
			DatabaseInterface db = new DatabaseInterface();
			DataTable dt = new DataTable();

			try {

				DataParameters[] parameters = new DataParameters[1];
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@intSaleID";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = salesID;			
				

				
				
				/////////////
				///

				
				string connStr = ConfigurationSettings.AppSettings["ConnectionString"];
				dt = db.ExecuteFetchDataTable("crm_get_adjustments", CommandType.StoredProcedure, parameters);

 			
				
				
			}catch (Exception ex) {
				throw new Global.CRMException("",ex,0,"DataBaseObjects.GetAdjustments");
			}

			return(dt);
		}


		public static DataTable AuthentificatedUser (string userName, string password) {
			DatabaseInterface db = new DatabaseInterface();
			DataTable dt = new DataTable();

			try {

				DataParameters[] parameters = new DataParameters[2];
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@user_name";
				parameters[0].DataType = DbType.String;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = userName;			
				
				parameters[1] = new DataParameters();
				parameters[1].ParameterName = "@password";
				parameters[1].DataType = DbType.String;
				parameters[1].ParamDirection = ParameterDirection.Input;
				parameters[1].Value = password;			
				
				
				
				/////////////
				///

				
				string connStr = ConfigurationSettings.AppSettings["ConnectionString"];
				dt = (DataTable) db.ExecuteFetchDataTable ("crm_authentificate_user", CommandType.StoredProcedure,parameters);

 			
				
				
			}catch (Exception ex) {
				throw new Global.CRMException("",ex,0,"DataBaseObjects.AuthentificatedUser");
			}

			return(dt);
		}

		public static DataTable GetSpecificPayment(int salesID, int paymentNo) {
			DatabaseInterface db = new DatabaseInterface();
			DataTable dt = new DataTable();

			try {

				DataParameters[] parameters = new DataParameters[2];
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@intSalesID";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = salesID;
			
				parameters[1] = new DataParameters();
				parameters[1].ParameterName = "@intPaymentNo";
				parameters[1].DataType = DbType.Int32;
				parameters[1].ParamDirection = ParameterDirection.Input;
				parameters[1].Value = paymentNo;
			
				
				/////////////
				///

				
				string connStr = ConfigurationSettings.AppSettings["ConnectionString"];
				dt = db.ExecuteFetchDataTable("crm_get_payment_details", CommandType.StoredProcedure, parameters);

 			
				
				
			}catch (Exception ex) {
				throw new Global.CRMException("",ex,0,"DataBaseObjects.GetSpecificPayment");
			}

			return(dt);
		}

		public static DataTable GetSpecificAdjustment(int SalesID, int adjustmentNo) {
			DatabaseInterface db = new DatabaseInterface();
			DataTable dt = new DataTable();

			try {

				DataParameters[] parameters = new DataParameters[2];
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@intSalesID";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = SalesID;
			
				parameters[1] = new DataParameters();
				parameters[1].ParameterName = "@intAdjustmentNo";
				parameters[1].DataType = DbType.Int32;
				parameters[1].ParamDirection = ParameterDirection.Input;
				parameters[1].Value = adjustmentNo;
			
				
				/////////////
				///

				
				string connStr = ConfigurationSettings.AppSettings["ConnectionString"];
				dt = db.ExecuteFetchDataTable("crm_get_adjustment_details", CommandType.StoredProcedure, parameters);

 			
				
				
			}catch (Exception ex) {
				Debug.Write(ex.Message.ToString());
				//throw new AppExceptions(ex.Message, ex);
			}

			return(dt);
		}

		public static DataTable GetSalesPaymentAdjustment(int salesID) {
			DatabaseInterface db = new DatabaseInterface();
			DataTable dt = new DataTable();

			try {

				DataParameters[] parameters = new DataParameters[1];
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@intSalesID";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = salesID;			
				
				/////////////
				///

				
				string connStr = ConfigurationSettings.AppSettings["ConnectionString"];
				dt = db.ExecuteFetchDataTable("crm_get_sales_payment_adjustment", CommandType.StoredProcedure, parameters);

 			
				
				
			}catch (Exception ex) {
				Debug.Write(ex.Message.ToString());
				//throw new AppExceptions(ex.Message, ex);
			}

			return(dt);
		}

		public static DataSet GetTasks_brut(int consultantID, int previousDays, int followingDays) {
		   
			//create new dataset
			DataSet ds = new DataSet();
			try{

			
				string connString = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
			
				// Connection object 
				SqlConnection connection = new SqlConnection (connString); 
                  
				
				//command object 
				SqlCommand cmd = connection.CreateCommand();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "crm_get_tasks_for_today";


				cmd.Parameters.Add(new SqlParameter("@intConsultantID", SqlDbType.Int, 5));
				cmd.Parameters["@intConsultantID"].Value = 1567;
			
				cmd.Parameters.Add(new SqlParameter("@intPreviousDays", SqlDbType.Int, 5));
				cmd.Parameters["@intPreviousDays"].Value = 70;

				cmd.Parameters.Add(new SqlParameter("@intFollowingDays", SqlDbType.Int, 5));
				cmd.Parameters["@intFollowingDays"].Value = 5;



				// Create data adapter object 
				SqlDataAdapter dataAdapter = new SqlDataAdapter();
				dataAdapter.SelectCommand = cmd;			    

						
	

				//dataAdapter.Fill(dataSet, "crm_get_tasks_for_today"); 
				dataAdapter.Fill(ds); 


					
			}catch (Exception ex) {
				Debug.Write(ex.Message.ToString());
				//throw new AppExceptions(ex.Message, ex);
			}

			return ds;



			/*

						DatabaseInterface db = new DatabaseInterface();
						DataTable dt = new DataTable();

						try {

							DataParameters[] parameters = new DataParameters[3];
							parameters[0] = new DataParameters();
							parameters[0].ParameterName = "@intConsultantID";
							parameters[0].DataType = DbType.Int32;
							parameters[0].ParamDirection = ParameterDirection.Input;
							parameters[0].Value = consultantID;
				
							parameters[1] = new DataParameters();
							parameters[1].ParameterName = "@intPreviousDays";
							parameters[1].DataType = DbType.Int32;
							parameters[1].ParamDirection = ParameterDirection.Input;
							parameters[1].Value = previousDays;
				
							parameters[2] = new DataParameters();
							parameters[2].ParameterName = "@intFollowingDays";
							parameters[2].DataType = DbType.Int32;
							parameters[2].ParamDirection = ParameterDirection.Input;
							parameters[2].Value = followingDays;
				
				
							/////////////
							///

				
							string connStr = ConfigurationSettings.AppSettings["ConnectionString"];
							dt = db.ExecuteFetchDataTable("crm_get_tasks_for_today", CommandType.StoredProcedure, parameters);

 			
				
				
						}catch (Exception ex) {
							Debug.Write(ex.Message.ToString());
							//throw new AppExceptions(ex.Message, ex);
						}

						return(dt);*/
		}


		public static DataSet GetUnassignedLeads(int promoGroup){
			DatabaseInterface db = new DatabaseInterface();
			DataSet ds = new DataSet();

			try {

				
				DataParameters[] parameters = new DataParameters[1];
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@promo_group_id";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				if (promoGroup == 0) {
				   parameters[0].Value = DBNull.Value;
				}else{
				   parameters[0].Value = promoGroup;
				}
				
					
				string connStr = ConfigurationSettings.AppSettings["ConnectionString"];
				ds = db.ExecuteFetchDataSet(CommandType.StoredProcedure,"crm_get_unassigned_leads", parameters);
			
				
			}catch (Exception ex) {
				throw new Global.CRMException("",ex,0,"DataBaseObjects.GetUnassignedLeads");
			}

			return(ds);
		}


		public static DataTable GetFMInfoForLead(int leadID){
			DatabaseInterface db = new DatabaseInterface();
			DataSet ds = new DataSet();

			try {

				DataParameters[] parameters = new DataParameters[1];
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@lead_id";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = leadID;
					
				string connStr = ConfigurationSettings.AppSettings["ConnectionString"];
				ds = db.ExecuteFetchDataSet(CommandType.StoredProcedure,"crm_get_lead_fm_info", parameters);
			
				
			}catch (Exception ex) {
				throw new Global.CRMException("",ex,0,"DataBaseObjects.GetFMinfoForLead");
			}

			return(ds.Tables[0]);
		}

		
		public static DataTable GetGirlsScoutLead()
		{
			DatabaseInterface db = new DatabaseInterface();
			DataTable dt = new DataTable();

			try 
			{

				DataParameters[] parameters = new DataParameters[0];

					
				string connStr = ConfigurationSettings.AppSettings["ConnectionString"];
				dt = db.ExecuteFetchDataTable("crm_get_unassigned_girls_scout_leads", CommandType.StoredProcedure,parameters);
				
				
			}
			catch (Exception ex) 
			{
				throw new Global.CRMException("",ex,0,"DataBaseObjects.GetGirlScoutLeads");
			}

			return(dt);
		}

		public static DataTable GetConsultantLeads(int consultantID, string entryStartDate, string entryEndDate, string assignStartDate, string assignEndDate){
			DatabaseInterface db = new DatabaseInterface();
			DataTable dt = new DataTable();

			try {
			
					DataParameters[] parameters = new DataParameters[5];			
					parameters[0] = new DataParameters();
					parameters[0].ParameterName = "@consultant_id";
					parameters[0].DataType = DbType.Int32;
					parameters[0].ParamDirection = ParameterDirection.Input;
					parameters[0].Value = consultantID;
				
					parameters[1] = new DataParameters();
					parameters[1].ParameterName = "@entry_start_date";
					parameters[1].DataType = DbType.DateTime;
					parameters[1].ParamDirection = ParameterDirection.Input;
					if (entryStartDate == ""){
						parameters[1].Value = DBNull.Value;
					}else{
						parameters[1].Value = Convert.ToDateTime(entryStartDate);
					}
				
				
					parameters[2] = new DataParameters();
					parameters[2].ParameterName = "@entry_end_date";
					parameters[2].DataType = DbType.DateTime;
					parameters[2].ParamDirection = ParameterDirection.Input;
					if (entryEndDate == ""){
						parameters[2].Value = DBNull.Value;
					}else{
						parameters[2].Value = Convert.ToDateTime(entryEndDate);
					}
				
					parameters[3] = new DataParameters();
					parameters[3].ParameterName = "@assign_start_date";
					parameters[3].DataType = DbType.DateTime;
					parameters[3].ParamDirection = ParameterDirection.Input;
					if (assignStartDate == ""){
						parameters[3].Value = DBNull.Value;
					}else{
						parameters[3].Value = Convert.ToDateTime(assignStartDate);
					}
				

					parameters[4] = new DataParameters();
					parameters[4].ParameterName = "@assign_end_date";
					parameters[4].DataType = DbType.DateTime;
					parameters[4].ParamDirection = ParameterDirection.Input;
					if (assignEndDate == ""){
						parameters[4].Value = DBNull.Value;
					}else{
						parameters[4].Value = Convert.ToDateTime(assignEndDate);
					}
				

				
					string connStr = ConfigurationSettings.AppSettings["ConnectionString"];
					dt = db.ExecuteFetchDataTable("crm_get_assigned_leads",CommandType.StoredProcedure, parameters);
				
				
				
			}catch (Exception ex) {
			   throw new Global.CRMException("",ex,0,"DataBaseObjects.GetConsultantLeads");
			}

			return(dt);
		}


		public static DataTable GetTasks(int consultantID, int previousDays, int followingDays) {
			DatabaseInterface db = new DatabaseInterface();
			DataTable dt = new DataTable();

			try {

				DataParameters[] parameters = new DataParameters[3];
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@intConsultantID";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = consultantID;
				
				parameters[1] = new DataParameters();
				parameters[1].ParameterName = "@intPreviousDays";
				parameters[1].DataType = DbType.Int32;
				parameters[1].ParamDirection = ParameterDirection.Input;
				parameters[1].Value = previousDays;
				
				parameters[2] = new DataParameters();
				parameters[2].ParameterName = "@intFollowingDays";
				parameters[2].DataType = DbType.Int32;
				parameters[2].ParamDirection = ParameterDirection.Input;
				parameters[2].Value = followingDays;
				
				
				/////////////
				///

				
				string connStr = ConfigurationSettings.AppSettings["ConnectionString"];
				dt = db.ExecuteFetchDataTable("crm_get_tasks_for_today", CommandType.StoredProcedure, parameters);

 			
				
				
			}catch (Exception ex) {
				Debug.Write(ex.Message.ToString());
				//throw new AppExceptions(ex.Message, ex);
			}

			return(dt);
		}


		public static DataTable GetPromoByGroups (int promoGroupID) {
			DatabaseInterface db = new DatabaseInterface();
			DataTable dt = new DataTable();

			try {

				DataParameters[] parameters = new DataParameters[1];
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@promo_group_id";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = promoGroupID;
			
				/////////////
				///
			
				string connStr = ConfigurationSettings.AppSettings["ConnectionString"];
				dt = db.ExecuteFetchDataTable("crm_get_promotions_by_group",CommandType.StoredProcedure, parameters);

 			
				
				
			}catch (Exception ex) {
				throw new Global.CRMException("",ex,0,"DataBaseObjects.GetPromoByGroups");
			}

			return(dt);
		}

		public static int UpdatePromoGroup (int promoGroupID, int promoID) {
			DatabaseInterface db = new DatabaseInterface();
			int success = 0;

			try {

				DataParameters[] parameters = new DataParameters[2];
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@promo_group_id";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = promoGroupID;

				parameters[1] = new DataParameters();
				parameters[1].ParameterName = "@promotion_id";
				parameters[1].DataType = DbType.Int32;
				parameters[1].ParamDirection = ParameterDirection.Input;
				parameters[1].Value = promoID;

				/////////////
				///
			
				string connStr = ConfigurationSettings.AppSettings["ConnectionString"];
				success = db.ExecuteNonQuery(CommandType.StoredProcedure,"crm_update_group_of_promotion", parameters);

 			
				
				
			}catch (Exception ex) {
				throw new Global.CRMException("",ex,0,"DataBaseObjects.UpdatePromoGroup");
			}

			return(success);
		}

		public static DataTable GetPromoGroups () {
			DatabaseInterface db = new DatabaseInterface();
			DataTable dt = new DataTable();

			try {

				DataParameters[] parameters = new DataParameters[0];
			
				/////////////
				///
			
				string connStr = ConfigurationSettings.AppSettings["ConnectionString"];
				dt = db.ExecuteFetchDataTable("select * from promotion_group",CommandType.Text, parameters);

 			
				
				
			}catch (Exception ex) {
				throw new Global.CRMException("",ex,0,"DataBaseObjects.GetPromoGroups");
			}

			return(dt);
		}


		public static DataTable GetPromoTypes () {
			DatabaseInterface db = new DatabaseInterface();
			DataTable dt = new DataTable();

			try {

				DataParameters[] parameters = new DataParameters[0];
			
				/////////////
				///
			
				string connStr = ConfigurationSettings.AppSettings["ConnectionString"];
				dt = db.ExecuteFetchDataTable("select * from promotion_type",CommandType.Text, parameters);
				
				
			}catch (Exception ex) {
				throw new Global.CRMException("",ex,0,"DataBaseObjects.GetPromoTypes");
			}

			return(dt);
		}

		public static DataTable GetPartners () {
			DatabaseInterface db = new DatabaseInterface();
			DataTable dt = new DataTable();

			try {

				DataParameters[] parameters = new DataParameters[0];
			
				/////////////
				///
			
				string connStr = ConfigurationSettings.AppSettings["ConnectionString"];
				dt = db.ExecuteFetchDataTable("select p.partner_id, p.partner_name from partner p inner join promotion pr on p.partner_id = pr.partner_id group by p.partner_id, p.partner_name",CommandType.Text, parameters);
				
				
			}catch (Exception ex) {
				throw new Global.CRMException("",ex,0,"DataBaseObjects.GetPartners");
			}

			return(dt);
		}

		public static DataTable GetTasksForLead(int leadID) {
			DatabaseInterface db = new DatabaseInterface();
			DataTable dt = new DataTable();

			try {

				DataParameters[] parameters = new DataParameters[1];
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@intLeadID";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = leadID;
				
				///

				
				string connStr = ConfigurationSettings.AppSettings["ConnectionString"];
				dt = db.ExecuteFetchDataTable("crm_get_tasks_for_lead", CommandType.StoredProcedure, parameters);

 			
				
				
			}catch (Exception ex) {
				Debug.Write(ex.Message.ToString());
				//throw new AppExceptions(ex.Message, ex);
			}

			return(dt);
		}


		
		public static DataTable GetCollectionStatus() {
			DatabaseInterface db = new DatabaseInterface();
			DataTable dt = new DataTable();

			try {

				DataParameters[] parameters = new DataParameters[0];
				string connStr = ConfigurationSettings.AppSettings["ConnectionString"];
				dt = db.ExecuteFetchDataTable("select * from collection_status", CommandType.Text, parameters);

 			
				
				
			}catch (Exception ex) {
				Debug.Write(ex.Message.ToString());
				//throw new AppExceptions(ex.Message, ex);
			}

			return(dt);
		}


		
		public static int InsertFirstCall(int leadID) {
			DatabaseInterface db = new DatabaseInterface();
			int success = 0;

			try {

				DataParameters[] parameters = new DataParameters[2];
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@lead_id";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = leadID;
				
				parameters[1] = new DataParameters();
				parameters[1].ParameterName = "@activity_type_id";
				parameters[1].DataType = DbType.Int32;
				parameters[1].ParamDirection = ParameterDirection.Input;
				parameters[1].Value = 2;
				
				

				string connStr = ConfigurationSettings.AppSettings["ConnectionString"];
				success = Convert.ToInt32(db.ExecuteNonQuery(CommandType.StoredProcedure,"crm_insert_new_activity", parameters));

 			
				
				
			}catch (Exception ex) {
				throw new Global.CRMException("Insert First Call. Lead: " + leadID,ex,0,"DataBaseObjects.InsertFirstCall");
			}

			return(success);
		}



		public static int AssignLead(int leadID, int consultantID, int assignerID, int kitID, bool transfer) {
			DatabaseInterface db = new DatabaseInterface();
			int success = 0;
            DataParameters[] parameters = new DataParameters[6];
			
			try {

				
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@lead_id";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = leadID;
				
				parameters[1] = new DataParameters();
				parameters[1].ParameterName = "@consultant_id";
				parameters[1].DataType = DbType.Int32;
				parameters[1].ParamDirection = ParameterDirection.Input;
				parameters[1].Value = consultantID;
				
				parameters[2] = new DataParameters();
				parameters[2].ParameterName = "@assigner_id";
				parameters[2].DataType = DbType.Int32;
				parameters[2].ParamDirection = ParameterDirection.Input;
				parameters[2].Value = assignerID;
				
				parameters[3] = new DataParameters();
				parameters[3].ParameterName = "@kit_id";
				parameters[3].DataType = DbType.Int32;
				parameters[3].ParamDirection = ParameterDirection.Input;
				parameters[3].Value = 0;

				parameters[4] = new DataParameters();
				parameters[4].ParameterName = "@transfer_date";
				parameters[4].DataType = DbType.DateTime;
				parameters[4].ParamDirection = ParameterDirection.Input;
				if (transfer){
				   parameters[4].Value = DateTime.Now;
				}else{
                   parameters[4].Value = DBNull.Value;
				}
						
				parameters[5] = new DataParameters();
				parameters[5].ParameterName = "returnValue";
				parameters[5].DataType = DbType.Int32;
				parameters[5].ParamDirection = ParameterDirection.ReturnValue;
							
    			string connStr = ConfigurationSettings.AppSettings["ConnectionString"];
				success = Convert.ToInt32(db.ExecuteScalar(CommandType.StoredProcedure,"crm_assign_lead", parameters));

				//Debug.Write(parameters[5].Value.ToString());


			}catch (Exception ex) {
				throw new Global.CRMException("Error Assigning Lead: " + leadID + " to consultant: " + consultantID,ex,0,"DataBaseObjects.AssignLead");
			}
            
			return(Convert.ToInt32(parameters[5].Value));
			
		}

		public static int UnassignLead(int leadID, int consultantID, int unassignerID) {
			DatabaseInterface db = new DatabaseInterface();
			int success = 0;

			try {

				DataParameters[] parameters = new DataParameters[3];
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@lead_id";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = leadID;
				
				parameters[1] = new DataParameters();
				parameters[1].ParameterName = "@consultant_id";
				parameters[1].DataType = DbType.Int32;
				parameters[1].ParamDirection = ParameterDirection.Input;
				parameters[1].Value = consultantID;
				
				parameters[2] = new DataParameters();
				parameters[2].ParameterName = "@user_id";
				parameters[2].DataType = DbType.Int32;
				parameters[2].ParamDirection = ParameterDirection.Input;
				parameters[2].Value = unassignerID;
				
				

				string connStr = ConfigurationSettings.AppSettings["ConnectionString"];
				success = Convert.ToInt32(db.ExecuteNonQuery(CommandType.StoredProcedure,"crm_unassign_lead", parameters));

 			
				
				
			}catch (Exception ex) {
				throw new Global.CRMException("Error unassigning Lead: " + leadID + " from consultant:" + consultantID,ex,0,"DataBaseObjects.UnassignLead");
			}

			return(success);
		}


		public static DataTable GetAdjustmentReasons() {
			DatabaseInterface db = new DatabaseInterface();
			DataTable dt = new DataTable();

			try {

				DataParameters[] parameters = new DataParameters[0];
				string connStr = ConfigurationSettings.AppSettings["ConnectionString"];
				dt = db.ExecuteFetchDataTable("SELECT Reason.Reason_ID, Reason.Description FROM Reason WHERE Is_Active <> 0 ORDER BY Reason.Description", CommandType.Text, parameters);

 			
				
				
			}catch (Exception ex) {
				throw new Global.CRMException("",ex,0,"DataBaseObjects.GetAdjustmentReasons");
			}

			return(dt);
		}

		public static DataTable GetPaymentMethods() {
			DatabaseInterface db = new DatabaseInterface();
			DataTable dt = new DataTable();

			try {

				DataParameters[] parameters = new DataParameters[0];
				string connStr = ConfigurationSettings.AppSettings["ConnectionString"];
				dt = db.ExecuteFetchDataTable("select * from payment_method", CommandType.Text, parameters);

 			
				
				
			}catch (Exception ex) {
				throw new Global.CRMException("",ex,0,"DataBaseObjects.GetPaymentMethods");
			}

			return(dt);
		}


		
		public static DataTable GetLeadInfo(int leadID) {
			DatabaseInterface db = new DatabaseInterface();
			DataTable dt = new DataTable();

			try {

				DataParameters[] parameters = new DataParameters[1];
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@lead_id";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = leadID;

				string connStr = ConfigurationSettings.AppSettings["ConnectionString"];
				dt = db.ExecuteFetchDataTable("crm_get_lead_info", CommandType.StoredProcedure, parameters);

 							
				
			}catch (Exception ex) {
				throw new Global.CRMException("Lead:" + leadID,ex,0,"DataBaseObjects.GetLeadInfo");
			}

			return(dt);
		}


		public static DataTable GetLeadComments(int leadID) {
			DatabaseInterface db = new DatabaseInterface();
			DataTable dt = new DataTable();

			try {

				DataParameters[] parameters = new DataParameters[1];
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@lead_id";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = leadID;

				string connStr = ConfigurationSettings.AppSettings["ConnectionString"];
				dt = db.ExecuteFetchDataTable("crm_get_lead_comments", CommandType.StoredProcedure, parameters);

 							
				
			}catch (Exception ex) {
				throw new Global.CRMException("Lead:" + leadID,ex,0,"DataBaseObjects.GetLeadComments");
			}

			return(dt);
		}
		

		public static DataTable GetAllAccountsForLead(int leadID) {
			DatabaseInterface db = new DatabaseInterface();
			DataTable dt = new DataTable();

			try {

				DataParameters[] parameters = new DataParameters[1];
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@lead_id";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = leadID;

				string connStr = ConfigurationSettings.AppSettings["ConnectionString"];
				dt = db.ExecuteFetchDataTable("crm_get_all_accounts_for_lead", CommandType.StoredProcedure, parameters);

 							
				
			}catch (Exception ex) {
				throw new Global.CRMException("Lead:" + leadID,ex,0,"DataBaseObjects.GetAllAccountsForLead");
			}

			return(dt);
		}

		public static int Insert_Update_Payment(int salesID, int paymentNo, int paymentMethodID, int
			                      collectionStatusID, DateTime paymentDate, DateTime cashableDate,
			                     string creditCardNo, string expDate, string nameOnCard,
		                         string authorizationNo, decimal paymentAmt, int commission, bool newPayment){
			                     
			DatabaseInterface db = new DatabaseInterface();
			int result = 0;

			try {


				DataParameters[] parameters = new DataParameters[12];
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@intSalesID";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = salesID;
				
				parameters[1] = new DataParameters();
				parameters[1].ParameterName = "@intPaymentNo";
				parameters[1].DataType = DbType.Int32;
				parameters[1].ParamDirection = ParameterDirection.Input;
				parameters[1].Value = paymentNo;
				
				parameters[2] = new DataParameters();
				parameters[2].ParameterName = "@intPaymentMethodID";
				parameters[2].DataType = DbType.Int32;
				parameters[2].ParamDirection = ParameterDirection.Input;
				parameters[2].Value = paymentMethodID;
				
				parameters[3] = new DataParameters();
				parameters[3].ParameterName = "@intCollectionStatusID";
				parameters[3].DataType = DbType.Int32;
				parameters[3].ParamDirection = ParameterDirection.Input;
				if (collectionStatusID == 0){
					parameters[3].Value = DBNull.Value;
				}else{
				   parameters[3].Value = collectionStatusID;
				}
				
				
				parameters[4] = new DataParameters();
				parameters[4].ParameterName = "@datePaymentDate";
				parameters[4].DataType = DbType.DateTime;
				parameters[4].ParamDirection = ParameterDirection.Input;
				parameters[4].Value = paymentDate;
				
				parameters[5] = new DataParameters();
				parameters[5].ParameterName = "@dateCashableDate";
				parameters[5].DataType = DbType.DateTime;
				parameters[5].ParamDirection = ParameterDirection.Input;
				parameters[5].Value = cashableDate;
				
				parameters[6] = new DataParameters();
				parameters[6].ParameterName = "@strCreditCardNo";
				parameters[6].DataType = DbType.String;
				parameters[6].ParamDirection = ParameterDirection.Input;
				parameters[6].Value = creditCardNo;
				
				parameters[7] = new DataParameters();
				parameters[7].ParameterName = "@strExpDate";
				parameters[7].DataType = DbType.String;
				parameters[7].ParamDirection = ParameterDirection.Input;
				parameters[7].Value = expDate;
				
				parameters[8] = new DataParameters();
				parameters[8].ParameterName = "@strNameOnCard";
				parameters[8].DataType = DbType.String;
				parameters[8].ParamDirection = ParameterDirection.Input;
				parameters[8].Value = nameOnCard;
				
				parameters[9] = new DataParameters();
				parameters[9].ParameterName = "@strAuthorizationNo";
				parameters[9].DataType = DbType.String;
				parameters[9].ParamDirection = ParameterDirection.Input;
				parameters[9].Value = authorizationNo;
				
				parameters[10] = new DataParameters();
				parameters[10].ParameterName = "@decPaymentAMT";
				parameters[10].DataType = DbType.Decimal;
				parameters[10].ParamDirection = ParameterDirection.Input;
				parameters[10].Value = paymentAmt;
				
				parameters[11] = new DataParameters();
				parameters[11].ParameterName = "@bitCommission";
				parameters[11].DataType = DbType.Boolean;
				parameters[11].ParamDirection = ParameterDirection.Input;
				parameters[11].Value = commission;
					
				

				/////////////
				///

				
				string connStr = ConfigurationSettings.AppSettings["ConnectionString"];

				if (newPayment){
					result = Convert.ToInt32(db.ExecuteScalar(CommandType.StoredProcedure,"crm_insert_payment",  parameters));
				}else{
					result = Convert.ToInt32(db.ExecuteScalar(CommandType.StoredProcedure,"crm_update_payment",  parameters));
				}
				

 			
				
				
			}catch (Exception ex) {
				Debug.Write(ex.Message.ToString());
				//throw new AppExceptions(ex.Message, ex);
			}

			return(result);
		}


		public static int Insert_Update_Adjustment(int salesID, int adjustmentNo, int reasonID,
			        DateTime adjustmentDate, decimal AdjustmentOnSaleAmt, decimal AdjustmentOnShipping,
			        decimal AdjustmentAmt, string comment, bool newAdj){

			
			DatabaseInterface db = new DatabaseInterface();
			int result = 0;

			try {


				DataParameters[] parameters = new DataParameters[8];
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@intSalesID";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = salesID;
				
				parameters[1] = new DataParameters();
				parameters[1].ParameterName = "@intAdjustmentNo";
				parameters[1].DataType = DbType.Int32;
				parameters[1].ParamDirection = ParameterDirection.Input;
				parameters[1].Value = adjustmentNo;
				
				parameters[2] = new DataParameters();
				parameters[2].ParameterName = "@intReasonID";
				parameters[2].DataType = DbType.Int32;
				parameters[2].ParamDirection = ParameterDirection.Input;
				parameters[2].Value = reasonID;
				

				parameters[3] = new DataParameters();
				parameters[3].ParameterName = "@dateAdjustmentDate";
				parameters[3].DataType = DbType.DateTime;
				parameters[3].ParamDirection = ParameterDirection.Input;
				parameters[3].Value = adjustmentDate;
								
				
				parameters[4] = new DataParameters();
				parameters[4].ParameterName = "@decAdjustmentOnSalesAmt";
				parameters[4].DataType = DbType.Decimal;
				parameters[4].ParamDirection = ParameterDirection.Input;
				parameters[4].Value = AdjustmentOnSaleAmt;
				
				parameters[5] = new DataParameters();
				parameters[5].ParameterName = "@decAdjustmentOnShipping";
				parameters[5].DataType = DbType.Decimal;
				parameters[5].ParamDirection = ParameterDirection.Input;
				parameters[5].Value = AdjustmentOnShipping;
				
				parameters[6] = new DataParameters();
				parameters[6].ParameterName = "@decAdjustmentAmt";
				parameters[6].DataType = DbType.Decimal;
				parameters[6].ParamDirection = ParameterDirection.Input;
				parameters[6].Value = AdjustmentAmt;
				
				parameters[7] = new DataParameters();
				parameters[7].ParameterName = "@strComment";
				parameters[7].DataType = DbType.String;
				parameters[7].ParamDirection = ParameterDirection.Input;
				parameters[7].Value = comment;
				
			
				/////////////
				///

				
				string connStr = ConfigurationSettings.AppSettings["ConnectionString"];

				if (newAdj){
					result = Convert.ToInt32(db.ExecuteScalar(CommandType.StoredProcedure,"crm_insert_adjustment",  parameters));
				}else{
					result = Convert.ToInt32(db.ExecuteScalar(CommandType.StoredProcedure,"crm_update_adjustment",  parameters));
				}
				

 			
				
				
			}catch (Exception ex) {
				Debug.Write(ex.Message.ToString());
				//throw new AppExceptions(ex.Message, ex);
			}

			return(result);
		}


	}




}
