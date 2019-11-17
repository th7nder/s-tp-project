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

  public abstract class BaseClass
  {
    [Test]
    public Class1 Class { get; set; }
    public IEnumerable<string> Enumerable { get; set; }
    public static void Static() { }
    public abstract void Abstract();
    public void Test1(in bool first, out bool second, ref int third, [Test] int optional = 5)
    {
      second = true;
    }
  }

  public interface ISomething
  {
    [Test]
    String Concat(String a, String b);
  }


  public static class ExtensionClass
  {
    public static void ExtensionMethod(this Class1 obj)
    {

    } 
  }

  [Test]
  public class Class1 : BaseClass, ISomething
  {
    
    public Class1 Class { get; set; }
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
  }
}
