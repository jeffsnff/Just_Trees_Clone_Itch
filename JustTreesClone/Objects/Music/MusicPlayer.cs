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
    private Texture2D _play;
    private Texture2D _skip;
    private KeyboardState _previousKeyBoardState = Keyboard.GetState();


    public MusicPlayer(ContentManager Content)
    {
      _backgroundMusic.Add(new MusicTrack(Content.Load<Song>("music/Adeline Yeo (HP) - With Hush Child Chill Vibes Lofi Music Collaboration"), "With Hush Child"));
      _backgroundMusic.Add(new MusicTrack(Content.Load<Song>("music/snoozy beats - lofi autumn days"), "Lofi Autumn Days"));
      _backgroundMusic.Add(new MusicTrack(Content.Load<Song>("music/snoozy beats - lofi mood [chill lofi]"), "Lofi Mood [Chill Lofi]"));
      _backgroundMusic.Add(new MusicTrack(Content.Load<Song>("music/snoozy beats - lofi piano beat"), "Lofi Piano Beat"));

      _play = Content.Load<Texture2D>("icons/music/play");
      _skip = Content.Load<Texture2D>("icons/music/next");



      MediaPlayer.Volume = 0.50f;
    }

    public void Update(KeyboardState keyBoardState)
    {
      if (keyBoardState.IsKeyDown(Keys.P))
      {
        if (MediaPlayer.State == MediaState.Stopped)
        {
          MediaPlayer.Play(_backgroundMusic[_track].Song);
        }
      }

      if (keyBoardState.IsKeyDown(Keys.Space) && _previousKeyBoardState.IsKeyUp(Keys.Space))
      {
        _track++;
        if(_track == _backgroundMusic.Count)
        {
          _track = 0;
        }
        MediaPlayer.Stop();
        MediaPlayer.Play(_backgroundMusic[_track].Song);

      }
      _previousKeyBoardState = keyBoardState;
    }

    public void Draw(SpriteBatch spriteBatch, SpriteFont gameFont, Vector2 screenUI)
    {


      spriteBatch.Draw(
        _skip,
        new Vector2(screenUI.X + 10, screenUI.Y + 15),
        null,
        Color.White,
        0.0f,
        Vector2.Zero,
        0.5f, // Scale of sprite
        SpriteEffects.FlipHorizontally,
        0.0f);

      spriteBatch.Draw(
        _play,
        new Vector2(screenUI.X+34, screenUI.Y+15),
        null,
        Color.White,
        0.0f,
        Vector2.Zero,
        0.5f, // Scale of sprite
        SpriteEffects.None,
        0.0f);

      spriteBatch.Draw(
        _skip,
        new Vector2(screenUI.X + 60, screenUI.Y + 15),
        null,
        Color.White,
        0.0f,
        Vector2.Zero,
        0.5f, // Scale of sprite
        SpriteEffects.None,
        0.0f);


      spriteBatch.DrawString(
        gameFont, _backgroundMusic[_track].Name,
        new Vector2(screenUI.X+100,screenUI.Y+22),
        Color.Red
        
        );
    }
  }
}
