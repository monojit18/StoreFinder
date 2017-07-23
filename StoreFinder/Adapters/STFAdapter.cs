using System;
using StoreFinder.DataServices;
using StoreFinder.DataModels;

namespace StoreFinder.Adapters
{
    public class STFAdapter
    {

		protected STFHttpDataService _storeDataService;

        public STFAdapter()
        {
        }

		public void Cancel()
		{

			if (_storeDataService == null)
				return;

			_storeDataService.Cancel();

		}

    }
}
