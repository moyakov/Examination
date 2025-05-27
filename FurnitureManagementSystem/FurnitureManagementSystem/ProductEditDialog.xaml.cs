using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace FurnitureManagementSystem
{
    public partial class ProductEditDialog : Window
    {
        public int ProductId { get; private set; }
        public string ProductName => productNameTextBox.Text;
        public string ArticleNumber => articleNumberTextBox.Text;
        public decimal MinPartnerPrice => decimal.Parse(priceTextBox.Text);
        public int ProductTypeId { get; private set; }
        public int MaterialTypeId { get; private set; }
        public List<WorkshopInfo> Workshops { get; private set; } = new List<WorkshopInfo>();

        public ProductEditDialog(bool isAdding = true)
        {
            InitializeComponent();
            Title = isAdding ? "Добавление продукта" : "Редактирование продукта";
            LoadProductTypes();
            LoadMaterials();
        }

        public ProductEditDialog(ProductInfo product) : this(false)
        {
            ProductId = product.ProductId;
            productNameTextBox.Text = product.ProductName;
            articleNumberTextBox.Text = product.ArticleNumber;
            priceTextBox.Text = product.MinPartnerPrice.ToString("F2");

            foreach (ComboBoxItem item in productTypeComboBox.Items)
            {
                if (item.Content.ToString() == product.ProductType.TypeName)
                {
                    productTypeComboBox.SelectedItem = item;
                    break;
                }
            }
        }

        private void LoadProductTypes()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["FurnitureDbConnection"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT product_type_id, type_name FROM product_type ORDER BY type_name";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var item = new ComboBoxItem
                        {
                            Content = reader["type_name"].ToString(),
                            Tag = (int)reader["product_type_id"]
                        };
                        productTypeComboBox.Items.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке типов продуктов: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadMaterials()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["FurnitureDbConnection"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT material_type_id, material_name FROM material_type ORDER BY material_name";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var item = new ComboBoxItem
                        {
                            Content = reader["material_name"].ToString(),
                            Tag = (int)reader["material_type_id"]
                        };
                        materialComboBox.Items.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке материалов: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
                return;

            if (productTypeComboBox.SelectedItem is ComboBoxItem selectedType)
            {
                ProductTypeId = (int)selectedType.Tag;
            }

            if (materialComboBox.SelectedItem is ComboBoxItem selectedMaterial)
            {
                MaterialTypeId = (int)selectedMaterial.Tag;
            }

            DialogResult = true;
            Close();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(ProductName))
            {
                MessageBox.Show("Введите наименование продукта", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(ArticleNumber))
            {
                MessageBox.Show("Введите артикул продукта", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!decimal.TryParse(priceTextBox.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("Введите корректную цену (положительное число)", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (productTypeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите тип продукта", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (materialComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите основной материал", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }

    public class WorkshopInfo
    {
        public string WorkshopName { get; set; }
        public decimal ProductionTime { get; set; }
    }
}