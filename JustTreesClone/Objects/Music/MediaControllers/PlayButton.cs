using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace JustTreesClone.Objects.Music.MediaControllers
{
  public class PlayButton
  {
    private Texture2D _play;
    private int XCoord;
    private int YCoord;

    public PlayButton(ContentManager Content, Vector2 screenUI)
    {
      _play = Content.Load<Texture2D>("icons/music/play");
      XCoord = (int)screenUI.X + 34;
      YCoord = (int)screenUI.Y + 15;
    }

    public Rectangle GetBounds()
    {
      return new Rectangle(XCoord, YCoord, 64, 64);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(
        _play,
        new Vector2(XCoord, YCoord),
        null,
        Color.White,
        0.0f,
        Vector2.Zero,
        0.5f, // Scale of sprite
        SpriteEffects.None,
        0.0f);
    }
  }
}
