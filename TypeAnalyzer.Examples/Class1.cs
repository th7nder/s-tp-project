using System;

namespace TypeAnalyzer.Examples
{
  public class TestAttribute : Attribute
  {

  }
  public class BaseClass
  {
    [Test]
    public Class1 Class { get; set; }
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
  }
}
