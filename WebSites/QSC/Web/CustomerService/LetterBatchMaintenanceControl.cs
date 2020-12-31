using System;
using System.Web.UI.WebControls;
using Business.Reports;
using Business.Objects;
using QSPFulfillment.CommonWeb;
using Common;

namespace QSPFulfillment.CustomerService
{
	/// <summary>
	/// Summary description for LetterBatchGenerationControl.
	/// </summary>
	public class LetterBatchMaintenanceControl : CustomerServiceControl
	{
		private DataGrid dtgMain = null;
		private RSGeneration letterBatchReportControl = null;

		public event SelectedTemplateChangedEventHandler SelectedTemplateChanged;

		protected virtual string ExtendedTable
		{
			get 
			{
				throw new NotImplementedException("extendedTable");
			}
		}

		#region Events

		protected virtual void OnInit(EventArgs e, DataGrid dtgMain)
		{
			this.dtgMain = dtgMain;

			this.dtgMain.ItemCommand += new DataGridCommandEventHandler(dtgMain_ItemCommand);

			this.dtgMain.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgMain_PageIndexChanged);

			base.OnInit(e);
		}

		protected virtual void OnSelectedTemplateChanged(SelectedTemplateChangedArgs e) 
		{
			if(SelectedTemplateChanged != null) 
			{
				SelectedTemplateChanged(this, e);
			}
		}

		private void dtgMain_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				switch(e.CommandName)
				{
					case "Reprint" : 
					{
						PrintReport(GetReportName(e.Item), GetID(e.Item));
						break;
					}
					case "Cancel" : 
					{
						CancelBatch(GetID(e.Item));
						DataBind();
						break;
					}
					case "MarkPrinted" : 
					{
						MarkBatchPrinted(GetLetterBatchItem(e.Item));
						DataBind();
						break;
					}
					case "CloseBatch" : 
					{
						CloseBatch(GetLetterBatchItem(e.Item));
						DataBind();
						break;
					}
				}
			}
			catch(Exception ex)
			{
				this.Page.ManageError(ex);
			}
		}

		private void dtgMain_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			dtgMain.SelectedIndex = -1;
			dtgMain.CurrentPageIndex = e.NewPageIndex;
			DataBind();
		}

		#endregion

		#region Controls

		public virtual RSGeneration LetterBatchReportControl 
		{
			get 
			{
				return letterBatchReportControl;
			}
			set 
			{
				letterBatchReportControl = value;
			}
		}

		#endregion

		#region Fields

		protected virtual int LetterTemplateIDSearch 
		{
			get 
			{
				throw new NotImplementedException("LetterTemplateIDSearch");
			}
			set 
			{
				throw new NotImplementedException("LetterTemplateIDSearch");
			}
		}

		protected virtual DateTime DateCreatedFromSearch 
		{
			get 
			{
				throw new NotImplementedException("DateCreatedFromSearch");
			}
			set 
			{
				throw new NotImplementedException("DateCreatedFromSearch");
			}
		}

		protected virtual DateTime DateCreatedToSearch 
		{
			get 
			{
				throw new NotImplementedException("DateCreatedToSearch");
			}
			set 
			{
				throw new NotImplementedException("DateCreatedToSearch");
			}
		}

		protected virtual LetterBatchType LetterBatchTypeSearch 
		{
			get 
			{
				throw new NotImplementedException("LetterBatchTypeSearch");
			}
			set 
			{
				throw new NotImplementedException("LetterBatchTypeSearch");
			}
		}

		protected virtual DateTime DateFromSearch 
		{
			get 
			{
				throw new NotImplementedException("DateFromSearch");
			}
			set 
			{
				throw new NotImplementedException("DateFromSearch");
			}
		}

		protected virtual DateTime DateToSearch 
		{
			get 
			{
				throw new NotImplementedException("DateToSearch");
			}
			set 
			{
				throw new NotImplementedException("DateToSearch");
			}
		}

		protected virtual int RunIDFromSearch
		{
			get 
			{
				throw new NotImplementedException("RunIDFromSearch");
			}
			set 
			{
				throw new NotImplementedException("RunIDFromSearch");
			}
		}

		protected virtual int RunIDToSearch
		{
			get 
			{
				throw new NotImplementedException("RunIDToSearch");
			}
			set 
			{
				throw new NotImplementedException("RunIDToSearch");
			}
		}

		protected virtual BooleanNullable IsPrintedSearch 
		{
			get 
			{
				throw new NotImplementedException("IsPrintedSearch");
			}
			set 
			{
				throw new NotImplementedException("IsPrintedSearch");
			}
		}

		protected virtual BooleanNullable IsLockedSearch 
		{
			get 
			{
				throw new NotImplementedException("IsLockedSearch");
			}
			set 
			{
				throw new NotImplementedException("IsLockedSearch");
			}
		}

		#endregion

		public virtual void DataBindStatelessData() { }

		public virtual void DataBindInitialData() { }

		public override void DataBind()
		{
			LetterBatch letterBatch = LoadData();
			this.dtgMain.DataSource = letterBatch.dataSet;
			this.dtgMain.DataMember = letterBatch.dataSet.LetterBatch.TableName;
			this.dtgMain.DataBind();
		}

		protected virtual LetterBatch LoadData() 
		{
			LetterBatch letterBatch = new LetterBatch();

			letterBatch.GetAll(GetLetterBatchSearchCriteria());

			return letterBatch;
		}

		protected virtual LetterBatchSearchCriteria GetLetterBatchSearchCriteria() 
		{
			LetterBatchSearchCriteria letterBatchSearchCriteria = new LetterBatchSearchCriteria();
			
			letterBatchSearchCriteria.fill(LetterTemplateIDSearch, DateCreatedFromSearch, DateCreatedToSearch, LetterBatchTypeSearch, DateFromSearch, DateToSearch, RunIDFromSearch, RunIDToSearch, IsPrintedSearch, IsLockedSearch);
			
			return letterBatchSearchCriteria;
		}

		protected virtual void PrintReport(string reportName, int letterBatchID) 
		{
			LetterBatchReportControl.Generate(reportName, new LetterBatchReportParameterCollection(letterBatchID));
		}

		protected virtual void CancelBatch(int letterBatchID) 
		{
			LetterBatch letterBatch = new LetterBatch();

			letterBatch.Cancel(letterBatchID);
		}

		protected virtual void MarkBatchPrinted(LetterBatchItem letterBatchItem) 
		{
			LetterBatch letterBatch = new LetterBatch();

			letterBatchItem.IsPrinted = true;
			letterBatchItem.DatePrinted = DateTime.Now;

			letterBatch.Update(letterBatchItem);
		}

		protected virtual void CloseBatch(LetterBatchItem letterBatchItem) 
		{
			LetterBatch letterBatch = new LetterBatch();

			letterBatchItem.IsLocked = true;

			letterBatch.Update(letterBatchItem);
		}

		protected virtual string GetReportName(DataGridItem e) 
		{
			return ((Label) e.FindControl("lblReportName")).Text;
		}

		protected virtual LetterBatchItem GetLetterBatchItem(DataGridItem e) 
		{
			return LetterBatchItemFactory.Instance.GetLetterBatchItem(null, GetID(e), GetLetterTemplateID(e), GetDateFrom(e), GetDateTo(e), GetRunID(e), GetIsPrinted(e), GetDatePrinted(e), GetIsLocked(e), GetUserIDCreated(e), GetDateCreated(e), GetDeletedTF(e));
		}

		protected virtual int GetID(DataGridItem e) 
		{
			return Convert.ToInt32(((Label) e.FindControl("lblID")).Text);
		}

		protected virtual int GetLetterTemplateID(DataGridItem e) 
		{
			return Convert.ToInt32(((Label) e.FindControl("lblLetterTemplateID")).Text);
		}

		protected virtual LetterBatchType GetLetterBatchType(DataGridItem e) 
		{
			return (LetterBatchType) Convert.ToInt32(((Label) e.FindControl("lblLetterBatchType")).Text);
		}

		protected virtual DateTime GetDateFrom(DataGridItem e) 
		{
			return Convert.ToDateTime(((Label) e.FindControl("lblDateFrom")).Text);
		}

		protected virtual DateTime GetDateTo(DataGridItem e) 
		{
			return Convert.ToDateTime(((Label) e.FindControl("lblDateTo")).Text);
		}

		protected virtual int GetRunID(DataGridItem e) 
		{
			return Convert.ToInt32(((Label) e.FindControl("lblRunID")).Text);
		}

		protected virtual bool GetIsPrinted(DataGridItem e) 
		{
			return Convert.ToBoolean(((Label) e.FindControl("lblIsPrinted")).Text);
		}

		protected virtual DateTime GetDatePrinted(DataGridItem e) 
		{
			return Convert.ToDateTime(((Label) e.FindControl("lblDatePrinted")).Text);
		}

		protected virtual bool GetIsLocked(DataGridItem e) 
		{
			return Convert.ToBoolean(((Label) e.FindControl("lblIsLocked")).Text);
		}

		protected virtual int GetUserIDCreated(DataGridItem e) 
		{
			return Convert.ToInt32(((Label) e.FindControl("lblUserIDCreated")).Text);
		}

		protected virtual DateTime GetDateCreated(DataGridItem e) 
		{
			return Convert.ToDateTime(((Label) e.FindControl("lblDateCreated")).Text);
		}

		protected virtual bool GetDeletedTF(DataGridItem e) 
		{
			return Convert.ToBoolean(((Label) e.FindControl("lblDeletedTF")).Text);
		}

		public virtual void CopyTo(LetterBatchMaintenanceControl control) 
		{
			control.LetterTemplateIDSearch = this.LetterTemplateIDSearch;
			control.DateCreatedFromSearch = this.DateCreatedFromSearch;
			control.DateCreatedToSearch = this.DateCreatedToSearch;
			control.LetterBatchTypeSearch = this.LetterBatchTypeSearch;
			control.DateFromSearch = this.DateFromSearch;
			control.DateToSearch = this.DateToSearch;
			control.RunIDFromSearch = this.RunIDFromSearch;
			control.RunIDToSearch = this.RunIDToSearch;
			control.IsPrintedSearch = this.IsPrintedSearch;
			control.IsLockedSearch = this.IsLockedSearch;
		}
	}
}
