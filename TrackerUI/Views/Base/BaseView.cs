using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TrackerUI.Animations;
using TrackerUI.ViewModels;

namespace TrackerUI.Views {

    public class BaseView<T> : UserControl where T : BaseViewModel, new() {

        private T _viewModel;

        public BaseView() {
            ViewModel = new T();

            if (PageLoadAnimation != PageAnimation.None) {
                Visibility = Visibility.Collapsed;
            }

            Loaded += BaseView_Loaded;
        }

        public PageAnimation PageLoadAnimation { get; set; } = PageAnimation.SlideAndFadeInFromRight;

        public PageAnimation PageUnloadAnimation { get; set; } = PageAnimation.SlideAndFadeOutToLeft;

        /// <summary>Time it takes for a slide transition to complete</summary>
        public float SlideSeconds { get; set; } = 0.8f;

        public T ViewModel {
            get => _viewModel;
            set {
                if (Equals(_viewModel, value)) {
                    return;
                }

                _viewModel = value;

                DataContext = _viewModel;
            }
        }

        public async Task AnimateIn() {
            if (PageLoadAnimation == PageAnimation.None) {
                return;
            }

            switch (PageLoadAnimation) {
                case PageAnimation.SlideAndFadeInFromRight :
                    await this.SlideAndFadeInFromRight(SlideSeconds);

                    break;
            }
        }

        public async Task AnimateOut() {
            if (PageUnloadAnimation == PageAnimation.None) {
                return;
            }

            switch (PageLoadAnimation) {
                case PageAnimation.SlideAndFadeOutToLeft :
                    await this.SlideAndFadeOutToLeft(SlideSeconds);

                    break;
            }
        }

        /// <summary>Perform required animations once the page is loaded</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BaseView_Loaded(object sender, RoutedEventArgs e) {
            await AnimateIn();
        }

    }

}