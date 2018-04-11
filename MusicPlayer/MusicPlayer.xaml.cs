using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;
using System.Windows.Threading;
namespace MusicPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();
        private VideoDrawing vd = new VideoDrawing();
        //private Queue<Uri> queue = new Queue<Uri>();
        private List<Uri> queue = new List<Uri>();
        static bool isPlaying = false;
        static bool isLooping = false;
        private static int songIndex = 0;
        private static bool execute = true;
        DispatcherTimer timeline = new DispatcherTimer();
        private static TimeSpan duration;
        private static TimeSpan length;
        
        public MainWindow()
        {
            InitializeComponent();

            timeline.Tick += Timeline_Tick;
            timeline.Interval = new TimeSpan(0, 0, 1);
            mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
        }

        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            if(isLooping == true)
            {
                mediaPlayer.Position = new TimeSpan(0);
            }else
            {
                btnPlay.Content = "Play";
                nextSong();
            }
        }

        private void Timeline_Tick(object sender, EventArgs e)
        {
            duration = mediaPlayer.Position;
            //HasTimeSpan will turn to true about a second after the mp3 is played
            if (mediaPlayer.NaturalDuration.HasTimeSpan == true)
            {
                length = mediaPlayer.NaturalDuration.TimeSpan;
            }

        }

        private void btnOpenAudioFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            
            if (openFileDialog.ShowDialog() == true)
            {
                
                for (int i = 0; i < openFileDialog.FileNames.Length; i++)
                {
                    queue.Add(new Uri(openFileDialog.FileNames[i]));
                    Console.WriteLine(queue[i].ToString());
                }
                
            }
           
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            playSong();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            nextSong();
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            if(duration > new TimeSpan(0, 0, 5))
            {
                mediaPlayer.Position = new TimeSpan(0);
            }else
            {
                prevSong();

            }
        }



        private void btnLoop_Click(object sender, RoutedEventArgs e)
        {
            isLooping = !isLooping;
            Console.WriteLine("Loop: " + isLooping);
        }

        private void nextSong()
        {
            // Check index upperbound
            if ((songIndex + 1) < queue.Count)
            {
                songIndex++;
                execute = true;
                isPlaying = false;
                playSong();
            }
        }

        private void prevSong()
        {
            // Check index lowerbound
            if ((songIndex - 1) > -1)
            {
                songIndex--;
                execute = true;
                isPlaying = false;
                playSong();
            }
        }
        private void playSong()
        {
            if (execute)
            {
                if (queue.Count > 0)
                {
                    Uri song = queue[songIndex];
                    mediaPlayer.Open(song);
                    timeline.Start();
                    execute = false;
                }else
                {
                    Console.WriteLine("There is no song to play");
                    
                }
            }

            if (isPlaying == false)
            {
                mediaPlayer.Play();
                btnPlay.Content = "Pause";
                isPlaying = true;
                timeline.Start();
            }
            else
            {
                mediaPlayer.Pause();
                btnPlay.Content = "Play";
                isPlaying = false;
                timeline.Stop();
            }
        }
    }
}
