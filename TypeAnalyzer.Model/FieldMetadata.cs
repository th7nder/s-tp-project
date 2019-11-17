using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace TypeAnalyzer.Model
{
    [DataContract]
    public class FieldMetadata
    {
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public TypeMetadata Type { get; private set; }
        [DataMember]
        public IEnumerable<AttributeMetadata> Attributes { get; private set; }
        [DataMember] 
        public AccessModifier AccessModifier { get; set; }
        
        public FieldMetadata(FieldInfo fieldInfo)
        {
            Name = fieldInfo.Name;
            Type = TypeMetadata.Analyze(fieldInfo.FieldType.GetTypeInfo());
            Attributes = from attribute in fieldInfo.CustomAttributes
                         select AttributeMetadata.Analyze(attribute);
            AccessModifier = fieldInfo.GetAccessModifier();
        }
    }
}