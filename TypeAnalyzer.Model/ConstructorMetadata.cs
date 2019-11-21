using System.Reflection;
using System.Runtime.Serialization;

namespace TypeAnalyzer.Model
{
    [DataContract(Namespace = "")]
    public class ConstructorMetadata : MethodBaseMetadata
    {
        public ConstructorMetadata(ConstructorInfo constructor) : base(constructor)
        {
        }
    }
}