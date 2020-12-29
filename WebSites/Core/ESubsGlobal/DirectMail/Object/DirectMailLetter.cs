using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA.BDC.Core.ESubsGlobal.DirectMail.Object
{

    public class DirectMailLetter
    {
        public const int STATUS_NORMAL = 1;
        public const int STATUS_BAR_CODED = 2;

        private int directMailLetterId = int.MinValue;
        private int directMailId = int.MinValue;
        private string letterBarCode1 = null;
        private string letterBarCode2 = null;
        private int letterType = int.MinValue;
        private DateTime createDate = DateTime.MinValue;

        public DirectMailLetter()
        {

        }

        #region Methods

        public static List<DirectMailLetter> GetDirectMailLetters()
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetDirectMailLetters();
        }

        public static DirectMailLetter GetDirectMailLetterById(int directMailLetterId)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetDirectMailLetterById(directMailLetterId);
        }

        public bool Insert()
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.InsertDirectMailLetter(null, this);
        }

        public int Update()
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.UpdateDirectMailLetter(null, this);
        }

        #endregion

        #region Properties

        public int DirectMailLetterId
        {
            get { return directMailLetterId; }
            set { directMailLetterId = value; }
        }

        public int DirectMailId
        {
            get { return directMailId; }
            set { directMailId = value; }
        }

        public string LetterBarCode1
        {
            get { return letterBarCode1; }
            set { letterBarCode1 = value; }
        }

        public string LetterBarCode2
        {
            get { return letterBarCode2; }
            set { letterBarCode2 = value; }
        }

        public int LetterType
        {
            get { return letterType; }
            set { letterType = value; }
        }

        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        #endregion
    }
}
