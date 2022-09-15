using Firebase.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicPlayer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Device.StartTimer(TimeSpan.FromSeconds(0.1), CheckStateText);

        }
        public string WebAPIkey = "AIzaSyAr-xNyjwE8I-9AQRST_3jFzxircWYal5o";
        bool CheckStateText()
        {
            if ((UserNewEmail.Text != null) && (UserNewPassword.Text != null))
            {
                signupbutton.IsEnabled = true;
                return true;
            }
            else
            {
                return true;
            }
            
            
        }
        async void signupbutton_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(UserNewEmail.Text, UserNewPassword.Text);
                var auth1 = await authProvider.SignInWithEmailAndPasswordAsync(UserNewEmail.Text, UserNewPassword.Text);
                var content = await auth1.GetFreshAuthAsync();
                var serializedcontnet = JsonConvert.SerializeObject(content);
                Preferences.Set("MyFirebaseRefreshToken", serializedcontnet);
                await Navigation.PushAsync(new MainPage());

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Внимание", "Возможно, вы используете зарегистрированный Email", "OK");
            }

        }

        private async void logbutton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}