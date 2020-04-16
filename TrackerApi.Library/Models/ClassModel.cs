using System.Collections.Generic;

namespace TrackerApi.Library.Models {

    public class ClassModel {

        #region model

        public string Name { get; set; }

        public List<SpecModel> Specs { get; set; }

        #endregion

    }

}