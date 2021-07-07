using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System;
using System.IO;
using System.Text;
using com.stern.json.mapper;


namespace mapper_test.valuegetter
{
    [TestClass]
    public class AttributeSetterTests
    {
        private static ValueGetter valueGetter;
        [ClassInitialize]
        public static void AssemblyInit(TestContext context)
        {

        }


        [DataRow("from1.json", "my_string")]
        [DataTestMethod]
        public void should_get_value_from_json(string json, string expectedValue)
        {
            valueGetter = new ValueGetter(File.ReadAllText(System.AppContext.BaseDirectory + json));
            string value = valueGetter.getValue(new MappingConfig
            {
                name = "simple_string_attr",
                type = "string"
            });
            value.Should().Be(expectedValue);
        }
    }
}
