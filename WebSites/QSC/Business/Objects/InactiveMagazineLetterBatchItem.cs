using System;
using Common.TableDef;
using DAL;

namespace Business.Objects
{
	/// <summary>
	/// Summary description for InactiveMagazineLetterBatchItem.
	/// </summary>
	public class InactiveMagazineLetterBatchItem : LetterBatchItem
	{
		private int    letterBatchID = DBInteractionBase.NullValueInteger;
		private string productCode   = DBInteractionBase.NullValueString;
		private int    reason        = DBInteractionBase.NullValueInteger;
		protected const int INACTIVEMAGAZINETEMPLATEID = 4;

		public InactiveMagazineLetterBatchItem():base()
		{
			this.IsLocked = false;
		} 

		public InactiveMagazineLetterBatchItem(int userIDCreated):base(INACTIVEMAGAZINETEMPLATEID, userIDCreated)
		{
			this.IsLocked = false;
		}

		public InactiveMagazineLetterBatchItem(int letterTemplateID, int userIDCreated):base(letterTemplateID, userIDCreated)
		{
			this.IsLocked = false;
		}

		public InactiveMagazineLetterBatchItem(int id, int letterTemplateID, DateTime dateFrom, DateTime dateTo, int runID, bool isPrinted, DateTime datePrinted, bool isLocked, int userIDCreated, DateTime dateCreated, bool deletedTF, string productCode, int reason)
			:base(id, letterTemplateID, dateFrom, dateTo, runID, isPrinted, datePrinted, isLocked, userIDCreated, dateCreated, deletedTF)
		{
			this.IsLocked = false;
			this.letterBatchID = id;
			this.productCode = productCode;
			this.reason = reason;
		}

		public InactiveMagazineLetterBatchItem(LetterBatchDataSet.LetterBatchRow row):base(row)
		{
			//Get corresponding InactiveMagazineLetterBatch Row
			LetterBatchDataSet.InactiveMagazineLetterBatchRow inactiveMagazineLetterBatchRow = (LetterBatchDataSet.InactiveMagazineLetterBatchRow) row.GetParentRow("InactiveMagazineLetterBatchLetterBatch");

			this.productCode = inactiveMagazineLetterBatchRow.ProductCode;
			this.reason = inactiveMagazineLetterBatchRow.Reason;
		}

		public int LetterBatchID 
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

		public string ProductCode 
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

		public int Reason 
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
