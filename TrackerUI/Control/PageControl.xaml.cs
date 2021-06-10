using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TrackerUI.Views;

namespace TrackerUI.Control {

    /// <summary>Interaction logic for PageControl.xaml</summary>
    public partial class PageControl : UserControl {

        /// <summary>Registers <see cref="CurrentPage" /> as a dependency property</summary>
        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register(
                nameof(CurrentPage),
                typeof(BaseView),
                typeof(PageControl),
                new UIPropertyMetadata(CurrentPagePropertyChanged)
            );

        public PageControl() {
            InitializeComponent();
        }

        public BaseView CurrentPage {
            get => (BaseView)GetValue(CurrentPageProperty);
            set => SetValue(CurrentPageProperty, value);
        }

        private static void CurrentPagePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ContentControl newPageFrame = (d as PageControl)?.NewPage;
            ContentControl oldPageFrame = (d as PageControl)?.OldPage;

            if (newPageFrame == null || oldPageFrame == null) { return; }

            var oldPageContent = newPageFrame.Content;

            newPageFrame.Content = null;

            oldPageFrame.Content = oldPageContent;

            if (oldPageContent is BaseView oldPage) {
                oldPage.ShouldAnimateOut = true;

                Task.Delay((int)oldPage.SlideSeconds * 1000).ContinueWith(
                    t => {
                        Application.Current.Dispatcher.Invoke(
                            () => {
                                oldPageFrame.Content = null;
                            }
                        );
                    }
                );
            }

            newPageFrame.Content = e.NewValue;
        }

    }

}