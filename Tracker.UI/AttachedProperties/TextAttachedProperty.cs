using System.Windows;

namespace Tracker.UI.AttachedProperties {

    /// <summary>
    ///     Keyboard focus this element on load
    /// </summary>
    public class IsFocusedProperty : BaseAttachedProperty<IsFocusedProperty, bool> {

        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) {
            if (sender is not System.Windows.Controls.Control control) { return; }

            control.Loaded += (s, se) => control.Focus();
        }

    }

}