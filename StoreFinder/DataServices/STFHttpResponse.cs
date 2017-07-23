using System;
using System.Net;

namespace StoreFinder.DataServices
{
    public class STFHttpResponse
    {
		public string _reponseString;
		public string ResponseString
		{
			get
			{
				return _reponseString;
			}

		}

		public HttpStatusCode _statusCode;
		public HttpStatusCode StatusCode
		{
			get
			{
				return _statusCode;
			}

		}

		public WebExceptionStatus _exceptionStatus;
		public WebExceptionStatus ExceptionStatus
		{
			get
			{
				return _exceptionStatus;
			}

		}

		public STFHttpResponse(string responseString, HttpStatusCode statusCode,
		                       WebExceptionStatus exceptionStatus)
		{

			_reponseString = string.Copy(responseString);
			_statusCode = statusCode;
			_exceptionStatus = exceptionStatus;

		}

	}
}
