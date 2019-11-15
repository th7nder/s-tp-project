using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeAnalyzer.Model
{
  public class PropertyMetadata
  {
    public string Name { get; set; }
    public TypeMetadata TypeMetadata { get; set; }
  }
}
