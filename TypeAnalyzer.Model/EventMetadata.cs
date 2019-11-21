using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace TypeAnalyzer.Model
{
  [DataContract(Namespace = "")]
  public class EventMetadata
  {
    [DataMember]
    public string Name { get; private set; }
    [DataMember]
    public IEnumerable<AttributeMetadata> Attributes { get; private set; } = new List<AttributeMetadata>();
    [DataMember]
    public bool IsMulticast { get; set; }
    [DataMember]
    public TypeMetadata EventType { get; set; }

    public EventMetadata(EventInfo eventInfo)
    {
      Name = eventInfo.Name;
      Attributes = from attribute in eventInfo.CustomAttributes
                   select AttributeMetadata.Analyze(attribute);
      IsMulticast = eventInfo.IsMulticast;
      EventType = TypeMetadata.Analyze(eventInfo.EventHandlerType.GetTypeInfo());
    }
  }
}