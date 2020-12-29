using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GA.BDC.Core.ESubsGlobal.DirectMail.Object;

namespace GA.BDC.Core.ESubsGlobal.DirectMail.Processing.Stages
{
    interface IStage
    {
        bool Execute(string filename, List<DirectMailInfo> directMailInfo);
    }
}
