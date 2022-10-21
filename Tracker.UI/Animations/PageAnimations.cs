using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Tracker.UI.Animations;

public static class PageAnimations
{
    public static async Task SlideAndFadeInFromRightAsync(this UserControl control, float seconds)
    {
        var storyboard = new Storyboard();
        var offset = (double)control.Parent.GetValue(FrameworkElement.ActualWidthProperty);

        storyboard.AddSlideFromRight(seconds, offset);
        storyboard.AddFadeIn(seconds);

        storyboard.Begin(control);

        control.Visibility = Visibility.Visible;

        await Task.Delay((int)seconds * 1000);
    }

    public static async Task SlideAndFadeOutToLeftAsync(this UserControl control, float seconds)
    {
        var storyboard = new Storyboard();
        var offset = (double)control.Parent.GetValue(FrameworkElement.ActualWidthProperty);

        storyboard.AddSlideToLeft(seconds, offset);
        storyboard.AddFadeOut(seconds);

        storyboard.Begin(control);

        control.Visibility = Visibility.Visible;

        await Task.Delay((int)seconds * 1000);
    }
}
