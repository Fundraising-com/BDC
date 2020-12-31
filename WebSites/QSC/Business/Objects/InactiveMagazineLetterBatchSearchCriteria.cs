using System;
using Common.TableDef;
using Common;

namespace Business.Objects
{
	/// <summary>
	/// Summary description for InactiveMagazineLetterBatchSearchCriteria.
	/// </summary>
	public class InactiveMagazineLetterBatchSearchCriteria : LetterBatchSearchCriteria
	{
		private int letterBatchID;
		private string productCode;
		private int reason;

		public InactiveMagazineLetterBatchSearchCriteria()
		{
			//return InactiveMagazineLetterBatchSearchCriteria;
		} 

		public void fill(int LetterTemplateID, DateTime DateCreatedFrom, DateTime DateCreatedTo, LetterBatchType LetterBatchType, DateTime DateFrom, DateTime DateTo, int RunIDFrom, int RunIDTo, BooleanNullable IsPrinted, BooleanNullable IsLocked, string ProductCode, int Reason)
		{
			base.fill(LetterTemplateID, DateCreatedFrom, DateCreatedTo, LetterBatchType, DateFrom, DateTo, RunIDFrom, RunIDTo, IsPrinted, IsLocked);
			
			productCode = ProductCode;
			reason = Reason;
		}

		public virtual int LetterBatchID
		{
			get
			{
				return letterBatchID;
			}
			set
			{
				letterBatchID = value;
			}
		}

		public virtual string ProductCode
		{
			get
			{
				return productCode;
			}
			set
			{
				productCode = value;
			}
		}

		public virtual int Reason
		{
			get
			{
				return reason;
			}
			set
			{
				reason = value;
			}
		}
	}
}
