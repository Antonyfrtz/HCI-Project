using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp1
{
    public class ViewModel : INotifyPropertyChanged
    {
        private int _badgeValue;
        public int BadgeValue
        {
            get { return _badgeValue; }
            set { _badgeValue = value; NotifyPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}