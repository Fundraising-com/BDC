using System;
using System.Collections;
using System.IO;

namespace GA.BDC.Core.EnterpriseComponents {
	/// <summary>
	/// Summary description for SqlFormat.
	/// </summary>
	public class SqlFormat {
		
		public enum Operator {
			AND,
			AND_NOT,
			OR
		}

		public SqlFormat() {

		}

		public static ArrayList GetFullTextSearchNoiseWord(string filename) {
			ArrayList noiseWords = new ArrayList();

			try {
				StreamReader srReadLine = new StreamReader((System.IO.Stream)File.OpenRead(filename),
					System.Text.Encoding.ASCII);
				srReadLine.BaseStream.Seek(0, SeekOrigin.Begin);
				while (srReadLine.Peek() > -1) {
					string line = srReadLine.ReadLine().Trim();

					// skip comment or empty lines
					if(!(line.StartsWith("#") || line == "")) {
						noiseWords.Add(line.ToLower());
					}
				}
				srReadLine.Close();
			} catch(System.Exception ex) {
				// return an empty array list and send an error
 				EnterpriseComponents.LoggingSystem.LogError("Unable to open " + filename + "!\n" + ex.Message);
				return new ArrayList();
			}
			return noiseWords;
		}

		public static bool IsNoiseWord(ArrayList noiseList, string noiseWord) {
			for(int i=0;i<noiseList.Count;i++) {
				if((string)noiseList[i] == noiseWord.ToLower())
					return true;
			}
			return false;
			// return noiseList.Contains(noiseWord.ToLower());
		}


		public static string GetContainsSQLString(ArrayList noiseList, string rawSQLQuery, Operator oper, bool wildCard) {
			string[] splittedWords = rawSQLQuery.Split(' ');
			ArrayList goodWord = new ArrayList();
			string rSqlQuery = "";
			
			foreach(string word in splittedWords) {
				if(word.Length > 2) {
					if(!IsNoiseWord(noiseList, word)) {
						goodWord.Add(word + (wildCard?"*":""));
					}
				}
			}

			for(int i=0;i<goodWord.Count;i++) {
				rSqlQuery += "\"" + goodWord[i].ToString() + "\"";
				if(i + 1 < goodWord.Count) {
					string operatorType = "";
					switch(oper) {
						case Operator.AND:
							operatorType = "AND";
							break;
						case Operator.OR:
							operatorType = "OR";
							break;
						case Operator.AND_NOT:
							operatorType = "AND NOT";
							break;
					}
					rSqlQuery += " " + oper + " ";
				}
			}

			return rSqlQuery;
		}
	}
}
