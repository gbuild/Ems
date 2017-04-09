using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Configuration;
using MainForm.ViewModel;

namespace MainForm.Pages
{
    /// <summary>
    /// Логика взаимодействия для Welcome.xaml
    /// </summary>
    public partial class Welcome : Page
    {
        public Welcome()
        {
            InitializeComponent();
        }

        string cn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\blagi\Source\Repos\Ems\Ems\MainForm\ApplicationData.mdf;Integrated Security = True";
        DataTable userTable;

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            userTable = new DataTable();
            if (LoginBox.Text.Length <1 || PasswordBox.Password.Length <1)
            {
                MessageBox.Show("Значения Логин|Пароль не могут быть пустыми.","Ошибка при запуске приложения.");
                return;
            }
            try
            {
                SqlConnection connection = new SqlConnection(cn);
                SqlCommand command = new SqlCommand("Select * from tbl_login where UserName=@username and Password=@password", connection);
                command.Parameters.AddWithValue("@username", LoginBox.Text);
                command.Parameters.AddWithValue("@password", PasswordBox.Password); 
                connection.Open();
                SqlDataAdapter adapt = new SqlDataAdapter(command);
                DataSet dataset = new DataSet();
                adapt.Fill(dataset);
                connection.Close();
                
                int count = dataset.Tables[0].Rows.Count;
                if (count == 1)
                {
                    NavigationService.Navigate(new Uri("Pages/SecondOne.xaml", UriKind.Relative));
                    MessageBox.Show("Succses");


                }
                else
                {
                    MessageBox.Show("\n Пользователь с указной парой Логин|Пароль не найден. \n Возможно вы неверно ввели пароль.","Ошибка при авторизации пользователя.");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Отсутсвует подключение к базе данных, обратитесь к администратору \n {ex.Message}","Ошибка при авторизации пользователя.");
            }


        }
    }
}
