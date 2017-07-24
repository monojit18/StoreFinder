using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using UIKit;
using Foundation;
using StoreFinder.Common;
using StoreFinder.ViewModels;
using StoreFinder.iOS.CustomCells;

namespace StoreFinder.iOS
{

    public delegate void SearchCompleteCallback(string searchString);

    public partial class STFHostViewController : UIViewController
    {

        private STFStoresListViewModel _storesListViewModel;
        private string _searchString;

        private async Task PopulateStoresAsync(string storeAddressString)
        {

            if (string.IsNullOrEmpty(storeAddressString) == true)
                return;

            if (_storesListViewModel == null)
                return;

            LoadingIndicatorView.Hidden = false;
            LoadingIndicatorView.StartAnimating();

            Task fetchTask = null;
            if (AllButton.Selected == true)
                fetchTask = _storesListViewModel.GetAllStoresAsync(storeAddressString);
            else if (GroceriesButton.Selected == true)
                fetchTask = _storesListViewModel.GetGroceryStoresAsync(storeAddressString);
            else if (FuelButton.Selected == true)
                fetchTask = _storesListViewModel.GetFuelStoresAsync(storeAddressString);

            await fetchTask;

            LoadingIndicatorView.StopAnimating();

            var tableViewSource = StoreFinderTableView.Source as STFStoresTableViewSource;

            //if (_storesListViewModel.StoreViewModels == null)
            //{

            //    var existingList = tableViewSource.StoresViewModelsList;
            //    if (existingList.Count != 0)
            //    {

            //        var errorStoreViewModel = new STFStoreViewModel();
            //        existingList.Insert(0, errorStoreViewModel);
            //        _storesListViewModel.StoreViewModels = existingList;

            //    }

            //}

            tableViewSource.StoresViewModelsList = _storesListViewModel.StoreViewModels;
            StoreFinderTableView.Source = tableViewSource;
            StoreFinderTableView.ReloadData();


        }

        private void PrepareFilterButtons()
        {

            AllButton.Selected = true;
            GroceriesButton.Selected = false;
            FuelButton.Selected = false;

            AllButton.TouchUpInside += async (object sender, EventArgs e) => 
            {

                AllButton.Selected = true;
                GroceriesButton.Selected = false;
                FuelButton.Selected = false;
                await PopulateStoresAsync(_searchString);

            };

            GroceriesButton.TouchUpInside += async (object sender, EventArgs e) =>
            {

                GroceriesButton.Selected = true;
                AllButton.Selected = false;
                FuelButton.Selected = false;
                await PopulateStoresAsync(_searchString);

            };

            FuelButton.TouchUpInside += async (object sender, EventArgs e) =>
            {

                FuelButton.Selected = true;
                GroceriesButton.Selected = false;
                AllButton.Selected = false;
                await PopulateStoresAsync(_searchString);

            };

        }

        private void PrepareView()
        {

            PrepareFilterButtons();
            LoadingIndicatorView.Hidden = true;

            StoreFinderSearchBar.AutocapitalizationType = UITextAutocapitalizationType.None;
            _storesListViewModel = new STFStoresListViewModel();

            var storesTableViewSource = new STFStoresTableViewSource(StoreFinderTableView);
            StoreFinderTableView.Source = storesTableViewSource;

            StoreFinderSearchBar.Delegate = new STFStoreSearchBarDelegate((string searchString) =>
            {

                _searchString = string.Copy(searchString);
                PopulateStoresAsync(searchString);


            }, StoreFinderSearchBar);


        }

        public STFHostViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {

            base.ViewDidLoad();
            PrepareView();

        }

        public override void ViewWillAppear(bool animated)
        {

            base.ViewWillAppear(animated);

        }

        public override void ViewDidAppear(bool animated)
        {

            base.ViewDidAppear(animated);


        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.        
        }
    }


    public class STFStoreSearchBarDelegate : UISearchBarDelegate
    {

        private SearchCompleteCallback _searchCallback;
        private UISearchBar _searchBar;

        public STFStoreSearchBarDelegate(SearchCompleteCallback searchCallback, UISearchBar searchBar)
        {

            _searchCallback = searchCallback;
            _searchBar = searchBar;

        }

        public override void SearchButtonClicked(UISearchBar searchBar)
        {

            if (_searchCallback != null)
                _searchCallback.Invoke(searchBar.Text);
            
            searchBar.ResignFirstResponder();

        }

        public override void CancelButtonClicked(UISearchBar searchBar)
        {

            searchBar.ResignFirstResponder();

        }

    }


    public class STFStoresTableViewSource : UITableViewSource
    {

        private UITableView _storesTableView;
        public List<STFStoreViewModel> StoresViewModelsList { get; set; }

        public STFStoresTableViewSource(UITableView storesTableView)
        {

            _storesTableView = storesTableView;
            _storesTableView.RegisterNibForCellReuse(UINib.FromName(STFConstants.KStoreTableViewCellString, null),
                                                     STFConstants.KStoresCellReuseIdentifierString);

        }

        public override nint NumberOfSections(UITableView tableView)
        {
            if (StoresViewModelsList == null)
                return 1;

            return StoresViewModelsList.Count;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {

            return 1;


        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return STFStoreTableViewCell.CellHeight;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {

            var cell = tableView.DequeueReusableCell(STFConstants.KStoresCellReuseIdentifierString, indexPath);
            if (StoresViewModelsList == null)
            {

                cell.Hidden = true;
                return cell;

            }

            cell.Hidden = false;
            cell.BackgroundColor = UIColor.Clear;
            var storeCell = cell as STFStoreTableViewCell;
            var storeViewModel = StoresViewModelsList[indexPath.Row];
            storeCell.PrepareCell(storeViewModel);
            return storeCell;

        }
    }
}
