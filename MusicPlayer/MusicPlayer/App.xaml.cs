using Plugin.BluetoothClassic.Abstractions;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicPlayer
{
    public partial class App : Application
    {
        public static IBluetoothManagedConnection CurrentBluetoothConnection { get; internal set; }
        public App()
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(Preferences.Get("MyFirebaseRefreshToken", "")))
            {
                MainPage = new NavigationPage(new MainPage())
                {
                    BarBackgroundColor = Color.FromHex("#F9F9FF")
                };
            }
            else
            {
                MainPage = new NavigationPage(new SignUpPage())
                {
                    BarBackgroundColor = Color.FromHex("#F9F9FF")
                };
            }
            
        }


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
