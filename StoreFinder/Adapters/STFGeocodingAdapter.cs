using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using StoreFinder.DataServices;
using StoreFinder.DataModels;
using Newtonsoft.Json;

namespace StoreFinder.Adapters
{
    public class STFGeocodingAdapter : STFAdapter
    {

        public STFGeocodingAdapter(STFGeocodingService geocodingService)
        {

            _storeDataService = geocodingService;


        }

        public async Task<STFLocationModel> GetStoreGeocodingAsync()
        {

            var geocodingService = _storeDataService as STFGeocodingService;
            var geocodingTask = geocodingService.GetStoreGeocodingAsync();
            await geocodingTask;



            try
            {

                var result = geocodingTask.Result;
                var allGeocodingModels = JsonConvert.DeserializeObject<STFGeocodingModelsList>(result);
                if (allGeocodingModels == null)
                    return null;

                var geocodingModelsList = allGeocodingModels.Results;
                if (geocodingModelsList == null || geocodingModelsList.Count == 0)
                    return null;
                
                var gecodingModel = geocodingModelsList.FirstOrDefault();
                if (allGeocodingModels == null)
                    return null;
                
                var geometryModel = gecodingModel.Geometry;
                if (geometryModel == null)
                    return null;
                
                return geometryModel.Location;

            }

            catch (AggregateException ex)
            {

                Console.WriteLine(ex.Message);
                return null;

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return null;

            }
        }
    }
}
