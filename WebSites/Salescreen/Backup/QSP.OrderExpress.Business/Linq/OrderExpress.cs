
namespace QSP.OrderExpress.Business.Context {
    using System;
    using System.Configuration;

    partial class OrderExpressDataContext {
        /// <summary>
        /// Loads connection string from Web.config
        /// </summary>
        partial void OnCreated() {
            ConnectionStringSettings s = ConfigurationManager.ConnectionStrings["QSPForm.Data.ConnectionString"];
            if (s != null)
                Connection.ConnectionString = s.ConnectionString;

            int timeout;
            int.TryParse(ConfigurationManager.AppSettings["LinqOperationTimeout"], out timeout);
            timeout = timeout == 0 ? 60 : timeout;
            base.CommandTimeout = timeout;
        }
    }
}