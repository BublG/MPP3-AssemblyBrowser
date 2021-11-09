using System.Reflection;

namespace AssemblyBrowserLibrary.Structures
{
    public class DataTypeInfo
    {
        public string Name { get; }
        public FieldInfo[] FieldInfos { get; }
        public PropertyInfo[] PropertyInfos { get; }
        public MethodInfo[] MethodInfos { get; }

        public DataTypeInfo(string name, FieldInfo[] fieldInfos, PropertyInfo[] propertyInfos, MethodInfo[] methodInfos)
        {
            Name = name;
            FieldInfos = fieldInfos;
            PropertyInfos = propertyInfos;
            MethodInfos = methodInfos;
        }
    }
}