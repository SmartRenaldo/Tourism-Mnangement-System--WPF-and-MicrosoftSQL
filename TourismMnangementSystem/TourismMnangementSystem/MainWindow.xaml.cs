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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace TourismMnangementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection sqlConnection;

        public MainWindow()
        {
            InitializeComponent();

            string conncetionString = ConfigurationManager.ConnectionStrings["TourismMnangementSystem.Properties.Settings.TestingDatabaseConnectionString"].ConnectionString;
            sqlConnection = new SqlConnection(conncetionString);
            ShowCities();
        }

        private void ShowCities()
        {
            try
            {
                string query = "select * from City";
                SqlDataAdapter adapter = new SqlDataAdapter(query, sqlConnection);

                using (adapter)
                {
                    DataTable cityTable = new DataTable();

                    adapter.Fill(cityTable);

                    cities.DisplayMemberPath = "Name";
                    cities.SelectedValuePath = "Id";
                    cities.ItemsSource = cityTable.DefaultView;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
