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

  public class BaseClass
  {
    [Test]
    public Class1 Class { get; set; }
    public IEnumerable<string> Enumerable { get; set; }
  }

  public interface ISomething
  {
    [Test]
    String Concat(String a, String b);
  }

  [Test]
  public class Class1 : BaseClass, ISomething
  {
    
    public Class1 Class { get; set; }
    public string Concat(string a, string b)
    {
      return a + b;
    }

    private sealed class NestedPrivate
    {
      private void PrivateMethod()
      {
        
      }
    }
  }
}
