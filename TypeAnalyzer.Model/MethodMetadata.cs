using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace TypeAnalyzer.Model
{

  [DataContract(Namespace = "")]
  public class MethodMetadata : MethodBaseMetadata
  { 
    [DataMember]
    public TypeMetadata ReturnType { get; private set; }
    [DataMember]
    public IEnumerable<TypeMetadata> GenericArguments { get; private set; }
    [DataMember]
    public bool IsExtensionMethod { get; private set; }
    
    public MethodMetadata(MethodInfo method) : base(method)
    {
      IsExtensionMethod = method.IsDefined(typeof(ExtensionAttribute), false);
      ReturnType = TypeMetadata.Analyze(method.ReturnType.GetTypeInfo());
      GenericArguments = from generic in method.GetGenericArguments()
                         select TypeMetadata.Analyze(generic.GetTypeInfo());
    }
  }
}
