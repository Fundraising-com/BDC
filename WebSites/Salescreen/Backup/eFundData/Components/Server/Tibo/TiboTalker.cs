using System;
using System.Threading;

namespace efundraising.eFundData.Components.Server.Tibo {
	/// <summary>
	/// Summary description for TiboTalker.
	/// </summary>
	public class TiboTalker {
		private string displayName;
		private string extraInformation;
		private efundraising.Core.BusinessBase obj;
		private string objectDescription;
		private string objectLink;
		private int objectType;

		public TiboTalker(string displayName, string extraInformation, efundraising.Core.BusinessBase obj, string objectDescription, string objectLink, int objectType) {
			this.displayName = displayName;
			this.extraInformation = extraInformation;
			this.obj = obj;
			this.objectDescription = objectDescription;
			this.objectLink = objectLink;
			this.objectType = objectType;

			if(efundraising.EnterpriseComponents.Helper.GetWebConfigValue("Tibo").ToLower() == "true") {

#if DEBUG
				this.displayName = "Development: " + displayName;
#endif

				Thread tiboThread = new Thread(new ThreadStart(Start));
				tiboThread.Start();
			}
		}

		private void Start() {
			try {
				com.norbera.tibo.Service tiboSystem = 
					new com.norbera.tibo.Service();
				tiboSystem.InsertTIBO(displayName, extraInformation, "http://webservice.efundraising.com",
					7, "EFundraisingWebService", "", obj.ToString(), objectDescription, objectLink, obj.ToXmlString(), objectType);
			} catch(System.Exception ex) {
				//efundraising.Diagnostics.Logger.LogError("Tibo System Failed", ex);
			}
		}
	}
}
