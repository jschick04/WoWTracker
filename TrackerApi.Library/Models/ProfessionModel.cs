using System.Collections.Generic;

namespace TrackerApi.Library.Models {

    public class ProfessionModel {

        #region model

        public string Name { get; set; }

        public List<ItemModel> Items { get; set; }

        #endregion

    }

}