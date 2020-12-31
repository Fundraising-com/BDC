// Created by:	Benoit Nadon
// Date:		2005-10-03

using System;
using Business.ReportExecution;

namespace Business.Objects
{
	/// <summary>
	/// Conversion methods from the application's parameter classes to the RS web service's
	/// ParameterValue class.
	/// </summary>
	public class RSConvert
	{
		/// <summary>
		/// Converts a ParameterValue to an RS ParameterValue.
		/// </summary>
		/// <param name="parameterValue">ParameterValue to convert.</param>
		/// <returns>Converted RS ParameterValue.</returns>
        public static ParameterValue ToParameterValue(ParameterValue parameterValue) 
		{
			ParameterValue rsParameterValue = new ParameterValue();

			rsParameterValue.Label = parameterValue.Label;
			rsParameterValue.Name = parameterValue.Name;
			rsParameterValue.Value = parameterValue.Value;

			return rsParameterValue;
		}

		/// <summary>
		/// Converts a ParameterValueCollection to an RS ParameterValue array.
		/// </summary>
		/// <param name="parameterValueCollection">ParameterValueCollection to convert.</param>
		/// <returns>Converted RS ParameterValue array.</returns>
        public static ParameterValue[] ToParameterValueArray(ParameterValueCollection parameterValueCollection) 
		{
            ParameterValue[] parameterValues = new ParameterValue[parameterValueCollection.Count];

			for(int i = 0; i < parameterValueCollection.Count; i++)
			{
				parameterValues[i] = RSConvert.ToParameterValue(parameterValueCollection[i]);
			}

			return parameterValues;
		}
	}
}