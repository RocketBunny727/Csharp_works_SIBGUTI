using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using System.Linq;

namespace Homework_6
{
    public partial class MainWindow : Window
    {
        private const string ApiKey = "3729556e1ffcd0ac06fc094c71261da1";
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5/forecast";
        private const string CityUrl = "https://api.openweathermap.org/geo/1.0/direct";

        private TextBox CityT;
        private TextBlock WeatherToday;
        private TextBlock WeatherOther;
        private TextBlock Today;
        private TextBlock Main;
        // private Image WeatherIcon; // Оставляем только одно определение
        // private Image OtherWeatherIcon; // Оставляем только одно определение

        public MainWindow()
        {
            InitializeComponent();
            GetWeatherForecast(CityT.Text);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            CityT = this.FindControl<TextBox>("CityTextBox");
            WeatherToday = this.FindControl<TextBlock>("TodayWeatherTextBlock");
            WeatherOther = this.FindControl<TextBlock>("OtherDaysWeatherTextBlock");
            Today = this.FindControl<TextBlock>("TodayTextBlock");
            Main = this.FindControl<TextBlock>("MainTextBlock");
            WeatherIcon = this.FindControl<Image>("WeatherIcon");
            OtherWeatherIcon = this.FindControl<Image>("OtherWeatherIcon");
        }

        private async void UpdateButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var city = CityT.Text;
            await GetWeatherForecast(city);
        }

        private async Task GetWeatherForecast(string city)
        {
            var cityCoordinates = await GetCityCoordinates(city);
            var url = $"{BaseUrl}?lat={cityCoordinates.lat}&lon={cityCoordinates.lon}&appid={ApiKey}&units=metric";

            using var client = new HttpClient();
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var weatherForecast = JsonSerializer.Deserialize<WeatherForecastResponse>(content);
                Console.WriteLine(content);

                var groupedForecast = GroupForecastByDay(weatherForecast.list);
                DisplayForecast(groupedForecast);
            }
            else
            {
                WeatherToday.Text = "Failed to retrieve weather forecast.";
                WeatherOther.Text = "";
            }
        }

        private Dictionary<string, List<WeatherForecastItem>> GroupForecastByDay(List<WeatherForecastItem> forecastItems)
        {
            var groupedForecast = new Dictionary<string, List<WeatherForecastItem>>();
            foreach (var forecastItem in forecastItems)
            {
                var date = DateTimeOffset.FromUnixTimeSeconds(forecastItem.dt).DateTime.Date.ToString("yyyy-MM-dd");
                if (!groupedForecast.ContainsKey(date))
                {
                    groupedForecast[date] = new List<WeatherForecastItem>();
                }
                groupedForecast[date].Add(forecastItem);
            }
            return groupedForecast;
        }

        private void DisplayForecast(Dictionary<string, List<WeatherForecastItem>> groupedForecast)
        {
            WeatherToday.Text = "";
            WeatherOther.Text = "";
            Today.Text = "";

            var todayDate = DateTime.Today.ToString("yyyy-MM-dd");
            var today = DateTime.Today;
            Today.Text += $"{today.DayOfWeek}\n";

            foreach (var dateForecast in groupedForecast)
            {
                if (dateForecast.Key == todayDate)
                {
                    var weatherLine = "";
                    var timeLine = "";
                    var count = 0;
                    foreach (var forecastItem in dateForecast.Value)
                    {
                        var time = DateTimeOffset.FromUnixTimeSeconds(forecastItem.dt).DateTime.ToString("HH:mm");
                        var temperature = $"{Math.Round(forecastItem.main.temp)}°C";
                        Main.Text = $"{Math.Round(forecastItem.main.temp)}°C";

                        if (count < 6)
                        {
                            if (count == 0)
                            {
                                time = "Now";
                            }
                            weatherLine += $"{temperature,5}\t";
                            timeLine += $"{time,1}\t";
                            count++;
                        }
                        SetWeatherIcon(forecastItem.weather[0].icon);
                    }
                    WeatherToday.Text += $"{timeLine}\n{weatherLine}\n";
                    SetWeatherIconToday(dateForecast.Value.First().weather[0].icon);
                }
                else
                {
                    var forecastItem = dateForecast.Value.FirstOrDefault();
                    if (forecastItem != null)
                    {
                        var forecastDate = DateTime.Parse(dateForecast.Key);
                        var dayOfWeek = forecastDate.DayOfWeek.ToString();
                        var temperature = $"{Math.Round(forecastItem.main.temp)}°C";

                        dayOfWeek += $"{temperature,5}\t";
                        WeatherOther.Text += $"{dayOfWeek} {forecastDate.ToString("dd MMMM ")}:{Math.Round(forecastItem.main.temp)}°C\n\n\n";
                        SetWeatherIconOther(forecastItem.weather[0].icon);
                    }
                }
            }
        }

        private void SetWeatherIcon(string iconCode)
        {
            string iconFolderPath = "~/../../../../Assets/";
            string iconPath = $"{iconFolderPath}{iconCode}.png";

            if (WeatherIcon != null)
            {
                WeatherIcon.Source = new Bitmap(iconPath);
            }
            else
            {
                Console.WriteLine("WeatherIcon is not initialized.");
            }
        }

        private void SetWeatherIconToday(string iconCode)
        {
            string iconFolderPath = "~/../../../../Assets/";
            string iconPath = $"{iconFolderPath}{iconCode}.png";

            if (WeatherIcon != null)
            {
                WeatherIcon.Source = new Bitmap(iconPath);
            }
            else
            {
                Console.WriteLine("WeatherIcon is not initialized.");
            }
        }

        private void SetWeatherIconOther(string iconCode)
        {
            string iconFolderPath = "~/../../../../Assets/";
            string iconPath = $"{iconFolderPath}{iconCode}.png";

            if (OtherWeatherIcon != null) // Используем OtherWeatherIcon вместо WeatherIcon
            {
                OtherWeatherIcon.Source = new Bitmap(iconPath);
            }
            else
            {
                Console.WriteLine("OtherWeatherIcon is not initialized.");
            }
        }

        private async Task<CityCoordinates> GetCityCoordinates(string city)
        {
            var url = $"{CityUrl}?q={city}&appid={ApiKey}";
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var cities = JsonSerializer.Deserialize<List<CityCoordinates>>(content);
                return cities[0];
            }
            throw new Exception("Failed to retrieve city coordinates");
        }
    }

    public class CityCoordinates
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }

    public class WeatherForecastResponse
    {
        public string cod { get; set; }
        public double message { get; set; }
        public int cnt { get; set; }
        public List<WeatherForecastItem> list { get; set; }
    }

    public class WeatherForecastItem
    {
        public long dt { get; set; }
        public MainInfo main { get; set; }
        public List<WeatherInfo> weather { get; set; }
    }

    public class MainInfo
    {
        public double temp { get; set; }
        public double feels_like { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
    }

    public class WeatherInfo
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }
}
