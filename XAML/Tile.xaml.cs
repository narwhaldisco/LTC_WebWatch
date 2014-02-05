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
using Microsoft.Phone.Tasks;

namespace WebRequest_test2
{
    public partial class Tile : PhoneApplicationPage
    {

        MarketplaceDetailTask _marketPlaceDetailTask = new MarketplaceDetailTask();

        public Tile()
        {
            InitializeComponent();
        }

        private void buybutton(object sender, EventArgs e)
        {
            _marketPlaceDetailTask.Show();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           
        }
    }
}