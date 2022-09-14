using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicPlayer
{
    public partial class TeacherPage : ContentPage
    {
        IOrientationHandler orientationHandler = DependencyService.Get<IOrientationHandler>();
        public TeacherPage()
        {   
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            
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

        private async void buttonTeacherLessonCreate_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Внимание", "Навык „Создать упражнение“ находится в разработке, он появится в следующих обновлениях", "Хорошо");

        }


        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}