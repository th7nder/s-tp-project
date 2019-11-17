
using System.Linq;
using TypeAnalyzer.Model;

namespace TypeAnalyzer.ViewModel.TreeItemViewModels
{
    public class EventViewModel : TreeItemViewModel
    {
        private readonly EventMetadata _eventMetadata;
        
        public EventViewModel(EventMetadata eventMetadata)
        {
            _eventMetadata = eventMetadata;
            Name = eventMetadata.Name;
        }
        
        protected override void BuildMyself()
        {
            Children.Add(new DetailViewModel("Name: ", _eventMetadata.Name));
            Children.Add(new DetailViewModel("Is multicast: ", _eventMetadata.IsMulticast.ToString()));
            
            if (_eventMetadata.Attributes.Any())
            {
                Children.Add(new AttributesViewModel(_eventMetadata.Attributes));  
            }
        }
    }
}