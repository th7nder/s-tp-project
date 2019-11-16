using System.Reflection;
using System.Runtime.Serialization;

namespace TypeAnalyzer.Model
{
    static class AccessModifierUtils
    {
        public static AccessModifier GetAccessModifier(this TypeInfo typeInfo) {
            if (typeInfo.IsPublic)
                return AccessModifier.Public;
            if (typeInfo.IsNotPublic)
                return AccessModifier.Internal;
            if (typeInfo.IsNestedPrivate)
                return AccessModifier.Private;
            if (typeInfo.IsNestedFamily)
                return AccessModifier.Protected;
            if (typeInfo.IsNestedFamORAssem)
                return AccessModifier.ProtectedInternal;

            return AccessModifier.None;
        }
    }

    [DataContract]
    public enum AccessModifier
    {
        [EnumMember]
        Private,
        [EnumMember]
        Protected,
        [EnumMember]
        ProtectedInternal,
        [EnumMember]
        Internal,
        [EnumMember]
        Public,
        None
    }
}