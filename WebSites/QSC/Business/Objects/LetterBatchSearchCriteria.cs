using System;
using Common.TableDef;
using Common;

namespace Business.Objects
{
	/// <summary>
	/// Summary description for LetterBatchSearchCriteria.
	/// </summary>
	public class LetterBatchSearchCriteria
	{
		private int letterTemplateID;
		private DateTime dateCreatedFrom;
		private DateTime dateCreatedTo;
		private LetterBatchType letterBatchType;
		private DateTime dateFrom;
		private DateTime dateTo;
		private int runIDFrom;
		private int runIDTo;
		private BooleanNullable isPrinted;
		private BooleanNullable isLocked;

		public LetterBatchSearchCriteria()
		{
		}

		public virtual void fill(int LetterTemplateID, DateTime DateCreatedFrom, DateTime DateCreatedTo, LetterBatchType LetterBatchType, DateTime DateFrom, DateTime DateTo, int RunIDFrom, int RunIDTo, BooleanNullable IsPrinted, BooleanNullable IsLocked)
		{
			letterTemplateID = LetterTemplateID;
			dateCreatedFrom = DateCreatedFrom;
			dateCreatedTo = DateCreatedTo;
			letterBatchType = LetterBatchType;
			dateFrom = DateFrom;
			dateTo = DateTo;
			runIDFrom = RunIDFrom;
			runIDTo = RunIDTo;
			isPrinted = IsPrinted;
			isLocked = IsLocked;
		}

		public virtual int LetterTemplateID
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

		public virtual DateTime DateCreatedFrom
		{
			get
			{
				return dateCreatedFrom;
			}
			set
			{
				dateCreatedFrom = value;
			}
		}

		public virtual DateTime DateCreatedTo
		{
			get
			{
				return dateCreatedTo;
			}
			set
			{
				dateCreatedTo = value;
			}
		}

		public virtual LetterBatchType LetterBatchType
		{
			get
			{
				return letterBatchType;
			}
			set
			{
				letterBatchType = value;
			}
		}

		public virtual DateTime DateFrom
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

		public virtual DateTime DateTo
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

		public virtual int RunIDFrom
		{
			get
			{
				return runIDFrom;
			}
			set
			{
				runIDFrom = value;
			}
		}

		public virtual int RunIDTo
		{
			get
			{
				return runIDTo;
			}
			set
			{
				runIDTo = value;
			}
		}

		public virtual BooleanNullable IsPrinted
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
		public virtual BooleanNullable IsLocked
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
	}
}
