using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;
using NHibernate.Expression;

namespace QSP.Business.Fulfillment
{
    public partial class Form
    {

        #region Methods

        public static ICriteria CreateCriteria2()
        {
            ISession session = SqlSessionManager.OpenSession();
            ICriteria c = session.CreateCriteria(typeof(Form));
            return c;
        }

        public static List<Form> GetFormListByFormName(string formName)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(Form));

                c.Add(Expression.Eq(FormNameProperty, formName));

                return (List<Form>)c.List<Form>();
            }
        }

        public static List<Form> GetProgramForm(int programTypeId)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(Form));

                c.Add(Expression.Eq(ProgramTypeIdProperty, programTypeId));
                c.Add(Expression.Eq(EntityTypeIdProperty, 2));
                c.Add(Expression.Eq(EnabledProperty, true));
                c.Add(Expression.Eq(DeletedProperty, false));

                return (List<Form>)c.List<Form>();
            }
        }

        #endregion

    }
}
