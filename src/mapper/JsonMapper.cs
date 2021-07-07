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
        private Mappings mappings;

        public JsonMapper(string file)
        {
            mappings = new MappingReader(file).Read();
        }

        public string Map(string from)
        {
            var json = JObject.Parse(from);

            var output = JObject.Parse("{}");
            foreach (var element in mappings.mappings)
            {
                output = MapElement(element, json, output);
            }
            return output.ToString();
        }
        private JObject MapElement(Dictionary<string, MappingConfig> mappingConfig, JObject input, JObject output)
        {
            string[] toAttrs = mappingConfig["to"].name.Split(".", StringSplitOptions.None);
            var value = new ValueGetter(input).getValue(mappingConfig["from"]);
            Console.WriteLine($"Value: {value}");
            new AttributeSetter(output).setAttribute(mappingConfig["to"], value);
            return output;
        }
    }

    class AttributeSetter
    {
        private JObject output;

        public AttributeSetter(JObject output)
        {
            this.output = output;
        }

        public void setAttribute(MappingConfig mappingConfig, Object valueObject)
        {
            var value = (string)valueObject;
            if (value is null)
            {
                return;
            }
            string[] toAttributes = mappingConfig.name.Split('.');
            foreach (var attribute in toAttributes)
            {
                if (this.output[attribute] is null)
                {
                    this.output.Add(new JProperty(attribute, value.ToString()));
                }
                // Console.WriteLine($"SetAttribute: {output}");
            }
        }
    }

    class ValueGetter
    {
        private JObject input;
        public ValueGetter(JObject input)
        {
            this.input = input;
        }
        public Object getValue(MappingConfig mappingConfig)
        {
            // Console.WriteLine($"GetValue: mappingConfig: {mappingConfig}, name {mappingConfig.name}.");
            string[] fromAttrs = mappingConfig.name.Split('.');
            var value = getValueToken(input, fromAttrs);
            return value;
        }

        private Object getValueToken(JToken input, string[] attrs)
        {
            Console.WriteLine($"getValueToken: input: {input}, attrs: {attrs[0]}, attrslength {attrs.Length}.");
            if (input is null)
            {
                return null;
            }
            if (attrs.Length == 1)
            {
                Console.WriteLine("is length 1");
                if (input[attrs[0]] is null)
                {
                    return null;
                }
                return input[attrs[0]].ToString();
            }
            else
            {
                var item = input[attrs[0]];
                return getValueToken(item, attrs.Skip(1).Take(attrs.Length - 1).ToArray());
            }
        }
    }

    public class MappingReadException : Exception
    {
        public MappingReadException(string message) : base(message)
        {
        }
    }
    public class TypeNotImplementedException : Exception
    {
        public TypeNotImplementedException(string message) : base(message)
        {
        }
    }
}