using System;
using Business.Reports;
using Business.Objects;
using QSPFulfillment.CommonWeb;

namespace QSPFulfillment.CustomerService
{
	/// <summary>
	/// Summary description for LetterBatchGenerationControl.
	/// </summary>
	public class LetterTemplateGenerationControl : CustomerServiceControl
	{
		private LetterTemplateItem selectedTemplate = null;
		private RSGeneration letterBatchReportControl = null;

		public virtual LetterTemplateItem SelectedTemplate
		{
			get 
			{
				return selectedTemplate;
			}
			set 
			{
				selectedTemplate = value;
			}
		}

		#region Fields

		public virtual int RunID 
		{
			get 
			{
				throw new NotImplementedException("RunID");
			}
			set 
			{
				throw new NotImplementedException("RunID");
			}
		}

		public virtual DateTime DateFrom 
		{
			get 
			{
				throw new NotImplementedException("DateFrom");
			}
			set 
			{
				throw new NotImplementedException("DateFrom");
			}
		}

		public virtual DateTime DateTo 
		{
			get 
			{
				throw new NotImplementedException("DateTo");
			}
			set 
			{
				throw new NotImplementedException("DateTo");
			}
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

		public virtual void PreviewBatch()
		{
			LetterBatch letterBatch = LetterBatchFactory.Instance.GetLetterBatch(SelectedTemplate.ExtendedTable);
			LetterBatchItem letterBatchItem = GetLetterBatchItem();

			letterBatch.ValidateGenerate(letterBatchItem);

			LetterBatchReportParameterCollection letterBatchReportParameterCollection = LetterBatchReportParameterCollectionFactory.Instance.GetLetterBatchReportParameterCollection(SelectedTemplate.ExtendedTable, letterBatchItem);
			LetterBatchReportControl.Generate(SelectedTemplate.ReportName, letterBatchReportParameterCollection);
		}

		public virtual int GenerateBatch() 
		{
			int letterBatchID = 0;
			LetterBatch letterBatch = LetterBatchFactory.Instance.GetLetterBatch(SelectedTemplate.ExtendedTable);
			LetterBatchItem letterBatchItem = GetLetterBatchItem();
			LetterBatchReportParameterCollection letterBatchReportParameterCollection = null;

			letterBatchID = letterBatch.Generate(letterBatchItem);

			letterBatchReportParameterCollection = new LetterBatchReportParameterCollection(letterBatchID);
			LetterBatchReportControl.Generate(SelectedTemplate.ReportName, letterBatchReportParameterCollection);

			return letterBatchID;
		}

		protected virtual LetterBatchItem GetLetterBatchItem() 
		{
			LetterBatchItem letterBatchItem = LetterBatchItemFactory.Instance.GetLetterBatchItem(SelectedTemplate.ExtendedTable, SelectedTemplate.ID, Convert.ToInt32(this.Page.UserID));

			letterBatchItem.DateFrom = DateFrom;
			letterBatchItem.DateTo = DateTo;
			letterBatchItem.RunID = RunID;

			return letterBatchItem;
		}

		public virtual void CopyTo(LetterTemplateGenerationControl control) 
		{
			control.RunID = this.RunID;
			control.DateFrom = this.DateFrom;
			control.DateTo = this.DateTo;
		}
	}
}
