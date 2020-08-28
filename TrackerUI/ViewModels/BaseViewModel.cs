using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TrackerUI.Helpers;

namespace TrackerUI.ViewModels {

    public abstract class BaseViewModel : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        protected void OnPropertyChanged([CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected async Task RunCommand(Expression<Func<bool>> updatingFlag, Func<Task> action) {
            if (updatingFlag.GetPropertyValue()) {
                return;
            }

            updatingFlag.SetPropertyValue(true);

            try {
                await action();
            } finally {
                updatingFlag.SetPropertyValue(false);
            }
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string name = null) {
            if (EqualityComparer<T>.Default.Equals(field, value)) {
                return false;
            }

            field = value;
            OnPropertyChanged(name);

            return true;
        }

    }

}