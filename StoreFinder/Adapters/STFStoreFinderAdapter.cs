using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using StoreFinder.DataServices;
using StoreFinder.DataModels;
using Newtonsoft.Json;

namespace StoreFinder.Adapters
{
	public class STFStoreFinderAdapter : STFAdapter
    {

		public STFStoreFinderAdapter(STFStoreFinderService storeFinderService)
        {

			_storeDataService = storeFinderService;


        }

        public async Task<List<STFStoreLocationModel>> GetStoreFinderAsync()
		{

			var storeFinderService = _storeDataService as STFStoreFinderService;
            var fetchTask = storeFinderService.GetStoreFinderAsync();
			await fetchTask;

			var result = fetchTask.Result;
			Console.WriteLine(result);

            try
            {

                var storeModelsList = JsonConvert.DeserializeObject<STFStoreModelsList>(result);
                return storeModelsList.Results;

            }
            catch(AggregateException ex)
            {

                Console.WriteLine(ex.Message);
                return null;

            }
            catch(Exception ex)
            {

                Console.WriteLine(ex.Message);
                return null;

            }



		}

    }
}
