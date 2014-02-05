using System;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WebRequest_test2
{
    public partial class MainPage : PhoneApplicationPage
    {

        bool map = false;
        

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            RouteList.ItemsSource = App.routeList;
            
        }



        

        private void Settings_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Settings.xaml?", UriKind.Relative));
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

        private void Map_Click(object sender, EventArgs e)
        {
            // Select the item in the listbox that was clicked
            RouteList.SelectedItem = ((Button)sender).DataContext;
            // If selected index is -1 (no selection) do nothing
            if (RouteList.SelectedItem == null)
                return;

            map = true;

            Route curRoute = (Route)RouteList.SelectedItem;
            string address;

            if(curRoute.RouteNum < 10)
            {
                address = "http://www.ltconline.ca/htmlscheds/0" + curRoute.RouteNum + "ltc.htm";
            }
            else
            {
                address = "http://www.ltconline.ca/htmlscheds/" + curRoute.RouteNum + "ltc.htm";
            }

            


            WebBrowserTask task = new WebBrowserTask();
            task.Uri = (new Uri(address));
            task.Show();

        }

        private void Button_Click(object sender, EventArgs e)
        {
            // Select the item in the listbox that was clicked
            RouteList.SelectedItem = ((Button)sender).DataContext;
            // If selected index is -1 (no selection) do nothing
            if (RouteList.SelectedItem == null)
                return;

            Route curRoute = (Route)RouteList.SelectedItem;
         

            this.NavigationService.Navigate(new Uri("/DirectionPage.xaml?Route=" + curRoute.FullName + "&RouteNum=" + curRoute.RouteNum, UriKind.Relative));

        }

        private void RouteList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            // if an item is selected
            if (RouteList.SelectedIndex != -1 && map == false)
            {
     

            }

        }

        protected override void OnNavigatedFrom(NavigationEventArgs args)
        {
            // make sure no item is highlighted in the list of cities
            RouteList.SelectedIndex = -1;
            RouteList.SelectedItem = null;

            
                
            
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }
    }
}