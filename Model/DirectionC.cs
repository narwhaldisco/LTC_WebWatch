using System.ComponentModel;

namespace WebRequest_test2
{
    public class DirectionC
    {
        private string directionName;

        public DirectionC()
        {
        }

        public string DirectionName
        {
            get
            {
                return directionName;
            }
            set
            {
                if (value != directionName)
                {
                    this.directionName = value;
                }
            }
        }


    }
}
