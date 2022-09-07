﻿using System;
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
            ShowTourisms();
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

        private void ShowAssociatedTourism()
        {
            try
            {
                string query = "select * from Tourism t left join CityTourismRelationship ctr" +
                    " on t.Id = ctr.TourismId where ctr.CityId = @CityId";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);

                using (adapter)
                {
                    sqlCommand.Parameters.AddWithValue("@CityId", cities.SelectedValue);
                    DataTable cityTable = new DataTable();

                    adapter.Fill(cityTable);

                    associatedTourisms.DisplayMemberPath = "Name";
                    associatedTourisms.SelectedValuePath = "Id";
                    associatedTourisms.ItemsSource = cityTable.DefaultView;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void ShowTourisms()
        {
            try
            {
                string query = "select * from Tourism";
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(query, sqlConnection);

                using (sqlAdapter)
                {
                    DataTable tourismTable = new DataTable();
                    sqlAdapter.Fill(tourismTable);

                    tourisms.DisplayMemberPath = "Name";
                    tourisms.SelectedValuePath = "Id";
                    tourisms.ItemsSource = tourismTable.DefaultView;
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());
            }
        }

        private void cities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowAssociatedTourism();
        }

        private void DelectCityClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "delete from City where id = @CityId";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@CityId", cities.SelectedValue);
                sqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sqlConnection.Close();
                ShowCities();
            }
        }

        private void AddCityClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "insert into City values (@Name)";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@Name", cityTextBox.Text);
                sqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sqlConnection.Close();
                ShowCities();
            }
        }
    }
}
