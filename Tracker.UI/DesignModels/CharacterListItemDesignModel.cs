using Tracker.UI.Core.ViewModels;

namespace Tracker.UI.DesignModels {

    public class CharacterListItemDesignModel : CharacterListItemViewModel {

        public CharacterListItemDesignModel() {
            CharacterName = "Moutagg";
            ClassInitial = "M";
            ClassIconColorValue = "00c541";
            Profession = "Enchanting - Alchemy - Cooking";
            CanCraft = true;
        }

        public static CharacterListItemDesignModel Instance => new CharacterListItemDesignModel();

    }

}