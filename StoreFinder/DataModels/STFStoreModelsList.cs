using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace StoreFinder.DataModels
{

    public class STFStoreModelsList : STFDataModel
    {

        [JsonProperty("results")]
        public List<STFStoreLocationModel> Results { get; set; }

    }

    public class STFStoreLocationModel : STFDataModel
    {

        [JsonProperty("Location")]
        public STFStoreModel StoreModel { get; set; }
        public double Distance { get; set; }

    }

    public class STFStoreModel : STFDataModel
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string StoreDisplayName { get; set; }
        public STFAddressModel Address { get; set; }

    }

    public class STFAddressModel : STFDataModel
    {

        [JsonProperty("lineOne")]
        public string LineOne { get; set; }
        public string City { get; set; }
        public STFStateModel State { get; set; }
        public string Zip { get; set; }
        public STFCoordinatesModel Coordinates { get; set; }

    }

    public class STFStateModel : STFDataModel
    {

        public string Abbreviation { get; set; }

    }

    public class STFCoordinatesModel : STFDataModel
    {

        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
}
