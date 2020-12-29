//
// 2005-08-23 - Stephen Lim - New class.
//


using System;
using GA.BDC.Core.ESubsGlobal.DataAccess;
using System.Collections.Generic;

namespace GA.BDC.Core.ESubsGlobal.Touch
{
	/// <summary>
	/// Summary description for TouchRule.
	/// </summary>
	public class TouchRule : EnvironmentBase
	{

		#region Fields
		private int _ruleId = int.MinValue;
		private int _emailTemplateId = int.MinValue;
		private string procedureCall = null;
		private short priorityLevel = short.MinValue;
		#endregion

		#region Constructors
		public TouchRule()
		{

		}
		#endregion

		#region Methods
		public List<TouchEmail> GetEmails()
		{
			ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
			return dbo.GetTouchEmails(this);
		}
		#endregion


		#region Properties
		public int RuleId
		{
			get { return _ruleId; }
			set { _ruleId = value; }
		}

		public int EmailTemplateId
		{
			get { return _emailTemplateId; }
			set { _emailTemplateId = value; }
		}

		public string ProcedureCall
		{
			get { return procedureCall; }
			set { procedureCall = value; }
		}

		public short PriorityLevel
		{
			get { return priorityLevel; }
			set { priorityLevel = value; }
		}
		#endregion
	}
}
