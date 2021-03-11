using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CoolApp.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace CoolApp.ViewModels
{
    public class MainPageViewModel
    {
        public WeatherData Data { get; set; }

        public ICommand SearchCommand { get; set; }

        public MainPageViewModel()
        {
            SearchCommand = new Command(async (searchTerm)=>
                {
                    await GetData("https://api.weatherbit.io/v2.0/current?lat=35.7796&lon=-78.6382&key=baac9df06c38429b89305960297a62ac&include=minutely");
                });
        }

        public async Task GetData(string url)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            var response =
                await client.GetAsync(client.BaseAddress);
            response.EnsureSuccessStatusCode();
            var jsonresult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<WeatherData>(jsonresult);
            Data = result;
        }
    }
}
