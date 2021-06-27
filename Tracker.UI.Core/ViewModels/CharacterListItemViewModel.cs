namespace Tracker.UI.Core.ViewModels {

    public class CharacterListItemViewModel : BaseViewModel {

        public bool IsSelected { get; set; }

        public string CharacterName { get; set; }

        public string ClassIconColorValue { get; set; }

        public string ClassInitial { get; set; }

        public string Profession { get; set; }

        public bool CanCraft { get; set; }

    }

}