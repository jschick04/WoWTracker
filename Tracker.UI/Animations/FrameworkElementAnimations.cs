using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Tracker.UI.Animations {

    public static class FrameworkElementAnimations {

        public static async Task SlideAndFadeInFromLeftAsync(
            this FrameworkElement element,
            float seconds = 0.3f,
            bool keepMargin = true
        ) {
            var storyboard = new Storyboard();

            storyboard.AddSlideFromLeft(seconds, element.ActualWidth, keepMargin:keepMargin);
            storyboard.AddFadeIn(seconds);

            storyboard.Begin(element);

            element.Visibility = Visibility.Visible;

            await Task.Delay((int)seconds * 1000);
        }

        public static async Task SlideAndFadeInFromRightAsync(
            this FrameworkElement element,
            float seconds = 0.3f,
            bool keepMargin = true
        ) {
            var storyboard = new Storyboard();

            storyboard.AddSlideFromRight(seconds, element.ActualWidth, keepMargin:keepMargin);
            storyboard.AddFadeIn(seconds);

            storyboard.Begin(element);

            element.Visibility = Visibility.Visible;

            await Task.Delay((int)seconds * 1000);
        }

        public static async Task SlideAndFadeOutToLeftAsync(
            this FrameworkElement element,
            float seconds = 0.3f,
            bool keepMargin = true
        ) {
            var storyboard = new Storyboard();

            storyboard.AddSlideToLeft(seconds, element.ActualWidth, keepMargin:keepMargin);
            storyboard.AddFadeOut(seconds);

            storyboard.Begin(element);

            element.Visibility = Visibility.Visible;

            await Task.Delay((int)seconds * 1000);
        }

        public static async Task SlideAndFadeOutToRightAsync(
            this FrameworkElement element,
            float seconds = 0.3f,
            bool keepMargin = true
        ) {
            var storyboard = new Storyboard();

            storyboard.AddSlideToRight(seconds, element.ActualWidth, keepMargin:keepMargin);
            storyboard.AddFadeOut(seconds);

            storyboard.Begin(element);

            element.Visibility = Visibility.Visible;

            await Task.Delay((int)seconds * 1000);
        }

    }

}