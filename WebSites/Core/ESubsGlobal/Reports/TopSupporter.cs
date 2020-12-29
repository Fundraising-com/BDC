using System;

namespace GA.BDC.Core.ESubsGlobal.Reports {
	/// <summary>
	/// Summary description for TopSupporter.
	/// </summary>
    [Serializable]
	public class TopSupporter {
		private string name;
        private Decimal amount = Decimal.MinValue;
        private DateTime createdDate = DateTime.MinValue;

		public TopSupporter() {

		}

		#region Properties
		public string Name {
			set { name = value; }
			get { return name; }
		}

		public Decimal Amount {
			set { amount = value; }
			get { return amount; }
		}

		public DateTime CreatedDate {
			set { createdDate = value; }
			get { return createdDate; }
		}
		#endregion
	}
}
