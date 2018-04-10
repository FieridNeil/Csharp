using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;


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
        bool isPlaying = false;
        bool isLooping = false;
        private static int songIndex = 0;
        private static bool execute = true;
   
        public MainWindow()
        {
            InitializeComponent();

            nowPlayinglabel.Content = "No file is playing";

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
            songIndex++;
            execute = true;
            isPlaying = false;
            playSong();
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            songIndex--;
            execute = true;
            isPlaying = false;
            playSong();
        }

        private void btnLoop_Click(object sender, RoutedEventArgs e)
        {

        }

        private void playSong()
        {
            if (execute)
            {
                if (queue.Count > 0)
                {
                    Uri song = queue[songIndex];
                    nowPlayinglabel.Content = song.ToString();
                    mediaPlayer.Open(song);
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
            }
            else
            {
                mediaPlayer.Pause();
                btnPlay.Content = "Play";
                isPlaying = false;
            }
        }
    }
}
