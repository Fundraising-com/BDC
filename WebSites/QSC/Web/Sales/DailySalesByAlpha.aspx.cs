using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace QSPFulfillment.Sales
{
	///<summary>DailySalesByAlpha.</summary>
	public partial class DailySalesByAlpha : QSPFulfillment.CommonWeb.QSPPage
	{
		#region auto-generated code
		///<summary>Required method for Designer support</summary>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		///<summary>Required method for Designer support</summary>
		private void InitializeComponent()
		{
			this.cnMags = new System.Data.SqlClient.SqlConnection();
			this.cnMags.ConnectionString = ConfigurationSettings.AppSettings["DSN"];
			this.grdCurYr.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.grdCurYr_ItemDataBound);
			this.grdPriorYr.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.grdPriorYr_ItemDataBound);
			this.grdTwoAgo.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.grdTwoAgo_ItemDataBound_1);
		}
		#endregion auto-generated code
		
		#region Item Declarations
		private int SubsCur = 0;
		private int SubsLastYr = 0;
		private int SubDiff = 0;

		protected System.Data.SqlClient.SqlConnection cnMags;
		protected System.Web.UI.WebControls.Panel pnlCur;
		protected System.Web.UI.WebControls.Panel pnlPrYr;
		protected System.Web.UI.WebControls.Panel pnlTwoAgo;

		string strID;
		private  double runningTotal = 0;
		private  double runningDolrs = 0;

		private  double runningTotalPrYr = 0;
		private  double runningDolrsPrYr = 0;

		private  double runningTotal2Yr = 0;
		protected System.Web.UI.WebControls.Label lblUmc;
		protected System.Web.UI.WebControls.Label lblProductName;
		protected System.Web.UI.WebControls.Label lblYr_1;
		protected System.Web.UI.WebControls.Label lblYr_2;
		protected System.Web.UI.WebControls.Label lblYr_3;
		protected System.Web.UI.WebControls.Label lblLastYrYTDSubs;
		protected System.Web.UI.WebControls.Label lblCurYrSubs;
		protected System.Web.UI.WebControls.Label lblMsg1;
		protected System.Web.UI.WebControls.Label lblToday;
		protected System.Web.UI.WebControls.Label lblMsg2;
		protected System.Web.UI.WebControls.Label lblSubDiff;
		private  double runningDolrs2Yr = 0;
		#endregion Item Declarations

		protected void Page_Load(object sender, System.EventArgs e)
		{
			strID = (string)Request.QueryString["RequestID"];
			// Get a valid code
			if (strID == null)
			{
				throw new ArgumentException(
					"This page expects a code value.");
			}
			else
			{

				SqlConnection cnMags = new SqlConnection (ConfigurationSettings.AppSettings["DSN"]);
				SqlCommand cmd;
				SqlDataReader reader;


				cmd = new SqlCommand("GetMagByAlphaCode", cnMags);
				cmd.CommandType = CommandType.StoredProcedure;

				// create the required parameters
				cmd.Parameters.Add(new SqlParameter("@ProductCode", SqlDbType.NVarChar,10));
				cmd.Parameters["@ProductCode"].Value = strID;

				//Initialize the adapters

				cnMags.Open();

				reader = cmd.ExecuteReader (CommandBehavior.CloseConnection);
				if (reader.Read () )
				{

				}
				try
				{
					//Fill the dataset
					this.lblUmc.Text = reader["product_code"].ToString ();
					this.lblProductName.Text = reader["product_name"].ToString ();
					this.lblYr_1.Text = reader["fiscalYear"].ToString ();
					this.lblYr_2.Text = reader["LastYr"].ToString ();
					this.lblYr_3.Text = reader["TwoYrsAgo"].ToString ();
				}
				catch
				{
					//just pass the exception up
					throw;
				}
				finally
				{
					//con.Close();
					reader.Close ();
				}

				SqlCommand cmd2;
				SqlDataReader reader2;


				cmd2 = new SqlCommand("GetLastYrSubsYTDbyAlpha", cnMags);
				cmd2.CommandType = CommandType.StoredProcedure;

				// create the required parameters
				cmd2.Parameters.Add(new SqlParameter("@ProductCode", SqlDbType.NVarChar,10));
				cmd2.Parameters["@ProductCode"].Value = strID;

				//Initialize the adapters

				cnMags.Open();

				reader2 = cmd2.ExecuteReader (CommandBehavior.CloseConnection);
				if (reader2.Read () )
				{

				}
				try
				{
					//Fill the dataset
					this.lblLastYrYTDSubs.Text = reader2["subs"].ToString ();
					SubsLastYr = Int32.Parse(reader2["subs"].ToString ());

					//this.lblCurYrSubs.Text = CurSubs;

				}
				catch
				{
					//just pass the exception up
					throw;
				}
				finally
				{
					//con.Close();
					reader2.Close ();
				}

				SqlCommand cmd3;
				SqlDataReader reader3;


				cmd3 = new SqlCommand("GetCurYrSubsYTDbyAlpha", cnMags);
				cmd3.CommandType = CommandType.StoredProcedure;

				// create the required parameters
				cmd3.Parameters.Add(new SqlParameter("@ProductCode", SqlDbType.NVarChar,10));
				cmd3.Parameters["@ProductCode"].Value = strID;

				//Initialize the adapters

				cnMags.Open();

				reader3 = cmd3.ExecuteReader (CommandBehavior.CloseConnection);
				if (reader3.Read () )
				{

				}
				try
				{
					//Fill the dataset
					this.lblCurYrSubs.Text = reader3["subs"].ToString ();
					this.lblToday.Text = reader3["Today"].ToString ();
					SubsCur = Int32.Parse(reader3["subs"].ToString ());

				}
				catch
				{
					//just pass the exception up
					throw;
				}
				finally
				{
					SubDiff = SubsCur - SubsLastYr;
					this.lblSubDiff.Text = String.Format("{0:#,###}", SubDiff);
					reader3.Close ();
				}




				//SqlConnection cnMags = new SqlConnection (ConfigurationSettings.AppSettings["cnMags.ConnectionString"]);
				SqlCommand cmdCurYr = new SqlCommand("GetWeeklySalesNumbersByAlphaCode", cnMags);
				cmdCurYr.CommandType = CommandType.StoredProcedure;
				cmdCurYr.Parameters.Add(new SqlParameter("@ProductCode", SqlDbType.NVarChar,10));
				cmdCurYr.Parameters["@ProductCode"].Value = strID;
				SqlDataAdapter daMags = new SqlDataAdapter();
				daMags.SelectCommand = cmdCurYr;
				DataSet dsMags = new DataSet ();

				daMags.Fill (dsMags, "Magazines");
				grdCurYr.DataSource = dsMags;
				grdCurYr.DataBind();


				SqlCommand cmdPriorYr = new SqlCommand("GetWeeklySalesNumbersByAlphaCodePriorYr", cnMags);
				cmdPriorYr.CommandType = CommandType.StoredProcedure;
				cmdPriorYr.Parameters.Add(new SqlParameter("@ProductCode", SqlDbType.NVarChar,10));
				cmdPriorYr.Parameters["@ProductCode"].Value = strID;
				SqlDataAdapter daPriorYr = new SqlDataAdapter();
				daPriorYr.SelectCommand = cmdPriorYr;
				DataSet dsPriorYr = new DataSet ();

				daPriorYr.Fill (dsPriorYr, "MagsPriorYr");
				grdPriorYr.DataSource = dsPriorYr;
				grdPriorYr.DataBind();

				SqlCommand cmdTwoAgo = new SqlCommand("GetWeeklySalesNumbersByAlphaCodeTwoYrsAgo", cnMags);
				cmdTwoAgo.CommandType = CommandType.StoredProcedure;
				cmdTwoAgo.Parameters.Add(new SqlParameter("@ProductCode", SqlDbType.NVarChar,10));
				cmdTwoAgo.Parameters["@ProductCode"].Value = strID;
				SqlDataAdapter daTwoAgo = new SqlDataAdapter();
				daTwoAgo.SelectCommand = cmdTwoAgo;
				DataSet dsTwoAgo = new DataSet ();

				daTwoAgo.Fill (dsTwoAgo, "MagsTwoAgo");
				grdTwoAgo.DataSource = dsTwoAgo;
				grdTwoAgo.DataBind();


				cnMags.Close();
			}


		}


		private void CalcTotal (string _subs)
		{
			try
			{
				runningTotal += Double.Parse(_subs);
			}
			catch
			{
			}
		}


		private void CalcTotalDolrs (string _dols)
		{
			try
			{
				runningDolrs += Double.Parse(_dols);
			}
			catch
			{
			}
		}
		private void CalcTotalPrYr (string _subs)
		{
			try
			{
				runningTotalPrYr += Double.Parse(_subs);
			}
			catch
			{
			}
		}

		private void CalcTotalDolrsPrYr (string _dols)
		{
			try
			{
				runningDolrsPrYr += Double.Parse(_dols);
			}
			catch
			{
			}
		}

		private void CalcTotal2Yr (string _subs)
		{
			try
			{
				runningTotal2Yr += Double.Parse(_subs);
			}
			catch
			{
			}
		}

		private void CalcTotalDolrs2Yr (string _dols)
		{
			try
			{
				runningDolrs2Yr += Double.Parse(_dols);
			}
			catch
			{
			}
		}


		private void grdCurYr_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)

		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				CalcTotal( e.Item.Cells[1].Text );
				e.Item.Cells[1].Text = string.Format("{0:#,###}", Convert.ToDouble(e.Item.Cells[1].Text));
				CalcTotalDolrs ( e.Item.Cells[2].Text );
				e.Item.Cells[2].Text = string.Format("{0:c}", Convert.ToDouble(e.Item.Cells[2].Text));

			}
			else if(e.Item.ItemType == ListItemType.Footer )
			{
				e.Item.Cells[0].Text="Totals";
				e.Item.Cells[1].Text = string.Format("{0:#,###}", runningTotal);
				e.Item.Cells[2].Text= string.Format("{0:c}", runningDolrs);

			}
		}


		private void grdPriorYr_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)

		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				CalcTotalPrYr ( e.Item.Cells[1].Text );
				e.Item.Cells[1].Text = string.Format("{0:#,###}", Convert.ToDouble(e.Item.Cells[1].Text));
				CalcTotalDolrsPrYr ( e.Item.Cells[2].Text );
				e.Item.Cells[2].Text = string.Format("{0:c}", Convert.ToDouble(e.Item.Cells[2].Text));

			}
			else if(e.Item.ItemType == ListItemType.Footer )
			{
				e.Item.Cells[1].Text= string.Format("{0:#,###}", runningTotalPrYr);
				e.Item.Cells[2].Text= string.Format("{0:c}", runningDolrsPrYr);
			}

		}



		private void grdTwoAgo_ItemDataBound_1(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				CalcTotal2Yr ( e.Item.Cells[1].Text );
				e.Item.Cells[1].Text = string.Format("{0:#,###}", Convert.ToDouble(e.Item.Cells[1].Text));
				CalcTotalDolrs2Yr ( e.Item.Cells[2].Text );
				e.Item.Cells[2].Text = string.Format("{0:c}", Convert.ToDouble(e.Item.Cells[2].Text));

			}
			else if(e.Item.ItemType == ListItemType.Footer )
			{
				e.Item.Cells[1].Text= string.Format("{0:#,###}", runningTotal2Yr);
				e.Item.Cells[2].Text= string.Format("{0:c}", runningDolrs2Yr);
			}

		}

	}
}
