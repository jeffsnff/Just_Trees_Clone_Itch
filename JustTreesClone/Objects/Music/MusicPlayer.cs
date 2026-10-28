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
    private List<Song> _backgroundMusic = new List<Song>();
    private int _track = 1;
    private KeyboardState _previousKeyBoardState = Keyboard.GetState();


    public MusicPlayer(ContentManager Content)
    {
      _backgroundMusic.Add(Content.Load<Song>("music/Adeline Yeo (HP) - With Hush Child Chill Vibes Lofi Music Collaboration"));
      _backgroundMusic.Add(Content.Load<Song>("music/snoozy beats - lofi autumn days"));
      _backgroundMusic.Add(Content.Load<Song>("music/snoozy beats - lofi mood [chill lofi]"));
      _backgroundMusic.Add(Content.Load<Song>("music/snoozy beats - lofi piano beat"));

      MediaPlayer.Volume = 0.10f;
    }

    public void Update(KeyboardState keyBoardState)
    {
      if (keyBoardState.IsKeyDown(Keys.P))
      {
        if (MediaPlayer.State == MediaState.Stopped)
        {
          MediaPlayer.Play(_backgroundMusic[_track]);
        }
      }

      if (keyBoardState.IsKeyDown(Keys.Space) && _previousKeyBoardState.IsKeyUp(Keys.Space))
      {
        if (_track < _backgroundMusic.Count)
        {
          _track++;
        }
        else
        {
          _track = 1;
        }
        MediaPlayer.Stop();
        MediaPlayer.Play(_backgroundMusic[_track-1]);

      }
      _previousKeyBoardState = keyBoardState;
    }

    public void Draw(SpriteBatch spriteBatch, SpriteFont gameFont, Vector2 screenUI)
    {
      spriteBatch.DrawString(
        gameFont, $"{_track}",
        new Vector2(screenUI.X,screenUI.Y),
        Color.Red
        
        );
    }
  }
}
