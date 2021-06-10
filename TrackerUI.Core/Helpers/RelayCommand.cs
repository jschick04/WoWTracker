using System;
using System.Windows.Input;

namespace TrackerUI.Core.Helpers {

    public class RelayCommand : ICommand {

        private readonly Action _action;

        public RelayCommand(Action action) {
            _action = action;
        }

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => _action();

    }

}