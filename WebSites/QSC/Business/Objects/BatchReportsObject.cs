using System;
using System.Data;
using DAL;
using Common;
using FileStore;
using Business.Reports;
using Business.ReportExecution;

namespace Business.Objects
{
	/// <summary>
	/// Summary description for BatchReportsObject.
	/// </summary>
	public abstract class BatchReportsObject : MarshalByRefObject
	{
		private Business.Reports.Report[] oReports = null;
		private Message messageManager;
		private PDFStore oPDFStore;
        public bool includeOEFUReport = false;  

		public BatchReportsObject() : base()
		{
			try 
			{
				InitializeReports();
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}
		}

        public BatchReportsObject(bool IncludeOEFUReport)
            : base()
        {
            try
            {
                includeOEFUReport = IncludeOEFUReport;
                InitializeReports();
            }
            catch (Exception ex)
            {
                ManageError(ex);
            }
        }

		protected abstract DataSet baseDataSet
		{
			get;
		}

		internal abstract string DefaultTableName 
		{
			get;
		}

		protected abstract string FileNameExpression 
		{
			get;
		}

		protected virtual Business.Reports.Report[] Reports 
		{
			get 
			{
				return oReports;
			}
			set 
			{
				oReports = value;
			}
		}

		protected Message CurrentMessageManager
		{
			get
			{
				if(messageManager == null)
					messageManager = new Message(false);

				return messageManager;
			}
		}

		protected abstract DBTableOperation DataAccessReference
		{
			get;
		}

		protected abstract void InitializeReports();

		protected abstract void GetDataList();

		public virtual byte[] Generate() 
		{
			byte[] bPDFReportFile = null;

			try 
			{
				oPDFStore = new PDFStore();

				PrepareDataSet(true);

				if(baseDataSet.Tables[DefaultTableName].Rows.Count == 0) 
				{
					this.CurrentMessageManager.Add(Message.ERRMSG_BATCH_REPORT_EMPTY_LIST_0);
					this.CurrentMessageManager.PrepareErrorMessage();

					throw new MessageException(this.CurrentMessageManager);
				}

				bPDFReportFile = GenerateReports();
			} 
			catch(Exception ex)
			{
				ManageError(ex);
			}

			return bPDFReportFile;
		}

		public virtual byte[] Generate(bool getData) 
		{
			byte[] bPDFReportFile = null;

			if(getData) 
			{
				bPDFReportFile = Generate();
			} 
			else 
			{
				try 
				{
					oPDFStore = new PDFStore();

					PrepareDataSet(getData);

					if(baseDataSet.Tables[DefaultTableName].Rows.Count == 0) 
					{
						this.CurrentMessageManager.Add(Message.ERRMSG_BATCH_REPORT_EMPTY_LIST_0);
						this.CurrentMessageManager.PrepareErrorMessage();

						throw new MessageException(this.CurrentMessageManager);
					}

					bPDFReportFile = GenerateReports();
				} 
				catch(Exception ex)
				{
					ManageError(ex);
				}
			}

			return bPDFReportFile;
		}

		private void PrepareDataSet(bool getData) 
		{
			DataColumn oFileNameExpressionColumn;

			if(getData) 
			{
				GetDataList();
			}
            
			if(!baseDataSet.Tables[DefaultTableName].Columns.Contains("FileNameExpression")) 
			{
				oFileNameExpressionColumn = new DataColumn("FileNameExpression", typeof(string), FileNameExpression);
				baseDataSet.Tables[DefaultTableName].Columns.Add(oFileNameExpressionColumn);
			}
		}

		private byte[] GenerateReports() 
		{
			byte[] bPDFReportFile = null;
			byte[] bPDFMergedReportFile = null;

			try 
			{
				for(int i = 0; i < baseDataSet.Tables[DefaultTableName].Rows.Count; i++) 
				{
					for(int j = 0; j < this.Reports.Length; j++) 
					{
						bPDFReportFile = this.Reports[j].Generate(baseDataSet.Tables[DefaultTableName].Rows[i]);

						if(bPDFReportFile != null) 
						{
							oPDFStore.Add(i.ToString().PadLeft(20, '0') + "_" + j.ToString().PadLeft(20, '0') + ".pdf", bPDFReportFile);
						}
					}
				}

				bPDFMergedReportFile = oPDFStore.Get(oPDFStore.Merge());
				oPDFStore.Close();
			} 
			catch(Exception ex) 
			{
				oPDFStore.Close();

				throw ex;
			}

			return bPDFMergedReportFile;
		}

		protected void ManageError(Exception ex)
		{

			if (ex is MessageException )
			{
				throw ex;
			}
			else
			{
				QSPFulfillment.DataAccess.Common.ApplicationError.ManageError(ex);
				CurrentMessageManager.SetSystemErrorMessage(Message.ERRMSG_SYSTEM_VAR_0);
				throw new MessageException(CurrentMessageManager, ex);
			}	
			
		}
		protected void ManageError(Common.MessageException ex)
		{			
			QSPFulfillment.DataAccess.Common.ApplicationError.ManageError(ex);	
		}
	}
}
