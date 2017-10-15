using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Configuration;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlDataAdapter adapter;
        DataSet dataset1;
        DataSet dataset2;
        DataSet dataset3;
        string cs;
        SqlConnection connection;
        public MainWindow()
        {
            
            InitializeComponent();
            cs = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
            
            //view.UpdateLayout();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection = new SqlConnection(cs);
                var command = new SqlCommand("select * from users", connection);
                connection.Open();
                view.Text += "Cоединение с базой данных установлено;\r\n";
                view.Text += "Отбор данных в локальное хранилище начат;\r\n";
                adapter = new SqlDataAdapter(command);
                dataset1 = new DataSet();
                adapter.Fill(dataset1, "Users");
                UsersGrid.ItemsSource = dataset1.Tables[0].DefaultView;
                command = new SqlCommand("select * from payments", connection);
                adapter = new SqlDataAdapter(command);
                dataset2 = new DataSet();
                adapter.Fill(dataset2, "Payments");
                PaymentsGrid.ItemsSource = dataset2.Tables[0].DefaultView;
                command = new SqlCommand("select * from goals", connection);
                adapter = new SqlDataAdapter(command);
                dataset3 = new DataSet();
                adapter.Fill(dataset3, "Goals");
                view.Text += "Отбор данных в локальное хранилище закончен;\r\n";
                GoalsGrid.ItemsSource = dataset3.Tables[0].DefaultView;
                view.Text += "отображение данных из локального хранилища в табличных элементах управления закончено!!!\r\n";
            }
            catch (Exception ex)
            {
                view.Text += "Ошибка: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                connection = new SqlConnection(cs);
                adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand("select * from users", connection);
                SqlCommandBuilder builder = new SqlCommandBuilder();
                builder.DataAdapter = adapter;
                adapter.UpdateCommand = builder.GetUpdateCommand();
                adapter.InsertCommand = builder.GetInsertCommand();
                adapter.DeleteCommand = builder.GetDeleteCommand();
                DataTable dt = dataset1.Tables["Users"];
                adapter.Update(dt);
                dt.AcceptChanges();
                adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand("select * from goals", connection);
                builder = new SqlCommandBuilder();
                builder.DataAdapter = adapter;
                adapter.UpdateCommand = builder.GetUpdateCommand();
                adapter.InsertCommand = builder.GetInsertCommand();
                adapter.DeleteCommand = builder.GetDeleteCommand();
                dt = dataset3.Tables["Goals"];
                adapter.Update(dt);
                dt.AcceptChanges();
                adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand("select * from payments", connection);
                builder = new SqlCommandBuilder();
                builder.DataAdapter = adapter;
                adapter.UpdateCommand = builder.GetUpdateCommand();
                adapter.InsertCommand = builder.GetInsertCommand();
                adapter.DeleteCommand = builder.GetDeleteCommand();
                dt = dataset2.Tables["Payments"];
                adapter.Update(dt);
                dt.AcceptChanges();
                view.Text += "Редактирование данных завершено и синхронизировано с БД\r\n";
            } catch (Exception ex)
            {
                view.Text += "Ошибка: " + ex.Message;
            } finally
            {
                connection.Close();
            }  
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try 
            {
                connection = new SqlConnection(cs);
                var command = new SqlCommand("select * from users where users.surname = @surname", connection);
                command.Parameters.AddWithValue("@surname", User.Text);
                connection.Open();
                view.Text += "Поиск пользователя по фамилии начат\r\n";
                adapter = new SqlDataAdapter(command);
                dataset1 = new DataSet();
                adapter.Fill(dataset1, "Founded");
                view.Text += "Поиск завершен\r\n";
            }
            catch (Exception ex)
            {
                view.Text += "Ошибка: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }
            FindUser f = new FindUser(dataset1.Tables["Founded"].DefaultView);
            f.Show();
            f.Activate();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            try 
            {
                connection = new SqlConnection(cs);
                var command = new SqlCommand("select payments.paymentId, payments.Amount, payments.Date, payments.AccountId, goals.GoalName from payments inner join goals on payments.goalId=goals.goalId where goals.goalname = @goal", connection);
                command.Parameters.AddWithValue("@goal", Goal.Text);
                connection.Open();
                view.Text += "Поиск платежей по цели начат\r\n";
                adapter = new SqlDataAdapter(command);
                dataset1 = new DataSet();
                adapter.Fill(dataset1, "Founded");
                view.Text += "Поиск платежей завершен\r\n";
            }
            catch(Exception ex)
            {
                view.Text += "Ошибка: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }
            FindUser f = new FindUser(dataset1.Tables["Founded"].DefaultView);
            f.Show();
            f.Activate();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            try 
            {
                connection = new SqlConnection(cs);
                var command = new SqlCommand("select * from goals where goalName = @goal", connection);
                command.Parameters.AddWithValue("@goal", Goal2.Text);
                connection.Open();
                view.Text += "Поиск целей по названию начат\r\n";
                adapter = new SqlDataAdapter(command);
                dataset1 = new DataSet();
                adapter.Fill(dataset1, "Founded");
                view.Text += "Поиск целей завершен\r\n";
            }
            catch(Exception ex)
            {
                view.Text += "Ошибка: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }
            FindUser f = new FindUser(dataset1.Tables["Founded"].DefaultView);
            f.Show();
            f.Activate();
        }
    }
}
