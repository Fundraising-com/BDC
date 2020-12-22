using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;
using NHibernate.Expression;

namespace QSP.Business.Fulfillment
{
    public partial class FormSection
    {

        #region Methods

        public static ICriteria CreateCriteria2()
        {
            ISession session = SqlSessionManager.OpenSession();
            ICriteria c = session.CreateCriteria(typeof(Form));
            return c;
        }

        public static List<FormSection> GetFormSectionListByFormId(int formId)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(FormSection));

                c.Add(Expression.Eq(FormIdProperty, formId));

                return (List<FormSection>)c.List<FormSection>();
            }
        }

        public static List<FormSection> GetFormSectionListByFormSectionTypeId(int formSectionTypeId)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(FormSection));

                c.Add(Expression.Eq(FormSectionTypeIdProperty, formSectionTypeId));

                return (List<FormSection>)c.List<FormSection>();
            }
        }

        public static List<FormSection> GetFormSectionListByFormIdAndFormSectionTypeId(int formId, int formSectionTypeId)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(FormSection));

                c.Add(Expression.Eq(FormIdProperty, formId));
                c.Add(Expression.Eq(FormSectionTypeIdProperty, formSectionTypeId));

                return (List<FormSection>)c.List<FormSection>();
            }
        }

        #endregion

    }
}
