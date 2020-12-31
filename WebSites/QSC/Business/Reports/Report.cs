using System;
using System.Collections;
using System.Data;
using Business.ReportService;
using System.Net;

namespace Business.Reports
{
	/// <summary>
	/// Summary description for Report.
	/// </summary>
	public abstract class Report : MarshalByRefObject
	{
		private const int TIMEOUT = 900000;

		private ParameterFieldReference[] oReportParameters = null;

		public Report() : base() 
		{
			InitializeReportParameters();
		}

		protected abstract string ReportName 
		{
			get;
		}

		protected virtual ParameterFieldReference[] ReportParameters 
		{
			get 
			{
				return oReportParameters;
			}
			set 
			{
				oReportParameters = value;
			}
		}

		protected abstract void InitializeReportParameters();

		public virtual byte[] Generate(DataRow row) 
		{
            byte[] result;
            Business.ReportExecution.ParameterValue[] inputParams = GetParameterValues(ReportParameters, row);

            Business.Objects.RSClient rs = new Business.Objects.RSClient();
            result = rs.GenerateReportStream(ReportName, "PDF", inputParams, TIMEOUT);

            return result;
		}

        private Business.ReportExecution.ParameterValue[] GetParameterValues(ParameterFieldReference[] fieldReferences, DataRow row) 
		{
			ArrayList oParameterValues = new ArrayList();
            Business.ReportExecution.ParameterValue oParameterValue;

			foreach(ParameterFieldReference fieldReference in fieldReferences) 
			{
				try 
				{
                    oParameterValue = new Business.ReportExecution.ParameterValue();

					oParameterValue.Name = fieldReference.ParameterName;

					if(fieldReference.FieldAlias != null) 
					{
						oParameterValue.Value = row[fieldReference.FieldAlias].ToString();
					}

					oParameterValues.Add(oParameterValue);
				}
				catch 
				{
					throw new UndefinedReportParameterException(fieldReference.ParameterName);
				}
			}

            return (Business.ReportExecution.ParameterValue[])oParameterValues.ToArray(typeof(Business.ReportExecution.ParameterValue));
		}
	}
}
