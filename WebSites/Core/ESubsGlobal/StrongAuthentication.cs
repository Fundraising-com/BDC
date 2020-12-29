using System;

namespace GA.BDC.Core.ESubsGlobal {
	/// <summary>
	/// 
	/// </summary>
	public enum StrongAuthenticationMethod {
		InternalNetwork = 0x00,
        UserCreationProcess = 0x01,
		Login = 0x02,
		XFactorAutoLogin = 0x03,
		FromTouch = 0x04,
		FromLead = 0x05
	}

	/// <summary>
	/// Summary description for StrongAuthentication.
	/// </summary>
    [Serializable]
	public class StrongAuthentication {
		private StrongAuthenticationMethod strongAuthentication;
		private DateTime strongAuthenticationDate;

		public StrongAuthentication(StrongAuthenticationMethod strongAuthentication) {
			this.strongAuthentication = strongAuthentication;
			strongAuthenticationDate = DateTime.Now;
		}

		public static implicit operator bool (StrongAuthentication strongAuthentication) {
			return (strongAuthentication != null);
		}

        public StrongAuthenticationMethod CurrentStrongAuthenticationMethod
        {
            get { return strongAuthentication; }
        }
	}
}
