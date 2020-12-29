using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace GA.BDC.Core.ESubsGlobal.DirectMail.Object
{

    public class DirectMail
    {
        public const int STATUS_NEW = 1;

        private int directMailId = int.MinValue;
        private int directMailInfoId = int.MinValue;
        private int directMailStatus = int.MinValue;
        private int eventParticipationId = int.MinValue;
        private int memberHierarchyId = int.MinValue;
        private int postalAddressId = int.MinValue;
        private DateTime createDate = DateTime.MinValue;

        public DirectMail()
        {

        }

        #region Methods

        public static List<DirectMail> GetDirectMailInfos()
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetDirectMail();
        }

        public static List<DirectMail> GetDirectMailInfoById(int directMailId)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetDirectMailByDirectMailInfoId(directMailId);
        }

        public static List<DirectMail> GetDirectMailSent(int eventParticipationId)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetDirectMailSent(eventParticipationId);
        }

        public static int GetDirectMailSentCount(int eventParticipationId)
        {
            List<DirectMail> directMails = GetDirectMailSent(eventParticipationId);

            directMails = directMails.Where(p => p.DirectMailStatus != DirectMailInfo.STATUS_DRAFT).ToList();


            return directMails.Count;
        }

        public bool Insert()
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.InsertDirectMail(null, this);
        }

        public bool Update()
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.UpdateDirectMail(null, this);
        }

        public PdfDocument GetPdfDocumentPreview()
        {
            //PdfReader reader = PdfReader.Open("C:\\Documents and Settings\\jbuist0033\\Desktop\\DM\\letter.pdf");
           // PdfDocument pdfDocument = new PdfDocument();

            PdfDocument pdfDocument = PdfReader.Open("C:\\Documents and Settings\\jbuist0033\\Desktop\\DM\\letter.pdf");
            return pdfDocument;
        }

        #endregion

        #region Properties

        public int DirectMailId
        {
            get { return directMailId; }
            set { directMailId = value; }
        }

        public int DirectMailInfoId
        {
            get { return directMailInfoId; }
            set { directMailInfoId = value; }
        }

        public int DirectMailStatus
        {
            get { return directMailStatus; }
            set { directMailStatus = value; }
        }

        public int EventParticipationId
        {
            get { return eventParticipationId; }
            set { eventParticipationId = value; }
        }

        public int MemberHierarchyId
        {
            get { return memberHierarchyId; }
            set { memberHierarchyId = value; }
        }

        public int PostalAddressId
        {
            get { return postalAddressId; }
            set { postalAddressId = value; }
        }

        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        #endregion
    }
}
