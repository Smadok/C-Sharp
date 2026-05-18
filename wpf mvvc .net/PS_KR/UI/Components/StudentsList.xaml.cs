using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DataLayer.Database;

namespace UI.Components
{
    /// <summary>
    /// Interaction logic for StudentsList.xaml
    /// </summary>
    public partial class StudentsList : UserControl
    {
        public StudentsList()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (var context = new DatabaseContext())
            {
                studentsGrid.ItemsSource = context.Users.ToList();
            }
        }
    }
}
