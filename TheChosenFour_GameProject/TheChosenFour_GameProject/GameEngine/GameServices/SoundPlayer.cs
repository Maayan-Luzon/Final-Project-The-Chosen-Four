using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace GameEngine.GameServices
{
    // the 'static' means that in order to use this class's methods, we doesn't have to create an instance of this class.
    // moreover, this allowd enable us to use this class from all the diffrent projects. 
    public static class SoundPlayer 
    {
        private static MediaPlayer _mediaPlayer = new MediaPlayer(); // the media player
        public static bool IsOn { get; set; } = false; // Are the effects enable?

        /// <summary>
        /// The action plays a musical effect. Provided the sounds enable
        /// </summary>
        /// <param name="fileName">The name of the file to play</param>
        public static void Play(string fileName)
        {
            if (IsOn)
            {
                _mediaPlayer.Source = MediaSource.CreateFromUri(new Uri($"ms-appx:///Assets/Sounds/{fileName}"));
                _mediaPlayer.Play();              
            }
        }

        /// <summary>
        /// the methode pause the sounds
        /// </summary>
        public static void Stop()
        {
            _mediaPlayer.Pause();
        }
    }
}
