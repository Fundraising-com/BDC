
using System;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;

using Debug = System.Diagnostics.Debug;
using StackTrace = System.Diagnostics.StackTrace;

namespace DAL
{
	public sealed class SqlCommandGenerator
	{
		private SqlCommandGenerator() 
		{
			throw new NotSupportedException();
		}

		public static readonly string ReturnValueParameterName = "RETURN_VALUE";
		public static readonly object[] NoValues = new object[] {};

		public static SqlCommand GenerateCommand(SqlConnection connection,
			MethodInfo method, object[] values)
		{
			if (method == null)
				method = (MethodInfo) (new StackTrace().GetFrame(1).GetMethod());

			//
			// Get the SqlCommandMethodAttribute on the method passed in.
			// This attribute is required in order to use the method for
			// generating a command object.
			//

			SqlCommandMethodAttribute commandAttribute = (SqlCommandMethodAttribute) Attribute.GetCustomAttribute(
				method, typeof(SqlCommandMethodAttribute));

			Debug.Assert(commandAttribute != null);
			Debug.Assert(commandAttribute.CommandType == CommandType.StoredProcedure ||
				commandAttribute.CommandType == CommandType.Text);

			//
			// Create a SqlCommand object and configure it according to the
			// specification of the attribute.
			//

			SqlCommand command = new SqlCommand();
			command.Connection = connection;
			command.CommandType = commandAttribute.CommandType;

			//
			// Get the text of the command. If it is zero-length then use 
			// the name of the method as a stored procedure name.
			//

			if (commandAttribute.CommandText.Length == 0)
			{
				Debug.Assert(commandAttribute.CommandType == CommandType.StoredProcedure);
				command.CommandText = method.Name;
			}
			else
			{
				command.CommandText = commandAttribute.CommandText;
			}

			//
			// Generate the command parameters, adding the return value.
			//

			GenerateCommandParameters(command, method, values);
			command.Parameters.Add(ReturnValueParameterName, SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

			return command;
		}

		private static void GenerateCommandParameters(
			SqlCommand command, MethodInfo method, object[] values)
		{
			//
			// Get all the parameters and process each in a loop.
			//

			ParameterInfo[] methodParameters = method.GetParameters();
            
			int paramIndex = 0;

			foreach (ParameterInfo paramInfo in methodParameters)
			{
				//
				// If the parameter is tagged with the [NonCommandParameter ]
				// attribute then skip it completely.
				//

				if (Attribute.IsDefined(paramInfo, typeof(NonCommandParameterAttribute)))
					continue;

				//
				// Get the SqlParameter attribute on the parameter.
				// If it is absent, then create one here and use its default settings.
				//

				SqlParameterAttribute paramAttribute = (SqlParameterAttribute) Attribute.GetCustomAttribute(
					paramInfo, typeof(SqlParameterAttribute));

				if (paramAttribute == null)
					paramAttribute = new SqlParameterAttribute();

				//
				// Setup the stored procedure parameter object using 
				// settings from the attribute. Use those values that 
				// were defined otherwise infer them from the method's
				// parameter.
				//

				SqlParameter sqlParameter = new SqlParameter();

				if (paramAttribute.IsNameDefined)
					sqlParameter.ParameterName = paramAttribute.Name;
				else
					sqlParameter.ParameterName = paramInfo.Name;

				if (!sqlParameter.ParameterName.StartsWith("@"))
					sqlParameter.ParameterName = "@" + sqlParameter.ParameterName;

				if (paramAttribute.IsTypeDefined)
					sqlParameter.SqlDbType = paramAttribute.SqlDbType;

				if (paramAttribute.IsSizeDefined)
					sqlParameter.Size = paramAttribute.Size;

				if (paramAttribute.IsScaleDefined)
					sqlParameter.Scale = paramAttribute.Scale;

				if (paramAttribute.IsPrecisionDefined)
					sqlParameter.Precision = paramAttribute.Precision;

				if (paramAttribute.IsDirectionDefined)
				{
					sqlParameter.Direction = paramAttribute.Direction;
				}
				else
				{
					if (paramInfo.ParameterType.IsByRef)
					{
						sqlParameter.Direction = paramInfo.IsOut ? 
							ParameterDirection.Output : 
							ParameterDirection.InputOutput;
					}
					else
					{
						sqlParameter.Direction = ParameterDirection.Input;
					}
				}

				//
				// Sanity check: Too few values supplied?
				//

				Debug.Assert(paramIndex < values.Length);

				sqlParameter.Value = values[paramIndex];
				command.Parameters.Add(sqlParameter);

				paramIndex++;
			}

			//
			// Sanity check: Too many values supplied?
			//

			Debug.Assert(paramIndex == values.Length);
		}
	}
}
