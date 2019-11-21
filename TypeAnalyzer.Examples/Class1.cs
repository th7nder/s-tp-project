using System;
using System.Collections.Generic;

namespace TypeAnalyzer.Examples
{
  public class TestAttribute : Attribute
  {

  }

  public class GenericType<T, U>
  {
    T test { get; set; }
  }

  public class ClosedOpenType<R> : GenericType<int, string>
  {

  }

  internal abstract class BaseClass
  {
    [Test]
    public Class1 Class { get; set; }
    public IEnumerable<string> Enumerable { get; set; }
    public static void Static() { }
    public abstract void Abstract();
    [Test]
    public void Test1(in bool first, out bool second, ref int third, [Test] int optional = 5)
    {
      second = true;
    }
    protected void Protected() { }
    protected internal void ProtectedInternal() { }
    private protected void PrivateProtected() { }
  }

  public interface ISomething
  {
    [Test]
    String Concat(String a, String b);
  }


  internal static class ExtensionClass
  {
    public static int[,,] array = new int[1, 2, 3];
    public static void ExtensionMethod(this Class1 obj)
    {

    }
  }

  internal struct TestStruct
  {

  }

  [Test]
  internal class Class1 : BaseClass, ISomething
  {
    public Class1 Class { get; set; }
    [Test]
    public Animals AnimalField;
    [Test]
    public event EventHandler SomeEvent;

    private Class1(Animals animalField)
    {
      AnimalField = animalField;
    }

    public string Concat(string a, string b)
    {
      return a + b;
    }

    public override void Abstract()
    {
    }

    public enum Animals
    {
      DOG, CAT
    }

    private sealed class NestedPrivate<K>
    {
      private void PrivateMethod<T>(T a, T b)
      {

      }

      private unsafe void PointerMagic(int* omg)
      {

      }
    }

    public class FieldTest
    {
      private int privateField;
      protected int protectedField;
      public int publicField;
      internal int internalField;
      protected internal int protectedInternalField;
      private protected int privateProtectedField;
    }

    public abstract class MethodTest
    {
      private void Private() { }
      protected void Protected() { }
      public void Public() { }
      internal void Internal() { }
      protected internal void ProtectedInternal() { }
      private protected void PrivateProtected() { }

      public abstract void Abstract();
      static void Static() { }
      public virtual void Virtual() { }

    }

    public class MethodFinalTest : MethodTest
    {
      public override void Abstract()
      {
        throw new NotImplementedException();
      }

      public override sealed void Virtual() { }
    }
  }
}
