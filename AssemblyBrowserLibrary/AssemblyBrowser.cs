using System;
using System.Reflection;
using AssemblyBrowserLibrary.Structures;

namespace AssemblyBrowserLibrary
{
    public class AssemblyBrowser
    {
        public static AssemblyInfo GetAssemblyInfo(string path)
        {
            try
            {
                AssemblyInfo assemblyInfo = new AssemblyInfo();
                Assembly assembly = Assembly.LoadFile(path);
                foreach (Type type in assembly.GetTypes())
                {
                    string typeNamespace = type.Namespace;
                    if (typeNamespace != null)
                    {
                        if (!assemblyInfo.NamespaceInfos.ContainsKey(typeNamespace))
                        {
                            assemblyInfo.NamespaceInfos.Add(typeNamespace, new NameSpaceInfo());
                        }

                        assemblyInfo.NamespaceInfos[typeNamespace].DataTypeInfos.Add(
                            new DataTypeInfo(type.Name, type.GetFields(), type.GetProperties(),
                                type.GetMethods()));
                    }
                }

                return assemblyInfo;
            }
            catch (Exception e)
            {
                return new AssemblyInfo();
            }
        }

        public static Node BuildTree(AssemblyInfo assemblyInfo)
        {
            var root = new Node();
            foreach (var keyValue in assemblyInfo.NamespaceInfos)
            {
                Node namespaceNode = new Node();
                namespaceNode.Name = "Namespace: " + keyValue.Key;
                root.Child.Add(namespaceNode);
                foreach (DataTypeInfo dataTypeInfo in keyValue.Value.DataTypeInfos)
                {
                    Node dataTypeInfoNode = new Node();
                    dataTypeInfoNode.Name = "Class: " + dataTypeInfo.Name;
                    AddMethodsNodes(dataTypeInfo.MethodInfos, dataTypeInfoNode);
                    AddFieldsNodes(dataTypeInfo.FieldInfos, dataTypeInfoNode);
                    AddPropertyNodes(dataTypeInfo.PropertyInfos, dataTypeInfoNode);
                    namespaceNode.Child.Add(dataTypeInfoNode);
                }
            }

            return root;
        }

        private static void AddMethodsNodes(MethodInfo[] methodInfos, Node dataTypeInfoNode)
        {
            foreach (MethodInfo method in methodInfos)
            {
                Node methodInfoNode = new Node();
                string parametersString = null;
                ParameterInfo[] parameters = method.GetParameters();
                foreach (var parameter in parameters)
                {
                    parametersString += parameter.ParameterType.Name + " " + parameter.Name + ", ";
                }

                if (string.IsNullOrEmpty(parametersString))
                {
                    parametersString = parametersString.Substring(0, parametersString.Length - 2);
                }
                
                methodInfoNode.Name = "Method: " + (method.IsPublic ? "public " : "") +
                                      (method.IsPrivate ? "private " : "") + (method.IsStatic ? "static " : "") +
                                      method.ReturnType.Name + " " + method.Name + "(" + parametersString + ")";
                dataTypeInfoNode.Child.Add(methodInfoNode);
            }
        }

        private static void AddFieldsNodes(FieldInfo[] fieldInfos, Node dataTypeInfoNode)
        {
            foreach (FieldInfo field in fieldInfos)
            {
                Node fieldInfo = new Node();
                fieldInfo.Name = "Field: " + field.Name + " " + field.FieldType.Name;
                dataTypeInfoNode.Child.Add(fieldInfo);
            }
        }

        private static void AddPropertyNodes(PropertyInfo[] propertyInfos, Node dataTypeInfoNode)
        {
            foreach (PropertyInfo property in propertyInfos)
            {
                Node propertyInfo = new Node();
                propertyInfo.Name = "Property: " + property.Name + " " + property.PropertyType.Name;
                dataTypeInfoNode.Child.Add(propertyInfo);
            }
        }
    }
}