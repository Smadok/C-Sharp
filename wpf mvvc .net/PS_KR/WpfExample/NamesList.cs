using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfExample
{
    public class NamesList : INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;
        private string _selectedName;
        private readonly AddCommand _addNameCommand = new AddCommand();
        private readonly RemoveCommand _removeNameCommand = new RemoveCommand();

        public NamesList()
        {
            Names = new ObservableCollection<string>();
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string SelectedName
        {
            get => _selectedName;
            set
            {
                _selectedName = value;
                OnPropertyChanged(nameof(SelectedName));
            }
        }

        public ObservableCollection<string> Names { get; }
        public AddCommand AddNameCommand => _addNameCommand;
        public RemoveCommand RemoveNameCommand => _removeNameCommand;


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
