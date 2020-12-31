using System;
using Common;
using Common.TableDef;
using DAL;
using dataSetRef = Common.TableDef.RemitTestDataSet;
using dataAccessRef = DAL.RemitTestData;
using Business.Objects.RemitTests;

namespace Business.Objects
{
	/// <summary>
	/// Creator factory : produces an object of a specific type depending on remit test's name
	/// </summary>
	/// <remarks>
	///		Instance of the Simple Factory Pattern
	///		Implements the Singleton Pattern
	/// </remarks>
	public class RemitTestFactory
	{
		private static RemitTestFactory singletonInstance;

		private RemitTestFactory() { }

		public static RemitTestFactory Instance 
		{
			get 
			{
				if(singletonInstance == null) 
				{
					singletonInstance = new RemitTestFactory();
				}

				return singletonInstance;
			}
		}

		public RemitTest CreateRemitTest(dataSetRef.RemitTestRow dr) 
		{
			RemitTest rt;

			if(dr.Name.IndexOf("Province") >= 0 || dr.Name.IndexOf("Postal") >= 0 || dr.Name.IndexOf("Zero") >= 0 || dr.Name.IndexOf("Staff Orders for Time") >= 0)
			{
				rt = new RemitTestExcelHandling();
			}
			else
			{
				rt = new RemitTest();
			}

			rt.testID = dr.ID;
			rt.testName = dr.Name;
			rt.testScript = dr.Script;
			rt.testFix = dr.CorrectionScript;
			rt.testCritical = dr.IsCritical;

			return rt;
		}
	}
}
