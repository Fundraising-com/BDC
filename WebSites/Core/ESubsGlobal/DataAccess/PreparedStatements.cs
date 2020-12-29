using System;
using GA.BDC.Core.Data.Sql;

namespace GA.BDC.Core.ESubsGlobal.DataAccess
{
	/// <summary>
	/// Summary description for PreparedStatements.
	/// </summary>
	public class PreparedStatements
	{
		private PreparedStatements()
		{
			
		}

		private static string GetFileContent(string filename) {
			System.Reflection.Assembly assembly = null;
			System.IO.TextReader textReader = null;
			try {
				assembly = System.Reflection.Assembly.GetExecutingAssembly();
				textReader = new System.IO.StreamReader(assembly.GetManifestResourceStream("GA.BDC.Core.ESubsGlobal.DataAccess.PreparedStatements." + filename + ".txt"));
			}
			catch {
				return string.Empty;
			}
			return textReader.ReadToEnd();
		}

		public static string GetQuery(string scriptName, SqlDataParameterCollection paramCol) {
			string query = GetFileContent(scriptName);

			foreach(GA.BDC.Core.Data.Sql.SqlDataParameter param in paramCol) {
				if(param.Value is string) {
					string objstr = ToObjString(param.Value);
					query = query.Replace(param.ParameterName, "'" + objstr.Replace("'", "''") + "'");
				} else {
					query = query.Replace(param.ParameterName, ToObjString(param.Value));
				}
			}

			return query;
		}

		private static string ToObjString(object obj) {
			if(obj != null) {
				return obj.ToString();
			}
			return "";
		}
	}
}
