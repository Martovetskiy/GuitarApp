using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicPlayer
{
    public partial class StudentPage : ContentPage
    {
        IOrientationHandler orientationHandler = DependencyService.Get<IOrientationHandler>();
        public StudentPage()
        {
            InitializeComponent();
           
            NavigationPage.SetHasNavigationBar(this, false);
        }


        

       
        private async void buttonStudentLesson_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Lesson1());

        }

        private async void buttonStudentSong_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PaintItBlackMusicPage());
        }

        

        private async void Button1_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}