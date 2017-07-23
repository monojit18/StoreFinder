using System;
using StoreFinder.Adapters;

namespace StoreFinder.ViewModels
{
    public class STFViewModel
    {
        
		protected STFAdapter _storeAdapter;

		public STFViewModel()
        {
        }

		public void Cancel()
		{

			if (_storeAdapter == null)
				return;

			_storeAdapter.Cancel();

		}

    }
}
