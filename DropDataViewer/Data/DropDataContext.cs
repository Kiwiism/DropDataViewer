using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace DDV.Data
{
    public class DropDataContext : DbContext
    {
        public DropDataContext(DbContextOptions<DropDataContext> options) : base(options)
        {
            dropperXmlData = new Dictionary<string, string>();

            XDocument xmlDoc = XDocument.Load("Data/XML/Mob.img.xml");
            foreach (XElement element in xmlDoc.Elements("imgdir").Elements("imgdir"))
            {
                string? key = element.Attribute("name")?.Value;
                string? value = element.Element("string")?.Attribute("value")?.Value;
                
                if (key != null && value != null)
                {
                    dropperXmlData[key] = value;
                }
            }

            itemXmlData = new Dictionary<string, KeyValuePair<string, string>>();

            xmlDoc = XDocument.Load("Data/XML/Eqp.img.xml");
            foreach (XElement elements in xmlDoc.Elements("imgdir").Elements("imgdir"))
            {
                foreach (XElement element in elements.Elements("imgdir").Elements("imgdir"))
                {
                    string? key = element.Attribute("name")?.Value;
                    string? name = string.Empty;
                    string? desc = string.Empty;

                    foreach (XElement str in element.Elements())
                    {
                        if (str.Attribute("name")?.Value == "name")
                        {
                            name = str.Attribute("value")?.Value;
                        }
                        else if (str.Attribute("name")?.Value == "desc")
                        {
                            desc = str.Attribute("value")?.Value;
                        }
                    }

                    if (key != null)
                    {
                        itemXmlData[key] = new KeyValuePair<string, string>(name, desc);
                    }
                }
            }

            xmlDoc = XDocument.Load("Data/XML/Consume.img.xml");
            foreach (XElement element in xmlDoc.Elements("imgdir").Elements("imgdir"))
            {
                string? key = element.Attribute("name")?.Value;
                string? name = string.Empty;
                string? desc = string.Empty;

                foreach (XElement str in element.Elements())
                {
                    if (str.Attribute("name")?.Value == "name")
                    {
                        name = str.Attribute("value")?.Value;
                    }
                    else if (str.Attribute("name")?.Value == "desc")
                    {
                        desc = str.Attribute("value")?.Value;
                    }
                }

                if (key != null)
                {
                    itemXmlData[key] = new KeyValuePair<string, string>(name, desc);
                }
            }

            xmlDoc = XDocument.Load("Data/XML/Etc.img.xml");
            foreach (XElement element in xmlDoc.Elements("imgdir").Elements("imgdir").Elements("imgdir"))
            {
                string? key = element.Attribute("name")?.Value;
                string? name = string.Empty;
                string? desc = string.Empty;

                foreach (XElement str in element.Elements())
                {
                    if (str.Attribute("name")?.Value == "name")
                    {
                        name = str.Attribute("value")?.Value;
                    }
                    else if (str.Attribute("name")?.Value == "desc")
                    {
                        desc = str.Attribute("value")?.Value;
                    }
                }

                if (key != null)
                {
                    itemXmlData[key] = new KeyValuePair<string, string>(name, desc);
                }
            }

            itemXmlData["0"] = new KeyValuePair<string, string>("Meso", "-");
        }

        public DbSet<Models.DropData> drop_data  { get; set; }
        public DbSet<Models.DropDataGlobal> drop_data_global  { get; set; }
        
        public readonly Dictionary<string, string> dropperXmlData;
        public readonly Dictionary<string, KeyValuePair<string, string>> itemXmlData;
    }
}
