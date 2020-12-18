using System;
using System.Data;
using System.Configuration;
using System.Threading;
using GA.BDC.Core.EnterpriseComponents;


namespace CRMWeb.Classes
{
	/// <summary>
	/// Summary description for BusinessProcess.
	/// </summary>
	public class BusinessProcess
	{
		public BusinessProcess()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		public void ProcessFlagpoles(int leadID){
		///like processFlagpole(dt) but doesnt care if assigned or unassigned
            DataTable dt =  DatabaseObjects.GetFMInfoForLead(leadID);
			if (dt.Rows.Count > 0){
				ProcessFlagpoles(dt);
			}


		}

		public void ProcessGirlsScout()
		{
			DataTable dt =  DatabaseObjects.GetGirlsScoutLead();
			for(int i=0;i<dt.Rows.Count;i++)
			{
				int leadID = Convert.ToInt32(dt.Rows[i]["lead_id"]);
				int success = DatabaseObjects.AssignLead(leadID, 9003, 9000, 0,true);
			
				if (success == 0)
				{
			 
					SendEmail se = new SendEmail();
					se.REPLY = "Dispatcher@rd.com";
					se.FROM = "Dispatcher"; 
					se.TO = ConfigurationSettings.AppSettings["Assignment_Email"];
							
					se.BuildGirlScoutConfirmationEmail(leadID);
								
					//se.Send();
					Thread t = new Thread(new ThreadStart(se.Send));
					t.Priority = ThreadPriority.Lowest;
					t.Start();
				}

			}
				  
			 

		}

		public void ProcessFlagpoles(DataTable dt){
			try{

				string process = ConfigurationSettings.AppSettings["ProcessFlagPoles"].ToString();
				if (process == "true"){

					int nbAccounts, consultantID, leadID, nbFM;
					bool isActive;
					string email, name;

		
					for(int i=0;i<dt.Rows.Count;i++){
						consultantID = 0;
						isActive = false;
						email = "";
						name = "";

						nbAccounts = Convert.ToInt32(dt.Rows[i]["fm_nb_account"]);
						nbFM = Convert.ToInt32(dt.Rows[i]["nb_fm"]);
						if (dt.Rows[i]["is_active"] != DBNull.Value){
							isActive =  Convert.ToBoolean(dt.Rows[i]["is_active"]);
						}
				
						if (dt.Rows[i]["consultant_id"] != DBNull.Value){
							consultantID = Convert.ToInt32(dt.Rows[i]["consultant_id"]);
						}
				
						if (dt.Rows[i]["email_address"] != DBNull.Value){
							email = dt.Rows[i]["email_address"].ToString();
						}
				
						leadID = Convert.ToInt32(dt.Rows[i]["lead_ID"]);
						name = dt.Rows[i]["name"].ToString();



						SendEmail se = new SendEmail();
						se.REPLY = "Dispatcher@rd.com";
						se.FROM = "Dispatcher"; 
						se.TO = ConfigurationSettings.AppSettings["Assignment_Email"];

				
						//assign to fm
						
						if (nbFM == 1 && isActive && Helper.IsValidEmail(email)){
							int success = DatabaseObjects.AssignLead(leadID, consultantID, 9000, 10, true);
         
						}else{
							int success = DatabaseObjects.AssignLead(leadID, 9000, 9000, 10, true);
						}

						
						se.BuildConfirmationEmail(leadID);
								
						//se.Send();
						Thread t = new Thread(new ThreadStart(se.Send));
						t.Priority = ThreadPriority.Lowest;
						t.Start();
				 
						
					}
				}
			}catch(Exception ex){
				throw new Global.CRMException("",ex,0,"BusinessProcess.ProcessFlagPoles");
			}
		
		}
	}
}
