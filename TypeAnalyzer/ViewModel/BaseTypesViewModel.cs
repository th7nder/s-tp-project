using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeAnalyzer.Model;

namespace TypeAnalyzer.ViewModel
{
    class BaseTypesViewModel : TreeItemViewModel
    {
        private readonly IEnumerable<TypeMetadata> _types;

        public BaseTypesViewModel(IEnumerable<TypeMetadata> types)
        {
            Name = "Base types";
            _types = types;
        }

        protected override void BuildMyself()
        {
            IEnumerable<TypeViewModel> types = from typeMetadata in _types
                                               select new TypeViewModel(typeMetadata);
            foreach (TypeViewModel type in types)
            {
                Children.Add(type);
            }
        }
    }
}
