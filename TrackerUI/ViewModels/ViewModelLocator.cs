using TrackerUI.Core;
using TrackerUI.Core.ViewModels;

namespace TrackerUI.ViewModels {

    public class ViewModelLocator {

        public static ApplicationViewModel ApplicationViewModel => IoC.Get<ApplicationViewModel>();

        public static ViewModelLocator Instance { get; private set; } = new ViewModelLocator();

    }

}