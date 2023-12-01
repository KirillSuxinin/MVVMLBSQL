using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfMVVM_MySql_Login.ViewModel
{
    internal class DataTable_ViewModel : INotifyPropertyChanged
    {
        private string login;

        private string password;

        public string Login
        {
            get
            {
                return login;
            }
            set
            {
                string lastData = login;
                login = value;
                if (lastData != null)
                {
                    string sqlTransaction = $"Use KirillLB Update Account_Login set [Login] = \'{login}\' Where [Login] = \'{lastData}\'";
                    System.Data.SqlClient.SqlConnectionStringBuilder strSql = new System.Data.SqlClient.SqlConnectionStringBuilder();
                    strSql.DataSource = "DESKTOP-4AI1AAE\\SQLEXPRESS";
                    strSql.InitialCatalog = "KirillLB";
                    strSql.IntegratedSecurity = true;
                    using (SqlConnection connect = new SqlConnection(strSql.ConnectionString))
                    {
                        connect.Open();
                        SqlCommand command = new SqlCommand(sqlTransaction, connect);
                        int a = command.ExecuteNonQuery();
                    }
                }
                OnPropertyChanged("Login");
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                string lastData = password;
                password = value;
                if (lastData != null)
                {
                    string sqlTransaction = $"Use KirillLB Update Account_Login set [Password] = \'{password}\' Where [Password] = \'{lastData}\'";
                    System.Data.SqlClient.SqlConnectionStringBuilder strSql = new System.Data.SqlClient.SqlConnectionStringBuilder();
                    strSql.DataSource = "DESKTOP-4AI1AAE\\SQLEXPRESS";
                    strSql.InitialCatalog = "KirillLB";
                    strSql.IntegratedSecurity = true;
                    using (SqlConnection connect = new SqlConnection(strSql.ConnectionString))
                    {
                        connect.Open();
                        SqlCommand command = new SqlCommand(sqlTransaction, connect);
                        int result = command.ExecuteNonQuery();
                        Console.WriteLine(result);
                    }
                }
                OnPropertyChanged("Password");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    internal class WinTableViewModel : INotifyPropertyChanged
    {
        private DataTable_ViewModel selected_Data;

        public ObservableCollection<DataTable_ViewModel> DataTables { get; set; }

        public DataTable_ViewModel Selected_Data
        {
            get
            {
                return selected_Data;
            }
            set
            {
                selected_Data = value;
                OnPropertyChanged("Selected_Data");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public WinTableViewModel()
        {
            System.Data.SqlClient.SqlConnectionStringBuilder strSql = new System.Data.SqlClient.SqlConnectionStringBuilder();
            strSql.DataSource = "DESKTOP-4AI1AAE\\SQLEXPRESS";
            strSql.InitialCatalog = "KirillLB";
            strSql.IntegratedSecurity = true;

            using (SqlConnection connect = new SqlConnection(strSql.ConnectionString))
            {
                connect.Open();
                DataTables = new ObservableCollection<DataTable_ViewModel>();
                SqlCommand command = new SqlCommand($"use KirillLB Select * From Account_Login", connect);
                var a = command.ExecuteReader();
                while (a.Read())
                {
                    DataTables.Add(new DataTable_ViewModel() { Login = a[0].ToString(), Password = a[1].ToString() });
                }
            }
        }
    }
}
