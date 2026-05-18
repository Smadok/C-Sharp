using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfExample
{
    public class AddCommand : ICommand
    {
        public void Execute(object parameter)
        {
            if (parameter is NamesList nameList)
            {
                var newName = $"{nameList.FirstName} {nameList.LastName}";
                nameList.Names.Add(newName);
                nameList.FirstName = nameList.LastName = "";
            }
        }

        public bool CanExecute(object parameter) => true;
        public event EventHandler CanExecuteChanged;
    }
}
