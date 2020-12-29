using System.Linq;
using Newtonsoft.Json;

using GA.BDC.PAP.Data.GpfRpcObjects;
using Newtonsoft.Json.Linq;


namespace GA.BDC.PAP.Data.Utilities
{
    // ReSharper disable once InconsistentNaming
    internal sealed class PAPSession : PAPCommnicationBase
    {
        private static string _papSessionGuid;
        private static readonly PAPCommnicationBase communicationBase = new PAPCommnicationBase();

        private static string getPapSessionGUID()
        {
            var gpfApiSessionRequest = new GpfApiSessionRequest();
            var serializeObject = JsonConvert.SerializeObject(gpfApiSessionRequest, Formatting.None);
            var jsonSingleObjectRequestWrapper = JsonWrapper.JsonSingleObjectRequestWrapper(serializeObject);
            var result = JsonConvert.DeserializeObject((new PAPCommunication()).CallPapAPI(jsonSingleObjectRequestWrapper, communicationBase.serverURL, communicationBase.serverMethodPost)) as Newtonsoft.Json.Linq.JArray;
            foreach (var jToken in result)
            {
                var ie = (JObject) jToken;
                if (ie.First != null && ie.First.First != null && ie.First.First.Last != null && ie.First.First.Last.Count() > 1)
                {
                    return ie.First.First.Last.Previous[1].ToString();
                    //return ie.First.First.Last[1].ToString();
                }
            }

            return null;

        }

        public static string PAPSessionGUID
        {
            get
            {
                if (string.IsNullOrEmpty(_papSessionGuid))
                {
                    _papSessionGuid = getPapSessionGUID();
                }
                return _papSessionGuid;
            }
        }

        public static void ClearSession()
        {
            _papSessionGuid = string.Empty;
        }

        public static string GetVisitorId()
        {
            return (new PAPCommunication()).CallPapAPI(string.Empty, communicationBase.trackerUrl, communicationBase.serverMethodGet);

        }
    }
}
