using Tracker.UI.Core.ViewModels;

namespace Tracker.UI.DesignModels {

    public class SummaryDesignModel : SummaryViewModel {

        public SummaryDesignModel() {
            CharacterName = "Moutagg";
            CharacterClass = "Monk";
        }

        public static SummaryDesignModel Instance => new SummaryDesignModel();

    }

}