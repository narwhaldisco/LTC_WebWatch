using System.ComponentModel;

namespace WebRequest_test2
{
    public class Route
    {
        private string routeName;
        private int routeNum;
        private string fullName;



        public string FullName
        {
            get
            {
                return fullName;
            }
            set
            {
                if (value != fullName)
                {
                    fullName = value;
                }
            }
        }

        public string RouteName
        {
            get
            {
                return routeName;
            }
            set
            {
                if (value != routeName)
                {
                    routeName = value;
                }
            }
        }

        public int RouteNum
        {
            get
            {
                return routeNum;
            }
            set
            {
                if (value != routeNum)
                {
                    routeNum = value;
                }
            }
        }

        public Route(string routeName, int routeNum)
        {
            RouteName = routeName;
            RouteNum = routeNum;
            FullName = routeNum + " " + routeName;


        }



    }
}
