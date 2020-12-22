using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using NHibernate;
using NHibernate.Expression;
using NHibernate.Mapping.Attributes;

namespace QSP.Site.MyFeedback
{
    public partial class Feedback
    {
        public static List<Feedback> GetTopFeedbackList(int maxResults)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(Feedback));
                c.Add(Expression.Eq(Feedback.ActiveProperty, true));
                c.Add(Expression.Eq(Feedback.PublishedProperty, true));
                c.AddOrder(Order.Desc(Feedback.CreateDateProperty));
                c.SetMaxResults(maxResults);
                return (List<Feedback>)c.List<Feedback>();
            }
        }

        public virtual string ShortMessage
        {
            get
            {
                if (this.Message.Length > 200)
                    return this.Message.Substring(0, 200);
                else
                    return this.Message;
            }
        }
    }
}
