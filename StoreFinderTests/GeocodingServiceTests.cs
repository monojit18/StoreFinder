
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NUnit.Framework;
using StoreFinder.Common;
using StoreFinder.DataServices;

namespace StoreFinderTests
{
    [TestFixture]
    public class GeocodingServiceTests
    {


        [Test]
        public void GeocodingQueryTest()
        {

            var geocodingService = new STFGeocodingService();

            var queryDictionary = new Dictionary<string, string>();
            queryDictionary.Add(STFConstants.KAddressKeyString, "83 Jessore Road, Srijan Midlands, Kolkata-700132");
            queryDictionary.Add(STFConstants.KApiKeyString, STFConstants.KApiValueString);

            geocodingService = geocodingService.Query(queryDictionary) as STFGeocodingService;
            geocodingService = geocodingService.Build() as STFGeocodingService;
            Assert.That(queryDictionary.Count > 0);


        }

    }
}
