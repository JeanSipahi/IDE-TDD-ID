using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Exam_Biblio
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowAllBooks_Click(object sender, RoutedEventArgs e)
        {
            LoadDataFromDatabase();
        }

        private void LoadDataFromDatabase()
        {
            try
            {
                // Define connection string
                string connectionString = "Server=D107981-650\\SQLEXPRESS;Initial Catalog=BIblio;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Define SQL query
                    string query = "SELECT * FROM Livres";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGrid
                        dta.ItemsSource = dataTable.DefaultView;

                        // Show the DataGrid
                        dta.Visibility = Visibility.Visible;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            FindTitles();
        }

        private void FindTitles()
        {
            try
            {
                // Check if DataGrid is null and initialize it if needed
                if (dta == null)
                {
                    LoadDataFromDatabase();
                }

                // Get the search keyword from the TextBox
                string searchKeyword = searchTextBox.Text.Trim();

                // Define connection string
                string connectionString = "Server=D107981-650\\SQLEXPRESS;Initial Catalog=BIblio;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Define SQL query with a WHERE clause to filter by title
                    string query = $"SELECT * FROM Livres WHERE Titre LIKE '%{searchKeyword}%'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the filtered DataTable to the DataGrid
                        dta.ItemsSource = dataTable.DefaultView;

                        // Show the DataGrid
                        dta.Visibility = Visibility.Visible;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
