using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Just_Trees_Clone.Objects.Helper;

namespace Just_Trees_Clone.Objects.Player
{
  internal class Player
  {
    public float XCoord { get; private set; }
    public float YCoord { get; private set; }
    private float Speed { get; set; }
    public PlayerState State { get; private set; }
    private bool LeftMouseClicked { get; set; } = false;
    public int NumberOfLogs { get; set; }

    public Player(ContentManager content)
    {
      XCoord = 0;
      YCoord = 0;
      Speed = 5;
      State = PlayerState.Idel;
      NumberOfLogs = 0;
    }


    public Rectangle GetBounds()
    {
      return new Rectangle((int)XCoord, (int)YCoord, 64, 64);
    }

    private void Move()
    {
      KeyboardState currentKeyboardState = Keyboard.GetState();
      MouseState currentMouseState = Mouse.GetState();
      GamePadState currentGamePadState = GamePad.GetState(PlayerIndex.One);
      float horizontalMovement = currentGamePadState.ThumbSticks.Left.X;
      float verticalMovement = currentGamePadState.ThumbSticks.Left.Y;
      
      if (currentGamePadState.Buttons.X != ButtonState.Pressed && 
          currentGamePadState.Buttons.A != ButtonState.Pressed &&
          currentKeyboardState.IsKeyUp(Keys.F) &&
          currentMouseState.LeftButton != ButtonState.Pressed
          )
        State = PlayerState.Idel;
      
      
      if (State == PlayerState.Idel)
      {
        if (horizontalMovement < 0 || 0 < horizontalMovement)
        {
          XCoord = XCoord + horizontalMovement * Speed;
          State = PlayerState.Walking;
        } 
        if (verticalMovement < 0 || 0 < verticalMovement)
        {
          YCoord = YCoord - verticalMovement * Speed;
          State = PlayerState.Walking;
        }
        
        if (currentKeyboardState.IsKeyDown(Keys.D))
        {
          XCoord += Speed;
          State = PlayerState.Walking;
        }
        if (currentKeyboardState.IsKeyDown(Keys.A))
        {
          XCoord -= Speed;
          State = PlayerState.Walking;
        }
        if (currentKeyboardState.IsKeyDown(Keys.W))
        {
          YCoord -= Speed;
          State = PlayerState.Walking;
        }
        if (currentKeyboardState.IsKeyDown(Keys.S))
        {
          YCoord += Speed;
          State = PlayerState.Walking;
        }
      }
      
      if (!LeftMouseClicked && (currentGamePadState.Buttons.X == ButtonState.Pressed ||
                                currentMouseState.LeftButton == ButtonState.Pressed))
      {
        State = PlayerState.Chopping;
        LeftMouseClicked = true;
      }
      if (currentGamePadState.Buttons.A == ButtonState.Pressed || currentKeyboardState.IsKeyDown(Keys.F))
      {
        State = PlayerState.Feeding;
      }

      if (LeftMouseClicked && currentMouseState.LeftButton == ButtonState.Released)
      {
        LeftMouseClicked = false;
      }
    }
    public void Update()
    {
      State = PlayerState.Idel;
      Move();
    }

    public void Draw(SpriteBatch spriteBatch, Texture2D boundingBoxTexture, SpriteFont gameFont)
    {
      // TODO: Draw Sprite
      spriteBatch.DrawString(gameFont, "State: "+State, new Vector2(XCoord, YCoord - 32), Color.White);
      spriteBatch.DrawString(gameFont, "Num Logs: "+NumberOfLogs, new Vector2(XCoord+80, YCoord - 32), Color.White);
      spriteBatch.Draw(boundingBoxTexture, GetBounds(), Color.Red);
      spriteBatch.DrawString(gameFont, "Left Clicked: "+LeftMouseClicked, new Vector2(XCoord, YCoord + 84), Color.White);
    }
  }
}
