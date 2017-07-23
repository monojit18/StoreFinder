// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace StoreFinder.iOS.CustomCells
{
    [Register ("STFStoreTableViewCell")]
    partial class STFStoreTableViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ErrorLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView ErrorView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel StoreAddress1Label { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel StoreAddress2Label { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel StoreDistanceLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView StoreImageView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel StoreNameLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ErrorLabel != null) {
                ErrorLabel.Dispose ();
                ErrorLabel = null;
            }

            if (ErrorView != null) {
                ErrorView.Dispose ();
                ErrorView = null;
            }

            if (StoreAddress1Label != null) {
                StoreAddress1Label.Dispose ();
                StoreAddress1Label = null;
            }

            if (StoreAddress2Label != null) {
                StoreAddress2Label.Dispose ();
                StoreAddress2Label = null;
            }

            if (StoreDistanceLabel != null) {
                StoreDistanceLabel.Dispose ();
                StoreDistanceLabel = null;
            }

            if (StoreImageView != null) {
                StoreImageView.Dispose ();
                StoreImageView = null;
            }

            if (StoreNameLabel != null) {
                StoreNameLabel.Dispose ();
                StoreNameLabel = null;
            }
        }
    }
}