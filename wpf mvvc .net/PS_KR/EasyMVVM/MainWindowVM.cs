using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMVVM
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        private ObservableCollection<string> _backingProperty;

        public MainWindowVM()
        {
            var model = new Model();
            BoundProperty = model.GetData();
        }

        public ObservableCollection<string> BoundProperty
        {
            get => _backingProperty;
            set
            {
                _backingProperty = value;
                PropChanged(nameof(BoundProperty));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void PropChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
