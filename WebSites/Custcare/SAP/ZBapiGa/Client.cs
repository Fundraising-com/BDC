using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SWCorporate.SAP.Shared.WebServiceModel;

namespace GA.BDC.Web.Custcare.SAP.ZBapiGa
{
    /// <summary>
    /// In your C:\Windows\Microsoft.NET\Framework64\v4.0.30319\Config\machine.config, change the "SW/GA" entry to any of the following:
    /// DEV:
    ///   <Entry DestinationName="SW/GA" AppServerHost="SAPD0101" SystemNumber="00" Client="20" Username="SAPCPIC" Password="ADMIN"/>
    /// TEST:
    ///   <Entry DestinationName="SW/GA" AppServerHost="SAPT0101.swgao.int" SystemNumber="01" Client="10" Username="SAPCPIC" Password="ADMIN"/>
    /// PROD:
    ///   <Entry DestinationName="SW/GA" AppServerHost="SAPP0101" SystemNumber="02" Client="10" Username="SAPCPIC" Password="ADMIN"/> 
    /// </summary>
    public class Client
    {
        public static byte[] GetSAPOnlineProfitStatement(string bdc_sponsor_account_number)
        {            
            ZBapiGaGetOlProfitStmt serviceContract = new ZBapiGaGetOlProfitStmt();
            serviceContract.IAltid = bdc_sponsor_account_number;
            ZBapiGaGetOlProfitStmtResponse serviceResponse;

            try
            {
                using (var serviceProxy = ConfiguredSoapHttpClientFactory<service>.Create())
                {
                    serviceResponse = serviceProxy.ZBapiGaGetOlProfitStmt(serviceContract);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            if (serviceResponse != null)
            {
                if (string.IsNullOrEmpty(serviceResponse.EReturn.Message))
                {
                    return serviceResponse.EPdfout;
                }
                else { throw new ApplicationException(serviceResponse.EReturn.Message); }
            }
            else { throw new ApplicationException("Error calling ZBapiGaGetOlProfitStmt SAP Web Service method"); }
        }
    }
}