using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using StoreFinder.Common;

namespace StoreFinder.DataServices
{
	public class STFHttpDataService
    {

        protected HttpClient _httpClient;
        protected Dictionary<string, string> _queryDictionary;
        protected HttpRequestMessage _requestMessage;
        protected CancellationTokenSource _tokenSource;
        protected string _httpURLString;

        protected virtual string PrepareUrl()
        {

            return null;
        }

        public STFHttpDataService()
        {

            _httpClient = new HttpClient();


        }

        public virtual STFHttpDataService Query(Dictionary<string, string> queryDictionary)
        {

            _queryDictionary = queryDictionary;
            return this;

        }

        public virtual STFHttpDataService Build()
        {

            _httpURLString = PrepareUrl();

            if (_queryDictionary != null && _queryDictionary.Count > 0)
            {

                string queryString = string.Empty;

                foreach (string keyString in _queryDictionary.Keys)
                    queryString += keyString + "=" + _queryDictionary[keyString] + "&";

                queryString = queryString.Remove(queryString.Length - 1);
                _httpURLString += "?" + queryString;

            }

            _tokenSource = new CancellationTokenSource();
            _requestMessage = new HttpRequestMessage();
            _requestMessage.RequestUri = new Uri(_httpURLString);
            return this;

        }

        protected async Task<STFHttpResponse> PerformAsync()
        {

            if (_requestMessage == null)
                return null;

            try
            {

                HttpResponseMessage responseMessage = await _httpClient.SendAsync(_requestMessage, _tokenSource.Token);
                string responseRead = null;
                STFHttpResponse httpResponse = null;
                if (responseMessage == null)
                    return null;

                if (responseMessage.StatusCode != HttpStatusCode.OK)
                    return new STFHttpResponse(responseMessage.ReasonPhrase, responseMessage.StatusCode, 0);

                responseRead = await responseMessage.Content?.ReadAsStringAsync();
                httpResponse = new STFHttpResponse(responseRead, responseMessage.StatusCode, 0);
                return httpResponse;

            }
            catch (WebException ex)
            {

                var httpResponse = new STFHttpResponse(ex.Message, 0, ex.Status);
                return httpResponse;

            }
            catch (AggregateException ex)
            {

                var httpResponse = new STFHttpResponse(ex.Message, 0, 0);
                return httpResponse;

            }
            catch (Exception ex)
            {

                var httpResponse = new STFHttpResponse(ex.Message, 0, 0);
                return httpResponse;

            }

        }

        public virtual void Cancel() { }

    }
}
