using System;

namespace GA.BDC.Core.Reporting.Artefact
{
	/// <summary>
	/// Summary description for ReportParameter.
	/// </summary>
	public class ReportParameter
	{	
		private int reportParameterId;
		private Parameter parameter;
		private bool displayable;	// FALSE by default
		
		public ReportParameter()
		{
			reportParameterId = int.MinValue;
            parameter = new Parameter();
			displayable = false;	// FALSE by default
		}
		

		#region Properties
		public int ReportParameterId{
			set { reportParameterId = value; }
			get { return reportParameterId; }
		}

		public Parameter Parameter{
			set { parameter = value; }
			get { return parameter; }
		}

		public bool Displayable {
			set { displayable = value; }
			get { return displayable; }
		}
		#endregion

   	}
}
