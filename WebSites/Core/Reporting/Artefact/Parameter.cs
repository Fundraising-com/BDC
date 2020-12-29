using System;

namespace GA.BDC.Core.Reporting.Artefact
{
	/// <summary>
	/// Summary description for Parameter.
	/// </summary>
	public class Parameter
	{
		private int parameterId;
		private ParameterControl parameterControl;
		private string parameterLabel;
		private string parameterDescription;
		private string parameterName;
		private string parameterRefTable;
		private string parameterRefId;
		private string parameterRefText;
		private string parameterRefOrderBy;
		private string parameterDefaultValue;
		private string parameterSqlQuery;

		public Parameter()
		{
			parameterId = int.MinValue;
			parameterControl = new ParameterControl();
			parameterLabel = string.Empty;
			parameterDescription = string.Empty;
			parameterName = string.Empty;
			parameterRefTable = string.Empty;
			parameterRefId = string.Empty;
			parameterRefText = string.Empty;
			parameterRefOrderBy = string.Empty;
			parameterDefaultValue = string.Empty;
			parameterSqlQuery = string.Empty;
		}

		
		#region Properties

		public int ParameterId
		{
			set { parameterId = value; }
			get { return parameterId; }
		}

		public int ParameterControlId {
			set { parameterControl.ParameterControlId = value; }
			get { return parameterControl.ParameterControlId; }
		}

		public string ParameterControlName {
			set { parameterControl.ParameterControlName = value; }
			get { return parameterControl.ParameterControlName; }
		}

		public string ParameterLabel {
			set { parameterLabel = value; }
			get { return parameterLabel; }
		}

		public string ParameterDescription {
			set { parameterDescription = value; }
			get { return parameterDescription; }
		}

		public string ParameterName {
			set { parameterName = value; }
			get { return parameterName; }
		}
		
		public string ParameterRefTable {
			set { parameterRefTable = value; }
			get { return parameterRefTable; }
		}

		public string ParameterRefId {
			set { parameterRefId = value; }
			get { return parameterRefId; }
		}

		public string ParameterRefText {
			set { parameterRefText = value; }
			get { return parameterRefText; }
		}

		public string ParameterRefOrderBy {
			set { parameterRefOrderBy = value; }
			get { return parameterRefOrderBy; }
		}

		public string ParameterDefaultValue {
			set { parameterDefaultValue = value; }
			get { return parameterDefaultValue; }
		}

		public string ParameterSqlQuery {
			set { parameterSqlQuery = value; }
			get { return parameterSqlQuery; }
		}

		#endregion

	}
}
