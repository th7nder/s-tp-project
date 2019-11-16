using System.Reflection;

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

    public enum AccessModifier
    {
        Private,
        Protected,
        ProtectedInternal,
        Internal,
        Public,
        None
    }
}