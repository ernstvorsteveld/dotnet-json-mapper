using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;


namespace com.stern.json.mapper.converters
{
    public enum AttributeType
    {
        @string,
        @numeric
    }
    public class ConverterProvider
    {
        private IDictionary<AttributeType, Converter> converters;

        public ConverterProvider(IDictionary<AttributeType, Converter> c)
        {
            converters = c;
        }

        public Converter GetConverter(AttributeType t)
        {
            return converters[t];
        }
    }

    public class ConverterProviderBuilder
    {
        private IDictionary<AttributeType, Converter> converters = new Dictionary<AttributeType, Converter>();

        public static ConverterProviderBuilder New() 
        {
            return new ConverterProviderBuilder();
        }

        public ConverterProviderBuilder converter(AttributeType t, Converter converter)
        {
            converters.Add(t, converter);
            return this;
        }
        public ConverterProvider build()
        {
            return new ConverterProvider(converters);
        }
    }

    public interface Converter
    {
        public string from(JToken token, MappingConfig mappingConfig);
        public T to<T>(string value, MappingConfig mappingConfig);
    }
    public class StringConverter : Converter
    {
        public string from(JToken token, MappingConfig mappingConfig)
        {
            return token.First.First.Value<string>();
        }
        public String to<String>(string value, MappingConfig mappingConfig)
        {
            return default(String);
        }
    }
}
