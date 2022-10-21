using System.Collections.Generic;

namespace Tracker.UI.Core.ViewModels;

public class CharacterListViewModel : BaseViewModel
{
    public List<CharacterListItemViewModel> Characters { get; set; }
}
