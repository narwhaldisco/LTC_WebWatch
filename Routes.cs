using System.Collections.ObjectModel;

namespace WebRequest_test2
{
    public class Routes : ObservableCollection<Route>
    {

        public Routes() { }

        public void LoadDefaultData()
        {
            App.routeList.Add(new Route("Kipps Lane/Thompson", 01));
            App.routeList.Add(new Route("Dundas", 02));
            App.routeList.Add(new Route("Hamilton", 03));
            App.routeList.Add(new Route("Oxford East", 04));
            App.routeList.Add(new Route("Springbank", 05));
            App.routeList.Add(new Route("Richmond", 06));
            App.routeList.Add(new Route("Wavell", 07));
            App.routeList.Add(new Route("Riverside", 08));
            App.routeList.Add(new Route("Whitehills", 09));
            App.routeList.Add(new Route("Wonderland", 10));
            App.routeList.Add(new Route("SouthCrest", 11));
            App.routeList.Add(new Route("Wharncliffe South", 12));
            App.routeList.Add(new Route("Wellington", 13));
            App.routeList.Add(new Route("Highbury", 14));
            App.routeList.Add(new Route("Westmount", 15));
            App.routeList.Add(new Route("Adelaide", 16));
            App.routeList.Add(new Route("Oxford West", 17));
            App.routeList.Add(new Route("Oakridge", 19));
            App.routeList.Add(new Route("CherryHill", 20));
            App.routeList.Add(new Route("Huron Heights", 21));
            App.routeList.Add(new Route("Trafalgar", 22));
            App.routeList.Add(new Route("Berkshire", 23));
            App.routeList.Add(new Route("Baseline", 24));
            App.routeList.Add(new Route("Kilally", 25));
            App.routeList.Add(new Route("Jalna Blvd. West", 26));
            App.routeList.Add(new Route("Fanshawe College", 27));
            App.routeList.Add(new Route("Lambeth", 28));
            App.routeList.Add(new Route("Newbold", 30));
            App.routeList.Add(new Route("Orchard Park", 31));
            App.routeList.Add(new Route("Windermere", 32));
            App.routeList.Add(new Route("Proudfoot", 33));
            App.routeList.Add(new Route("Medway", 34));
            App.routeList.Add(new Route("Argyle", 35));
            App.routeList.Add(new Route("Airport/Industrial", 36));
            App.routeList.Add(new Route("Soverign Road", 37));
            App.routeList.Add(new Route("Stoney Creek", 38));
            App.routeList.Add(new Route("Fanshawe West", 39));
        }
    }
}
