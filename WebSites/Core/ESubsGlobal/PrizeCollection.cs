/* Title:	Prize Collection.
 * Author:	Jean-Francois Buist
 * Summary:	Collection of prizes.
 * 
 * Create Date:	August 1, 2005
 * 
 */

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GA.BDC.Core.ESubsGlobal {
	/// <summary>
	/// Summary description for PrizeCollection.
	/// </summary>
	[Serializable, XmlInclude(typeof(Prize))]
	public class PrizeCollection : EnvironmentBase {
		private List<Prize> prizes;

		public PrizeCollection() {
            prizes = new List<Prize>();
		}

		public void Add(int id, string name) {
			Add(new Prize(id, name));
		}

		public void Add(Prize prize) {
			prizes.Add(prize);
		}

		public Prize GetPrizeByID(int id) {
			foreach(Prize p in prizes) {
				if(p.PrizeID == id) {
					return p;
				}
			}
			return null;
		}

		public Prize[] GetPrizesByProgramID(int id) {
            List<Prize> prizesList = new List<Prize>();
			foreach(Prize p in prizes) {
				if(p.ProgramTypeID == id) {
                    prizesList.Add(p);
				}
			}
            return prizesList.ToArray();
		}

		public Prize GetPrizeByName(string name) {
			foreach(Prize p in prizes) {
				if(p.Name.ToLower() == name.ToLower()) {
					return p;
				}
			}
			return null;
		}

		public Prize[] GetPrizes() {
            return prizes.ToArray();
		}

		public static PrizeCollection GetPrizeByPartnerID(int programID, int partnerId) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPrizeCollection(programID, partnerId);
		}
	}
}
