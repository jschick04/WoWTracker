using System.Windows;
using Tracker.UI.Animations;

namespace Tracker.UI.AttachedProperties {

    /// <summary>A base class to run any animation method when a boolean is set to true and a reverse animation is set to false</summary>
    /// <typeparam name="TParent"></typeparam>
    public abstract class AnimateBaseProperty<TParent> : BaseAttachedProperty<TParent, bool>
        where TParent : BaseAttachedProperty<TParent, bool>, new() {

        public bool FirstLoad { get; set; } = true;

        public override void OnValueUpdated(DependencyObject sender, object value) {
            if (sender is not FrameworkElement element) {
                return;
            }

            if (sender.GetValue(ValueProperty) == value && !FirstLoad) {
                return;
            }

            if (FirstLoad) {
                RoutedEventHandler onLoaded = null;

                onLoaded = (ss, ee) => {
                    element.Loaded -= onLoaded;
                    DoAnimation(element, (bool)value);
                    FirstLoad = false;
                };

                element.Loaded += onLoaded;
            } else {
                DoAnimation(element, (bool)value);
            }
        }

        /// <summary>The animation method that fires when the value changes</summary>
        /// <param name="element">The element</param>
        /// <param name="value">The new value</param>
        protected virtual void DoAnimation(FrameworkElement element, bool value) { }

    }

    /// <summary>Animates a framework element sliding it in from the left and show and sliding out to the left on hide</summary>
    public class AnimateSlideInFromLeftProperty : AnimateBaseProperty<AnimateSlideInFromLeftProperty> {

        protected override async void DoAnimation(FrameworkElement element, bool value) {
            if (value) {
                await element.SlideAndFadeInFromLeftAsync(FirstLoad ? 0 : 0.3f, false);
            } else {
                await element.SlideAndFadeOutToLeftAsync(FirstLoad ? 0 : 0.3f, false);
            }
        }

    }

}