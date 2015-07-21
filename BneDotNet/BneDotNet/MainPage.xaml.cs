using Newtonsoft.Json;
using OpenWeatherMap;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SparkFun;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BneDotNet
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            display();
        }
        private SparkFunWeatherSheild sheild { get; set; }
        private async void display()
        {
            sheild = new SparkFunWeatherSheild();
             
            if(await sheild.Setup()) {
                Temperature.Text = sheild.Temperature.ToString();
                Humidity.Text = sheild.Humidity.ToString();
                PostToAzure();
            }
        }
        private  async void PostToAzure()
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("DeviceID", Guid.NewGuid().ToString()),
                new KeyValuePair<string,string>("Humidity", sheild.Humidity.ToString()),
                new KeyValuePair<string,string>("Temperature", sheild.Temperature.ToString())
            });

            var client = new HttpClient();
            var response = await client.PostAsync("http://YourWebAPi.azurewebsites.net/api/weather", formContent);
        }
        public async Task<OpenWeatherMapData> GetData()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://api.openweathermap.org/data/2.5/weather?q=brisbane&units=metric");
            return JsonConvert.DeserializeObject<OpenWeatherMapData>(await response.Content.ReadAsStringAsync());
        }
        private void textBlock1_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
