using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GA.BDC.Core.ESubsGlobal.DirectMail.Processing.Stages;
using GA.BDC.Core.ESubsGlobal.DirectMail.Object;
using System.Collections;

namespace GA.BDC.Core.ESubsGlobal.DirectMail.Processing
{
    public class DirectMailDirector
    {
        private List<IStage> stages = null;

        public DirectMailDirector()
        {
            this.stages = new List<IStage>();

            this.stages.Add(new GeneratePDFStage());
            this.stages.Add(new ConfirmProcessedStage());
        }

        public ArrayList Preview()
        {
            List<DirectMailInfo> directMailInfos = DirectMailInfo.GetDirectMailInfosReadyToBeProcessed();

            PreviewPDFStage previewPdfState = new PreviewPDFStage();

            return previewPdfState.Preview(directMailInfos);

        }

        public void Execute(string filename)
        {
            List<DirectMailInfo> directMailInfos = DirectMailInfo.GetDirectMailInfosReadyToBeProcessed();

            foreach (IStage stage in this.stages)
            {
                stage.Execute(filename, directMailInfos);
            }
        }

        public void ExecuteTest(string filename)
        {
            List<DirectMailInfo> directMailInfos = DirectMailInfo.GetDirectMailInfosReadyToBeProcessed();

            foreach (IStage stage in this.stages)
            {
                if (! (stage is ConfirmProcessedStage))
                {
                    stage.Execute(filename, directMailInfos);
                }
            }
        }
    }
}
