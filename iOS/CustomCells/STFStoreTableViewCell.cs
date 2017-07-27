using System;
using System.Text;
using Foundation;
using UIKit;
using StoreFinder.Common;
using StoreFinder.ViewModels;

namespace StoreFinder.iOS.CustomCells
{
    public partial class STFStoreTableViewCell : UITableViewCell
    {

        private STFStoreViewModel _storeViewModel;

        public static readonly NSString KeyString = new NSString(STFConstants.KStoreTableViewCellString);
        public static readonly UINib Nib;

        public static nfloat CellHeight => 110.0F;

        static STFStoreTableViewCell()
        {
            Nib = UINib.FromName(KeyString, NSBundle.MainBundle);
        }

        protected STFStoreTableViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void PrepareCell(STFStoreViewModel storeViewModel)
        {

            _storeViewModel = storeViewModel;

            if (string.IsNullOrEmpty(storeViewModel.StoreId) == true)
            {

                ErrorView.Hidden = false;
                ContentView.BringSubviewToFront(ErrorView);
                ErrorLabel.Text = STFConstants.KNoLuckString;
                ErrorView.Layer.BorderColor = UIColor.Clear.CGColor;
                ActualContentView.Hidden = true;


            }
            else
            {

                ErrorView.Hidden = true;
                ContentView.SendSubviewToBack(ErrorView);
                ActualContentView.Hidden = false;
                StoreNameLabel.Text = storeViewModel?.StoreName;
                StoreAddress1Label.Text = storeViewModel?.StoreAddressLine1;
                StoreAddress2Label.Text = storeViewModel?.StoreAddressLine2;

                var distanceText = ((double)(storeViewModel?.StoreDistance)).ToString();
                var distanceBuilder = new StringBuilder(distanceText);
                distanceBuilder.Append(" ").Append(STFConstants.KDistanceMilesAwayString);
                StoreDistanceLabel.Text = distanceBuilder.ToString();

                StoreImageView.Hidden = false;
                StoreImageView.Layer.CornerRadius = 25;
                StoreImageView.Layer.BorderWidth = (nfloat)0.5;

                ActualContentView.Layer.CornerRadius = (nfloat)5.0;
                ActualContentView.Layer.BorderWidth = (nfloat)0.5;
                ActualContentView.Layer.BorderColor = UIColor.Clear.CGColor;

            }

        }

    }
}
