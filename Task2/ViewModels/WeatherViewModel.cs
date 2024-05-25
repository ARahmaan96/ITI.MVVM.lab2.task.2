using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Task2.Models;
using Task2.Services;
using Task2.Command;

namespace Task2.ViewModels
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private readonly WeatherService _weatherService;
        private WeatherModel _currentWeather;
        private string _cityName;

        public WeatherViewModel()
        {
            _weatherService = new WeatherService();
            FetchWeatherCommand = new RelayCommand(async () => await FetchWeatherAsync());
        }

        public WeatherModel CurrentWeather
        {
            get => _currentWeather;
            set
            {
                _currentWeather = value;
                OnPropertyChanged();
            }
        }

        public string CityName
        {
            get => _cityName;
            set
            {
                _cityName = value;
                OnPropertyChanged();
            }
        }

        public ICommand FetchWeatherCommand { get; }

        private async Task FetchWeatherAsync()
        {
            CurrentWeather = await _weatherService.GetWeatherAsync(CityName);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
