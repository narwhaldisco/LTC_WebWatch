using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;
using System.Windows;
using Microsoft.Phone.Net.NetworkInformation;
using System.IO.IsolatedStorage;
using System.Collections.ObjectModel;
using Microsoft.Phone.Shell;

namespace WebRequest_test2
{
    public partial class TimesPage : PhoneApplicationPage
    {


        string add;



        string routeFullName;
        int direction;
        int routeNum;
        ObservableCollection<StopsC> FavsList;
        string dir;
        string Stopname;

        Times times;
        

        public TimesPage()
        {
            InitializeComponent();
            
            FavsList = new ObservableCollection<StopsC>();
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            times.TimesList.Clear();
            TimesList.ItemsSource = null;
            times.Refresh(add);
            TimesList.ItemsSource = times.TimesList;
            //Do work for your application here.
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Settings.xaml?", UriKind.Relative));
        }

        private void About_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/About.xaml?", UriKind.Relative));
        }

        private void Fav_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/FavPage.xaml?", UriKind.Relative));
        }

        private void Tiles_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Tile.xaml?", UriKind.Relative));
        }

        private void Home_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/MainPage.xaml?", UriKind.Relative));
        }

        private void FavAdd_Click(object sender, EventArgs e)
        {

           

                MessageBoxResult m = MessageBox.Show("add favourite?", "Favourites", MessageBoxButton.OKCancel);

                if (m == MessageBoxResult.OK)
                {
                    StopsC favC = new StopsC();

                    favC.StopsName = Stopname;
                    favC.AddName = add;
                    favC.Direction = direction;
                    favC.RouteNum = routeNum;
                    favC.Dir = dir;
                    favC.RouteName = routeFullName;

                    //Save stuff
                    var settings = IsolatedStorageSettings.ApplicationSettings;



                    //Saving Favourite
                    if (settings.Contains("Fav"))
                    {
                        var favs = IsolatedStorageSettings.ApplicationSettings["Fav"] as ObservableCollection<StopsC>;
                        favs.Add(favC);
                        settings["Fav"] = favs;
                    }
                    else
                    {
                        FavsList.Add(favC);
                        settings.Add("Fav", FavsList);
                    }

                    (this.ApplicationBar.Buttons[2] as ApplicationBarIconButton).IsEnabled = false;

                    


                
         
            }

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            routeFullName = this.NavigationContext.QueryString["Route"];
            routeNum = Int32.Parse(this.NavigationContext.QueryString["RouteNum"]);
            direction = Int32.Parse(this.NavigationContext.QueryString["Direction"]);
            Stopname = this.NavigationContext.QueryString["StopsName"];
            add = this.NavigationContext.QueryString["Add"] + "&d=" + this.NavigationContext.QueryString["d"] + "&s=" + this.NavigationContext.QueryString["s"];
            dir = "";

            if (direction == 1) { dir = "EASTBOUND"; }
            else if (direction == 2) { dir = "NORTHBOUND"; }
            else if (direction == 3) { dir = "SOUTHBOUND"; }
            else if (direction == 4) { dir = "WESTBOUND"; }

            ApplicationTitle.Text = routeFullName + " " + dir;
            PageTitle.Text = Stopname;

            

            times = new Times();

            // get the forecast for the given latitude and longitude
            times.GetTimes(add);

            // set data context for page to this forecast
            DataContext = times;

            // set source for ForecastList box in page to our list of forecast time periods
            TimesList.ItemsSource = times.TimesList;

            

        }

        /// <summary>
        /// Make sure no item can be selected in the forecast list box
        /// </summary>
        private void TimesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TimesList.SelectedIndex = -1;
            TimesList.SelectedItem = null;
        }

    }
}