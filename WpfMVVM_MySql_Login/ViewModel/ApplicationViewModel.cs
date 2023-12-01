using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using OxyPlot;
using OxyPlot.Series;

namespace WpfMVVM_MySql_Login.ViewModel
{
    internal class ApplicationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private string login;
        public string Login
        {
            get
            {
                return login;
            }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }

        private string password;
        public string Password
        {
            get
            {
                if (password == null && !string.IsNullOrWhiteSpace(Properties.Settings.Default.Passwordlast))
                    password = Properties.Settings.Default.Passwordlast;
                return password;
            }
            set
            {
                password = value;
                Properties.Settings.Default.Passwordlast = password;
                OnPropertyChanged("Password");
            }
        }

        private RelayCommand exitApp;
        public RelayCommand ExitApp
        {
            get
            {
                return exitApp ?? (new RelayCommand(obj =>
                {
                    Application.Current.Shutdown(0);
                }));
            }
        }

        private RelayCommand loginApp;

        public RelayCommand LoginApp
        {
            get
            {
                return loginApp ?? (new RelayCommand(obj =>
                {
                    System.Data.SqlClient.SqlConnectionStringBuilder strSql = new System.Data.SqlClient.SqlConnectionStringBuilder();
                    strSql.DataSource = "DESKTOP-4AI1AAE\\SQLEXPRESS";
                    strSql.InitialCatalog = "KirillLB";
                    strSql.IntegratedSecurity = true;

                    using (SqlConnection connect = new SqlConnection(strSql.ConnectionString))
                    {
                        connect.Open();
                        SqlCommand command = new SqlCommand($"use KirillLB Select * From Account_Login Where [Login] = \'{this.login}\' and [Password] = \'{this.password}\'", connect);
                        var resultCount = command.ExecuteReader();
                        
                        if(resultCount.HasRows)
                        {
                            WinTable win = new WinTable();
                            win.Show();
                        }
                        else
                        {
                            MessageBox.Show("BAD");
                        }
                    }
                    

                    //here we connect and login mysql server
                }));
            }
        }

        private RelayCommand enterInput;

        public RelayCommand EnterInput
        {
            get
            {
                return enterInput ?? (new RelayCommand(obj =>
                {
                    Console.WriteLine(obj);
                }));
            }
        }



        
    }


}
