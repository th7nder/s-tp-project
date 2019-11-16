using System;

namespace TypeAnalyzer.Examples
{
    public class BaseClass
    {
        public Class1 Class { get; set; }
    }

    public interface ISomething
    {
        String Concat(String a, String b);
    }

    public class Class1 : BaseClass, ISomething
    {
      public Class1 Class { get; set; }
      public string Concat(string a, string b)
      {
         return a + b;
      }
    }
}
