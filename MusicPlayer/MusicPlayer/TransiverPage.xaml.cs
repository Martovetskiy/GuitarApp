using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Plugin.BluetoothClassic.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicPlayer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransiverPage : ContentPage
    {
        public TransiverPage()
        {
            InitializeComponent();
        }

        private async void btnTransmit_Clicked(object sender, EventArgs e)
        {
            const int BufferSize = 1;
            const int OffsetDefault = 0;
            var device = (BluetoothDeviceModel)BindingContext;
            if (device != null)
            {
                var adapter = DependencyService.Resolve<IBluetoothAdapter>();
                using (var connection = adapter.CreateConnection(device))
                {
                    if (await connection.RetryConnectAsync(retriesCount: 2))
                    {
                        byte[] buffer = new byte[BufferSize] { (byte)stepperDigit.Value };
                        if (!await connection.RetryTransmitAsync(buffer, OffsetDefault, buffer.Length))
                        {
                            await DisplayAlert("Ошибка", "Данные не отправленны", "Закрыть");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Ошибка", "Не удалось подключиться к гитаре", "Закрыть");
                    }
                }
            }
        }

        private async void btnRecive_Clicked(object sender, EventArgs e)
        {
            const int BufferSize = 1;
            const int OffsetDefault = 0;
            var device = (BluetoothDeviceModel)BindingContext;
            if (device != null)
            {
                var adapter = DependencyService.Resolve<IBluetoothAdapter>();
                using (var connection = adapter.CreateConnection(device))
                {
                    if (await connection.RetryConnectAsync(retriesCount: 2))
                    {
                        byte[] buffer = new byte[BufferSize];
                        if (!(await connection.RetryReciveAsync(buffer, OffsetDefault, buffer.Length)).Succeeded)
                        {
                            await DisplayAlert("Ошибка", "Данные не отправленны", "Закрыть");
                        }
                        else
                        {
                            stepperDigit.Value = buffer.FirstOrDefault();
                        }
                    }
                    else
                    {
                        await DisplayAlert("Ошибка", "Не удалось подключиться к гитаре", "Закрыть");
                    }
                }
            }
        }
    }
}