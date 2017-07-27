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

        private enum OptionEnum
        {

            eAll,
            eGroceries,
            eFuel

        }


        private STFStoresListViewModel _storesListViewModel;
        private string _searchString;
        private OptionEnum _selectedOption;

        private void ClearStoreList()
        {

            if ((_storesListViewModel == null) || (_storesListViewModel.StoreViewModels == null))
                return;            
            
            _storesListViewModel.StoreViewModels.Clear();
            var tableViewSource = StoreFinderTableView.Source as STFStoresTableViewSource;
            tableViewSource.StoresViewModelsList.Clear();
            StoreFinderTableView.Source = tableViewSource;
            StoreFinderTableView.ReloadData();

        }

        private void PositionScrollIndicatorView(OptionEnum selectedOptionEnum)
        {

            var scrollIndicatorFrame = ScrollIndicatorView.Frame;
            nfloat scrollIndicatorLeft = 0;
            nfloat scrollIndicatorRight = 0;         

            switch(selectedOptionEnum)
            {

                case OptionEnum.eAll:
                    scrollIndicatorLeft = AllButton.Frame.Left + 5.0F;
                    scrollIndicatorRight = scrollIndicatorLeft + AllButton.Frame.Width;
                    break;
                case OptionEnum.eGroceries:
                    scrollIndicatorLeft = GroceriesButton.Frame.Left + 5.0F;
                    scrollIndicatorRight = scrollIndicatorLeft + GroceriesButton.Frame.Width;
                    break;
                case OptionEnum.eFuel:
                    scrollIndicatorLeft = FuelButton.Frame.Left + 5.0F;
                    scrollIndicatorRight = scrollIndicatorLeft + FuelButton.Frame.Width;
                    break;

            }

            ScrollIndicatorView.Frame = CoreGraphics.CGRect.FromLTRB(scrollIndicatorLeft, scrollIndicatorFrame.Top,
                                                                     scrollIndicatorRight,
                                                                     scrollIndicatorFrame.Top
                                                                     + scrollIndicatorFrame.Height);



        }

        private async Task PopulateStoresAsync(string storeAddressString)
        {

            if (string.IsNullOrEmpty(storeAddressString) == true)
                return;

            if (_storesListViewModel == null)
                return;

            LoadingIndicatorView.Hidden = false;
            LoadingIndicatorView.StartAnimating();

            Task fetchTask = null;
            switch (_selectedOption)
            {
                case OptionEnum.eAll:
                    fetchTask = _storesListViewModel.GetAllStoresAsync(storeAddressString);
                    break;
                case OptionEnum.eGroceries:
                    fetchTask = _storesListViewModel.GetGroceryStoresAsync(storeAddressString);
                    break;
                case OptionEnum.eFuel:
                    fetchTask = _storesListViewModel.GetFuelStoresAsync(storeAddressString);
                    break;
            }

            await fetchTask;

            LoadingIndicatorView.StopAnimating();

            var tableViewSource = StoreFinderTableView.Source as STFStoresTableViewSource;

            if (_storesListViewModel.StoreViewModels == null)
            {

                var existingList = tableViewSource.StoresViewModelsList;
                if (existingList != null && existingList.Count != 0)
                {

                    var errorStoreViewModel = new STFStoreViewModel();
                    existingList.Insert(0, errorStoreViewModel);
                    _storesListViewModel.StoreViewModels = existingList;

                }
                else
                {

                    var alertController = UIAlertController.Create(string.Empty, STFConstants.KErrorResponseString,
                                                                   UIAlertControllerStyle.Alert);
                    var alertAction = UIAlertAction.Create(STFConstants.KOKButtonTitleString, UIAlertActionStyle.Cancel,
                                                           null);
                    alertController.AddAction(alertAction);
                    PresentViewController(alertController, true, null);


                }

            }

            tableViewSource.StoresViewModelsList = _storesListViewModel.StoreViewModels;
            StoreFinderTableView.Source = tableViewSource;
            StoreFinderTableView.ReloadData();


        }

        private void PrepareFilterButtons()
        {

            AllButton.TitleLabel.Font = UIFont.FromName(STFConstants.KButtonFontNameBoldString,
                                                        STFConstants.KButtonFontSize);
            _selectedOption = OptionEnum.eAll;

            AllButton.TouchUpInside += async (object sender, EventArgs e) => 
            {

                AllButton.TitleLabel.Font = UIFont.FromName(STFConstants.KButtonFontNameBoldString,
                                                            STFConstants.KButtonFontSize);
                GroceriesButton.TitleLabel.Font = UIFont.FromName(STFConstants.KButtonFontNameRegularString,
                                                                  STFConstants.KButtonFontSize);
                FuelButton.TitleLabel.Font = UIFont.FromName(STFConstants.KButtonFontNameRegularString,
                                                             STFConstants.KButtonFontSize);
                _selectedOption = OptionEnum.eAll;
                PositionScrollIndicatorView(_selectedOption);

                ClearStoreList();
                await PopulateStoresAsync(_searchString);

            };

            GroceriesButton.TouchUpInside += async (object sender, EventArgs e) =>
            {

                AllButton.TitleLabel.Font = UIFont.FromName(STFConstants.KButtonFontNameRegularString,
                                                            STFConstants.KButtonFontSize);
                GroceriesButton.TitleLabel.Font = UIFont.FromName(STFConstants.KButtonFontNameBoldString,
                                                                  STFConstants.KButtonFontSize);
                FuelButton.TitleLabel.Font = UIFont.FromName(STFConstants.KButtonFontNameRegularString,
                                                             STFConstants.KButtonFontSize);
                _selectedOption = OptionEnum.eGroceries;
                PositionScrollIndicatorView(_selectedOption);
               
                ClearStoreList();
                await PopulateStoresAsync(_searchString);

            };

            FuelButton.TouchUpInside += async (object sender, EventArgs e) =>
            {

                AllButton.TitleLabel.Font = UIFont.FromName(STFConstants.KButtonFontNameRegularString,
                                                            STFConstants.KButtonFontSize);
                GroceriesButton.TitleLabel.Font = UIFont.FromName(STFConstants.KButtonFontNameRegularString,
                                                            STFConstants.KButtonFontSize);
                FuelButton.TitleLabel.Font = UIFont.FromName(STFConstants.KButtonFontNameBoldString,
                                                             STFConstants.KButtonFontSize);
                _selectedOption = OptionEnum.eFuel;
                PositionScrollIndicatorView(_selectedOption);

                ClearStoreList();
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
            var storeViewModel = StoresViewModelsList[indexPath.Section];
            storeCell.PrepareCell(storeViewModel);
            return storeCell;

        }

        public override nfloat GetHeightForHeader(UITableView tableView, nint section)
        {
            return STFConstants.KSectionHeaderHeigthValue;
        }

        public override nfloat GetHeightForFooter(UITableView tableView, nint section)
        {
            return STFConstants.KSectionHeaderHeigthValue;
        }
    }
}
