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
using System.Windows.Navigation;
using System.IO.IsolatedStorage;
using System.Collections.ObjectModel;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;

namespace WebRequest_test2
{
    public partial class FavPage : PhoneApplicationPage
    {

        bool removing = false;
        bool livetile = false;


        public FavPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //Loading favourites
            var settings = IsolatedStorageSettings.ApplicationSettings;

            //ApplicationTitle.Text = App.fav_counter.ToString();

            //Loading Favs
            if (settings.Contains("Fav"))
            {
                var favs = IsolatedStorageSettings.ApplicationSettings["Fav"] as ObservableCollection<StopsC>;

                ObservableCollection<StopsC> favList = new ObservableCollection<StopsC>();

                

                // copy the list of times periods over
                foreach (StopsC stopsC in favs)
                {
                    favList.Add(stopsC);
                }

                FavList.ItemsSource = favList;
               
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            // Select the item in the listbox that was clicked
            FavList.SelectedItem = ((Button)sender).DataContext;
            // If selected index is -1 (no selection) do nothing
            if (FavList.SelectedItem == null)
                return;

            StopsC curStop = (StopsC)FavList.SelectedItem;
            var settings = IsolatedStorageSettings.ApplicationSettings;

            if (removing == true)
            {
                int index = FavList.SelectedIndex;
                var favs = IsolatedStorageSettings.ApplicationSettings["Fav"] as ObservableCollection<StopsC>;
                favs.RemoveAt(index);
                settings["Fav"] = favs;
                FavList.ItemsSource = favs;
                removing = false;
                (this.ApplicationBar.Buttons[1] as ApplicationBarIconButton).IsEnabled = true;


            }
            else if (livetile == true)
            {

                Uri uri = new Uri("LiveTile.png", UriKind.Relative);

                 // Look to see whether the Tile already exists and if so, don't try to create it again.
                ShellTile TileToFind = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("/TimesPage.xaml?Route=" + curStop.RouteName + "&RouteNum=" + curStop.RouteNum + "&Direction=" + curStop.Direction + "&Add=" + curStop.AddName + "&StopsName=" + curStop.StopsName));

            // Create the Tile if we didn't find that it already exists.
            if (TileToFind == null)
            {
                // Create the Tile object and set some initial properties for the Tile.
                // The Count value of 12 shows the number 12 on the front of the Tile. Valid values are 1-99.
                // A Count value of 0 indicates that the Count should not be displayed.

                Uri img = new Uri("LiveTile.png", UriKind.Relative);
                BitmapImage imgSource = new BitmapImage(img);

                IconicTileData NewTileData = new IconicTileData
                {
                    

                    // tile foreground data
                    Title = curStop.RouteName,
                    Count = curStop.RouteNum,
                    WideContent1 = curStop.RouteName,
                    WideContent2 = curStop.StopsName,
                    //SmallIconImage = img,
                    //IconImage = img,
                    
                    
                    BackgroundColor = new Color{ A = 255, R = 23, G = 157, B = 97 }
                };

                // Create the Tile and pin it to Start. This will cause a navigation to Start and a deactivation of our application.
                ShellTile.Create(new Uri("/TimesPage.xaml?Route=" + curStop.RouteName + "&RouteNum=" + curStop.RouteNum + "&Direction=" + curStop.Direction + "&Add=" + curStop.AddName + "&StopsName=" + curStop.StopsName, UriKind.Relative), NewTileData, true);
                }

               
            }
            else
            {
                this.NavigationService.Navigate(new Uri("/TimesPage.xaml?Route=" + curStop.RouteName + "&RouteNum=" + curStop.RouteNum + "&Direction=" + curStop.Direction + "&Add=" + curStop.AddName + "&StopsName=" + curStop.StopsName, UriKind.Relative));
            }

        }

        private void Home_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/MainPage.xaml?", UriKind.Relative));
        }

        private void Tile_Click(object sender, EventArgs e)
        {
            
                MessageBox.Show("Choose a favourte to create a Tile for on the homescreen");
                livetile = true;
                (this.ApplicationBar.Buttons[2] as ApplicationBarIconButton).IsEnabled = false;
            

        }

        private void Tiles_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Tile.xaml?", UriKind.Relative));
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Settings.xaml?", UriKind.Relative));
        }

        private void About_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/About.xaml?", UriKind.Relative));
        }

        private void Remove_Click(object sender, EventArgs e)
        {
            (this.ApplicationBar.Buttons[1] as ApplicationBarIconButton).IsEnabled = false;

            MessageBox.Show("Select a Stop to remove from Favouritres");

            removing = true;

            
        }

        private void FavList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (FavList.SelectedIndex != -1)
            {

                if (removing == true || livetile == true)
                {
                    
                }
                else
                {
                    StopsC curStop = (StopsC)FavList.SelectedItem;
                    this.NavigationService.Navigate(new Uri("/TimesPage.xaml?Route=" + curStop.RouteName + "&RouteNum=" + curStop.RouteNum + "&Direction=" + curStop.Direction + "&Add=" + curStop.AddName + "&StopsName=" + curStop.StopsName, UriKind.Relative));
                }
            }
         

        }
    }
}