using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Sockets;

namespace XamarinFormsClient
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Connect_Clicked(object sender, EventArgs e)
        {
            try
            {
                TcpClient client = new TcpClient();
                await client.ConnectAsync(IPAddress.Text, Convert.ToInt32(Port.Text));
                if (client.Connected)
                {
                    Connection.Instance.client = client;
                    //Application.Current.MainPage = new NavigationPage(new OperationsPage());
                    await DisplayAlert("Connected", "Conntected to server successfully!", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "Connection unsuccessful!", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "" + ex.ToString(), "Ok");
            }
        }
    }
}
