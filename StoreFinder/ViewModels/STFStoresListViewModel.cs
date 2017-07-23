using System;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using StoreFinder.Common;
using StoreFinder.Adapters;
using StoreFinder.DataServices;
using StoreFinder.DataModels;

namespace StoreFinder.ViewModels
{
    public class STFStoresListViewModel : STFViewModel
    {

        public List<STFStoreViewModel> StoreViewModels { get; set; }

        private double FormatDistance(double storeDistance)
        {

            var formattedDistanceString = string.Format("{0:0.0}", storeDistance);
            return Double.Parse(formattedDistanceString);

        }

        private string PrepareAddressLine2(STFAddressModel storeAddressModel)
        {

            var addressBuilder = new StringBuilder(storeAddressModel?.City).Append(",");
            addressBuilder.Append(storeAddressModel?.State?.Abbreviation).Append(" ");
            addressBuilder.Append(storeAddressModel?.Zip);
            return addressBuilder.ToString();

        }

        private void ConvertToViewModel(List<STFStoreLocationModel> storeLocationModelsList)
        {

            if (storeLocationModelsList == null || storeLocationModelsList.Count == 0)
                return;

            var storeViewModelsList = new List<STFStoreViewModel>();
            foreach(var storeLocationModel in storeLocationModelsList)
            {

                var storeModel = storeLocationModel?.StoreModel;
                var storeFinderViewModel = new STFStoreViewModel()
                {

                    StoreId = storeModel?.Id,
                    StoreName = storeModel?.Name,
                    StoreAddressLine1 = storeModel?.Address.LineOne,
                    StoreCity = storeModel?.Address?.City,
                    StoreState = storeModel.Address?.State?.Abbreviation,
                    StoreZipCode = storeModel?.Address?.Zip,
                    StoreLatitude = (double)(storeModel?.Address?.Coordinates?.Latitude),
                    StoreLongitude = (double)(storeModel?.Address?.Coordinates?.Longitude),
                    StoreDistance = FormatDistance((double)(storeLocationModel?.Distance)),
                    StoreAddressLine2 = PrepareAddressLine2(storeModel?.Address)

                };

                storeViewModelsList.Add(storeFinderViewModel);

            }

            StoreViewModels = storeViewModelsList;

        }

        private async Task<STFLocationModel> GetGeocodingAsync(string storeAddressString)
        {

            var geocodingService = new STFGeocodingService();

            var queryDictionary = new Dictionary<string, string>();
            queryDictionary.Add(STFConstants.KAddressKeyString, storeAddressString);
            queryDictionary.Add(STFConstants.KApiKeyString, STFConstants.KApiValueString);

            geocodingService = geocodingService.Query(queryDictionary).Build() as STFGeocodingService;

            var geocodingAdapter = new STFGeocodingAdapter(geocodingService);
            var geocodingTask = geocodingAdapter.GetStoreGeocodingAsync();
            await geocodingTask;
            return geocodingTask.Result;


        }

        private async Task GetStoreFinderAsync(string storeAddressString,
                                               Dictionary<string, string> additionalQueryDictionary)
        {

            if (string.IsNullOrEmpty(storeAddressString) == true)
                return;

            var geocodingTask = GetGeocodingAsync(storeAddressString);
            await geocodingTask;
            var geocodingModel = geocodingTask.Result;
            if (geocodingModel == null)
            {

                StoreViewModels = null;
                return;

            }

            var storeFinderService = new STFStoreFinderService();

            var queryDictionary = new Dictionary<string, string>();
            queryDictionary.Add(STFConstants.KLatiudeString, geocodingModel.Latitude.ToString());
            queryDictionary.Add(STFConstants.KLongitudeString, geocodingModel.Longitude.ToString());
            queryDictionary.Add(STFConstants.KDocumentString, STFConstants.KExtendedString);
            queryDictionary.Add(STFConstants.KLimitString, STFConstants.KSearchLimitValue.ToString());

            foreach (var addtionalQuery in additionalQueryDictionary)
                queryDictionary.Add(addtionalQuery.Key, addtionalQuery.Value);
           
            storeFinderService = storeFinderService.Query(queryDictionary).Build() as STFStoreFinderService;

            var storeFinderAdapter = new STFStoreFinderAdapter(storeFinderService);
            _storeAdapter = storeFinderAdapter;

            var fetchTask = storeFinderAdapter.GetStoreFinderAsync();
            await fetchTask;

            var result = fetchTask.Result;
            ConvertToViewModel(result);

        }

        public async Task GetAllStoresAsync(string storeAddressString)
        {

            var additionalQueryDictionary = new Dictionary<string, string>();        
            additionalQueryDictionary.Add(STFConstants.KBannerGEKeyString, STFConstants.KBannerGEValueString);
            additionalQueryDictionary.Add(STFConstants.KBannerGGKeyString, STFConstants.KBannerGGValueString);
            additionalQueryDictionary.Add(STFConstants.KBannerMDKeyString, STFConstants.KBannerMDValueString);
            await GetStoreFinderAsync(storeAddressString, additionalQueryDictionary);           

        }

        public async Task GetGroceryStoresAsync(string storeAddressString)
        {

            var additionalQueryDictionary = new Dictionary<string, string>();
            additionalQueryDictionary.Add(STFConstants.KBannerGEKeyString, STFConstants.KBannerGEValueString);
            additionalQueryDictionary.Add(STFConstants.KBannerGGKeyString, STFConstants.KBannerGGValueString);
            await GetStoreFinderAsync(storeAddressString, additionalQueryDictionary);

        }

        public async Task GetFuelStoresAsync(string storeAddressString)
        {

            var additionalQueryDictionary = new Dictionary<string, string>();
            additionalQueryDictionary.Add(STFConstants.KBannerMDKeyString, STFConstants.KBannerMDValueString);
            await GetStoreFinderAsync(storeAddressString, additionalQueryDictionary);

        }

    }
}
