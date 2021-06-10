using TrackerUI.Core.ViewModels;

namespace TrackerUI.DesignModels {

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