// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace StoreFinder.iOS
{
    [Register ("STFHostViewController")]
    partial class STFHostViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton AllButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton FuelButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton GroceriesButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIActivityIndicatorView LoadingIndicatorView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISearchBar StoreFinderSearchBar { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView StoreFinderTableView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AllButton != null) {
                AllButton.Dispose ();
                AllButton = null;
            }

            if (FuelButton != null) {
                FuelButton.Dispose ();
                FuelButton = null;
            }

            if (GroceriesButton != null) {
                GroceriesButton.Dispose ();
                GroceriesButton = null;
            }

            if (LoadingIndicatorView != null) {
                LoadingIndicatorView.Dispose ();
                LoadingIndicatorView = null;
            }

            if (StoreFinderSearchBar != null) {
                StoreFinderSearchBar.Dispose ();
                StoreFinderSearchBar = null;
            }

            if (StoreFinderTableView != null) {
                StoreFinderTableView.Dispose ();
                StoreFinderTableView = null;
            }
        }
    }
}