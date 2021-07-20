using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System;
using Newtonsoft.Json.Linq;
using com.stern.json.mapper;
using com.stern.json.mapper.converters;

namespace mapper_test.converters
{
    [TestClass]
    public class ConvertersTest
    {
        private ConverterProvider convertProvider;
        private MappingConfig mappingConfig;

        [DataRow(AttributeType.@string, "simple-value", "simple-value")]
        [DataTestMethod]
        public void should_map_simple_string(AttributeType type, Object value, string expected)
        {
            givenConvertersAreConfigured();
            givenMappingConfigForString(new MappingConfig { name = "_", type = "string" });

            var actual = whenConverting(type, value);

            thanAssert(actual, expected);
        }

        private void givenConvertersAreConfigured()
        {
            this.convertProvider =
            ConverterProviderBuilder
                .New()
                .converter(AttributeType.@string, new StringConverter())
                .build();
        }

        private void givenMappingConfigForString(MappingConfig mappingConfig)
        {
            this.mappingConfig = mappingConfig;
        }

        string whenConverting(AttributeType type, Object value)
        {
            JToken token = null;
            switch (type)
            {
                case AttributeType.@string: 
                    token = JToken.Parse($"{{ 'value' : '{value}' }}");
                    break;
                default:
                    throw new NotImplementedException("Type not implemented yet");
            }

            return convertProvider.GetConverter(type).from(token, this.mappingConfig);
        }

        void thanAssert(string actual, string expected)
        {
            actual.Should().Be(expected);
        }
    }
}
