using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Newtonsoft.Json.Linq;
using RestSharp;
namespace Apparel_Factory;

public partial class AddWindow : Window
{
    public AddWindow()
    {
        InitializeComponent();
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
        var request = new RestRequest("Apparel", RestSharp.Method.Post);

        request.AddQueryParameter("AddName", AddName.Text);
        request.AddQueryParameter("AddCategory", AddCategory.Text);
        request.AddQueryParameter("AddProduction_date", AddDate.Text);
        request.AddQueryParameter("AddProdcution_amount", AddAmount.Text);
        request.AddQueryParameter("AddPrice_per_pcs", AddPrice.Text);

        var response = client.Execute(request);

        MessageBox.Show("Data Berhasil Ditambahkan\nSilahkan Refresh Table");
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
}