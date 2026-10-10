using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Just_Trees_Clone.Objects.Tree;

public class Tree
{
    public int Health { get; private set; } = 5;
    public bool IsAlive { get; private set; } = true;
    private int XCoord { get; set; }
    private int YCoord { get; set; }

    private Color _color = Color.LawnGreen;

    public Tree()
    {
        Random rng = new Random();
        XCoord = rng.Next(0, 500);
        YCoord = rng.Next(0,500);
    }

    public Rectangle GetBounds()
    {
        return new Rectangle(XCoord, YCoord, 64, 64);
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
        spriteBatch.DrawString(gameFont, "Health: "+Health, new Vector2(XCoord, YCoord-20), Color.White);
        spriteBatch.Draw(boundBoxTexture, GetBounds(), _color);
    }
}