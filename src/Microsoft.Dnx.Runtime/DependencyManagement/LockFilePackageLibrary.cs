using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Dnx.Runtime.DependencyManagement
{
    public class LockFilePackageLibrary : LockFileLibrary
    {
        public bool IsServiceable { get; set; }

        public string Sha512 { get; set; }

        public IList<string> Files { get; set; } = new List<string>();
    }
}
