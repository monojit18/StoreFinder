using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using StoreFinder.Common;

namespace StoreFinder.DataServices
{
	public class STFGeocodingService : STFHttpDataService
    {

        protected override string PrepareUrl()
        {

            return STFConstants.KGeocodingBaseURLString;

        }

        public override STFHttpDataService Query(Dictionary<string, string> queryDictionary)
        {

            if (queryDictionary == null || queryDictionary.Count == 0)
                return null;

            var geocodingAddressString = queryDictionary[STFConstants.KAddressKeyString];
            if (string.IsNullOrEmpty(geocodingAddressString) == true)
                return null;

            geocodingAddressString = geocodingAddressString.Replace(" ", "+");

            var encodedQueryDictionary = new Dictionary<string, string>(queryDictionary);
            encodedQueryDictionary[STFConstants.KAddressKeyString] = geocodingAddressString;
            _queryDictionary = encodedQueryDictionary;
            return this;

        }

        public async Task<string> GetStoreGeocodingAsync()
        {

            if (_requestMessage == null)
                return null;

            _requestMessage.Method = HttpMethod.Get;
            var getResponse = (await PerformAsync());
            if (getResponse == null)
                return null;

            return getResponse.ResponseString;

        }

        public override void Cancel()
        {

            if (_tokenSource == null)
                return;

            _tokenSource.Cancel();
            _httpClient.CancelPendingRequests();

        }

    }
}
