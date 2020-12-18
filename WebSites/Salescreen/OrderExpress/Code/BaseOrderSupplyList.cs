using System;

namespace QSP.OrderExpress.Web.Code {
    /// <summary>
    /// Summary description for BaseOrderSuplyList.
    /// </summary>
    public class BaseOrderSupplyList : BaseWebUserControl {
        public virtual void BindForm() {
            throw new NotImplementedException();
        }

        public virtual int ParentID {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public virtual int FormID {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public virtual QSPForm.Common.DataDef.OrderDetailTable DataSource {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public virtual int DefaultQuantity {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public virtual bool UpdateDataSource() {
            throw new NotImplementedException();
        }

        public virtual bool ValidateForm() {
            throw new NotImplementedException();
        }
    }
}