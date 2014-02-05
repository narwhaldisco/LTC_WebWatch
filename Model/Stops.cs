using System;
using System.Net;
using System.Windows;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using HtmlAgilityPack;
using System.IO.IsolatedStorage;

namespace WebRequest_test2
{
    public class Stops : INotifyPropertyChanged
    {

        
        string stopsfile;

        private Visibility vis;
        private bool clickable = true;

        int id = 0;

        public bool Clickable
        {
            get { return clickable; }

            set
            {
                clickable = value;
                NotifyPropertyChanged("Clickable");
            }
        }

        public Visibility Vis
        {
            get { return vis; }

            set
            {
                vis = value;
                NotifyPropertyChanged("Vis");
            }
        }

        public ObservableCollection<StopsC> StopsList
        {
            get;
            set;
        }

        public Stops()
        {
            StopsList = new ObservableCollection<StopsC>();
        }

        public void Refresh(int routeNum, int direction)
        {
            Deployment.Current.Dispatcher.BeginInvoke(
               () => { Vis = Visibility.Visible; });

            id++;

            HtmlWeb.LoadAsync("http://www.ltconline.ca/WebWatch/ada.aspx?r=" + routeNum.ToString() + "&d=" + direction.ToString() + "&id=" + id, DownloadCompleted);

        }

        public void GetStops(int routeNum, int direction)
        {
            Deployment.Current.Dispatcher.BeginInvoke(
                () => { Vis = Visibility.Visible; });

            stopsfile = "Stops" + direction.ToString() + routeNum.ToString();

            if (IsolatedStorageSettings.ApplicationSettings.Contains("Date") && IsolatedStorageSettings.ApplicationSettings.Contains(stopsfile))
            {
                if ((IsolatedStorageSettings.ApplicationSettings["Date"] as string) != DateTime.Today.ToString())
                {
                    id++;
                    HtmlWeb.LoadAsync("http://www.ltconline.ca/WebWatch/ada.aspx?r=" + routeNum.ToString() + "&d=" + direction.ToString() + "&id=" + id, DownloadCompleted);
                }
                else
                {
                    Load();
                    
                }
            }
            else
            {
                id++;
                HtmlWeb.LoadAsync("http://www.ltconline.ca/WebWatch/ada.aspx?r=" + routeNum.ToString() + "&d=" + direction.ToString() + "&id=" + id, DownloadCompleted);
            }
        }

        void Load()
        {

            var newStopsList = IsolatedStorageSettings.ApplicationSettings[stopsfile] as ObservableCollection<StopsC>;

            

            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {

                StopsList.Clear();

             

                // copy the list of forecast time periods over
                foreach (StopsC stopsC in newStopsList)
                {
                    StopsList.Add(stopsC);
                }
               
                


                Vis = Visibility.Collapsed;


            });

        }

        void DownloadCompleted(object sender, HtmlDocumentLoadCompleted e)
        {
            if (e.Error == null)
            {
                HtmlDocument doc = e.Document;
                if (doc != null)
                {

                    StopsC newC;

                    

                    ObservableCollection<StopsC> newStopsList =
                    new ObservableCollection<StopsC>();

                    if (doc.DocumentNode.SelectNodes("//*[@class=\"ada\"]") == null)
                    {
                        Clickable = false;
                        newC = new StopsC();
                        newC.StopsName = "No stops with upcoming crossings times found.";
                        newStopsList.Add(newC);
                    }
                    else
                    {

                        foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//*[@class=\"ada\"]"))
                        {
                            try
                            {
                                string c;
                                //link.SelectSingleNode("/table");
                                newC = new StopsC();
                                c = link.Attributes["title"].Value;
                                newC.AddName = link.Attributes["href"].Value;
                                newC.StopsName = c;
                                newStopsList.Add(newC);
                            }
                            catch (FormatException)
                            {

                            }
                        }
                    }

                    



                    // copy the data over
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {

                        Vis = Visibility.Collapsed;

                        StopsList.Clear();

                        //Saving stuff
                        var settings = IsolatedStorageSettings.ApplicationSettings;


                        //Saving Direction List
                        if (settings.Contains(stopsfile))
                        {
                            settings[stopsfile] = newStopsList;
                        }
                        else
                        {
                            settings.Add(stopsfile, newStopsList);
                        }

                        //Saving Date
                        if (settings.Contains("Date"))
                        {
                            settings["Date"] = DateTime.Today.ToString();
                        }
                        else
                        {
                            settings.Add("Date", DateTime.Today.ToString());
                        }

                        settings.Save();

                        // copy the list of forecast time periods over
                        foreach (StopsC stopsC in newStopsList)
                        {
                            StopsList.Add(stopsC);
                        }

                    });


                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }



    }
}
