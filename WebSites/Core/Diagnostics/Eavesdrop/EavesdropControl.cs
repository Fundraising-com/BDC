using System;
using System.Collections;

namespace GA.BDC.Core.Diagnostics.Eavesdrop
{
	/// <summary>
	/// Summary description for Eavesdrop.
	/// </summary>
	public class EavesdropControl {
		private static EavesdropControl instance;
		private static readonly object padlock = new object();
		
		private EavesdropActions items;
		private int day = -1;

		private EavesdropControl() {
			items = new EavesdropActions();
		}

		public void Add(string itemToMonitor, string action) {
			lock (instance) {
				try {
					if(DateTime.Now.Day != day) {
						day = DateTime.Now.Day;
						items.Clear();
					}
					items[itemToMonitor].AddAction(action);
				} catch {}
			}
		}

		public static EavesdropControl Create2() {
			lock(padlock) {
				if(instance == null) {
					instance = new EavesdropControl();
				}
				return instance;
			}
		}
		
		public EavesdropActions Actions {
			get { return items; }
		}

		public int CurrentDay {
			get { return day; }
		}
	}
}
