using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WorldCreator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

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

        private void HidePages()
        {
            Canvas.SetZIndex(this.AboutPage, 0);
            Canvas.SetZIndex(this.HighScoresPage, 0);
            Canvas.SetZIndex(this.ProfilePage, 0); 
            Canvas.SetZIndex(this.GamePage, 0);
        }
    }
}
