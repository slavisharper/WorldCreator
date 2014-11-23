using WorldCreator.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WorldCreator.ViewModels;
using WorldCreator.Views;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace WorldCreator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private MainViewModel model;
        private const double XDelta = 30;
        private const float TopScrollViewToGameFieldRatio = 1.2f;

        public GamePage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        private void AddItem_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            this.model.Game.StartAddingItemMove((e.Container as Item).Name);
        }

        private void AddItem_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            var el = e.OriginalSource as Item;
            model.Game.AddItemToBoard(el.Name, e.Position.X - XDelta, e.Position.Y);
        }

        private void MoveItem_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            this.model.Game.StartItemMove((e.Container as Item).Name);
        }

        private void MoveItem_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            this.model.Game.CheckForCombination((e.Container as Item).Name);
        }

        private void MoveItem_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            var element = sender as Item;
            double x = e.Delta.Translation.X;
            double y = e.Delta.Translation.Y;
            double width = this.GamePageRoot.ActualWidth - XDelta;
            double height = this.GamePageRoot.ActualHeight / TopScrollViewToGameFieldRatio;
            model.Game.MoveItemOnBoard(element.Name, x, y, width, height);
        }

        private void Item_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var el = e.OriginalSource;
            Item item = null;

            if (el is TextBlock || el is Image)
            {
                var grid = (e.OriginalSource as FrameworkElement).Parent as Grid;
                item = grid.Parent as Item;
            }
            else if (el is Grid)
            {
                item = (el as Grid).Parent as Item;
            }
            else
            {
                item = el as Item;
            }

            if (item != null)
            {
                string name = item.Name;
                model.Game.RemoveItem(name);
            }
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        #region NavigationHelper registration
        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            model = e.NavigationParameter as MainViewModel;
            this.DataContext = model;
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
    }
}
