using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FormRegionPermission
{
    public class WorkerParameter
    {
        public int SourceFormId { get; set; }
        public int TargetFormId { get; set; }
        public string ConnectionString { get; set; }
    }
}
