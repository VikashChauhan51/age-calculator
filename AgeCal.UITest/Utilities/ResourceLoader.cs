using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AgeCal.UITest.Utilities
{
    public static class ResourceLoader
    {
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        public static Dictionary<string, string> ReadEmbededFile(string name)
        {
            var path = Path.Combine(AssemblyDirectory, name);
            using (var streamReader = new StreamReader(path))
            {
                var output = streamReader.ReadToEnd();
                var xml = XElement.Parse(output);
                return xml.Elements().ToDictionary(x => x.Name.LocalName, x => x.Value);
            }
        }
    }
}
