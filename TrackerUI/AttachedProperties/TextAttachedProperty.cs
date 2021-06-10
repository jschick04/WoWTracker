using System.Windows;

namespace TrackerUI.AttachedProperties {

    /// <summary>
    ///     Keyboard focus this element on load
    /// </summary>
    public class IsFocusedProperty : BaseAttachedProperty<IsFocusedProperty, bool> {

        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) {
            if (!(sender is System.Windows.Controls.Control control)) { return; }

            control.Loaded += (s, se) => control.Focus();
        }

    }

}