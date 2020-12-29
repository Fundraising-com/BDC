/* Title:	ESubsProgram Collection
 * Author:	Jean-Francois Buist
 * Summary:	A collection of programs for each partners.
 * 
 * Create Date:	August 1, 2005
 * 
 */

using System;
using System.Collections;

namespace GA.BDC.Core.ESubsGlobal {
	/// <summary>
	/// This class manages the different programs for a partner.
	/// </summary>
    [Serializable]
	public class ESubsProgramCollection : EnvironmentBase {
		private ArrayList programs;

		public ESubsProgramCollection() {
			programs = new ArrayList();
		}

		public void AddProgram(int id, string name, string val) {
			AddProgram(new ESubsProgram(id, name, val));
		}

		public void AddProgram(ESubsProgram p) {
			programs.Add(p);
		}

		public static ESubsProgramCollection operator +(ESubsProgramCollection pc, ESubsProgram p) {
			pc.programs.Add(p);
			return pc;
		}

		public static ESubsProgramCollection operator -(ESubsProgramCollection pc, ESubsProgram p) {
			pc.programs.Remove(p);
			return pc;
		}

		public ESubsProgram GetProgramByName(string name) {
			foreach(ESubsProgram pr in programs) {
				if(pr.Name.ToLower() == name.ToLower()) {
					return pr;
				}
			}
			return null;
		}

		public ESubsProgram[] GetPrograms() {
			ESubsProgram[] prs = new ESubsProgram[programs.Count];
			int i=0;
			foreach(ESubsProgram p in programs) {
				prs[i++] = p;
			}
			return prs;
		}
	}
}
