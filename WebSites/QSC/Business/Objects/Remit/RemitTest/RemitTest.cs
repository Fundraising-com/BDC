using System;
using System.Data;
using Common.TableDef;
using DAL;
using dataSetRef = Common.TableDef.RemitTestDataSet;
using dataAccessRef = DAL.RemitTestData;
using QSPFulfillment.DataAccess.Common;

enum TestResultCode 
{ 
	Success = 64001, Failure = 64002, Corrected = 64003  
}

namespace Business.Objects.RemitTests
{
	/// <summary>
	/// Base class for Remit tests
	/// </summary>
	/// <remarks>
	/// Madina Saitakhmetova
	/// August 2006
	/// </remarks>
	public class RemitTest
	{
		protected int tID;
		protected string tName;
		protected string tScript;
		protected string tFix;
		protected bool tCritical;
		protected dataAccessRef dataAccess;

		#region Properties

		public int testID
		{
			get
			{
				return tID;
			}

			set 
			{
				tID = value;
			}

		}	
	
		public string testName
		{
			get
			{
				return tName;
			}

			set 
			{
				tName = value;
			}

		}		

		public string testScript
		{
			get
			{
				return tScript;
			}

			set 
			{
				tScript = value;
			}

		}		

		public string testFix
		{
			get
			{
				return tFix;
			}

			set 
			{
				tFix = value;
			}
		}		

		public bool testCritical
		{
			get
			{
				return tCritical;
			}

			set 
			{
				tCritical = value;
			}
		}	

		#endregion
		
		public RemitTest()
		{		
			dataAccess = new dataAccessRef();
		}

		public virtual bool ValidateRemitByRunID(int runID, RemitTestLog rtLog)
		{
			bool isValid = true;

			isValid = dataAccess.Validate(testScript, runID);
		
			rtLog.AddNewHeader(this.testName);

			if(isValid)
			{
				rtLog.AddNewSuccessMsg();	
				this.LogRemitTest(runID, (int) TestResultCode.Success);
			}
			else
			{
				rtLog.AddNewFailMsg();
			}

			return isValid;
		}

		/// <summary>
		/// execute fix, if fix script is present
		/// </summary>
		public virtual bool FixRemitByRunID(int runID, RemitTestLog rtLog)
		{
			bool isValid = false;

			if(this.testFix.Length > 0)
			{	
				dataAccess.Fix(this.testFix, runID);
				isValid = dataAccess.Validate(testScript, runID);
				
				if(isValid)
				{
					rtLog.AddNewSuccessfulFixMsg();
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

		/// <summary>
		/// log the remit test result in the database
		/// </summary>
		/// <param name="runID">run id</param>
		/// <param name="testResult">result of the test</param>
		protected void LogRemitTest(int runID, int testResult)
		{
			dataAccess.LogTest(runID, this.testID, testResult);
		}
	}
}
