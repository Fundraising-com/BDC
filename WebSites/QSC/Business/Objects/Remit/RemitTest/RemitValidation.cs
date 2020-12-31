using System;
using System.Data;
using System.Collections;
using Common.TableDef;
using DAL;
using FileStore;
using dataSetRef = Common.TableDef.RemitTestDataSet;
using dataAccessRef = DAL.RemitTestData;
using QSPFulfillment.DataAccess.Common;

namespace Business.Objects.RemitTests
{
	/// <summary>
	/// Object responsible for remit validation functionalities.
	/// </summary>
	/// <remarks>
	/// Madina Saitakhmetova
	/// August 2006
	/// </remarks>
	public class RemitValidation : BusinessSystem
	{
		protected RemitTestCollection remitTests;
		protected RemitTestLog rtLog;

		dataAccessRef dataAccess = new dataAccessRef();
		dataSetRef dtsDataSet;

		#region Properties

		public dataSetRef dataSet 
		{
			get 
			{
				return this.dtsDataSet;
			}
		}

		protected override DBTableOperation DataAccessReference
		{
			get
			{
				return dataAccess;
			}
		}

		internal override DataSet baseDataSet
		{
			get
			{
				return (DataSet) this.dtsDataSet;
			}
		}

		public override string DefaultTableName
		{
			get
			{
				return this.dtsDataSet.RemitTest.TableName;
			 }
		}
	
		#endregion

		#region Constructors

		public RemitValidation()
		{
			this.dtsDataSet = new dataSetRef();
			this.rtLog = new RemitTestLog();
		}

		public RemitValidation(bool isGetAll) : this()
		{
			if(isGetAll)
			{
				remitTests = FillRemitTestCollection();
			}
		}

		#endregion		

		/// <summary>
		/// Get all remit tests
		/// </summary>
		/// <returns>remit test collection</returns>
		private RemitTestCollection FillRemitTestCollection()
		{
			try
			{
				dataAccess.SelectAll(dtsDataSet, DefaultTableName);
			}
			catch(Exception ex) 
			{
				ApplicationError.ManageError(ex);
				throw ex;
			}

			RemitTestCollection remitTests = new RemitTestCollection();

			foreach(dataSetRef.RemitTestRow dr in dtsDataSet.RemitTest.Rows)
			{
				RemitTest rt = RemitTestFactory.Instance.CreateRemitTest(dr);
				remitTests.Add(rt);
			}

			return remitTests;
		}

		/// <summary>
		/// Validate the remit given run id
		/// </summary>
		/// <param name="runID">run id</param>
		/// <returns>true, if remit is valid. false otherwise</returns>
		public bool Validate(int runID, Store fileStore, RemitMailer rMailer)
		{
			bool isValid = true;

			//ArrayList aList = new ArrayList(); 

			foreach(RemitTest rt in remitTests)
			{
				//if test failed
				if(!rt.ValidateRemitByRunID(runID, rtLog))
				{	
					//execute fix
					isValid = rt.FixRemitByRunID(runID, rtLog);
					
					//if fix didn't fail and test wasn't critical, we're fine
					isValid = isValid || !rt.testCritical;

					//stop testing if critical error encountered
					if(!isValid)
					{
						break;
					}
				}
			}
			
			//send e-mail with remit test results
			rtLog.CloseHtmlMessage();
			rMailer.BodyBuilder.Append(rtLog.htmlMessage);
			for(int i = 0; i < rtLog.attachments.Count; i++)
			{
				fileStore.Add(rtLog.attachments.Keys[i], (byte []) rtLog.attachments[i]);
				rMailer.AddAttachment(fileStore.StoreDirectory.FullName + "\\" + rtLog.attachments.Keys[i]);
			}		

			rtLog.attachments.Clear();

			return isValid;
		}
	}
}
