using System.Collections.Generic;

namespace AssemblyBrowserLibrary.Structures
{
    public class AssemblyInfo
    {
        public Dictionary<string, NameSpaceInfo> NamespaceInfos { get; }

        public AssemblyInfo()
        {
            NamespaceInfos = new Dictionary<string, NameSpaceInfo>();
        }
    }
}