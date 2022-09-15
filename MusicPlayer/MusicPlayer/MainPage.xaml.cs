using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static MusicPlayer.LogOutPage;
namespace MusicPlayer
{
    public partial class MainPage : ContentPage
    {
        IOrientationHandler orientationHandler = DependencyService.Get<IOrientationHandler>();
        public MainPage()
        {   
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
            if (App.CurrentBluetoothConnection != null)
            {
                buttonStudent.IsEnabled = true;
                buttonTeacher.IsEnabled = true;
            }
        }

        private double width = 0;
        private double height = 0;

        protected override void OnSizeAllocated(double width, double height)
        {
            orientationHandler.ForcePortrait();
            base.OnSizeAllocated(width, height); //must be called
            if (this.width != width || this.height != height)
            {
                this.width = width;
                this.height = height;
            }


        }
        
       

        private async void buttonStudent_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StudentPage());
        }

        private async void buttonTeacher_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TeacherPage());
        }
        private async void buttonTeacherBluetooth_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SelectBlDevice());
        }

        private async void LogOUTbut_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LogOutPage());
        }
    }
}
