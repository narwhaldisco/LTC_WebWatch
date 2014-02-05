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
using System.Collections.Generic;

namespace WebRequest_test2
{
    public class Times : INotifyPropertyChanged
    {

        private Visibility vis;
        int id = 0; 

        
        //set up refresh button

      


        public Visibility Vis
        {
            get { return vis; }

            set
            {
                vis = value;
                NotifyPropertyChanged("Vis");
            }
        }

        public ObservableCollection<TimesC> TimesList
        {
            get;
            set;
        }

        public Times()
        {
            TimesList = new ObservableCollection<TimesC>();
        }

        public void Refresh(string add)
        {
            TimesList.Clear();

            Deployment.Current.Dispatcher.BeginInvoke(() => 
            {


            id++;


            Vis = Visibility.Visible;
            HtmlWeb.LoadAsync("http://www.ltconline.ca/WebWatch/ada.aspx" + add + "&id=" + id, DownloadCompleted);
               
            });

           

        }

        public void GetTimes(string add)
        {
            Deployment.Current.Dispatcher.BeginInvoke(
                () => { Vis = Visibility.Visible; });
            id++;

            HtmlWeb.LoadAsync("http://www.ltconline.ca/WebWatch/ada.aspx" + add + "&id=" + id, DownloadCompleted);
        }

        void DownloadCompleted(object sender, HtmlDocumentLoadCompleted e)
        {
            if (e.Error == null)
            {
                HtmlDocument doc = e.Document;
                if (doc != null)
                {

                    TimesC newC;

                    ObservableCollection<TimesC> newTimesList =
                    new ObservableCollection<TimesC>();

                    List<string> names = new List<string>();

                    foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//*[@class=\"ada\"]"))
                    {
                        try
                        {
                            string c;
                            c = link.InnerText;
                            names.Add(c);
                        }
                        catch (FormatException)
                        {

                        }
                    }

                    int i = 1;


                    HtmlNodeCollection test;
                    test = doc.DocumentNode.SelectNodes("//td[@align=\"left\"]");

                    if (test != null)
                    {
                        foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//td[@align=\"left\"]"))
                        {
                            try
                            {
                                string c;
                                c = link.InnerText;
                                names[i] += c;
                                i++;

                            }
                            catch (FormatException)
                            {

                            }
                        }
                    }

                    foreach (string name in names)
                    {
                        newC = new TimesC();
                        newC.TimesName = name;
                        newTimesList.Add(newC);
                    }


                   

                  


                    // copy the data over
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {

                        Vis = Visibility.Collapsed;

                        TimesList.Clear();

                        

                        // copy the list of forecast time periods over
                        foreach (TimesC timesC in newTimesList)
                        {
                            TimesList.Add(timesC);
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
