using Tracker.UI.Animations;
using Tracker.UI.Core.ViewModels;

namespace Tracker.UI.Views {

    /// <summary>Interaction logic for SummaryView.xaml</summary>
    public partial class SummaryView : BaseView<SummaryViewModel> {

        public SummaryView() {
            InitializeComponent();
            PageUnloadAnimation = PageAnimation.SlideAndFadeOutToRight;
        }

    }

}