namespace TrackerUI.Core.ViewModels {

    public class CharacterListItemViewModel : BaseViewModel {

        public bool CanCraft { get; set; }

        public string CharacterName { get; set; }

        public string ClassIconColorValue { get; set; }

        public string ClassInitial { get; set; }

        public bool IsSelected { get; set; }

        public string Profession { get; set; }

    }

}