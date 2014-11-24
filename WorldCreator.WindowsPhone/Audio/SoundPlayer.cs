using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Playback;
using WorldCreator.Common;

namespace WorldCreator.Audio
{
    public class SoundPlayer : ISoundPlayer
    {
        private MediaPlayer player; 

        public SoundPlayer()
        {
            this.player = BackgroundMediaPlayer.Current;
            this.player.MediaOpened += this.MediaPlayer_MediaOpened;
        }

        public void PlaySound(string path)
        {
            this.player.AutoPlay = false;
            this.player.SetUriSource(new Uri(path));
        }

        private void MediaPlayer_MediaOpened(MediaPlayer sender, object args)
        {
            sender.Play();
        }
    }
}
