using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GA.BDC.Core.ESubsGlobal.DirectMail.Object;
using GA.BDC.Core.ESubsGlobal.DirectMail.Document;
using GA.BDC.Core.ESubsGlobal.Users;
using GA.BDC.Core.ESubsGlobal.Common;
using System.Windows.Forms;
using System.Collections;

namespace GA.BDC.Core.ESubsGlobal.DirectMail.Processing.Stages
{
    public class PreviewPDFStage
    {

        public ArrayList Preview(List<DirectMailInfo> directMailInfos)
        {
            ArrayList preview = new ArrayList();
            ArrayList namesAndAddresses = new ArrayList();

            preview.Add("Total number of Direct Mails Batches ready to be processed: " + directMailInfos.Count);

            foreach (DirectMailInfo directMailInfo in directMailInfos)
            {
                List<DirectMail.Object.DirectMail> directMails = 
                    DirectMail.Object.DirectMail.GetDirectMailInfoById(directMailInfo.DirectMailInfoId);

                foreach (DirectMail.Object.DirectMail directMail in directMails)
                {
                    GA.BDC.Core.ESubsGlobal.EventParticipation eventParticipation =
                        GA.BDC.Core.ESubsGlobal.EventParticipation.GetEventParticipationByEventParticipationID(directMail.EventParticipationId);

                    GA.BDC.Core.ESubsGlobal.Event e =
                        GA.BDC.Core.ESubsGlobal.Event.GetEventByEventID(eventParticipation.EventID);

                    UnknownUser sender = UnknownUser.LoadByHierarchyID(directMailInfo.MemberHierarchyId);

                    UnknownUser recipient = UnknownUser.LoadByHierarchyID(eventParticipation.MemberHierarchyID);
                    PostalAddress postalAddress = recipient.PostalAddresses[0];

                    namesAndAddresses.Add(e.Name + " From: " + sender.Name + " to: " + recipient.Name + " at " + postalAddress.ToString());
                }
            }

            preview.Add("Total number of letters to send: " + namesAndAddresses.Count);

            foreach (string nameAndAddress in namesAndAddresses)
            {
                preview.Add(nameAndAddress);
            }

            return preview;
        }

    }
}
