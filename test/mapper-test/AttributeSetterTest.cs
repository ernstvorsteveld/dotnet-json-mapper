using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System;
using System.IO;
using System.Text;
using com.stern.json.mapper;


namespace mapper_test.attributesetter
{
    [TestClass]
    public class AttributeSetterTests
    {
        private static AttributeSetter attributeSetter;
        [ClassInitialize]
        public static void AssemblyInit(TestContext context)
        {
        }


        // [DataRow("from1.json", "my_string")]
        // [DataTestMethod]
        // public void should_map_simple_string_attribute(string json, string expectedValue)
        // {
        //     attributeSetter = new AttributeSetter()
        //     string jsonSource = File.ReadAllText(System.AppContext.BaseDirectory + jsoh);
        //     string value = mapper.Map(jsonSource);
        //     value.Should().Be(File.ReadAllText(System.AppContext.BaseDirectory + to));
        // }
    }
}
