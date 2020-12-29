using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace GA.BDC.Core.Parsers.SimpleParser {
	/// <summary>
	/// SimpleTagParser will 
	/// </summary>
	/// <example>
	/// <code>
	/// 		string source = "Salut <%= DynTags[\"Group1\"].ToUpper().ToCapitalize(salut); %>, <%= DynTags[\"Group1\"]; %>!";
	///
	///			ArrayList commands = SimpleTagParser.GetCommandLines(source);
	///			foreach(string s in commands) {
	///				SimpleTagParser stp = new SimpleTagParser(s);
	///				stp.VariableName;	// = DynTags
	///				stp.VariableKey;	// = Group1
	///				stp.Functions;		// = {ToUpper(), ToCapitalize(salut)}
	///			}
	///
	/// </code>
	/// </example>
	public class SimpleTagParser {
		private string variableName;
		private string variableKey;
		private ArrayList functions;

		public SimpleTagParser(string commandLine) {
			GetDynamicTagKey(commandLine);
			functions = GetFunctionsLine(commandLine);
		}

		private ArrayList GetFunctions(string functionsLine) {
			string source = functionsLine;
			string find = "\\.([\\w]+\\([\\w]*\\))";
			ArrayList functions = new ArrayList();
			MatchCollection mc = Regex.Matches(source, find, RegexOptions.IgnoreCase);
			string variable = "";
			for(int i=0;i<mc.Count;i++) {
				Match m = mc[i];
				if(m.Groups.Count > 0) {
					Group g = m.Groups[1];
					if(g.Value != "") {
						functions.Add(g.Value);
					}
				}
			}
			return functions;		
		}

		private ArrayList GetFunctionsLine(string dynTag) {
			string source = dynTag;
			string find = variableName + "\\[\\\"[\\w]+\\\"\\](.*)";
			MatchCollection mc = Regex.Matches(source, find, RegexOptions.IgnoreCase);
			for(int i=0;i<mc.Count;i++) {
				Match m = mc[i];
				if(m.Groups.Count > 0) {
					Group g = m.Groups[1];
					if(g.Value != "") {
						return GetFunctions(g.Value);
					}
				}
			}
			return new ArrayList();
		}

		private void GetDynamicTagKey(string dynTag) {
			string source = dynTag;
			// string find = "DynTags\\[\\\"([\\w]+)\\\"\\]";
			string find = "([\\w]+)\\[\\\"([\\w]+)\\\"\\]";
			MatchCollection mc = Regex.Matches(source, find, RegexOptions.IgnoreCase);
			for(int i=0;i<mc.Count;i++) {
				Match m = mc[i];
				if(m.Groups.Count > 0) {
					variableName = m.Groups[1].Value;
					variableKey = m.Groups[2].Value;
				}
			}
		}

		public static ArrayList GetCommandLines(string command) {
			ArrayList cmds = new ArrayList();
			string source = command;
			// string find = "<%=\\s([\\w\\;\\[\\]\\\"\\.\\(\\)]+);\\s%>";
			string find = "(<%=\\s*[\\w\\;\\[\\]\\\"\\.\\(\\)]+\\s*\\s*%>)";
			MatchCollection mc = Regex.Matches(source, find, RegexOptions.IgnoreCase);
			for(int i=0;i<mc.Count;i++) {
				Match m = mc[i];
				if(m.Groups.Count > 0) {
					Group g = m.Groups[1];
					cmds.Add(g.Value);
				}
			}
			return cmds;
		}

		#region Attributes
		public string VariableName {
			set { variableName = value; }
			get { return variableName; }
		}

		public string VariableKey {
			set { variableKey = value; }
			get { return variableKey; }
		}

		public ArrayList Functions {
			set { functions = value; }
			get { return functions; }
		}
		#endregion
	}
}
