using System.Collections.Generic;

namespace AssemblyBrowserLibrary.Structures
{
    public class NameSpaceInfo
    {
        public List<DataTypeInfo> DataTypeInfos { get; }

        public NameSpaceInfo()
        {
            DataTypeInfos = new List<DataTypeInfo>();
        }
    }
}