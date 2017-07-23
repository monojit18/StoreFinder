using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace StoreFinder.DataModels
{

    public class STFGeocodingModelsList : STFDataModel
    {

        [JsonProperty("results")]
        public List<STFGeocodingModel> Results { get; set; }

    }

    public class STFGeocodingModel : STFDataModel
    {

        [JsonProperty("geometry")]
        public STFGeometryModel Geometry { get; set; }

    }

    public class STFGeometryModel : STFDataModel
    {

        [JsonProperty("location")]
        public STFLocationModel Location { get; set; }

    }

    public class STFLocationModel : STFDataModel
    {

        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("lng")]
        public double Longitude { get; set; }

    }
}
