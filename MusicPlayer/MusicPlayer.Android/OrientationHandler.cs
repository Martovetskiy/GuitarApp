using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MusicPlayer.Droid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
[assembly: Xamarin.Forms.Dependency(typeof(OrientationHandler))]
namespace MusicPlayer.Droid
{
    public class OrientationHandler : IOrientationHandler
    {
        public void ForceLandscape()
        {
            ((Activity)Forms.Context).RequestedOrientation = ScreenOrientation.Landscape;
        }

        public void ForcePortrait()
        {
            ((Activity)Forms.Context).RequestedOrientation = ScreenOrientation.Portrait;
        }
    }
}