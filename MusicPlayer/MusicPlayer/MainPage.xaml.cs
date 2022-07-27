using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MusicPlayer
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        async void Lesson1_clk(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Lesson1());
        }

        private async void Bluetooth_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SelectBlDevice());
        }
    }
}
