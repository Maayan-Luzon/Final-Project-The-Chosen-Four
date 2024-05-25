using System;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace GameEngine.GameServices
{
    public static class MusicPlayer
    {
        public static MediaPlayer _mediaPlayer = new MediaPlayer(); // the media player
        public static bool IsOn { get; set; } = false; // is the music playing? 
        private static double _volume = 0.5; // the init value

        public static double Volume // the volume of the music
        {
            set 
            { 
                _volume = value / 100; 
                _mediaPlayer.Volume = _volume;
            }
            get
            {
                return _volume * 100;
            }
        }

        /// <summary>
        /// the methode start to play the fileName's music
        /// </summary>
        /// <param name="fileName"> the name of the file</param>
        public static void Play(string fileName)
        {
            if (!IsOn)
            {
                _mediaPlayer.Source = MediaSource.CreateFromUri(new Uri($"ms-appx:///Assets/Music/{fileName}"));
                _mediaPlayer.IsLoopingEnabled = true;
                _mediaPlayer.Play();
                IsOn = true;
            }
        }

        /// <summary>
        /// the methode pause the music
        /// </summary>
        public static void Pause()
        {
            _mediaPlayer.Pause();
            IsOn = false;
        }

        /// <summary>
        /// the methode resume the music
        /// </summary>
        public static void Resume()
        {
            _mediaPlayer.Play();
            IsOn = true;
        }
    }  

}
