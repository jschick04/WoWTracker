using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TrackerUI.Animations;
using TrackerUI.Core.ViewModels;

namespace TrackerUI.Views {

    public class BaseView : UserControl {

        public BaseView() {
            if (PageLoadAnimation != PageAnimation.None) { Visibility = Visibility.Collapsed; }

            Loaded += BaseView_LoadedAsync;
        }

        public PageAnimation PageLoadAnimation { get; set; } = PageAnimation.SlideAndFadeInFromRight;

        public PageAnimation PageUnloadAnimation { get; set; } = PageAnimation.SlideAndFadeOutToLeft;

        /// <summary>
        ///     A flag to indicate if this page should animate out on load
        ///     Useful for when we are moving the page to another frame
        /// </summary>
        public bool ShouldAnimateOut { get; set; }

        /// <summary>Time it takes for a slide transition to complete</summary>
        public float SlideSeconds { get; set; } = 0.4f;

        public async Task AnimateInAsync() {
            if (PageLoadAnimation == PageAnimation.None) { return; }

            switch (PageLoadAnimation) {
                case PageAnimation.SlideAndFadeInFromRight :
                    await this.SlideAndFadeInFromRightAsync(SlideSeconds);
                    break;
            }
        }

        public async Task AnimateOutAsync() {
            if (PageUnloadAnimation == PageAnimation.None) { return; }

            switch (PageUnloadAnimation) {
                case PageAnimation.SlideAndFadeOutToLeft :
                    await this.SlideAndFadeOutToLeftAsync(SlideSeconds);
                    break;
                case PageAnimation.SlideAndFadeOutToRight :
                    await this.SlideAndFadeOutToRightAsync(SlideSeconds);
                    break;
            }
        }

        /// <summary>Perform required animations once the page is loaded</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BaseView_LoadedAsync(object sender, RoutedEventArgs e) {
            if (ShouldAnimateOut) {
                await AnimateOutAsync();
            } else {
                await AnimateInAsync();
            }
        }

    }

    public class BaseView<T> : BaseView where T : BaseViewModel, new() {

        private T _viewModel;

        public BaseView() {
            ViewModel = new T();
        }

        public T ViewModel {
            get => _viewModel;
            set {
                if (Equals(_viewModel, value)) { return; }

                _viewModel = value;

                DataContext = _viewModel;
            }
        }

    }

}