using System;
using QSPForm.Common.DataDef;

namespace QSP.OrderExpress.Web.Code {
    /// <summary>
    /// Summary description for BaseOrderDetailList.
    /// </summary>
    public class BaseOrderDetailList : BaseWebUserControl {
        public virtual OrderData DataSource {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        public virtual void BindForm() {
            throw new NotImplementedException();
        }

        public virtual int FormID {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public virtual bool UpdateDataSource() {
            throw new NotImplementedException();
        }

        public virtual bool ValidateForm() {
            throw new NotImplementedException();
        }

        public virtual int ParentID {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}