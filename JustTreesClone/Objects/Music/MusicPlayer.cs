using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace JustTreesClone.Objects.Music
{
  public class MusicPlayer
  {
    private List<MusicTrack> _backgroundMusic = new List<MusicTrack>();
    private int _track = 0;

    public MusicPlayer(ContentManager Content)
    {
      _backgroundMusic.Add(new MusicTrack(Content.Load<Song>("music/Adeline Yeo (HP) - With Hush Child Chill Vibes Lofi Music Collaboration"), "With Hush Child"));
      _backgroundMusic.Add(new MusicTrack(Content.Load<Song>("music/snoozy beats - lofi autumn days"), "Lofi Autumn Days"));
      _backgroundMusic.Add(new MusicTrack(Content.Load<Song>("music/snoozy beats - lofi mood [chill lofi]"), "Lofi Mood [Chill Lofi]"));
      _backgroundMusic.Add(new MusicTrack(Content.Load<Song>("music/snoozy beats - lofi piano beat"), "Lofi Piano Beat"));

      MediaPlayer.Volume = 0.50f;
    }

    public void PlayMusic()
    {
      switch (MediaPlayer.State)
      {
        case MediaState.Stopped:
          MediaPlayer.Play(_backgroundMusic[_track].Song);
          break;
        case MediaState.Paused:
          MediaPlayer.Resume();
          break;
        case MediaState.Playing:
          MediaPlayer.Pause();
          break;
      }
    }

    public void NextSong()
    {
      MediaPlayer.Stop();
      _track++;
      if (_track == _backgroundMusic.Count)
      {
        _track = 0;
      }
      MediaPlayer.Play(_backgroundMusic[_track].Song);
    }

    public void PreviousSong()
    {
      MediaPlayer.Stop();
      _track--;
      if (_track == -1)
      {
        _track = _backgroundMusic.Count - 1;
      }
      MediaPlayer.Play(_backgroundMusic[_track].Song);
    }

    public void Draw(SpriteBatch spriteBatch, SpriteFont gameFont, Vector2 screenUI)
    {
      spriteBatch.DrawString(
        gameFont, _backgroundMusic[_track].Name,
        new Vector2(screenUI.X+100,screenUI.Y+22),
        Color.Black
      );
    }
  }
}
