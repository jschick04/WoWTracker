using TrackerUI.ViewModels;

namespace TrackerUI.DesignModel {

    public class CharacterListItemDesignModel : CharacterListItemViewModel {

        public CharacterListItemDesignModel() {
            CanCraft = true;
            ClassInitial = "M";
            CharacterName = "Moutagg";
            ClassIconColorValue = "00c541";
            Profession = "Enchanting - Alchemy - Cooking";
        }

        public static CharacterListItemDesignModel Instance => new CharacterListItemDesignModel();

    }

}