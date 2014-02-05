using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;
using System.Collections.ObjectModel;

namespace WebRequest_test2
{
    public partial class Settings : PhoneApplicationPage
    {

        private void Cache_Click(object sender, EventArgs e)
        {

            var settings = IsolatedStorageSettings.ApplicationSettings;

            if (settings.Contains("Fav"))
            {
                var favs = IsolatedStorageSettings.ApplicationSettings["Fav"] as ObservableCollection<StopsC>;
                IsolatedStorageSettings.ApplicationSettings.Clear();
                settings.Add("Fav", favs);
            }
            else
            {
                IsolatedStorageSettings.ApplicationSettings.Clear();
            }

        }

        private void Settings_Click(object sender, EventArgs e)
        {
            
        }

        private void Home_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/MainPage.xaml?", UriKind.Relative));
        }

        private void About_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/About.xaml?", UriKind.Relative));
        }

        private void FavButton_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/FavPage.xaml?", UriKind.Relative));
        }


        public Settings()
        {
            InitializeComponent();
        }

        private void Fav_Click(object sender, RoutedEventArgs e)
        {

            var settings = IsolatedStorageSettings.ApplicationSettings;

            if (settings.Contains("Fav"))
            {
                settings.Remove("Fav");
            }


        }
    }
}