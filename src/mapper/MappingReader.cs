using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;


namespace com.stern.json.mapper
{
    public class MappingReader
    {
        private string json;
        public MappingReader(string input)
        {
            this.json = File.ReadAllText(input);
        }
        public Mappings Read()
        {
            Mappings specification = JsonSerializer.Deserialize<Mappings>(json);
            return specification;
        }
    }

    public class Mappings
    {
        public IList<Dictionary<string, MappingConfig>> mappings { get; set; }

        public override string ToString()
        {
            var text = "Mappings: \n";
            foreach (var element in mappings)
            {
                text += " Element:(from)\n " + element["from"] + ",\n";
                text += " Element:(to)\n " + element["to"] + ",\n";
            }
            return text;
        }
    }

    public class MappingConfig
    {
        public string name { get; set; }
        public string type { get; set; }

        public override string ToString()
        {
            var text = " MappingConfig: ";
            text += "\n   Name: " + name + "\n   Type: " + type;
            return text;
        }
    }
}
