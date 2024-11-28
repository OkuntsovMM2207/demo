using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Demo.View
{
    public partial class List : Window
    {
        private readonly string _connectionString = "Server=(local);Database=demo;Trusted_Connection=True";

        public List()
        {
            InitializeComponent();
            LoadRequests();
        }

        // Загрузка данных из базы в DataGrid
        private void LoadRequests()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "SELECT requestID, startDate, orgTechType, orgTechModel, problemDescryption, requestStatus FROM inputDataRequests";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    RequestsDataGrid.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Обработчик кнопки "Добавить"
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO inputDataRequests (startDate, orgTechType, orgTechModel, problemDescryption, requestStatus) " +
                                   "VALUES (@StartDate, @OrgTechType, @OrgTechModel, @ProblemDescription, @RequestStatus)";

                    SqlCommand command = new SqlCommand(query, connection);

                    // Пример данных. Реализуйте окно для ввода пользовательских данных
                    command.Parameters.AddWithValue("@StartDate", DateTime.Now);
                    command.Parameters.AddWithValue("@OrgTechType", "Пример техники");
                    command.Parameters.AddWithValue("@OrgTechModel", "Пример модели");
                    command.Parameters.AddWithValue("@ProblemDescription", "Описание проблемы");
                    command.Parameters.AddWithValue("@RequestStatus", "Новый");

                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Запрос добавлен успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadRequests();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления запроса: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Обработчик кнопки "Удалить"
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (RequestsDataGrid.SelectedItem is DataRowView selectedRow)
            {
                int requestId = Convert.ToInt32(selectedRow["requestID"]);

                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();

                        string query = "DELETE FROM inputDataRequests WHERE requestID = @RequestID";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@RequestID", requestId);
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Запрос удалён успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadRequests();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления запроса: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Выберите запрос для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
