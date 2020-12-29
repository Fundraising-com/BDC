using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GA.BDC.Core.ESubsGlobal.DirectMail.Object;

namespace GA.BDC.Core.ESubsGlobal.DirectMail.Processing.Stages
{
    public class ConfirmProcessedStage : IStage
    {
        #region IStage Members

        public bool Execute(string filename, List<DirectMailInfo> directMailInfos)
        {
            List<int> CompletedDirectMailInfoId = new List<int>();

            foreach (DirectMailInfo directMailInfo in directMailInfos)
            {
                if (CompletedDirectMailInfoId.Contains(directMailInfo.DirectMailInfoId))
                {
                    continue;
                }

                List<DirectMail.Object.DirectMail> directMails =
                    DirectMail.Object.DirectMail.GetDirectMailInfoById(directMailInfo.DirectMailInfoId);

                foreach (DirectMail.Object.DirectMail directMail in directMails)
                {
                    directMail.DirectMailStatus = 2;
                    directMail.Update();
                }

                CompletedDirectMailInfoId.Add(directMailInfo.DirectMailInfoId);
            }

            return true;
        }

        #endregion
    }
}
