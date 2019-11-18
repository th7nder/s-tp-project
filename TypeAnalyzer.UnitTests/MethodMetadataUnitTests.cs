 using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TypeAnalyzer.Model;

namespace TypeAnalyzer.UnitTests
{
  [TestClass]
  public class MethodMetadataUnitTests
  {

    [TestMethod]
    public void MethodMetadata_LoadsParametersAndReturnType()
    {
      NamespaceMetadata namespaceMetadata = TestReflector.Reflector.AssemblyMetadata.Namespaces.First();
      TypeMetadata methodType = namespaceMetadata.Types.Single(type => type.Name == "BaseClass");

      MethodMetadata methodMetadata = methodType.Methods.Single(method => method.Name == "Test1");

      Assert.AreEqual("Void", methodMetadata.ReturnType.Name);
      Assert.AreEqual(4, methodMetadata.Parameters.Count());
    }

    [TestMethod]
    public void MethodMetadata_LoadsAttributes()
    {
      NamespaceMetadata namespaceMetadata = TestReflector.Reflector.AssemblyMetadata.Namespaces.First();
      TypeMetadata methodType = namespaceMetadata.Types.Single(type => type.Name == "BaseClass");

      MethodMetadata methodMetadata = methodType.Methods.Single(method => method.Name == "Test1");

      Assert.IsNotNull(methodMetadata.Attributes.Single(attribute => attribute.Type.Name == "TestAttribute"));
    }

    [TestMethod]
    public void MethodMetadata_GivesAccessModifier()
    {
      NamespaceMetadata namespaceMetadata = TestReflector.Reflector.AssemblyMetadata.Namespaces.First();
      TypeMetadata containingType = namespaceMetadata.Types.Single(type => type.Name == "MethodTest");

      AccessModifier internalTypeModifier = containingType.Methods.Single(method => method.Name == "Internal").AccessModifier;
      AccessModifier privateTypeModifier = containingType.Methods.Single(method => method.Name == "Private").AccessModifier;
      AccessModifier publicTypeModifier = containingType.Methods.Single(method => method.Name == "Public").AccessModifier;
      AccessModifier protectedTypeModifier = containingType.Methods.Single(method => method.Name == "Protected").AccessModifier;
      AccessModifier protectedInternalTypeModifier = containingType.Methods.Single(method => method.Name == "ProtectedInternal").AccessModifier;
      AccessModifier privateProtectedTypeModifier = containingType.Methods.Single(method => method.Name == "PrivateProtected").AccessModifier;

      Assert.AreEqual(AccessModifier.Internal, internalTypeModifier);
      Assert.AreEqual(AccessModifier.Private, privateTypeModifier);
      Assert.AreEqual(AccessModifier.Public, publicTypeModifier);
      Assert.AreEqual(AccessModifier.Protected, protectedTypeModifier);
      Assert.AreEqual(AccessModifier.ProtectedInternal, protectedInternalTypeModifier);
      Assert.AreEqual(AccessModifier.PrivateProtected, privateProtectedTypeModifier);
    }

    [TestMethod]
    public void MethodMetadata_GivesMethodModifier()
    {
      NamespaceMetadata namespaceMetadata = TestReflector.Reflector.AssemblyMetadata.Namespaces.First();
      TypeMetadata containingType = namespaceMetadata.Types.Single(type => type.Name == "MethodTest");
      TypeMetadata containingSealedType = namespaceMetadata.Types.Single(type => type.Name == "MethodFinalTest");

      MethodModifier abstractModifier = containingType.Methods.Single(method => method.Name == "Abstract").MethodModifier;
      MethodModifier staticModifier = containingType.Methods.Single(method => method.Name == "Static").MethodModifier;
      MethodModifier virtualModifier = containingType.Methods.Single(method => method.Name == "Virtual").MethodModifier;
      MethodModifier sealedModifier = containingSealedType.Methods.Single(method => method.Name == "Virtual").MethodModifier;

      Assert.AreEqual(MethodModifier.Abstract, abstractModifier);
      Assert.AreEqual(MethodModifier.Final, sealedModifier);
      Assert.AreEqual(MethodModifier.Static, staticModifier);
      Assert.AreEqual(MethodModifier.Virtual, virtualModifier);
    }

    [TestMethod]
    public void MethodMetadata_LoadsGenericArguments()
    {
      NamespaceMetadata namespaceMetadata = TestReflector.Reflector.AssemblyMetadata.Namespaces.First();
      TypeMetadata containingType = namespaceMetadata.Types.Single(type => type.Name == "NestedPrivate<K>");

      MethodMetadata methodMetadata = containingType.Methods.Single(method => method.Name == "PrivateMethod");

      Assert.AreEqual(1, methodMetadata.GenericArguments.Count());
    }

    [TestMethod]
    public void MethodMetadata_ChecksExtensionMethod()
    {
      NamespaceMetadata namespaceMetadata = TestReflector.Reflector.AssemblyMetadata.Namespaces.First();
      TypeMetadata containingType = namespaceMetadata.Types.Single(type => type.Name == "ExtensionClass");

      MethodMetadata methodMetadata = containingType.Methods.Single(method => method.Name == "ExtensionMethod");

      Assert.IsTrue(methodMetadata.IsExtensionMethod);
    }

    [TestMethod]
    public void MethodParameterMetadata_GivesKind()
    {
      NamespaceMetadata namespaceMetadata = TestReflector.Reflector.AssemblyMetadata.Namespaces.First();
      TypeMetadata methodType = namespaceMetadata.Types.Single(type => type.Name == "BaseClass");
      MethodMetadata methodMetadata = methodType.Methods.Single(method => method.Name == "Test1");

      MethodParameterKind kindIn = methodMetadata.Parameters.Single(param => param.Name == "first").Kind;
      MethodParameterKind kindOut = methodMetadata.Parameters.Single(param => param.Name == "second").Kind;
      MethodParameterKind kindRef = methodMetadata.Parameters.Single(param => param.Name == "third").Kind;
      MethodParameterKind kindNone = methodMetadata.Parameters.Single(param => param.Name == "optional").Kind;

      Assert.AreEqual(MethodParameterKind.In, kindIn);
      Assert.AreEqual(MethodParameterKind.None, kindNone);
      Assert.AreEqual(MethodParameterKind.Out, kindOut);
      Assert.AreEqual(MethodParameterKind.Ref, kindRef);

    }

    [TestMethod]
    public void MethodParameterMetadata_LoadsAttributes()
    {
      NamespaceMetadata namespaceMetadata = TestReflector.Reflector.AssemblyMetadata.Namespaces.First();
      TypeMetadata methodType = namespaceMetadata.Types.Single(type => type.Name == "BaseClass");
      MethodMetadata methodMetadata = methodType.Methods.Single(method => method.Name == "Test1");

      MethodParameterMetadata methodParameterMetadata = methodMetadata.Parameters.Single(param => param.Name == "optional");

      Assert.IsNotNull(methodParameterMetadata.Attributes.Single(attr => attr.Type.Name == "TestAttribute"));
    }

    [TestMethod]
    public void MethodParameterMetadata_GetsDefaultValue()
    {
      NamespaceMetadata namespaceMetadata = TestReflector.Reflector.AssemblyMetadata.Namespaces.First();
      TypeMetadata methodType = namespaceMetadata.Types.Single(type => type.Name == "BaseClass");
      MethodMetadata methodMetadata = methodType.Methods.Single(method => method.Name == "Test1");

      MethodParameterMetadata methodParameterMetadata = methodMetadata.Parameters.Single(param => param.Name == "optional");

      Assert.AreEqual(5, (int)methodParameterMetadata.DefaultValue);
    }

    [TestMethod]
    public void MethodParameterMetadata_GivesSignature()
    {
      NamespaceMetadata namespaceMetadata = TestReflector.Reflector.AssemblyMetadata.Namespaces.First();
      TypeMetadata methodType = namespaceMetadata.Types.Single(type => type.Name == "BaseClass");
      MethodMetadata methodMetadata = methodType.Methods.Single(method => method.Name == "Test1");

      MethodParameterMetadata methodParameterMetadata = methodMetadata.Parameters.Single(param => param.Name == "optional");
      MethodParameterMetadata otherParameterMetadata = methodMetadata.Parameters.Single(param => param.Name == "first");

      Assert.AreEqual("Int32 optional = 5", methodParameterMetadata.GetSignature());
      Assert.AreEqual("in Boolean& first", otherParameterMetadata.GetSignature());
    }
  }
}
