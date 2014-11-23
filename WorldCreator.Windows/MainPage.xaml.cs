namespace WorldCreator
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;

    using WorldCreator.ViewModels;
    using WorldCreator.Views;

    // The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MainViewModel model;
        private const double XDelta = 100;
        private const float TopScrollViewToGameFieldRatio = 1.2f;

        public MainPage()
        {
            this.InitializeComponent();
            model = new MainViewModel();
            this.DataContext = model;
        }

        private void AddItem_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            var el = e.OriginalSource as Item;
            model.Game.AddItemToBoard(el.Name, e.Position.X - XDelta, e.Position.Y);
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
            double width = this.GamePage.ActualWidth - XDelta;
            double height = this.GamePage.ActualHeight / TopScrollViewToGameFieldRatio;
            model.Game.MoveItemOnBoard(element.Name, x, y, width, height);
        }

        private void MoveItem_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            this.model.Game.StartItemMove((e.Container as Item).Name);
        }

        private void AddItem_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            this.model.Game.StartAddingItemMove((e.Container as Item).Name);
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

        #region Navigation
        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            this.HidePages();
            Canvas.SetZIndex(this.GamePage, 1);
        }

        private void HighScoresButton_Click(object sender, RoutedEventArgs e)
        {
            this.HidePages();
            Canvas.SetZIndex(this.HighScoresPage, 1);
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            this.HidePages();
            Canvas.SetZIndex(this.AboutPage, 1);
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            this.HidePages();
            Canvas.SetZIndex(this.ProfilePage, 1);
        }
        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            this.HidePages();
            Canvas.SetZIndex(this.StartPage, 1);
        }

        private void HidePages()
        {
            Canvas.SetZIndex(this.AboutPage, 0);
            Canvas.SetZIndex(this.HighScoresPage, 0);
            Canvas.SetZIndex(this.ProfilePage, 0);
            Canvas.SetZIndex(this.GamePage, 0);
            Canvas.SetZIndex(this.StartPage, 0);
        }

        #endregion
    }
}
