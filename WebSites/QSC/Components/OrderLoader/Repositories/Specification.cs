using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Business
{
    public class Specification<E> : ISpecification<E>
    {
        #region Private Members

        private Func<E, bool> _evalFunc = null;
        private Expression<Func<E, bool>> _evalPredicate;

        #endregion

        #region Virtual Accessors

        public virtual Expression<Func<E, bool>> EvalPredicate
        {
            get { return _evalPredicate; }
        }

        public virtual Func<E, bool> EvalFunc
        {
            get { return _evalFunc; }
        }

        #endregion

        #region Constructors

        public Specification(Expression<Func<E, bool>> predicate)
        {
            _evalPredicate = predicate;
        }

        private Specification() { }

        #endregion
    }
}
