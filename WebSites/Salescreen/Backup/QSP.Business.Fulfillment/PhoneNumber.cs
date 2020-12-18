using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Expression;
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;


namespace QSP.Business.Fulfillment
{
    public partial class PhoneNumber
    {
        #region Methods

        public static List<PhoneNumber> FindPhoneNumber(string phoneNumber)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(PhoneNumber));
                c.Add(Expression.Eq(Phone_NumberProperty, phoneNumber));
                c.Add(Expression.Eq(DeletedProperty, false));

                return (List<PhoneNumber>)c.List<PhoneNumber>();
            }

            return new List<PhoneNumber>();
        }

        public static int GetPhoneNumberID(string phoneNumber){
           // ApplicationSettings.GetConfig()["EFundraisingProd.SqlConnection.Release", "connectionString"]); }
           

            //NEED TO CHANGE THIS> ALWAY FALSE

            int isProd = Convert.ToInt32(System.Web.HttpContext.Current.Session["isProd"]);
            string connectionString = "";
            if (isProd == 0)
            {
               connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["QSPFulfillmentConnectionStringDEV"].ConnectionString;
            }
            else
            {
                connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["QSPFulfillmentConnectionString"].ConnectionString;
            }
            
            

            int phoneNumberId = 0;

            string storedProcName = "pr_phone_number_id_select";
            
            
            SqlConnection conn = new SqlConnection(connectionString);
            try{
                SqlCommand cmd = new SqlCommand(storedProcName, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@phone_number", phoneNumber);
                conn.Open();
                
                // Use DataAdapter to fill dataset
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt != null && dt.Rows.Count > 0)
				{
                    phoneNumberId = Convert.ToInt32(dt.Rows[0]["phone_number_id"]);
				}

			}
			finally
			{
				conn.Close();
			}
			return phoneNumberId;
		}



        #endregion
    }
}
