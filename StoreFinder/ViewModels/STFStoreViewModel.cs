using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using StoreFinder.Common;
using StoreFinder.Adapters;
using StoreFinder.DataServices;

namespace StoreFinder.ViewModels
{
	public class STFStoreViewModel : STFViewModel
    {
	
        public string StoreId { get; set; }
        public string StoreName { get; set; }
        public string StoreAddressLine1 { get; set; }
        public string StoreAddressLine2 { get; set; }
        public string StoreCity { get; set; }
        public string StoreState { get; set; }
        public string StoreZipCode { get; set; }
        public double StoreLatitude { get; set; }
        public double StoreLongitude { get; set; }
        public double StoreDistance { get; set; }

		

    }
}
