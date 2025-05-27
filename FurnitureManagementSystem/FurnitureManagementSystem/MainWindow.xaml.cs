using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace FurnitureManagementSystem
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadProductsData();
        }

        private void LoadProductsData()
        {
            try
            {
                List<ProductInfo> products = new List<ProductInfo>();
                string connectionString = ConfigurationManager.ConnectionStrings["FurnitureDbConnection"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT 
                            p.product_id,
                            pt.type_name AS ProductType,
                            p.product_name AS ProductName,
                            p.article_number AS ArticleNumber,
                            p.min_partner_price AS MinPartnerPrice,
                            SUM(pw.production_time) AS TotalProductionTime
                        FROM 
                            products p
                            JOIN product_type pt ON p.product_type_id = pt.product_type_id
                            LEFT JOIN product_workshops pw ON p.product_name = pw.product_name
                        GROUP BY
                            p.product_id, pt.type_name, p.product_name, p.article_number, p.min_partner_price
                        ORDER BY
                            p.product_name";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ProductInfo product = new ProductInfo
                        {
                            ProductId = (int)reader["product_id"],
                            ProductType = new ProductType { TypeName = reader["ProductType"].ToString() },
                            ProductName = reader["ProductName"].ToString(),
                            ArticleNumber = reader["ArticleNumber"].ToString(),
                            MinPartnerPrice = (decimal)reader["MinPartnerPrice"],
                            TotalProductionTime = reader["TotalProductionTime"] != DBNull.Value ?
                                Convert.ToDecimal(reader["TotalProductionTime"]) : 0
                        };

                        products.Add(product);
                    }
                    reader.Close();
                }

                productsGrid.ItemsSource = products;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ProductEditDialog();
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["FurnitureDbConnection"].ConnectionString;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string query = @"INSERT INTO products (product_type_id, product_name, article_number, min_partner_price, material_type_id) 
                              VALUES (@ProductTypeId, @ProductName, @ArticleNumber, @MinPartnerPrice, @MaterialTypeId);
                              SELECT SCOPE_IDENTITY();";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ProductTypeId", dialog.ProductTypeId);
                        command.Parameters.AddWithValue("@ProductName", dialog.ProductName);
                        command.Parameters.AddWithValue("@ArticleNumber", dialog.ArticleNumber);
                        command.Parameters.AddWithValue("@MinPartnerPrice", dialog.MinPartnerPrice);
                        command.Parameters.AddWithValue("@MaterialTypeId", dialog.MaterialTypeId);

                        int productId = Convert.ToInt32(command.ExecuteScalar());

                        foreach (var workshop in dialog.Workshops)
                        {
                            query = @"INSERT INTO product_workshops (product_name, workshop_name, production_time)
                                      VALUES (@ProductName, @WorkshopName, @ProductionTime)";

                            command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@ProductName", dialog.ProductName);
                            command.Parameters.AddWithValue("@WorkshopName", workshop.WorkshopName);
                            command.Parameters.AddWithValue("@ProductionTime", workshop.ProductionTime);

                            command.ExecuteNonQuery();
                        }
                    }

                    LoadProductsData();
                    MessageBox.Show("Продукт успешно добавлен", "Успех",
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при добавлении продукта: {ex.Message}", "Ошибка",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            if (productsGrid.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите продукт для редактирования", "Внимание",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (productsGrid.SelectedItem is ProductInfo selectedProduct)
            {
                var dialog = new ProductEditDialog(selectedProduct);
                if (dialog.ShowDialog() == true)
                {
                    try
                    {
                        string connectionString = ConfigurationManager.ConnectionStrings["FurnitureDbConnection"].ConnectionString;
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            // Обновление продукта
                            string query = @"UPDATE products 
                                   SET product_type_id = @ProductTypeId,
                                       product_name = @ProductName, 
                                       article_number = @ArticleNumber, 
                                       min_partner_price = @MinPartnerPrice,
                                       material_type_id = @MaterialTypeId
                                   WHERE product_id = @ProductId";

                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@ProductTypeId", dialog.ProductTypeId);
                            command.Parameters.AddWithValue("@ProductName", dialog.ProductName);
                            command.Parameters.AddWithValue("@ArticleNumber", dialog.ArticleNumber);
                            command.Parameters.AddWithValue("@MinPartnerPrice", dialog.MinPartnerPrice);
                            command.Parameters.AddWithValue("@MaterialTypeId", dialog.MaterialTypeId);
                            command.Parameters.AddWithValue("@ProductId", selectedProduct.ProductId);

                            command.ExecuteNonQuery();

                            query = "DELETE FROM product_workshops WHERE product_name = @ProductName";
                            command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@ProductName", dialog.ProductName);
                            command.ExecuteNonQuery();

                            foreach (var workshop in dialog.Workshops)
                            {
                                query = @"INSERT INTO product_workshops (product_name, workshop_name, production_time)
                                          VALUES (@ProductName, @WorkshopName, @ProductionTime)";

                                command = new SqlCommand(query, connection);
                                command.Parameters.AddWithValue("@ProductName", dialog.ProductName);
                                command.Parameters.AddWithValue("@WorkshopName", workshop.WorkshopName);
                                command.Parameters.AddWithValue("@ProductionTime", workshop.ProductionTime);

                                command.ExecuteNonQuery();
                            }
                        }

                        LoadProductsData();
                        MessageBox.Show("Продукт успешно обновлен", "Успех",
                                        MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при обновлении продукта: {ex.Message}", "Ошибка",
                                      MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (productsGrid.SelectedItem is ProductInfo selectedProduct)
            {
                MessageBoxResult result = MessageBox.Show(
                    $"Вы уверены, что хотите удалить продукт {selectedProduct.ProductName}?",
                    "Подтверждение удаления",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        string connectionString = ConfigurationManager.ConnectionStrings["FurnitureDbConnection"].ConnectionString;
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            string deleteWorkshopsQuery = "DELETE FROM product_workshops WHERE product_name = @ProductName";
                            SqlCommand deleteWorkshopsCommand = new SqlCommand(deleteWorkshopsQuery, connection);
                            deleteWorkshopsCommand.Parameters.AddWithValue("@ProductName", selectedProduct.ProductName);
                            deleteWorkshopsCommand.ExecuteNonQuery();

                            string deleteProductQuery = "DELETE FROM products WHERE product_id = @ProductId";
                            SqlCommand deleteProductCommand = new SqlCommand(deleteProductQuery, connection);
                            deleteProductCommand.Parameters.AddWithValue("@ProductId", selectedProduct.ProductId);
                            deleteProductCommand.ExecuteNonQuery();
                        }

                        LoadProductsData();
                        MessageBox.Show($"Продукт {selectedProduct.ProductName} успешно удален", "Успех",
                                      MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении продукта: {ex.Message}", "Ошибка",
                                      MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите продукт для удаления", "Внимание",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }

    public class ProductInfo
    {
        public int ProductId { get; set; }
        public ProductType ProductType { get; set; }
        public string ProductName { get; set; }
        public string ArticleNumber { get; set; }
        public decimal MinPartnerPrice { get; set; }
        public decimal TotalProductionTime { get; set; }
    }

    public class ProductType
    {
        public string TypeName { get; set; }
    }
}