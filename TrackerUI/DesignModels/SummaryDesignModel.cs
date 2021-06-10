using TrackerUI.Core.ViewModels;

namespace TrackerUI.DesignModels {

    public class SummaryDesignModel : SummaryViewModel {

        public SummaryDesignModel() {
            CharacterName = "Moutagg";
            CharacterClass = "Monk";
        }

        public static SummaryDesignModel Instance => new SummaryDesignModel();

    }

}