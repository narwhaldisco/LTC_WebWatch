using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;
using System.Windows;
using Microsoft.Phone.Net.NetworkInformation;

namespace WebRequest_test2
{
    public partial class StopsPage : PhoneApplicationPage
    {

        Stops stops;

        int routeNum;
        string routeFullName;
        int direction;

        public StopsPage()
        {
            InitializeComponent();
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            //StopList.Clear();
            stops.StopsList.Clear();
            StopsList.ItemsSource = null;
            stops.Refresh(routeNum, direction);
            StopsList.ItemsSource = stops.StopsList;
            //Do work for your application here.
        }

        private void About_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/About.xaml?", UriKind.Relative));
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Settings.xaml?", UriKind.Relative));
        }

        private void Tiles_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Tile.xaml?", UriKind.Relative));
        }

        private void Fav_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/FavPage.xaml?", UriKind.Relative));
        }

        private void Home_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/MainPage.xaml?", UriKind.Relative));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           

            routeFullName = this.NavigationContext.QueryString["Route"];
            routeNum = Int32.Parse(this.NavigationContext.QueryString["RouteNum"]);
            direction = Int32.Parse(this.NavigationContext.QueryString["Direction"]);
            string dir = "";

            if(direction == 1) {dir = "EASTBOUND";}
            else if(direction == 2){dir = "NORTHBOUND";}
            else if (direction == 3) { dir = "SOUTHBOUND"; }
            else if (direction == 4) { dir = "WESTBOUND"; }

            ApplicationTitle.Text = routeFullName + " " + dir;

            stops = new Stops();

            // get the forecast for the given latitude and longitude
            stops.GetStops(routeNum, direction);

            // set data context for page to this forecast
            DataContext = stops;

            // set source for ForecastList box in page to our list of forecast time periods
            StopsList.ItemsSource = stops.StopsList;

        }

        private void Button_Click(object sender, EventArgs e)
        {
            // Select the item in the listbox that was clicked
            StopsList.SelectedItem = ((Button)sender).DataContext;
            // If selected index is -1 (no selection) do nothing
            if (StopsList.SelectedItem == null)
                return;


            if (stops.Clickable)
            {
                
               StopsC curStop = (StopsC)StopsList.SelectedItem;
               this.NavigationService.Navigate(new Uri("/TimesPage.xaml?Route=" + routeFullName + "&RouteNum=" + routeNum + "&Direction=" + direction + "&Add=" + curStop.AddName + "&StopsName=" + curStop.StopsName, UriKind.Relative));
                
            }

        }

        /// <summary>
        /// Make sure no item can be selected in the forecast list box
        /// </summary>
        private void StopsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (stops.Clickable)
            {
                if (StopsList.SelectedIndex != -1)
                {
                    StopsC curStop = (StopsC)StopsList.SelectedItem;


                    this.NavigationService.Navigate(new Uri("/TimesPage.xaml?Route=" + routeFullName + "&RouteNum=" + routeNum + "&Direction="+ direction + "&Add=" + curStop.AddName + "&StopsName=" + curStop.StopsName, UriKind.Relative));
                }
            }

        }

    }
}