using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using StoreFinder.Common;

namespace StoreFinder.DataServices
{
	
    public class STFStoreFinderService : STFHttpDataService
    {
        
        protected override string PrepareUrl()
		{

			return STFConstants.KFinderBaseURLString;

		}

        public override STFHttpDataService Build()
        {

            base.Build();

            _httpURLString = _httpURLString.Replace(STFConstants.KBannerGEKeyString, STFConstants.KBannerKeyString)
                                           .Replace(STFConstants.KBannerGGKeyString, STFConstants.KBannerKeyString)
                                           .Replace(STFConstants.KBannerMDKeyString, STFConstants.KBannerKeyString);
            return this;


        }

        public async Task<string> GetStoreFinderAsync()
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
