using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace JustTreesClone.Objects.Music.MediaControllers
{
  public class PreviousButton
  {
    private Texture2D _next;
    private int _xCoord;
    private int _yCoord;

    public PreviousButton(ContentManager Content, Vector2 screenUI)
    {
      _next = Content.Load<Texture2D>("Icons/Music/next");
      _xCoord = (int)screenUI.X + 10;
      _yCoord = (int)screenUI.Y + 15;
    }

    public Rectangle GetBounds()
    {
      return new Rectangle(_xCoord, _yCoord, 25, 25);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(
        _next,
        new Vector2(_xCoord, _yCoord),
        null,
        Color.White,
        0.0f,
        Vector2.Zero,
        0.5f, // Scale of sprite
        SpriteEffects.FlipHorizontally,
        0.0f);
    }
  }
}