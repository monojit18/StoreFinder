
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NUnit.Framework;
using StoreFinder.Common;
using StoreFinder.DataServices;

namespace StoreFinderTests
{
    [TestFixture]
    public class StoreFinderServiceTests
    {
       
		[Test]
        [Ignore("Later")]
		public void StoreAllFinder()
		{

			var storeFinderService = new STFStoreFinderService();

			var queryDictionary = new Dictionary<string, string>();
			queryDictionary.Add(STFConstants.KDocumentString, STFConstants.KExtendedString);
			queryDictionary.Add(STFConstants.KLimitString, STFConstants.KSearchLimitValue.ToString());
			queryDictionary.Add(STFConstants.KBannerGEKeyString, STFConstants.KBannerGEValueString);
			//queryDictionary.Add(STFConstants.KBannerGGKeyString, STFConstants.KBannerGGValueString);
			//queryDictionary.Add(STFConstants.KBannerMDKeyString, STFConstants.KBannerMDValueString);

			storeFinderService.Query(queryDictionary);
			storeFinderService.Build();

			//Task.Run(async () => 
			//{

			//	var fetchTask = storeFinderService.GetAsync();
			//	await fetchTask;
			//	Console.WriteLine(fetchTask.Result);
			//	Assert.That(fetchTask.Result != null);

			//}).GetAwaiter().OnCompleted(() => 
			//{

			//	Console.WriteLine("Done");

			//});


		}

    }
}
