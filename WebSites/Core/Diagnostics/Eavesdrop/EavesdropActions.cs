using System;
using System.Collections;

namespace GA.BDC.Core.Diagnostics.Eavesdrop {
	/// <summary>
	/// Summary description for ItemToMonitor.
	/// </summary>
	public class EavesdropActions : CollectionBase {

		public EavesdropActions() {
			
		}

		public EavesdropAction this[string itemName] {
			get {
				foreach(EavesdropAction cItem in List) {
					if(cItem.Name == itemName) {
						return cItem;
					}
				}
				EavesdropAction newEaves = new EavesdropAction();
				newEaves.Name = itemName;
				List.Add(newEaves);
				return newEaves;
			}

			set {
				if(List.Contains(value)) {
					for(int i=0;i<List.Count;i++) {
						EavesdropAction cItem = (EavesdropAction)List[i];
						if(cItem.Name == itemName) {
							cItem = value;
							return;
						}
					}
				} else {
					List.Add(value);
				}
			}
		}

		public EavesdropAction this[int index]  {
			get { return (EavesdropAction)List[index]; }
			set { List[index] = value; }
		}

		public void Remove(EavesdropAction item)  {
			List.Remove(item);
		}

		public new void Clear() {
			List.Clear();
		}
	}
}
