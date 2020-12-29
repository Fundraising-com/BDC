using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA.BDC.Core.ESubsGlobal.DirectMail.Processing
{
    public class DirectMailFactory
    {
        private DirectMailFactory()
        {

        }

        public static DirectMailDirector DirectMailDirector()
        {
            return new DirectMailDirector();
        }
    }
}
