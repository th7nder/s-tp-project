﻿using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace TypeAnalyzer.Model.Serialization
{
  public class XmlSerialization
  {
    public void WriteFile<Type>(Type dataObject, string path, string stylesheetName)
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
        xmlWriter.WriteProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" " + string.Format("href=\"{0}\"", stylesheetName));
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
