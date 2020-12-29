using System;

namespace GA.BDC.Core.Reporting.Artefact
{
	/// <summary>
	/// Summary description for ParameterControl.
	/// </summary>
	public class ParameterControl
	{
		private int parameterControlId;
		private string parameterControlName;

		public ParameterControl()
		{
			parameterControlId = int.MinValue;
			parameterControlName = string.Empty;
		}

		public void SetParameterControl(int id, string name)
		{
			parameterControlId = id;
			parameterControlName = name;
		}
	

		#region Properties
		public int ParameterControlId {
			set { parameterControlId = value; }
			get { return parameterControlId; }
		}

		public string ParameterControlName
		{
			set { parameterControlName = value; }
			get { return parameterControlName; }
		}
		#endregion
	
	}
}
