using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfMVVM_MySql_Login
{
    /// <summary>
    /// Логика взаимодействия для WinTable.xaml
    /// </summary>
    public partial class WinTable : Window
    {
        public WinTable()
        {
            InitializeComponent();
            this.DataContext = new ViewModel.WinTableViewModel();
        }
    }
}
