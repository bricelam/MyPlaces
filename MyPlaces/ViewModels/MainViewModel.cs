using GalaSoft.MvvmLight;
using Microsoft.Extensions.Configuration;

namespace MyPlaces.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        readonly IConfiguration _configuraiton;

        public MainViewModel(IConfiguration configuraiton)
        {
            _configuraiton = configuraiton;
        }

        public string BingMapsKey
            => _configuraiton["BingMapsKey"];
    }
}
