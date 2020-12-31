using System;
using QSPFulfillment.CommonWeb;
using Business.Objects;

namespace QSPFulfillment.CustomerService
{
	/// <summary>
	/// Summary description for InactiveMagazineLetterBatchReportParameterCollection.
	/// </summary>
 	[Serializable]
	public class InactiveMagazineLetterBatchReportParameterCollection : LetterBatchReportParameterCollection
	{
		public InactiveMagazineLetterBatchReportParameterCollection(LetterBatchItem letterBatchItem) : base(letterBatchItem)
		{
			InactiveMagazineLetterBatchItem inactiveMagazineLetterBatchItem = letterBatchItem as InactiveMagazineLetterBatchItem;

			ParameterValue parameterValue;

			parameterValue = new ParameterValue();
			parameterValue.Name = "sProductCode";
			parameterValue.Value = inactiveMagazineLetterBatchItem.ProductCode.ToString();
			this.Add(parameterValue);

			parameterValue = new ParameterValue();
			parameterValue.Name = "iReason";
			parameterValue.Value = inactiveMagazineLetterBatchItem.Reason.ToString();
			this.Add(parameterValue);
		}

		public InactiveMagazineLetterBatchReportParameterCollection(LetterBatchItem letterBatchItem, int customerOrderHeaderInstance, int transID) : this(letterBatchItem) {}

	}
}
