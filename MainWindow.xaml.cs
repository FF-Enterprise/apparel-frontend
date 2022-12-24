using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace Apparel_Factory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<Product> list = new List<Product>();

            string conn = "https://localhost:7094/api/Apparel";

            var client = new RestClient(conn);
            var request = new RestRequest();
            request.AddHeader("Accept", "application/json");
            var response = client.Execute(request);


            JArray jArray = JArray.Parse(response.Content!);

            foreach (JObject item in jArray)
            {
                var itemJson = JObject.Parse(item.ToString());
                Product tempApparel = new Product();

                tempApparel.id = itemJson["id"]!.ToString();
                tempApparel.Name = itemJson["name"]!.ToString();
                tempApparel.Category = itemJson["category"]!.ToString();
                tempApparel.Date = itemJson["production_date"]!.ToString();
                tempApparel.Amount = itemJson["prodcution_amount"]!.ToString();
                tempApparel.Price = itemJson["price_per_pcs"]!.ToString();

                DataGridXAML.Items.Add(tempApparel);
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private bool _isMaximized = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (_isMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    _isMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    _isMaximized = true;
                }
            }
        }

        private void Exit_Button(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Add_Button(object sender, EventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            addWindow.ShowDialog();
        }

        private void Refresh_Button(object sender, EventArgs e)
        {
            DataGridXAML.Items.Clear();
            List<Product> list = new List<Product>();

            string conn = "https://localhost:7094/api/Apparel";

            var client = new RestClient(conn);
            var request = new RestRequest();
            request.AddHeader("Accept", "application/json");
            var response = client.Execute(request);


            JArray jArray = JArray.Parse(response.Content!);

            foreach (JObject item in jArray)
            {
                var itemJson = JObject.Parse(item.ToString());
                Product tempApparel = new Product();

                tempApparel.id = itemJson["id"]!.ToString();
                tempApparel.Name = itemJson["name"]!.ToString();
                tempApparel.Category = itemJson["category"]!.ToString();
                tempApparel.Date = itemJson["production_date"]!.ToString();
                tempApparel.Amount = itemJson["prodcution_amount"]!.ToString();
                tempApparel.Price = itemJson["price_per_pcs"]!.ToString();

                DataGridXAML.Items.Add(tempApparel);
            }
        }

        public void refresh()
        {
            DataGridXAML.Items.Clear();
            List<Product> list = new List<Product>();

            string conn = "https://localhost:7094/api/Apparel";

            var client = new RestClient(conn);
            var request = new RestRequest();
            request.AddHeader("Accept", "application/json");
            var response = client.Execute(request);


            JArray jArray = JArray.Parse(response.Content!);

            foreach (JObject item in jArray)
            {
                var itemJson = JObject.Parse(item.ToString());
                Product tempApparel = new Product();

                tempApparel.id = itemJson["id"]!.ToString();
                tempApparel.Name = itemJson["name"]!.ToString();
                tempApparel.Category = itemJson["category"]!.ToString();
                tempApparel.Date = itemJson["production_date"]!.ToString();
                tempApparel.Amount = itemJson["prodcution_amount"]!.ToString();
                tempApparel.Price = itemJson["price_per_pcs"]!.ToString();

                DataGridXAML.Items.Add(tempApparel);
            }
        }

        private void Delete_Button(object sender, RoutedEventArgs e)
        {
            object ID = ((Button)sender).CommandParameter;
            var deleteID = ID.ToString();
            var delete = Int32.Parse(deleteID!);
            string conn = "https://localhost:7094/api";

            var client = new RestClient(conn);
            var request = new RestRequest("Apparel", Method.Delete);

            request.AddQueryParameter("searchID", delete);

            var response = client.Execute(request);

            MessageBox.Show("Berhasil Menghapus Data");
            refresh();
        }

        public void Edit_Button(object sender, RoutedEventArgs e)
        {
            Product? edit = DataGridXAML.SelectedItem as Product;
            string? idP = Convert.ToString(edit!.id);
            string? nameP = Convert.ToString(edit!.Name);
            string? categoryP = Convert.ToString(edit!.Category);
            string? dateP = Convert.ToString(edit!.Date);
            string? amountP = Convert.ToString(edit!.Amount);
            string? priceP = Convert.ToString(edit!.Price);

            EditWindow editWindow = new EditWindow(idP!, nameP!, categoryP!, dateP!, amountP!, priceP!);
            editWindow.ShowDialog();
  

        }

        public class Product
        {
            public string? id { get; set; }
            public string? Name { get; set; }
            public string? Category { get; set; }
            public string? Date { get; set; }
            public string? Amount { get; set; }
            public string? Price { get; set; }
            public Brush? BgColor { get; set; }
        }
    }
}
