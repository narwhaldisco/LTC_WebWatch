using System.ComponentModel;

namespace WebRequest_test2
{
    public class TimesC
    {
        private string timesName;

        public TimesC()
        {
        }

        public string TimesName
        {
            get
            {
                return timesName;
            }
            set
            {
                if (value != timesName)
                {
                    this.timesName = value;
                }
            }
        }


    }
}
