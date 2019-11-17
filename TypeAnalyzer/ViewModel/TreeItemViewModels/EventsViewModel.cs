using System.Collections.Generic;
using System.Linq;
using TypeAnalyzer.Model;

namespace TypeAnalyzer.ViewModel.TreeItemViewModels
{
    public class EventsViewModel : TreeItemViewModel
    {
        private readonly IEnumerable<EventMetadata> _events;

        public EventsViewModel(IEnumerable<EventMetadata> events, string name = "Events")
        {
            Name = name;
            _events = events;
        }

        protected override void BuildMyself()
        {
            IEnumerable<EventViewModel> events = from eventMetadata in _events
                                                 select new EventViewModel(eventMetadata);
            foreach (EventViewModel method in events)
            {
                Children.Add(method);
            }
        }
    }
}