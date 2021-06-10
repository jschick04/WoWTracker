﻿using TrackerUI.Animations;
using TrackerUI.Core.ViewModels;

namespace TrackerUI.Views {

    /// <summary>Interaction logic for SummaryView.xaml</summary>
    public partial class SummaryView : BaseView<SummaryViewModel> {

        public SummaryView() {
            InitializeComponent();
            PageUnloadAnimation = PageAnimation.SlideAndFadeOutToRight;
        }

    }

}