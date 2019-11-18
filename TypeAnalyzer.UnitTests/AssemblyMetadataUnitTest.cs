using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeAnalyzer.Model;

namespace TypeAnalyzer.UnitTests
{
  [TestClass]
  public class AssemblyMetadataUnitTest
  {

    [TestMethod]
    public void AssemblyMetadata_ParsesAssembly()
    {
      AssemblyMetadata assemblyMetadata = TestReflector.Reflector.AssemblyMetadata;

      Assert.IsTrue(assemblyMetadata.Name.Contains("TypeAnalyzer.Examples"));
      Assert.AreEqual(assemblyMetadata.Namespaces.Count(), 1);
    }

    [TestMethod]
    public void AssemblyMetadata_ParsesNamespace()
    {
      NamespaceMetadata namespaceMetadata = TestReflector.Reflector.AssemblyMetadata.Namespaces.First();

      Assert.IsTrue(namespaceMetadata.Types.Count() > 5);
    }

    [TestMethod]
    public void AssemblyMetadata_ParsesType()
    {
      TypeMetadata typeMetadata = TestReflector.Reflector.AssemblyMetadata.Namespaces.First().Types.Single(type => type.Name == "Class1");

      Assert.IsNotNull(typeMetadata);
    }

    [TestMethod]
    public void TypeMetadata_ParsesTypePropertyRecursively()
    {
      TypeMetadata typeMetadata = TestReflector.Reflector.AssemblyMetadata.Namespaces.First().Types.Single(type => type.Name == "Class1");

      PropertyMetadata propertyMetadata = typeMetadata.Properties.Single(prop => prop.Name == "Class");

      Assert.IsNotNull(propertyMetadata);
      Assert.AreSame(typeMetadata, propertyMetadata.TypeMetadata);
    }

    [TestMethod]
    public void TypeMetadata_DoesNotDuplicateAttributeMetadata()
    {
      NamespaceMetadata namespaceMetadata = TestReflector.Reflector.AssemblyMetadata.Namespaces.First();
      TypeMetadata attributedClass = namespaceMetadata.Types.Single(type => type.Name == "Class1");
      PropertyMetadata attributedProperty = namespaceMetadata.Types.Single(type => type.Name == "BaseClass").Properties.Single(property => property.Name == "Class");

      AttributeMetadata classAttribute = attributedClass.Attributes.First(attribute => attribute.Type.Name == "TestAttribute");
      AttributeMetadata propertyAttribute = attributedProperty.Attributes.First(attribute => attribute.Type.Name == "TestAttribute");

      Assert.AreSame(classAttribute, propertyAttribute);
    }

    [TestMethod]
    public void TypeMetadata_RecognizesTypeKind()
    {
      NamespaceMetadata namespaceMetadata = TestReflector.Reflector.AssemblyMetadata.Namespaces.First();

      TypeMetadata staticClass = namespaceMetadata.Types.Single(type => type.Name == "ExtensionClass");
      TypeMetadata interfaceType = namespaceMetadata.Types.Single(type => type.Name == "ISomething");
      TypeMetadata abstractClass = namespaceMetadata.Types.Single(type => type.Name == "BaseClass");
      TypeMetadata valueType = namespaceMetadata.Types.Single(type => type.Name == "TestStruct");
      TypeMetadata enumType = namespaceMetadata.Types.Single(type => type.Name == "Animals");
      TypeMetadata classType = namespaceMetadata.Types.Single(type => type.Name == "Class1");

      Assert.AreEqual(TypeKind.Static, staticClass.TypeKind);
      Assert.AreEqual(TypeKind.Interface, interfaceType.TypeKind);
      Assert.AreEqual(TypeKind.AbstractClass, abstractClass.TypeKind);
      Assert.AreEqual(TypeKind.ValueType, valueType.TypeKind);
      Assert.AreEqual(TypeKind.Enum, enumType.TypeKind);
      Assert.AreEqual(TypeKind.Class, classType.TypeKind);
    }

    [TestMethod]
    public void TypeMetadata_RecognizesGenerics()
    {
      NamespaceMetadata namespaceMetadata = TestReflector.Reflector.AssemblyMetadata.Namespaces.First();

      TypeMetadata genericType = namespaceMetadata.Types.Single(type => type.Name == "ClosedOpenType<R>");
      TypeMetadata genericTypeBase = genericType.BaseTypes.First();

      Assert.AreEqual("R", genericType.TypeParameters.First().Name);
      Assert.IsNotNull(genericTypeBase.TypeArguments.Single(type => type.Name == "Int32"));
      Assert.IsNotNull(genericTypeBase.TypeArguments.Single(type => type.Name == "String"));
    }

    [TestMethod]
    public void TypeMetadata_ChecksDeclaringType()
    {
      NamespaceMetadata namespaceMetadata = TestReflector.Reflector.AssemblyMetadata.Namespaces.First();

      TypeMetadata genericType = namespaceMetadata.Types.Single(type => type.Name == "ClosedOpenType<R>");
      TypeMetadata genericTypeParameter = genericType.TypeParameters.First();

      Assert.AreSame(genericType, genericTypeParameter.DeclaringType);
    }


    [TestMethod]
    public void TypeMetadata_RecognizesAccessLevel()
    {
      NamespaceMetadata namespaceMetadata = TestReflector.Reflector.AssemblyMetadata.Namespaces.First();

      TypeMetadata internalType = namespaceMetadata.Types.Single(type => type.Name == "Class1");
      TypeMetadata enumType = namespaceMetadata.Types.Single(type => type.Name == "Animals");
      TypeMetadata privateType = namespaceMetadata.Types.Single(type => type.Name == "NestedPrivate<K>");
      TypeMetadata publicType = namespaceMetadata.Types.Single(type => type.Name == "ISomething");
      AccessModifier protectedTypeModifier = namespaceMetadata.Types.Single(type => type.Name == "BaseClass").Methods.First(method => method.Name == "Protected").AccessModifier;
      AccessModifier protectedInternalTypeModifier = namespaceMetadata.Types.Single(type => type.Name == "BaseClass").Methods.First(method => method.Name == "ProtectedInternal").AccessModifier;
      AccessModifier privateProtectedTypeModifier = namespaceMetadata.Types.Single(type => type.Name == "BaseClass").Methods.First(method => method.Name == "PrivateProtected").AccessModifier;

      Assert.AreEqual(AccessModifier.Internal, internalType.AccessModifier);
      Assert.AreEqual(AccessModifier.None, enumType.AccessModifier);
      Assert.AreEqual(AccessModifier.Private, privateType.AccessModifier);
      Assert.AreEqual(AccessModifier.Public, publicType.AccessModifier);
      Assert.AreEqual(AccessModifier.Protected, protectedTypeModifier);
      Assert.AreEqual(AccessModifier.ProtectedInternal, protectedInternalTypeModifier);
      Assert.AreEqual(AccessModifier.PrivateProtected, privateProtectedTypeModifier);
      
    }

    [TestMethod]
    public void TypeMetadata_ChecksIfTypeIsSealed()
    {
      NamespaceMetadata namespaceMetadata = TestReflector.Reflector.AssemblyMetadata.Namespaces.First();

      TypeMetadata sealedType = namespaceMetadata.Types.Single(type => type.Name == "NestedPrivate<K>");
      TypeMetadata unsealedType = namespaceMetadata.Types.Single(type => type.Name == "Class1");

      Assert.IsTrue(sealedType.IsSealed);
      Assert.IsFalse(unsealedType.IsSealed);
    }

    [TestMethod]
    public void TypeMetadata_ChecksIfTypeIsPointer()
    {
      NamespaceMetadata namespaceMetadata = TestReflector.Reflector.AssemblyMetadata.Namespaces.First();

      TypeMetadata pointerType = namespaceMetadata.Types.Single(type => type.Name == "NestedPrivate<K>").Methods.First(method => method.Name == "PointerMagic").Parameters.First().Type;

      Assert.IsTrue(pointerType.IsPointer);
    }

    [TestMethod]
    public void TypeMetadata_LoadsBaseTypes()
    {
      NamespaceMetadata namespaceMetadata = TestReflector.Reflector.AssemblyMetadata.Namespaces.First();

      TypeMetadata childType = namespaceMetadata.Types.Single(type => type.Name == "Class1");

      Assert.AreEqual(2, childType.BaseTypes.Count());
    }

    [TestMethod]
    public void TypeMetadata_LoadsNestedTypes()
    {
      NamespaceMetadata namespaceMetadata = TestReflector.Reflector.AssemblyMetadata.Namespaces.First();

      TypeMetadata withNestedType = namespaceMetadata.Types.Single(type => type.Name == "Class1");

      Assert.AreEqual(5, withNestedType.NestedTypes.Count());
    }

    [TestMethod]
    public void TypeMetadata_LoadsAttributes()
    {
      NamespaceMetadata namespaceMetadata = TestReflector.Reflector.AssemblyMetadata.Namespaces.First();

      TypeMetadata attributeType = namespaceMetadata.Types.Single(type => type.Name == "Class1");

      Assert.AreEqual("TestAttribute", attributeType.Attributes.First().Type.Name);
    }

    [TestMethod]
    public void TypeMetadata_LoadsProperties()
    {
      NamespaceMetadata namespaceMetadata = TestReflector.Reflector.AssemblyMetadata.Namespaces.First();

      TypeMetadata withPropertiesType = namespaceMetadata.Types.Single(type => type.Name == "BaseClass");

      Assert.AreEqual(2, withPropertiesType.Properties.Count());
    }

    [TestMethod]
    public void TypeMetadata_LoadsEvents()
    {
      NamespaceMetadata namespaceMetadata = TestReflector.Reflector.AssemblyMetadata.Namespaces.First();

      TypeMetadata eventType = namespaceMetadata.Types.Single(type => type.Name == "Class1");
      EventMetadata eventMetadata = eventType.Events.First();

      Assert.AreEqual("SomeEvent", eventMetadata.Name);
      Assert.AreEqual("EventHandler", eventMetadata.EventType.Name);
    }

    [TestMethod]
    public void TypeMetadata_LoadsFields()
    {
      NamespaceMetadata namespaceMetadata = TestReflector.Reflector.AssemblyMetadata.Namespaces.First();

      TypeMetadata fieldType = namespaceMetadata.Types.Single(type => type.Name == "Class1");
      FieldMetadata fieldMetadata = fieldType.Fields.First(field => field.Name == "AnimalField");

      Assert.AreEqual("Animals", fieldMetadata.Type.Name);
    }

    [TestMethod]
    public void TypeMetadata_LoadsExtensionMethods()
    {
      NamespaceMetadata namespaceMetadata = TestReflector.Reflector.AssemblyMetadata.Namespaces.First();

      TypeMetadata extensionType = namespaceMetadata.Types.Single(type => type.Name == "Class1");

      Assert.AreEqual(1, extensionType.ExtensionMethods.Count());
    }
  }
}
