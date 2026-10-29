using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace JustTreesClone.Objects.Music.MediaControllers
{
  public class NextButton
  {
    private Texture2D _next;
    private int _xCoord;
    private int _yCoord;

    public NextButton(ContentManager Content, Vector2 screenUI)
    {
      _next = Content.Load<Texture2D>("icons/music/next");
      _xCoord = (int)screenUI.X + 64;
      _yCoord = (int)screenUI.Y + 15;
    }

    public Rectangle GetBounds()
    {
      return new Rectangle(_xCoord, _yCoord, 64, 64);
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
        SpriteEffects.None,
        0.0f);
    }
  }
}