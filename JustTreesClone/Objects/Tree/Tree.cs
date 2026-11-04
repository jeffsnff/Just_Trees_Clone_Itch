using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Just_Trees_Clone.Objects.Tree;

public class Tree
{
  public int Health { get; private set; } = 5;
  public bool IsAlive { get; private set; } = true;
  public int XCoord { get; private set; }
  public int YCoord { get; private set; }
  private Texture2D _sprite;
  private int _animationIndex, _animationRow, _animationTimer;

  private Color _color = Color.LawnGreen;

  public Tree(int xCoord, int yCoord, ContentManager Content)
  {
    XCoord = xCoord;
    YCoord = yCoord;
    _sprite = Content.Load<Texture2D>("Sprites/Tree/Trees");
    _animationRow = 0;
    _animationIndex = 2;
  }

  public Rectangle GetBounds()
  {
    return new Rectangle(XCoord+65, YCoord+200, 65, 75);
  }

  public void TakeDamage()
  {
    Health -= 1;
  }

  private void Dead()
  {
    if (Health <= 0)
    {
      IsAlive = false;
    }
  }

  public void Update(Color color)
  {
    _color = color;
    Dead();
  }

  public void Draw(SpriteBatch spriteBatch, Texture2D boundBoxTexture, SpriteFont gameFont)
  {
    spriteBatch.DrawString(gameFont, "Health: " + Health, new Vector2(XCoord, YCoord - 15), Color.White);
    
     spriteBatch.Draw(boundBoxTexture, GetBounds(), _color);
    spriteBatch.Draw(_sprite, new Vector2(XCoord, YCoord), new Rectangle(_animationIndex * 50, _animationRow * 96, 54, 96), Color.White, 0.0f, new Vector2(0, 0), 3.5f, SpriteEffects.None, 0);
    
  }
}