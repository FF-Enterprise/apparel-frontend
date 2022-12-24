using System;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
namespace Apparel_Factory;

public partial class EditWindow : Window
{
    public EditWindow(string id, string name, string category, string date, string amount, string price)
    {
        InitializeComponent();
        IDBox.Text = id;
        Name.Text = name;
        Category.Text = category;
        Date.Text = date;
        Amount.Text = amount;
        Price.Text = price;
    }
    
    private void Exit_Button(object sender, EventArgs e)
    {
        this.Close();
    }

    private void Add_Button(object sender, EventArgs e)
    {
        ///isi untuk add data
        string conn = "https://localhost:7094/api";

        var client = new RestClient(conn);
        var request = new RestRequest("Apparel", RestSharp.Method.Put);

        request.AddQueryParameter("UpdateID", IDBox.Text);
        request.AddQueryParameter("UpdateName", Name.Text);
        request.AddQueryParameter("UpdateCategory", Category.Text);
        request.AddQueryParameter("UpdateProduction_date", Date.Text);
        request.AddQueryParameter("UpdateProdcution_amount", Amount.Text);
        request.AddQueryParameter("UpdatePrice_per_pcs", Price.Text);

        var response = client.Execute(request);

        MessageBox.Show("Data Berhasil Diubah\nSilahkan Refresh Table");
        this.Close();
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

    public class Edit
    {
        public string? id { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; }
        public string? Date { get; set; }
        public string? Amount { get; set; }
        public string? Price { get; set; }
    }


}