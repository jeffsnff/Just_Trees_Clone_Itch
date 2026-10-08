using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Just_Trees_Clone.Objects.Helper;

namespace Just_Trees_Clone.Objects.Spirit
{
  internal class Spirit
  {
    public float XCoord { get; private set; } = 50;
    public float YCoord { get; private set; } = 50;
    private int Full { get; set; } = 0;
    public SpiritState State { get; private set; } = SpiritState.Idel;
    private int Speed { get; } = 1;
    private int MoveTimer { get; set; } = 0;

    public Spirit(ContentManager content)
    {

    }

    public Rectangle GetBounds()
    {
      return new Rectangle((int) XCoord,(int) YCoord, 64, 64);
    }

    public void Update()
    {
      if (MoveTimer >= 120)
      {
        MoveTimer = 0;
        Random rng = new Random();
        int choice = rng.Next(0, 3);
        if (State != SpiritState.Idel)
        {
          State = SpiritState.Idel;
        }
        else
        {
          State = (SpiritState) choice;
        }
      }
      
      switch (State)
      {
        case SpiritState.Idel:
          break;
        case SpiritState.Up:
          YCoord -= Speed;
          break;
        case SpiritState.Down:
          YCoord += Speed;
          break;
        case SpiritState.Right:
          XCoord += Speed;
          break;
        case SpiritState.Left:
          XCoord -= Speed;
          break;
      }
      MoveTimer++;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Begin();
      // TODO: Draw Sprite
      spriteBatch.End();
    }

  }
}
