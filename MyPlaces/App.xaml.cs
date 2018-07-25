using System.Windows;
using GalaSoft.MvvmLight.Threading;

namespace MyPlaces
{
    public partial class App : Application
    {
        static App()
        {
            DispatcherHelper.Initialize();
        }
    }
}
