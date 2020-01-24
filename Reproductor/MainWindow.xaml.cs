using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Win32;

using NAudio.Wave;
using NAudio.Wave.SampleProviders;


using System.Windows.Threading;



namespace Reproductor
{

    

    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer timer;

        AudioFileReader reader; //leer el archivo
        WaveOut output; // reproducir el archivo, exclusivo de salida

        bool dragging = false;

        public MainWindow()
        {
            InitializeComponent();
            

            btnReproducir.IsEnabled = true; //deshabilitar botines
            btnDetener.IsEnabled = false;
            btnPausa.IsEnabled = false;

            //Poner el intervalo al timer,  cada 500 milisegundos se va a ejecutar la función Tick
            timer = new DispatcherTimer();

          
                timer.Interval = TimeSpan.FromMilliseconds(500);
            
           

            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            lblTiempoActual.Text = reader.CurrentTime.ToString().Substring(0, 8);

            if (!dragging) { 
            sldTiempo.Value = reader.CurrentTime.TotalSeconds;
            }

            pbCancion.Value = reader.CurrentTime.TotalSeconds;

            if (pbCancion.Value >= 6)
            {
                txtLyrics.Text = "With Geralt of Rivia along came this song";
            }

            if (pbCancion.Value >= 15)
            {
                txtLyrics.Text = "From when the White Wolf fought  a silver-tongued devil";
            }

            if (pbCancion.Value >= 20)
            {
                txtLyrics.Text = "His army of elves at his hooves did they revel";
            }

            if (pbCancion.Value >= 29)
            {
                txtLyrics.Text = "They came after me with masterful deceit";
            }

            if (pbCancion.Value >= 34)
            {
                txtLyrics.Text = "Broke down my lute and they kicked in my teeth";
            }
        }



     

        private void BtnReproducir_Click(object sender, RoutedEventArgs e)
        {

            if (output != null && output.PlaybackState == PlaybackState.Paused)
            {
                //retomo la reproducción
                output.Play();
                btnReproducir.IsEnabled = false;
                btnPausa.IsEnabled = true;
                btnDetener.IsEnabled = false;
            }
            else
            {
                    string testfile = @"TossACoin.mp3";
                    reader = new AudioFileReader(testfile);

                    output = new WaveOut();
                   

                    output.PlaybackStopped += Output_PlaybackStopped;

                    output.Init(reader); //inicializar el output (reproduccion), necesita un wave provider en este caso "reader"
                    output.Play(); // Reproducir el archivo

                    btnReproducir.IsEnabled = false; //habilitar botones
                    btnDetener.IsEnabled = true;
                    btnPausa.IsEnabled = true;

                    lblTiempoTotal.Text = reader.TotalTime.ToString().Substring(0, 8);
                    lblTiempoActual.Text = reader.CurrentTime.ToString().Substring(0, 8);

                    sldTiempo.Maximum = reader.TotalTime.TotalSeconds;

                     pbCancion.Maximum = reader.TotalTime.TotalSeconds;

                    //iniciar el timer
                    timer.Start();

                    txtLyrics.Text = "When a humble bard graced a ride along";

                   
                    
            }

            
                
        }

        private void Output_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            timer.Stop();
            reader.Dispose();
            output.Dispose();
            
        }

        private void BtnDetener_Click(object sender, RoutedEventArgs e)
        {
            if(output != null)
            {
                output.Stop();
                btnReproducir.IsEnabled = true;
                btnPausa.IsEnabled = false;
                btnDetener.IsEnabled = false;
            }
        }

        private void BtnPausa_Click(object sender, RoutedEventArgs e)
        {
            if (output != null)
            {
                output.Pause();
                btnReproducir.IsEnabled = true;
                btnPausa.IsEnabled = false;
                btnDetener.IsEnabled = true;
            }
        }

        private void sldTiempo_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            dragging = true;
        }

        private void sldTiempo_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            dragging = false;
            if (reader != null && output != null && output.PlaybackState != PlaybackState.Stopped)
            {
                reader.CurrentTime = TimeSpan.FromSeconds(sldTiempo.Value);
            }
        }
    }
}
