using System.Reflection;
using System.Runtime.Serialization;

namespace TypeAnalyzer.Model
{
    [DataContract]
    public class ConstructorMetadata : MethodBaseMetadata
    {
        public ConstructorMetadata(ConstructorInfo constructor) : base(constructor)
        {
        }
    }
}