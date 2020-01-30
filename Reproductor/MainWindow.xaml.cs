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

            if (pbCancion.Value >= 38)
            {
                txtLyrics.Text = "While the devils horns minced our tender meat";
            }

            if (pbCancion.Value >= 43)
            {
                txtLyrics.Text = "And so cried the Witcher he can't be bleat ";
            }

            if (pbCancion.Value >= 49)
            {
                txtLyrics.Text = "Toss a coin to your Witcher o' Valley of Plenty";
            }

            if (pbCancion.Value >= 54)
            {
                txtLyrics.Text = "O' Valley of Plenty";
            }

            if (pbCancion.Value >= 57)
            {
                txtLyrics.Text = "Ooooh";
            }

            if (pbCancion.Value >= 59)
            {
                txtLyrics.Text = "Toss a coin to your Witcher o' Valley of Plenty";
            }

            if (pbCancion.Value >= 71)
            {
                txtLyrics.Text = "At the edge of the world fight the mighty horde  ";
            }

            if (pbCancion.Value >= 76)
            {
                txtLyrics.Text = "That bashes and breaks you and brings you the morn' ";
            }


            if (pbCancion.Value >= 85)
            {
                txtLyrics.Text = "He thrust every elf far back on the shelf";
            }

            if (pbCancion.Value >= 89)
            {
                txtLyrics.Text = "High up on the mountain from whence it came";
            }

            if (pbCancion.Value >= 98)
            {
                txtLyrics.Text = "He wipped out your pest got kicked in his chest";
            }

            if (pbCancion.Value >= 103)
            {
                txtLyrics.Text = "He's a friend of humanity so give him the rest";
            }

            if (pbCancion.Value >= 108)
            {
                txtLyrics.Text = "That's my epic tale a champion prevailed";
            }

            if (pbCancion.Value >= 113)
            {
                txtLyrics.Text = "Defeated the villain now pour him some ale";
            }

            if (pbCancion.Value >= 118)
            {
                txtLyrics.Text = "Toss a coin to your Witcher o' Valley of Plenty";
            }

            if (pbCancion.Value >= 123)
            {
                txtLyrics.Text = "O' Valey of Plenty";
            }

            if (pbCancion.Value >= 126)
            {
                txtLyrics.Text = "ohhh";
            }

            if (pbCancion.Value >= 127)
            {
                txtLyrics.Text = "Toss a coin to your Witcher a friend of humanity";
            }

            if (pbCancion.Value >= 137)
            {
                txtLyrics.Text = "Toss a coin to your Witcher O' Valley of Plenty";
            }

            if (pbCancion.Value >= 142)
            {
                txtLyrics.Text = "O' Valey of Plenty";
            }

            if (pbCancion.Value >= 144)
            {
                txtLyrics.Text = "Ohhh";
            }

            if (pbCancion.Value >= 146)
            {
                txtLyrics.Text = "Toss a coin to your Witcher a friend of humanity";
            }

            if (pbCancion.Value >= 155)
            {
                txtLyrics.Text = "Toss a coin to your Witcher O' Valley of Plenty";
            }

            if (pbCancion.Value >= 160)
            {
                txtLyrics.Text = "O' Valey of Plenty";
            }

            if (pbCancion.Value >= 162)
            {
                txtLyrics.Text = "Ohhh";
            }

            if (pbCancion.Value >= 164)
            {
                txtLyrics.Text = "Toss a coin to your Witcher  a friend of humanity";
            }

            if (pbCancion.Value >= 175)
            {
                txtLyrics.Text = "";
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
