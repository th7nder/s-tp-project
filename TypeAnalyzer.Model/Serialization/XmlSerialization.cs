using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TypeAnalyzer.Model.Serialization
{
  public class XmlSerialization
  {
    public void WriteFile<Type>(Type dataObject, string path)
    {
      if (string.IsNullOrEmpty(path))
        throw new ArgumentNullException("Path cannot be empty");
      if (dataObject == null)
        throw new ArgumentNullException("Object cannot be null");

      DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(Type), null, 655360, true, true, null);
      XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
      {
        Indent = true,
        IndentChars = "  ",
        NewLineChars = "\r\n"
      };
      using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
      using (XmlWriter xmlWriter = XmlWriter.Create(fileStream, xmlWriterSettings))
      {
        dataContractSerializer.WriteObject(xmlWriter, dataObject);
      }
    }

    public Type ReadFile<Type>(string path, FileMode mode)
    {
      if (string.IsNullOrEmpty(path))
        throw new ArgumentNullException("Path cannot be empty");

      DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(Type));
      Type dataObject = default;
      using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
      {
        dataObject = (Type)dataContractSerializer.ReadObject(fileStream);
      }

      return dataObject;
    }
  }
}
