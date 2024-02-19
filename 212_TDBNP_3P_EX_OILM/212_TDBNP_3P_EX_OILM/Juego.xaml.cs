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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.Media.Playback;
using Windows.Media.Core;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace _212_TDBNP_3P_EX_OILM
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Juego : Page
    {
        RepeatBehavior rb;
        Vida v;
        DispatcherTimer timer;
        MediaPlayer player;
        public Juego()
        {
            this.InitializeComponent();
            player = new MediaPlayer();

            rb = new RepeatBehavior(1);
            sbInicio.RepeatBehavior = rb;
            this.sbInicio.Begin();

            v = new Vida();

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 10);
            timer.Tick += (a, b) =>
            {
                Inicio5.Visibility = Visibility.Visible;
                sbAtacar.Stop();
                sbCurarse.Stop();
                sbMagia.Stop();
                btnAtacar.IsEnabled = false;
                cmbAcciones.IsEnabled = false;
                sbFlecha.RepeatBehavior = rb;
                sbFlecha.Begin();
                v.valorVK -= 30;
                sldrKnight.Value = v.valorVK;
                txtVidaK.Text = sldrKnight.Value.ToString();
                btnAtacar.IsEnabled = true;
                cmbAcciones.IsEnabled = true;

                juegoTerminadoKnight();
            };
            timer.Start();

            txtVidaK.Visibility = Visibility.Visible;
            txtVidaR.Visibility = Visibility.Visible;

            sldrKnight.IsEnabled = false;
            sldrRadience.IsEnabled = false;
            
        }

        private void btnAtacar_Click(object sender, RoutedEventArgs e)
        {
            Inicio5.Visibility = Visibility.Collapsed;
            txtAnuncio.Visibility = Visibility.Collapsed;
            sldrKnight.Visibility = Visibility.Visible;
            sldrRadience.Visibility = Visibility.Visible;
            btnAtacar.Visibility = Visibility.Visible;
            cmbAcciones.Visibility = Visibility.Visible;
            swtImposible.Visibility = Visibility.Visible;
            sbInicio.Stop();
            sbMagia.Stop();
            sbCurarse.Stop();

            txtVidaK.Text = sldrKnight.Value.ToString();
            sbAtacar.RepeatBehavior = rb;
            sbAtacar.Begin();

            v.valorVR -= 15;
            sldrRadience.Value = v.valorVR;
            txtVidaR.Text = sldrRadience.Value.ToString();

            JuegoTerminadoRadience();
        }

        private void cmbAcciones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sldrKnight.Visibility = Visibility.Visible;
            sldrRadience.Visibility = Visibility.Visible;
            btnAtacar.Visibility = Visibility.Visible;
            cmbAcciones.Visibility = Visibility.Visible;
            swtImposible.Visibility = Visibility.Visible;

            if (cmbAcciones.SelectedIndex == 0)
            {
                if(sldrKnight.Value != 100)
                {
                    Inicio5.Visibility = Visibility.Collapsed;
                    sbMagia.Stop();
                    sbAtacar.Stop();

                    sbCurarse.RepeatBehavior = rb;
                    sbCurarse.Begin();
                    v.valorVK += 15;
                    sldrKnight.Value = v.valorVK;
                }
            }
            else
            {
                Inicio5.Visibility = Visibility.Collapsed;
                sbInicio.Stop();
                sbAtacar.Stop();
                sbCurarse.Stop();

                sbMagia.RepeatBehavior = rb;
                sbMagia.Begin();
                v.valorVR -= 25;
                sldrRadience.Value = v.valorVR;
                txtVidaR.Text = sldrRadience.Value.ToString();

                JuegoTerminadoRadience();
            }
        }

        private void swtImposible_Toggled(object sender, RoutedEventArgs e)
        {
            if (swtImposible.IsOn)
            {
                sldrRadience.Maximum = 1000;
                v.valorVR = 1000;
                sldrRadience.Value = v.valorVR;
                txtVidaR.Text = sldrRadience.Value.ToString();
            }
            else
            {
                sldrRadience.Maximum = 100;
                v.valorVR = 100;
                sldrRadience.Value = v.valorVR;
            }

            swtImposible.IsEnabled = false;
        }

        public void JuegoTerminadoRadience()
        {
            if (sldrRadience.Value == 0)
            {
                Radiance.Visibility = Visibility.Collapsed;
                txtAnuncio.Visibility = Visibility.Visible;
                txtAnuncio.Text = "Ganaste!!!";
                btnAtacar.IsEnabled = false;
                cmbAcciones.IsEnabled = false;
                swtImposible.IsEnabled = false;
                timer.Stop();
                sbAtacar.Stop();
                sbCurarse.Stop();
                sbFlecha.Stop();
                sbInicio.Stop();
                sbMagia.Stop();
                Inicio5.Visibility = Visibility.Visible;
                txtTimer.Visibility = Visibility.Collapsed;
                btnReiniciar.Visibility = Visibility.Visible;
                player.Pause();
            }
        }

        private void btnReiniciar_Click(object sender, RoutedEventArgs e)
        {
            Radiance.Visibility = Visibility.Visible;
            btnAtacar.IsEnabled = true;
            cmbAcciones.IsEnabled = true;
            swtImposible.IsEnabled = true;
            txtAnuncio.Visibility = Visibility.Collapsed;
            Inicio5.Visibility = Visibility.Collapsed;

            txtAnuncio.Text = "FIGHT!!!";

            rb = new RepeatBehavior(1);
            sbInicio.RepeatBehavior = rb;
            this.sbInicio.Begin();

            v = new Vida();

            timer.Start();

            txtVidaK.Visibility = Visibility.Visible;
            txtVidaR.Visibility = Visibility.Visible;

            sldrKnight.IsEnabled = false;
            sldrRadience.IsEnabled = false;

            sldrRadience.Maximum = 100;
            sldrRadience.Value = 100;

            btnReiniciar.Visibility = Visibility.Collapsed;
            player.Play();
        }

        public void juegoTerminadoKnight()
        {
            if (sldrKnight.Value == 0)
            {
                Inicio5.Visibility = Visibility.Collapsed;
                txtAnuncio.Visibility = Visibility.Visible;
                txtAnuncio.Text = "Perdiste!!!";
                btnAtacar.IsEnabled = false;
                cmbAcciones.IsEnabled = false;
                swtImposible.IsEnabled = false;
                timer.Stop();
                sbAtacar.Stop();
                sbCurarse.Stop();
                sbFlecha.Stop();
                sbInicio.Stop();
                sbMagia.Stop();
                txtTimer.Visibility = Visibility.Collapsed;
                btnReiniciar.Visibility = Visibility.Visible;
                player.Pause();
            }
        }

        private async void gridJuego_Loaded(object sender, RoutedEventArgs e)
        {
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            Windows.Storage.StorageFile file = await folder.GetFileAsync("videoplayback.mp3");

            player.AutoPlay = false;
            player.Source = MediaSource.CreateFromStorageFile(file);

            player.Play();
        }
    }
}
