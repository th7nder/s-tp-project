using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TypeAnalyzer.Model
{
  public class Reflector
  {
    public AssemblyMetadata AssemblyMetadata { get; private set; }
    public Reflector(string assemblyFile)
    {
      AssemblyMetadata = new AssemblyMetadata(Assembly.LoadFrom(assemblyFile));
    }
  }
}
