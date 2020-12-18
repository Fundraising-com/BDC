using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace QSP.Business.Fulfillment
{
    public partial class ClientAccount
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="sequenceCode"></param>
        /// <returns></returns>
        public static List<ClientAccount> GetClientAccountByClientIdAndSequenceCode(int clientId, string sequenceCode)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(ClientAccount));
                c.Add(Expression.Eq(ClientIdProperty, clientId));
                c.Add(Expression.Eq(SequenceCodeProperty, sequenceCode));
                return (List<ClientAccount>)c.List<ClientAccount>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="sequenceCode"></param>
        /// <param name="programTypeId"></param>
        /// <returns></returns>
        public static ClientAccount GetClientAccountByClientIdSequenceCodeAndProgramType(int clientId, string sequenceCode, int programTypeId)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(ClientAccount));
                c.Add(Expression.Eq(ClientIdProperty, clientId));
                c.Add(Expression.Eq(SequenceCodeProperty, sequenceCode));
                c.Add(Expression.Eq(ProgramTypeIdProperty, programTypeId));
                return c.UniqueResult<ClientAccount>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="sequenceCode"></param>
        /// <param name="programTypeId"></param>
        /// <returns></returns>
        public static List<ClientAccount> GetClientAccountListByClientIdSequenceCodeAndProgramType(int clientId, string sequenceCode, int programTypeId)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(ClientAccount));
                c.Add(Expression.Eq(ClientIdProperty, clientId));
                c.Add(Expression.Eq(SequenceCodeProperty, sequenceCode));
                c.Add(Expression.Eq(ProgramTypeIdProperty, programTypeId));
                return (List<ClientAccount>)c.List<ClientAccount>();
            }
        }


    }
}
