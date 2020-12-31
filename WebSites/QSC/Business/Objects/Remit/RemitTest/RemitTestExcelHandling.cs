using System;
using System.Data;
using FileStore;
using QSPFulfillment.DataAccess.Common;

namespace Business.Objects.RemitTests
{
	/// <summary>
	/// Handles special Remit tests that return result sets
	/// </summary>
	/// <remarks>
	/// Madina Saitakhmetova
	/// August 2006
	/// </remarks>
	public class RemitTestExcelHandling : RemitTest
	{
		DataSet dtResultSet;
		ExcelManager excelMgr;

		public RemitTestExcelHandling() : base()
		{
			dtResultSet = new DataSet();
		}

		/// <summary>
		/// Validate remit, create excel file if result set returned
		/// </summary>
		/// <param name="runID">current runID</param>
		/// <param name="rtLog">message collecting object</param>
		/// <returns>true if valid, false otherwise</returns>
		public override bool ValidateRemitByRunID(int runID, RemitTestLog rtLog)
		{
			bool isValid = true;

			try
			{
				isValid = dataAccess.Validate(testScript, dtResultSet, runID);
			}
			catch(Exception ex)
			{
				ApplicationError.ManageError(ex);
				throw ex;
			}

			rtLog.AddNewHeader(this.testName);

			if(isValid)
			{
				rtLog.AddNewSuccessMsg();	
				this.LogRemitTest(runID, (int) TestResultCode.Success);
			}
			else
			{
				//put dtResultSet in excel object and attach it to rtLog
				excelMgr = new ExcelManager(dtResultSet);

				rtLog.AddNewFailMsg();
				rtLog.attachments.Add("Summary_" + this.testName.Replace(" ", "_") + ".xls", excelMgr.ExcelFile);
			}

			return isValid;
		}

		/// <summary>
		/// execute fix, if fix script is present
		/// </summary>
		public override bool FixRemitByRunID(int runID, RemitTestLog rtLog)
		{
			bool isValid = true;

			if(this.testFix.Length > 0)
			{
				try
				{
					dataAccess.Fix(this.testFix, runID);				
					isValid = dataAccess.Validate(testScript, runID);
				}
				catch(Exception ex)
				{
					ApplicationError.ManageError(ex);
					throw ex;
				}

				if(isValid)
				{
					rtLog.AddNewSuccessfulBadEntriesFixMsg();
					this.LogRemitTest(runID, (int) TestResultCode.Corrected);
				}
				else
				{
					rtLog.AddNewFixFailMsg();
					this.LogRemitTest(runID, (int) TestResultCode.Failure);
				}
			}

			return isValid;
		}
	}
}
