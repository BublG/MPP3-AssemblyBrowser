using System;
using System.Collections.Generic;
using System.Linq;
using AssemblyBrowserLibrary;
using AssemblyBrowserLibrary.Structures;
using NUnit.Framework;

namespace LibraryTest
{
    public class LibraryTest
    {
        private const string Path = "D:\\учеба\\СПП\\AssemblyBrowser\\LibraryTest\\obj\\Debug\\net5.0\\LibraryTest.dll";
        private const string TestClassName = "TestClassPerson";
        private const string TestNamespaceName = "LibraryTest";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetAssemblyInfoTest()
        {
            AssemblyInfo emptyAssemblyInfo = AssemblyBrowser.GetAssemblyInfo("bad path");
            Assert.AreEqual(emptyAssemblyInfo.NamespaceInfos.Count, 0);
            
            AssemblyInfo assemblyInfo = AssemblyBrowser.GetAssemblyInfo(Path);
            Assert.True(assemblyInfo.NamespaceInfos.ContainsKey(TestNamespaceName));

            Type personType = typeof(TestClassPerson);
            DataTypeInfo personTypeInfo = new DataTypeInfo(TestClassName, personType.GetFields(),
                personType.GetProperties(), personType.GetMethods());
            List<DataTypeInfo> dataTypeInfos = assemblyInfo.NamespaceInfos[TestNamespaceName].DataTypeInfos;
            DataTypeInfo loadedPersonTypeInfo = null;
            foreach (var dataTypeInfo in dataTypeInfos)
            {
                if (dataTypeInfo.Name.Equals(TestClassName))
                {
                    loadedPersonTypeInfo = dataTypeInfo;
                }
            }

            Assert.NotNull(loadedPersonTypeInfo);
            Assert.AreEqual(personTypeInfo.FieldInfos.Length, loadedPersonTypeInfo.FieldInfos.Length);
            Assert.AreEqual(personTypeInfo.PropertyInfos.Length, loadedPersonTypeInfo.PropertyInfos.Length);
            Assert.AreEqual(personTypeInfo.MethodInfos.Length, loadedPersonTypeInfo.MethodInfos.Length);
            for (int i = 0; i < personTypeInfo.FieldInfos.Length; i++)
            {
                Assert.AreEqual(personTypeInfo.FieldInfos[i].Name, loadedPersonTypeInfo.FieldInfos[i].Name);
            }

            for (int i = 0; i < personTypeInfo.PropertyInfos.Length; i++)
            {
                Assert.AreEqual(personTypeInfo.PropertyInfos[i].Name, loadedPersonTypeInfo.PropertyInfos[i].Name);
            }

            for (int i = 0; i < personTypeInfo.MethodInfos.Length; i++)
            {
                Assert.AreEqual(personTypeInfo.MethodInfos[i].Name, loadedPersonTypeInfo.MethodInfos[i].Name);
            }
        }
    }
}