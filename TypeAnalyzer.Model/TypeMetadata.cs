﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeAnalyzer.Model
{
  public class TypeMetadata
  {
    public string Name { get; set; }
    public IEnumerable<PropertyMetadata> PropertyMetadata { get; set; }
  }
}
