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

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace _212_TDBNP_3P_PROYECTO
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            RepeatBehavior rb = new RepeatBehavior();
            rb.Type = RepeatBehaviorType.Forever;

            sbParado.RepeatBehavior = rb;
            this.sbParado.Begin();

        }

        private void btnAtacar_Click(object sender, RoutedEventArgs e)
        {
            this.sbParado.Stop();
            this.sbAtacar.Begin();
        }

        private void btnAtaqueFinal_Click(object sender, RoutedEventArgs e)
        {
            this.sbParado.Stop();
            this.sbAtacar.Stop();
            this.sbAtaqueFinal.Begin();
        }

        private void btnReiniciar_Click(object sender, RoutedEventArgs e)
        {
            this.sbAtaqueFinal.Stop();
            this.sbParado.Begin();
        }
    }
}
