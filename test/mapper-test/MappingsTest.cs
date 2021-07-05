using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System;
using System.IO;
using System.Text;
using com.stern.json.mapper;


namespace mapper_test
{
    [TestClass]
    public class TestMappings
    {
        private static Mapper mapper;
        [ClassInitialize]
        public static void AssemblyInit(TestContext context)
        {
            // mapper = new JsonMapper(System.AppContext.BaseDirectory + "mapper-config.json");
        }


        [DataRow("from1.json", "to1.json")]
        [DataTestMethod]
        public void should_map_simple_string_attribute(string from, string to)
        {
            mapper = new JsonMapper(System.AppContext.BaseDirectory + "mapper-config.json");
            string jsonSource = File.ReadAllText(System.AppContext.BaseDirectory + from);
            string value = mapper.Map(jsonSource);
            value.Should().Be(File.ReadAllText(System.AppContext.BaseDirectory + to));
        }
    }
}
