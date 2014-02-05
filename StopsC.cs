using System.ComponentModel;

namespace WebRequest_test2
{
    public class StopsC
    {
        private string stopsName;
        public string addName;
        private int direction;
        private string dir;
        private int routeNum;
        private string routeName;


        public StopsC()
        {
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
                    this.routeName = value;
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
                    this.routeNum = value;
                }
            }
        }

        public int Direction
        {
            get
            {
                return direction;
            }
            set
            {
                if (value != direction)
                {
                    this.direction = value;
                }
            }
        }

        public string Dir
        {
            get
            {
                return dir;
            }
            set
            {
                if (value != dir)
                {
                    this.dir = value;
                }
            }
        }

        public string StopsName
        {
            get
            {
                return stopsName;
            }
            set
            {
                if (value != stopsName)
                {
                    this.stopsName = value;
                }
            }
        }

    

        public string AddName
        {
            get
            {
                return addName;
            }
            set
            {
                if (value != addName)
                {
                    this.addName = value;
                }
            }
        }


    }
}
