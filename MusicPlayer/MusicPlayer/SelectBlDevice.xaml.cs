using Plugin.BluetoothClassic.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicPlayer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectBlDevice : ContentPage
    {
        public SelectBlDevice()
        {
            InitializeComponent();
            FillBondedDevices();
        }

        private void FillBondedDevices()
        {
            var adapter = DependencyService.Resolve<IBluetoothAdapter>();
            DeviceList.ItemsSource = adapter.BondedDevices;
        }

        private async void DeviceList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var devices = (BluetoothDeviceModel)e.SelectedItem;
            if (devices != null)
            {
                await Navigation.PushAsync(new TransiverPage { BindingContext = devices });
            }
            DeviceList.SelectedItem = null;
        }
    }
}