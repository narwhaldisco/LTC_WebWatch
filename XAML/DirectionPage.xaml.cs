using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;
using System.Windows;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization.Json;
using System.IO;
using Microsoft.Phone.Net.NetworkInformation;

namespace WebRequest_test2
{
    public partial class DirectionPage : PhoneApplicationPage
    {

        Direction direction;
        int routeNum;
        string routeFullName;
        bool hasNetworkConnection = NetworkInterface.GetIsNetworkAvailable();

        public DirectionPage()
        {
            InitializeComponent();
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            direction.DirectionList.Clear();
            DirectionList.ItemsSource = null;
            direction.Refresh(routeNum);
            DirectionList.ItemsSource = direction.DirectionList;
            //Do work for your application here.
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Settings.xaml?", UriKind.Relative));
        }

        private void Home_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/MainPage.xaml?", UriKind.Relative));
        }

        private void About_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/About.xaml?", UriKind.Relative));
        }

        private void Tiles_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Tile.xaml?", UriKind.Relative));
        }

        private void Fav_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/FavPage.xaml?", UriKind.Relative));
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            if(hasNetworkConnection == false)
            {
                MessageBox.Show("No network connection detected, connect to a network and try again.");   
            }

            
            

            routeFullName = this.NavigationContext.QueryString["Route"];
            routeNum = Int32.Parse(this.NavigationContext.QueryString["RouteNum"]);
            ApplicationTitle.Text = routeFullName;

            direction = new Direction();

            // get the forecast for the given latitude and longitude

            direction.GetDirection(routeNum);
            

            // set data context for page to this forecast
            DataContext = direction;


            // set source for ForecastList box in page to our list of forecast time periods
            DirectionList.ItemsSource = direction.DirectionList;
       
        }

        private void Button_Click(object sender, EventArgs e)
        {
            // Select the item in the listbox that was clicked
            DirectionList.SelectedItem = ((Button)sender).DataContext;
            // If selected index is -1 (no selection) do nothing
            if (DirectionList.SelectedItem == null)
                return;

            DirectionC curDir = (DirectionC)DirectionList.SelectedItem;

            if (curDir.DirectionName == "NORTHBOUND")
            {
                this.NavigationService.Navigate(new Uri("/StopsPage.xaml?Route=" + routeFullName + "&RouteNum=" + routeNum + "&Direction=2", UriKind.Relative));
            }
            else if (curDir.DirectionName == "SOUTHBOUND")
            {
                this.NavigationService.Navigate(new Uri("/StopsPage.xaml?Route=" + routeFullName + "&RouteNum=" + routeNum + "&Direction=3", UriKind.Relative));
            }
            else if (curDir.DirectionName == "EASTBOUND")
            {
                this.NavigationService.Navigate(new Uri("/StopsPage.xaml?Route=" + routeFullName + "&RouteNum=" + routeNum + "&Direction=1", UriKind.Relative));
            }
            else if (curDir.DirectionName == "WESTBOUND")
            {
                this.NavigationService.Navigate(new Uri("/StopsPage.xaml?Route=" + routeFullName + "&RouteNum=" + routeNum + "&Direction=4", UriKind.Relative));
            }

        }



        /// <summary>
        /// Make sure no item can be selected in the forecast list box
        /// </summary>
        private void DirectionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (DirectionList.SelectedIndex != -1)
            {
                DirectionC curDir = (DirectionC)DirectionList.SelectedItem;

                if (curDir.DirectionName == "NORTHBOUND")
                {
                    this.NavigationService.Navigate(new Uri("/StopsPage.xaml?Route=" + routeFullName + "&RouteNum=" + routeNum + "&Direction=2", UriKind.Relative));
                }
                else if (curDir.DirectionName == "SOUTHBOUND")
                {
                    this.NavigationService.Navigate(new Uri("/StopsPage.xaml?Route=" + routeFullName + "&RouteNum=" + routeNum + "&Direction=3", UriKind.Relative));
                }
                else if (curDir.DirectionName == "EASTBOUND")
                {
                    this.NavigationService.Navigate(new Uri("/StopsPage.xaml?Route=" + routeFullName + "&RouteNum=" + routeNum + "&Direction=1", UriKind.Relative));
                }
                else if (curDir.DirectionName == "WESTBOUND")
                {
                    this.NavigationService.Navigate(new Uri("/StopsPage.xaml?Route=" + routeFullName + "&RouteNum=" + routeNum + "&Direction=4", UriKind.Relative));
                }

            }

        }

    }


}