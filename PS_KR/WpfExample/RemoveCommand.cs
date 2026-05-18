using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfExample
{
    public class RemoveCommand : ICommand
    {
        public void Execute(object parameter)
        {
            if (parameter is NamesList nameList)
                nameList.Names.Remove(nameList.SelectedName);
        }

        public bool CanExecute(object parameter) => true;
        public event EventHandler CanExecuteChanged;
    }
}
