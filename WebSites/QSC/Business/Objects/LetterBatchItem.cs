using System;
using Common.TableDef;
using DAL;

namespace Business.Objects
{
	/// <summary>
	/// Summary description for LetterBatchItem.
	/// </summary>
	[Serializable]
	public class LetterBatchItem
	{
		private int id = DBInteractionBase.NullValueInteger;
		private int letterTemplateID = DBInteractionBase.NullValueInteger;
		private DateTime dateFrom = DBInteractionBase.NullValueDateTime;
		private DateTime dateTo = DBInteractionBase.NullValueDateTime;
		private int runID = DBInteractionBase.NullValueInteger;
		private bool isPrinted = false;
		private DateTime datePrinted = DBInteractionBase.NullValueDateTime;
		private bool isLocked = true;
		private int userIDCreated = DBInteractionBase.NullValueInteger;
		private DateTime dateCreated = DBInteractionBase.NullValueDateTime;
		private bool deletedTF = false;

		public LetterBatchItem() { }

		public LetterBatchItem(int letterTemplateID, int userIDCreated) 
		{
			this.letterTemplateID = letterTemplateID;
			this.userIDCreated = userIDCreated;
			this.dateCreated = DateTime.Now;
		}

		public LetterBatchItem(int id, int letterTemplateID, DateTime dateFrom, DateTime dateTo, int runID, bool isPrinted, DateTime datePrinted, bool isLocked, int userIDCreated, DateTime dateCreated, bool deletedTF)
		{
			this.id = id;
			this.letterTemplateID = letterTemplateID;
			this.dateFrom = dateFrom;
			this.dateTo = dateTo;
			this.runID = runID;
			this.isPrinted = isPrinted;
			this.datePrinted = datePrinted;
			this.isLocked = isLocked;
			this.userIDCreated = userIDCreated;
			this.dateCreated = dateCreated;
			this.deletedTF = deletedTF;
		}

		public LetterBatchItem(LetterBatchDataSet.LetterBatchRow row) 
		{
			this.id = row.ID;
			this.letterTemplateID = row.LetterTemplateID;

			if(!row.IsDateFromNull()) 
			{
				this.dateFrom = row.DateFrom;
			} 
			else 
			{
				this.dateFrom = DBInteractionBase.NullValueDateTime;
			}

			if(!row.IsDateToNull()) 
			{
				this.dateTo = row.DateTo;
			} 
			else 
			{
				this.dateTo = DBInteractionBase.NullValueDateTime;
			}

			this.runID = row.RunID;
			this.isPrinted = row.IsPrinted;

			if(!row.IsDatePrintedNull()) 
			{
				this.datePrinted = row.DatePrinted;
			} 
			else 
			{
				this.datePrinted = DBInteractionBase.NullValueDateTime;
			}

			this.isLocked = row.IsLocked;
			this.userIDCreated = row.UserIDCreated;
			this.dateCreated = row.DateCreated;
			this.deletedTF = row.DeletedTF;
		}

		public int ID 
		{
			get 
			{
				return id;
			}
			set 
			{
				id = value;
			}
		}

		public int LetterTemplateID 
		{
			get 
			{
				return letterTemplateID;
			}
			set 
			{
				letterTemplateID = value;
			}
		}

		public LetterBatchType LetterBatchType 
		{
			get 
			{
				LetterBatchType letterBatchType = LetterBatchType.CustomerService;

				if(dateFrom != DBInteractionBase.NullValueDateTime && dateTo != DBInteractionBase.NullValueDateTime) 
				{
					letterBatchType = LetterBatchType.DateRange;
				} 
				else if(runID != DBInteractionBase.NullValueInteger) 
				{
					letterBatchType = LetterBatchType.RemitBatchID;
				}

				return letterBatchType;
			}
		}

		public DateTime DateFrom 
		{
			get 
			{
				return dateFrom;
			}
			set 
			{
				dateFrom = value;
			}
		}

		public DateTime DateTo 
		{
			get 
			{
				return dateTo;
			}
			set 
			{
				dateTo = value;
			}
		}

		public int RunID 
		{
			get 
			{
				return runID;
			}
			set 
			{
				runID = value;
			}
		}

		public bool IsPrinted 
		{
			get 
			{
				return isPrinted;
			}
			set 
			{
				isPrinted = value;
			}
		}

		public DateTime DatePrinted 
		{
			get 
			{
				return datePrinted;
			}
			set 
			{
				datePrinted = value;
			}
		}

		public bool IsLocked 
		{
			get 
			{
				return isLocked;
			}
			set 
			{
				isLocked = value;
			}
		}

		public int UserIDCreated 
		{
			get 
			{
				return userIDCreated;
			}
			set 
			{
				userIDCreated = value;
			}
		}

		public DateTime DateCreated 
		{
			get 
			{
				return dateCreated;
			}
			set 
			{
				dateCreated = value;
			}
		}

		public bool DeletedTF 
		{
			get 
			{
				return deletedTF;
			}
			set 
			{
				deletedTF = value;
			}
		}
	}
}
