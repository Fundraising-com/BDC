using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA.BDC.Core.ESubsGlobal.DirectMail.Object
{

    public class DirectMailTemplate
    {
        private int directId = int.MinValue;
        private string message = null;
        private string imageUrl = null;
        private string documentPath = null;
        private DateTime createDate = DateTime.MinValue;

        public DirectMailTemplate()
        {

        }

        #region Methods

        public static List<DirectMailTemplate> GetDirectMailTemplates()
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetDirectMailTemplates();
        }

        public static DirectMailTemplate GetDirectMailTemplateById(int directMailId)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetDirectMailTemplateById(directMailId);
        }

        #endregion

        #region Properties

        public int DirectId
        {
            get { return directId; }
            set { directId = value; }
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public string ImageUrl
        {
            get { return imageUrl; }
            set { imageUrl = value; }
        }

        public string DocumentPath
        {
            get { return documentPath; }
            set { documentPath = value; }
        }

        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        #endregion
    }
}
