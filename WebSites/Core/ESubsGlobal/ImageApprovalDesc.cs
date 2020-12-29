using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GA.BDC.Core.ESubsGlobal.DataAccess;

namespace GA.BDC.Core.ESubsGlobal
{
    public class ImageApprovalDesc
    {
        private string _approvaldescription;
        private int _approvalid;

        public ImageApprovalDesc() : this(null) { }
        public ImageApprovalDesc(string approvaldescription) : this(approvaldescription, int.MaxValue){}
        public ImageApprovalDesc(string approvaldescription, int approvalid)
        {
             _approvaldescription = approvaldescription;
             _approvalid = approvalid;

        }

        #region get/sets

        public int ApprovalId
        {
            set { _approvalid = value; }
            get { return _approvalid; }
        }

        public string ApprovalDescription
        {
            set { _approvaldescription = value; }
            get { return _approvaldescription; }
        }
        #endregion
        
        public static List<ImageApprovalDesc> GetImageApprovalDescStatus()
        {
            try
            {
                ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
                return dbo.GetImageApprovalDescStatus();
            }
            catch (Exception ex)
            {
                throw new ESubsGlobalException(ex.Message, ex);
            }
        }

    }
}
