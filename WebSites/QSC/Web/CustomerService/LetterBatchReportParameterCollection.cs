using System;
using QSPFulfillment.CommonWeb;
using Business.Objects;

namespace QSPFulfillment.CustomerService
{
	/// <summary>
	/// Summary description for LetterBatchReportParameterCollection.
	/// </summary>
	[Serializable]
	public class LetterBatchReportParameterCollection : ParameterValueCollection
	{
		public LetterBatchReportParameterCollection(int letterBatchID)
		{
			ParameterValue parameterValue;

			parameterValue = new ParameterValue();
			parameterValue.Name = "iLetterBatchID";
			parameterValue.Value = letterBatchID.ToString();
			this.Add(parameterValue);
		}

		public LetterBatchReportParameterCollection(LetterBatchItem letterBatchItem) : this(-1)
		{
			ParameterValue parameterValue;

			parameterValue = new ParameterValue();
			parameterValue.Name = "iLetterTemplateID";
			parameterValue.Value = letterBatchItem.LetterTemplateID.ToString();
			this.Add(parameterValue);

			if(letterBatchItem.LetterBatchType == LetterBatchType.DateRange) 
			{
				parameterValue = new ParameterValue();
				parameterValue.Name = "dDateFrom";
				parameterValue.Value = letterBatchItem.DateFrom.ToString();
				this.Add(parameterValue);
				
				parameterValue = new ParameterValue();
				parameterValue.Name = "dDateTo";
				parameterValue.Value = letterBatchItem.DateTo.ToString();
				this.Add(parameterValue);
			} 
			else if(letterBatchItem.LetterBatchType == LetterBatchType.RemitBatchID) 
			{
				parameterValue = new ParameterValue();
				parameterValue.Name = "iRunID";
				parameterValue.Value = letterBatchItem.RunID.ToString();
				this.Add(parameterValue);
			}
		}

		public LetterBatchReportParameterCollection(LetterBatchItem letterBatchItem, int customerOrderHeaderInstance, int transID) : this(letterBatchItem)
		{
			ParameterValue parameterValue;

			parameterValue = new ParameterValue();
			parameterValue.Name = "iCustomerOrderHeaderInstance";
			parameterValue.Value = customerOrderHeaderInstance.ToString();
			this.Add(parameterValue);

			parameterValue = new ParameterValue();
			parameterValue.Name = "iTransID";
			parameterValue.Value = transID.ToString();
			this.Add(parameterValue);
		}
	}
}
