using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using ZzaDashboard.Customers;

namespace ZzaDashboard
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private Timer _timer = new Timer(5000);

        public MainWindowViewModel()
        {
            CurrentViewModel = new CustomerListViewModel();
            _timer.Elapsed += (s, e) => NotificationMessage = "At the tone the timer will be: " + DateTime.Now.ToLocalTime();
            _timer.Start();
        }

        public object CurrentViewModel { get; set; }

        private string _notificationMessage;
        public string NotificationMessage
        {
            get
            {
                return _notificationMessage;
            }
            set
            {
                if (value != _notificationMessage)
                {
                    _notificationMessage = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("NotificationMessage"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
