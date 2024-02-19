using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Media.Playback;
using Windows.Media.Core;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace _212_TDBNP_3P_EX_OILM
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MediaPlayer player;
        public MainPage()
        {
            this.InitializeComponent();
            player = new MediaPlayer();

            
        }

        private void btnInicio_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Juego));
            player.Pause();
        }

        private async void gridInicio_Loaded(object sender, RoutedEventArgs e)
        {
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            Windows.Storage.StorageFile file = await folder.GetFileAsync("Hollow-Knight-OST-Enter-Hallownest.mp3");

            player.AutoPlay = false;
            player.Source = MediaSource.CreateFromStorageFile(file);

            player.Play();
        }
    }
}
