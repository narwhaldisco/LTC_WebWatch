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

    

    public class Direction : INotifyPropertyChanged
    {

        private string dirName;
        string dirfile;

        int id = 0;

        private Visibility vis;

        public Visibility Vis
        {
            get { return vis; }

            set
            {
                vis = value;
                NotifyPropertyChanged("Vis");
            }
        }


      

        public string DirName
        {
            get
            {
                return dirName;
            }
            set
            {
                if (value != dirName)
                {
                    dirName = value;
                }
            }
        }

  
        public ObservableCollection<DirectionC> DirectionList
        {
            get;
            set;
        }

        public Direction()
        {
            DirectionList = new ObservableCollection<DirectionC>();
            DirName = dirName;
        }

        public void Refresh(int routeNum)
        {
            
            Deployment.Current.Dispatcher.BeginInvoke(
               () => { Vis = Visibility.Visible; });

            id++;


            HtmlWeb.LoadAsync("http://www.ltconline.ca/WebWatch/ada.aspx?r=" + routeNum.ToString() + "&id=" + id, DownloadCompleted);

        }


        public void GetDirection(int routeNum)
        {
            Deployment.Current.Dispatcher.BeginInvoke(
                () => { Vis = Visibility.Visible; });

            dirfile = "Direction" + routeNum.ToString();

            if (IsolatedStorageSettings.ApplicationSettings.Contains("Date") && IsolatedStorageSettings.ApplicationSettings.Contains(dirfile))
            {
                if ((IsolatedStorageSettings.ApplicationSettings["Date"] as string) != DateTime.Today.ToString())
                {
                    id++;
                    HtmlWeb.LoadAsync("http://www.ltconline.ca/WebWatch/ada.aspx?r=" + routeNum.ToString() + "&id=" + id, DownloadCompleted);
                }
                else
                {
                    Load();
                }
            }
            else
            {
                id++;
                HtmlWeb.LoadAsync("http://www.ltconline.ca/WebWatch/ada.aspx?r=" + routeNum.ToString() + "&id=" + id, DownloadCompleted);
            }
            
        }

        void Load()
        {

            var newDirectionList = IsolatedStorageSettings.ApplicationSettings[dirfile] as ObservableCollection<DirectionC>;

             Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    
                    DirectionList.Clear();
                    // copy the list of forecast time periods over
                    foreach (DirectionC directionC in newDirectionList)
                    {
                        DirectionList.Add(directionC);
                        
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

                    DirectionC newC;

                    ObservableCollection<DirectionC> newDirectionList =
                    new ObservableCollection<DirectionC>();


                    foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//*[@class=\"ada\"]"))
                    {
                        try
                        {
                            string c;
                            //link.SelectSingleNode("/table");
                            newC = new DirectionC();
                            c = link.Attributes["title"].Value;

                            newC.DirectionName = c;
                            dirName = c;
                            newDirectionList.Add(newC);
                        }
                        catch (FormatException)
                        {

                        }
                    }




                    // copy the data over
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {

                        Vis = Visibility.Collapsed;

                        DirectionList.Clear();


                        //Saving stuff
                        var settings = IsolatedStorageSettings.ApplicationSettings;


                        //Saving Direction List
                        if (settings.Contains(dirfile))
                        {
                            settings[dirfile] = newDirectionList;
                        }
                        else
                        {
                            settings.Add(dirfile, newDirectionList);
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
                        foreach (DirectionC directionC in newDirectionList)
                        {
                            DirectionList.Add(directionC);

                        }



                    });



                }

            }
            else
            {
                Vis = Visibility.Collapsed;
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
