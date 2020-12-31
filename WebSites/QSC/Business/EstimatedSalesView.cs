using System;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.ComponentModel;
using DAL;

namespace Business
{
	/// <summary>
	/// Summary description for EstimatedOrderView.
	/// </summary>
	public class EstimatedSalesView
	{
		protected EstimatedSalesDataAccess aTable;

	    protected int program_id;
		[DAL.DataColumn("p_program_id")]
		public int p_program_id
		{
			get{ return this.program_id; }
			set{ this.program_id = value; }
		}   

		protected int status_instance;
		[DAL.DataColumn("p_status_instance")]
		public int p_status_instance
		{
			get{ return this.status_instance; }
			set{ this.status_instance = value; }
		}   

		protected string fm_id;
		[DAL.DataColumn("p_fm_id")]
		public string p_fm_id
		{
			get{ return this.fm_id; }
			set{ this.fm_id = value; }
		}   

		protected string from_date;
		[DAL.DataColumn("p_from_date")]
		public string p_from_date
		{
			get{ return this.from_date; }
			set{ this.from_date = value; }
		}

		protected string to_date;
		[DAL.DataColumn("p_to_date")]
		public string p_to_date
		{
			get{ return this.to_date; }
			set{ this.to_date = value; }
		}


		public EstimatedSalesView()
		{
			try
			{
               
				aTable = new EstimatedSalesDataAccess();

			}
			catch(COMException e)
			{
				int x = e.ErrorCode;
			}
		}

		//public string FetchProgram(int campaign_id)
		//{
		//	return "nothing";	
		//}

		public DataTable fetch_batch_info()
		{
			return aTable.fetch_batch_info(program_id,status_instance,fm_id,from_date,to_date);
		}


	}
}
