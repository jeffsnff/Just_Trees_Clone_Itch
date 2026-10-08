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

    public void Update()
    {
        
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        
        spriteBatch.End();
    }
}