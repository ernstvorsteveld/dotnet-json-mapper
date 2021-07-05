using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace com.stern.json.mapper
{
    public class JsonMapper : Mapper
    {
        private Mappings config;

        public JsonMapper(string file)
        {
            config = new MappingReader(file).Read();
        }

        public string Map(string from)
        {
            var json = JObject.Parse(from);
            // JArray mappings = (JArray)config["mappings"];
            // Console.WriteLine(config);


            // foreach (MappingSpecification element in config.mappingConfigs)
            // {

            // }

            return json.SelectToken("simple_string_attr").Value<string>();
        }
    }

    public class MappingReadException : Exception
    {
        public MappingReadException(string message) : base(message)
        {
        }
    }
}